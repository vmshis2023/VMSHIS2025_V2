using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Janus.Windows.GridEX;
using SubSonic;
using VMS.HIS.DAL;
using VNS.HIS.UI.Classess;
using VNS.HIS.UI.Forms.Dungchung;
using AggregateFunction = Janus.Windows.GridEX.AggregateFunction;
using System.IO;
using VNS.Libs;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;
using System.Drawing.Printing;
using VNS.HIS.Classes;
using VNS.HIS.UI.Baocao;
using VNS.HIS.UI.THANHTOAN;
using VNS.HIS.UI.Forms.Cauhinh;
using VNS.HIS.UI.Forms.HinhAnh;
using VNS.HIS.UI.HinhAnh;
using VNS.HIS.UI.NOITRU;
using VNS.HIS.UI.Forms.NGOAITRU;
using System.Threading;
using System.Diagnostics;
using System.Runtime.InteropServices;
using VNS.HIS.UI.Forms.Noitru;
using VNS.HIS.UI.GOIKHAM;
using VNS.HIS.BusRule.Goikham;
namespace VNS.HIS.UI.NGOAITRU
{
    public partial class frm_KCB_DSACH_BNHAN : BaseForm
    {
        KCB_CHIDINH_CANLAMSANG _KCB_CHIDINH_CANLAMSANG = new KCB_CHIDINH_CANLAMSANG();
        private DataTable m_dtDanhsachDichvuKCB = new DataTable();
        private DataTable m_dtPatient = new DataTable();
        private DataTable m_PhongKham = new DataTable();
        private DataTable m_kieuKham;
        private DataTable m_dtChiDinhCLS = new DataTable();
        private bool m_blnHasloaded = false;
        string Args = "ALL";
        string SplitterPath = "";
        public frm_KCB_DSACH_BNHAN(string Args)
        {
            InitializeComponent();
            SplitterPath = string.Format(@"{0}\{1}.splitter", Application.StartupPath, this.Name);
            Utility.SetVisualStyle(this);
            this.Args = Args;
            this.InitTrace();
            webBrowser1.Url = new Uri(Application.StartupPath.ToString() + @"\editor\ckeditor_simple.html");
            timer1.Enabled = true;
            timer1.Interval = 900;
            this.KeyPreview = true;
            dtmFrom.Value = globalVariables.SysDate;
            dtmTo.Value = globalVariables.SysDate;
            dtmFrom.MaxDate = dtmTo.MaxDate = globalVariables.SysDate;
            InitEvents();
            InitFtp();
            CauHinh();
        }
        void InitEvents()
        {
            Shown += frm_KCB_DSACH_BNHAN_Shown;
            this.txtPatientCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPatientCode_KeyDown);
            this.txtPatient_ID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPatient_ID_KeyDown);
            this.cmdTimKiem.Click += new System.EventHandler(this.cmdTimKiem_Click);

            grdList.SelectionChanged += new EventHandler(grdList_SelectionChanged);
            grdList.MouseDoubleClick += grdList_MouseDoubleClick;
            this.grdRegExam.ColumnButtonClick += new Janus.Windows.GridEX.ColumnActionEventHandler(this.grdRegExam_ColumnButtonClick);
            this.grdRegExam.SelectionChanged += new System.EventHandler(this.grdRegExam_SelectionChanged);
            grdRegExam.UpdatingCell += grdRegExam_UpdatingCell;

