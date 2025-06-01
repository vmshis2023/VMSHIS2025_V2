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
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using Aspose.Words;
namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_Quanly_phieutuvanPTTT : Form
    {
        private DataTable m_dtData=new DataTable();
        public TrangthaiNoitru TrangthaiNoitru = TrangthaiNoitru.NoiTru;
        DataTable _mDtKhoanoitru;
        public frm_Quanly_phieutuvanPTTT()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            dtToDate.Value = dtFromDate.Value =globalVariables.SysDate;
            Utility.VisiableGridEx(grdList,"id_phieu",globalVariables.IsAdmin);
            InitEvents();
        }
        void InitEvents()
        {
           
            cmdExit.Click += cmdExit_Click;
            cmdTimKiem.Click += cmdTimKiem_Click;
            txtMaluotkham.KeyDown += txtPatientCode_KeyDown;
            chkByDate.CheckedChanged += chkByDate_CheckedChanged;
            Load += frm_Quanly_phieutuvanPTTT_Load;
            KeyDown += frm_Quanly_phieutuvanPTTT_KeyDown;
            grdList.MouseDoubleClick += grdList_MouseDoubleClick;
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

        private void frm_Quanly_phieutuvanPTTT_Load(object sender, EventArgs e)
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
            cmdUpdate.Enabled = cmdPrint.Enabled = cmdXoa.Enabled =isValid;
            //cmdDelete.Enabled = cmdTrichBBHC.Enabled = false;
           
        }

        private void TimKiemThongTin()
        {
            DateTime tungay = chkByDate.Checked ? dtFromDate.Value : new DateTime(1900, 1, 1);
            DateTime denngay = chkByDate.Checked ? dtToDate.Value : new DateTime(1900, 1, 1);
            string ma_luotkham=(Utility.DoTrim(txtMaluotkham.Text));
            string ten_benhnhan=(Utility.DoTrim(txtTennguoibenh.Text));
            string maPhieutuvanPTTT=Utility.DoTrim(txtmaBBHC.Text);
            if (ma_luotkham.Length > 0)
            {
                tungay = denngay = new DateTime(1900, 1, 1);
                ten_benhnhan = "";
                maPhieutuvanPTTT = "";
            }
            m_dtData = SPs.KcbPhieutuvanPtttTimkiem(-1,tungay, denngay, -1, ma_luotkham, ten_benhnhan).GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdList, m_dtData, true, true, "1=1", "ngay_tuvan,ten_benhnhan");
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
        private void frm_Quanly_phieutuvanPTTT_KeyDown(object sender, KeyEventArgs e)
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
           // if (e.KeyCode == Keys.D && e.Control) cmdDelete.PerformClick();
            if (e.KeyCode == Keys.P && e.Control) cmdPrint.PerformClick();
        }
     
        KcbLuotkham objKcbLuotkham = null;
       

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            frm_phieutuvanPTTT PhieutuvanPTTT = new frm_phieutuvanPTTT();
            PhieutuvanPTTT._OnCreated += PhieutuvanPTTT__OnCreated;
            PhieutuvanPTTT.m_enAct = action.Insert;
            PhieutuvanPTTT.ucThongtinnguoibenh_doc_v11.txtMaluotkham.Focus();
            PhieutuvanPTTT.ShowDialog();
        }

        void PhieutuvanPTTT__OnCreated(long id,action m_enAct)
        {
            try
            {
                DataTable dt_temp = SPs.KcbPhieutuvanPtttTimkiem(id, new DateTime(1900, 1, 1), new DateTime(1900, 1, 1), -1, "", "").GetDataSet().Tables[0];
                if (m_enAct == action.Delete)
                {
                    if (DeleteMe())
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
                ModifyCommand();
            }
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                frm_phieutuvanPTTT PhieutuvanPTTT = new frm_phieutuvanPTTT();
                PhieutuvanPTTT._OnCreated += PhieutuvanPTTT__OnCreated;
                PhieutuvanPTTT.tuvanPttt = KcbPhieutuvanPttt.FetchByID(Utility.Int64Dbnull(grdList.GetValue("Id_phieu")));
                PhieutuvanPTTT.ucThongtinnguoibenh_doc_v11.txtMaluotkham.Text = Utility.sDbnull(grdList.GetValue("ma_luotkham"));
                PhieutuvanPTTT.ucThongtinnguoibenh_doc_v11.Refresh();
                PhieutuvanPTTT.m_enAct = action.Update;
                PhieutuvanPTTT.ShowDialog();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.Coquyen("kcbPhieutuvanPTTT_xoa"))
                {
                    Utility.ShowMsg("Bạn không có quyền xóa phiếu Phiếu tư vấn PTTT(kcbPhieutuvanPTTT_xoa)");
                    return;
                }
                KcbLuotkham objLuotkham = Utility.getKcbLuotkham(Utility.Int64Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan)), grdList.GetValue(KcbLuotkham.Columns.MaLuotkham).ToString());
                if (objLuotkham == null)
                {
                    Utility.ShowMsg("Bạn cần chọn phiếu Phiếu tư vấn PTTT trên lưới trước khi thực hiện xóa phiếu Phiếu tư vấn PTTT");
                    return;
                }
                if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn xóa phiếu Phiếu tư vấn PTTT với mã {0} của người bệnh {1} hay không?", grdList.GetValue(KcbBienbanhoichan.Columns.MaBbhc).ToString(), grdList.GetValue("ten_benhnhan").ToString()), "Xác nhận hủy chuyển viện", true))
                {

                    if (DeleteMe())
                    {
                        Utility.ShowMsg(string.Format("Xóa phiếu Phiếu tư vấn PTTT cho người bệnh {0} thành công", grdList.GetValue("ten_benhnhan").ToString()));
                        DataRow[] arrDr = m_dtData.Select(string.Format("{0}={1}", KcbBienbanhoichan.Columns.Id, grdList.GetValue(KcbBienbanhoichan.Columns.Id)));
                        if (arrDr.Length > 0)
                            m_dtData.Rows.Remove(arrDr[0]);
                        m_dtData.AcceptChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        bool DeleteMe()
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        new Delete().From(KcbBienbanhoichan.Schema).Where(KcbBienbanhoichan.Columns.Id).IsEqualTo(Utility.Int32Dbnull(grdList.GetValue(KcbBienbanhoichan.Columns.Id), -1)).Execute();
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
        private void lnkDelete_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dtpNgayin.Value = dtToDate.Value = DateTime.Now;
            txtMaluotkham.Clear();
            txtTennguoibenh.Clear();
            txtmaBBHC.Clear();
            //autohinhthuchc.SetCode("-1");
            txtmaBBHC.Focus();

        }

        private void cmdScanFinger_Click(object sender, EventArgs e)
        {
            RegisterFinger();
        }
        internal static IntPtr hWnd;
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern void SendMessageW(IntPtr hWnd, uint msg, uint wParam, uint lParam);
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindowW(string className, string windowName);
        internal static Process process;

        void RegisterFinger()
        {
            try
            {
                string patientID = Utility.sDbnull(grdList.CurrentRow.Cells["id_benhnhan"].Value, "");
                if (Utility.Int32Dbnull(patientID, -1) > 0)
                {
                    List<string> _list = new List<string>();
                    _list.Add(patientID.ToString());
                    _list.Add(0.ToString());
                    string sPatientInforFile = Application.StartupPath + @"\IVF_FR\PatientInfor.txt";
                    string appName = Application.StartupPath + @"\IVF_FR\IVF_FingerPrint.exe";
                    if (File.Exists(sPatientInforFile))
                    {
                        File.WriteAllLines(sPatientInforFile, _list.ToArray());
                    }
                    else
                    {
                        File.CreateText(sPatientInforFile);
                        File.WriteAllLines(sPatientInforFile, _list.ToArray());
                    }
                    Utility.KillProcess(appName);
                    Thread.Sleep(100);
                    process = Process.Start(Application.StartupPath + @"\IVF_FR\IVF_FingerPrint.exe");
                    if (process != null) process.WaitForExit();
                    WaitForSingleObject(process.Handle, 0xffffffff);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void cmdXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (Utility.Coquyen("noitruPhieutuvanPTTT_xoa") || globalVariables.UserName == grdList.GetValue("nguoi_tao"))
                {
                }
                else
                {
                    Utility.thongbaokhongcoquyen("noitruPhieutuvanPTTT_xoa", "xóa phiếu phẫu thuật thủ thuật");
                    return;
                }
                if (!Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Bạn cần chọn một phiếu Phiếu tư vấn PTTT trên danh sách để xóa. Vui lòng kiểm tra lại");
                    return;
                }
                if (Utility.AcceptQuestion("Bạn có muốn xóa thông tin phiếu Phiếu tư vấn PTTT đang chọn không ?", "Thông báo", true))
                {
                    int banghi = new Delete().From<KcbPhieutuvanPttt>()
                         .Where(KcbPhieutuvanPttt.Columns.IdPhieu)
                         .IsEqualTo(Utility.Int32Dbnull(grdList.GetValue("Id_phieu")))
                         .Execute();
                    if (banghi > 0)
                    {
                        Utility.ShowMsg("Bạn xóa thông tin phiếu Phiếu tư vấn PTTT thành công", "Thông báo");
                        DataRow dr = ((DataRowView)grdList.CurrentRow.DataRow).Row;
                        m_dtData.Rows.Remove(dr);
                        m_dtData.AcceptChanges();

                    }

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


        private void cmdPrint_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Bạn cần chọn Tờ Phiếu tư vấn PTTT trên lưới danh sách để thực hiện in");
                    return;
                }
                KcbPhieutuvanPttt tuvanPTTT = KcbPhieutuvanPttt.FetchByID(Utility.Int64Dbnull(grdList.GetValue("id_phieu")));
                if (tuvanPTTT == null || tuvanPTTT.IdPhieu <= 0)
                {
                    Utility.ShowMsg("Bạn cần tạo tờ Phiếu tư vấn PTTT trước khi thực hiện in");
                    return;
                }

                DataTable dtData = SPs.KcbPhieutuvanPtttIn(tuvanPTTT.IdPhieu, Utility.Int64Dbnull(grdList.GetValue("id_benhnhan")), Utility.sDbnull(grdList.GetValue("ma_luotkham"))).GetDataSet().Tables[0];
                dtData.TableName = "PhieutuvanPTTT";
                THU_VIEN_CHUNG.CreateXML(dtData, "PhieutuvanPTTT.xml");
                if (dtData == null || dtData.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                dtData.TableName = "PhieutuvanPTTT";
                List<string> lstAddedFields = new List<string>() { "thuoc1", "thuoc2", "thuoc3", "thuoc4", "giamdau1", "giamdau2" };
                DataTable dtMergeField = dtData.Clone();
                Utility.AddColums2DataTable(ref dtMergeField, lstAddedFields, typeof(string));  
                Document doc;
                DataRow drData = dtData.Rows[0];
                drData["dia_diem"] = globalVariables.gv_strDiadiem;
                drData["ngay_thang_nam"] = Utility.FormatDateTime_gio_ngay_thang_nam(tuvanPTTT.NgayTuvan, "Lúc");
                Dictionary<string, string> dicMF = new Dictionary<string, string>();
                dicMF.Add("thuoc1", tuvanPTTT.ThuocVt.Split(',')[0]);
                dicMF.Add("thuoc2", tuvanPTTT.ThuocVt.Split(',')[1]);
                dicMF.Add("thuoc3", tuvanPTTT.ThuocVt.Split(',')[2]);
                dicMF.Add("thuoc4", tuvanPTTT.ThuocVt.Split(',')[3]);
                dicMF.Add("giamdau1", tuvanPTTT.ThuthuatGiamdau.Split(',')[0]);
                dicMF.Add("giamdau2", tuvanPTTT.ThuthuatGiamdau.Split(',')[1]);

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
                    Utility.GetReport("PhieutuvanPTTT", ref tieude, ref PathDoc);
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
                               Path.GetFileNameWithoutExtension(PathDoc), "PhieutuvanPTTT", Utility.sDbnull(grdList.GetValue("ma_luotkham")), Utility.sDbnull(tuvanPTTT.IdPhieu), Guid.NewGuid().ToString(), Path.GetExtension(PathDoc));


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

       

        
    }
}
