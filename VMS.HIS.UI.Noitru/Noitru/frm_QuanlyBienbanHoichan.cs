using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using VNS.HIS.UI.Baocao;
using VNS.HIS.UI.Forms.Noitru;
using VNS.Libs;
using VMS.HIS.DAL;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;
using SubSonic;
using VNS.HIS.UI.Forms.NGOAITRU;
using System.Transactions;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using VNS.HIS.UI.Classess;
using Aspose.Words;
using System.Diagnostics;
namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_QuanlyBienbanHoichan : Form
    {
        private DataTable m_dtData=new DataTable();
        public TrangthaiNoitru TrangthaiNoitru = TrangthaiNoitru.NoiTru;
        DataTable _mDtKhoanoitru;
        long id_bbhc = -1;
        public frm_QuanlyBienbanHoichan()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            dtToDate.Value = dtFromDate.Value =globalVariables.SysDate;
            Utility.VisiableGridEx(grdList,"ID",globalVariables.IsAdmin);
            InitEvents();
        }
        void InitEvents()
        {
           
            cmdExit.Click += cmdExit_Click;
            cmdTimKiem.Click += cmdTimKiem_Click;
            txtMaluotkham.KeyDown += txtPatientCode_KeyDown;
            chkByDate.CheckedChanged += chkByDate_CheckedChanged;
            Load += frm_QuanlyBienbanHoichan_Load;
            KeyDown += frm_QuanlyBienbanHoichan_KeyDown;
            grdList.MouseDoubleClick += grdList_MouseDoubleClick;
            grdList.SelectionChanged += grdList_SelectionChanged;
        }

        void grdList_SelectionChanged(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdList)) return;
            id_bbhc = Utility.Int64Dbnull(grdList.GetValue("id"),-1);
        }

        void grdList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            cmdUpdate.PerformClick();
        }
        /// <summary>
        /// hàm thực hiện việc thoát Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_QuanlyBienbanHoichan_Load(object sender, EventArgs e)
        {
            
            InitData();
            TimKiemThongTin();
            ModifyCommand();
            
        }
        /// <summary>
        /// hàm thực hiện việc lấy thông tin khoa nội trú
        /// </summary>
        private void InitData()
        {
            autohinhthuchc.Init();
            autoLydohc.Init();

        }
        
        /// <summary>
        /// hàm thực hiện việc tìm kiếm thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdTimKiem_Click(object sender, EventArgs e)
        {
            TimKiemThongTin();
        }
        private void ModifyCommand()
        {
            bool isValid = Utility.isValidGrid(grdList);
            cmdUpdate.Enabled = cmdDelete.Enabled =cmdPrint.Enabled=cmdTrichBBHC.Enabled= isValid;
           
        }

        private void TimKiemThongTin()
        {
            DateTime tungay = chkByDate.Checked ? dtFromDate.Value : new DateTime(1900, 1, 1);
            DateTime denngay = chkByDate.Checked ? dtToDate.Value : new DateTime(1900, 1, 1);
            string ma_luotkham=(Utility.DoTrim(txtMaluotkham.Text));
            string ten_benhnhan=(Utility.DoTrim(txtTennguoibenh.Text));
            string ma_bbanhc=Utility.DoTrim(txtmaBBHC.Text);
           string lydo=autoLydohc.MyCode;
            string hinhthuc=autohinhthuchc.myCode;
            if (ma_luotkham.Length > 0)
            {
                tungay = denngay = new DateTime(1900, 1, 1);
                ten_benhnhan = "";
                ma_bbanhc = "";
                lydo = "-1";
                hinhthuc = "-1";
            }
            m_dtData = SPs.KcbTimkiemdanhsachbienbanhoichan(ma_bbanhc, tungay, denngay,ma_luotkham,ten_benhnhan,lydo,hinhthuc).GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdList, m_dtData, true, true, "1=1", "ngay_hoichan,ten_benhnhan");
            ModifyCommand();
        }

        /// <summary>
        /// hàm thực hiện trạng thái của tmf kiếm từ ngày đến ngày
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Enabled = dtFromDate.Enabled = chkByDate.Checked;
        }
      
        /// <summary>
        /// hàm thưc hiện việc tìm kiếm htoong tin nhanh cho bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPatientCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadMaLanKham();
                chkByDate.Checked = false;
                cmdTimKiem.PerformClick();
            }
        }
        private void LoadMaLanKham()
        {
            MaLuotkham = Utility.sDbnull(txtMaluotkham.Text.Trim());
            if (!string.IsNullOrEmpty(MaLuotkham) && txtMaluotkham.Text.Length < 8)
            {
                MaLuotkham = Utility.AutoFullPatientCode(txtMaluotkham.Text);
                txtMaluotkham.Text = MaLuotkham;
                txtMaluotkham.Select(txtMaluotkham.Text.Length, txtMaluotkham.Text.Length);
            }
         
        }
        /// <summary>
        /// hàm thực hiện việc phím tắt thông tin 
        /// </summary>
        private string MaLuotkham { get; set; }
        private void frm_QuanlyBienbanHoichan_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.F3)cmdTimKiem.PerformClick();
            if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
            if (e.KeyCode == Keys.F2)
            {
                txtMaluotkham.Focus();
                txtMaluotkham.SelectAll();
            }
            if(e.KeyCode==Keys.N&&e.Control)cmdInsert.PerformClick();
            if(e.KeyCode==Keys.U&&e.Control)cmdUpdate.PerformClick();
            if (e.KeyCode == Keys.D && e.Control) cmdDelete.PerformClick();
            if (e.KeyCode == Keys.P && e.Control) cmdPrint.PerformClick();
        }
     
        KcbLuotkham objKcbLuotkham = null;
       

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            frm_ThemBbanhoichan _bbhc = new frm_ThemBbanhoichan();
            _bbhc.m_enAct = action.Insert;
            _bbhc.ucThongtinnguoibenh_doc_v61.txtMaluotkham.Focus();
            _bbhc.ShowDialog();
            modifyCommandButtons();
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            if (globalVariables.IsAdmin || globalVariables.isSuperAdmin || Utility.Coquyen("kcb_bienbanhoichan_sua") || globalVariables.UserName == grdList.GetValue("nguoi_tao").ToString())
            {
            }
            else
            {
                Utility.ShowMsg(string.Format("Biên bản hội chẩn bạn đang chọn do người dùng {0} tạo nên bạn không có quyền sửa. Muốn sửa BBHC của người khác bạn phải là Admin,Super Admin hoặc có quyền (kcb_bienbanhoichan_sua).\nLiên hệ IT Bệnh viện để được hỗ trợ", grdList.GetValue("nguoi_tao").ToString()));
                return;

            }
            frm_ThemBbanhoichan _bbhc = new frm_ThemBbanhoichan();
            _bbhc.bbhc = KcbBienbanhoichan.FetchByID(Utility.Int64Dbnull(grdList.GetValue("id")));
            _bbhc.ucThongtinnguoibenh_doc_v61.txtMaluotkham.Text = Utility.sDbnull(grdList.GetValue("ma_luotkham"));
            _bbhc.ucThongtinnguoibenh_doc_v61.Refresh();
            _bbhc.m_enAct = action.Update;
            _bbhc.ShowDialog();
        }
        
        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Bạn cần chọn biên bản hội chẩn cần xóa");
                    return;
                }
                if (!Utility.Coquyen("kcb_bienbanhoichan_xoa"))
                {
                    Utility.thongbaokhongcoquyen("kcb_bienbanhoichan_xoa", "xóa biên bản hội chẩn");
                    return;
                }
                KcbLuotkham objLuotkham = Utility.getKcbLuotkham(Utility.Int64Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan)), grdList.GetValue(KcbLuotkham.Columns.MaLuotkham).ToString());
                if (objLuotkham == null)
                {
                    Utility.ShowMsg("Bạn cần chọn Biên bản hội chẩn trên lưới trước khi thực hiện xóa biên bản hội chẩn");
                    return;
                }
                long idbbhc = Utility.Int32Dbnull(grdList.GetValue(KcbBienbanhoichan.Columns.Id), -1);
                if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn xóa biên bản hội chẩn với id {0} của người bệnh {1} hay không?", grdList.GetValue(KcbBienbanhoichan.Columns.Id).ToString(), grdList.GetValue("ten_benhnhan").ToString()), "Xác nhận xóa Biên bản hội chẩn", true))
                {
                    try
                    {
                        using (var scope = new TransactionScope())
                        {
                            using (var dbscope = new SharedDbConnectionScope())
                            {
                                new Delete().From(KcbBienbanhoichan.Schema).Where(KcbBienbanhoichan.Columns.Id).IsEqualTo(idbbhc).Execute();
                            }
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Xóa biên bản hội chẩn ID {0} của người bệnh {1} mã khám {2} thành công ", idbbhc, objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                            scope.Complete();
                            Utility.ShowMsg(string.Format("Xóa biên bản hội chẩn cho người bệnh {0} thành công", grdList.GetValue("ten_benhnhan").ToString()));
                            DataRow[] arrDr = m_dtData.Select(string.Format("{0}={1}", KcbBienbanhoichan.Columns.Id, grdList.GetValue(KcbBienbanhoichan.Columns.Id)));
                            if (arrDr.Length > 0)
                                m_dtData.Rows.Remove(arrDr[0]);
                            m_dtData.AcceptChanges();

                        }
                    }
                    catch (Exception ex)
                    {
                        Utility.CatchException(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                modifyCommandButtons();
            }
        }
        void modifyCommandButtons()
        {
            cmdDelete.Enabled =cmdUpdate.Enabled=cmdPrint.Enabled=cmdTrichBBHC.Enabled= Utility.isValidGrid(grdList);
        }
        private void cmdPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    return;
                }
                KcbBienbanhoichan bbhc = KcbBienbanhoichan.FetchByID(Utility.Int64Dbnull(grdList.GetValue("id")));
                KcbLuotkham objLuotkham = Utility.getKcbLuotkham(grdList.CurrentRow);
                if (bbhc == null || bbhc.Id <= 0)
                {
                    Utility.ShowMsg("Bạn cần tạo biên bản hội chẩn trước khi thực hiện in");
                    return;
                }
                DataTable dtData = SPs.KcbLaythongtinBienbanhoichanIn(bbhc.Id).GetDataSet().Tables[0];

                List<string> lstAddedFields = new List<string>() { "khamtimmach_binhthuong", "khamtimmach_batthuong", "khamtimmach_khac",
                "khamhohap_binhthuong", "khamhohap_copd", "khamhohap_khac",
                "phanloaivetmo_sach","phanloaivetmo_sachnhiem",  
                "phanloaivetmo_nhiem", "phanloaivetmo_ban"};

                dtData.TableName = "kcb_bienbanhoichan";
                DataTable dtMergeField = dtData.Clone();
                Utility.AddColums2DataTable(ref dtMergeField, lstAddedFields, typeof(string));
                Document doc;
                DataRow drData = dtData.Rows[0];
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["ten_SYT"] = globalVariables.ParentBranch_Name;
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["diahchi_benhvien"] = globalVariables.Branch_Address;
                drData["SDT_bv"] = globalVariables.Branch_Phone;
                drData["Hotline_bv"] = globalVariables.Branch_Hotline;
                drData["Fax_bv"] = globalVariables.Branch_Fax;
                drData["website_bv"] = globalVariables.Branch_Website;
                drData["email_bv"] = globalVariables.Branch_Email;
                drData["ten_phieu"] = "BIÊN BẢN HỘI CHẨN";
                drData["sngay_hoichan_full"] = Utility.FormatDateTime_giophut_ngay_thang_nam(bbhc.NgayHoichan, "");
                drData["sngay_hoichan"] = Utility.FormatDateTime(bbhc.NgayHoichan);
                drData["ngay_in"] = Utility.FormatDateTime(DateTime.Now);
                drData["sngay_nhapvien"] = Utility.FormatDateTime_giophut_ngay_thang_nam(objLuotkham.NgayNhapvien, "");
                drData["sngay_dukienpttt"] = Utility.FormatDateTime_giophut_ngay_thang_nam(bbhc.DukienthoigianPttt.Value, "");
                Dictionary<string, string> dicMF = new Dictionary<string, string>();
                dicMF.Add("khamtimmach_binhthuong", bbhc.Timach.Value == 0 ? "1" : "0");
                dicMF.Add("khamtimmach_batthuong", bbhc.Timach.Value == 1 ? "1" : "0");
                dicMF.Add("khamtimmach_khac", bbhc.Timach.Value == 2 ? "1" : "0");
                dicMF.Add("khamhohap_binhthuong", bbhc.Hohap.Value == 0 ? "1" : "0");
                dicMF.Add("khamhohap_copd", bbhc.Hohap.Value == 1 ? "1" : "0");
                dicMF.Add("khamhohap_khac", bbhc.Hohap.Value == 2 ? "1" : "0");
                dicMF.Add("phanloaivetmo_sach", bbhc.PhanloaiVetmo.Value == 0 ? "1" : "0");
                dicMF.Add("phanloaivetmo_sachnhiem", bbhc.PhanloaiVetmo.Value == 1 ? "1" : "0");
                dicMF.Add("phanloaivetmo_nhiem", bbhc.PhanloaiVetmo.Value == 2 ? "1" : "0");
                dicMF.Add("phanloaivetmo_ban", bbhc.PhanloaiVetmo.Value == 3 ? "1" : "0");
                List<string> fieldNames = new List<string>();

                string PathDoc = AppDomain.CurrentDomain.BaseDirectory + "Doc\\BIENBAN_HOICHAN.doc";
                string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                string mergeFields = AppDomain.CurrentDomain.BaseDirectory + "MergeFields\\";
                if (!Directory.Exists(mergeFields)) Directory.CreateDirectory(mergeFields);
                Utility.CreateMergeFields(dtMergeField);
                if (!File.Exists(PathDoc))
                {
                    string tieude = "";
                    Utility.GetReport("BIENBAN_HOICHAN", ref tieude, ref PathDoc);
                }
                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg("Không tìm thấy file mẫu in phiếu Biên bản hội chẩn tại thư mục sau :" + PathDoc);
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
                               Path.GetFileNameWithoutExtension(PathDoc), "BIENBAN_HOICHAN", grdList.GetValue("ma_luotkham"), Utility.sDbnull(bbhc.Id), Guid.NewGuid().ToString(), Path.GetExtension(PathDoc));


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

            //try
            //{

            //    DataTable dtData = SPs.KcbLaythongtinBienbanhoichanIn(id_bbhc).GetDataSet().Tables[0];
            //    dtData.TableName = "kcb_bienbanhoichan";
            //    THU_VIEN_CHUNG.CreateXML(dtData, "kcb_bienbanhoichan.xml");
            //    if (dtData == null || dtData.Rows.Count <= 0)
            //    {
            //        Utility.ShowMsg("Không tìm thấy dữ liệu", "Thông báo", MessageBoxIcon.Warning);
            //        return;
            //    }
            //    noitru_inphieu.InBienbanHoichan(dtData, DateTime.Now, true, "noitru_bienbanhoichan");
            //}
            //catch (Exception ex)
            //{

            //    Utility.CatchException(ex);
            //}
        }

        private void lnkDelete_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dtpNgayin.Value = dtToDate.Value = DateTime.Now;
            txtMaluotkham.Clear();
            txtTennguoibenh.Clear();
            txtmaBBHC.Clear();
            autohinhthuchc.SetCode("-1");
            autoLydohc.SetCode("-1");
            txtmaBBHC.Focus();

        }

        private void cmdInPhieuVaoVien_Click(object sender, EventArgs e)
        {

        }

        private void cmdBienBanHoiChan_Click(object sender, EventArgs e)
        {

        }

        private void cmdInCamKet_Click(object sender, EventArgs e)
        {

        }

        private void cmdTrichBBHC_Click(object sender, EventArgs e)
        {
            try
            {

                DataTable dtData = SPs.KcbLaythongtinBienbanhoichanIn(id_bbhc).GetDataSet().Tables[0];
                dtData.TableName = "kcb_bienbanhoichan";
                THU_VIEN_CHUNG.CreateXML(dtData, "kcb_bienbanhoichan.xml");
                if (dtData == null || dtData.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                noitru_inphieu.InBienbanHoichan(dtData, DateTime.Now, true, "noitru_trichbienbanhoichan");
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        private void cmdView_Click(object sender, EventArgs e)
        {
            frm_ThemBbanhoichan _bbhc = new frm_ThemBbanhoichan();
            _bbhc.bbhc = KcbBienbanhoichan.FetchByID(Utility.Int64Dbnull(grdList.GetValue("id")));
            _bbhc.ucThongtinnguoibenh_doc_v61.txtMaluotkham.Text = Utility.sDbnull(grdList.GetValue("ma_luotkham"));
            _bbhc.ucThongtinnguoibenh_doc_v61.Refresh();
            _bbhc.m_enAct = action.View;
            _bbhc.ShowDialog();
        }
     
    }
}