            this.cmdThemMoiBN.Click += new System.EventHandler(this.cmdThemMoiBN_Click);
            this.cmdSuaThongTinBN.Click += new System.EventHandler(this.cmdSuaThongTinBN_Click);
            this.cmdThemLanKham.Click += new System.EventHandler(this.cmdThemLanKham_Click);
            this.cmdXoaBenhNhan.Click += new System.EventHandler(this.cmdXoaBenhNhan_Click);
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);

            this.cboKieuKham.ValueChanged += new System.EventHandler(this.cboKieuKham_ValueChanged);
            this.cmdAddDvuKCB.Click += new System.EventHandler(this.cmdAddDvuKCB_Click);
            this.lblPhuThu.TextChanged += new System.EventHandler(this.lblPhuThu_TextChanged);
            this.lblDonGia.TextChanged += new System.EventHandler(this.lblDonGia_TextChanged);
            this.lblDonGia.Click += new System.EventHandler(this.lblDonGia_Click);
            this.cmdThanhToanKham.Click += new System.EventHandler(this.cmdThanhToanKham_Click);
            this.cmdInPhieuKham.Click += new System.EventHandler(this.cmdInPhieuKham_Click);
            this.cmdXoaCongkham.Click += new System.EventHandler(this.cmdXoaCongkham_Click);
            grdAssignDetail.MouseDoubleClick += grdAssignDetail_MouseDoubleClick;
            this.grdAssignDetail.CellValueChanged += new Janus.Windows.GridEX.ColumnActionEventHandler(this.grdAssignDetail_CellValueChanged);
            this.grdAssignDetail.FormattingRow += new Janus.Windows.GridEX.RowLoadEventHandler(this.grdAssignDetail_FormattingRow);
            this.grdAssignDetail.ColumnHeaderClick += new Janus.Windows.GridEX.ColumnActionEventHandler(this.grdAssignDetail_ColumnHeaderClick);
            this.grdAssignDetail.SelectionChanged += new System.EventHandler(this.grdAssignDetail_SelectionChanged);
            this.txtTongChiPhi.TextChanged += new System.EventHandler(this.txtTongChiPhi_TextChanged);
            this.cmdXoaChiDinh.Click += new System.EventHandler(this.cmdXoaChiDinh_Click);
            this.cmdSuaChiDinh.Click += new System.EventHandler(this.cmdSuaChiDinh_Click);
            this.cmdThemChiDinh.Click += new System.EventHandler(this.cmdThemChiDinh_Click);

            this.chkByDate.CheckedChanged += new System.EventHandler(this.chkByDate_CheckedChanged);

            this.cmdCauhinh.Click += new System.EventHandler(this.cmdCauhinh_Click);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_KCB_DSACH_BNHAN_FormClosing);
            this.Load += new System.EventHandler(this.frm_KCB_DSACH_BNHAN_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_KCB_DSACH_BNHAN_KeyDown);
            txtPhongkham._OnSelectionChanged += new UCs.AutoCompleteTextbox.OnSelectionChanged(txtPhongkham__OnSelectionChanged);
            txtKieuKham._OnSelectionChanged += new UCs.AutoCompleteTextbox.OnSelectionChanged(txtKieuKham__OnSelectionChanged);
            txtPhongkham._OnEnterMe += txtPhongkham__OnEnterMe;
            txtKieuKham._OnEnterMe += txtKieuKham__OnEnterMe;
            txtExamtypeCode._OnSelectionChanged += new VNS.HIS.UCs.AutoCompleteTextbox.OnSelectionChanged(txtExamtypeCode__OnSelectionChanged);
            txtExamtypeCode._OnEnterMe += txtExamtypeCode__OnEnterMe;
            mnuMaDVu.Click += new EventHandler(mnuMaDVu_Click);

            cboKieuin.SelectedIndexChanged += new EventHandler(cboKieuin_SelectedIndexChanged);
            chkInsaukhiluu.CheckedChanged += new EventHandler(chkInsaukhiluu_CheckedChanged);
            cboPrintPreview.SelectedIndexChanged += new EventHandler(cboPrintPreview_SelectedIndexChanged);
            cboLaserPrinters.SelectedIndexChanged += new EventHandler(cboLaserPrinters_SelectedIndexChanged);
            cboA4.SelectedIndexChanged += new EventHandler(cboA4_SelectedIndexChanged);

            cmdInBienlai.Click += new EventHandler(cmdInlaihoadon_Click);
            cmdInhoadon.Click += new EventHandler(cmdInhoadon_Click);
            txtExamtypeCode.KeyDown += txtExamtypeCode_KeyDown;
            cmdPrintAssign.Click += new EventHandler(cmdPrintAssign_Click);
            mnuDelFile.Click+=mnuDelFile_Click;
            lnkMore.Click += lnkMore_Click;
            //mnuNhapKQCDHA.Click += mnuNhapKQCDHA_Click;
            //pnlXQ.Width = PropertyLib._KCBProperties.ResultInput ? 400 : 0;
            txtKieuKham.LostFocus += txtKieuKham_LostFocus;
            txtPhongkham.LostFocus += txtPhongkham_LostFocus;
        }

        void txtExamtypeCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
                grdList_SelectionChanged(grdList, new EventArgs());
        }
        bool AllowCongkham_SelectionChanged = false;
        DataTable dtDataCheck = new DataTable();
        void grdRegExam_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                 long id_kham = Utility.Int64Dbnull(grdRegExam.GetValue("id_kham"));
                 int num = 0;
                 decimal tyle_tt = 0;
                KcbDangkyKcb objCK =null;
                if (e.Column.Key == "stt_tt37")
                {
                    AllowCongkham_SelectionChanged = false;
                    byte oldSTT = Utility.ByteDbnull(e.InitialValue, 1);
                    byte newSTT = Utility.ByteDbnull(e.Value, 0);
                    if (newSTT <= 0 || newSTT > 255)
                    {
                        e.Value = e.InitialValue;
                        Utility.ShowMsg(string.Format("STT công khám phải nằm trong khoảng: 1-{0}", m_dtDangkyPhongkham.Rows.Count));
                        return;
                    }

                    tyle_tt = THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb) ? THU_VIEN_CHUNG.Bhyt_Laytyle_tt_congkham(THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb), oldSTT) : 100;
                    //Tìm công khám có STT hoán đổi với công khám này
                     objCK = new Select().From(KcbDangkyKcb.Schema)
                   .Where(KcbDangkyKcb.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                   .And(KcbDangkyKcb.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                   .And(KcbDangkyKcb.Columns.TrangthaiThanhtoan).IsEqualTo(0)
                   .And(KcbDangkyKcb.Columns.SttTt37).IsEqualTo(newSTT)
                   .And(KcbDangkyKcb.Columns.IdKham).IsNotEqualTo(id_kham)
                   .ExecuteSingle<KcbDangkyKcb>();
                   
                    if (objCK != null)
                    {
                        objCK.TyleTt = tyle_tt;
                        objCK.SttTt37 = oldSTT;
                        THU_VIEN_CHUNG.Bhyt_PhantichGiaCongkham(objLuotkham, objCK);
                        num = new Update(KcbDangkyKcb.Schema)
                        .Set(KcbDangkyKcb.Columns.SttTt37).EqualTo(objCK.SttTt37)
                    .Set(KcbDangkyKcb.Columns.BhytChitra).EqualTo(objCK.BhytChitra)
                    .Set(KcbDangkyKcb.Columns.BhytGiaTyle).EqualTo(objCK.BhytGiaTyle)
                    .Set(KcbDangkyKcb.Columns.BnCct).EqualTo(objCK.BnCct)
                    .Set(KcbDangkyKcb.Columns.BnTtt).EqualTo(objCK.BnTtt)
                    .Set(KcbDangkyKcb.Columns.BnhanChitra).EqualTo(objCK.BnhanChitra)
                    .Set(KcbDangkyKcb.Columns.TyleTt).EqualTo(objCK.TyleTt)
                    .Set(KcbDangkyKcb.Columns.PtramBhyt).EqualTo(objCK.PtramBhyt)
                    .Set(KcbDangkyKcb.Columns.PtramBhytGoc).EqualTo(objCK.PtramBhytGoc)
                   .Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(objCK.IdKham)
                   .Execute();
                        if (num > 0)
                        {
                            DataRow[] arrDr = m_dtDangkyPhongkham.Select(string.Format("id_kham={0}", objCK.IdKham));
                            if (arrDr.Length > 0)
                            {
                                arrDr[0][KcbDangkyKcb.Columns.SttTt37] = objCK.SttTt37;
                                arrDr[0][KcbDangkyKcb.Columns.BhytChitra] = objCK.BhytChitra;
                                arrDr[0][KcbDangkyKcb.Columns.BhytGiaTyle] = objCK.BhytGiaTyle;
                                arrDr[0][KcbDangkyKcb.Columns.BnCct] = objCK.BnCct;
                                arrDr[0][KcbDangkyKcb.Columns.BnTtt] = objCK.BnTtt;
                                arrDr[0][KcbDangkyKcb.Columns.BnhanChitra] = objCK.BnhanChitra;
                                arrDr[0][KcbDangkyKcb.Columns.TyleTt] = objCK.TyleTt;
                                arrDr[0][KcbDangkyKcb.Columns.PtramBhyt] = objCK.PtramBhyt;
                                arrDr[0][KcbDangkyKcb.Columns.PtramBhytGoc] = objCK.PtramBhytGoc;
                            }
                            //(from p in m_dtDangkyPhongkham.AsEnumerable() where Utility.Int64Dbnull(p["id_kham"], -1) == objCK.IdKham select p).ToList()
                            //    .ForEach(x =>
                            //    {
                            //        x[KcbDangkyKcb.Columns.SttTt37] = objCK.SttTt37;
                            //        x[KcbDangkyKcb.Columns.BhytChitra] = objCK.BhytChitra;
                            //        x[KcbDangkyKcb.Columns.BhytGiaTyle] = objCK.BhytGiaTyle;
                            //        x[KcbDangkyKcb.Columns.BnCct] = objCK.BnCct;
                            //        x[KcbDangkyKcb.Columns.BnTtt] = objCK.BnTtt;
                            //        x[KcbDangkyKcb.Columns.BnhanChitra] = objCK.BnhanChitra;
                            //        x[KcbDangkyKcb.Columns.TyleTt] = objCK.TyleTt;
                            //        x[KcbDangkyKcb.Columns.PtramBhyt] = objCK.PtramBhyt;
                            //        x[KcbDangkyKcb.Columns.PtramBhytGoc] = objCK.PtramBhytGoc;
                            //    });
                            m_dtDangkyPhongkham.AcceptChanges();
                        }

                    }
                    tyle_tt = THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb) ? THU_VIEN_CHUNG.Bhyt_Laytyle_tt_congkham(THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb), newSTT) : 100;
                    objCK = KcbDangkyKcb.FetchByID(id_kham);
                    objCK.TyleTt = tyle_tt;
                    objCK.SttTt37 = newSTT;
                    THU_VIEN_CHUNG.Bhyt_PhantichGiaCongkham(objLuotkham, objCK);
                    num = new Update(KcbDangkyKcb.Schema)
                     .Set(KcbDangkyKcb.Columns.SttTt37).EqualTo(objCK.SttTt37)
                 .Set(KcbDangkyKcb.Columns.BhytChitra).EqualTo(objCK.BhytChitra)
                 .Set(KcbDangkyKcb.Columns.BhytGiaTyle).EqualTo(objCK.BhytGiaTyle)
                 .Set(KcbDangkyKcb.Columns.BnCct).EqualTo(objCK.BnCct)
                 .Set(KcbDangkyKcb.Columns.BnTtt).EqualTo(objCK.BnTtt)
                 .Set(KcbDangkyKcb.Columns.BnhanChitra).EqualTo(objCK.BnhanChitra)
                 .Set(KcbDangkyKcb.Columns.TyleTt).EqualTo(objCK.TyleTt)
                 .Set(KcbDangkyKcb.Columns.PtramBhyt).EqualTo(objCK.PtramBhyt)
                 .Set(KcbDangkyKcb.Columns.PtramBhytGoc).EqualTo(objCK.PtramBhytGoc)
                 .Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(objCK.IdKham)
                .Execute();
                    if (num > 0)
                    {
                        DataRow[] arrDr = m_dtDangkyPhongkham.Select(string.Format("id_kham={0}", objCK.IdKham));
                        if (arrDr.Length > 0)
                        {
                            arrDr[0][KcbDangkyKcb.Columns.SttTt37] = objCK.SttTt37;
                            arrDr[0][KcbDangkyKcb.Columns.BhytChitra] = objCK.BhytChitra;
                            arrDr[0][KcbDangkyKcb.Columns.BhytGiaTyle] = objCK.BhytGiaTyle;
                            arrDr[0][KcbDangkyKcb.Columns.BnCct] = objCK.BnCct;
                            arrDr[0][KcbDangkyKcb.Columns.BnTtt] = objCK.BnTtt;
                            arrDr[0][KcbDangkyKcb.Columns.BnhanChitra] = objCK.BnhanChitra;
                            arrDr[0][KcbDangkyKcb.Columns.TyleTt] = objCK.TyleTt;
                            arrDr[0][KcbDangkyKcb.Columns.PtramBhyt] = objCK.PtramBhyt;
                            arrDr[0][KcbDangkyKcb.Columns.PtramBhytGoc] = objCK.PtramBhytGoc;
                        }
                        //(from p in m_dtDangkyPhongkham.AsEnumerable() where Utility.Int64Dbnull(p["id_kham"], -1) == objCK.IdKham select p).ToList()
                        //        .ForEach(x =>
                        //        {
                        //            x[KcbDangkyKcb.Columns.SttTt37] = objCK.SttTt37;
                        //            x[KcbDangkyKcb.Columns.BhytChitra] = objCK.BhytChitra;
                        //            x[KcbDangkyKcb.Columns.BhytGiaTyle] = objCK.BhytGiaTyle;
                        //            x[KcbDangkyKcb.Columns.BnCct] = objCK.BnCct;
                        //            x[KcbDangkyKcb.Columns.BnTtt] = objCK.BnTtt;
                        //            x[KcbDangkyKcb.Columns.BnhanChitra] = objCK.BnhanChitra;
                        //            x[KcbDangkyKcb.Columns.TyleTt] = objCK.TyleTt;
                        //            x[KcbDangkyKcb.Columns.PtramBhyt] = objCK.PtramBhyt;
                        //            x[KcbDangkyKcb.Columns.PtramBhytGoc] = objCK.PtramBhytGoc;
                        //        });
                        m_dtDangkyPhongkham.AcceptChanges();
                    }
                }
                else if (e.Column.Key == "ngay_dangky")
                {
                    if (e.InitialValue != e.Value)
                    {
                        DateTime tgian_dky=Convert.ToDateTime(e.Value);
                        objCK = KcbDangkyKcb.FetchByID(id_kham);
                        if (objCK.ThoigianBatdau.HasValue)
                        {
                            if (tgian_dky < objCK.NgayDangky)//Chỉnh time Đăng ký trước thời điểm đăng ký tiếp đón
                            {
                                Utility.ShowMsg(string.Format("Thời gian đăng ký phải >= thời gian tiếp đón người bệnh {0}", objCK.NgayDangky.Value.ToString("dd/MM/yyyy HH:mm:ss")));
                                e.Value = e.InitialValue;
                                return;
                            }
                        }
                        if (objCK.ThoigianBatdau.HasValue)
                        {
                            if (tgian_dky < objCK.ThoigianBatdau)//Chỉnh time Đăng ký trước thời điểm bắt đầu khám
                            {
                                Utility.ShowMsg(string.Format("Thời gian đăng ký phải >= thời gian bắt đầu khám {0}", objCK.ThoigianBatdau.Value.ToString("dd/MM/yyyy HH:mm:ss")));
                                e.Value = e.InitialValue;
                                return;
                            }
                        }
                        if (objCK.ThoigianKetthuc.HasValue)
                        {
                            if (tgian_dky > objCK.ThoigianKetthuc)//Đăng ký sau thời gian kết thúc khám
                            {
                                Utility.ShowMsg(string.Format("Thời gian đăng ký phải<= thời gian kết thúc khám {0}", objCK.ThoigianKetthuc.Value.ToString("dd/MM/yyyy HH:mm:ss")));
                                e.Value = e.InitialValue;
                                return;
                            }
                        }
                        KcbChidinhcl objChidinh = new Select().From(KcbChidinhcl.Schema).Where(KcbChidinhcl.Columns.IdKham).IsEqualTo(objCongkham.IdKham).And(KcbChidinhcl.Columns.NgayChidinh).IsLessThan(tgian_dky).ExecuteSingle<KcbChidinhcl>();
                        if (objChidinh!=null)
                        {
                            Utility.ShowMsg(string.Format("Ngày đăng ký đang > thời gian chỉ định CLS: {0} - Mã chỉ định: {1} của công khám. Đề nghị nhập lại thời gian đăng ký", objChidinh.NgayChidinh.ToString("dd/MM/yyyy HH:mm:ss"), objChidinh.MaChidinh), "Thông báo");
                            grdRegExam.Focus();
                            return ;
                        }
                        KcbDonthuoc objDT = new Select().From(KcbDonthuoc.Schema).Where(KcbDonthuoc.Columns.IdKham).IsEqualTo(objCongkham.IdKham).And(KcbDonthuoc.Columns.NgayKedon).IsLessThan(tgian_dky).ExecuteSingle<KcbDonthuoc>();
                        if (objDT != null)
                        {
                            Utility.ShowMsg(string.Format("Ngày đăng ký đang > thời gian kê đơn thuốc: {0} - ID đơn thuốc: {1} của công khám. Đề nghị nhập lại thời gian đăng ký", objDT.NgayKedon.ToString("dd/MM/yyyy HH:mm:ss"), objDT.IdDonthuoc), "Thông báo");
                            grdRegExam.Focus();
                            return;
                        }
                        dtDataCheck = Utility.ExecuteSql(string.Format("select 1 from kcb_dangky_kcb where id_benhnhan={0} and ma_luotkham='{1}' and ngay_dangky<={2} and id_kham<>{3}", objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, tgian_dky,id_kham), CommandType.Text).Tables[0];
                        byte newSTT = Utility.ByteDbnull( dtDataCheck.Rows.Count + 1);
                        tyle_tt = THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb) ? THU_VIEN_CHUNG.Bhyt_Laytyle_tt_congkham(THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb), newSTT) : 100;
                        num = new Update(KcbDangkyKcb.Schema)
                            .Set(KcbDangkyKcb.NgayDangkyColumn).EqualTo(tgian_dky)
                            .Set(KcbDangkyKcb.SttTt37Column).EqualTo(newSTT)
                            .Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(id_kham).Execute();
                        grdRegExam.CurrentRow.BeginEdit();
                        grdRegExam.CurrentRow.Cells["stt_tt37"].Value = newSTT;
                        grdRegExam.CurrentRow.EndEdit();
                    }
                }

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);

            }
            finally
            {
                AllowCongkham_SelectionChanged = true;
            }
        }

        void CanhbaoNgaydangky(int errType)
        {
            if (errType == 1)
            {
                Utility.ShowMsg("Tồn tại bản ghi");
            }
            else if (errType == 2)
            {
            }
            else if (errType == 3)
            {
            }
        }
        void txtPhongkham_LostFocus(object sender, EventArgs e)
        {
            //AutoselectcongkhambyKieukham_Phongkham();  
        }

        void txtKieuKham_LostFocus(object sender, EventArgs e)
        {
            //AutoselectcongkhambyKieukham_Phongkham();
        }
        void AutoselectcongkhambyKieukham_Phongkham()
        {
            if (txtPhongkham.MyID.ToString() != "-1")
            {
                DataRow[] arrDr = m_dtDanhsachDichvuKCB.Select(string.Format("id_dichvukcb={0} ", txtPhongkham.MyID.ToString()));
                //DataRow[] arrDr = m_dtDanhsachDichvuKCB.Select(string.Format("id_kieukham={0} and id_phongkham={1}", txtKieuKham.MyID.ToString(), txtPhongkham.MyID.ToString()));
                if (arrDr.Length > 0)
                    cboKieuKham.SelectedIndex = Utility.GetSelectedIndex(cboKieuKham, arrDr[0][DmucDichvukcb.Columns.IdDichvukcb].ToString());
            }
        }
        void frm_KCB_DSACH_BNHAN_Shown(object sender, EventArgs e)
        {
            Try2Splitter();
            Application.DoEvents();
            LoadMe();
        }
        void Try2Splitter()
        {
            try
            {
                int value1 = Utility.Int32Dbnull(Utility.GetFirstValueFromFile(SplitterPath), -1);
                if (value1 > 0)
                    splitContainer1.SplitterDistance = value1;
            }
            catch (Exception)
            {

            }
        }
        void txtExamtypeCode__OnEnterMe()
        {
            cboKieuKham.Text = txtMyNameEdit.Text;
            //cboKieuKham.Value = txtExamtypeCode.MyID;
            txtKieuKham._Text = cboKieuKham.Text;
            txtIDKieuKham.Text = Utility.sDbnull(txtExamtypeCode.MyID);
        }

        void txtPhongkham__OnEnterMe()
        {
            DataRow[] arrDr = m_dtDanhsachDichvuKCB.Select("id_dichvukcb=" + txtPhongkham.MyID);
            AutoselectcongkhambyKieukham_Phongkham();
            //cboKieuKham.SelectedIndex = Utility.GetSelectedIndex(cboKieuKham, txtPhongkham.MyID.ToString());//Text = arrDr.Length <= 0 ? "---Chọn công khám----" : arrDr[0]["ten_dichvukcb"].ToString();
            //cboKieuKham_ValueChanged(cboKieuKham, new EventArgs());
        }

        void txtKieuKham__OnEnterMe()
        {
            AutoloadPhongkham_Congkham();
        }
        void AutoloadPhongkham_Congkham()
        {
            m_PhongKham = m_dtDanhsachDichvuKCB.Clone();

            DataRow[] arrdr = m_dtDanhsachDichvuKCB.Select("id_kieukham=" + txtKieuKham.MyID);
            if (arrdr.Length > 0)
                m_PhongKham = arrdr.CopyToDataTable();
            if (!m_PhongKham.Columns.Contains("TEN"))
                m_PhongKham.Columns.AddRange(new DataColumn[] { new DataColumn("Id", typeof(string)), new DataColumn("Ma", typeof(string)), new DataColumn("TEN", typeof(string)) });
            foreach (DataRow dr in m_PhongKham.Rows)
            {
                dr["id"] = dr["id_dichvukcb"].ToString();// string.Format("{0}-{1}", dr["id_phongkham"].ToString(), dr["id_dichvukcb"].ToString());
                dr["ma"] = dr["ma_phongkham"].ToString();// string.Format("{0}-{1}", dr["ma_phongkham"].ToString(), dr["ma_dichvukcb"].ToString());
                dr["ten"] = string.Format("{0}-{1}-Đơn giá:{2}", dr["ten_phongkham"].ToString(), dr["ten_dichvukcb"].ToString(), dr["don_gia"].ToString());
            }
            txtPhongkham.Init(m_PhongKham, new List<string>() { "id", "ma", "ten" });
        }
        void mnuNhapKQCDHA_Click(object sender, EventArgs e)
        {
            BeginExam();
        }

        void lnkMore_Click(object sender, EventArgs e)
        {
            BeginExam();
        }
        void grdAssignDetail_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            BeginExam();
        }

        void grdList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            cmdSuaThongTinBN.PerformClick();
        }

        void cmdPrintAssign_Click(object sender, EventArgs e)
        {
            try
            {
                string mayin = "";
                int vAssignId = Utility.Int32Dbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh), -1);
                string vAssignCode = Utility.sDbnull(grdAssignDetail.GetValue(KcbChidinhcl.Columns.MaChidinh), -1);
                var nhomcls = new List<string>();
                foreach (GridEXRow gridExRow in grdAssignDetail.GetDataRows())
                {
                    if (Utility.Int64Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChidinh].Value) == vAssignId)
                        if (!nhomcls.Contains(Utility.sDbnull(gridExRow.Cells["nhom_in_cls"].Value)))
                        {
                            nhomcls.Add(Utility.sDbnull(gridExRow.Cells["nhom_in_cls"].Value));
                        }
                }
                List<long> lstSelectedPrint = (from p in grdAssignDetail.GetCheckedRows().AsEnumerable()
                                               select Utility.Int64Dbnull(p.Cells["id_chitietchidinh"].Value, 0)).ToList();
                string nhomincls = "ALL";
                if (cboServicePrint.SelectedIndex > 0)
                {
                    nhomincls = Utility.sDbnull(cboServicePrint.SelectedValue, "ALL");
                    if (nhomincls == "-1")
                    {
                        nhomincls = "DV";
                    }

                }
                var actionResult = ActionResult.Error;
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THAMKHAM_INTACHTOANBO_CLS", "0", false) == "1" &&
                   chkIntach.Checked && cboServicePrint.SelectedIndex <= 0)
                {
                    actionResult = KcbInphieu.InTachToanBoPhieuCls(lstSelectedPrint,(int)objLuotkham.IdBenhnhan,
                                                                     objLuotkham.MaLuotkham, vAssignId,
                                                                     vAssignCode, nhomcls, Utility.sDbnull(cboServicePrint.SelectedValue, "ALL"),
                                                                     cboServicePrint.SelectedIndex, chkIntach.Checked,
                                                                     ref mayin);
                }
                else
                {
                    actionResult = KcbInphieu.InphieuChidinhCls((int)objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham,
                                                                  vAssignId,
                                                                  vAssignCode, nhomincls, cboServicePrint.SelectedIndex,
                                                                  chkIntach.Checked,
                                                                  ref mayin);
                }
                //KcbInphieu.InphieuChidinhCls((int)objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, vAssignId, vAssignCode, nhomincls, cboServicePrint.SelectedIndex, chkIntach.Checked, ref mayin);
                if (mayin != "") cboLaserPrinters.Text = mayin;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        void cmdInhoadon_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdRegExam)) return;
                int paymentId = Utility.Int32Dbnull(grdRegExam.CurrentRow.Cells[KcbDangkyKcb.Columns.IdThanhtoan].Value);
                InHoadon(paymentId);
            }
            catch
            { }
        }

        void cmdInlaihoadon_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdRegExam)) return;
                int Payment_Id = Utility.Int32Dbnull(grdRegExam.CurrentRow.Cells[KcbDangkyKcb.Columns.IdThanhtoan].Value);
                new INPHIEU_THANHTOAN_NGOAITRU().InBienlai(false, Payment_Id,-1, objLuotkham, 0);
            }
            catch
            { }
        }




        void cboA4_SelectedIndexChanged(object sender, EventArgs e)
        {

            PropertyLib._MayInProperties.CoGiayInBienlai = cboA4.SelectedIndex == 0 ? Papersize.A4 : Papersize.A5;
            PropertyLib.SaveProperty(PropertyLib._KCBProperties);
        }

        void cboLaserPrinters_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_blnHasloaded) return;
            PropertyLib._MayInProperties.TenMayInPhieuKCB = cboLaserPrinters.Text;
            PropertyLib.SaveProperty(PropertyLib._KCBProperties);
        }

        void cboPrintPreview_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_blnHasloaded) return;
            PropertyLib._MayInProperties.PreviewPhieuKCB = cboPrintPreview.SelectedIndex == 0;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }

        void chkInsaukhiluu_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_blnHasloaded) return;
            PropertyLib._MayInProperties.InPhieuKCBsaukhiluu = chkInsaukhiluu.Checked;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }

        void cboKieuin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_blnHasloaded) return;
            PropertyLib._MayInProperties.KieuInPhieuKCB = cboKieuin.SelectedIndex == 0 ? KieuIn.Innhiet : KieuIn.InLaser;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }

        void txtExamtypeCode__OnSelectionChanged()
        {
            cboKieuKham.Text = txtMyNameEdit.Text;
            //cboKieuKham.Value = txtExamtypeCode.MyID;
            txtKieuKham._Text = cboKieuKham.Text;
            txtIDKieuKham.Text = Utility.sDbnull(txtExamtypeCode.MyID);
        }

        void mnuMaDVu_Click(object sender, EventArgs e)
        {
            PropertyLib._KCBProperties.GoMaDvu = mnuMaDVu.Checked;
            pnlKieuPhongkham.Visible = !PropertyLib._KCBProperties.GoMaDvu;
            pnlGoMaDvu.Visible = PropertyLib._KCBProperties.GoMaDvu;
            PropertyLib.SaveProperty(PropertyLib._KCBProperties);
        }

        void txtPhongkham__OnSelectionChanged()
        {
            AutoLoadKieuKham();
        }

        void txtKieuKham__OnSelectionChanged()
        {
            AutoLoadKieuKham();
        }
        private void AutoLoadKieuKham()
        {
            try
            {
                if (Utility.Int32Dbnull(txtIDKieuKham, -1) == -1 || Utility.Int32Dbnull(txtIDPkham, -1) == -1)
                {
                    cboKieuKham.Text = "CHỌN PHÒNG KHÁM";
                    cboKieuKham.SelectedIndex = -1;
                }
                DataRow[] arrDr =
                     m_dtDanhsachDichvuKCB.Select(DmucDoituongkcb.Columns.MaDoituongKcb + "='ALL' OR " + DmucDoituongkcb.Columns.MaDoituongKcb + "='" + MA_DTUONG + "' AND " + DmucKieukham.Columns.IdKieukham + "=" +
                                                  txtIDKieuKham.Text.Trim() + " AND " + DmucDichvukcb.Columns.IdPhongkham + "=" + txtIDPkham.Text.Trim());
                if (arrDr.Length <= 0)
                {
                    cboKieuKham.Text = "CHỌN PHÒNG KHÁM";
                    cboKieuKham.SelectedIndex = -1;
                }
                else
                {
                    cboKieuKham.Text = arrDr[0][DmucDichvukcb.Columns.TenDichvukcb].ToString();
                }
            }
            catch
            {
            }
        }
        private void CauHinh()
        {

            if (PropertyLib._KCBProperties != null)
            {

                //cmdThanhToanKham.Enabled = blnChophepthanhtoan;
                //cmdThanhToanKham.Visible = cmdThanhToanKham.Enabled;

                grdRegExam.RootTable.Columns["colThanhtoan"].Visible = blnChophepthanhtoan && (PropertyLib._KCBProperties.Kieuhienthi == Kieuhienthi.Trenluoi || PropertyLib._KCBProperties.Kieuhienthi == Kieuhienthi.Cahai);
                grdRegExam.RootTable.Columns["colDelete"].Visible = PropertyLib._KCBProperties.Kieuhienthi == Kieuhienthi.Trenluoi || PropertyLib._KCBProperties.Kieuhienthi == Kieuhienthi.Cahai;
                grdRegExam.RootTable.Columns["colIn"].Visible = PropertyLib._KCBProperties.Kieuhienthi == Kieuhienthi.Trenluoi || PropertyLib._KCBProperties.Kieuhienthi == Kieuhienthi.Cahai;
                pnlnutchucnang.Visible = PropertyLib._KCBProperties.Kieuhienthi != Kieuhienthi.Trenluoi;
                pnlnutchucnang.Height = PropertyLib._KCBProperties.Kieuhienthi == Kieuhienthi.Trenluoi ? 0 : 33;
                tabPageChiDinh.TabVisible = THU_VIEN_CHUNG.Laygiatrithamsohethong("TIEPDON_CHOPHEP_CHIDINH_KHONGQUAPHONGKHAM", "0", false) == "1";
                tabChiDinh.Width = THU_VIEN_CHUNG.Laygiatrithamsohethong("TIEPDON_CHOPHEP_CHIDINH_KHONGQUAPHONGKHAM", "0", false) == "0" ? 0 : PropertyLib._KCBProperties.Chieurong;
                pnlKieuPhongkham.Visible = !PropertyLib._KCBProperties.GoMaDvu;
                pnlGoMaDvu.Visible = PropertyLib._KCBProperties.GoMaDvu;
                mnuMaDVu.Checked = PropertyLib._KCBProperties.GoMaDvu;
                cboA4.Text = PropertyLib._MayInProperties.CoGiayInBienlai == Papersize.A4 ? "A4" : "A5";
                cboPrintPreview.SelectedIndex = PropertyLib._MayInProperties.PreviewPhieuKCB ? 0 : 1;
                cboLaserPrinters.Text = PropertyLib._MayInProperties.TenMayInBienlai;
                cboKieuin.SelectedIndex = PropertyLib._MayInProperties.KieuInPhieuKCB == KieuIn.Innhiet ? 0 : 1;
                chkInsaukhiluu.Checked = PropertyLib._MayInProperties.InPhieuKCBsaukhiluu;

            }
            mnuUpdateMaBenhAn.Visible =
                mnuUpdateMaBenhNhan.Visible = mnuUpdateMalankham.Visible = mnuupdatengaykham.Visible = Utility.Coquyen("tiepdon_capnhat_thongtinbenhnhan");
        }
        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtmTo.Enabled = dtmFrom.Enabled = chkByDate.Checked;
        }
        void TimKiemThongTin(bool theongay)
        {
            try
            {
                mnuShow.Visible = mnuHide.Visible =mnuShowHideManagement.Visible= Utility.CoquyenSuperAdmin("TIEPDON_SHS");
                int Hos_status = 0;
                //if (radNgoaiTru.Checked) Hos_status = 0;
                //if (radNoiTru.Checked) Hos_status = 1;
                Hos_status = Utility.Int16Dbnull(cboTrangThai.SelectedValue, -1);
                m_dtPatient = _KCB_DANGKY.KcbTiepdonTimkiemBenhnhan(theongay ? (chkByDate.Checked ? dtmFrom.Value.ToString("dd/MM/yyyy") : "01/01/1900") : "01/01/1900",
                    theongay ? (chkByDate.Checked ? dtmTo.Value.ToString("dd/MM/yyyy") : "01/01/1900") : "01/01/1900",
                                                     Utility.Int32Dbnull(cboObjectType.SelectedValue, -1), Hos_status,
                                                     Utility.sDbnull(txtPatientName.Text),
                                                     Utility.Int32Dbnull(txtPatient_ID.Text, -1),
                                                     Utility.sDbnull(txtPatientCode.Text), "",new DateTime(1900,1,1),100, "", globalVariables.MA_KHOA_THIEN, 0, (byte)100, this.Args.Split('-')[0],Utility.Bool2byte(globalVariables.isSuperAdmin));
                Utility.SetDataSourceForDataGridEx(grdList, m_dtPatient, true, true, "1=1", KcbDanhsachBenhnhan.Columns.IdBenhnhan + " desc");
                //Utility.SetMsg(lblTongSo, string.Format("&Tổng số bản ghi :{0}", m_dtPatient.Rows.Count), true);
                if (grdList.GetDataRows().Length <= 0)
                    m_dtDangkyPhongkham.Rows.Clear();
                UpdateGroup();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
            finally
            {
                ModifyCommand();
            }
        }
        void UpdateGroup()
        {
            try
            {
                var counts = m_dtPatient.AsEnumerable().GroupBy(x => x.Field<string>("ten_doituong_kcb"))
                    .Select(g => new { g.Key, Count = g.Count() });
                if (counts.Count() >= 2)
                {
                    if (grdList.RootTable.Groups.Count <= 0)
                    {
                        GridEXColumn gridExColumn = grdList.RootTable.Columns["ten_doituong_kcb"];
                        var gridExGroup = new GridEXGroup(gridExColumn);
                        gridExGroup.GroupPrefix = "Nhóm đối tượng KCB: ";
                        grdList.RootTable.Groups.Add(gridExGroup);
                    }
                }
                else
                {
                    GridEXColumn gridExColumn = grdList.RootTable.Columns["ten_doituong_kcb"];
                    var gridExGroup = new GridEXGroup(gridExColumn);
                    grdList.RootTable.Groups.Clear();
                }
                grdList.UpdateData();
                grdList.Refresh();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        /// <summary>
        /// hàm thực hiện viecj tìm kiếm thông tin 
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdTimKiem_Click(object sender, EventArgs e)
        {
            TimKiemThongTin(true);
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
        private bool AllowTextChanged;
        private bool AllowSelectionChanged=false;
        void NapThongtinDichvuKCB()
        {
            bool oldStatus = AllowTextChanged;
            try
            {
                cboKieuKham.DataSource = null;
                //Khởi tạo danh mục Loại khám
                string objecttype_code = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaDoituongKcb));
                string Id =cboGoi.Enabled && cboGoi.SelectedValue.ToString()!="-1"? cboGoi.SelectedValue.ToString().Split('_')[0]:"-1";
                m_dtDanhsachDichvuKCB = THU_VIEN_CHUNG.LayDsach_Dvu_KCB(objecttype_code, this.Args.Split('-')[1], -1, Utility.Int32Dbnull(cboGoi.Enabled ? Utility.Int32Dbnull(Id, -1) : -1, -1));
                Get_KIEUKHAM(objecttype_code);
                Get_PHONGKHAM(objecttype_code);
                AutocompleteMaDvu();
               // AutocompletePhongKham();
                AutocompleteKieuKham();
                m_dtDanhsachDichvuKCB.AcceptChanges();
                cboKieuKham.DataSource = m_dtDanhsachDichvuKCB;
                cboKieuKham.DataMember = DmucDichvukcb.Columns.IdDichvukcb;
                cboKieuKham.ValueMember = DmucDichvukcb.Columns.IdDichvukcb;
                cboKieuKham.DisplayMember = DmucDichvukcb.Columns.TenDichvukcb;
                //   cboKieuKham.Visible = globalVariables.UserName == "ADMIN";
                if (m_dtDanhsachDichvuKCB == null || m_dtDanhsachDichvuKCB.Columns.Count <= 0) return;
                AllowTextChanged = true;
                if (m_dtDanhsachDichvuKCB.Rows.Count == 1)
                {
                    cboKieuKham.SelectedIndex = 0;

                }
                else
                {
                    txtExamtypeCode.SetCode("-1");
                }
                AllowTextChanged = oldStatus;

            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }
        //private void Get_PHONGKHAM(string MA_DTUONG)
        //{
        //    m_PhongKham = THU_VIEN_CHUNG.Get_PHONGKHAM(MA_DTUONG);
        //}

        //private void Get_KIEUKHAM(string MA_DTUONG)
        //{
        //    m_kieuKham = THU_VIEN_CHUNG.Get_KIEUKHAM(MA_DTUONG, -1);
        //}
        private void Get_PHONGKHAM(string MA_DTUONG)
        {
            List<int> lstIdCongkham = (from p in m_dtDanhsachDichvuKCB.AsEnumerable()
                                       select Utility.Int32Dbnull(p.Field<int>(DmucDichvukcb.Columns.IdDichvukcb))).Distinct().ToList<int>();
            if (lstIdCongkham.Count <= 0) lstIdCongkham.Add(-1);
            m_PhongKham = new Select().From(DmucKieukham.Schema).Where(DmucKieukham.Columns.IdKieukham).In(lstIdCongkham).ExecuteDataSet().Tables[0];
        }

        private void Get_KIEUKHAM(string MA_DTUONG)
        {
            List<Int16> lstIdKieuKham = (from p in m_dtDanhsachDichvuKCB.AsEnumerable()
                                         select Utility.Int16Dbnull(p.Field<Int16>(DmucDichvukcb.Columns.IdKieukham))).Distinct().ToList<Int16>();
            if (lstIdKieuKham.Count <= 0) lstIdKieuKham.Add(-1);
            m_kieuKham = new Select().From(DmucKieukham.Schema).Where(DmucKieukham.Columns.IdKieukham).In(lstIdKieuKham).ExecuteDataSet().Tables[0];

        }
        bool blnChophepthanhtoan = false;
        void NapPhongkhamThiluc()
        {
            DataTable dtPKTL = SPs.BvmKcbLayphongdothiluc().GetDataSet().Tables[0];
            DataBinding.BindDataCombobox(cboPhongkhamThiluc, dtPKTL.Copy(), DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong);
        }
        /// <summary>
        /// hàm thực hiện việc load thông tin của tiếp đón lên
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_KCB_DSACH_BNHAN_Load(object sender, EventArgs e)
        {
            //ModifyCommand();
            //txtExamtypeCode.txtMyName_Edit = txtMyNameEdit;
            //blnChophepthanhtoan = THU_VIEN_CHUNG.Laygiatrithamsohethong("TIEPDON_CHOPHEPTHANHTOAN", "0", false) == "1";
            //DataTable dtNhomin = THU_VIEN_CHUNG.LayDulieuDanhmucChung("NHOM_INPHIEU_CLS", true);
            //DataBinding.BindDataCombox(cboServicePrint, dtNhomin, DmucChung.Columns.Ma, DmucChung.Columns.Ten, "Tất cả", true);
            //if (cboServicePrint.Items.Count > 0) cboServicePrint.SelectedIndex = 0;
            //AllowTextChanged = true;
            //LayDsach_DoituongKCB();
            //LayThongtinDVu_KCB();
            //NapPhongkhamThiluc();
            //cboTrangThai.SelectedIndex = 0;
            //TimKiemThongTin(true);
            //AutoloadSaveAndPrintConfig();
            //ModifyButtonCommandRegExam();
            //LoadLaserPrinters();
            //m_blnHasloaded = true;
        }
        private void LoadMe()
        {
            ModifyCommand();
            txtExamtypeCode.txtMyName_Edit = txtMyNameEdit;
            blnChophepthanhtoan = THU_VIEN_CHUNG.Laygiatrithamsohethong("TIEPDON_CHOPHEPTHANHTOAN", "0", false) == "1";
            DataTable dtNhomin = THU_VIEN_CHUNG.LayDulieuDanhmucChung("NHOM_INPHIEU_CLS", true);
            DataBinding.BindDataCombox(cboServicePrint, dtNhomin, DmucChung.Columns.Ma, DmucChung.Columns.Ten, "Tất cả", true);
            if (cboServicePrint.Items.Count > 0) cboServicePrint.SelectedIndex = 0;
            AllowTextChanged = true;
            LayDsach_DoituongKCB();
            LayThongtinDVu_KCB();
            NapPhongkhamThiluc();
            cboTrangThai.SelectedIndex = 0;
            TimKiemThongTin(true);
            AutoloadSaveAndPrintConfig();
            ModifyButtonCommandRegExam();
            LoadLaserPrinters();
            m_blnHasloaded = true;
        }
        private void LoadLaserPrinters()
        {
            if (string.IsNullOrEmpty(PropertyLib._MayInProperties.TenMayInPhieuKCB))
            {
                PropertyLib._MayInProperties.TenMayInPhieuKCB = Utility.GetDefaultPrinter();
                m_strDefaultLazerPrinterName = Utility.sDbnull(PropertyLib._MayInProperties.TenMayInPhieuKCB);
            }
            if (PropertyLib._KCBProperties != null)
            {
                try
                {
                    //khoi tao may in
                    String pkInstalledPrinters;
                    cboLaserPrinters.Items.Clear();
                    for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
                    {
                        pkInstalledPrinters = PrinterSettings.InstalledPrinters[i];
                        cboLaserPrinters.Items.Add(pkInstalledPrinters);
                    }
                }
                catch (Exception exception)
                {
                    Utility.ShowMsg("Lỗi:" + exception.Message);
                }
                finally
                {
                    m_strDefaultLazerPrinterName = Utility.sDbnull(PropertyLib._MayInProperties.TenMayInPhieuKCB);
                    cboLaserPrinters.Text = m_strDefaultLazerPrinterName;
                }
            }
            if (File.Exists(Application.StartupPath + "\\CAUHINH\\chkintachphieu.txt"))
            {
                chkIntach.Checked =
                    Convert.ToInt16(File.ReadAllText(Application.StartupPath + "\\CAUHINH\\chkintachphieu.txt")) ==
                    1
                        ? true
                        : false;
            }
        }
        private void LayThongtinDVu_KCB()
        {
            try
            {
                m_dtDanhsachDichvuKCB = THU_VIEN_CHUNG.LayDsach_Dvu_KCB("ALL", this.Args.Split('-')[1], -1, -1);
                m_kieuKham = THU_VIEN_CHUNG.Get_KIEUKHAM("ALL", -1);
                m_PhongKham = THU_VIEN_CHUNG.Get_PHONGKHAM("ALL");
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:" + exception.Message);
            }
        }
        private void AutocompleteMaDvu()
        {
            DataRow[] arrDr = null;
            try
            {
                if (m_dtDanhsachDichvuKCB == null) return;
                if (!m_dtDanhsachDichvuKCB.Columns.Contains("ShortCut"))
                    m_dtDanhsachDichvuKCB.Columns.Add(new DataColumn("ShortCut", typeof(string)));
                arrDr = m_dtDanhsachDichvuKCB.Select(DmucDoituongkcb.Columns.MaDoituongKcb + "='ALL' OR " + DmucDoituongkcb.Columns.MaDoituongKcb + "='" + MA_DTUONG + "'");
                if (arrDr.Length <= 0)
                {
                    this.txtExamtypeCode.AutoCompleteList = new List<string>();
                    return;
                }
                txtExamtypeCode.Init(m_dtDanhsachDichvuKCB, new List<string>() { DmucDichvukcb.Columns.IdDichvukcb, DmucDichvukcb.Columns.MaDichvukcb, DmucDichvukcb.Columns.TenDichvukcb });
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }


        private void AutocompletePhongKham()
        {
            try
            {
                if (m_PhongKham == null) return;
                if (!m_PhongKham.Columns.Contains("ShortCut"))
                    m_PhongKham.Columns.Add(new DataColumn("ShortCut", typeof(string)));
                txtPhongkham.Init(m_PhongKham, new List<string>() { DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong });
            }
            catch
            {
            }
            finally
            {

            }
        }

        private void AutocompleteKieuKham()
        {
            try
            {
                if (m_kieuKham == null) return;
                if (!m_kieuKham.Columns.Contains("ShortCut"))
                    m_kieuKham.Columns.Add(new DataColumn("ShortCut", typeof(string)));
                txtKieuKham.Init(m_kieuKham, new List<string>() { DmucKieukham.Columns.IdKieukham, DmucKieukham.Columns.MaKieukham, DmucKieukham.Columns.TenKieukham });
                txtKieuKham.RaiseEnterEvents();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }
        private void LayDsach_DoituongKCB()
        {
            DataBinding.BindDataCombobox(cboObjectType, globalVariables.gv_dtDoituong,
                                       DmucDoituongkcb.Columns.IdDoituongKcb, DmucDoituongkcb.Columns.TenDoituongKcb, "---Chọn đối tượng---", true);
        }

        private void LoadChiDinh()
        {
            AllowSelectionChanged = false;
            string MaLuotkham = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
            int Patient_ID = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
            m_dtChiDinhCLS = _KCB_DANGKY.LayChiDinhCLS_KhongKham(MaLuotkham, Patient_ID, 200);
            Utility.SetDataSourceForDataGridEx(grdAssignDetail, m_dtChiDinhCLS, false, true, "1=1", "");
            UpdateWhanChanged();
            ModifycommandAssignDetail();
            AllowSelectionChanged = true;
        }
        private DataTable m_dtDangkyPhongkham = new DataTable();
        private void LayDanhsachCongkham()
        {
            AllowCongkham_SelectionChanged = false;
            string MaLuotkham = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
            int Patient_ID = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
            m_dtDangkyPhongkham = _KCB_DANGKY.LayDsachCongkhamDadangki(MaLuotkham, Patient_ID,0);
            Utility.SetDataSourceForDataGridEx(grdRegExam, m_dtDangkyPhongkham, false, true, "", "" );
            AllowCongkham_SelectionChanged = true;

        }
        private void UpdateWhanChanged()
        {
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdAssignDetail.GetDataRows())
            {
                if (gridExRow.RowType == RowType.Record)
                {
                    gridExRow.BeginEdit();
                    gridExRow.Cells["TT"].Value =
                        Utility.DecimaltoDbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.DonGia].Value, 0) *
                        Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.SoLuong].Value);

                }
            }
            grdList.UpdateData();
            m_dtChiDinhCLS.AcceptChanges();
            UpdateSumOfChiDinh();
        }

        private void UpdateSumOfChiDinh()
        {
            Janus.Windows.GridEX.GridEXColumn gridExColumn = grdAssignDetail.RootTable.Columns["TT"];
            Janus.Windows.GridEX.GridEXColumn gridExColumnPhuThu = grdAssignDetail.RootTable.Columns[KcbChidinhclsChitiet.Columns.PhuThu];
            decimal Thanhtien = Utility.DecimaltoDbnull(grdAssignDetail.GetTotal(gridExColumn, AggregateFunction.Sum));
            decimal phuthu = Utility.DecimaltoDbnull(grdAssignDetail.GetTotal(gridExColumnPhuThu, AggregateFunction.Sum));

            txtTongChiPhi.Text = Utility.sDbnull(Thanhtien);// + phuthu);
        }
        string MA_DTUONG = "DV";
        private void grdList_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    if (m_dtDangkyPhongkham != null) m_dtDangkyPhongkham.Clear();
                    objLuotkham = null;
                    objBenhnhan = null;
                    objDoituongKCB = null;
                    return;
                }
                if (grdList.CurrentRow != null)
                {
                    objLuotkham = CreatePatientExam();
                    if (objLuotkham != null)
                        objBenhnhan = KcbDanhsachBenhnhan.FetchByID(objLuotkham.IdBenhnhan);
                    MA_DTUONG = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaDoituongKcb));
                    objDoituongKCB = new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.MaDoituongKcbColumn).IsEqualTo(MA_DTUONG).ExecuteSingle<DmucDoituongkcb>();
                    txtKieuKham._Text = "";
                    txtPhongkham._Text = "";
                    txtIDKieuKham.Text = "-1";
                    txtIDPkham.Text = "-1";
                    cboKieuKham.Text = "CHỌN PHÒNG KHÁM";
                    cboKieuKham.SelectedIndex = -1;
                    LoadthongtinGoiKham();
                    NapThongtinDichvuKCB();
                    cboKieuKham_SelectedIndexChanged(cboKieuKham, new EventArgs());
                    LayDanhsachCongkham();
                    LoadChiDinh();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
            finally
            {
                ModifyButtonCommandRegExam();
                ModifyCommand();
            }
        }
        /// <summary>
        /// hàm thực hiện việc cáu hình trạng thái của nút
        /// </summary>
        private void ModifyCommand()
        {
            cmdSuaThongTinBN.Enabled =
           cmdXoaBenhNhan.Enabled =
           cmdThemLanKham.Enabled = mnuFingerRegister.Enabled = cmdRegisterFinger.Enabled = cmdXemLichsuKCB.Enabled = Utility.isValidGrid(grdList);
            plnAddDvuKCB.Enabled = Utility.isValidGrid(grdList);
            ModifyUpDownButton();
        }
        /// <summary>
        /// hàm thực hiện viecj nhận formating trên lưới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdAssignDetail_FormattingRow(object sender, RowLoadEventArgs e)
        {

            if (e.Row.RowType == RowType.TotalRow)
            {
                e.Row.Cells[DmucDichvuclsChitiet.Columns.TenChitietdichvu].Value = "Tổng cộng :";
            }
        }
        private void frm_KCB_DSACH_BNHAN_FormClosing(object sender, FormClosingEventArgs e)
        {
            Utility.SaveValue2File(SplitterPath, splitContainer1.SplitterDistance.ToString());
        }

        private void txtTongChiPhi_TextChanged(object sender, EventArgs e)
        {
            Utility.FormatCurrencyHIS(txtTongChiPhi);
        }

        private void cboKieuKham_SelectedIndexChanged(object sender, EventArgs e)
        {

            //LG
        }
        DmucDoituongkcb objDoituongKCB = null;
        private KcbDangkyKcb CreateNewRegExam()
        {
            bool b_HasKham = false;
            var query = from phong in m_dtDangkyPhongkham.AsEnumerable().Cast<DataRow>()
                        where
                            Utility.Int32Dbnull(phong[KcbDangkyKcb.Columns.IdDichvuKcb], -100) ==
                            Utility.Int32Dbnull(cboKieuKham.Value, -1)
                        select phong;
            if (query.Count() > 0)
            {
                Utility.ShowMsg("Bệnh nhân đã đăng ký dịch vụ khám này. Đề nghị bạn xem lại");
                b_HasKham = true;
            }
            else
            {
                b_HasKham = false;
            }

            if (!b_HasKham)
            {
                int min = 1;
                int max = 255;
                //List<int> levels = m_dtDangkyPhongkham.AsEnumerable().Select(al => al.Field<int>(KcbDangkyKcb.Columns.SttTt37)).Distinct().ToList();
                // min = levels.Min();
                // max = levels.Max();
                ////Hoặc
                 min = Convert.ToInt32(m_dtDangkyPhongkham.AsEnumerable()
                        .Min(row => row[KcbDangkyKcb.Columns.SttTt37]));
                 max = Convert.ToInt32(m_dtDangkyPhongkham.AsEnumerable()
                        .Max(row => row[KcbDangkyKcb.Columns.SttTt37]));

                KcbDangkyKcb objCongkham = new KcbDangkyKcb();
                DmucDichvukcb objDichvuKCB = DmucDichvukcb.FetchByID(Utility.Int32Dbnull(cboKieuKham.Value));
                DmucKhoaphong objdepartment = new Select().From(DmucKhoaphong.Schema).Where(DmucKhoaphong.IdKhoaphongColumn).IsEqualTo(objDichvuKCB.IdKhoaphong).ExecuteSingle<DmucKhoaphong>();
                objDoituongKCB = new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.MaDoituongKcbColumn).IsEqualTo(MA_DTUONG).ExecuteSingle<DmucDoituongkcb>();
                if (objDichvuKCB != null)
                {
                    int id_goi = -1;
                    int id_dangkygoi = -1;
                    if (cboGoi.Enabled && cboGoi.SelectedValue.ToString() != "-1")
                    {
                        id_dangkygoi = Utility.Int32Dbnull(cboGoi.SelectedValue.ToString().Split('_')[0], -1);
                        id_goi = Utility.Int32Dbnull(cboGoi.SelectedValue.ToString().Split('_')[1], -1);
                    }
                    string MaLuotkham = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
                    int Patient_ID = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan), -1);
                    int dungtuyen = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.DungTuyen), 0);
                    objCongkham.IdDichvuKcb = Utility.Int16Dbnull(objDichvuKCB.IdDichvukcb, -1);
                    objCongkham.IdKieukham = objDichvuKCB.IdKieukham;
                    objCongkham.NhomBaocao = objDichvuKCB.NhomBaocao;
                    objCongkham.LaPhidichvukemtheo = 0;
                    objCongkham.SttKham = -1;
                    objCongkham.IdCha = -1;
                    objCongkham.IdGoi = id_goi;
                    objCongkham.IdDangky = id_dangkygoi;
                    if (min > 1)
                        objCongkham.SttTt37 =Utility.ByteDbnull( min - 1);
                    else
                        objCongkham.SttTt37 = Utility.ByteDbnull(max + 1);
                    objCongkham.TyleTt = THU_VIEN_CHUNG.Bhyt_Laytyle_tt_congkham(THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb), objCongkham.SttTt37);
                    objCongkham.IdKhoakcb = objDichvuKCB.IdKhoaphong;
                    if (objLuotkham != null)
                    {
                        objCongkham.MadoituongGia = objLuotkham.MadoituongGia;
                    }

                    objCongkham.DonGia = THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb) ? Utility.DecimaltoDbnull(objDichvuKCB.DonGia, 0) : Utility.DecimaltoDbnull(objDichvuKCB.DonGia, 0) * (1 + Utility.DecimaltoDbnull(objDoituongKCB.MotaThem, 0) / 100);
                    objCongkham.NguoiTao = globalVariables.UserName;
                    if (objdepartment != null)
                    {

                        objCongkham.MaPhongStt = objdepartment.MaPhongStt;

                    }
                    if (objDoituongKCB != null)
                    {
                        objCongkham.IdLoaidoituongkcb = objDoituongKCB.IdLoaidoituongKcb;
                        objCongkham.MaDoituongkcb = objDoituongKCB.MaDoituongKcb;
                        objCongkham.IdDoituongkcb = objDoituongKCB.IdDoituongKcb;
                    }
                    if (Utility.Int16Dbnull(objDichvuKCB.IdPhongkham, -1) > -1)
                        objCongkham.IdPhongkham = Utility.Int16Dbnull(objDichvuKCB.IdPhongkham, -1);
                    else
                        objCongkham.IdPhongkham = Utility.Int16Dbnull(txtIDPkham.Text, -1);

                    if (Utility.Int32Dbnull(objDichvuKCB.IdBacsy) > 0)
                        objCongkham.IdBacsikham = Utility.Int16Dbnull(objDichvuKCB.IdBacsy);
                    //else
                    //{
                    //    objCongkham.IdBacsikham = globalVariables.gv_intIDNhanvien;
                    //}
                    objCongkham.PhuThu = THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb) ? (dungtuyen == 1 ? Utility.DecimaltoDbnull(objDichvuKCB.PhuthuDungtuyen) : Utility.DecimaltoDbnull(objDichvuKCB.PhuthuTraituyen)) : 0m;
                    objCongkham.NgayDangky = globalVariables.SysDate;
                    objCongkham.IdBenhnhan = Patient_ID;
                    objCongkham.TrangthaiThanhtoan = 0;
                    objCongkham.TrangthaiHuy = 0;
                    objCongkham.Noitru = 0;
                    objCongkham.TrangthaiIn = 0;
                    objCongkham.TuTuc = !THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb) ? (byte)0 : Utility.ByteDbnull(objDichvuKCB.TuTuc, 0);//Đối tượng dịch vụ thì tự túc luôn =0
                    if (THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb) && !Utility.Byte2Bool(objLuotkham.DungTuyen) && THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_TRAITUYENNGOAITRU_GIADICHVU", false) == "1")
                        objCongkham.TuTuc = 1;
                    objCongkham.MaKhoaThuchien = objdepartment.MaKhoaphong;// globalVariables.MA_KHOA_THIEN;
                    objCongkham.TenDichvuKcb = objDichvuKCB.TenDichvukcb;
                    objCongkham.NgayTiepdon = globalVariables.SysDate;
                    objCongkham.IpMaytao = globalVariables.gv_strIPAddress;
                    objCongkham.TenMaytao = globalVariables.gv_strComputerName;
                    objCongkham.MaLuotkham = MaLuotkham;
                    if (THU_VIEN_CHUNG.IsNgoaiGio() && !THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb))
                    {
                        objCongkham.KhamNgoaigio = 1;
                        objCongkham.DonGia = Utility.DecimaltoDbnull(objDichvuKCB.DongiaNgoaigio, 0);
                        objCongkham.PhuThu = Utility.Byte2Bool(objLuotkham.DungTuyen) ? Utility.DecimaltoDbnull(objDichvuKCB.PhuthuNgoaigio, 0) : Utility.DecimaltoDbnull(objDichvuKCB.PhuthuDungtuyen);
                    }
                    else
                    {
                        objCongkham.KhamNgoaigio = 0;
                    }
                    objCongkham.TinhChiphi = 1;
                }
                else
                {
                    objCongkham = null;
                }
                ////240722 Bỏ do đã tự động tính bằng hàm max,min phía trên
                //if (THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb) && m_dtDangkyPhongkham.Rows.Count > 0)
                //{
                //    THU_VIEN_CHUNG.TinhlaiGiaChiphiKcb(m_dtDangkyPhongkham, ref objCongkham);
                //}

                return objCongkham;
            }
            return null;
        }
        KCB_DANGKY _KCB_DANGKY = new KCB_DANGKY();
        void ProcessData()
        {
            long id_kham = -1;
            if (objLuotkham == null) objLuotkham = CreatePatientExam();
            if (objLuotkham != null)
            {
                KcbDangkyKcb objCongkham = CreateNewRegExam();
                if (objCongkham != null)
                {
                    objCongkham.MaLuotkham = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
                    objCongkham.IdBenhnhan = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
                 
                    ActionResult actionResult = _KCB_DANGKY.InsertRegExam(objCongkham, objLuotkham, ref id_kham, Utility.Int32Dbnull(cboKieuKham.Value));
                    if (actionResult == ActionResult.Success)
                    {
                        Utility.Log(this.Name, globalVariables.UserName, string.Format("Thêm công khám {0} cho bệnh nhân ID={1}, PID={2}, Tên={3} thành công",objCongkham.TenDichvuKcb, Utility.getValueOfGridCell(grdList, "id_benhnhan"), Utility.getValueOfGridCell(grdList, "ma_luotkham"), Utility.getValueOfGridCell(grdList, "ten_benhnhan")), newaction.Add, this.GetType().Assembly.ManifestModule.Name);
                        if (m_dtDangkyPhongkham != null)
                        {
                            LayDanhsachCongkham();
                            Utility.GonewRowJanus(grdRegExam, KcbDangkyKcb.Columns.IdKham, id_kham.ToString());
                            cmdInPhieuKham.Focus();
                        }
                        //Reset cac thong tin chi dinh phong kham
                        cboKieuKham.Text = "CHỌN PHÒNG KHÁM";
                        cboKieuKham.SelectedIndex = -1;
                        txtKieuKham.SetCode("-1");
                        txtPhongkham.SetCode("-1");
                        txtIDPkham.Text = "-1";
                        txtIDKieuKham.Text = "-1";
                        txtExamtypeCode.SetCode("-1");
                        m_dtDangkyPhongkham.AcceptChanges();
                        if (themcongkhamtronggoi)
                            cboGoi_SelectedIndexChanged(cboGoi, new EventArgs());
                    }
                    if (actionResult == ActionResult.Error)
                    {
                        Utility.ShowMsg("Lỗi trong quá trình cập nhập thông tin ", "Thông báo lỗi", MessageBoxIcon.Error);
                        return;
                    }
                }
            }
        }
        bool themcongkhamtronggoi = false;
        private void cmdAddDvuKCB_Click(object sender, EventArgs e)
        {
            try
            {
                themcongkhamtronggoi = false;
                Utility.Log(this.Name, globalVariables.UserName, string.Format("Nhấn nút thêm công khám cho bệnh nhân ID={0}, PID={1}, Tên={2}", Utility.getValueOfGridCell(grdList, "id_benhnhan"), Utility.getValueOfGridCell(grdList, "ma_luotkham"), Utility.getValueOfGridCell(grdList, "ten_benhnhan")), newaction.Add, this.GetType().Assembly.ManifestModule.Name);

                objLuotkham = CreatePatientExam();
                if (objLuotkham == null) 
                {
                    Utility.ShowMsg("Vui lòng chọn Lượt khám trước khi thêm công khám mới");
                    return;
                }
                if (objLuotkham.NgayTiepdon.Date != DateTime.Now.Date)
                {
                    Utility.ShowMsg(string.Format("Người bệnh đăng ký lượt khám vào ngày {0} nên hệ thống chỉ cho phép thêm công khám trong ngày {1}", objLuotkham.NgayTiepdon.ToString("dd/MM/yyyy"), objLuotkham.NgayTiepdon.ToString("dd/MM/yyyy")));
                    return;
                }
                if (!PropertyLib._KCBProperties.GoMaDvu)
                    AutoselectcongkhambyKieukham_Phongkham();
                if (Utility.Int32Dbnull(cboKieuKham.Value) <= -1)
                {
                    Utility.ShowMsg("Bạn phải chọn Công khám cần thêm cho bệnh nhân", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                DmucDichvukcb objDichvuKCB =
                       DmucDichvukcb.FetchByID(Utility.Int32Dbnull(cboKieuKham.Value));
                //Kiểm tra các dịch vụ trong gói xem số lượng có > số lượng còn lại hay không. Có thể do nhiều BS cùng kê
                if(cboGoi.Enabled && cboGoi.SelectedValue.ToString() != "-1" )//? ;
                {
                    int id_dangky = Utility.Int32Dbnull(cboGoi.SelectedValue.ToString().Split('_')[0] , -1);
                    int so_luong_ke = 1;
                    if (id_dangky > 0)
                    {
                        themcongkhamtronggoi = true;//Để load lại công khám trong gói sau khi thêm thành công
                        GoiTinhtrangsudung goittsd = GoiTinhtrangsudung.FetchByID(id_dangky);
                        if (goittsd.SoluongDung < so_luong_ke)
                        {
                            string msg = string.Format("Dịch vụ trong gói : {0} đang có số lượng chỉ định: {1} nhiều hơn số lượng khả dụng: {2}. Đề nghị chỉnh lại số lượng chỉ định phù hợp", objDichvuKCB.TenDichvukcb, so_luong_ke, goittsd.SoluongDung);
                            Utility.ShowMsg(msg);
                            return ;
                        }
                    }
                }
                
                if (objDichvuKCB != null)
                {
                    Utility.SetMsg(lblDonGia, Utility.sDbnull(objDichvuKCB.DonGia), true);
                    Utility.SetMsg(lblPhuThu, Utility.sDbnull(Utility.Int32Dbnull(objLuotkham.DungTuyen.Value, 0) == 1 ? objDichvuKCB.PhuthuDungtuyen : objDichvuKCB.PhuthuTraituyen), true);
                    if ((m_dtDangkyPhongkham.Select(KcbDangkyKcb.Columns.IdPhongkham + "=" + objDichvuKCB.IdPhongkham + "").GetLength(0) <= 0))
                    {
                        ProcessData();
                    }
                    else
                    {
                        if (Utility.AcceptQuestion("Bệnh nhân đã được đăng ký khám chữa bệnh tại phòng khám này. Bạn có muốn tiếp tục đăng ký dịch vụ KCB mới vừa chọn hay không?", "Thông báo", true))
                        {
                            ProcessData();
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            
        }
        private KcbLuotkham CreatePatientExam()
        {
            if (!Utility.isValidGrid(grdList)) return null;
            string MaLuotkham = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
            int Patient_ID = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
            KcbLuotkham objLuotkham = new KcbLuotkham();

            SqlQuery sqlQuery = new Select().From(KcbLuotkham.Schema)
                .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(MaLuotkham)
                .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(Patient_ID);
            objLuotkham = sqlQuery.ExecuteSingle<KcbLuotkham>();
            return objLuotkham;


        }
        private KcbDanhsachBenhnhan CreatePatientInfo()
        {
            int Patient_ID = Utility.Int32Dbnull(grdList.GetValue(KcbDanhsachBenhnhan.Columns.IdBenhnhan));
            KcbDanhsachBenhnhan objBenhnhan = new KcbDanhsachBenhnhan();

            SqlQuery sqlQuery = new Select().From(KcbDanhsachBenhnhan.Schema)
                .Where(KcbDanhsachBenhnhan.Columns.IdBenhnhan).IsEqualTo(Patient_ID);

            objBenhnhan = sqlQuery.ExecuteSingle<KcbDanhsachBenhnhan>();
            return objBenhnhan;
        }
        private void ModifycommandAssignDetail()
        {
            try
            {
                cmdSuaChiDinh.Enabled = Utility.isValidGrid(grdAssignDetail);
                cmdXoaChiDinh.Enabled = Utility.isValidGrid(grdAssignDetail);
                cmdPrintAssign.Enabled = Utility.isValidGrid(grdAssignDetail);
                chkIntach.Enabled = cmdPrintAssign.Enabled;
                cboServicePrint.Enabled = cmdPrintAssign.Enabled;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
            //cmdSend.Enabled = grdAssignInfo.RowCount > 0;
        }

        private void cmdThemChiDinh_Click(object sender, EventArgs e)
        {
            KcbLuotkham objLuotkham = CreatePatientExam();
            KcbDanhsachBenhnhan objbenhnhan = CreatePatientInfo();
            if (objLuotkham != null)
            {
                frm_KCB_CHIDINH_CLS frm = new frm_KCB_CHIDINH_CLS("-GOI,-TIEN,-CHIPHITHEM", 0);
                frm.Exam_ID = -1;
                frm.tnv_chidinh = 1;
                frm.txtAssign_ID.Text = "-100";
                frm.objLuotkham = objLuotkham;
                frm.objBenhnhan = objbenhnhan;
                frm.txtidkham.Text = Utility.sDbnull(grdRegExam.GetValue("id_kham"), -1);
                //frm.objCongkham = KcbDangkyKcb.FetchByID(Utility.Int32Dbnull(grdRegExam.GetValue("id_kham"), -1));
                frm.m_eAction = action.Insert;
                frm.noitru = 0;
                frm.ShowDialog();
                if (!frm.m_blnCancel)
                {
                    LoadChiDinh();
                    UpdateSumOfChiDinh();
                }
                ModifycommandAssignDetail();
            }
            ModifyCommand();
        }
        private bool InValiUpdateChiDinh()
        {
            int Assign_ID = Utility.Int32Dbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh), "-1");
            SqlQuery sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                .Where(KcbChidinhclsChitiet.Columns.IdChidinh).IsEqualTo(Assign_ID)
                .And(KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan).IsGreaterThanOrEqualTo(1);
            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Phiếu chỉ định này đã thanh toán, Bạn không được phép sửa(Có thể liên hệ với Quầy thanh toán để hủy thanh toán trước khi sửa lại)", "Thông báo");
                cmdThemChiDinh.Focus();
                return false;
            }
            return true;
        }
        private void cmdSuaChiDinh_Click(object sender, EventArgs e)
        {
            try
            {
                KcbLuotkham objLuotkham = CreatePatientExam();
                KcbDanhsachBenhnhan objbenhnhan = CreatePatientInfo();
                if (objLuotkham != null)
                {
                    if (!InValiUpdateChiDinh()) return;
                    frm_KCB_CHIDINH_CLS frm = new frm_KCB_CHIDINH_CLS("-GOI,-TIEN", 0);
                    frm.noitru = 0;
                    frm.tnv_chidinh = 1;
                    frm.Exam_ID = Utility.Int32Dbnull(-1, -1);
                    frm.objLuotkham = CreatePatientExam();
                    frm.objBenhnhan = objbenhnhan;
                    frm.m_eAction = action.Update;
                    frm.txtAssign_ID.Text = Utility.sDbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh), "-1");
                    frm.ShowDialog();
                    if (!frm.m_blnCancel)
                    {
                        //  LoadChiDinhCLS();
                        LoadChiDinh();
                        UpdateSumOfChiDinh();
                    }
                    ModifycommandAssignDetail();
                }
                ModifyCommand();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }

        }
        private bool isValidChidinh()
        {
            bool b_Cancel = false;
            if (grdAssignDetail.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn một bản ghi thực hiện xóa chỉ định CLS", "Thông báo",
                                MessageBoxIcon.Warning);
                grdAssignDetail.Focus();
                return false;
            }
            if (Utility.Coquyen("quyen_suaphieuchidinhcls") || globalVariables.IsAdmin || globalVariables.isSuperAdmin)
            {
            }
            else
            {
                foreach (GridEXRow gridExRow in grdAssignDetail.GetCheckedRows())
                {
                    int AssignDetail_ID =
                        Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value, -1);
                    SqlQuery sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                        .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(AssignDetail_ID)
                        .And(KcbChidinhclsChitiet.Columns.NguoiTao).IsNotEqualTo(globalVariables.UserName);
                    if (sqlQuery.GetRecordCount() > 0)
                    {
                        Utility.ShowMsg("Trong các chỉ định bạn chọn xóa, có một số chỉ định được kê bởi Bác sĩ khác nên bạn không được phép xóa. Mời bạn chọn lại chỉ các chỉ định do chính bạn kê để thực hiện xóa");
                        return false;
                        break;
                    }
                }
            }
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdAssignDetail.GetCheckedRows())
            {
                int AssignDetail_ID = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value,
                                                          -1);
                SqlQuery sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                    .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(AssignDetail_ID)
                    .And(KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(1);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    b_Cancel = true;
                    break;
                }
            }
            if (b_Cancel)
            {
                Utility.ShowMsg("Chỉ định bạn chọn đã được thanh toán nên bạn không thể xóa. Đề nghị kiểm tra lại");
                return false;

            }
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdAssignDetail.GetCheckedRows())
            {
                int AssignDetail_ID = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value, -1);
                SqlQuery sqlQuery = new Select().From(KcbChidinhclsChitiet.Schema)
                    .Where(KcbChidinhclsChitiet.Columns.IdChitietchidinh).IsEqualTo(AssignDetail_ID)
                    .And(KcbChidinhclsChitiet.Columns.TrangThai).IsGreaterThanOrEqualTo(1);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    b_Cancel = true;
                    break;
                }
            }
            if (b_Cancel)
            {
                Utility.ShowMsg("Chỉ định bạn chọn đã được chuyển làm cận lâm sàng hoặc đã có kết quả nên không thể xóa. Đề nghị kiểm tra lại");
                return false;
            }
            return true;
        }

        private void PerforActionDeleteAssign()
        {
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdAssignDetail.GetCheckedRows())
            {
                int AssignDetail_ID = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChitietchidinh].Value,
                                                          -1);
                int Assign_ID = Utility.Int32Dbnull(gridExRow.Cells[KcbChidinhclsChitiet.Columns.IdChidinh].Value,
                                                    -1);

                _KCB_CHIDINH_CANLAMSANG.XoaChiDinhCLSChitiet(AssignDetail_ID, Assign_ID);
                gridExRow.Delete();
                m_dtChiDinhCLS.AcceptChanges();
            }
            UpdateSumOfChiDinh();
        }

        private void cmdXoaChiDinh_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isValidChidinh()) return;
                var query = (from chidinh in grdAssignDetail.GetCheckedRows().AsEnumerable()
                             let x = Utility.sDbnull(chidinh.Cells[DmucDichvuclsChitiet.Columns.TenChitietdichvu].Value)
                             select x).ToArray();
                string ServiceDetail_Name = string.Join("; ", query);
                string Question = string.Format("Bạn có muốn xóa thông tin chỉ định {0} \r\n đang chọn không", ServiceDetail_Name);
                if (Utility.AcceptQuestion(Question, "Thông báo", true))
                {
                    PerforActionDeleteAssign();
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Xóa chỉ định của bệnh nhân ID={0}, PID={1}, Tên={2}, chỉ định bị xóa={3} thành công", Utility.getValueOfGridCell(grdList, "id_benhnhan"), Utility.getValueOfGridCell(grdList, "ma_luotkham"), Utility.getValueOfGridCell(grdList, "ten_benhnhan"), ServiceDetail_Name), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                    //ModifyCommmand();
                    ModifycommandAssignDetail();
                }

            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }


        }

        private void grdAssignDetail_ColumnHeaderClick(object sender, ColumnActionEventArgs e)
        {
            ModifycommandAssignDetail();
        }

        private void grdAssignDetail_CellValueChanged(object sender, ColumnActionEventArgs e)
        {
            ModifycommandAssignDetail();
        }

        private void txtTongChiPhiKham_TextChanged(object sender, EventArgs e)
        {
            //Utility.FormatCurrencyHIS(txtTongChiPhiKham);
        }
        public  void OpenForm2Tab(Janus.Windows.UI.Tab.UITabPage tp, Form f)
        {
            uiTabPageUpdate.SuspendLayout();
            if (!uiTabPageUpdate.TabVisible) uiTabPageUpdate.TabVisible = true;
            f.TopLevel = false;
            //no border if needed
            f.FormBorderStyle = FormBorderStyle.None;
            f.AutoScaleMode = AutoScaleMode.Dpi;
            uiTabPageUpdate.Controls.Clear();
            if (!tp.Controls.Contains(f) && tp.Controls.Count<=0)
            {
                tp.Controls.Add(f);
                f.Dock = DockStyle.Fill;
                f.Show();
                Refresh();
                uiTabPageUpdate.Selected = true;
            }
            Refresh();
            uiTabPageUpdate.ResumeLayout();
        }

        private void cmdThemMoiBN_Click(object sender, EventArgs e)
        {
            try
            {
                frm_KCB_DANGKY frm = new frm_KCB_DANGKY(this.Args);
                frm.m_enAct = action.Insert;
                frm.m_dtPatient = m_dtPatient;
                this.myTrace.FunctionID = globalVariables.FunctionID;
                this.myTrace.FunctionName = globalVariables.FunctionName;
                frm.myTrace = this.myTrace;
                frm._OnActionSuccess += frm__OnActionSuccess;
                frm._OnClose += Frm__OnClose;
                frm.grdList = grdList;
                if(THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_TIEPDON_OPEN2TAB","0",true)=="1")
                {
                    OpenForm2Tab(uiTabPageUpdate, frm);
                    
                }   
                else
                frm.ShowDialog();
                if (!frm.MBlnCancel)
                {
                    UpdateGroup();
                    grdList_SelectionChanged(grdList, new EventArgs());
                }
                ModifyCommand();
                ModifycommandAssignDetail();
                ModifyButtonCommandRegExam();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }

        }

        void frm__OnActionSuccess(string ma_luotkham,action en_act)
        {
            try
            {
                DataTable dt_temp = _KCB_DANGKY.KcbTiepdonTimkiemBenhnhan("01/01/1900", "01/01/1900", -1, -1, "", -1, ma_luotkham, "", new DateTime(1900, 1, 1), 100, "", globalVariables.MA_KHOA_THIEN, 0, (byte)100, this.Args.Split('-')[0],Utility.Bool2byte(globalVariables.isSuperAdmin));
                if (en_act == action.Delete)
                {
                   
                }
                if (en_act == action.Insert && m_dtPatient != null && m_dtPatient.Columns.Count > 0 && dt_temp.Rows.Count > 0)
                {
                    m_dtPatient.ImportRow(dt_temp.Rows[0]);
                    return;
                }
                if (en_act == action.Update && m_dtPatient != null && m_dtPatient.Columns.Count > 0 && dt_temp.Rows.Count > 0)
                {
                    DataRow[] arrDr = m_dtPatient.Select(string.Format("ma_luotkham='{0}'", ma_luotkham));
                    if (arrDr.Length > 0)
                    {
                        Utility.CopyData(dt_temp.Rows[0],ref arrDr[0]);
                    }
                    else
                        m_dtPatient.ImportRow(dt_temp.Rows[0]);

                }
                m_dtPatient.AcceptChanges();
                Utility.GotoNewRowJanus(grdList, "ma_luotkham", ma_luotkham);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                UpdateGroup();
                ModifyCommand();
            }
           
        }
        /// <summary>
        /// hàm thục hiện việc thêm lần khám
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdThemLanKham_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Bạn phải chọn bệnh nhân để thêm lượt khám mới");
                    return;
                }
               
                DataTable _temp = _KCB_DANGKY.KcbLaythongtinBenhnhan(Utility.Int64Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan)));
                if (_temp != null && Utility.ByteDbnull(_temp.Rows[0][KcbLuotkham.Columns.TrangthaiNoitru], 0) > 0 && Utility.ByteDbnull(_temp.Rows[0][KcbLuotkham.Columns.TrangthaiNoitru], 0) < 4)
                {
                    Utility.ShowMsg("Bệnh nhân đang ở trạng thái nội trú và chưa ra viện nên không thể thêm lần khám mới. Đề nghị bạn xem lại");
                    return;
                }
                if (ChuaKetthuckhamhet())
                    return;
                frm_KCB_DANGKY frm = new frm_KCB_DANGKY(this.Args);
                frm.txtIdBenhnhan.Text = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
                frm.txtMaLankham.Text = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
                frm.txtIdBenhnhan.Enabled = false;
                frm._mabenhnhan = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
                frm.Maluotkham = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
                this.myTrace.FunctionID = globalVariables.FunctionID;
                this.myTrace.FunctionName = globalVariables.FunctionName;
                frm.myTrace = this.myTrace;
                frm.m_enAct = action.Add;
                frm._OnActionSuccess += frm__OnActionSuccess;
                frm._OnClose += Frm__OnClose;
                frm.m_dtPatient = m_dtPatient;
                frm.grdList = grdList;
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_TIEPDON_OPEN2TAB", "0", true) == "1")
                {
                    OpenForm2Tab(uiTabPageUpdate, frm);
                }
                else
                    frm.ShowDialog();
                if (!frm.MBlnCancel)
                {
                    UpdateGroup();
                    grdList_SelectionChanged(grdList, new EventArgs());
                    
                }
                ModifyCommand();
                ModifycommandAssignDetail();
                ModifyButtonCommandRegExam();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }
        bool ChuaKetthuckhamhet()
        {
            try
            {
                KcbDangkyKcbCollection lstDangkyKCB = new Select().From(KcbDangkyKcb.Schema).
                    Where(KcbDangkyKcb.Columns.IdBenhnhan).IsEqualTo(Utility.Int64Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan), -1))
                    .And(KcbDangkyKcb.Columns.MaLuotkham).IsEqualTo(Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham), -1))
                    .AndExpression(KcbDangkyKcb.Columns.TrangThai).IsEqualTo(0).OrExpression(KcbDangkyKcb.Columns.TrangThai).IsNull().CloseExpression().ExecuteAsCollection<KcbDangkyKcbCollection>();
                if (lstDangkyKCB.Count > 0)
                {
                    string s = "";
                    var q = from p in lstDangkyKCB.AsEnumerable()
                            where p.NgayTiepdon.Value.ToString("dd/MM/yyyy") == DateTime.Now.ToString("dd/MM/yyyy")
                            select string.Format("Ngày :{0}- Mã lượt khám: {1}", p.NgayDangky.Value.ToString("dd/MM/yyyy HH:mm:ss"), p.MaLuotkham);

                    if (q.Any())
                        s = string.Join("\r\n", q.ToArray<string>());
                    if (s.Length > 0)
                    {
                        Utility.ShowMsg(string.Format("Người bệnh {0} vẫn còn các lượt khám sau đăng ký trong ngày {1} mà chưa kết thúc\r\n{2}.\r\nDo vậy bạn chỉ có thể sửa thông tin mà không được phép thêm lượt khám", Utility.sDbnull(grdList.GetValue("ten_benhnhan")), DateTime.Now.ToString("dd/MM/yyyy"), s));
                        return true;
                    }
                    else
                        return false;
                }
                //Đợt khám nội trú lần khám trước vẫn chưa cho ra viện
                DataSet dsCheck = Utility.ExecuteSql("select ma_luotkham,ngay_tiepdon from kcb_luotkham where id_benhnhan=@id_benhnhan and trangthai_noitru>1 and trangthai_noitru<=2", CommandType.Text);
                if (dsCheck != null && dsCheck.Tables.Count > 0 && dsCheck.Tables[0].Rows.Count > 0)
                {
                    string msg = string.Format("Người bệnh còn lần khám {0} ngày {1} ở trạng thái nội trú và chưa kết thúc ra viện nên không thể thêm lần khám. Vui lòng kiểm tra lại", dsCheck.Tables[0].Rows[0]["ma_luotkham"].ToString(),Convert.ToDateTime( dsCheck.Tables[0].Rows[0]["ngay_tiepdon"]).ToString("dd/MM/yyyy HH:ss"));
                    Utility.ShowMsg(msg);
                    return false;
                }
                return false;

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                return false;
            }
        }
        /// <summary>
        /// hàm thực hiện sửa thông tin của bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSuaThongTinBN_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Bạn phải chọn ít nhất 1 bệnh nhân để sửa thông tin");
                    return;
                }
                SqlQuery sqlkiemtra = new Select().From(KcbThanhtoan.Schema).Where(KcbThanhtoan.Columns.MaLuotkham).IsEqualTo(Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham)));
                bool updateOnly = sqlkiemtra.GetRecordCount() > 0;
                //if ()
                //{
                //    frm_KCB_DANGKY_CAPNHAT frm = new frm_KCB_DANGKY_CAPNHAT(this.Args);
                //    frm.txtMaBN.Text = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
                //    frm.txtMaLankham.Text = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
                //    frm._mabenhnhan = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
                //    frm.Maluotkham = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
                //    frm.txtMaBN.Enabled = false;
                //    frm.txtMaLankham.Enabled = false;
                //    this.myTrace.FunctionID = globalVariables.FunctionID;
                //    this.myTrace.FunctionName = globalVariables.FunctionName;
                //    frm.myTrace = this.myTrace;
                //    frm._OnActionSuccess += frm__OnActionSuccess;
                //    frm.MEnAction = action.Update;
                //    frm.m_dtPatient = m_dtPatient;
                //    frm.grdList = grdList;
                //    frm.ShowDialog();
                //    if (!frm.MBlnCancel)
                //    {
                //        UpdateGroup();
                //        grdList_SelectionChanged(grdList, new EventArgs());

                //    }
                //}
                //else
                //{
                frm_KCB_DANGKY frm = new frm_KCB_DANGKY(this.Args);
                frm.updateonly = updateOnly;
                frm.txtIdBenhnhan.Text = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
                frm.txtMaLankham.Text = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
                frm._mabenhnhan = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
                frm.Maluotkham = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
                frm.txtIdBenhnhan.Enabled = false;
                frm.txtMaLankham.Enabled = false;
                this.myTrace.FunctionID = globalVariables.FunctionID;
                this.myTrace.FunctionName = globalVariables.FunctionName;
                frm.myTrace = this.myTrace;
                frm._OnActionSuccess += frm__OnActionSuccess;
                frm._OnClose += Frm__OnClose;
                frm.m_enAct = action.Update;
                frm.m_dtPatient = m_dtPatient;
                frm.grdList = grdList;
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_TIEPDON_OPEN2TAB", "0", true) == "1")
                {
                    OpenForm2Tab(uiTabPageUpdate, frm);
                }
                else
                    frm.ShowDialog();
                if (!frm.MBlnCancel)
                {
                    UpdateGroup();
                    grdList_SelectionChanged(grdList, new EventArgs());

                }
                //}
                ModifyCommand();
                ModifycommandAssignDetail();
                ModifyButtonCommandRegExam();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }

        }

        private void Frm__OnClose()
        {
            uiTabPageUpdate.TabVisible = false;
            Refresh();
        }
        #region "Thông tin khám chữa bệnh"
        private void cboKieuKham_TextChanged(object sender, EventArgs e)
        {
            string _rowFilter = "1=1";
            if (!string.IsNullOrEmpty(cboKieuKham.Text) && m_blnHasloaded)
            {


                cboKieuKham.DroppedDown = true;

            }
            else
            {

                cboKieuKham.DroppedDown = false;
            }


        }
        /// <summary>
        /// hàm thực hiện việc xóa thông tin khám 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdXoaCongkham_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdList))
            {
                Utility.ShowMsg("Bạn phải chọn ít nhất 1 bệnh nhân để xóa khám");
                return;
            }
            if (!IsValidData()) return;
            if (Utility.AcceptQuestion("Bạn có muốn thực hiện việc xóa công khám đang chọn không ?", "Thông báo", true))
            {
                HuyThamKham();
            }
            ModifyButtonCommandRegExam();
        }
        private void HuyThamKham()
        {

            if (grdRegExam.CurrentRow != null)
            {

                int id_kham = Utility.Int32Dbnull(grdRegExam.CurrentRow.Cells[KcbDangkyKcb.Columns.IdKham].Value, -1);

                //if (Utility.AcceptQuestion("Bạn muốn hủy dịch vụ khám đang chọn ", "Thông báo", true))
                //{

                    ActionResult actionResult = _KCB_DANGKY.PerformActionDeleteRegExam(id_kham);
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Xóa công khám của bệnh nhân ID={0}, PID={1}, Tên={2}, công khám={3} thành công", Utility.getValueOfGridCell(grdList, "id_benhnhan"), Utility.getValueOfGridCell(grdList, "ma_luotkham"), Utility.getValueOfGridCell(grdList, "ten_benhnhan"), Utility.getValueOfGridCell(grdRegExam, "ten_dichvukcb")), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                            DataRow[] arrDr = m_dtDangkyPhongkham.Select(KcbDangkyKcb.Columns.IdKham + "=" + id_kham + " OR  " + KcbDangkyKcb.IdChaColumn.ColumnName + "=" + id_kham);
                            if (arrDr.GetLength(0) > 0)
                            {
                                int _count = arrDr.Length;
                                List<string> lstregid = (from p in arrDr.AsEnumerable()
                                                         select p.Field<long>(KcbDangkyKcb.IdKhamColumn.ColumnName).ToString()
                                                      ).ToList<string>();
                                for (int i = 1; i <= _count; i++)
                                {
                                    DataRow[] tempt = m_dtDangkyPhongkham.Select(KcbDangkyKcb.Columns.IdKham + "=" + lstregid[i - 1]);
                                    if (tempt.Length > 0)
                                        tempt[0].Delete();
                                    m_dtDangkyPhongkham.AcceptChanges();
                                }
                            }
                            m_dtDangkyPhongkham.AcceptChanges();

                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Bạn thực hiện xóa công khám không thành công. Liên hệ đơn vị cung cấp phần mềm để được trợ giúp", "Thông báo");
                            break;
                    }
                //}
            }
            ModifyButtonCommandRegExam();

        }
        private void ModifyButtonCommandRegExam()
        {
            if (Utility.isValidGrid(grdRegExam))
            {
                cmdXoaCongkham.Enabled = Utility.Int32Dbnull(grdRegExam.GetValue(KcbDangkyKcb.Columns.TrangthaiThanhtoan), 0) == 0;
                cmdInBienlai.Enabled = !cmdXoaCongkham.Enabled;
                cmdInhoadon.Enabled = cmdInBienlai.Enabled;
                cmdThanhToanKham.Text = cmdXoaCongkham.Enabled ? "T.Toán" : "Hủy TT";
                cmdThanhToanKham.Tag = cmdXoaCongkham.Enabled ? "TT" : "HTT";
                cmdInPhieuKham.Enabled = grdRegExam.RowCount > 0 && grdRegExam.CurrentRow.RowType == RowType.Record;

                grdRegExam.RootTable.Columns["colThanhtoan"].ButtonText = cmdThanhToanKham.Text;
                pnlPrint.Visible = !cmdXoaCongkham.Enabled;
                cmdInBienlai.Visible = !cmdXoaCongkham.Enabled;
                cmdInhoadon.Visible = cmdInBienlai.Visible;
                //cmdThanhToanKham.Enabled = blnChophepthanhtoan;
                //cmdThanhToanKham.Visible = cmdThanhToanKham.Enabled;
            }
            else
            {
                cmdXoaCongkham.Enabled = cmdInBienlai.Enabled = cmdInPhieuKham.Enabled = cmdThanhToanKham.Enabled = false;
                pnlPrint.Visible = false;
                cmdInBienlai.Visible = false;
                cmdInhoadon.Visible = false;
                cmdThanhToanKham.Visible = false;
            }
        }
        private void cmdInPhieuKham_Click(object sender, EventArgs e)
        {
            InPhieu();
            ModifyButtonCommandRegExam();
        }
        private readonly string strSaveandprintPath = Application.StartupPath + @"\CAUHINH\SaveAndPrintConfig.txt";
        private string m_strDefaultLazerPrinterName = "";
        private void Try2CreateFolder()
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(strSaveandprintPath)))
                    Directory.CreateDirectory(Path.GetDirectoryName(strSaveandprintPath));
            }
            catch
            {
            }
        }
        private readonly string strSaveandprintPath1 = Application.StartupPath +
                                                       @"\CAUHINH\DefaultPrinter_PhieuHoaSinh.txt";
        private void AutoloadSaveAndPrintConfig()
        {
            try
            {
                Try2CreateFolder();
                if (File.Exists(strSaveandprintPath1))
                {
                    using (var _reader = new StreamReader(strSaveandprintPath1))
                    {
                        if (_reader.ReadLine().Trim() != "")
                        {
                            m_strDefaultLazerPrinterName = _reader.ReadLine().Trim();

                            _reader.BaseStream.Flush();
                            _reader.Close();
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }
        KCB_QMS _KCB_QMS = new KCB_QMS();
        void InPhieu()
        {
            try
            {
                if (grdRegExam.GetDataRows().Count() <= 0 || grdRegExam.CurrentRow.RowType != RowType.Record)
                    return;
                
                if (PropertyLib._MayInProperties.KieuInPhieuKCB == KieuIn.Innhiet)
                    InPhieuKCB();
                else
                    InphieuKham();
                //Thêm thông tin QMS cho phòng khám
                bool AllowQMS = THU_VIEN_CHUNG.Laygiatrithamsohethong("QMS_SINHSO_QMSPK_KHIINPHIEUKHAM","1",true)=="1";
                if (objBenhnhan != null && objLuotkham != null && objCongkham != null && AllowQMS && THU_VIEN_CHUNG.Laygiatrithamsohethong("QMSPK_ENABLE", "0", false) == "1")
                    _KCB_QMS.QmsPhongkhamInsert((int)objCongkham.SttKham.Value, objCongkham.MaPhongStt, DateTime.Now, objCongkham.NgayTiepdon.Value, objCongkham.MaLuotkham, objBenhnhan.IdBenhnhan, objBenhnhan.TenBenhnhan, (int)objBenhnhan.NamSinh, Utility.Int32Dbnull(grdList.GetValue("tuoi"), 0), objBenhnhan.GioiTinh, objCongkham.MaKhoaThuchien, (int)objCongkham.IdPhongkham, objCongkham.IdKham, (int)objCongkham.IdDichvuKcb, objCongkham.TenDichvuKcb);
            }
            catch (Exception ex)
            {

                Utility.ShowMsg("Lỗi khi in phiếu khám\r\n" + ex.Message);
            }
        }
        /// <summary>
        /// Lấy về regID của phòng khám thay vì lấy  phải phí dịch vụ kèm theo
        /// </summary>
        /// <returns></returns>
        int GetrealRegID()
        {
            int IdKham = Utility.Int32Dbnull(grdRegExam.CurrentRow.Cells[KcbDangkyKcb.IdKhamColumn.ColumnName].Value, -1);
            int idphongchidinh = Utility.Int32Dbnull(grdRegExam.CurrentRow.Cells[KcbDangkyKcb.IdChaColumn.ColumnName].Value, -1);
            int LaphiDVkemtheo = Utility.Int32Dbnull(grdRegExam.CurrentRow.Cells[KcbDangkyKcb.LaPhidichvukemtheoColumn.ColumnName].Value, -1);
            if (LaphiDVkemtheo == 1)
            {
                foreach (GridEXRow _row in grdRegExam.GetDataRows())
                {
                    if (Utility.Int32Dbnull(_row.Cells[KcbDangkyKcb.IdKhamColumn.ColumnName].Value, -1) == idphongchidinh)
                        return Utility.Int32Dbnull(_row.Cells[KcbDangkyKcb.IdKhamColumn.ColumnName].Value, -1);
                }
            }
            else
                return IdKham;
            return IdKham;
        }
        private void InPhieuKCB()
        {

            int IdKham = -1;
            string tieude = "", reportname = "";
            //VMS.HISLink.Report.Report.tiepdon_PHIEUKHAM_NHIET crpt = new VMS.HISLink.Report.Report.tiepdon_PHIEUKHAM_NHIET();
            ReportDocument crpt = Utility.GetReport("tiepdon_PHIEUKHAM_NHIET", ref tieude, ref reportname);
            if (crpt == null) return;
            var objPrint = new frmPrintPreview("IN PHIẾU KHÁM", crpt, true, true);
            IdKham = GetrealRegID();
           
            try
            {
                objCongkham = KcbDangkyKcb.FetchByID(IdKham);
                DmucKhoaphong lDepartment = DmucKhoaphong.FetchByID(objCongkham.IdPhongkham);
                Utility.SetParameterValue(crpt, "PHONGKHAM", Utility.sDbnull(lDepartment.MaKhoaphong));
                Utility.SetParameterValue(crpt, "STT", Utility.sDbnull(objCongkham.SttKham, ""));
                Utility.SetParameterValue(crpt, "BENHAN", Utility.sDbnull(grdList.CurrentRow.Cells[KcbLuotkham.Columns.MaLuotkham].Value, ""));
                Utility.SetParameterValue(crpt, "TENBN", Utility.sDbnull(grdList.CurrentRow.Cells[KcbDanhsachBenhnhan.Columns.TenBenhnhan].Value, ""));
                Utility.SetParameterValue(crpt, "GT_TUOI", Utility.sDbnull(grdList.CurrentRow.Cells[KcbDanhsachBenhnhan.Columns.GioiTinh].Value, "") + " - " + Utility.sDbnull(grdList.CurrentRow.Cells["Tuoi"].Value, "") + " tuổi");
                string SOTHE = "Không có thẻ";
                string HANTHE = "Không có hạn";
                if (Utility.sDbnull(grdList.CurrentRow.Cells[KcbLuotkham.Columns.MaDoituongKcb].Value) == "BHYT")
                {
                    SOTHE = Utility.sDbnull(grdList.CurrentRow.Cells[KcbLuotkham.Columns.MatheBhyt].Value, "Không có thẻ");
                    HANTHE = Utility.sDbnull(grdList.CurrentRow.Cells[KcbLuotkham.Columns.NgayketthucBhyt].Value);
                }
                Utility.SetParameterValue(crpt, "SOTHE", SOTHE);
                Utility.SetParameterValue(crpt, "HANTHE", HANTHE);
                if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInPhieuKCB, PropertyLib._MayInProperties.PreviewPhieuKCB))
                    objPrint.ShowDialog();
                else
                {
                    crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInPhieuKCB;
                    crpt.PrintToPrinter(1, false, 0, 0);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Có lỗi trong quá trình in phiếu khám-->\r\n" + ex.Message);
            }
            finally
            {
                Utility.FreeMemory(crpt);
            }
        }
        KcbDangkyKcb objCongkham = null;
        private void InphieuKham()
        {
            int IdKham = GetrealRegID();
            objCongkham = KcbDangkyKcb.FetchByID(IdKham);
            if (objCongkham != null)
            {
                new Update(KcbDangkyKcb.Schema)
                    .Set(KcbDangkyKcb.Columns.TrangthaiIn).EqualTo(1)
                    .Set(KcbDangkyKcb.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                    .Set(KcbDangkyKcb.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                    .Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(objCongkham.IdKham).Execute();
                IEnumerable<GridEXRow> query = from kham in grdRegExam.GetDataRows()
                                               where
                                                   kham.RowType == RowType.Record &&
                                                   Utility.Int32Dbnull(kham.Cells[KcbDangkyKcb.Columns.IdKham].Value, -1) ==
                                                   Utility.Int32Dbnull(objCongkham.IdKham)
                                               select kham;
                if (query.Count() > 0)
                {
                    GridEXRow gridExRow = query.FirstOrDefault();
                    gridExRow.BeginEdit();
                    gridExRow.Cells[KcbDangkyKcb.Columns.TrangthaiIn].Value = 1;
                    gridExRow.EndEdit();
                    grdRegExam.UpdateData();
                }
                DataTable v_dtData = _KCB_DANGKY.LayThongtinInphieuKCB(IdKham);
                v_dtData.TableName = "dt_PhieuKCB";
                THU_VIEN_CHUNG.CreateXML(v_dtData, Application.StartupPath + @"\Xml4Reports\PhieuKCB.XML");
                Utility.CreateBarcodeData(ref v_dtData, Utility.sDbnull(grdRegExam.GetValue(KcbDangkyKcb.Columns.MaLuotkham)));
                if (v_dtData.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy bản ghi nào", "Thông báo");
                    return;
                }
                KcbLuotkham objLuotkham = CreatePatientExam();
                if (objLuotkham != null)
                    KcbInphieu.INPHIEU_KHAM(Utility.sDbnull(objLuotkham.MaDoituongKcb), v_dtData,
                                                  "PHIẾU KHÁM BỆNH", cboA4.Text == @"A5" ? "A5" : "A4");
            }
        }
        /// <summary>
        /// hàm thực hiện lần thanh toán 
        /// </summary>
        /// <returns></returns>
        private KcbThanhtoan TaophieuThanhtoan()
        {
            KcbLuotkham objLuotkham = CreatePatientExam();
            KcbThanhtoan objPayment = new KcbThanhtoan();
            if (objLuotkham != null)
            {
                objPayment.IdThanhtoan = -1;
                objPayment.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham);
                objPayment.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan, -1);
                objPayment.NgayThanhtoan = globalVariables.SysDate;
                objPayment.IdNhanvienThanhtoan = globalVariables.gv_intIDNhanvien;
                objPayment.MaKhoaThuchien = globalVariables.MA_KHOA_THIEN;
                objPayment.KieuThanhtoan = 0;//0=Ngoại trú;1=nội trú
                objPayment.TrangthaiIn = 0;
                objPayment.NgayIn = null;
                objPayment.TtoanThuoc = false;
                objPayment.NguoiIn = string.Empty;
                objPayment.NgayTonghop = null;
                //objPayment.MaPttt = cboPttt.SelectedValue.ToString();
                //objPayment.MaNganhang = cboNganhang.Enabled ? Utility.sDbnull(cboNganhang.SelectedValue, "") : "-1";
                objPayment.NguoiTonghop = string.Empty;
                objPayment.NgayChot = null;
                objPayment.TrangthaiChot = 0;
                objPayment.TongTien = 0;
                //2 mục này được tính lại ở Business
                objPayment.BnhanChitra = -1;
                objPayment.NoiTru = 0;
                objPayment.BhytChitra = -1;
                objPayment.PtramBhyt = Utility.Int16Dbnull(objLuotkham.PtramBhyt);
                objPayment.MucHuongBhyt = objLuotkham.MucHuongBhyt;
                objPayment.TileChietkhau = 0;
                objPayment.KieuChietkhau = "%";
                objPayment.TongtienChietkhau = 0;
                objPayment.TongtienChietkhauChitiet = 0;
                objPayment.TongtienChietkhauHoadon = 0;
                objPayment.MaLydoChietkhau = "";
                objPayment.NgayTao = globalVariables.SysDate;
                objPayment.NguoiTao = globalVariables.UserName;
                objPayment.IpMaytao = globalVariables.gv_strIPAddress;
                objPayment.TenMaytao = globalVariables.gv_strComputerName;
                objPayment.MaPttt = "TM";
            }
            return objPayment;
        }
        KcbLuotkham objLuotkham = null;
        KcbDanhsachBenhnhan objBenhnhan = null;
        private List<int> GetIDKham()
        {
            List<int> lstRegID = new List<int>();
            int IdKham = Utility.Int32Dbnull(grdRegExam.CurrentRow.Cells[KcbDangkyKcb.Columns.IdKham].Value, -1);
            DataRow[] arrDr = null;
            if (PropertyLib._KCBProperties.Thanhtoancaphidichvukemtheo)
                arrDr = m_dtDangkyPhongkham.Select(KcbDangkyKcb.IdKhamColumn.ColumnName + "=" + IdKham.ToString() + " OR " + KcbDangkyKcb.IdChaColumn.ColumnName + "=" + IdKham.ToString());
            else
                arrDr = m_dtDangkyPhongkham.Select(KcbDangkyKcb.IdKhamColumn.ColumnName + "=" + IdKham.ToString());
            foreach (DataRow dr in arrDr)
            {
                if (Utility.Int32Dbnull(dr[KcbDangkyKcb.Columns.IdThanhtoan], -1) > 0)
                {
                    IdKham = Utility.Int32Dbnull(dr[KcbDangkyKcb.Columns.IdKham], -1);
                    if (!lstRegID.Contains(IdKham)) lstRegID.Add(IdKham);
                }
            }
            return lstRegID;
        }
        /// <summary>
        /// hàm thực hiện thanh toán chi tiết 
        /// </summary>
        /// <returns></returns>
        private KcbThanhtoanChitiet[] Taodulieuthanhtoanchitiet(ref List<int> lstRegID)
        {
            int IdKham = Utility.Int32Dbnull(grdRegExam.CurrentRow.Cells[KcbDangkyKcb.Columns.IdKham].Value, -1);
            DataRow[] arrDr = null;
            if (PropertyLib._KCBProperties.Thanhtoancaphidichvukemtheo)
                arrDr = m_dtDangkyPhongkham.Select(KcbDangkyKcb.IdKhamColumn.ColumnName + "=" + IdKham.ToString() + " OR " + KcbDangkyKcb.IdChaColumn.ColumnName + "=" + IdKham.ToString());
            else
                arrDr = m_dtDangkyPhongkham.Select(KcbDangkyKcb.IdKhamColumn.ColumnName + "=" + IdKham.ToString());
            List<KcbThanhtoanChitiet> lstPaymentDetail = new List<KcbThanhtoanChitiet>();

            foreach (DataRow dr in arrDr)
            {
                if (Utility.Int32Dbnull(dr[KcbDangkyKcb.Columns.IdThanhtoan], -1) <= 0)
                {
                    KcbThanhtoanChitiet newItem = new KcbThanhtoanChitiet();
                    newItem.IdThanhtoan = -1;
                    newItem.IdChitiet = -1;
                    newItem.TinhChiphi = 1;
                    if (!lstRegID.Contains(IdKham)) lstRegID.Add(IdKham);
                    if (objLuotkham.PtramBhyt != null) newItem.PtramBhyt = objLuotkham.PtramBhyt.Value;
                    if (objLuotkham.PtramBhytGoc != null) newItem.PtramBhytGoc = objLuotkham.PtramBhytGoc.Value;
                    newItem.SoLuong = 1;
                    //Phần tiền BHYT chi trả,BN chi trả sẽ tính lại theo % mới nhất của bệnh nhân trong phần Business
                    newItem.BnhanChitra = -1;
                    newItem.BhytChitra = -1;
                    newItem.GiaGoc = 0;
                    newItem.DonGia = Utility.DecimaltoDbnull(dr[KcbDangkyKcb.Columns.DonGia], 0);
                    newItem.TyleTt = Utility.DecimaltoDbnull(dr[KcbThanhtoanChitiet.Columns.TyleTt], 0);
                    newItem.PhuThu = Utility.DecimaltoDbnull(dr[KcbDangkyKcb.Columns.PhuThu], 0);
                    newItem.TuTuc = Utility.ByteDbnull(dr[KcbDangkyKcb.Columns.TuTuc], 0);
                    newItem.IdPhieu = Utility.Int32Dbnull(dr[KcbDangkyKcb.Columns.IdKham], -1);
                    newItem.IdKham = Utility.Int32Dbnull(dr[KcbDangkyKcb.Columns.IdKham], -1);
                    newItem.IdPhieuChitiet = Utility.Int32Dbnull(dr[KcbDangkyKcb.Columns.IdKham], -1);
                    newItem.IdDichvu = Utility.Int32Dbnull(dr[KcbDangkyKcb.Columns.IdDichvuKcb], -1);
                    newItem.IdChitietdichvu = Utility.Int32Dbnull(dr[KcbDangkyKcb.Columns.IdKieukham], -1);
                    newItem.TenChitietdichvu = Utility.sDbnull(dr[KcbDangkyKcb.Columns.TenDichvuKcb], "Không xác định").Trim();
                    newItem.TenBhyt = Utility.sDbnull(dr[KcbDangkyKcb.Columns.TenDichvuKcb], "Không xác định").Trim();
                    newItem.SttIn = 0;
                    newItem.IdKhoakcb = Utility.Int16Dbnull(dr["id_khoakcb"], -1);
                    newItem.IdPhongkham = Utility.Int16Dbnull(dr[KcbDangkyKcb.Columns.IdPhongkham], -1);
                    newItem.IdBacsiChidinh = globalVariables.gv_intIDNhanvien;
                    newItem.IdLoaithanhtoan = (byte)(Utility.Int32Dbnull(dr[KcbDangkyKcb.LaPhidichvukemtheoColumn.ColumnName], 0) == 1 ? 0 : 1);
                    newItem.TenLoaithanhtoan = THU_VIEN_CHUNG.MaKieuThanhToan(newItem.IdLoaithanhtoan);
                    newItem.MaDoituongKcb = objLuotkham.MaDoituongKcb;
                    newItem.UserTao = globalVariables.UserName;

                    newItem.DonviTinh = "Lượt";
                    newItem.KieuChietkhau = "%";
                    newItem.TileChietkhau = 0;
                    newItem.TienChietkhau = 0m;
                    newItem.IdThanhtoanhuy = -1;
                    newItem.TrangthaiHuy = 0;
                    newItem.TrangthaiBhyt = 0;
                    newItem.TrangthaiChuyen = 0;
                    newItem.NoiTru = 0;
                    newItem.NguonGoc = (byte)0;
                    newItem.NgayTao = globalVariables.SysDate;
                    newItem.NguoiTao = globalVariables.UserName;

                    lstPaymentDetail.Add(newItem);
                }
            }
            return lstPaymentDetail.ToArray(); ;
        }
        private void HuyThanhtoan()
        {
            try
            {
                string ma_lydohuy = "";
                string ten_lydohuy = "";
                if (!Utility.isValidGrid(grdRegExam)) return;
                if (!Utility.Coquyen("thanhtoan_huythanhtoan"))
                {
                    Utility.ShowMsg("Bạn không được cấp quyền hủy thanh toán(thanhtoan_huythanhtoan). Đề nghị liên hệ Admin để được cấp quyền");
                    return;
                }

                if (objLuotkham == null)
                {
                    objLuotkham = CreatePatientExam();
                }
                if (objLuotkham == null)
                {
                    Utility.ShowMsg("Không lấy được thông tin bệnh nhân dựa vào dữ liệu trên lưới danh sách bệnh nhân. Liên hệ bộ phận IT để được trợ giúp");
                    return;
                }
                if (Utility.Int32Dbnull(objLuotkham.TrangthaiNoitru, 0) >= Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_CHAN_THANHTOANNGOAITRU", "2", false), 2))
                {
                    Utility.ShowMsg("Bệnh nhân này đã ở trạng thái nội trú nên hệ thống không cho phép hủy thanh toán ngoại trú nữa");
                    return;
                }
                KcbDangkyKcb objDangky = KcbDangkyKcb.FetchByID(Utility.Int32Dbnull(grdRegExam.CurrentRow.Cells[KcbDangkyKcb.Columns.IdKham].Value, -1));
                if (objDangky == null)
                {
                    Utility.ShowMsg("Không lấy được thông tin Đăng ký dịch vụ KCB. Liên hệ bộ phận IT để được trợ giúp");
                    return;
                }
                if (Utility.Byte2Bool(objDangky.DachidinhCls))
                {
                    Utility.ShowMsg("Dịch vụ Khám đang chọn đã được bác sĩ chỉ định dịch vụ cận lâm sàng nên bạn không thể hủy thanh toán");
                    return;
                }
                if (Utility.Byte2Bool(objDangky.DakeDonthuoc))
                {
                    Utility.ShowMsg("Dịch vụ Khám đang chọn đã được bác sĩ kê đơn thuốc nên bạn không thể hủy thanh toán");
                    return;
                }
                //if (Utility.Byte2Bool(objDangky.TrangThai))
                //{
                //    Utility.ShowMsg("Dịch vụ Khám đang chọn đã được khám xong nên bạn không thể hủy thanh toán");
                //    return;
                //}


                int v_intIdThanhtoan = Utility.Int32Dbnull(grdRegExam.CurrentRow.Cells["Id_thanhtoan"].Value, -1);
                KcbThanhtoan objPayment = KcbThanhtoan.FetchByID(v_intIdThanhtoan);
                if (objPayment != null)
                {
                    //Kiểm tra ngày hủy
                    int kcbThanhtoanSongayHuythanhtoan =
                        Utility.Int32Dbnull(
                            THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_SONGAY_HUYTHANHTOAN", "0", true), 0);
                    var chenhlech =
                        (int)Math.Ceiling((globalVariables.SysDate.Date - objPayment.NgayThanhtoan.Date).TotalDays);
                    if (chenhlech > kcbThanhtoanSongayHuythanhtoan)
                    {
                        Utility.ShowMsg(
                            "Hệ thống không cho phép bạn hủy thanh toán đã quá ngày. Cần liên hệ quản trị hệ thống để được trợ giúp");
                        return;
                    }
                    if (Utility.Byte2Bool(objPayment.TrangthaiChot))
                    {
                        Utility.ShowMsg(
                            "Thanh toán đang chọn đã được chốt nên bạn không thể hủy thanh toán. Mời bạn xem lại!");
                        return;
                    }

                    List<int> lstRegID = GetIDKham();
                    if (PropertyLib._ThanhtoanProperties.Hienthihuythanhtoan)
                    {
                        frm_HuyThanhtoan frm = new frm_HuyThanhtoan("1");
                        frm.objLuotkham = objLuotkham;
                        frm.v_Payment_Id = v_intIdThanhtoan;
                        frm.Chuathanhtoan = 0;
                        frm.ShowCancel = true;
                        frm.ShowDialog();
                        if (!frm.m_blnCancel)
                        {
                            foreach (DataRow _row in m_dtDangkyPhongkham.Rows)
                            {
                                if (lstRegID.Contains(Utility.Int32Dbnull(_row[KcbDangkyKcb.Columns.IdKham], -1)))
                                {
                                    _row["ten_trangthai_thanhtoan"] = "Chưa thanh toán";
                                    _row[KcbDangkyKcb.Columns.IdThanhtoan] = -1;
                                    _row[KcbDangkyKcb.Columns.TrangthaiThanhtoan] = (byte)0;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (PropertyLib._ThanhtoanProperties.Hoitruockhihuythanhtoan)
                            if (!Utility.AcceptQuestion(string.Format("Bạn có muốn hủy lần thanh toán với Mã thanh toán {0}", v_intIdThanhtoan.ToString()), "Thông báo", true))
                            {
                                return;
                            }
                        if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_BATNHAPLYDO_HUYTHANHTOAN", "1", false) == "1")
                        {
                            frm_Chondanhmucdungchung _Nhaplydohuythanhtoan = new frm_Chondanhmucdungchung("LYDOHUYTHANHTOAN", "Hủy thanh toán tiền Bệnh nhân", "Nhập lý do hủy thanh toán trước khi thực hiện...", "Lý do hủy thanh toán",false);
                            _Nhaplydohuythanhtoan.ShowDialog();
                            if (_Nhaplydohuythanhtoan.m_blnCancel) return;
                            ma_lydohuy = _Nhaplydohuythanhtoan.ma;
                            ten_lydohuy = _Nhaplydohuythanhtoan.ten;
                        }
                        bool HUYTHANHTOAN_HUYBIENLAI = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_HUYTHANHTOAN_HUYBIENLAI", "1", true) == "1";
                        ActionResult actionResult = new KCB_THANHTOAN().HuyThanhtoan(KcbThanhtoan.FetchByID(v_intIdThanhtoan), objLuotkham, ma_lydohuy, Utility.Int32Dbnull(grdRegExam.CurrentRow.Cells[KcbThanhtoan.Columns.IdHdonLog], -1), HUYTHANHTOAN_HUYBIENLAI);
                        switch (actionResult)
                        {
                            case ActionResult.Success:
                                Utility.Log(this.Name, globalVariables.UserName, string.Format("Hủy thanh toán công khám cho người bệnh tại form tiếp đón Id={0}, PID={1}, Tên ={2}, Công khám={3}, Lý do hủy={4} ", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham, Utility.sDbnull(grdList.GetValue(KcbDanhsachBenhnhan.Columns.TenBenhnhan)), Utility.sDbnull(grdRegExam.GetValue("ten_dichvukcb")),string.Format("Mã: {0}- Tên: {1}",ma_lydohuy, ten_lydohuy)), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                                foreach (DataRow _row in m_dtDangkyPhongkham.Rows)
                                {
                                    if (lstRegID.Contains(Utility.Int32Dbnull(_row[KcbDangkyKcb.Columns.IdKham], -1)))
                                    {
                                        _row["ten_trangthai_thanhtoan"] = "Chưa thanh toán";
                                        _row[KcbDangkyKcb.Columns.IdThanhtoan] = -1;
                                        _row[KcbDangkyKcb.Columns.TrangthaiThanhtoan] = 0;
                                    }
                                }
                                break;
                            case ActionResult.ExistedRecord:
                                break;
                            case ActionResult.Error:
                                Utility.ShowMsg("Lỗi trong quá trình hủy thông tin thanh toán", "Thông báo", MessageBoxIcon.Error);
                                break;
                            case ActionResult.UNKNOW:
                                Utility.ShowMsg("Lỗi không xác định", "Thông báo", MessageBoxIcon.Error);
                                break;
                            case ActionResult.Cancel:
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi hủy thanh toán", "Thông báo", MessageBoxIcon.Error);
            }
            finally
            {
                ModifyButtonCommandRegExam();
            }

        }
        void Thanhtoan()
        {
            try
            {
                Utility.SetMsg(lblMsg, "", false);
                if (!Utility.Coquyen("thanhtoan_quyenthanhtoancongkham"))
                {
                    Utility.ShowMsg("Bạn chưa được cấp quyền thanh toán công khám(thanhtoan_quyenthanhtoancongkham) tại chức năng tiếp đón. Vui lòng liên hệ bộ phận IT để được hỗ trợ");
                    return;
                }
              
                if (grdRegExam.RowCount <= 0)
                {
                    Utility.ShowMsg("Chọn phòng khám để thanh toán,Mời bạn xem lại", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                int IdKham = Utility.Int32Dbnull(grdRegExam.GetValue(KcbDangkyKcb.Columns.IdKham));
                SqlQuery sqlQuery = new Select().From(KcbDangkyKcb.Schema)
                    .Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(IdKham)
                    .And(KcbDangkyKcb.Columns.TrangthaiThanhtoan).IsEqualTo(1);
                if (sqlQuery.GetRecordCount() > 0)
                {
                    Utility.ShowMsg("Bản ghi này đã thanh toán,Mời bạn xem lại", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                if (PropertyLib._KCBProperties.Hoitruockhithanhtoan)

                    if (!Utility.AcceptQuestion("Bạn có muốn thực hiện việc thanh toán khám bệnh cho bệnh nhân không ?",
                                               "Thông báo", true))
                        return;
                long Payment_Id = -1;
                objLuotkham = CreatePatientExam();
                if (Utility.Int32Dbnull(objLuotkham.TrangthaiNoitru, 0) >= Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_CHAN_THANHTOANNGOAITRU", "2", false), 2))
                {
                    Utility.ShowMsg("Bệnh nhân này đã ở trạng thái nội trú nên hệ thống không cho phép thanh toán ngoại trú nữa");
                    return;
                }
                KcbThanhtoan objPayment = TaophieuThanhtoan();
                DateTime maxDate = Convert.ToDateTime(grdRegExam.GetValue("ngay_dangky"));
                objPayment.MaxNgayTao = maxDate;
                List<int> lstRegID = new List<int>();
                decimal TTBN_Chitrathucsu = 0;
                string ErrMsg = "";
                ActionResult actionResult = new KCB_THANHTOAN().ThanhtoanChiphiDvuKcb(objPayment, objLuotkham,null, Taodulieuthanhtoanchitiet(ref lstRegID).ToList<KcbThanhtoanChitiet>(),null, ref Payment_Id, -1, false,false,"", ref TTBN_Chitrathucsu, ref ErrMsg);

                switch (actionResult)
                {
                    case ActionResult.Success:
                        Utility.Log(this.Name, globalVariables.UserName, string.Format("Thanh toán công khám cho người bệnh tại form tiếp đón Id={0}, PID={1}, Tên ={2}, Công khám={3} ", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham, Utility.sDbnull(grdList.GetValue(KcbDanhsachBenhnhan.Columns.TenBenhnhan)), Utility.sDbnull(grdRegExam.GetValue("ten_dichvukcb"))), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                        foreach (DataRow _row in m_dtDangkyPhongkham.Rows)
                        {
                            if (lstRegID.Contains(Utility.Int32Dbnull(_row[KcbDangkyKcb.Columns.IdKham], -1)))
                            {
                                _row["ten_trangthai_thanhtoan"] = "Đã thanh toán";
                                _row[KcbDangkyKcb.Columns.IdThanhtoan] = Payment_Id;
                                _row[KcbDangkyKcb.Columns.TrangthaiThanhtoan] = 1;
                            }
                        }
                        m_dtDangkyPhongkham.AcceptChanges();
                        Payment_Id = Utility.Int64Dbnull(objPayment.IdThanhtoan);
                        if (PropertyLib._MayInProperties.TudonginhoadonSaukhiThanhtoan && TTBN_Chitrathucsu > 0)
                        {
                            int KCB_THANHTOAN_KIEUINHOADON = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_KIEUINHOADONTUDONG_SAUKHITHANHTOAN", "1", false));
                            if (KCB_THANHTOAN_KIEUINHOADON == 1 || KCB_THANHTOAN_KIEUINHOADON == 3)
                                InHoadon(Payment_Id);
                            if (KCB_THANHTOAN_KIEUINHOADON == 2 || KCB_THANHTOAN_KIEUINHOADON == 3)
                                new INPHIEU_THANHTOAN_NGOAITRU().InBienlai(false, Payment_Id,-1, objLuotkham, 0);
                        }
                        Utility.SetMsg(lblMsg, "Thanh toán thành công", false);
                        break;
                    case ActionResult.Error:
                        Utility.SetMsg(lblMsg, "Lỗi khi thanh toán", true);
                        break;
                    case ActionResult.Cancel:
                        Utility.ShowMsg(ErrMsg);
                        break;
                }
            }
            catch
            {
            }
            finally
            {
                ModifyButtonCommandRegExam();
            }


        }
        private KcbPhieuthu CreatePhieuThu(KcbThanhtoan objPayment, decimal TONG_TIEN)
        {
            var objPhieuThu = new KcbPhieuthu();
            objPhieuThu.IdThanhtoan = objPayment.IdThanhtoan;
            objPhieuThu.MaPhieuthu = THU_VIEN_CHUNG.GetMaPhieuThu(globalVariables.SysDate, 0);
            objPhieuThu.SoluongChungtugoc = 1;
            objPhieuThu.LoaiPhieuthu = Convert.ToByte(0);
            objPhieuThu.NgayThuchien = globalVariables.SysDate;
            objPhieuThu.SoTien = TONG_TIEN;
            objPhieuThu.NguoiNop = globalVariables.UserName;
            objPhieuThu.IdNhanvien = globalVariables.gv_intIDNhanvien;
            objPhieuThu.IdKhoaThuchien = globalVariables.idKhoatheoMay;
            objPhieuThu.IdBenhnhan = objPayment.IdBenhnhan;
            objPhieuThu.MaLuotkham = objPayment.MaLuotkham;
            objPhieuThu.TaikhoanCo = "";
            objPhieuThu.TaikhoanNo = "";
            objPhieuThu.LydoNop = "Thu phí KCB bệnh nhân";
            objPhieuThu.NguoiTao = globalVariables.UserName;
            objPhieuThu.NgayTao = globalVariables.SysDate;
            return objPhieuThu;
        }
        void InHoadon(long _Payment_ID)
        {
            try
            {
                KcbThanhtoan objPayment = new Select().From(KcbThanhtoan.Schema).Where(KcbThanhtoan.IdThanhtoanColumn).IsEqualTo(_Payment_ID).ExecuteSingle<KcbThanhtoan>();
                if (objPayment == null)
                {
                    Utility.ShowMsg("Không lấy được thông tin hóa đơn thanh toán", "Thông báo lỗi", MessageBoxIcon.Warning);
                    return;
                }
                decimal TONG_TIEN = Utility.Int32Dbnull(objPayment.TongTien, -1);
                ActionResult actionResult = new KCB_THANHTOAN().UpdateDataPhieuThu(CreatePhieuThu(objPayment, TONG_TIEN));
                switch (actionResult)
                {
                    case ActionResult.Success:
                        new INPHIEU_THANHTOAN_NGOAITRU().IN_HOADON(_Payment_ID);
                        break;
                    case ActionResult.Error:
                        Utility.ShowMsg("Lỗi trong quá trình in hóa đơn", "Thông báo lỗi", MessageBoxIcon.Warning);
                        break;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi trong quá trình in hóa đơn\r\n" + ex.Message, "Thông báo lỗi");
            }
        }





        private void cmdThanhToanKham_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdRegExam)) return;
            if (Utility.sDbnull(cmdThanhToanKham.Tag, "ABCD") == "TT")
                Thanhtoan();
            else
                HuyThanhtoan();
        }
        /// <summary>
        /// hàm thực hiện việc hủy phiếu khám bệnh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdRegExam_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            if (e.Column.Key == "colDelete")
            {
                if (!IsValidData())
                    return;
                HuyThamKham();
            }
            if (e.Column.Key == "colIn")
            {
                InPhieu();
            }
            if (e.Column.Key == "colThanhtoan")
            {
                if (Utility.sDbnull(cmdThanhToanKham.Tag, "ABCD") == "TT")
                    Thanhtoan();
                else
                    HuyThanhtoan();
            }
        }

        #endregion
        private bool IsValidData()
        {
            try
            {
                if (!Utility.isValidGrid(grdRegExam)) return false;
                if (grdRegExam.CurrentRow == null) return false;
                if (Utility.Coquyen("tiepdon_xoacongkham") || globalVariables.UserName == Utility.sDbnull(grdRegExam.GetValue("nguoi_tao")))
                {
                }
                else
                {
                    Utility.ShowMsg("Bạn không được cấp quyền xóa công khám. Liên hệ quản trị hệ thống để được cấp thêm quyền", "Thông báo");
                    return false;
                }
                int id_kham = Utility.Int32Dbnull(grdRegExam.CurrentRow.Cells[KcbDangkyKcb.Columns.IdKham].Value, -1);
                KcbDangkyKcb objCongkham = KcbDangkyKcb.FetchByID(id_kham);
                if (objCongkham != null)
                {
                    if (objCongkham.KhamThiluc >= 1)
                    {
                        KcbPhieukhamThiluc objPKTL = new Select().From(KcbPhieukhamThiluc.Schema).Where(KcbPhieukhamThiluc.Columns.IdCongkham).IsEqualTo(objCongkham.IdKham).ExecuteSingle<KcbPhieukhamThiluc>();
                        if(objPKTL!=null && objPKTL.TrangThai>0)
                        {
                            Utility.ShowMsg("Công khám Đo thị lực bạn đang chọn xóa đã được nhập dữ liệu nên Bạn không thể xóa. Vui lòng liên hệ người đo thị lực {0} để yêu cầu xóa phiếu Đo thị lực trước khi xóa công khám này", "Thông báo");
                            grdRegExam.Focus();
                            return false;
                        }    
                       
                    }
                    if (objCongkham.TrangthaiThanhtoan >= 1)
                    {
                        Utility.ShowMsg("Công khám đang chọn đã thanh toán, Bạn không thể xóa", "Thông báo");
                        grdRegExam.Focus();
                        return false;
                    }
                    if (objCongkham.TrangThai >= 1)
                    {
                        Utility.ShowMsg("Công khám bạn chọn đã được bác sĩ kết thúc khám nên bạn không thể xóa", "Thông báo");
                        grdRegExam.Focus();
                        return false;
                    }


                    SqlQuery q =
                        new Select().From(KcbChidinhclsChitiet.Schema).Where(KcbChidinhclsChitiet.Columns.IdKham).IsEqualTo(
                            objCongkham.IdKham).And(KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(1);
                    if (q.GetRecordCount() > 0)
                    {
                        Utility.ShowMsg("Công khám đang chọn đã được bác sĩ chỉ định CLS và đã được thanh toán. Yêu cầu Hủy thanh toán các chỉ định CLS trước khi hủy phòng khám", "Thông báo");
                        grdRegExam.Focus();
                        return false;
                    }
                    SqlQuery qPres =
                        new Select().From(KcbDonthuocChitiet.Schema).Where(KcbDonthuocChitiet.Columns.IdKham).IsEqualTo(
                            objCongkham.IdKham).And(KcbDonthuocChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(1);
                    if (qPres.GetRecordCount() > 0)
                    {
                        Utility.ShowMsg("Công khám đang chọn đã được bác sĩ kê đơn thuốc và đã được thanh toán. Yêu cầu hủy thanh toán đơn thuốc trước khi hủy phòng khám", "Thông báo");
                        grdRegExam.Focus();
                        return false;
                    }


                    if (objCongkham.DachidinhCls >= 1)
                    {
                        Utility.ShowMsg("Công khám đang chọn đã được bác sĩ chỉ định CLS. Yêu cầu xóa chỉ định CLS trước khi xóa công khám", "Thông báo");
                        grdRegExam.Focus();
                        return false;
                    }
                    else if (objCongkham.DakeDonthuoc >= 1)
                    {
                        Utility.ShowMsg("Công khám đang chọn đã được bác sĩ kê đơn thuốc. Yêu cầu xóa đơn thuốc trước khi xóa công khám", "Thông báo");
                        grdRegExam.Focus();
                        return false;
                    }
                    q =
                       new Select().From(KcbChidinhcl.Schema).Where(KcbChidinhcl.Columns.IdKham).IsEqualTo(
                           objCongkham.IdKham);
                    if (q.GetRecordCount() > 0)
                    {
                        Utility.ShowMsg("Công khám đang chọn đã được bác sĩ chỉ định CLS. Yêu cầu xóa chỉ định CLS trước khi xóa công khám", "Thông báo");
                        grdRegExam.Focus();
                        return false;
                    }
                    qPres =
                       new Select().From(KcbDonthuoc.Schema).Where(KcbDonthuoc.Columns.IdKham).IsEqualTo(objCongkham.IdKham);
                    if (qPres.GetRecordCount() > 0)
                    {
                        Utility.ShowMsg("Công khám đang chọn đã được bác sĩ kê đơn thuốc. Yêu cầu xóa đơn thuốc trước khi xóa công khám", "Thông báo");
                        grdRegExam.Focus();
                        return false;
                    }
                    qPres =
                  new Select().From(NoitruPhieunhapvien.Schema).Where(NoitruPhieunhapvien.Columns.IdKham).IsEqualTo(objCongkham.IdKham);
                    if (qPres.GetRecordCount() > 0)
                    {
                        Utility.ShowMsg("Công khám được kết thúc để nhập viện nên không thể xóa", "Thông báo");
                        grdRegExam.Focus();
                        return false;
                    }

                }
                return true;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                return false;
            }
            
        }
        private void frm_KCB_DSACH_BNHAN_KeyDown(object sender, KeyEventArgs e)
        {
            if (uiTab1.SelectedIndex == 0)
            {
                if (e.KeyCode == Keys.F11) Utility.ShowMsg(this.ActiveControl.Name);
                if (e.KeyCode == Keys.F3) cmdTimKiem.PerformClick();
                if (e.KeyCode == Keys.Enter && uiTab1.SelectedTab != uiTabPageUpdate)
                {
                    Control ActCtrl = Utility.getActiveControl(this);
                    if (ActCtrl.GetType().Equals(cboPhongkhamThiluc.GetType())) return;
                    else
                        SendKeys.Send("{TAB}");
                }
                if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
                if (e.KeyCode == Keys.F1) tabChiDinh.SelectedTab = Tabpagedangky;
                if (e.KeyCode == Keys.F2) tabChiDinh.SelectedTab = tabPageChiDinh;
                if (e.KeyCode == Keys.N && e.Control) cmdThemMoiBN.PerformClick();
                if (e.KeyCode == Keys.U && e.Control) cmdSuaThongTinBN.PerformClick();
                if (e.KeyCode == Keys.D && e.Control) cmdXoaBenhNhan.PerformClick();
                if (e.KeyCode == Keys.K && e.Control) cmdThemLanKham.PerformClick();
            }
        }
        /// <summary>
        /// hàm thực hiện việc xóa thông tin bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdXoaBenhNhan_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Bạn phải chọn ít nhất 1 bệnh nhân để xóa");
                    return;
                }
                string ErrMgs = "";
                string v_MaLuotkham =
                   Utility.sDbnull(
                     grdList.GetValue(KcbLuotkham.Columns.MaLuotkham),
                       "");
                int v_Patient_ID =
                     Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan), -1);

                if (!IsValidDeleteData()) return;
                if (Utility.AcceptQuestion("Bạn có muốn xóa thông tin lần khám này không", "Thông báo", true))
                {
                    myTrace.FunctionID = globalVariables.FunctionID;
                    myTrace.FunctionName = globalVariables.FunctionName;
                    ActionResult actionResult = _KCB_DANGKY.PerformActionDeletePatientExam(myTrace, v_MaLuotkham,
                                                                                                       v_Patient_ID, ref ErrMgs);
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Xóa bệnh nhân ID={0}, PID={1}, Tên={2} ", v_Patient_ID.ToString(), v_MaLuotkham,  Utility.sDbnull(grdList.GetValue(KcbDanhsachBenhnhan.Columns.TenBenhnhan))), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                            grdList.CurrentRow.Delete();
                            grdList.UpdateData();
                            grdList_SelectionChanged(grdList, e);
                            m_dtPatient.AcceptChanges();
                            UpdateGroup();
                           
                            //Utility.ShowMsg("Xóa lần khám thành công", "Thành công");
                            break;
                        case ActionResult.Exception:
                            if (ErrMgs != "")
                                Utility.ShowMsg(ErrMgs);
                            else
                                Utility.ShowMsg("Bệnh nhân đã có thông tin chỉ định dịch vụ hoặc đơn thuốc... /n bạn không thể xóa lần khám này", "Thông báo");
                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Có lỗi trong quá trình xóa thông tin", "Thông báo");
                            break;
                    }
                }
                ModifyButtonCommandRegExam();
                ModifyCommand();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
            finally
            {
                GC.Collect();
            }
        }
        void ResetNhominCLS()
        {
            try
            {
                if (grdAssignDetail.GetDataRows().Length <= 0) return;
                var nhomcls = new List<string>();
                foreach (GridEXRow gridExRow in grdAssignDetail.GetDataRows())
                {
                    if (!nhomcls.Contains(Utility.sDbnull(gridExRow.Cells["nhom_in_cls"].Value)))
                    {
                        nhomcls.Add(Utility.sDbnull(gridExRow.Cells["nhom_in_cls"].Value));
                    }
                }
                DataTable dtNhomin = THU_VIEN_CHUNG.LayDulieuDanhmucChung(globalVariables.DC_NHOMIN_CLS, true);
                DataTable dttempt = dtNhomin.Clone();
                foreach (DataRow dr in dtNhomin.Rows)
                    if (nhomcls.Contains(Utility.sDbnull(dr[DmucChung.Columns.Ma], "")))
                        dttempt.ImportRow(dr);
                DataBinding.BindDataCombobox(cboServicePrint, dttempt, DmucChung.Columns.Ma, DmucChung.Columns.Ten,
                                                          "Tất cả", true);
            }
            catch (Exception ex)
            {

                Utility.ShowMsg(ex.ToString());
            }
        }
        private bool IsValidDeleteData()
        {

            string v_MaLuotkham =
             Utility.sDbnull(
               grdList.GetValue(KcbLuotkham.Columns.MaLuotkham),
                 "");
            int v_Patient_ID =
                 Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan), -1);
            SqlQuery sqlQuery = null; 
            // sqlQuery = new Select().From(KcbDangkyKcb.Schema)
            //    .Where(KcbDangkyKcb.Columns.MaLuotkham).IsEqualTo(v_MaLuotkham)
            //    .And(KcbDangkyKcb.Columns.IdBenhnhan).IsEqualTo(v_Patient_ID)
            //    .And(KcbDangkyKcb.Columns.TrangthaiThanhtoan).IsEqualTo(1);
            //if (sqlQuery.GetRecordCount() > 0)
            //{
            //    Utility.ShowMsg("Công khám đã thanh toán nên không được phép xóa");
            //    return false;
            //}
            sqlQuery = new Select().From(KcbChidinhcl.Schema).Where(KcbChidinhcl.Columns.MaLuotkham).IsEqualTo(v_MaLuotkham).And(KcbChidinhcl.Columns.IdBenhnhan).IsEqualTo(v_Patient_ID);
            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Bệnh nhân đã được chỉ định dịch vụ CLS nên bạn không thể xóa bệnh nhân. Vui lòng kiểm tra lại", "Thông báo");
                return false;
            }
            sqlQuery = sqlQuery = new Select().From(KcbDonthuoc.Schema).Where(KcbDonthuoc.Columns.MaLuotkham).IsEqualTo(v_MaLuotkham).And(KcbDonthuoc.Columns.IdBenhnhan).IsEqualTo(v_Patient_ID);
            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Bệnh nhân đã được kê đơn Thuốc/VTTH nên bạn không thể xóa bệnh nhân. Vui lòng kiểm tra lại", "Thông báo");
                return false;
            }
             sqlQuery = new Select().From(KcbThanhtoan.Schema)
                .Where(KcbThanhtoan.Columns.MaLuotkham).IsEqualTo(v_MaLuotkham)
                .And(KcbThanhtoan.Columns.IdBenhnhan).IsEqualTo(v_Patient_ID);
            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Bệnh nhân đã có hóa đơn nên bạn không thể xóa bệnh nhân. Vui lòng kiểm tra lại", "Thông báo");
                return false;
            }
            sqlQuery = new Select().From(KcbPhieuthu.Schema)
                .Where(KcbPhieuthu.Columns.MaLuotkham).IsEqualTo(v_MaLuotkham)
                .And(KcbPhieuthu.Columns.IdBenhnhan).IsEqualTo(v_Patient_ID);
            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Bệnh nhân đã có hóa đơn(hoặc phiếu thu -chi khác) nên bạn không thể xóa bệnh nhân. Vui lòng kiểm tra lại", "Thông báo");
                return false;
            }
            sqlQuery = new Select().From(NoitruTamung.Schema)
                .Where(NoitruTamung.Columns.MaLuotkham).IsEqualTo(v_MaLuotkham)
                .And(NoitruTamung.Columns.IdBenhnhan).IsEqualTo(v_Patient_ID);
            if (sqlQuery.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Bệnh nhân đã có phiếu tạm ứng nên bạn không thể xóa bệnh nhân. Vui lòng kiểm tra lại", "Thông báo");
                return false;
            }
            objLuotkham = Utility.getKcbLuotkham(grdList.CurrentRow);
            if (objLuotkham != null)
            {
                if (objLuotkham.TrangthaiNgoaitru > 0)
                {
                    Utility.ShowMsg("Bệnh nhân đã kết thúc khám ngoại trú nên bạn không được phép xóa");
                    return false;
                }
                if (objLuotkham.TrangthaiNoitru > 0)
                {
                    Utility.ShowMsg("Bệnh nhân đã nhập viện điều trị nội trú nên bạn không được phép xóa");
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// ham thực hiện viecj in phiếu cỉnh định
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInPhieuChiDinh_Click(object sender, EventArgs e)
        {
            if (grdAssignDetail.CurrentRow != null)
            {
                int v_AssignId = Utility.Int32Dbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh), -1);
                KcbChidinhcl objAssignInfo = KcbChidinhcl.FetchByID(v_AssignId);
                if (objAssignInfo != null)
                {
                    frm_INPHIEU_CLS frm = new frm_INPHIEU_CLS();
                    frm.objAssignInfo = KcbChidinhcl.FetchByID(v_AssignId);
                    frm.ShowDialog();
                }

            }

        }
        /// <summary>
        /// hàm thực hiện việc chọn kiểu khám bệnh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboKieuKham_ValueChanged(object sender, EventArgs e)
        {
            if (cboKieuKham.SelectedIndex >= 0)
            {
                DmucDichvukcb objDichvuKCB =
                    DmucDichvukcb.FetchByID(Utility.Int32Dbnull(cboKieuKham.Value));
                if (objDichvuKCB != null)
                {
                    Utility.SetMsg(lblDonGia, Utility.sDbnull(objDichvuKCB.DonGia), true);
                    Utility.SetMsg(lblPhuThu, Utility.sDbnull(objDichvuKCB.PhuthuDungtuyen), true);
                }
                else
                {
                    Utility.SetMsg(lblDonGia, Utility.sDbnull(0), true);
                    Utility.SetMsg(lblPhuThu, Utility.sDbnull(0), true);
                }
            }
        }
        /// <summary>
        /// hàm thực hiện viec lọc thông in trên lưới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grdList_ApplyingFilter(object sender, CancelEventArgs e)
        {
            ModifyCommand();
        }

        private void grdList_AddingRecord(object sender, CancelEventArgs e)
        {
            ModifyCommand();
        }

        private void lblDonGia_Click(object sender, EventArgs e)
        {

        }

        private void lblDonGia_TextChanged(object sender, EventArgs e)
        {
            Utility.FormatCurrencyHIS(lblDonGia);
        }

        private void lblPhuThu_TextChanged(object sender, EventArgs e)
        {
            Utility.FormatCurrencyHIS(lblPhuThu);
        }

        private void grdRegExam_SelectionChanged(object sender, EventArgs e)
        {
            if (!AllowCongkham_SelectionChanged) return;
            ModifyButtonCommandRegExam();
        }

        private void grdAssignDetail_SelectionChanged(object sender, EventArgs e)
        {
            if (!AllowSelectionChanged) return;
            ModifycommandAssignDetail();
            ResetNhominCLS();
            ShowResult();
            pnlAttachedFiles.Height = Utility.isValidGrid(grdAssignDetail) ? 60 : 0;
        }

        private void txtPatientCode_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                errorProvider1.Clear();
                var dtPatient = new DataTable();
                if (e.KeyCode == Keys.Enter && Utility.DoTrim(txtPatientCode.Text.Trim()) != "")
                {
                    string _ID = txtPatient_ID.Text.Trim();
                    string patient_ID = Utility.GetYY(DateTime.Now) + Utility.FormatNumberToString(Utility.Int32Dbnull(txtPatientCode.Text, 0), "000000");
                    txtPatient_ID.Clear();
                    txtPatientCode.Text = patient_ID;
                    TimKiemThongTin(false);
                    if (grdList.RowCount == 1) grdList_SelectionChanged(grdList, new EventArgs());
                    txtPatient_ID.Text = _ID;
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy thông tin bệnh nhân");
                //throw;
            }
        }

        private void txtPatient_ID_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                errorProvider1.Clear();
                var dtPatient = new DataTable();
                if (e.KeyCode == Keys.Enter && Utility.DoTrim(txtPatient_ID.Text.Trim()) != "")
                {
                    string _code = txtPatientCode.Text.Trim();
                    txtPatientCode.Clear();
                    TimKiemThongTin(false);
                    if (grdList.RowCount == 1) grdList_SelectionChanged(grdList, new EventArgs());
                    txtPatientCode.Text = _code;
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy thông tin bệnh nhân");
                //throw;
            }
        }



        private void cmdCauhinh_Click(object sender, EventArgs e)
        {
            frm_Properties frm = new frm_Properties(PropertyLib._KCBProperties);
            frm.ShowDialog();
            CauHinh();
        }

        private void mnuUpdateMalankham_Click(object sender, EventArgs e)
        {
            VNS.HIS.UI.Forms.Dungchung.frmUpdateMaLanKham frm = new frmUpdateMaLanKham();
            frm.txtmabenhnhancu.Text = Utility.sDbnull(grdList.CurrentRow.Cells[KcbLuotkham.Columns.MaLuotkham].Value);
            frm.txtidbenhnhancu.Text = Utility.sDbnull(grdList.CurrentRow.Cells[KcbLuotkham.Columns.IdBenhnhan].Value);
            frm.ShowDialog();
        }

        private void mnuUpdateMaBenhAn_Click(object sender, EventArgs e)
        {
            var frm = new frmUpdateMaBenhAn();
            frm.txtmalankham.Text = Utility.sDbnull(grdList.CurrentRow.Cells[KcbLuotkham.Columns.MaLuotkham].Value);
            frm.txtidbenhnhancu.Text = Utility.sDbnull(grdList.CurrentRow.Cells[KcbLuotkham.Columns.IdBenhnhan].Value);
            frm.ShowDialog();
        }

        private void mnuUpdateMaBenhNhan_Click(object sender, EventArgs e)
        {
            var frm = new frmUpdateMaBenhNhan();
            frm.txtmabenhnhancu.Text = Utility.sDbnull(grdList.CurrentRow.Cells[KcbLuotkham.Columns.IdBenhnhan].Value);
            frm.ShowDialog();
        }

        private void cmdNhapVien_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.Coquyen("noitru_suaphieunhapvien_trongkhoanoitru"))
                {
                    Utility.thongbaokhongcoquyen("noitru_suaphieunhapvien_trongkhoanoitru", "sửa thông tin phiếu nhập viện từ khoa nội trú");
                    return;
                }

                var frm = new frm_Nhapvien();
                frm.CallfromParent = true;
                frm.id_kham = -1;//Load lại từ phiếu nhập viện
                frm.id_bskham = -1;//Load lại từ phiếu nhập viện
                frm.objLuotkham = objLuotkham;
                frm.ucThongtinnguoibenh1.txtMaluotkham.Text = objLuotkham.MaLuotkham;
                frm.ShowDialog();
                if (frm.b_Cancel)
                {

                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }

            //if (objLuotkham.TrangthaiNoitru > 1)
            //{
            //    Utility.ShowMsg(
            //        "Bệnh nhân đã được điều trị nội trú nên bạn chỉ có thể xem và không được phép sửa các thông tin thăm khám");
            //    return;
            //}
            ////Kiểm tra xem có đơn thuốc ngoại trú hay không
            //string errMsg = "";
            //StoredProcedure v_sp = SPs.KcbKiemtradieukiennhapvien(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, errMsg);
            //v_sp.Execute();
            //errMsg = Utility.sDbnull(v_sp.OutputValues[0]);
            //if (Utility.DoTrim(errMsg).Length > 0)
            //{
            //    Utility.ShowMsg(errMsg);
            //    return;
            //}
            //var frm = new frm_Nhapvien();
            //frm.CallfromParent = true;
            //frm.id_kham = Utility.Int32Dbnull(grdRegExam.CurrentRow.Cells["id_kham"].Value);
            //frm.objLuotkham = objLuotkham;
            //frm.ShowDialog();
            //if (frm.b_Cancel)
            //{
            //    TimKiemThongTin(true);
            //}
        }

        private void mnuupdatengaykham_Click(object sender, EventArgs e)
        {
            var frm = new FrmUpdateNgaykham(Utility.sDbnull(grdList.CurrentRow.Cells["ma_luotkham"].Value));
            frm.ShowDialog();
        }

        private void cmdInphieuravien_Click(object sender, EventArgs e)
        {
            try
            {
                string maLankham = Utility.sDbnull(grdList.CurrentRow.Cells[KcbLuotkham.Columns.MaLuotkham].Value);
                Utility.WaitNow(this);
                DataTable dtData =
                    SPs.NoitruInphieuravien(Utility.DoTrim(maLankham)).GetDataSet().Tables[0];

                if (dtData.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu cho báo cáo", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                dtData.TableName = "dt_PhieuRavien";
                THU_VIEN_CHUNG.CreateXML(dtData, "noitru_phieuravien.XML");
                Utility.UpdateLogotoDatatable(ref dtData);
                string StaffName = globalVariables.gv_strTenNhanvien;
                if (string.IsNullOrEmpty(globalVariables.gv_strTenNhanvien)) StaffName = globalVariables.UserName;

                string tieude = "", reportname = "";
                ReportDocument crpt = Utility.GetReport("noitru_phieuravien", ref tieude, ref reportname);
                if (crpt == null) return;
                var objForm = new frmPrintPreview(tieude, crpt, true,
                    dtData.Rows.Count > 0);
                crpt.SetDataSource(dtData);

                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = "noitru_phieuravien";
                Utility.SetParameterValue(crpt, "StaffName", StaffName);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "sCurrentDate",
                    Utility.FormatDateTimeWithThanhPho(Convert.ToDateTime(dtData.Rows[0]["ngay_ravien"].ToString())));
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "txtTrinhky",
                          Utility.getTrinhky(objForm.mv_sReportFileName, globalVariables.SysDate));
                objForm.crptViewer.ReportSource = crpt;
                objForm.ShowDialog();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                Utility.DefaultNow(this);
            }
        }

        private void chkIntach_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIntach.Checked)
            {
                File.WriteAllText(Application.StartupPath + "\\CAUHINH\\chkintachphieu.txt", "1");
            }
            else
            {
                File.WriteAllText(Application.StartupPath + "\\CAUHINH\\chkintachphieu.txt", "0");
            }
        }

        void LoadHTML()
        {
            string noidung = txtNoiDung.Text;
            webBrowser1.Document.InvokeScript("setValue", new[] { noidung });
        }
        DmucVungkhaosat vks = null;
        void ShowEditor(int id_VungKS)
        {
            vks = DmucVungkhaosat.FetchByID(id_VungKS);
            txtNoiDung.Text = vks != null ? vks.MotaHtml : "";
            richtxtKetluan.Text = Utility.sDbnull(Utility.GetValueFromGridColumn(grdAssignDetail, "ket_luan"));
            txtDenghi.Text = Utility.sDbnull(Utility.GetValueFromGridColumn(grdAssignDetail, "de_nghi"));
            string html = Utility.sDbnull(Utility.GetValueFromGridColumn(grdAssignDetail, "mo_ta_HTML"), "");
            if (html != "")
                txtNoiDung.Text = html;
            timer1.Start();
            LoadHTML();
        }
        private void ShowResult()
        {
            try
            {
                
                pnlXQ.Width = 0;
                return;
                //mnuNhapKQCDHA.Visible = false;
                LoadAttachedFiles();
                if (!PropertyLib._KCBProperties.ResultInput) return;
                //mnuNhapKQCDHA.Visible = false;
                Int16 trangthaiChitiet =
                    Utility.Int16Dbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.TrangthaiChitiet), 0);
                Int16 coChitiet =
                    Utility.Int16Dbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.CoChitiet), 0);

                int idChitietdichvu =
                    Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdChitietdichvu), 0);
                int idDichvu =
                    Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdDichvu), 0);

                int idChitietchidinh =
                    Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdChitietchidinh), 0);
                int idChidinh =
                    Utility.Int32Dbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.IdChidinh), 0);
                string ketluanHa =
                  Utility.sDbnull(
                      Utility.GetValueFromGridColumn(grdAssignDetail, "ketluan_ha"), "");
                string maloaiDichvuCls =
                    Utility.sDbnull(
                        Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.MaLoaidichvu), "XN");
                string maChidinh =
                    Utility.sDbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.MaChidinh),
                                    "XN");
                string maBenhpham =
                    Utility.sDbnull(Utility.GetValueFromGridColumn(grdAssignDetail, VKcbChidinhcl.Columns.MaBenhpham),
                                    "XN");
                if (trangthaiChitiet <= 2)
                //0=Mới chỉ định;1=Đã chuyển CLS;2=Đang thực hiện;3= Đã có kết quả CLS;4=Đã xác nhận kết quả
                {
                    if (maloaiDichvuCls != "XN")
                    {
                        pnlXQ.Width = 0;
                        //mnuNhapKQCDHA.Visible = true;
                    }
                    else
                    {
                    }

                    Application.DoEvents();
                }
                else
                {
                    
                    if (PropertyLib._ThamKhamProperties.HienthiKetquaCLSTrongluoiChidinh)
                    {
                        if (coChitiet == 1 || maloaiDichvuCls != "XN")
                            pnlXQ.Width = PropertyLib._ThamKhamProperties.DorongVungKetquaCLS;
                        else
                            pnlXQ.Width = 0;
                    }
                    else
                    {
                        pnlXQ.Width = PropertyLib._ThamKhamProperties.DorongVungKetquaCLS;
                    }
                    //Utility.ShowColumns(grdKetqua, coChitiet == 1 ? lstKQCochitietColumns : lstKQKhongchitietColumns);
                    //Lấy dữ liệu CLS
                    if (maloaiDichvuCls == "XN")
                    {
                        
                        
                    }
                    else //XQ,SA,DT,NS
                    {
                        //mnuNhapKQCDHA.Visible = true;
                        ShowEditor(Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, "id_VungKS"), 0));
                    }
                    Application.DoEvents();
                }
               
            }
            catch (Exception exception)
            {
                Utility.ShowMsg(exception.Message);
            }
        }

        void LoadAttachedFiles()
        {
            try
            {
                flowAttachedFiles.Controls.Clear();
                int idChidinh = Utility.Int32Dbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh), -1);
                DataTable dtAttachedFiles = new Select().From(TblFiledinhkem.Schema).Where(TblFiledinhkem.Columns.IdChidinh).IsEqualTo(idChidinh).ExecuteDataSet().Tables[0];
                foreach (DataRow dr in dtAttachedFiles.Rows)
                {
                    Label _file = new Label();
                    _file.ContextMenuStrip = ctxDelFile;
                    _file.AutoSize = true;
                    _file.Font = lblSample.Font;
                    _file.ForeColor = lblSample.ForeColor;
                    _file.Text = dr[TblFiledinhkem.Columns.Id].ToString() + "-" + dr[TblFiledinhkem.Columns.FileName].ToString();
                    _file.Tag = dr[TblFiledinhkem.Columns.FileData];
                    _file.Click += _file_Click;
                    flowAttachedFiles.Controls.Add(_file);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());

            }

        }

        void _file_Click(object sender, EventArgs e)
        {
            Label obj = sender as Label;
            byte[] fileData = obj.Tag as byte[];
            //Save to temp folder
            string fileName = Application.StartupPath + @"\tempdpf\" + obj.Text;
            Utility.CreateFolder(fileName);
            File.WriteAllBytes(fileName, fileData);
            System.Diagnostics.Process.Start(fileName);
        }
        private void cmdFileAttach_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdAssignDetail))
            {
                Utility.ShowMsg("Cần có phiếu chỉ định mới có thể thực hiện thêm các File kết quả đính kèm", "Thông báo");
                return;
            }
            int idChidinh = Utility.Int32Dbnull(grdAssignDetail.GetValue(KcbChidinhclsChitiet.Columns.IdChidinh), -1);
            string MaChidinh = Utility.sDbnull(grdAssignDetail.GetValue(KcbChidinhcl.Columns.MaChidinh), "xxx");
            OpenFileDialog _openfile = new OpenFileDialog();
            _openfile.Multiselect = true;
            if (_openfile.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (_openfile.FileNames.Count() >= 5)
                {
                    if (!Utility.AcceptQuestion(string.Format("Bạn đang chọn nhiều ({0}) file kết quả cho phiếu chỉ định này. Bạn có chắc chắn?", _openfile.FileNames.Count().ToString()), "Cảnh báo", true))
                    {
                        return;
                    }
                }
                foreach (string sfile in _openfile.FileNames)
                {
                    byte[] file;
                    using (var stream = new FileStream(sfile, FileMode.Open, FileAccess.Read))
                    {
                        using (var reader = new BinaryReader(stream))
                        {
                            file = reader.ReadBytes((int)stream.Length);
                        }
                    }

                    //TblFiledinhkem _file = new Select().From(TblFiledinhkem.Schema).Where(TblFiledinhkem.Columns.IdChidinh).IsEqualTo(idChidinh).ExecuteSingle<TblFiledinhkem>();
                    //if (_file == null)
                    //{
                    TblFiledinhkem _file = new TblFiledinhkem();
                    _file.NguoiTao = globalVariables.UserName;
                    _file.NgayTao = globalVariables.SysDate;
                    _file.IsNew = true;
                    //}
                    //else
                    //{
                    //    _file.IsNew = false;
                    //    _file.MarkOld();
                    //}
                    _file.FileName = Path.GetFileName(sfile);
                    _file.IdChidinh = idChidinh;
                    _file.FileData = file;
                    _file.Save();

                }
                LoadAttachedFiles();
            }

        }

        private void mnuDelFile_Click(object sender, EventArgs e)
        {
            // Try to cast the sender to a MenuItem
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;
            if (menuItem != null)
            {
                ContextMenuStrip owner = menuItem.Owner as ContextMenuStrip;
                if (owner != null)
                {
                    Label sourceControl = owner.SourceControl as Label;
                    int id = Utility.Int32Dbnull(sourceControl.Text.Split('-')[0], 0);
                    if (Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa file đính kèm vừa chọn?", "Xác nhận xóa", true))
                    {
                        new Delete().From(TblFiledinhkem.Schema).Where(TblFiledinhkem.Columns.Id).IsEqualTo(id).Execute();
                        LoadAttachedFiles();
                    }
                }

                // Get the control that is displaying this context menu

            }
        }
        public FTPclient FtpClient;
        public string _FtpClientCurrentDirectory;
        private string _baseDirectory = string.Format("{0}{1}", AppDomain.CurrentDomain.BaseDirectory, "Radio\\");
        private void InitFtp()
        {
            try
            {
                if(PropertyLib._HinhAnhProperties==null) PropertyLib._HinhAnhProperties= PropertyLib.GetHinhAnhProperties();
                FtpClient = new FTPclient(PropertyLib._HinhAnhProperties.FTPServer, PropertyLib._HinhAnhProperties.UID, PropertyLib._HinhAnhProperties.PWD);
                FtpClient.UsePassive = true;
                _FtpClientCurrentDirectory = FtpClient.CurrentDirectory;
                _baseDirectory = Utility.DoTrim(PropertyLib._HinhAnhProperties.ImageFolder);
                if (_baseDirectory.EndsWith(@"\")) _baseDirectory = _baseDirectory.Substring(0, _baseDirectory.Length - 1);
                if (!Directory.Exists(_baseDirectory))
                {
                    Directory.CreateDirectory(_baseDirectory);
                }
            }
            catch
            {
            }
        }
        private void BeginExam()
        {
            if (!PropertyLib._KCBProperties.ResultInput) return;
            int v_id_chitietchidinh = -1;
            try
            {
                if (PropertyLib._HinhAnhProperties == null) PropertyLib._HinhAnhProperties = PropertyLib.GetHinhAnhProperties();
                this.Cursor = Cursors.WaitCursor;
                try
                {
                    if (grdAssignDetail.CurrentRow != null && grdAssignDetail.CurrentRow.RowType == RowType.Record)
                    {
                        v_id_chitietchidinh = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, "id_chitietchidinh"), -1);
                        if (Utility.DoTrim(Utility.sDbnull(grdAssignDetail.CurrentRow.Cells["dsach_vungkhaosat"].Value, "")) == "")
                        {
                            Utility.ShowMsg("Dịch vụ này chưa gán vùng khảo sát nên không thể nhập kết quả.\r\nNhấn OK để thực hiện gán vùng khảo sát cho dịch vụ đang chọn");
                            frm_chonvungksat _chonvungks = new frm_chonvungksat(new List<string>());
                            _chonvungks.Hthi_Chon = true;
                            _chonvungks.ten_dvu = Utility.GetValueFromGridColumn(grdAssignDetail, "ten_chitietdichvu");
                            if (_chonvungks.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                            {
                                if (Utility.DoTrim(_chonvungks.vungks).Length > 0)
                                {
                                    grdAssignDetail.CurrentRow.BeginEdit();
                                    grdAssignDetail.CurrentRow.Cells["dsach_vungkhaosat"].Value = _chonvungks.vungks;
                                    grdAssignDetail.CurrentRow.EndEdit();
                                    grdAssignDetail.UpdateData();
                                    grdAssignDetail.Refetch();
                                    DmucDichvuclsChitiet objDvu = DmucDichvuclsChitiet.FetchByID(Utility.GetValueFromGridColumn(grdAssignDetail, "id_chitietdichvu"));
                                    if (objDvu != null)
                                    {
                                        objDvu.DsachVungkhaosat = _chonvungks.vungks;
                                        objDvu.Save();
                                        m_dtChiDinhCLS.AsEnumerable().Where(c => c.Field<Int32>("id_chitietdichvu") == Utility.Int64Dbnull(Utility.GetValueFromGridColumn(grdAssignDetail, "id_chitietdichvu"))).ToList().ForEach(x1 => { x1["dsach_vungkhaosat"] = _chonvungks.vungks; });
                                    }
                                }
                            }
                            else
                            {
                                Utility.ShowMsg("Bạn vừa hủy chọn vùng khảo sát nên không thể nhập kết quả");
                                return;
                            }

                        }
                        DataRowView dr = grdAssignDetail.CurrentRow.DataRow as DataRowView;
                        Utility.SetMsg(lblMsg, "Đang nạp thông tin bệnh nhân...", false);
                        if (FtpClient != null) FtpClient.CurrentDirectory = _FtpClientCurrentDirectory;
                        int Status = Utility.Int32Dbnull(grdAssignDetail.CurrentRow.Cells["trang_thai"].Value, -1);
                        if (Status <= 2)
                        {
                            new KCB_HinhAnh().UpdateXacNhanDaThucHien(v_id_chitietchidinh, 2);
                        }
                        int id_VungKS = Utility.Int32Dbnull(grdAssignDetail.CurrentRow.Cells["id_VungKS"].Value, -1);
                        if (id_VungKS > 0)
                        {
                            goto _EnterResult;
                        }
                        List<string> lstID = new List<string>();
                        lstID = Utility.sDbnull(grdAssignDetail.CurrentRow.Cells["dsach_vungkhaosat"].Value, "-1").Split(',').ToList<string>();
                        if (lstID.Count >= 2)
                        {
                            frm_chonvungksat _chonvungksat = new frm_chonvungksat(lstID);
                            if (_chonvungksat.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                                id_VungKS = _chonvungksat.Id;
                            else
                                return;
                        }
                        else
                            if (lstID.Count > 0)
                                id_VungKS = Utility.Int32Dbnull(lstID[0], -1);
                    _EnterResult:
                        frm_NhaptraKQ frm = new frm_NhaptraKQ();
                        DataTable dtWorkList = SPs.HinhanhTimkiembnhanTheoIDchidinhchitiet(v_id_chitietchidinh).GetDataSet().Tables[0];
                        frm.drWorklistDetail = (((DataRowView)grdAssignDetail.CurrentRow.DataRow).Row);
                        frm.drWorklist = dtWorkList.Rows[0];
                        if (frm.FtpClient == null) frm.FtpClient = this.FtpClient;
                        frm.ID_Study_Detail = v_id_chitietchidinh;
                        frm.lstID = Utility.sDbnull(grdAssignDetail.CurrentRow.Cells["dsach_vungkhaosat"].Value, "-1").Split(',').ToList<string>();
                        frm.id_VungKS = id_VungKS;
                        frm.StrServiceCode = Utility.sDbnull(grdAssignDetail.CurrentRow.Cells["ma_loaidichvu"].Value);
                        frm.ShowDialog();
                        if (!frm.mv_blnCancel)
                        {
                            grdAssignDetail_SelectionChanged(grdAssignDetail, new EventArgs());
                        }
                        frm.Dispose();
                        frm = null;
                    }
                }
                catch (Exception ex)
                {
                    Utility.ShowMsg("Lỗi:" + ex.Message);
                }
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:" + exception.Message);
            }
            finally
            {
                Utility.SetMsg(lblMsg, "Mời bạn tiếp tục làm việc...", false);
                this.Cursor = Cursors.Default;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtNoiDung.Text)) ClickData();
        }
        private void ClickData()
        {
            webBrowser1.Document.InvokeScript("setValue", new[] { txtNoiDung.Text });
            timer1.Stop();
        }

        private void cmdIntachPhieu_Click(object sender, EventArgs e)
        {
            ChoninphieuCLS(false);
        }
        void ChoninphieuCLS(bool inchung)
        {
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Bạn cần chọn người bệnh trước khi thực hiện các chức năng in phiếu chỉ định");
                return;
            }
            frm_InphieuCLS _InphieuCLS = new frm_InphieuCLS(m_dtChiDinhCLS, Utility.getUserConfigValue("CHIDINHCLS_TUDONGCHONCACPHIEUKHI_INTACH", (byte)1) == 1);
            _InphieuCLS.objLuotkham = this.objLuotkham;
            if (!inchung)
                _InphieuCLS.ShowDialog();
            else
            {
                _InphieuCLS.InChungphieu();
            }
        }

        private void cmdInchung_Click(object sender, EventArgs e)
        {
            ChoninphieuCLS(true);
        }
        internal static IntPtr hWnd;
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern UInt32 WaitForSingleObject(IntPtr hHandle, UInt32 dwMilliseconds);
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern void SendMessageW(IntPtr hWnd, uint msg, uint wParam, uint lParam);
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindowW(string className, string windowName);
        internal static Process process;
        private void mnuFingerRegister_Click(object sender, EventArgs e)
        {
            RegisterFinger();  
        }

        private void cmdFingerPrint_Click(object sender, EventArgs e)
        {
            RegisterFinger();
        }
        void RegisterFinger()
        {
            try
            {
                string patientID = Utility.sDbnull(grdList.CurrentRow.Cells["id_benhnhan"].Value, "");
                if (Utility.Int32Dbnull(patientID, -1) > 0)
                {
                    List<string> _list = new List<string>();
                    _list.Add(patientID.ToString());
                    _list.Add(1.ToString());
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

        private void cmdXemLichsuKCB_Click(object sender, EventArgs e)
        {
            if (Utility.isValidGrid(grdList))
            {
                frm_lichsukcb _lichsukcb = new frm_lichsukcb();
                _lichsukcb.txtMaluotkham.Text = grdList.GetValue("ma_luotkham").ToString();
                _lichsukcb.AutoLoad = true;
                _lichsukcb.Anluoidanhsachbenhnhan = true;
                _lichsukcb.ShowDialog();
            }
            else
            {

            }
        }

        private void cmdChange_Click(object sender, EventArgs e)
        {
            try
            {
                int id_kham = Utility.Int32Dbnull(grdRegExam.CurrentRow.Cells[KcbDangkyKcb.Columns.IdKham].Value, -1);
                KcbDangkyKcb objCongkham = KcbDangkyKcb.FetchByID(id_kham);
                if (objCongkham != null)
                {

                    if (objCongkham.TrangThai >= 1)
                    {
                        Utility.ShowMsg("Công khám bạn chọn đã được bác sĩ kết thúc khám nên bạn không thể chuyển đổi", "Thông báo");
                        return;
                    }


                    SqlQuery q =
                        new Select().From(KcbChidinhclsChitiet.Schema).Where(KcbChidinhclsChitiet.Columns.IdKham).IsEqualTo(
                            objCongkham.IdKham).And(KcbChidinhclsChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(1);
                    if (q.GetRecordCount() > 0)
                    {
                        Utility.ShowMsg("Công khám đang chọn đã được bác sĩ chỉ định CLS và thanh toán nên bạn không thể chuyển đổi", "Thông báo");
                        return;
                    }
                    SqlQuery qPres =
                        new Select().From(KcbDonthuocChitiet.Schema).Where(KcbDonthuocChitiet.Columns.IdKham).IsEqualTo(
                            objCongkham.IdKham).And(KcbDonthuocChitiet.Columns.TrangthaiThanhtoan).IsEqualTo(1);
                    if (qPres.GetRecordCount() > 0)
                    {
                        Utility.ShowMsg("Công khám đang chọn đã được bác sĩ kê đơn thuốc và đã thanh toán nên bạn không thể chuyển đổi", "Thông báo");
                        return;
                    }


                    if (objCongkham.DachidinhCls >= 1)
                    {
                        Utility.ShowMsg("Công khám đang chọn đã được bác sĩ chỉ định CLS nên bạn không thể chuyển đổi", "Thông báo");
                        return;
                    }
                    else if (objCongkham.DakeDonthuoc >= 1)
                    {
                        Utility.ShowMsg("Công khám đang chọn đã được bác sĩ kê đơn thuốc nên bạn không thể chuyển đổi", "Thông báo");
                        return;
                    }
                    q =
                       new Select().From(KcbChidinhcl.Schema).Where(KcbChidinhcl.Columns.IdKham).IsEqualTo(
                           objCongkham.IdKham);
                    if (q.GetRecordCount() > 0)
                    {
                        Utility.ShowMsg("Công khám đang chọn đã được bác sĩ chỉ định CLS nên bạn không thể chuyển đổi", "Thông báo");
                        return;
                    }
                    qPres =
                       new Select().From(KcbDonthuoc.Schema).Where(KcbDonthuoc.Columns.IdKham).IsEqualTo(objCongkham.IdKham);
                    if (qPres.GetRecordCount() > 0)
                    {
                        Utility.ShowMsg("Công khám đang chọn đã được bác sĩ kê đơn thuốc nên bạn không thể chuyển đổi", "Thông báo");
                        return;
                    }
                    DmucDichvukcb objDichvuKCB = DmucDichvukcb.FetchByID(Utility.Int32Dbnull(cboKieuKham.Value));
                    decimal dongia_cu = Utility.DecimaltoDbnull(grdRegExam.GetValue("don_gia"), -1);
                    if (objCongkham.TrangthaiThanhtoan >= 1)//Kiểm tra xem cùng đơn giá không
                    {
                        if ( (Utility.DecimaltoDbnull(objDichvuKCB.DonGia, 0) * (1 + Utility.DecimaltoDbnull(objDoituongKCB.MotaThem, 0) / 100)) != dongia_cu)
                        {
                            Utility.ShowMsg(string.Format("Công khám bạn đã đăng ký đang có giá {0} và đã được thanh toán. Công khám bạn chọn đang có giá {1} khác với giá đã thanh toán {2}\r\nBạn vui lòng chọn công khám có giá tương đương({3}) để thực hiện chuyển đổi", Utility.FormatCurrencyHIS(dongia_cu), Utility.FormatCurrencyHIS(objDichvuKCB.DonGia), Utility.FormatCurrencyHIS(dongia_cu), Utility.FormatCurrencyHIS(dongia_cu)), "Thông báo");
                            return;
                        }
                        frm_Chondanhmucdungchung nhaplydohuythanhtoan = new frm_Chondanhmucdungchung("LYDOCHUYENPHONGKHAM",
                                   "Đổi phòng khám", "Nhập lý do đổi phòng khám trước khi thực hiện",
                                   "Lý do đổi",false);
                        nhaplydohuythanhtoan.ShowDialog();
                        if (nhaplydohuythanhtoan.m_blnCancel) return;
                        //Thực hiện chuyển đổi
                        if (ChuyenPhongkham.ChuyenPhong(objCongkham, Utility.DoTrim(nhaplydohuythanhtoan.ten), objDichvuKCB) == ActionResult.Success)
                        {
                            lblMsg.Text = @"Đổi phòng khám thành công";

                            InphieuKham();
                            Utility.Log(Name, globalVariables.UserName, string.Format("Đổi bệnh nhân {0} từ công khám {1}-{2} sang phòng {3}-{4}", objCongkham.MaLuotkham, objCongkham.IdDichvuKcb, grdRegExam.GetValue("ten_dichvukcb"), objDichvuKCB.IdDichvukcb, objDichvuKCB.TenDichvukcb), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                            Utility.ShowMsg("Bạn vừa thực hiện đổi Phòng khám và dịch vụ khám thành công. Nhấn OK để kết thúc");
                            grdList_SelectionChanged(grdList, e);

                        }
                    }
                    else//Xóa cái cũ và insert cái mới
                    {
                        Utility.ShowMsg("Chức năng đổi phòng khám và dịch vụ khám chỉ áp dụng cho các dịch vụ khám đã được thanh toán. Với dịch vụ bạn đang chọn chưa được thanh toán thì có thể xóa đi và thêm lại dịch vụ khác");
                    }
                    

                    
                }
            }
            catch (Exception ex)
            {
            }



        }

        private void mnuTrangthaicapcuu_Click(object sender, EventArgs e)
        {
            try
            {
                KcbLuotkham objluotkham = Utility.getKcbLuotkham(grdList.CurrentRow);
                if (Utility.Int32Dbnull(objluotkham.TrangthaiCapcuu, 0) == 0)
                {
                    if (Utility.AcceptQuestion("Bạn có chắc chắn muốn chuyển người bệnh từ trạng thái thường sang trạng thái cấp cứu hay không?", "Xác nhận", true))
                    {
                       int num= new Update(KcbLuotkham.Schema).Set(KcbLuotkham.Columns.TrangthaiCapcuu).EqualTo(1).Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objluotkham.MaLuotkham).Execute();
                       if (num > 0)
                       {
                           Utility.ShowMsg(string.Format("Cập nhật trạng thái cấp cứu cho người bệnh {0} thành công. Nhấn OK để kết thúc", grdList.GetValue("ten_benhnhan").ToString()));
                       }
                    }
                }
                else
                    Utility.ShowMsg("Người bệnh bạn chọn đang ở trạng thái cấp cứu nên không cần chuyển.");
            }
            catch (Exception ex)
            {
                
            }
        }

        private void mnuBarcode_Click(object sender, EventArgs e)
        {
            try
            {
                KcbLuotkham objluotkham = Utility.getKcbLuotkham(grdList.CurrentRow);
                if (objluotkham != null)
                {
                    QueryCommand cmd = SysMultiReport.CreateQuery().BuildCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandSql = string.Format("select * from v_kcb_luotkham where id_benhnhan={0} and ma_luotkham='{1}'", objluotkham.IdBenhnhan, objluotkham.MaLuotkham);
                    DataTable dt = DataService.GetDataSet(cmd).Tables[0];
                    if (!dt.Columns.Contains("barcodeID")) dt.Columns.Add("barcodeID", typeof(byte[]));
                    if (!dt.Columns.Contains("barcode")) dt.Columns.Add("barcode", typeof(byte[]));
                    dt.TableName = "dt_Barcode";
                    THU_VIEN_CHUNG.CreateXML(dt, "barcode.xml");
                    if (dt.Rows.Count > 0)
                    {
                        var frm = new FrmBarCodePrint(2);
                        frm.m_dtReport = dt;
                        frm.ShowDialog();
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void mnuInternalHistory_Click(object sender, EventArgs e)
        {
//frm_lichsubuonggiuong _lichsubuonggiuong=new frm_lichsubuonggiuong(
        }

        private void mnuNoithe_Click(object sender, EventArgs e)
        {
            if (objLuotkham != null)
            {
                frm_chuyendoituongkcb _chuyendoituongkcb = new frm_chuyendoituongkcb();
                _chuyendoituongkcb.ucThongtinnguoibenh_v21.txtMaluotkham.Text = objLuotkham.MaLuotkham;
                _chuyendoituongkcb.ShowDialog();
            }
            else
            {
                Utility.ShowMsg("Bạn cần chọn một người bệnh trước khi thực hiện nối thẻ(hoặc chuyển đối tượng KCB)");
            }
        }
        bool allowLoadGoikham = false;
        private void cmdDangkyGoi_Click(object sender, EventArgs e)
        {
            allowLoadGoikham = false;
            frm_QuanLyGoiKham dangkygoi = new frm_QuanLyGoiKham();
            dangkygoi.ucThongtinnguoibenh_doc_v71.txtMaluotkham.Text = objLuotkham.MaLuotkham;
            dangkygoi.ucThongtinnguoibenh_doc_v71.Refresh();
            dangkygoi.ShowDialog();
            LoadthongtinGoiKham();
        }
        void LoadthongtinGoiKham()
        {
            try
            {
                if (objLuotkham == null)
                {
                    cboGoi.Items.Clear();
                    cboGoi.Enabled = false;
                    return;
                }
                DataTable _dtGoiKhamTheoBNCaNhan = new clsGoikham().LayGoiKhamTheoBN(objLuotkham.IdBenhnhan, "-1");
                DataTable dtAvailable = _dtGoiKhamTheoBNCaNhan.Clone();
                var q = from p in _dtGoiKhamTheoBNCaNhan.AsEnumerable()
                        where Utility.Int32Dbnull(p["condichvu"], 0) > 0
                        && Utility.ByteDbnull(p["tthai_kichhoat"]) > 0
                        && Utility.ByteDbnull(p["tthai_ttoan"]) > 0
                        && Utility.ByteDbnull(p["tthai_huy"]) <= 0
                        select p;
                if (q.Any())
                    dtAvailable = q.CopyToDataTable();
                DataBinding.BindDataCombobox(cboGoi, dtAvailable, "ID", "ten_goi", "---Dịch vụ ngoài gói---", false, true);
                cboGoi.Enabled = objLuotkham != null && dtAvailable.Rows.Count > 0;
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            finally
            {
                allowLoadGoikham = true;
                if (cboGoi.Enabled)
                    if (cboGoi.Items.Count == 1) cboGoi.SelectedIndex = 0;
                    else if (cboGoi.Items.Count > 1) cboGoi.SelectedIndex = 1;
                    else
                        if (cboGoi.Items.Count <= 0) cboGoi.SelectedIndex = -1;
                        else cboGoi.SelectedIndex = 0;
                cboGoi_SelectedIndexChanged(cboGoi, new EventArgs());
            }
        }

        private void cboGoi_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!allowLoadGoikham) return;
            try
            {
                NapThongtinDichvuKCB();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void cmdDown_Click(object sender, EventArgs e)
        {
            byte STT = Utility.ByteDbnull(grdRegExam.CurrentRow.Cells["stt_tt37"].Value, 0);
            if (STT == grdRegExam.RowCount) return;//nút chắc chắn đã bị disable nên ko click được nhưng cứ bắt
            cmdDown.Enabled = STT == grdRegExam.RowCount - 1;
            DoiSTT(grdRegExam.CurrentRow, STT, Convert.ToByte(STT + 1), true);
            ModifyUpDownButton();
        }

        private void cmdUp_Click(object sender, EventArgs e)
        {
            byte STT = Utility.ByteDbnull(grdRegExam.CurrentRow.Cells["stt_tt37"].Value, 0);
            if (STT == 1) return;//nút chắc chắn đã bị disable nên ko click được nhưng cứ bắt
            cmdUp.Enabled = STT == 2;
            DoiSTT(grdRegExam.CurrentRow, STT,Convert.ToByte( STT - 1), true);
            ModifyUpDownButton();
        }
        bool DoiSTT(GridEXRow currentR, byte STT_cu, byte STT_moi, bool hoandoi)
        {
            try
            {
                long id_khamcu = -1;
                long id_khammoi = -1;
                var q = from p in m_dtDangkyPhongkham.AsEnumerable()
                        where Utility.Int32Dbnull(p["stt_tt37"], 0) == STT_moi
                        select p;
                if (q.Any())
                    id_khammoi = Utility.Int64Dbnull(q.First()["id_kham"], -1);
                q = from p in m_dtDangkyPhongkham.AsEnumerable()
                    where Utility.Int32Dbnull(p["stt_tt37"], 0) == STT_cu
                    select p;
                if (q.Any())
                    id_khamcu = Utility.Int64Dbnull(q.First()["id_kham"], -1);
                decimal tyle_tt = 100;
                tyle_tt = THU_VIEN_CHUNG.Bhyt_Laytyle_tt_congkham(THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb), STT_moi);
                if (!THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb)) tyle_tt = 100;
                int num = new Update(KcbDangkyKcb.Schema)
                    .Set(KcbDangkyKcb.Columns.TyleTt).EqualTo(tyle_tt)
                    .Set(KcbDangkyKcb.Columns.SttTt37).EqualTo(STT_moi)
                     .Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(id_khammoi).Execute();
                tyle_tt = THU_VIEN_CHUNG.Bhyt_Laytyle_tt_congkham(THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb), STT_cu);
                if (!THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb)) tyle_tt = 100;
                num = new Update(KcbDangkyKcb.Schema)
                    .Set(KcbDangkyKcb.Columns.TyleTt).EqualTo(tyle_tt)
                    .Set(KcbDangkyKcb.Columns.SttTt37).EqualTo(STT_cu)
                    .Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(id_khamcu).Execute();
                //Đổi trong datatable
                DataRow[] arrDr = null;
                //Tìm bản ghi có STT=số thứ tự mới và khác id chi tiết để hoán đổi số thứ tự với dòng hiện tại
                if (hoandoi)
                {
                    string filter = string.Format("{0}<>{1} and stt_tt37={2}", KcbDonthuocChitiet.Columns.IdKham, currentR.Cells[KcbDonthuocChitiet.Columns.IdKham].Value, STT_moi);
                    arrDr = m_dtDangkyPhongkham.Select(filter);
                    if (arrDr.Length > 0)
                    {
                        arrDr[0]["stt_tt37"] = STT_cu;
                    }
                }
                //Tìm bản ghi hiện tại để đổi STT
                ((DataRowView)currentR.DataRow).Row["stt_tt37"] = STT_moi;// > maxstt ? STT_moi : maxstt;
                m_dtDangkyPhongkham.AcceptChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        
        void ModifyUpDownButton()
        {
            bool canDown = Utility.isValidGrid(grdRegExam) && grdRegExam.RowCount > 1 && grdRegExam.CurrentRow.Position < grdRegExam.RowCount - 1;
            bool canUp = Utility.isValidGrid(grdRegExam) && grdRegExam.RowCount > 1 && grdRegExam.CurrentRow.Position > 0;

            cmdUp.Enabled = canUp;
            cmdDown.Enabled = canDown;
        }
        /// <summary>
        /// Hàm tự động cập nhật STT công khám(áp dụng đối với đối tượng BHYT). Khi cập nhật STT tự động cập nhật lại cả tỉ lệ và giá
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mnuUpdateSTT_Click(object sender, EventArgs e)
        {
            THU_VIEN_CHUNG.Bhyt_TudongCapnhatSTT_TyleTT_Congkham(objLuotkham);
            Utility.ShowMsg("Cập nhật toàn bộ thông tin STT, tỷ lệ thanh toán theo thông tư 37 của tất cả các công khám chưa thanh toán thành công. Nhấn Ok để Refresh lại dữ liệu");
            LayDanhsachCongkham();

        }

        private void mnuDoiphongdoTL_Click(object sender, EventArgs e)
        {

        }
        int num = 0;
        private void cmdDoiphongdoThiluc_Click(object sender, EventArgs e)
        {
            try
            {
                int id_kham = Utility.Int32Dbnull(grdRegExam.CurrentRow.Cells[KcbDangkyKcb.Columns.IdKham].Value, -1);
                objCongkham = KcbDangkyKcb.FetchByID(id_kham);
                if(!Utility.Byte2Bool( objCongkham.KhamThiluc))
                {
                    Utility.ShowMsg("Vui lòng chọn công khám đo thị lực để thay đổi");
                    return;
                }    
                if (Utility.Int16Dbnull(cboPhongkhamThiluc.SelectedValue) == Utility.Int16Dbnull(objCongkham.IdPhongkham))
                {
                    Utility.ShowMsg(string.Format("Bạn cần chọn phòng đo thị lực khác phòng đo hiện tại {0} trước khi thực hiện đổi phòng", grdRegExam.GetValue("ten_phongkham")));
                    return;
                }
                if (objCongkham.ThoigianBatdau.HasValue || objCongkham.ThoigianKetthuc.HasValue || objCongkham.TrangThai==1)
                {
                    Utility.ShowMsg(string.Format("Công khám {0} đã được bắt đầu thực hiện nên bạn không thể đổi phòng. Vui lòng liên hệ với bác sĩ để hủy khám nếu muốn đổi phòng đo khác", grdRegExam.GetValue("ten_dichvukcb")));
                    return;
                }
                if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn đổi phòng Đo thị lực từ {0} sang {1}? \r\n Chú ý: Số QMS ứng với phòng Đo thị lực mới sẽ được sinh ngay sau khi bạn chấp nhận", grdRegExam.GetValue("ten_phongkham"), cboPhongkhamThiluc.Text), "Xác nhận đổi phòng đo Thị lực", true))
                {
                   
                   
                    KCB_QMS _KCB_QMS = new KCB_QMS();
                    //Xóa QMS phòng đo thị lực hiện tại
                    if (objBenhnhan != null && objLuotkham != null && objCongkham != null && THU_VIEN_CHUNG.Laygiatrithamsohethong("QMSPK_ENABLE", "0", false) == "1")
                        _KCB_QMS.QmsPhongkhamDelete((int)objCongkham.SttKham.Value, objCongkham.MaPhongStt, objCongkham.MaLuotkham, objBenhnhan.IdBenhnhan, objCongkham.MaKhoaThuchien, (int)objCongkham.IdPhongkham, objCongkham.IdKham);
                    objCongkham.IdPhongkham = Utility.Int16Dbnull(cboPhongkhamThiluc.SelectedValue);
                    Int16 STT_QMS = THU_VIEN_CHUNG.LaySothutuKCB(Utility.Int32Dbnull(objCongkham.IdPhongkham, -1));
                    objCongkham.SttKham = STT_QMS;
                    //Lấy dịch vụ KCB ứng với phòng khám mới
                    DmucDichvukcb objDvuKcb =
                        new Select().From(DmucDichvukcb.Schema)
                        .Where(DmucDichvukcb.Columns.IdPhongkham).IsEqualTo(objCongkham.IdPhongkham)
                        .And(DmucDichvukcb.Columns.IdKieukham).IsEqualTo(objCongkham.IdKieukham)
                        .ExecuteSingle<DmucDichvukcb>();
                    DmucKhoaphong objKP = DmucKhoaphong.FetchByID(objCongkham.IdPhongkham);
                    string ten_dichvukcb = objDvuKcb != null ? objDvuKcb.TenDichvukcb : objCongkham.TenDichvuKcb;
                    num = new Update(KcbDangkyKcb.Schema)
                         .Set(KcbDangkyKcb.Columns.TenDichvuKcb).EqualTo(ten_dichvukcb)
                         .Set(KcbDangkyKcb.Columns.SttKham).EqualTo(STT_QMS)
                         .Set(KcbDangkyKcb.Columns.MaPhongStt).EqualTo(objKP.MaPhongStt)
                         .Set(KcbDangkyKcb.Columns.IdDichvuKcb).EqualTo(objDvuKcb.IdDichvukcb)
                          .Set(KcbDangkyKcb.Columns.IdPhongkham).EqualTo(objCongkham.IdPhongkham)
                         .Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(objCongkham.IdKham)
                         .Execute();
                    
                    if (objBenhnhan != null && objLuotkham != null && objCongkham != null && THU_VIEN_CHUNG.Laygiatrithamsohethong("QMSPK_ENABLE", "0", false) == "1")
                        _KCB_QMS.QmsPhongkhamInsert((int)objCongkham.SttKham.Value, objCongkham.MaPhongStt, DateTime.Now, objCongkham.NgayTiepdon.Value, objCongkham.MaLuotkham, objBenhnhan.IdBenhnhan, objBenhnhan.TenBenhnhan, (int)objBenhnhan.NamSinh, Utility.Int32Dbnull(objBenhnhan.NamSinh, 0), objBenhnhan.GioiTinh, objCongkham.MaKhoaThuchien, (int)objCongkham.IdPhongkham, objCongkham.IdKham, (int)objCongkham.IdDichvuKcb, objCongkham.TenDichvuKcb);
                    Utility.Log(Name, globalVariables.UserName, string.Format("Đổi phòng đo Thị lực từ {0} sang {1} với số QMS mới {2} cho người bệnh mã khám ={3}, tên ={4}", grdRegExam.GetValue("ten_phongkham"), cboPhongkhamThiluc.Text, STT_QMS, objBenhnhan.TenBenhnhan, objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                    grdList_SelectionChanged(grdList, e);
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void mnuCapnhatNguonGT_Doitac_Click(object sender, EventArgs e)
        {
            if (Utility.isValidGrid(grdList) && objBenhnhan != null && objLuotkham != null)
            {
                frm_CapnhatNguon_Doitac _CapnhatNguon_Doitac = new frm_CapnhatNguon_Doitac();
                _CapnhatNguon_Doitac._objLuotkham = this.objLuotkham;
                _CapnhatNguon_Doitac.objBenhnhan = this.objBenhnhan;
                _CapnhatNguon_Doitac.drData = ((DataRowView)grdList.CurrentRow.DataRow).Row;
                _CapnhatNguon_Doitac.ShowDialog();
            }
        }

        private void mnuSHS_Click(object sender, EventArgs e)
        {
            foreach(GridEXRow _row in grdList.GetCheckedRows())
            {
                try
                {
                    long id_benhnhan = Utility.Int64Dbnull(_row.Cells["id_benhnhan"].Value,-1);
                    string ma_luotkham = Utility.sDbnull(_row.Cells["ma_luotkham"].Value, "-1");
                    int num = SPs.TiepdonShs(id_benhnhan, ma_luotkham, 1).Execute();
                    Utility.Log(Name, globalVariables.UserName, string.Format("SHOW người bệnh với thông tin id={0},ma_luotkham={1}", id_benhnhan, ma_luotkham), newaction.View, this.GetType().Assembly.ManifestModule.Name);
                }
                catch (Exception ex)
                {

                    Utility.CatchException(ex);
                }
            }    
        }

        private void mnuHide_Click(object sender, EventArgs e)
        {
            foreach (GridEXRow _row in grdList.GetCheckedRows())
            {
                try
                {
                    long id_benhnhan = Utility.Int64Dbnull(_row.Cells["id_benhnhan"].Value, -1);
                    string ma_luotkham = Utility.sDbnull(_row.Cells["ma_luotkham"].Value, "-1");
                    int num = SPs.TiepdonShs(id_benhnhan, ma_luotkham, 0).Execute();
                    Utility.Log(Name, globalVariables.UserName, string.Format("HIDE người bệnh với thông tin id={0},ma_luotkham={1}", id_benhnhan, ma_luotkham), newaction.View, this.GetType().Assembly.ManifestModule.Name);
                }
                catch (Exception ex)
                {

                    Utility.CatchException(ex);
                }
            }
        }

        private void mnuShowHideManagement_Click(object sender, EventArgs e)
        {
            VMS.HIS.Ngoaitru.frmSHS _frmSHS = new VMS.HIS.Ngoaitru.frmSHS();
            _frmSHS.ShowDialog();
        }
    }
}
