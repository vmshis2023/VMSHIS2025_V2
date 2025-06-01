using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Printing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Aspose.Words;
using Aspose.Words.Drawing;
using Aspose.Words.Reporting;
using Aspose.Words.Tables;
using CrystalDecisions.CrystalReports.Engine;
using Janus.Windows.GridEX;
using Janus.Windows.UI.Tab;
using NLog;
using SubSonic;
using VNS.HIS.BusRule.Classes;
using VNS.HIS.DAL;
using VNS.HIS.UI.DANHMUC;
using VNS.HIS.UI.HinhAnh;
using VNS.Libs;
using VNS.Properties;
using VNS.UCs;
using WPF.UCs;

namespace VNS.HIS.UI.Forms.CanLamSang
{
    //0=Mới chỉ định;1=Đã chuyển CLS;2=Đang thực hiện;3= Đã có kết quả CLS;4=Đã xác nhận kết quả
    public partial class FrmQuanlycacphongchucnang : Form
    {
        #region "biến thực hiện việc xử lý ảnh"

        private readonly string _mStrMaDichvu = "SA";
        public string SPatientCode = "";
        public int VPatientId = -1;
        private Logger _log;
        private DataTable _mDKcbChidinhclsChitiet = new DataTable();
        private KcbChidinhclsChitiet _objKcbChidinhclsChitiet;
        private int _vIdChitietchidinh = -1;
        #endregion
        private Document _doc;
        private Document _doc2;
        private DataTable _dtData;
        private bool _mBlnForced2GetImagesFromFtp;
        #region "Khởi tạo form thực hiện "
        public string DocChuan = "SA";
        public FTPclient FtpClient;
        public string FtpClientCurrentDirectory;
        public string Mabaocao = "SA";

        public FrmQuanlycacphongchucnang(string serviceCode)
        {
            InitializeComponent();
            _log = LogManager.GetCurrentClassLogger();
            LoadLaserPrinters();
            dtmFrom.Value = globalVariables.SysDate;
            dtmTo.Value = dtmFrom.Value;
            cboPatientSex.SelectedIndex = 0;
            _mStrMaDichvu = serviceCode;
            cmdExit.Click += cmdExit_Click;
          //  cmdPrintRadio.Click += cmdPrint_Click;
            //cmdSave.Click += cmdSave_Click;
            LoadDataSysConfigRadio();
            cmdConfig.Visible = globalVariables.IsAdmin;
            InitEvents();
            CauHinh();
        }
        private void InitEvents()
        {
            mnuBrowseImage.Click += mnuBrowseImage_Click;
            mnuDeleteImage.Click += mnuDeleteImage_Click;
            //txtVKS._OnEnterMe += new UCs.AutoCompleteTextbox.OnEnterMe(txtVKS__OnEnterMe);
            imgBox1._OnBrowseImage += imgBox1__OnBrowseImage;
            imgBox2._OnBrowseImage += imgBox2__OnBrowseImage;
            imgBox3._OnBrowseImage += imgBox3__OnBrowseImage;
            imgBox4._OnBrowseImage += imgBox4__OnBrowseImage;
            imgBox1._OnDeleteImage += imgBox1__OnDeleteImage;
            imgBox2._OnDeleteImage += imgBox2__OnDeleteImage;
            imgBox3._OnDeleteImage += imgBox3__OnDeleteImage;
            imgBox4._OnDeleteImage += imgBox4__OnDeleteImage;
            imgBox1._OnViewImage += imgBox__OnViewImage;
            imgBox2._OnViewImage += imgBox__OnViewImage;
            imgBox3._OnViewImage += imgBox__OnViewImage;
            imgBox4._OnViewImage += imgBox__OnViewImage;
            cmdBrowseMauChuan.Click += cmdBrowseMauChuan_Click;
            mnuView.Click += mnuView_Click;
            lnkMore.Click += lnkMore_Click;
            //lnkSize.Click += lnkSize_Click;
            chkPreview.CheckedChanged += chkPreview_CheckedChanged;
            chkInsaukhiluu.CheckedChanged += chkInsaukhiluu_CheckedChanged;
          //  cmdConfig.Click += cmdConfig_Click;
            txtMauKQ._OnEnterMe += txtMauKQ__OnEnterMe;
            lnkAutoCorrect.Click += lnkAutoCorrect_Click;
            lnkDelFTPImages.Click += lnkDelFTPImages_Click;
            lnkGetImagesFromFTP.Click += lnkGetImagesFromFTP_Click;
            cboLaserPrinters.SelectedIndexChanged += cboLaserPrinters_SelectedIndexChanged;
        }
        private void cboLaserPrinters_SelectedIndexChanged(object sender, EventArgs e)
        {
            cmdPrintRadio.Enabled = cboLaserPrinters.Items.Count > 0 && cboLaserPrinters.SelectedIndex >= 0;
        }
        private void lnkGetImagesFromFTP_Click(object sender, EventArgs e)
        {
            GetImagesFromFtp();
        }
        private void lnkDelFTPImages_Click(object sender, EventArgs e)
        {
            DelFtpImages();
        }
        private void lnkAutoCorrect_Click(object sender, EventArgs e)
        {
            var dmucDchung = new DMUC_DCHUNG("AUTOCORRECT");
            dmucDchung.ShowDialog();
        }
        private void txtMauKQ__OnEnterMe()
        {
            txtMauchuan.Text = txtMauKQ.Text;
            DocChuan = txtMauchuan.Text;
            FillDynamicValues();
        }
        private void imgBox__OnViewImage(ImgBox imgBox)
        {
            if (File.Exists(Utility.sDbnull(imgBox.Tag, "")))
                Utility.OpenProcess(Utility.sDbnull(imgBox.Tag, ""));
        }
        private void cmdConfig_Click(object sender, EventArgs e)
        {
            var properties = new frm_Properties(PropertyLib._FTPProperties);
            properties.ShowDialog();
            SaveConfig();
            CauHinh();
        }
        private void chkInsaukhiluu_CheckedChanged(object sender, EventArgs e)
        {
            PropertyLib._FTPProperties.PrintAfterSave = chkInsaukhiluu.Checked;
            PropertyLib.SaveProperty(PropertyLib._FTPProperties);
        }
        private void chkPreview_CheckedChanged(object sender, EventArgs e)
        {
            PropertyLib._FTPProperties.PrintPreview = chkPreview.Checked;
            PropertyLib.SaveProperty(PropertyLib._FTPProperties);
        }
        private void mnuView_Click(object sender, EventArgs e)
        {
        }
        //private void lnkSize_Click(object sender, EventArgs e)
        //{
        //    var properties = new frm_Properties(PropertyLib._DynamicInputProperties);
        //    if (properties.ShowDialog() == DialogResult.OK)
        //    {
        //        FillDynamicValues();
        //    }
        //}

        private void lnkMore_Click(object sender, EventArgs e)
        {
            int idChitietdichvu = Utility.Int32Dbnull(txtIdDichvuChitiet.Text, -1);

            DmucDichvuclsChitiet objDichvuchitiet = DmucDichvuclsChitiet.FetchByID(idChitietdichvu);
            try
            {
                if (objDichvuchitiet == null)
                {
                    Utility.ShowMsg("Bạn cần chọn chỉ định chi tiết cần cập nhật kết quả");
                    return;
                }
                if (txtMauKQ.MyCode == "-1")
                {
                    Utility.ShowMsg("Bạn cần chọn mẫu kết quả của dịch vụ trước khi tạo thông tin nhập kết quả");
                    return;
                }
                var dynamicSetup = new frm_DynamicSetup
                {
                    objDichvuchitiet = objDichvuchitiet,
                    MafileDoc = txtMauKQ.MyCode,
                    ImageID = -1,
                    Id_chidinhchitiet = -1
                };
                if (dynamicSetup.ShowDialog() == DialogResult.OK)
                {
                    FillDynamicValues();
                    FocusMe(flowDynamics);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi: " + ex.Message);
            }
        }

        private void cmdBrowseMauChuan_Click(object sender, EventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                txtMauchuan.Text = Path.GetFileName(openFileDialog.FileName);
                DocChuan = txtMauchuan.Text;
                new Update(DmucDichvuclsChitiet.Schema).Set(DmucDichvuclsChitiet.Columns.MauChuan)
                    .EqualTo(txtMauchuan.Text)
                    .Where(DmucDichvuclsChitiet.Columns.IdChitietdichvu)
                    .IsEqualTo(Utility.Int32Dbnull(txtIdDichvuChitiet.Text, -1))
                    .Execute();
            }
        }


        private void imgBox1__OnDeleteImage(ImgBox imgBox)
        {
            chkSaveImg.Checked = true;
            pic1.Tag = "";
        }

        private void imgBox2__OnDeleteImage(ImgBox imgBox)
        {
            chkSaveImg.Checked = true;
            pic2.Tag = "";
        }

        private void imgBox3__OnDeleteImage(ImgBox imgBox)
        {
            chkSaveImg.Checked = true;
            pic3.Tag = "";
        }

        private void imgBox4__OnDeleteImage(ImgBox imgBox)
        {
            chkSaveImg.Checked = true;
            pic4.Tag = "";
        }

        private void imgBox1__OnBrowseImage(ImgBox imgBox)
        {
            chkSaveImg.Checked = true;
            pic1.Tag = imgBox1.Tag;
        }

        private void imgBox2__OnBrowseImage(ImgBox imgBox)
        {
            chkSaveImg.Checked = true;
            pic2.Tag = imgBox2.Tag;
        }

        private void imgBox3__OnBrowseImage(ImgBox imgBox)
        {
            chkSaveImg.Checked = true;
            pic3.Tag = imgBox3.Tag;
        }

        private void imgBox4__OnBrowseImage(ImgBox imgBox)
        {
            chkSaveImg.Checked = true;
            pic4.Tag = imgBox4.Tag;
        }


