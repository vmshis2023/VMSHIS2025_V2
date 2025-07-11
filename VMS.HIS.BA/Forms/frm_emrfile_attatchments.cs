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
using VNS.Libs;
using VMS.HIS.DAL;
using Janus.Windows.GridEX;
using System.IO;
using System.Threading;
using NLog;
using VMS.HIS.Bus.Emr;

namespace VMS.HIS.UI.EMR
{
    public partial class frm_emrfile_attatchments : Form
    {
        bool isAllowSelectionChanged = false;
        NLog.Logger log = null;
        DataTable dtFiles;
        List<string> lstFiles;
        public List<string> lstNewID = new List<string>();
        KcbLuotkham objLuotkham;
        public FTPclient FtpClientPDF;
        private string FtpClientCurrentDirectoryPdf = "";
        private readonly string _baseDirectoryPdf = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "emr_attachmentfiles\\");
        public frm_emrfile_attatchments(KcbLuotkham objLuotkham,List<string> lstFiles)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            log = LogManager.GetCurrentClassLogger();
            this.KeyPreview = true;
            this.lstFiles = lstFiles;
            this.objLuotkham = objLuotkham;
            grdList.CellUpdated += GrdList_CellUpdated;
            grdList.ColumnButtonClick += GrdList_ColumnButtonClick;
            cboGayEMR.SelectedValueChanged += CboGayEMR_SelectedValueChanged;
            InitFtp();
        }
        private void InitFtp()
        {
            try
            {
                string FTPServer = "";
                string UID = "";
                string PWD = "";
                string FTPInfor = THU_VIEN_CHUNG.Laygiatrithamsohethong("EMR_ATTATCHMENTFILE_SERVER", string.Format("{0}-{1}-{2}", "127.0.0.1", "emrfile", "emrfile"), true);
                if (FTPInfor.Length > 0 && FTPInfor.Split('-').Count() == 3)
                {
                   FTPServer = FTPInfor.Split('-')[0];
                    UID = FTPInfor.Split('-')[1];
                    PWD = FTPInfor.Split('-')[2];
                }
                if (!Directory.Exists(_baseDirectoryPdf))
                {
                    Directory.CreateDirectory(_baseDirectoryPdf);
                }
                
                FtpClientPDF = new FTPclient(FTPServer, UID, PWD);
                FtpClientPDF.UsePassive = true;
                FtpClientCurrentDirectoryPdf = FtpClientPDF.CurrentDirectory;
                FtpClientCurrentDirectoryPdf = FtpClientCurrentDirectoryPdf + "//" + objLuotkham.MaLuotkham;//Thư mục+mã lượt khám

            }
            catch
            {
            }
        }
        private string CreateFtpPdf(string sourcePath, string fileName)
        {
            try
            {
                log.Trace("Begin Ftp pdf...");
                //if (!_myProperties.EnabledFTP)
                //{
                //    return sourcePath;
                //}
                
                if (!FtpClientPDF.FtpDirectoryExists(FtpClientCurrentDirectoryPdf))
                    FtpClientPDF.FtpCreateDirectory(FtpClientCurrentDirectoryPdf);

                string uploadDirectory = string.Format("{0}/{1}", FtpClientCurrentDirectoryPdf, fileName);
                FtpClientPDF.CurrentDirectory = FtpClientCurrentDirectoryPdf;
                log.Trace(string.Format("sourcePath={0}uploadDirectory={1}", sourcePath, uploadDirectory));
                FtpClientPDF.Upload(sourcePath, uploadDirectory);
                return fileName;
            }
            catch (Exception ex)
            {
                log.Trace(ex.Message);
                Utility.ShowMsg(ex.ToString());
                return "";
            }
        }
        private void GrdList_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            try
            {
                if(Utility.AcceptQuestion(string.Format("Bạn có muốn xóa file {0}",grdList.GetValue("ten_file")),"Xác nhận xóa file",true))
                {
                    grdList.CurrentRow.Delete();
                }    
            }
            catch (Exception ex)
            {

            }
        }

        private void CboGayEMR_SelectedValueChanged(object sender, EventArgs e)
        {
            UpdateGay();
        }
        void UpdateGay()
        {
            try
            {
                if (!isAllowSelectionChanged) return;
                if (grdList.GetCheckedRows().Count() <= 0)
                {
                    Utility.ShowMsg("Bạn cần chọn các hồ sơ chưa sắp xếp vào gáy nào(màu đỏ) trước khi chọn gáy");
                    return;
                }
                foreach (GridEXRow row in grdList.GetCheckedRows())
                {
                    row.BeginEdit();
                    row.Cells["ma_gay"].Value = Utility.sDbnull(cboGayEMR.SelectedValue);
                    row.EndEdit();
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        private void GrdList_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }



        private void ModifyButtons()
        {
            cmdAccept.Enabled = grdList.GetDataRows().Count() > 0;
        }

        private void LoadFiles()
        {
            try
            {
                DataTable dtFiles = new DataTable();
                dtFiles.Columns.AddRange(new DataColumn[] { new DataColumn("ma_file",typeof(string)), new DataColumn("upload_file", typeof(string)), new DataColumn("file_path", typeof(string)), new DataColumn("ten_file", typeof(string)) , new DataColumn("ext", typeof(string)), new DataColumn("ma_gay", typeof(string)), new DataColumn("mota_them", typeof(string)) });
                foreach(string sfile in lstFiles)
                {
                    DataRow dr = dtFiles.NewRow();
                    
                    dr["ma_file"] = Guid.NewGuid();
                    dr["ten_file"] = Path.GetFileNameWithoutExtension(sfile);
                    dr["ext"] = Path.GetExtension(sfile);
                    dr["ma_gay"] = "";
                    dr["upload_file"] = sfile;
                    dr["mota_them"] = "";
                    dr["file_path"] = FtpClientCurrentDirectoryPdf;
                    dtFiles.Rows.Add(dr);
                    Thread.Sleep(1);
                }    
                Utility.SetDataSourceForDataGridEx(grdList, dtFiles, true, true, "1=1", "ten_file");
               
            }
            catch (Exception ex) { }
        }

        private void frm_emrfile_attatchments_Load(object sender, EventArgs e)
        {
            DataTable dtPhieuEMR = new Select("*").From(DmucChung.Schema).Where(DmucChung.Columns.Loai).IsEqualTo("EMR_GAYBA")
               .OrderAsc(DmucChung.Columns.SttHthi)
               .ExecuteDataSet().Tables[0];
            DataBinding.BindDataCombobox(cboGayEMR, dtPhieuEMR, DmucChung.Columns.Ma, DmucChung.Columns.Ten);
            if (grdList.DropDowns.Contains("cboGay"))
            {
                grdList.DropDowns["cboGay"].DataSource = dtPhieuEMR.Copy();
            }
            LoadFiles();
            isAllowSelectionChanged = true;
        }

        private void frm_emrfile_attatchments_KeyDown(object sender, KeyEventArgs e)
        {
        }

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            lstNewID = new List<string>();
            Utility.WaitNow(this);
            Tientrinh.Visible = true;
            Tientrinh.Maximum = grdList.GetDataRows().Count();
            Tientrinh.Minimum = 0;
            Tientrinh.Step = 1;
            Tientrinh.Value = 1;
            List<string> lstChuaCoGay = grdList.GetDataRows().Where(c => Utility.sDbnull(c.Cells["ma_gay"].Value).Length <= 0).Select(c => Utility.sDbnull(c.Cells["ma_file"].Value)).Distinct().ToList<string>();
            if (lstChuaCoGay.Count > 0)
            {
                if (!Utility.AcceptQuestion("Một số hồ sơ chưa xếp vào gáy nào\nBạn có chắc chắn vẫn muốn đẩy hồ sơ lên?\nNhấn Yes để tiếp tục đẩy hồ sơ. Nhấn No để quay lại xếp gáy cho các hồ sơ", "", true))
                {
                    return;
                }
            }
            foreach (GridEXRow row in grdList.GetDataRows())
            {
                try
                {
                    if (Tientrinh.Value + 1 > Tientrinh.Maximum)
                        Tientrinh.Value = Tientrinh.Maximum;
                    else
                        Tientrinh.Value += 1;
                    Application.DoEvents();
                    EmrDocument emrdoc = new EmrDocument();
                    emrdoc.IsNew = true;
                    emrdoc.FileIn = string.Format("{0}.{1}", Utility.sDbnull(row.Cells["ma_file"].Value), Path.GetExtension(Utility.sDbnull(row.Cells["file_path"].Value)));
                    emrdoc.NguonTao = 5;//File đính kèm từ ngoài hệ thống
                    emrdoc.NgayTao = globalVariables.SysDate;
                    emrdoc.NguoiTao = globalVariables.UserName;
                    emrdoc.Title = Utility.sDbnull(row.Cells["ten_file"].Value);
                    emrdoc.MaGayEmr = Utility.sDbnull(row.Cells["ma_gay"].Value);
                    emrdoc.MaPhieu = Utility.sDbnull(row.Cells["ma_file"].Value);
                    emrdoc.IdBenhnhan = objLuotkham.IdBenhnhan;
                    emrdoc.MaLuotkham = objLuotkham.MaLuotkham;
                    emrdoc.NgayPhieu = globalVariables.SysDate;
                    emrdoc.LoaiPhieuHis = Loaiphieu_HIS.FILE_DINHKEM;
                    emrdoc.Ext = Utility.sDbnull(row.Cells["ext"].Value);
                    emrdoc.MotaThem = Utility.sDbnull(row.Cells["mota_them"].Value);
                    emrdoc.FilePath = Utility.sDbnull(row.Cells["file_path"].Value);
                    emrdoc.TthaiAn = false;
                    emrdoc.TthaiChiase = false;
                    emrdoc.TthaiDuyet = 0;
                    emrdoc.TthaiHuy = false;
                    emrdoc.TthaiKydientu = false;
                    emrdoc.TthaiKyso = false;
                    emrdoc.TthaiXoa = false;
                    emrdoc.Save();
                    string upload_file= Utility.sDbnull(row.Cells["upload_file"].Value); 
                    lstNewID.Add(emrdoc.IdFile.ToString());
                    //Lưu FTP
                    CreateFtpPdf(upload_file, emrdoc.FileIn);
                }
                catch (Exception ex)
                {
                    Utility.CatchException(ex);
                }
            }
            Tientrinh.Visible = false;
            Utility.DefaultNow(this);
            Utility.ShowMsg(string.Format("Đã đẩy thành công {0} tệp tin đính kèm vào hệ thống. Nhấn OK để quay lại màn hình EMR", lstFiles.Count));
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cmdthoat_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            UpdateGay();
        }
    }
}