        private void CauHinh()
        {
            try
            {
                chkPreview.Checked = PropertyLib._FTPProperties.PrintPreview;
                chkInsaukhiluu.Checked = PropertyLib._FTPProperties.PrintAfterSave;
                lnkGetImagesFromFTP.Enabled = PropertyLib._FTPProperties.Push2FTP;
                lnkDelFTPImages.Enabled = PropertyLib._FTPProperties.Push2FTP;
                FtpClient = new FTPclient(PropertyLib._FTPProperties.IPAddress, PropertyLib._FTPProperties.UID, PropertyLib._FTPProperties.PWD);
                FtpClientCurrentDirectory = FtpClient.CurrentDirectory;

                _baseDirectory = Utility.DoTrim(PropertyLib._FTPProperties.ImageFolder);
                if (_baseDirectory.EndsWith(@"\"))
                    _baseDirectory = _baseDirectory.Substring(0, _baseDirectory.Length - 1);
                if (!Directory.Exists(_baseDirectory))
                {
                    Directory.CreateDirectory(_baseDirectory);
                }
                FillDynamicValues();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi: " + ex.Message);
            }
        }

        #endregion

        #region "hàm thực hiện sự kiện của form"

        /// <summary>
        ///     tìm kiếm thông tin tìm kiếm thông tin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            SearchData();
            // KcbChidinhclsChitiet.Columns.g
        }

        /// <summary>
        ///     tìm kiếm nhanh thông tin của form
        /// </summary>
        private void SearchData()
        {
            try
            {
                _objKcbChidinhclsChitiet = null;
                int id_benhnhan = -1;
                string maLuotkham = Utility.DoTrim(txtMaluotkham_tk.Text);
                if (maLuotkham == "") maLuotkham = "NULL";
                string tenBenhnhan = Utility.DoTrim(txtTenbenhnhan_tk.Text);
                if (tenBenhnhan == "") tenBenhnhan = "NULL";
                byte trangthaiXacnhan = 1;
                if (radChuaXacNhan.Checked) trangthaiXacnhan = 1;
                if (radDaXacNhan.Checked) trangthaiXacnhan = 4;
                if (radChoXacNhan.Checked) trangthaiXacnhan = 3;
                _mDKcbChidinhclsChitiet =
                    SPs.HinhanhTimkiembnhan(id_benhnhan, maLuotkham, tenBenhnhan, trangthaiXacnhan,
                        chkByDate.Checked ? dtmFrom.Value.ToString("dd/MM/yyyy") : "01/01/1900",
                        chkByDate.Checked ? dtmTo.Value.ToString("dd/MM/yyyy") : "01/01/1900"
                        , Utility.Int32Dbnull(cboObjectType.SelectedValue, -1),
                        Utility.Int32Dbnull(cboPatientSex.SelectedValue, -1),_mStrMaDichvu).GetDataSet().Tables[0];
                THU_VIEN_CHUNG.CreateXML(_mDKcbChidinhclsChitiet, "HINHANH.xml");
                Utility.SetDataSourceForDataGridEx(grdList, _mDKcbChidinhclsChitiet, true, true,"",
                   // _mStrMaDichvu == "ALL" ? "1=1" : VKcbChidinhcl.Columns.MaDichvu + "='" + _mStrMaDichvu + "'",
                    "ten_benhnhan");
                if (grdList.RowCount > 0)
                {
                    Utility.SetMsg(lblMsg, "Mời bạn tiếp tục thực hiện công việc", false);
                    grdList.MoveFirst();
                }
                else
                {
                    Utility.SetMsg(lblMsg, "Không có dữ liệu theo điều kiện bạn chọn", true);
                }
                ModifyButtonCommand();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi: " + ex.Message);
            }
        }

        public static void InphieuHa(DataTable mDtReport, DateTime ngayIn, int coHa)
        {
            if (mDtReport == null || mDtReport.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy thông tin ", "Thông báo", MessageBoxIcon.Warning);
                return;
            }
            string tieude = "", reportname = "";
            ReportDocument crpt = Utility.GetReport("thamkham_InphieuHinhAnh_A4", ref tieude, ref reportname);
            var objForm = new frmPrintPreview(tieude, crpt, true, mDtReport.Rows.Count > 0);
            int idDichvu = Utility.Int32Dbnull(Utility.GetValueOfDataRowFields(mDtReport.Rows[0], "id_dichvu", -1), -1);
            DmucDichvucl dmucDichvucl = DmucDichvucl.FetchByID(idDichvu);
            if (dmucDichvucl != null) tieude = dmucDichvucl.MotaThem;
            Utility.UpdateLogotoDatatable(ref mDtReport);
            try
            {
                mDtReport.AcceptChanges();
                crpt.SetDataSource(mDtReport);

                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = "thanhtoan_Hoadondo";
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);

                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(ngayIn));
                Utility.SetParameterValue(crpt, "coHA", coHa);
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
            }
            catch (Exception ex)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(ex.ToString());
                }
            }
        }

        private void chkBydate_CheckedChanged(object sender, EventArgs e)
        {
            dtmFrom.Enabled = chkByDate.Checked;
            dtmTo.Enabled = chkByDate.Checked;
        }


        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frm_quanlycacphongchucnang_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ProcessTabKey(true);
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.F4) cmdPrintRadio.PerformClick();
            if (e.KeyCode == Keys.F3) cmdSearch.PerformClick();
            if (e.KeyCode == Keys.F5) BeginExam();
            if (e.KeyCode == Keys.F11 && PropertyLib._AppProperties.ShowActiveControl)
                Utility.ShowMsg(ActiveControl.Name);
        }


        /// <summary>
        ///     hàm thực hiện việc hiên thị các nút thông tin của trạng thía
        /// </summary>
        private void ModifyButtonCommand()
        {
            try
            {
                toolChooseBN.Enabled = Utility.isValidGrid(grdList);
                toolPrintRadio.Enabled = Utility.isValidGrid(grdList) &&
                                         Utility.Int32Dbnull(
                                             grdList.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.TrangThai].Value) >=
                                         3;

                toolAccept.Enabled = Utility.isValidGrid(grdList) &&
                                     Utility.Int32Dbnull(
                                         grdList.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.TrangThai].Value) == 3;
                toolUnAccept.Enabled = Utility.isValidGrid(grdList) &&
                                       Utility.Int32Dbnull(
                                           grdList.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.TrangThai].Value) == 4;
                toolUnAccept.Visible = globalVariables.IsAdmin;
            }
            catch (Exception ex)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(ex.ToString());
                }
            }
        }

        /// <summary>
        ///     thực hienj việc load thông tin của Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_quanlycacphongchucnang_Load(object sender, EventArgs e)
        {
            InitData();

            ModifyCommand();
            _log = LogManager.GetCurrentClassLogger();

            _log.Trace("Form Load OK");
            TabInfo.SelectedTab = tabDanhsach;
            txtMaluotkham.Focus();
            txtMaluotkham.SelectAll();
            dtpPrintDate.Value = globalVariables.SysDate;
            //  cmdSearchListRadio.PerformClick();
        }

        /// <summary>
        ///     hàm thực hiện việc load thông tin của phần config hình ảnh
        /// </summary>
        private void LoadDataSysConfigRadio()
        {
            SysConfigRadio objConfigRadio = SysConfigRadio.FetchByID(1);
            if (objConfigRadio != null)
            {
                PropertyLib._FTPProperties.UNCPath = Utility.sDbnull(objConfigRadio.PathUNC, "");
                PropertyLib._FTPProperties.PWD = Utility.sDbnull(objConfigRadio.PassWord, "");
                PropertyLib._FTPProperties.IPAddress = Utility.sDbnull(objConfigRadio.Domain, "");
                PropertyLib._FTPProperties.UID = Utility.sDbnull(objConfigRadio.UserName, "");
            }
        }


        private void grdList_ApplyingFilter(object sender, CancelEventArgs e)
        {
            ModifyButtonCommand();
        }

        private void TabInfo_TabIndexChanged(object sender, EventArgs e)
        {
            ModifyButtonCommand();
        }

        private void TabInfo_ChangingSelectedTab(object sender, TabCancelEventArgs e)
        {
            ModifyButtonCommand();
        }

        private void TabInfo_SelectedTabChanged(object sender, TabEventArgs e)
        {
            ModifyButtonCommand();
        }


        /// <summary>
        ///     hàm thực hiện chọn thông tin bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdList_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            if (!Utility.isValidGrid(grdList)) return;
            if (e.Column.Key == "colChooseBN")
            {
                BeginExam();
            }
            else
            {
                txtMaluotkham.Text = "";
            }
        }

        private void BeginExam()
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                TabInfo.SelectedTab = TabInfo.TabPages[0];
                Application.DoEvents();
                var dr = grdList.CurrentRow.DataRow as DataRowView;
                //Fill Infor
                Utility.SetMsg(lblMsg, "Đang nạp thông tin bệnh nhân...", false);
                txtMaluotkham.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.MaLuotkham], "");
                txtTenBN.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.TenBenhnhan], "");
                txtIdBenhnhan.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.IdBenhnhan], "");
                txtGioitinh.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.GioiTinh], "");
                txtTuoi.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.Tuoi], "");
                txtDiaChi.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.DiaChi], "");
                txtObjectType_Name.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.TenDoituongKcb], "");

                txtDiachiBHYT.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.DiachiBhyt], "");
                txtSoBHYT.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.MatheBhyt], "");
                txtPtram.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.PtramBhyt], "");
                dtpNgayhethanBHYT.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.NgayketthucBhyt], "");
                ResetImages();
                //Fill Detail
                txtIdKham.Text = Utility.sDbnull(dr[VKcbChidinhcl.Columns.IdKham], "");
                txtidchidinhchitiet.Text = Utility.sDbnull(dr[VKcbChidinhcl.Columns.IdChitietchidinh], "");
                txtIdDichvuChitiet.Text = Utility.sDbnull(dr[VKcbChidinhcl.Columns.IdChitietdichvu], "");
                txtTendichvu.Text = Utility.sDbnull(dr[VKcbChidinhcl.Columns.TenChitietdichvu], "");
                txtPhongchidinh.Text = Utility.sDbnull(dr[VKcbChidinhcl.Columns.TenPhongchidinh], "");
                txtBSChidinh.Text = Utility.sDbnull(dr[VKcbChidinhcl.Columns.TenBacsiChidinh], "");
                txtChanDoan.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.ChanDoan], "");
                //txtGhiChu.Text = Utility.sDbnull(dr[VKcbChidinhcl.Columns.MotaThem], "");

                chkXacnhan.Checked = Utility.ByteDbnull(dr[VKcbChidinhcl.Columns.TrangThai], 0) == 4;
                //try2DelImageOnFTPFolder();
                FtpClient.CurrentDirectory = FtpClientCurrentDirectory;
                if (dr != null)
                {
                    CheckImages(dr.Row);
                    Utility.SetMsg(lblMsg, "Đang download ảnh từ FTP...", false);
                    LoadImage(imgBox1, Utility.sDbnull(dr["Local1"], ""),
                        Utility.sDbnull(dr[VKcbChidinhcl.Columns.ImgPath1], ""));
                    LoadImage(imgBox2, Utility.sDbnull(dr["Local2"], ""),
                        Utility.sDbnull(dr[VKcbChidinhcl.Columns.ImgPath2], ""));
                    LoadImage(imgBox3, Utility.sDbnull(dr["Local3"], ""),
                        Utility.sDbnull(dr[VKcbChidinhcl.Columns.ImgPath3], ""));
                    LoadImage(imgBox4, Utility.sDbnull(dr["Local4"], ""),
                        Utility.sDbnull(dr[VKcbChidinhcl.Columns.ImgPath4], ""));
                }
                pic1.Tag = imgBox1.Tag;
                pic2.Tag = imgBox2.Tag;
                pic3.Tag = imgBox3.Tag;
                pic4.Tag = imgBox4.Tag;
                _objKcbChidinhclsChitiet =
                    KcbChidinhclsChitiet.FetchByID(Utility.Int32Dbnull(txtidchidinhchitiet.Text, -1));
                DmucDichvuclsChitiet objDichvuchitiet =
                    DmucDichvuclsChitiet.FetchByID(Utility.Int32Dbnull(txtIdDichvuChitiet.Text, -1));

                txtketluan.Text = _objKcbChidinhclsChitiet.KetQua;
                DataTable dtMauQk = clsHinhanh.HinhanhLaydanhsachMauKQtheoDichvuCLS(objDichvuchitiet.IdChitietdichvu);

                txtMauKQ.Init(dtMauQk,
                    new List<string>
                    {
                        QheDichvuMauketqua.Columns.MaMauKQ,
                        QheDichvuMauketqua.Columns.MaMauKQ,
                        DmucChung.Columns.Ten
                    });
                txtMauKQ__OnEnterMe();
                new KCB_HinhAnh().UpdateXacNhanDaThucHien(_vIdChitietchidinh, 2);
                ModifyButtonAssignDetail_Status();
                FocusMe(flowDynamics);
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:" + exception.Message);
            }
            finally
            {
                Utility.SetMsg(lblMsg, "Mời bạn tiếp tục làm việc...", false);
                Cursor = Cursors.Default;
            }
        }

        private void FillDynamicValues()
        {
            try
            {
                flowDynamics.Controls.Clear();

                DataTable dtData = clsHinhanh.GetDynamicFieldsValues(Utility.Int32Dbnull(txtIdDichvuChitiet.Text),
                    txtMauKQ.MyCode, "", "", -1, Utility.Int32Dbnull(txtidchidinhchitiet.Text));

                foreach (DataRow dr in dtData.Select("1=1", "Stt_hthi"))
                {
                    dr[DynamicValue.Columns.IdChidinhchitiet] = Utility.Int32Dbnull(txtidchidinhchitiet.Text);
                    var ucTextSysparam = new ucDynamicParam(dr, true);

                    ucTextSysparam.TabStop = true;
                    ucTextSysparam._OnEnterKey += _ucTextSysparam__OnEnterKey;
                    ucTextSysparam.TabIndex = 10 +
                                              Utility.Int32Dbnull(dr[DynamicField.Columns.Stt],
                                                  flowDynamics.Controls.Count);

                    ucTextSysparam.Init();
                    if (Utility.Byte2Bool(dr[DynamicField.Columns.Rtxt]))
                    {
                        ucTextSysparam.Size = PropertyLib._DynamicInputProperties.RtfDynamicSize;
                        ucTextSysparam.txtValue.Size = PropertyLib._DynamicInputProperties.RtfTextSize;
                        ucTextSysparam.lblName.Size = PropertyLib._DynamicInputProperties.RtfLabelSize;
                    }
                    else
                    {
                        ucTextSysparam.Size = PropertyLib._DynamicInputProperties.DynamicSize;
                        ucTextSysparam.txtValue.Size = PropertyLib._DynamicInputProperties.TextSize;
                        ucTextSysparam.lblName.Size = PropertyLib._DynamicInputProperties.LabelSize;
                    }
                    flowDynamics.Controls.Add(ucTextSysparam);
                }
            }
            catch (Exception ex)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(ex.ToString());
                }
            }
        }

        private void _ucTextSysparam__OnEnterKey(ucDynamicParam obj)
        {
            try
            {
                if (!obj._AcceptTab)
                {
                    int idx = -1;
                    IEnumerable<int> q = (from p in flowDynamics.Controls.Cast<ucDynamicParam>().AsEnumerable()
                        where p.TabIndex > obj.TabIndex
                        select p.TabIndex);
                    IEnumerable<int> enumerable = q as int[] ?? q.ToArray();
                    if (enumerable.Any())
                        idx = enumerable.Min();
                    if (idx > 0)
                    {
                        foreach (ucDynamicParam ucs in flowDynamics.Controls)
                        {
                            if (ucs.TabIndex == idx)
                            {
                                ucs.FocusMe();
                                return;
                            }
                        }
                    }
                    else //Last Controls
                        cmdSave.Focus();
                }
            }
            catch (Exception ex)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(ex.ToString());
                }
            }
        }

        private void CheckImages(DataRow dr)
        {
            try
            {
                if (Utility.Byte2Bool(dr[KcbChidinhclsChitiet.Columns.FTPImage]))
                {
                    //  var lstImgFiles = new List<string>();

                    THU_VIEN_CHUNG.Laygiatrithamsohethong("PACS_SHAREDFOLDER", "", true);
                    THU_VIEN_CHUNG.Laygiatrithamsohethong("PACS_IMAGEREPLACEPATH", "",
                        true);
                    FtpClient.CurrentDirectory = string.Format("{0}{1}", FtpClientCurrentDirectory,
                        txtidchidinhchitiet.Text);

                    string strfile = Utility.sDbnull(dr[KcbChidinhclsChitiet.Columns.ImgPath1], "");
                    // Utility.DoTrim(strfile).ToUpper().Replace(pacsImagereplacepath.ToUpper(), pacsSharedfolder.ToUpper());
                    string localfile = string.Format(@"{0}\{1}\{2}", _baseDirectory, txtidchidinhchitiet.Text,
                        Utility.sDbnull(dr[KcbChidinhclsChitiet.Columns.ImgPath1], ""));
                    // Utility.DoTrim(_strfile).ToUpper().Replace(PACS_IMAGEREPLACEPATH.ToUpper(), _baseDirectory.ToUpper());

                    if (strfile != "")
                    {
                        //if (!File.Exists(localfile)) lstImgFiles.Add(Utility.sDbnull(dr[KcbChidinhclsChitiet.Columns.ImgPath1], ""));
                        dr["Local1"] = localfile;
                    }
                    strfile = Utility.sDbnull(dr[KcbChidinhclsChitiet.Columns.ImgPath2], "");
                    //  Utility.DoTrim(strfile).ToUpper().Replace(pacsImagereplacepath.ToUpper(), pacsSharedfolder.ToUpper());
                    localfile = string.Format(@"{0}\{1}\{2}", _baseDirectory, txtidchidinhchitiet.Text,
                        Utility.sDbnull(dr[KcbChidinhclsChitiet.Columns.ImgPath2], ""));

                    if (strfile != "")
                    {
                        // if (!File.Exists(localfile)) lstImgFiles.Add(Utility.sDbnull(dr[KcbChidinhclsChitiet.Columns.ImgPath2], ""));
                        dr["Local2"] = localfile;
                    }
                    strfile = Utility.sDbnull(dr[KcbChidinhclsChitiet.Columns.ImgPath3], "");
                    //  Utility.DoTrim(strfile).ToUpper().Replace(pacsImagereplacepath.ToUpper(), pacsSharedfolder.ToUpper());
                    localfile = string.Format(@"{0}\{1}\{2}", _baseDirectory, txtidchidinhchitiet.Text,
                        Utility.sDbnull(dr[KcbChidinhclsChitiet.Columns.ImgPath3], ""));

                    if (strfile != "")
                    {
                        //if (!File.Exists(localfile)) lstImgFiles.Add(Utility.sDbnull(dr[KcbChidinhclsChitiet.Columns.ImgPath3], ""));
                        dr["Local3"] = localfile;
                    }
                    strfile = Utility.sDbnull(dr[KcbChidinhclsChitiet.Columns.ImgPath4], "");
                    //  Utility.DoTrim(strfile).ToUpper().Replace(pacsImagereplacepath.ToUpper(), pacsSharedfolder.ToUpper());
                    localfile = string.Format(@"{0}\{1}\{2}", _baseDirectory, txtidchidinhchitiet.Text,
                        Utility.sDbnull(dr[KcbChidinhclsChitiet.Columns.ImgPath4], ""));

                    if (strfile != "")
                    {
                        //if (!File.Exists(localfile)) lstImgFiles.Add(Utility.sDbnull(dr[KcbChidinhclsChitiet.Columns.ImgPath4], ""));
                        dr["Local4"] = localfile;
                    }
                }
                else
                {
                    dr["Local1"] = dr[KcbChidinhclsChitiet.Columns.ImgPath1];
                    dr["Local2"] = dr[KcbChidinhclsChitiet.Columns.ImgPath2];
                    dr["Local3"] = dr[KcbChidinhclsChitiet.Columns.ImgPath3];
                    dr["Local4"] = dr[KcbChidinhclsChitiet.Columns.ImgPath4];
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }


        /// <summary>
        ///     hàm thực hiện việc thông tin status
        /// </summary>
        private void ModifyButtonAssignDetail_Status()
        {
            try
            {
                if (globalVariables.UserName != "ADMIN")
                {
                    cmdSaveAndAccept.Enabled =
                        Utility.Int32Dbnull(grdList.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.TrangThai].Value, 0) <=
                        1;
                    cmdSave.Enabled =
                        Utility.Int32Dbnull(grdList.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.TrangThai].Value, 0) <=
                        1;
                    chkXacnhan.Checked = Utility.Int32Dbnull(grdList.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.TrangThai].Value, 0) >= 2;
                }
            }
            catch (Exception ex)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(ex.ToString());
                }
            }
        }

        /// <summary>
        ///     hàm thực hiện làm sách đường dẫn
        /// </summary>
        private void ResetImages()
        {
            imgBox1._img.Source = null;
            imgBox1.Tag = "";

            imgBox2._img.Source = null;
            imgBox2.Tag = "";

            imgBox3._img.Source = null;
            imgBox3.Tag = "";

            imgBox4._img.Source = null;
            imgBox4.Tag = "";
        }

        private void toolAccept_Click(object sender, EventArgs e)
        {
            try
            {
                _vIdChitietchidinh =
                    Utility.Int32Dbnull(grdList.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value,
                        -1);

                ActionResult actionResult =
                    new KCB_HinhAnh().UpdateXacNhanDaThucHien(
                        _vIdChitietchidinh, 4);
                switch (actionResult)
                {
                    case ActionResult.Success:
                        grdList.CurrentRow.BeginEdit();
                        grdList.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.TrangThai].Value = 4;
                        grdList.CurrentRow.Cells["ten_trangthai"].Value = GetAsssignDetailStatus(4);
                        grdList.CurrentRow.EndEdit();
                        grdList.UpdateData();
                        grdList.Refresh();
                        _mDKcbChidinhclsChitiet.AcceptChanges();
                        break;
                    case ActionResult.Error:
                        Utility.ShowMsg("Có lỗi trong quá trình xác nhận", "Thông báo", MessageBoxIcon.Error);
                        break;
                }


                ModifyButtonAssignDetail_Status();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        /// <summary>
        ///     hàm thực hiện việc in phiếu kết quả
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolPrintRadio_Click(object sender, EventArgs e)
        {
            //KcbKetquaHa objKetQuaha = new Select().From(KcbKetquaHa.Schema).ExecuteSingle<KcbKetquaHa>();
            ////  string fileName = Path.GetTempFileName() + ".doc";
            //String connStr = "Data Source=.; Initial Catalog=VMS_2408; User ID=sa;Password=123456;";
            //SqlConnection cn = new SqlConnection(connStr);
            //SqlDataAdapter adap = new SqlDataAdapter("select * from kcb_ketqua_ha", cn);
            //var ds = new DataSet();
            //adap.Fill(ds);
            Int64 idchidinhchitiet =
                Utility.Int64Dbnull(grdList.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value, -1);
            DataSet ds = new Select().From(KcbKetquaHa.Schema).Where(KcbKetquaHa.Columns.IdChiTietChiDinh).IsEqualTo(idchidinhchitiet).ExecuteDataSet();
            if (ds.Tables[0].Rows.Count <= 0)
            {
                Utility.ShowMsg("Không có file kết quả để in");
            }
            else
            {
                byte[] fileData = (byte[])ds.Tables[0].Rows[0]["FileData"];
                string fileName = ds.Tables[0].Rows[0]["NameFile"].ToString();
                using (var fs = new FileStream(fileName, FileMode.Create))
                {
                    fs.Write(fileData, 0, fileData.Length);
                    fs.Close();
                }
                Process prc = new Process();
                prc.StartInfo.FileName = fileName;
                prc.Start();
                
            }
           
            //v_id_chitietchidinh = Utility.Int32Dbnull(grdList.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value, -1);
            //switch (BusinessHelper.GetAccountName())
            //{
            //    case "YHOCHAIQUAN":
            //        YHHQ_PrintDataRadio(v_id_chitietchidinh);
            //        break;
            //    case "KYDONG":
            //        KYDONG_PrintDataRadio(v_id_chitietchidinh);
            //        break;
            //}
        }

        /// <summary>
        ///     hàm thực hiện viêc hủy bỏ kết quả
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolUnAccept_Click(object sender, EventArgs e)
        {
            if (globalVariables.UserName == "ADMIN" ||
                Utility.sDbnull(grdList.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.NguoiThuchien].Value, "") ==
                globalVariables.UserName)
            {
                ActionResult actionResult =
                    new KCB_HinhAnh().UpdateXacNhanDaThucHien(
                        _vIdChitietchidinh, 3); //Trạng thái đang nhập kết quả

                switch (actionResult)
                {
                    case ActionResult.Success:
                        grdList.CurrentRow.BeginEdit();
                        grdList.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.TrangThai].Value = 3;
                        grdList.CurrentRow.Cells["ten_trangthai"].Value = GetAsssignDetailStatus(3);
                        grdList.CurrentRow.EndEdit();
                        grdList.UpdateData();
                        grdList.Refresh();
                        _mDKcbChidinhclsChitiet.AcceptChanges();
                        break;
                    case ActionResult.Error:
                        Utility.ShowMsg("Có lỗi trong quá trình xác nhận", "Thông báo", MessageBoxIcon.Error);
                        break;
                }
            }
            else
            {
                Utility.ShowMsg(
                    "Kết quả này được xác nhận bởi bác sĩ khác nên bạn không được phép hủy hoặc thay đổi. Muốn thay đổi bạn cần đăng nhập là Admin hoặc liên hệ bác sĩ xác nhận kết quả này");
            }
        }

        /// <summary>
        ///     nhan chuột phải thực hiện việc xử lý thông tin của phần chuẩn đoán đưa bệnh nhân vào chẩn đoán
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolChooseBN_Click(object sender, EventArgs e)
        {
            // if (!InValiRadio()) return;
            BeginExam();
        }

        #endregion

        #region "Khu vực xử lý thông tin ảnh"

        private readonly string _path = Application.StartupPath;
        private ActionResult _actionResult = ActionResult.Error;

        private string _baseDirectory = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "Radio\\");

        private string _localpath1 = "";
        private string _localpath2 = "";
        private string _localpath3 = "";
        private string _localpath4 = "";

        // private Logger log;
        private string UploadFile(string sourcePath, string sFileName)
        {
            try
            {
                if (Utility.DoTrim(sourcePath) == "" || !File.Exists(sourcePath)) return "";
                string newDirName = txtidchidinhchitiet.Text;
                string ftpCurrentDirectory = FtpClientCurrentDirectory + newDirName;
                if (!FtpClient.FtpDirectoryExists(ftpCurrentDirectory))
                    FtpClient.FtpCreateDirectory(ftpCurrentDirectory);
                string fileName = Guid.NewGuid() + Path.GetExtension(sFileName);
                string uploadDirectory = string.Format("{0}/{1}", ftpCurrentDirectory, fileName);
                FtpClient.CurrentDirectory = FtpClientCurrentDirectory;
                FtpClient.Upload(sourcePath, uploadDirectory);
                return fileName;
            }
            catch
            {
                return "";
            }
        }

        private void Try2DelImageOnFtpFolder()
        {
            if (!Directory.Exists(_baseDirectory)) return;
            foreach (string file in Directory.GetFiles(_baseDirectory))
            {
                try
                {
                    File.Delete(file);
                }
                catch (Exception ex)
                {
                    Utility.CatchException(ex);
                }
            }
        }
        private KcbChidinhclsChitiet CreateAssignDetail()
        {
            try
            {
                Utility.SetMsg(lblMsg, "", false);

                if (_objKcbChidinhclsChitiet == null)
                {
                    Utility.ShowMsg(
                        "Chỉ định cận lâm sàng này đã được người khác xóa hoặc sửa đổi. Đề nghị bạn nhấn lại nút tìm kiếm để kiểm tra lại");
                    return null;
                }
                _objKcbChidinhclsChitiet.FTPImage = Utility.Bool2byte(PropertyLib._FTPProperties.Push2FTP);
                if (chkSaveImg.Checked)
                {
                    if (PropertyLib._FTPProperties.Push2FTP)
                    {
                        Utility.SetMsg(lblMsg, "Đang xóa ảnh khỏi FTP...", false);
                        Try2DelImageOnFtp(_objKcbChidinhclsChitiet.ImgPath1);
                        Try2DelImageOnFtp(_objKcbChidinhclsChitiet.ImgPath2);
                        Try2DelImageOnFtp(_objKcbChidinhclsChitiet.ImgPath3);
                        Try2DelImageOnFtp(_objKcbChidinhclsChitiet.ImgPath4);

                        string local1 = _objKcbChidinhclsChitiet.ImgPath1;
                        string local2 = _objKcbChidinhclsChitiet.ImgPath2;
                        string local3 = _objKcbChidinhclsChitiet.ImgPath3;
                        string local4 = _objKcbChidinhclsChitiet.ImgPath4;

                        Utility.SetMsg(lblMsg, "Đang cập nhật lại ảnh trên FTP...", false);
                        _objKcbChidinhclsChitiet.ImgPath1 = UploadFile(Utility.sDbnull(imgBox1.Tag, ""),
                            Utility.sDbnull(imgBox1.Tag, ""));
                        _objKcbChidinhclsChitiet.ImgPath2 = UploadFile(Utility.sDbnull(imgBox2.Tag, ""),
                            Utility.sDbnull(imgBox2.Tag, ""));
                        _objKcbChidinhclsChitiet.ImgPath3 = UploadFile(Utility.sDbnull(imgBox3.Tag, ""),
                            Utility.sDbnull(imgBox3.Tag, ""));
                        _objKcbChidinhclsChitiet.ImgPath4 = UploadFile(Utility.sDbnull(imgBox4.Tag, ""),
                            Utility.sDbnull(imgBox4.Tag, ""));

                        Utility.SetMsg(lblMsg, "Đang cập nhật ảnh trên Local...", false);
                        Try2CopyImages2Local(Utility.sDbnull(imgBox1.Tag, ""), _objKcbChidinhclsChitiet.ImgPath1,
                            ref _localpath1);
                        Try2CopyImages2Local(Utility.sDbnull(imgBox2.Tag, ""), _objKcbChidinhclsChitiet.ImgPath2,
                            ref _localpath2);
                        Try2CopyImages2Local(Utility.sDbnull(imgBox3.Tag, ""), _objKcbChidinhclsChitiet.ImgPath3,
                            ref _localpath3);
                        Try2CopyImages2Local(Utility.sDbnull(imgBox4.Tag, ""), _objKcbChidinhclsChitiet.ImgPath4,
                            ref _localpath4);

                        imgBox1.Tag = _localpath1;
                        imgBox2.Tag = _localpath2;
                        imgBox3.Tag = _localpath3;
                        imgBox4.Tag = _localpath4;
                        Utility.SetMsg(lblMsg, "Đang xóa ảnh khỏi Local...", false);
                        Try2DelImageOnLocal(local1);
                        Try2DelImageOnLocal(local2);
                        Try2DelImageOnLocal(local3);
                        Try2DelImageOnLocal(local4);
                    }
                    else
                    {
                        _localpath1 = Utility.sDbnull(imgBox1.Tag, "");
                        _localpath2 = Utility.sDbnull(imgBox2.Tag, "");
                        _localpath3 = Utility.sDbnull(imgBox3.Tag, "");
                        _localpath4 = Utility.sDbnull(imgBox4.Tag, "");

                        _objKcbChidinhclsChitiet.ImgPath1 = Utility.sDbnull(imgBox1.Tag, "");
                        _objKcbChidinhclsChitiet.ImgPath2 = Utility.sDbnull(imgBox2.Tag, "");
                        _objKcbChidinhclsChitiet.ImgPath3 = Utility.sDbnull(imgBox3.Tag, "");
                        _objKcbChidinhclsChitiet.ImgPath4 = Utility.sDbnull(imgBox4.Tag, "");
                    }
                }

                pic1.Tag = imgBox1.Tag;
                pic2.Tag = imgBox2.Tag;
                pic3.Tag = imgBox3.Tag;
                pic4.Tag = imgBox4.Tag;
                _objKcbChidinhclsChitiet.IdKhoaThuchien = globalVariables.IdKhoaNhanvien;
                _objKcbChidinhclsChitiet.IdPhongThuchien = globalVariables.gv_intPhongNhanvien;
                _objKcbChidinhclsChitiet.KetQua = txtketluan.Text.Trim();
                _objKcbChidinhclsChitiet.TrangThai = (byte?) (chkXacnhan.Checked ? 4 : 3);
                _objKcbChidinhclsChitiet.NguoiThuchien = globalVariables.UserName;
                _objKcbChidinhclsChitiet.NgayThuchien = globalVariables.SysDate;
                return _objKcbChidinhclsChitiet;
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi lưu kết quả hình ảnh", ex);
                return null;
            }
        }

        private bool IsValidData()
        {
            Utility.SetMsg(lblMsg, "", true);

            if (_objKcbChidinhclsChitiet == null)
            {
                Utility.SetMsg(lblMsg, "Bạn cần chọn dịch vụ cần nhập kết quả trước khi Lưu", true);
                TabInfo.SelectedTab = tabDanhsach;
                return false;
            }
            if (Utility.DoTrim(Utility.sDbnull(_objKcbChidinhclsChitiet.NguoiThuchien, "")) != "" &&
                _objKcbChidinhclsChitiet.NguoiThuchien != globalVariables.UserName)
            {
                UpdateSaveMode(false);
                Utility.SetMsg(lblMsg,
                    string.Format(
                        "Dịch vụ này đã được sửa bởi Người dùng {0} nên bạn chỉ có thể xem và không được phép chỉnh sửa",
                        _objKcbChidinhclsChitiet.NguoiThuchien), true);
                return false;
            }
            if (!HasValue(flowDynamics))
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập ít nhất một kết quả trước khi Lưu", true);
                FocusMe(flowDynamics);
                return false;
            }
            return true;
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (!IsValidData()) return;
            if (SaveResult())
                if (chkInsaukhiluu.Checked)
                    cmdPrintRadio_Click(cmdPrintRadio, e);
        }

        private bool SaveResult()
        {
            try
            {
                _localpath1 = "";
                _localpath2 = "";
                _localpath3 = "";
                _localpath4 = "";
                KcbChidinhclsChitiet objAssignDetail = CreateAssignDetail();
                if (objAssignDetail == null) return false;
                SaveNow(flowDynamics);
                _actionResult = new KCB_HinhAnh().PerformActionUpdate(objAssignDetail);
                if (_actionResult == ActionResult.Success)
                    UpdateDataTable(objAssignDetail);
                else
                    return false;
                return true;
                //SetStatusMessage();
            }
            catch
            {
                return false;
            }
            finally
            {
                Utility.SetMsg(lblMsg, "Mời bạn tiếp tục làm việc...", false);
                chkSaveImg.Checked = false;
            }
        }

        private void FocusMe(FlowLayoutPanel pnlParent)
        {
            try
            {
                if (pnlParent.Controls.Count > 0)
                {
                    ((ucDynamicParam) pnlParent.Controls[0]).FocusMe();
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void UpdateSaveMode(bool allowSave)
        {
            foreach (ucDynamicParam ctrl in flowDynamics.Controls)
            {
                ctrl.AllowSave = allowSave;
            }
        }

        private bool HasValue(FlowLayoutPanel pnlParent)
        {
            if (pnlParent.Controls.Count <= 0) return true;
            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("HINHANH_BATNHAPKETQUA", "0", true) == "1")
            {
                return pnlParent.Controls.Cast<ucDynamicParam>().Any(ctrl => Utility.DoTrim(ctrl._Giatri) != "");
            }
            return true;
        }

        private void SaveNow(FlowLayoutPanel pnlParent)
        {
            foreach (ucDynamicParam ctrl in pnlParent.Controls)
            {
                if (!ctrl.isSaved)
                    ctrl.Save();
            }
        }

        private void UpdateDataTable(KcbChidinhclsChitiet objAssignDetail)
        {
            try
            {
                if (!Utility.isValidGrid(grdList)) return;

                DataRow dr =
                    _mDKcbChidinhclsChitiet.Select(VKcbChidinhcl.Columns.IdChitietchidinh + "=" +
                                                   txtidchidinhchitiet.Text)[0];
                dr[VKcbChidinhcl.Columns.ImgPath1] = objAssignDetail.ImgPath1;
                dr[VKcbChidinhcl.Columns.ImgPath2] = objAssignDetail.ImgPath2;
                dr[VKcbChidinhcl.Columns.ImgPath3] = objAssignDetail.ImgPath3;
                dr[VKcbChidinhcl.Columns.ImgPath4] = objAssignDetail.ImgPath4;
                if (PropertyLib._FTPProperties.Push2FTP)
                {
                    dr["Local1"] = _localpath1;
                    dr["Local2"] = _localpath2;
                    dr["Local3"] = _localpath3;
                    dr["Local4"] = _localpath4;
                }
                else
                {
                    dr["Local1"] = objAssignDetail.ImgPath1;
                    dr["Local2"] = objAssignDetail.ImgPath2;
                    dr["Local3"] = objAssignDetail.ImgPath3;
                    dr["Local4"] = objAssignDetail.ImgPath4;
                }

                dr[KcbChidinhclsChitiet.Columns.KetQua] = txtketluan.Text.Trim();
                dr[KcbChidinhclsChitiet.Columns.TrangThai] = chkXacnhan.Checked ? 4 : 3;
                dr["ten_trangthai"] = GetAsssignDetailStatus(chkXacnhan.Checked ? 4 : 3);
                _mDKcbChidinhclsChitiet.AcceptChanges();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void InitData()
        {
            try
            {
                //DataTable data = THU_VIEN_CHUNG.LaydanhsachBacsi();
                //VNS.Libs.DataBinding.BindDataCombobox(this.cboDoctorAssign, data, DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.TenNhanvien, "---Bác sỹ chỉ định---",true);
                //if (globalVariables.gv_intIDNhanvien <= 0)
                //{
                //    if (this.cboDoctorAssign.Items.Count > 0)
                //    {
                //        this.cboDoctorAssign.SelectedIndex = 0;
                //    }
                //}
                //else
                //{
                //    this.cboDoctorAssign.SelectedIndex = Utility.GetSelectedIndex(this.cboDoctorAssign, globalVariables.gv_intIDNhanvien.ToString());
                //}
                DataBinding.BindDataCombobox(cboObjectType, THU_VIEN_CHUNG.LaydanhsachDoituongKcb(),
                    DmucDoituongkcb.Columns.IdDoituongKcb, DmucDoituongkcb.Columns.TenDoituongKcb, "---Đối tượng KCB---",
                    true);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private string GetAsssignDetailStatus(int assginDetailStatus)
        {
            string assginDetailStatusName = "Chưa thực hiện";
            switch (assginDetailStatus)
            {
                case 0:
                    assginDetailStatusName = "Chưa thực hiện";
                    break;
                case 1:
                    assginDetailStatusName = "Đã chuyển CLS";
                    break;
                case 2:
                    assginDetailStatusName = "Đang thực hiện";
                    break;
                case 3:
                    assginDetailStatusName = "Đã có kết quả";
                    break;
                case 4:
                    assginDetailStatusName = "Đã xác nhận";
                    break;
            }
            return assginDetailStatusName;
        }
        
        private void LoadImage(ImgBox pImage, string mylocalImage, string imgPath)
        {
            try
            {
                if (File.Exists(mylocalImage) && !_mBlnForced2GetImagesFromFtp)
                {
                    pImage.fullName = mylocalImage;
                    pImage.LoadIMg();
                    pImage.Tag = mylocalImage;
                    return;
                }
                if (!string.IsNullOrEmpty(imgPath))
                {
                    if (!PropertyLib._FTPProperties.IamLocal || _mBlnForced2GetImagesFromFtp)
                    {
                        FtpClient.CurrentDirectory = string.Format("{0}{1}", FtpClientCurrentDirectory,
                            txtidchidinhchitiet.Text);
                        if (FtpClient.FtpFileExists(FtpClient.CurrentDirectory + imgPath))
                        {
                            string sPath1 = string.Format(@"{0}\{1}\{2}", _baseDirectory,
                                txtidchidinhchitiet.Text, imgPath);
                            Utility.CreateFolder(sPath1);
                            FtpClient.Download(imgPath, sPath1, true);
                            pImage.fullName = sPath1;
                            pImage.LoadIMg();
                            pImage.Tag = sPath1;
                            _log.Trace("load anh thu 1");
                        }
                        else
                        {
                            pImage.fullName = _path + @"\Path\noimage.jpg";
                            pImage.LoadIMg();
                            pImage.Tag = "";
                        }
                    }
                    else //Ảnh trên chính máy tính này
                    {
                        pImage.fullName = imgPath;
                        pImage.LoadIMg();
                        pImage.Tag = imgPath;
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                if (pImage._img.Source == null)
                {
                    pImage.fullName = _path + @"\Path\noimage.jpg";
                    pImage.LoadIMg();
                    pImage.Tag = "";
                }
            }
        }

        //private void cmdPrint_Click(object sender, EventArgs e)
        //{
        //    InPhieuHinhAnh(Utility.Int32Dbnull(txtidchidinhchitiet.Text));
        //}

        private void mnuDeleteImage_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    imgBoxtureBox imgBox = ((ContextMenuStrip)((ToolStripMenuItem)sender).GetCurrentParent()).SourceControl as imgBoxtureBox;

            //    imgBox.Image = null;
            //    imgBox.Tag = "";


            //}
            //catch
            //{
            //}
        }

        private void Try2DelImageOnFtp(string imgPath)
        {
            try
            {
                if (!string.IsNullOrEmpty(imgPath))
                {
                    FtpClient.CurrentDirectory = string.Format("{0}{1}", FtpClientCurrentDirectory,
                        txtidchidinhchitiet.Text);
                    if (FtpClient.FtpFileExists(FtpClient.CurrentDirectory + imgPath))
                    {
                        FtpClient.FtpDelete(imgPath);
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void Try2DelImageOnLocal(string imgPath)
        {
            try
            {
                if (!string.IsNullOrEmpty(imgPath))
                {
                    string sPath1 = string.Format(@"{0}\{1}\{2}", _baseDirectory,
                        txtidchidinhchitiet.Text, imgPath);
                    Utility.CreateFolder(sPath1);
                    if (File.Exists(sPath1))
                    {
                        File.Delete(sPath1);
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void Try2CopyImages2Local(string source, string des, ref string localpath)
        {
            try
            {
                if (!string.IsNullOrEmpty(source) && !string.IsNullOrEmpty(des))
                {
                    string sPath1 = string.Format(@"{0}\{1}\{2}", _baseDirectory,
                        txtidchidinhchitiet.Text, des);
                    localpath = sPath1;
                    Utility.CreateFolder(sPath1);
                    if (File.Exists(source))
                    {
                        File.Copy(source, sPath1);
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void mnuBrowseImage_Click(object sender, EventArgs e)
        {
            try
            {
                ImgBox imgBox = null;// =(ImgBox) ((ContextMenuStrip)((ToolStripMenuItem)sender).GetCurrentParent()).SourceControl ;
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Multiselect = false;
                if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    imgBox.fullName = openFileDialog.FileName;
                    imgBox.LoadIMg();
                    imgBox.Tag = openFileDialog.FileName; //Guid.NewGuid() + "." + Path.GetExtension(_OpenFileDialog.FileName) + "|" + _OpenFileDialog.FileName;
                }
            }
            catch
            {
            }
        }


        private Image GetThumbnail(Image imgToResize, Size size)
        {
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercentW = (size.Width/(float) sourceWidth);
            float nPercentH = (size.Height/(float) sourceHeight);

            float nPercent = nPercentH < nPercentW ? nPercentH : nPercentW;

            var destWidth = (int) (sourceWidth*nPercent);
            var destHeight = (int) (sourceHeight*nPercent);

            var b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage(b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return b;
        }


        /// <summary>
        ///     hàm thực hiện trạng thái của các nút
        /// </summary>
        private void ModifyCommand()
        {
            try
            {
                cmdSave.Enabled = !string.IsNullOrEmpty(txtMaluotkham.Text);
                cmdSaveAndAccept.Enabled = !string.IsNullOrEmpty(txtMaluotkham.Text);
                cmdPrintRadio.Enabled = !string.IsNullOrEmpty(txtMaluotkham.Text);
                cmdSaveBookMark.Enabled = !string.IsNullOrEmpty(txtMaluotkham.Text);
            }
            catch (Exception exception)
            {
                _log.Error("loi trang thai cua cac nut :" + exception);
            }
        }

        #endregion

       //// private void InPhieuHinhAnh(int idChitietchidinh)
       // {
       //     //m_dtReportHinhanh =
       //     //    SPs.YhhqPhieuHinhAnh(id_chitietchidinh).GetDataSet().Tables[0];
       //     //if (m_dtReportHinhanh.Rows.Count <= 0)
       //     //{
       //     //    Utility.ShowMsg("Không tìm thấy thông tin của bản ghi", "Thông báo");
       //     //    return;
       //     //}
       //     //Utility.UpdateLogotoDatatable(ref m_dtReportHinhanh);
       //     //ProcessData_SIEUAM(ref m_dtReportHinhanh);
       //     //BC_HinhAnh.HA_PhieuKetQua(mabaocao, m_dtReportHinhanh,
       //     //    txtTieuDe.Text, globalVariables.SysDate);
       // }

        private void cmdSaveAndAccept_Click(object sender, EventArgs e)
        {
            if (!IsValidData()) return;
            chkXacnhan.Checked = true;
            if (SaveResult())
            {
                //if (chkInsaukhiluu.Checked)
                //    cmdPrintRadio_Click(cmdPrintRadio, e);
            }
        }
        private void frm_quanlycacphongchucnang_FormClosing(object sender, FormClosingEventArgs e)
        {
            // SaveConfigCls();
        }
        private void radChuaXacNhan_CheckedChanged(object sender, EventArgs e)
        {
            if (radChuaXacNhan.Checked) SearchData();
        }

        private void radChoXacNhan_CheckedChanged(object sender, EventArgs e)
        {
            if (radChoXacNhan.Checked) SearchData();
        }

        private void radDaXacNhan_CheckedChanged(object sender, EventArgs e)
        {
            if (radDaXacNhan.Checked) SearchData();
        }

        private void grdList_DoubleClick(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdList)) return;
            BeginExam();
        }

        private void txtPatient_Code_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string patientCode = Utility.AutoFullPatientCode(txtMaluotkham.Text);
                txtMaluotkham.Text = patientCode;
                cmdSearch.PerformClick();
            }
        }

        private void cmdPrintRadio_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.SetMsg(lblMsg, "", false);
                if (cboLaserPrinters.Items.Count <= 0 || cboLaserPrinters.SelectedIndex < 0)
                {
                    Utility.SetMsg(lblMsg, "Bạn cần chọn máy in trước khi in", true);
                    cboLaserPrinters.Focus();
                    return;
                }
                DataRow[] arrDr =
                    _mDKcbChidinhclsChitiet.Select("id_chitietchidinh=" +
                                                   Utility.Int32Dbnull(txtidchidinhchitiet.Text, -1));

                _dtData = _mDKcbChidinhclsChitiet.Clone();
                if (arrDr.Length > 0)
                    _dtData = arrDr.CopyToDataTable();
                if (!_dtData.Columns.Contains("img1")) _dtData.Columns.Add("img1", typeof (byte[]));
                if (!_dtData.Columns.Contains("img2")) _dtData.Columns.Add("img2", typeof (byte[]));
                if (!_dtData.Columns.Contains("img3")) _dtData.Columns.Add("img3", typeof (byte[]));
                if (!_dtData.Columns.Contains("img4")) _dtData.Columns.Add("img4", typeof (byte[]));

                _dtData.Rows[0]["img1"] = Utility.bytGetImage(Utility.sDbnull(_dtData.Rows[0]["Local1"], ""));
                _dtData.Rows[0]["img2"] = Utility.bytGetImage(Utility.sDbnull(_dtData.Rows[0]["Local2"], ""));
                _dtData.Rows[0]["img3"] = Utility.bytGetImage(Utility.sDbnull(_dtData.Rows[0]["Local3"], ""));
                _dtData.Rows[0]["img4"] = Utility.bytGetImage(Utility.sDbnull(_dtData.Rows[0]["Local4"], ""));
                SwapImage(_dtData.Rows[0]);

                int coHa = _dtData.Rows[0]["img1"] != DBNull.Value ? 1 : 0;
                if (coHa == 0) coHa = _dtData.Rows[0]["img2"] != DBNull.Value ? 1 : 0;
                if (coHa == 0) coHa = _dtData.Rows[0]["img3"] != DBNull.Value ? 1 : 0;
                if (coHa == 0) coHa = _dtData.Rows[0]["img4"] != DBNull.Value ? 1 : 0;
                // InphieuHA(dtData, dtpPrintDate.Value, coHA);
                PrintMau(_dtData.Rows[0]);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void PrintMau(DataRow drData)
        {
            try
            {
                if (Utility.DoTrim(PropertyLib._FTPProperties.TenmayInPhieutraKQ).ToUpper() !=
                    Utility.DoTrim(cboLaserPrinters.Text).ToUpper())
                {
                    PropertyLib._FTPProperties.TenmayInPhieutraKQ = Utility.DoTrim(cboLaserPrinters.Text);
                    PropertyLib.SaveProperty(PropertyLib._FTPProperties);
                }
                var fieldNames = new List<string>
                {
                    "TEN_SO_YTE",
                    "TEN_BENHVIEN",
                    "DIACHI_BENHVIEN",
                    "DIENTHOAI_BENHVIEN",
                    "MA_LUOTKHAM",
                    "ID_BENHNHAN",
                    "TEN_BENHNHAN",
                    "DIA_CHI",
                    "DOITUONG_KCB",
                    "NOI_CHIDINH",
                    "BACSY_CHIDINH",
                    "BACSY_THUCHIEN",
                    "CHAN_DOAN",
                    "ID_PHIEU",
                    "ten_chitietdichvu",
                    "NAM_SINH",
                    "TUOI",
                    "GIOI_TINH",
                    "MATHE_BHYT",
                    "Ket_qua",
                    "KET_LUAN",
                    "DE_NGHI",
                    "NGAYTHANGNAM",
                    "imgPath1",
                    "imgPath2",
                    "imgPath3",
                    "imgPath4"
                };
                var values = new List<string>
                {
                    globalVariables.ParentBranch_Name,
                    globalVariables.Branch_Name,
                    globalVariables.Branch_Address,
                    globalVariables.Branch_Phone,
                    Utility.sDbnull(drData["MA_LUOTKHAM"], ""),
                    Utility.sDbnull(drData["ID_BENHNHAN"], ""),
                    Utility.sDbnull(drData["TEN_BENHNHAN"], ""),
                    Utility.sDbnull(drData["dia_chi"], ""),
                    Utility.sDbnull(drData["ten_doituong_kcb"], ""),
                    Utility.sDbnull(drData["ten_phongchidinh"], ""),
                    Utility.sDbnull(drData["BACSY_CHIDINH"]),
                    globalVariables.gv_strTenNhanvien,
                    Utility.sDbnull(drData["CHAN_DOAN"], ""),
                    Utility.sDbnull(drData["id_chidinh"], ""),
                    Utility.sDbnull(drData["ten_chitietdichvu"], ""),
                    Utility.sDbnull(drData["nam_sinh"], ""),
                    Utility.sDbnull(drData["Tuoi"], ""),
                    Utility.sDbnull(drData["gioi_tinh"], ""),
                    Utility.sDbnull(drData["mathe_bhyt"], ""),
                    "",
                    "",
                    "",
                    Utility.FormatDateTime(globalVariables.SysDate),
                    Utility.sDbnull(drData["Local1"], ""),
                    Utility.sDbnull(drData["Local2"], ""),
                    Utility.sDbnull(drData["Local3"], ""),
                    Utility.sDbnull(drData["Local4"], "")
                };

                DmucDichvuclsChitiet objDichvuchitiet =
                    DmucDichvuclsChitiet.FetchByID(Utility.Int32Dbnull(txtIdDichvuChitiet.Text, -1));
                if (objDichvuchitiet == null)
                {
                    Utility.ShowMsg("Không lấy được thông tin dịch vụ CĐHA từ chi tiết chỉ định");
                    return;
                }
                DataTable dtDynamicValues = clsHinhanh.GetDynamicFieldsValues(objDichvuchitiet.IdChitietdichvu,
                    txtMauKQ.MyCode, objDichvuchitiet.Bodypart, objDichvuchitiet.ViewPosition, -1,
                    Utility.Int32Dbnull(txtidchidinhchitiet.Text, -1));
                foreach (DataRow dr in dtDynamicValues.Rows)
                {
                    string sCode = Utility.sDbnull(dr[DynamicValue.Columns.Ma], "");
                    //string SName = Utility.sDbnull(dr[DynamicValue.Columns.mota], "");
                    string sValue = Utility.sDbnull(dr[DynamicValue.Columns.Giatri], "");
                    fieldNames.Add(sCode);
                    values.Add(string.Format("{0}", sValue));
                    //Values.Add(string.Format("{0}{1}", SName, SValue));
                }
                string maubaocao = Application.StartupPath + @"\MauCDHA\" + DocChuan;
                string fileketqua = "";
                if (!File.Exists(maubaocao))
                {
                    Utility.ShowMsg("Chưa có mẫu báo cáo cho dịch vụ đang chọn. Bạn cần tạo mẫu và nhấn nút chọn mẫu để cập nhật cho dịch vụ đang thực hiện");
                    cmdBrowseMauChuan.Focus();
                    return;
                }
                string fileKetqua = string.Format("{0}{1}{2}{3}{4}", Path.GetDirectoryName(maubaocao),
                    Path.DirectorySeparatorChar, Path.GetFileNameWithoutExtension(maubaocao), "_ketqua",
                    Path.GetExtension(maubaocao));
                string filenameKetqua = string.Format("{0}_{1}_{2}_{3}_{4}_{5}", globalVariables.SysDate.ToString("YYYYMMDD"),
                    Utility.Bodau(txtTenBN.Text.Trim()).Replace(" ", ""), Utility.sDbnull(txtMaluotkham.Text),
                    Utility.sDbnull(_objKcbChidinhclsChitiet.IdChitietchidinh),
                    Utility.sDbnull(_objKcbChidinhclsChitiet.IdChitietdichvu), Utility.sDbnull(txtMauKQ.Text.Trim()));
                string fileurlKetqua = PropertyLib._FTPProperties.UNCFileKetQua + "\\MauCDHA\\" + filenameKetqua;
                if (File.Exists(maubaocao))
                {
                    _doc = new Document(maubaocao);
                    if (_doc == null)
                    {
                        Utility.ShowMsg("Không nạp được file word.");
                        return;
                    }
                    _doc.MailMerge.MergeImageField += MailMerge_MergeImageField;
                    _doc.MailMerge.MergeField += MailMerge_MergeField;
                    _doc.MailMerge.Execute(fieldNames.ToArray(), values.ToArray());

                    if (File.Exists(fileKetqua))
                    {
                        File.Delete(fileKetqua);
                    }
                    _doc.Save(fileKetqua, SaveFormat.Doc);

                    #region Ghi file kết quả word vào CSDL

                    string filePath = fileKetqua;
                    string filename = Path.GetFileName(filePath);

                    var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                    var br = new BinaryReader(fs);
                    Byte[] bytes = br.ReadBytes((Int32)fs.Length);
                    br.Close();
                    fs.Close();
                    SqlQuery sqlQuery = new Select().From(KcbKetquaHa.Schema).Where(KcbKetquaHa.Columns.IdChiTietChiDinh).IsEqualTo(_objKcbChidinhclsChitiet.IdChitietchidinh);
                   
                    var objKetquaHa = new KcbKetquaHa();
                    if (sqlQuery.GetRecordCount() > 0)
                    {
                        objKetquaHa.IsNew = false;
                        objKetquaHa.MarkOld();
                    }
                    else
                    {
                        objKetquaHa.IsNew = true;
                    }
                    objKetquaHa.NameFile = filename;
                    objKetquaHa.FileData = bytes;
                    objKetquaHa.IdChiTietChiDinh = _objKcbChidinhclsChitiet.IdChitietchidinh;
                    objKetquaHa.MaLanKham = _objKcbChidinhclsChitiet.MaLuotkham;
                    objKetquaHa.UploadDate = globalVariables.SysDate;
                    objKetquaHa.DisplayName = Utility.sDbnull(txtTendichvu.Text);
                    objKetquaHa.Save();
                    #endregion 
                    
                    string path = fileKetqua;
                    if (chkPreview.Checked)
                    {
                        if (File.Exists(path))
                        {
                            var process = new Process { StartInfo = { FileName = path } };
                            process.Start();
                            process.WaitForInputIdle();
                        }
                    }
                    else
                    {
                        string oldDefaultPrinter = LocalPrintServer.GetDefaultPrintQueue().FullName;
                        Try2SetDefaultPrinter4Computer(cboLaserPrinters.Text);

                        var info = new ProcessStartInfo(path);
                        info.Verb = "Print";
                        info.CreateNoWindow = false;
                        info.WindowStyle = ProcessWindowStyle.Hidden;
                        Process.Start(info);
                        Try2SetDefaultPrinter4Computer(oldDefaultPrinter);
                        //PrinterSettings printerSettings = new PrinterSettings();
                        //printerSettings.PrinterName = cboLaserPrinters.Text;
                        //printerSettings.DefaultPageSettings.Margins.Top = 0;
                        //printerSettings.Copies = 1;
                        //doc.Print(cboLaserPrinters.Text);
                    }
                }
                else
                {
                    MessageBox.Show(@"Không tìm thấy biểu mẫu", @"TThông báo", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }

        private void Try2SetDefaultPrinter4Computer(string printerName)
        {
            SetDefaultPrinter(printerName);
        }

        [DllImport("winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern bool SetDefaultPrinter(string name);

        private void LoadLaserPrinters()
        {
            try
            {
                //khoi tao may in
                cboLaserPrinters.Items.Clear();
                for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
                {
                    String pkInstalledPrinters = PrinterSettings.InstalledPrinters[i];
                    cboLaserPrinters.Items.Add(pkInstalledPrinters);
                }
                if (cboLaserPrinters.Items.Count <= 0)
                    Utility.ShowMsg("no printers found on your computer", "warning");
                else
                {
                    if (Utility.DoTrim(PropertyLib._FTPProperties.TenmayInPhieutraKQ) == "")
                        cboLaserPrinters.SelectedIndex = 0;
                    else
                        cboLaserPrinters.Text = Utility.DoTrim(PropertyLib._FTPProperties.TenmayInPhieutraKQ);
                }
                cmdPrintRadio.Enabled = cboLaserPrinters.Items.Count > 0 && cboLaserPrinters.SelectedIndex >= 0;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }

        private void MailMerge_MergeField(object sender, MergeFieldEventArgs e)
        {
            if (e.FieldName.Contains("imgPath"))
            {
                var builder = new DocumentBuilder(e.Document);

                // The code below should be adapted to your application specifics.
                byte[] imageData = GetImgFile(e.FieldName);

                InsertImage(e.FieldName, imageData, builder);
            }
        }

        private void MailMerge_MergeImageField(object sender, MergeImageFieldEventArgs e)
        {
            var builder = new DocumentBuilder(e.Document);

            // The code below should be adapted to your application specifics.
            byte[] imageData = GetImgFile(e.FieldName);

            InsertImage(e.FieldName, imageData, builder);
        }

        private byte[] GetImgFile(string fieldName)
        {
            try
            {
                if (_dtData.Rows[0][fieldName.ToLower().Replace("imgpath", "img")] == null ||
                    _dtData.Rows[0][fieldName.ToLower().Replace("imgpath", "img")].Equals(DBNull.Value))
                    return null;
                return (byte[]) _dtData.Rows[0][fieldName.ToLower().Replace("imgpath", "img")];
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
                return null;
            }
        }

        //private bool InsertImage(string mergeFieldName, string imageFileName, DocumentBuilder builder)
        //{
        //    if (File.Exists(imageFileName))
        //    {
        //        // Move builder to merge field (merge field is automatically removed).
        //        if (builder.MoveToMergeField(mergeFieldName))
        //        {
        //            // No image resize by default.
        //            // (Setting size to negative values makes image to be inserted without resizing.)
        //            double width = -1;
        //            double height = -1;

        //            // Check if the image is inserted into table cell.
        //            var cell = (Cell) builder.CurrentParagraph.GetAncestor(typeof (Cell));

        //            if (cell != null)
        //            {
        //                // Set the cell properties so that the inserted image could occupy the cell space exactly.
        //                cell.CellFormat.LeftPadding = 0;
        //                cell.CellFormat.RightPadding = 0;
        //                cell.CellFormat.TopPadding = 0;
        //                cell.CellFormat.BottomPadding = 0;
        //                cell.CellFormat.WrapText = false;
        //                cell.CellFormat.FitText = true;

        //                // Get cell dimensions.
        //                width = cell.CellFormat.Width;
        //                height = cell.ParentRow.RowFormat.Height;
        //            }

        //            // Check if the image is inserted into a textbox.
        //            var shape = (Shape) builder.CurrentParagraph.GetAncestor(typeof (Shape));

        //            if ((shape != null) && (shape.ShapeType == ShapeType.TextBox))
        //            {
        //                // Set the textbox properties so that the inserted image could occupy the textbox space exactly.
        //                shape.TextBox.InternalMarginTop = 0;
        //                shape.TextBox.InternalMarginLeft = 0;
        //                shape.TextBox.InternalMarginBottom = 0;
        //                shape.TextBox.InternalMarginRight = 0;

        //                // Get cell dimensions.
        //                width = shape.Width;
        //                height = shape.Height;
        //            }

        //            // Insert image with or without rescaling, based on the previously done analysis.
        //            builder.InsertImage(imageFileName, width, height);

        //            // Signal the caller that the image was succesfully inserted at merge field position.
        //            return true;
        //        }
        //        // Signal the caller that no merge field with the specified name could be found in the document.
        //        return false;
        //    }
        //    return true;
        //}

        private void InsertImage(string mergeFieldName, byte[] imagedata, DocumentBuilder builder)
        {
            if (imagedata != null)
            {
                // Move builder to merge field (merge field is automatically removed).
                if (builder.MoveToMergeField(mergeFieldName))
                {
                    // No image resize by default.
                    // (Setting size to negative values makes image to be inserted without resizing.)
                    double width = -1;
                    double height = -1;

                    // Check if the image is inserted into table cell.
                    var cell = (Cell) builder.CurrentParagraph.GetAncestor(typeof (Cell));

                    if (cell != null)
                    {
                        // Set the cell properties so that the inserted image could occupy the cell space exactly.
                        cell.CellFormat.LeftPadding = 0;
                        cell.CellFormat.RightPadding = 0;
                        cell.CellFormat.TopPadding = 0;
                        cell.CellFormat.BottomPadding = 0;
                        cell.CellFormat.WrapText = false;
                        cell.CellFormat.FitText = true;

                        // Get cell dimensions.
                        width = cell.CellFormat.Width;
                        height = cell.ParentRow.RowFormat.Height;
                    }

                    // Check if the image is inserted into a textbox.
                    var shape = (Shape) builder.CurrentParagraph.GetAncestor(typeof (Shape));

                    if ((shape != null) && (shape.ShapeType == ShapeType.TextBox))
                    {
                        // Set the textbox properties so that the inserted image could occupy the textbox space exactly.
                        shape.TextBox.InternalMarginTop = 0;
                        shape.TextBox.InternalMarginLeft = 0;
                        shape.TextBox.InternalMarginBottom = 0;
                        shape.TextBox.InternalMarginRight = 0;

                        // Get cell dimensions.
                        //width = shape.Width;
                        //height = shape.Height;
                    }

                    // Insert image with or without rescaling, based on the previously done analysis.
                    builder.InsertImage(imagedata, width, height);

                    // Signal the caller that the image was succesfully inserted at merge field position.
                }
                // Signal the caller that no merge field with the specified name could be found in the document.
            }
        }

        private void SwapImage(DataRow dr)
        {
            try
            {
                if (dr["img1"] != DBNull.Value && dr["img2"] != DBNull.Value && dr["img3"] != DBNull.Value &&
                    dr["img4"] != DBNull.Value) return;
                if (dr["img1"] == DBNull.Value)
                    dr["img1"] = FindNextNotNull(new List<string> {"img2", "img3", "img4"}, dr);
                if (dr["img2"] == DBNull.Value) dr["img2"] = FindNextNotNull(new List<string> {"img3", "img4"}, dr);
                if (dr["img3"] == DBNull.Value) dr["img3"] = FindNextNotNull(new List<string> {"img4"}, dr);
                if (dr["img4"] != DBNull.Value)
                {
                    if (dr["img1"] == DBNull.Value)
                    {
                        dr["img1"] = dr["img4"];
                        dr["img4"] = DBNull.Value;
                    }
                    if (dr["img2"] == DBNull.Value)
                    {
                        dr["img2"] = dr["img4"];
                        dr["img4"] = DBNull.Value;
                    }
                    if (dr["img3"] == DBNull.Value)
                    {
                        dr["img3"] = dr["img4"];
                        dr["img4"] = DBNull.Value;
                    }
                }

                if (dr["Local1"] != DBNull.Value && dr["Local2"] != DBNull.Value && dr["Local3"] != DBNull.Value &&
                    dr["Local4"] != DBNull.Value) return;
                if (dr["Local1"] == DBNull.Value)
                    dr["Local1"] = FindNextNotNull(new List<string> {"Local2", "Local3", "Local4"}, dr);
                if (dr["Local2"] == DBNull.Value)
                    dr["Local2"] = FindNextNotNull(new List<string> {"Local3", "Local4"}, dr);
                if (dr["Local3"] == DBNull.Value) dr["Local3"] = FindNextNotNull(new List<string> {"Local4"}, dr);
                if (dr["Local4"] != DBNull.Value)
                {
                    if (dr["Local1"] == DBNull.Value)
                    {
                        dr["Local1"] = dr["Local4"];
                    }
                    if (dr["Local2"] == DBNull.Value)
                    {
                        dr["Local2"] = dr["Local4"];
                        dr["Local4"] = DBNull.Value;
                    }
                    if (dr["Local3"] == DBNull.Value)
                    {
                        dr["Local3"] = dr["Local4"];
                        dr["Local4"] = DBNull.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }

        private byte[] FindNextNotNull(IEnumerable<string> lstFields, DataRow dr)
        {
            try
            {
                foreach (string field in lstFields)
                {
                    if (dr[field] != DBNull.Value)
                    {
                        var byt = dr[field] as byte[];
                        dr[field] = DBNull.Value;
                        return byt;
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        private void GetImagesFromFtp()
        {
            _mBlnForced2GetImagesFromFtp = true;
            BeginExam();
            _mBlnForced2GetImagesFromFtp = false;
        }

        private void DelFtpImages()
        {
            if (!Directory.Exists(_baseDirectory)) return;
            List<string> lstFiles = Directory.GetFiles(_baseDirectory).ToList();
            if (lstFiles.Count > 0)
            {
                if (Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa bớt các ảnh đã download từ FTP về không",
                    "Xác nhận", true))
                {
                    Cursor = Cursors.WaitCursor;
                    Try2DelImageOnFtpFolder();
                    Cursor = Cursors.Default;
                }
            }
        }

        private void chkPush2FTP_CheckedChanged(object sender, EventArgs e)
        {
            lnkGetImagesFromFTP.Enabled = PropertyLib._FTPProperties.Push2FTP;
            lnkDelFTPImages.Enabled = PropertyLib._FTPProperties.Push2FTP;
            PropertyLib._FTPProperties.Push2FTP = PropertyLib._FTPProperties.Push2FTP;
            PropertyLib.SaveProperty(PropertyLib._FTPProperties);
        }

        private void txtMaluotkham_tk_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string patientCode = Utility.AutoFullPatientCode(txtMaluotkham_tk.Text);
                txtMaluotkham_tk.Text = patientCode;
                cmdSearch.PerformClick();
            }
        }

        #region "Hàm thực hiện việc kiểm tra config"

        /// <summary>
        ///     hàm thực hiện việc conect thông tin của Unc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdConnect_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    //Set FTP
            //    FtpClient = new FTPclient(PropertyLib._FTPProperties.IPAddress, PropertyLib._FTPProperties.UID, PropertyLib._FTPProperties.PWD);

            //    foreach (FTPfileInfo folder in FtpClient.ListDirectoryDetail("/"))
            //    {
            //        var item = new ListViewItem();
            //        item.Text = folder.Filename;
            //        if (folder.FileType == FTPfileInfo.DirectoryEntryTypes.Directory)
            //            item.SubItems.Add("Folder");
            //        else
            //            item.SubItems.Add("File");

            //        item.SubItems.Add(folder.FullName);
            //        item.SubItems.Add(folder.Permission);
            //        item.SubItems.Add(folder.FileDateTime.ToShortTimeString() + folder.FileDateTime.ToShortDateString());
            //        item.SubItems.Add(GetFileSize(folder.Size));
            //        lstRemoteSiteFiles.Items.Add(item);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Utility.CatchException(ex);
            //}
        }

        private string GetFileSize(double byteCount)
        {
            string size = "0 Bytes";
            if (byteCount >= 1073741824.0)
                size = String.Format("{0:##.##}", byteCount/1073741824.0) + " GB";
            else if (byteCount >= 1048576.0)
                size = String.Format("{0:##.##}", byteCount/1048576.0) + " MB";
            else if (byteCount >= 1024.0)
                size = String.Format("{0:##.##}", byteCount/1024.0) + " KB";
            else if (byteCount > 0 && byteCount < 1024.0)
                size = byteCount + " Bytes";

            return size;
        }

        /// <summary>
        ///     hàm thực hện luưu lại thông tin của hàm
        /// </summary>
        /// <param name="e"></param>
        private void SaveConfig()
        {
            _baseDirectory = Utility.DoTrim(PropertyLib._FTPProperties.ImageFolder);
            if (_baseDirectory.EndsWith(@"\")) _baseDirectory = _baseDirectory.Substring(0, _baseDirectory.Length - 1);
            _actionResult = new KCB_HinhAnh().UpdateSysConfigRadio(CreateNewSysRadio());
            switch (_actionResult)
            {
                case ActionResult.Success:
                    Utility.ShowMsg("Bạn thực hiện lưu thông tin thành công", "Thông báo thành công");
                    break;
                case ActionResult.Error:
                    Utility.ShowMsg("Lỗi trong quá trình lưu thông tin", "Thông báo lỗi", MessageBoxIcon.Error);
                    break;
            }
        }

        /// <summary>
        ///     khởi tạo thông tin của đối tượng cấu hình
        /// </summary>
        /// <returns></returns>
        private SysConfigRadio CreateNewSysRadio()
        {
            var objConfigRadio = new SysConfigRadio
            {
                PassWord = PropertyLib._FTPProperties.PWD,
                UserName = PropertyLib._FTPProperties.UID,
                Domain = PropertyLib._FTPProperties.IPAddress,
                PathUNC = PropertyLib._FTPProperties.UNCPath
            };
            return objConfigRadio;
        }

        #endregion

        private void cmdPrintOld_Click(object sender, EventArgs e)
        {
            KcbKetquaHa objKetQuaha = new Select().From(KcbKetquaHa.Schema).ExecuteSingle<KcbKetquaHa>();
          //  string fileName = Path.GetTempFileName() + ".doc";
            String connStr = "Data Source=.; Initial Catalog=VMS_2408; User ID=sa;Password=123456;";
            SqlConnection cn = new SqlConnection(connStr);
            SqlDataAdapter adap = new SqlDataAdapter("select * from kcb_ketqua_ha", cn);
            var ds = new DataSet();
            adap.Fill(ds);
            byte[] FileData = (byte[])ds.Tables[0].Rows[0]["FileData"];
            string fileName = ds.Tables[0].Rows[0]["NameFile"].ToString();
            using (FileStream fs = new FileStream(fileName, FileMode.Create))
            {
                fs.Write(FileData, 0, FileData.Length);
                fs.Close();
            }
            Process prc = new Process();
            prc.StartInfo.FileName = fileName;
            prc.Start();

            //using (SqlConnection conn = new SqlConnection(connStr))
            //{
            //    conn.Open();
            //    using (SqlCommand cmd = conn.CreateCommand())
            //    {
            //        // you have to distinguish here which document, I assume that there is an `id` column
            //        cmd.CommandText = "select * from kcb_ketqua_ha";

            //        SqlDataAdapter ADAP = new SqlDataAdapter("select * from kcb_ketqua_ha");
            //     //   cmd.Parameters.Add("@id", SqlDbType.Int).Value = 1;
            //        using (SqlDataReader dr = cmd.ExecuteReader())
            //        {
            //            while (dr.Read())
            //            {
            //                int size = 1024 * 1024;
            //                byte[] buffer = new byte[size];
            //                int readBytes = 0;
            //                int index = 0;

            //                using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write, FileShare.None))
            //                {
            //                    while ((readBytes = (int)dr.GetBytes(0, index, buffer, 0, size)) > 0)
            //                    {
            //                        fs.Write(buffer, 0, readBytes);
            //                        index += readBytes;
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}

            // open your file, the proper application will be executed because of proper file extension
            //Process prc = new Process();
            //prc.StartInfo.FileName = fileName;
            //prc.Start();
        }
    }
}