using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using VNS.HIS.UI.NGOAITRU;
using VNS.Libs;
using Janus.Windows.GridEX.EditControls;
using VMS.HIS.DAL;
using VNS.HIS.BusRule.Classes;
using SubSonic;
using VNS.HIS.UI.DANHMUC;
using System.Transactions;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using VNS.HIS.UI.Forms.Cauhinh;
namespace VNS.HIS.UI.Forms.NGOAITRU
{
    public partial class frm_chuyenvien : Form
    {
        bool AllowTextChanged = false;
        KcbPhieuchuyenvien objPhieuchuyenvien = null;
        KcbLuotkham objLuotkham = null;
        public action m_enAct = action.Insert;
        public byte noitru = 0;
        public string ma_huongdieutri = "-1";
        public int idbacsikham = -1;
        public frm_chuyenvien()
        {
            InitializeComponent();
            InitEvents();
        }
        void InitEvents()
        {
            this.KeyDown += new KeyEventHandler(frm_chuyenvien_KeyDown);
            this.Load += new EventHandler(frm_chuyenvien_Load);
            FormClosing+=frm_chuyenvien_FormClosing;
            Shown += frm_chuyenvien_Shown;
            txtphuongtienvc._OnSaveAsV1 += _OnSaveAsV1;
            txtphuongtienvc._OnShowDataV1 += _OnShowDataV1;

            txtTrangthainguoibenh._OnSaveAsV1 += _OnSaveAsV1;
            txtTrangthainguoibenh._OnShowDataV1 += _OnShowDataV1;

            txtdauhieucls._OnSaveAsV1 += _OnSaveAsV1;
            txtdauhieucls._OnShowDataV1 += _OnShowDataV1;
            txtHuongdieutri._OnSaveAsV1 += _OnSaveAsV1;
            txtHuongdieutri._OnShowDataV1 += _OnShowDataV1;

            txtHuongdieutri._OnSaveAsV1 += _OnSaveAsV1;
            txtHuongdieutri._OnShowDataV1 += _OnShowDataV1;

            cmdExit.Click += new EventHandler(cmdExit_Click);
            cmdChuyen.Click += new EventHandler(cmdChuyen_Click);
            cmdHuy.Click += new EventHandler(cmdHuy_Click);

            cmdPrint.Click += new EventHandler(cmdPrint_Click);
            AutoMabenhchinh._OnEnterMe += AutoMabenhchinh__OnEnterMe;
            txtNguoivanchuyen._OnShowData += txtNguoivanchuyen__OnShowData;
            ucThongtinnguoibenh1._OnEnterMe += ucThongtinnguoibenh1__OnEnterMe;
            txtNoichuyenden._OnEnterMe += txtNoichuyenden__OnEnterMe;
        }

        void _OnShowDataV1(UCs.AutoCompleteTextbox_Danhmucchung obj)
        {
            DMUC_DCHUNG dmucDchung = new DMUC_DCHUNG(obj.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = obj.myCode;
                obj.Init();
                obj.SetCode(oldCode);
                obj.Focus();
            } 
        }

        void _OnSaveAsV1(UCs.AutoCompleteTextbox_Danhmucchung obj)
        {
            if (Utility.DoTrim(obj.Text) == "") return;
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(obj.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtdauhieucls.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = obj.Text;
                obj.Init();
                obj._Text = oldCode;
                obj.Focus();
            }    
        }

        void frm_chuyenvien_Shown(object sender, EventArgs e)
        {
            
        }
        void LoadUserConfigs()
        {
            try
            {

                chkInsaukhiluu.Checked = Utility.getUserConfigValue(chkInsaukhiluu.Tag.ToString(), Utility.Bool2byte(chkInsaukhiluu.Checked)) == 1;
                chkThoatsaukhiluu.Checked = Utility.getUserConfigValue(chkThoatsaukhiluu.Tag.ToString(), Utility.Bool2byte(chkThoatsaukhiluu.Checked)) == 1;
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            finally
            {
                Application.DoEvents();
            }
        }
        void SaveUserConfigs()
        {
            try
            {

                Utility.SaveUserConfig(chkInsaukhiluu.Tag.ToString(), Utility.Bool2byte(chkInsaukhiluu.Checked));
                Utility.SaveUserConfig(chkThoatsaukhiluu.Tag.ToString(), Utility.Bool2byte(chkThoatsaukhiluu.Checked));
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        void frm_chuyenvien_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveUserConfigs();
        }

        void txtNoichuyenden__OnEnterMe()
        {
            txtIdNoichuyenden.Text = txtNoichuyenden.MyID.ToString();
        }

        void ucThongtinnguoibenh1__OnEnterMe()
        {
            this.objLuotkham = ucThongtinnguoibenh1.objLuotkham;
            if (objLuotkham.TrangthaiNoitru >= 1)//Kiểm tra nếu nội trú và chưa làm phiếu ra viện thì không được phép chuyển viện
            {
                DataTable dtPrv = new Select().From(NoitruPhieuravien.Schema)
                     .Where(NoitruPhieuravien.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                     .And(NoitruPhieuravien.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                     .ExecuteDataSet().Tables[0];
                if (dtPrv.Rows.Count <= 0)
                {
                    Utility.ShowMsg(string.Format("Người bệnh {0} đang ở trạng thái nội trú nên cần làm phiếu ra viện trước khi làm phiếu chuyển viện", ucThongtinnguoibenh1.txtTenBN.Text));
                    cmdChuyen.Enabled = false;
                    return;
                }
                cmdChuyen.Enabled = true;
            }
            Napthongtinphieuchuyenvien();
        }

        void txtNguoivanchuyen__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtNguoivanchuyen.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtNguoivanchuyen.Text;
                txtNguoivanchuyen.Init();
                txtNguoivanchuyen._Text = oldCode;
                txtNguoivanchuyen.Focus();
            }    
        }

        void AutoMabenhchinh__OnEnterMe()
        {
            if (!txtChandoan.Text.Contains(AutoMabenhchinh.MyText) || Utility.DoTrim(txtChandoan.Text).Length<=0)
                txtChandoan.AppendText(AutoMabenhchinh.MyText);
        }

        void cmdPrint_Click(object sender, EventArgs e)
        {
              Utility.WaitNow(this);
              long id_phieu = Utility.Int64Dbnull(txtId.Text);
                DataTable dtData =
                                 SPs.KcbThamkhamPhieuchuyenvien(id_phieu, Utility.DoTrim(ucThongtinnguoibenh1.txtMaluotkham.Text)).GetDataSet().Tables[0];

                if (dtData.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu cho báo cáo", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                THU_VIEN_CHUNG.CreateXML(dtData, "thamkhamPhieuchuyenvien.XML");
                Utility.UpdateLogotoDatatable(ref dtData);
                string StaffName = globalVariables.gv_strTenNhanvien;
                if (string.IsNullOrEmpty(globalVariables.gv_strTenNhanvien)) StaffName = globalVariables.UserName;

                string tieude = "", reportname = "";
                ReportDocument crpt = Utility.GetReport("thamkhamPhieuchuyenvien", ref tieude, ref reportname);
                if (crpt == null) return;
                try
                {
             
                frmPrintPreview objForm = new frmPrintPreview("PHIẾU CHUYỂN TUYẾN", crpt, true, dtData.Rows.Count <= 0 ? false : true);
                crpt.SetDataSource(dtData);
               
                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = "thamkhamPhieuchuyenvien";
                Utility.SetParameterValue(crpt, "StaffName", StaffName);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "Phone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "CurrentDate", Utility.FormatDateTimeWithThanhPho(dtpNgayin.Value));
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                Utility.SetParameterValue(crpt, "txtTrinhky", Utility.getTrinhky(objForm.mv_sReportFileName, globalVariables.SysDate));
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
                 GC.Collect();
                 Utility.FreeMemory(crpt);
            }
        }
        void cmdGetBV_Click(object sender, EventArgs e)
        {
            frm_danhsachbenhvien _danhsachbenhvien = new frm_danhsachbenhvien();
            if (_danhsachbenhvien.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                txtNoichuyenden.SetId(_danhsachbenhvien.idBenhvien);
            }
            
        }

       

        void cmdHuy_Click(object sender, EventArgs e)
        {
            if (!globalVariables.isSuperAdmin)
            {
                if (!Utility.Coquyen("noitru_chuyenvien_quyenhuy"))
                {
                    Utility.ShowMsg("Bạn không có quyền hủy phiếu chuyển viện(noitru_chuyenvien_quyenhuy). Đề nghị liên hệ quản trị hệ thống để được cấp quyền");
                    return;
                }
                if (objLuotkham == null)
                {
                    Utility.SetMsg(lblMsg, "Bạn cần chọn bệnh nhân trước khi thực hiện hủy chuyển viện", true);
                    return;
                }
                objLuotkham = Utility.getKcbLuotkham(objLuotkham);
                if (objLuotkham.TrangthaiNoitru == 4)
                {
                    Utility.SetMsg(lblMsg,
                        "Bệnh nhân đã được xác nhận dữ liệu nội trú để ra viện nên bạn không thể hủy chuyển viện", true);
                    return;
                }
                if (objLuotkham.TrangthaiNoitru == 5)
                {
                    Utility.SetMsg(lblMsg,
                        "Bệnh nhân đã được duyệt thanh toán nội trú để ra viện nên bạn không thể hủy chuyển viện", true);
                    return;
                }
                if (objLuotkham.TrangthaiNoitru == 6)
                {
                    Utility.SetMsg(lblMsg,
                        "Bệnh nhân đã kết thúc điều trị nội trú(Đã thanh toán xong) nên bạn không thể hủy chuyển viện", true);
                    return;
                }
            }
            if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn hủy chuyển viện cho bệnh nhân {0} hay không?", ucThongtinnguoibenh1.txtTenBN.Text), "Xác nhận hủy chuyển viện", true))
            {
                try
                {
                    using (var scope = new TransactionScope())
                    {
                        using (var dbscope = new SharedDbConnectionScope())
                        {
                            objLuotkham.TthaiChuyendi=0;
                            objLuotkham.IdBenhvienDi=-1;
                            objLuotkham.IdBacsiChuyenvien = -1;
                            //objLuotkham.NgayRavien = null;
                            objLuotkham.IsNew=false;
                            objLuotkham.MarkOld();
                            objLuotkham.Save();
                            new Delete().From(KcbPhieuchuyenvien.Schema).Where(KcbPhieuchuyenvien.Columns.IdPhieu).IsEqualTo(Utility.Int32Dbnull(txtId.Text, -1)).Execute();
                            new Delete().From(KcbChandoanKetluan.Schema)
                                .Where(KcbChandoanKetluan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                                .And(KcbChandoanKetluan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                                .And(KcbChandoanKetluan.Columns.KieuChandoan).IsEqualTo(3)
                                .Execute();
                        }
                        scope.Complete();

                        Utility.SetMsg(lblMsg, string.Format("Hủy chuyển viện cho bệnh nhân {0} thành công", ucThongtinnguoibenh1.txtTenBN.Text), true);
                        Utility.ShowMsg(lblMsg.Text);
                        m_enAct = action.Insert;
                        cmdNew.PerformClick();
                        
                    }
                }
                catch (Exception ex)
                {
                    Utility.CatchException(ex);
                }
            }
        }

        void cmdChuyen_Click(object sender, EventArgs e)
        {
            Utility.SetMsg(lblMsg, "", false);
            errorProvider1.Clear();
            //Check số chuyển tuyến
            DataTable dtPCV = SPs.KcbPhieuchuyenvienKiemtraSochuyenvien(objPhieuchuyenvien!=null? objPhieuchuyenvien.IdPhieu:-1, Utility.sDbnull(txtsochuyenvien.Text)).GetDataSet().Tables[0];
            if (dtPCV.Rows.Count > 0)
            {
                Utility.SetMsg(lblMsg, string.Format("Số phiếu {0} đã được sử dụng cho người bệnh khác. Vui lòng nhập số mới hoặc nhấn nút Refresh bên cạnh để lấy số mới nhất", Utility.sDbnull(txtsochuyenvien.Text)), true);
                errorProvider1.SetError(txtsochuyenvien, lblMsg.Text);
                txtsochuyenvien.Focus();
                return;
            }
            //Kiểm tra lại trạng thái đề phòng có người khác đã thao tác các bước sau như chuyển nội trú, phân buồng giường
            if (txtNoichuyenden.MyCode == "-1")
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin nơi chuyển đến", true);
                errorProvider1.SetError(txtNoichuyenden, lblMsg.Text);
                txtNoichuyenden.Focus();
                return;
            }
            if (Utility.DoTrim(txtdauhieucls.Text) == "")
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin dấu hiệu lâm sàng", true);
                errorProvider1.SetError(txtdauhieucls, lblMsg.Text);
                txtdauhieucls.Focus();
                return;
            }
            //if (Utility.DoTrim(txtketquaCls.Text) == "")
            //{
            //    Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin kết quả xét nghiệm, cận lâm sàng", true);
            //    txtketquaCls.Focus();
            //    return;
            //}
            //if (Utility.DoTrim(AutoMabenhchinh.MyCode) == "-1")
            //{
            //    Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin mã bệnh chính", true);
            //    errorProvider1.SetError(AutoMabenhchinh, lblMsg.Text);
            //    AutoMabenhchinh.Focus();
            //    return;
            //}
            if (Utility.DoTrim(txtChandoan.Text) == "" )
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin chẩn đoán", true);
                errorProvider1.SetError(txtChandoan, lblMsg.Text);
                txtChandoan.Focus();
                return;
            }
            //if (Utility.DoTrim(txtThuocsudung.Text) == "")
            //{
            //    Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin Phương pháp, thủ thuật, kỹ thuật, thuốc đã sử dụng trong điều trị:", true);
            //    txtThuocsudung.Focus();
            //    return;
            //}
            if (Utility.DoTrim(txtTrangthainguoibenh.Text) == "")
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin trạng thái người bệnh", true);
                errorProvider1.SetError(txtTrangthainguoibenh, lblMsg.Text);
                txtTrangthainguoibenh.Focus();
                return;
            }
            if (Utility.DoTrim(txtHuongdieutri.Text) == "")
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin hướng điều trị", true);
                errorProvider1.SetError(txtHuongdieutri, lblMsg.Text);
                txtHuongdieutri.Focus();
                return;
            }

            //if (Utility.DoTrim(txtphuongtienvc.Text) == "")
            //{
            //    Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin phương tiện vận chuyển", true);
            //    txtphuongtienvc.Focus();
            //    return;
            //}
            //if (Utility.DoTrim(txtNguoivanchuyen.Text) == "")
            //{
            //    Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin người vận chuyển", true);
            //    txtNguoivanchuyen.Focus();
            //    return;
            //}

            try
            {
               
                string sochuyenviencu = txtsochuyenvien.Text;
                string sochuyenvienmoi = "";
                if (objPhieuchuyenvien == null) objPhieuchuyenvien = new KcbPhieuchuyenvien();
                if (objPhieuchuyenvien.IdPhieu<=0)
                {
                    objPhieuchuyenvien.IsNew = true;
                    objPhieuchuyenvien.NgayTao = globalVariables.SysDate;
                    objPhieuchuyenvien.NguoiTao = globalVariables.UserName;
                   
                }
                else
                {
                    objPhieuchuyenvien = KcbPhieuchuyenvien.FetchByID(Utility.Int32Dbnull(txtId.Text));
                    objPhieuchuyenvien.IsNew = false;
                    objPhieuchuyenvien.MarkOld();
                    objPhieuchuyenvien.NguoiSua = globalVariables.UserName;
                    objPhieuchuyenvien.NgaySua = globalVariables.SysDate;
                }
                objPhieuchuyenvien.TuyenChuyen = Utility.ByteDbnull(optTuyentren.Checked ? 1 : (optTuyenduoi.Checked ? 2 : 3),1);
                objPhieuchuyenvien.SoChuyentuyen = txtsochuyenvien.Text;
                objPhieuchuyenvien.IdBenhnhan = objLuotkham.IdBenhnhan;
                objPhieuchuyenvien.MaLuotkham = objLuotkham.MaLuotkham;
                objPhieuchuyenvien.IdBenhvienChuyenden =Utility.Int16Dbnull( txtNoichuyenden.MyID,-1);
                objPhieuchuyenvien.DauhieuCls = Utility.DoTrim(txtdauhieucls.Text);
                objPhieuchuyenvien.KetquaXnCls = Utility.DoTrim(txtketquaCls.Text);
                objPhieuchuyenvien.ChanDoan = Utility.DoTrim(txtChandoan.Text);
                objPhieuchuyenvien.Mabenh =AutoMabenhchinh.MyCode;
                objPhieuchuyenvien.ThuocSudung = Utility.DoTrim(txtThuocsudung.Text);
                objPhieuchuyenvien.TrangthaiBenhnhan = Utility.DoTrim(txtTrangthainguoibenh.Text);
                objPhieuchuyenvien.HuongDieutri = Utility.DoTrim(txtHuongdieutri.Text);
                objPhieuchuyenvien.LydoChuyen = Utility.ByteDbnull(radDuDieukien.Checked ? "1" : "0");
                objPhieuchuyenvien.PhuongtienChuyen = Utility.DoTrim(txtphuongtienvc.Text);
                objPhieuchuyenvien.NgayChuyenvien = dtNgaychuyenvien.Value;
                objPhieuchuyenvien.IdBacsiChuyenvien = Utility.Int16Dbnull(txtBacsi.MyID, -1);
                objPhieuchuyenvien.TenNguoichuyen = Utility.DoTrim(txtNguoivanchuyen.Text);
                objPhieuchuyenvien.NoiTru = noitru;
                objPhieuchuyenvien.IdRavien = Utility.Int32Dbnull(txtIdravien.Text,-1);
                objPhieuchuyenvien.IdKhoanoitru = Utility.Int32Dbnull(txtIdkhoanoitru.Text, -1);
                objPhieuchuyenvien.IdBuong = Utility.Int32Dbnull(txtidBuong.Text, -1);
                objPhieuchuyenvien.IdGiuong = Utility.Int32Dbnull(txtidgiuong.Text, -1);
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        objPhieuchuyenvien.Save();
                        //if (objPhieuchuyenvien.IdRavien > 0)
                        //{
                        //    NoitruPhieuravien objravien = NoitruPhieuravien.FetchByID(objPhieuchuyenvien.IdRavien);
                        //    if (objravien != null)
                        //    {
                        //        objravien.IdBenhvienDi = objPhieuchuyenvien.IdBenhvienChuyenden;
                        //        objravien.Save();
                        //    }
                        //}
                        objLuotkham.TthaiChuyendi = 1;
                        objLuotkham.IdBacsiChuyenvien = objPhieuchuyenvien.IdBacsiChuyenvien;
                        objLuotkham.MaBenhravien = objPhieuchuyenvien.Mabenh;
                        objLuotkham.NgayKetthuc = objPhieuchuyenvien.NgayChuyenvien;
                        objLuotkham.NguoiKetthuc = objPhieuchuyenvien.NguoiTao;
                        objLuotkham.NgayRavien = objPhieuchuyenvien.NgayChuyenvien;
                        objLuotkham.KetLuan = "Chuyển viện";
                        //objLuotkham.HuongDieutri = "Chuyển viện";
                        objLuotkham.IdBenhvienDi = Utility.Int16Dbnull(txtNoichuyenden.MyID,-1);
                        objLuotkham.IsNew = false;
                        objLuotkham.MarkOld();
                        objLuotkham.Save();
                        KcbChandoanKetluan objChuandoanKetluan =
                          new Select().From(KcbChandoanKetluan.Schema).Where(KcbChandoanKetluan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                          .And(KcbChandoanKetluan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                          .And(KcbChandoanKetluan.Columns.KieuChandoan).IsEqualTo(3)//Lấy chẩn đoán chuyển viện
                          .ExecuteSingle<KcbChandoanKetluan>();
                        if (objChuandoanKetluan != null)
                        {
                            new Update(KcbChandoanKetluan.Schema)
                                .Set(KcbChandoanKetluan.Columns.MabenhChinh).EqualTo(objLuotkham.MabenhChinh)
                         .Where(KcbChandoanKetluan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                         .And(KcbChandoanKetluan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                         .And(KcbChandoanKetluan.Columns.Noitru).IsEqualTo(0)
                         .And(KcbChandoanKetluan.Columns.KieuChandoan).IsEqualTo(3)
                         .Execute();

                        }
                        else
                        {
                            objChuandoanKetluan = new KcbChandoanKetluan();
                            objChuandoanKetluan.IdBenhnhan = Utility.Int64Dbnull(objLuotkham.IdBenhnhan);
                            objChuandoanKetluan.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham, "");
                            objChuandoanKetluan.SongayDieutri = 1;
                            objChuandoanKetluan.Chandoan = txtChandoan.Text;
                            objChuandoanKetluan.KieuChandoan = 3;
                            objChuandoanKetluan.MabenhChinh = objLuotkham.MabenhChinh;
                            objChuandoanKetluan.NgayChandoan = globalVariables.SysDate;
                            objChuandoanKetluan.NguoiTao = globalVariables.UserName;
                            objChuandoanKetluan.IdBacsikham = globalVariables.gv_intIDNhanvien;
                            objChuandoanKetluan.IpMaytao = globalVariables.gv_strIPAddress;
                            objChuandoanKetluan.Noitru = 0;
                            objChuandoanKetluan.IsNew = true;
                            objChuandoanKetluan.Save();
                        }
                        //Tạm khóa tránh update khi chuyển viện nội trú
                        //new Update(KcbDangkyKcb.Schema).Set(KcbDangkyKcb.Columns.TrangThai).EqualTo(1).Where(
                        //    KcbDangkyKcb.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).And(
                        //        KcbDangkyKcb.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).Execute();
                    }
                    scope.Complete();
                }
                sochuyenvienmoi = objPhieuchuyenvien.SoChuyentuyen;
                if (sochuyenviencu != sochuyenvienmoi)
                    Utility.ShowMsg(string.Format("Cập nhật thông tin phiếu chuyển viện thành công với số phiếu chuyển viện:{0} khác so với số phiếu chuyện viện lúc bạn chọn người bệnh để bắt đầu thực hiện: {1}.\nLý do: Trong lúc bạn thực hiện đã có nhân viên khác cùng thao tác chức năng chuyển tuyến và thao tác lưu trước bạn.", sochuyenviencu, sochuyenvienmoi));
                else
                {
                   
                    Utility.SetMsg(lblMsg, "Cập nhật phiếu chuyển viện thành công", false);
                    Utility.ShowMsg(lblMsg.Text);
                }
                if (m_enAct == action.Insert)
                {
                    cmdPrint.Enabled = true;
                    cmdHuy.Enabled = true;
                }
                m_blnCancel = false;
                m_enAct = action.Update;
                txtId.Text = objPhieuchuyenvien.IdPhieu.ToString();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);   
            }
        }
        public bool m_blnCancel = true;
        void cmdExit_Click(object sender, EventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

     
       
        void frm_chuyenvien_Load(object sender, EventArgs e)
        {
            try
            {
                LoadUserConfigs();
                LaydanhsachBacsi();
                AutocompleteBenhvien();
                txtphuongtienvc.Init();
                txtTrangthainguoibenh.Init();
                txtHuongdieutri.Init();
                txtdauhieucls.Init();
                txtHuongdieutri.SetCode(ma_huongdieutri);
                txtBacsi.SetId(idbacsikham);
                dtNgaychuyenvien.Value = DateTime.Now;
                dtpNgayin.Value = DateTime.Now;
                if (Utility.sDbnull(ucThongtinnguoibenh1.txtMaluotkham.Text).Length > 0)
                    ucThongtinnguoibenh1.Refresh();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        private bool hasMorethanOne = true;
        private bool isLike = true;
       
       
        private void LaydanhsachBacsi()
        {
            try
            {
                txtBacsi.Init(globalVariables.gv_dtDmucNhanvien,
                              new List<string>
                                  {
                                      DmucNhanvien.Columns.IdNhanvien,
                                      DmucNhanvien.Columns.MaNhanvien,
                                      DmucNhanvien.Columns.TenNhanvien
                                  });
                if (globalVariables.gv_intIDNhanvien <= 0)
                {
                    txtBacsi.SetId(-1);
                }
                else
                {
                    txtBacsi.SetId(globalVariables.gv_intIDNhanvien);
                }
            }
            catch (Exception exception)
            {
                // throw;
            }

        }
        void frm_chuyenvien_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ProcessTabKey(true);
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.Control && e.KeyCode == Keys.S) cmdChuyen.PerformClick();
            if (e.Control && e.KeyCode == Keys.P) cmdPrint.PerformClick();
        }
        public static  int GetmaxSoChuyenVien()
        {
            int sochuyenvien = -1;
            sochuyenvien = new
        Select(Aggregate.Max(KcbPhieuchuyenvien.SoChuyentuyenColumn))
        .From(KcbPhieuchuyenvien.Schema)
        .ExecuteScalar<int>();
            return sochuyenvien + 1;
        }
        /// <summary>
        /// chủ yếu chạy cho trươngf hợp ngoại trú
        /// </summary>
        /// <param name="mabenh_chinh"></param>
        /// <param name="chan_doan"></param>
        void AutofillChandoan(string mabenh_chinh,string chan_doan)
        {
            try
            {
                string s = string.Format("[{0}] {1}", mabenh_chinh, chan_doan);
                DataTable dtchandoan = new Select().From(KcbChandoanKetluan.Schema).Where(KcbChandoanKetluan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                    .And(KcbChandoanKetluan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).ExecuteDataSet().Tables[0];
                string chandoan = "";
                foreach (DataRow dr in dtchandoan.Rows)
                {
                    string icdName = "";
                    string icdCode = "";
                    GetChanDoan(Utility.sDbnull(dr["mabenh_chinh"], ""), Utility.sDbnull(dr["mabenh_phu"], ""), ref icdName, ref icdCode);
                    chandoan += icdName;
                }
                txtChandoan.Text = s + ';' + chandoan; 

            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        private void GetChanDoan(string icdChinh, string idcPhu, ref string icdName, ref string icdCode)
        {
            try
            {
                List<string> lstIcd = icdChinh.Split(',').ToList();
                List<DataRow> query = (from bc in globalVariables.gv_dtDmucBenh.AsEnumerable()
                                       where lstIcd.Contains(Utility.sDbnull(bc[DmucBenh.Columns.MaBenh]))
                                       select bc).ToList();
                foreach (DataRow item in query)
                {
                    icdName += item[DmucBenh.Columns.TenBenh] + ";";
                    icdCode += item[DmucBenh.Columns.MaBenh] + ";";
                }

                lstIcd = idcPhu.Split(',').ToList();
                query = (from bp in globalVariables.gv_dtDmucBenh.AsEnumerable()
                         where lstIcd.Contains(Utility.sDbnull(bp[DmucBenh.Columns.MaBenh]))
                         select bp).ToList();
                foreach (DataRow item in query)
                {
                    icdName += item[DmucBenh.Columns.TenBenh] + ";";
                    icdCode += item[DmucBenh.Columns.MaBenh] + ";";
                }
                if (icdName.Trim() != "") icdName = icdName.Substring(0, icdName.Length - 1);
                if (icdCode.Trim() != "") icdCode = icdCode.Substring(0, icdCode.Length - 1);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        public void Napthongtinphieuchuyenvien()
        {
            try
            {
                var dtPatient = new DataTable();
                ClearControls();
                DataTable dt_Patient = new KCB_THAMKHAM().TimkiemThongtinBenhnhansaukhigoMaBN(ucThongtinnguoibenh1.txtMaluotkham.Text, -1, globalVariables.MA_KHOA_THIEN);
                if (dt_Patient != null && dt_Patient.Rows.Count > 0)
                {
                    cmdChuyen.Enabled = true;
                    txtIdkhoanoitru.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.IdKhoanoitru], "-1");
                    txtIdravien.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.IdRavien], "-1");
                    txtidBuong.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.IdBuong], "-1");
                    txtidgiuong.Text = Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.IdGiuong], "-1");
                    txtThuocsudung.Text = Utility.sDbnull(dt_Patient.Rows[0]["thuoc_sudung"], "");
                    AutoMabenhchinh.SetCode(Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.MabenhChinh], ""));
                    AutofillChandoan(Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.MabenhChinh], ""), Utility.sDbnull(dt_Patient.Rows[0][KcbLuotkham.Columns.ChanDoan], ""));
                    txtketquaCls.Text = Utility.sDbnull(dt_Patient.Rows[0]["ketqua_cls"], "");
                    objPhieuchuyenvien = new Select().From(KcbPhieuchuyenvien.Schema)
                       .Where(KcbPhieuchuyenvien.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                       .And(KcbPhieuchuyenvien.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                       .ExecuteSingle<KcbPhieuchuyenvien>();
                    if (objPhieuchuyenvien != null)
                    {
                        optTuyentren.Checked = Utility.ByteDbnull(objPhieuchuyenvien.TuyenChuyen, 1) == 1;
                        optTuyenduoi.Checked = Utility.ByteDbnull(objPhieuchuyenvien.TuyenChuyen, 1) == 2;
                        optCK.Checked = Utility.ByteDbnull(objPhieuchuyenvien.TuyenChuyen, 1) == 3;
                        txtId.Text = objPhieuchuyenvien.IdPhieu.ToString();
                        txtsochuyenvien.Text = Utility.sDbnull(objPhieuchuyenvien.SoChuyentuyen);
                        txtNoichuyenden.SetId(objPhieuchuyenvien.IdBenhvienChuyenden);
                        txtIdNoichuyenden.Text = objPhieuchuyenvien.IdBenhvienChuyenden.ToString();
                        txtdauhieucls._Text = objPhieuchuyenvien.DauhieuCls;
                        txtketquaCls.Text = objPhieuchuyenvien.KetquaXnCls;
                        txtChandoan.Text = Utility.sDbnull(objPhieuchuyenvien.ChanDoan, "");
                        AutoMabenhchinh.SetCode(Utility.sDbnull(objPhieuchuyenvien.Mabenh, ""));
                        txtThuocsudung.Text = objPhieuchuyenvien.ThuocSudung;
                        txtTrangthainguoibenh._Text = objPhieuchuyenvien.TrangthaiBenhnhan;
                        txtHuongdieutri._Text = objPhieuchuyenvien.HuongDieutri;
                        txtphuongtienvc._Text = objPhieuchuyenvien.PhuongtienChuyen;
                        txtNguoivanchuyen._Text = objPhieuchuyenvien.TenNguoichuyen;
                        txtBacsi.SetId(Utility.Int32Dbnull(objPhieuchuyenvien.IdBacsiChuyenvien, -1));
                        cmdPrint.Enabled = true;
                        cmdHuy.Enabled = true;
                    }
                    else
                    {
                        txtsochuyenvien.Text = Laysochuyenvien();
                        cmdPrint.Enabled = false;
                        cmdHuy.Enabled = false;
                    }
                    m_enAct = objPhieuchuyenvien == null ? action.Insert : action.Update;
                    if (m_enAct == action.Insert)
                        cmdPrint.Enabled = false;
                    else
                        cmdPrint.Enabled = true;
                    dtNgaychuyenvien.Focus();
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            
        }
        
        private string Laysochuyenvien()
        {
            string sochuyenvien = "";
            StoredProcedure sp = SPs.SpGetSoChuyenVien(DateTime.Now.Year, sochuyenvien);
            sp.Execute();
           return Utility.sDbnull(sp.OutputValues[0], "-1");
        }
        private void AutocompleteBenhvien()
        {
            
            DataTable m_dtBenhvien = new Select().From(DmucBenhvien.Schema).Where(DmucBenhvien.Columns.Trangthai).IsEqualTo(1).OrderAsc(DmucBenhvien.Columns.SttHthi).ExecuteDataSet().Tables[0];
            txtNoichuyenden.Init(m_dtBenhvien, new List<string>() { DmucBenhvien.Columns.IdBenhvien, DmucBenhvien.Columns.MaBenhvien, DmucBenhvien.Columns.TenBenhvien });
            AutoMabenhchinh.Init(globalVariables.gv_dtDmucBenh, new List<string> { DmucBenh.Columns.IdBenh, DmucBenh.Columns.MaBenh, DmucBenh.Columns.TenBenh });
        }
        private void ClearControls()
        {
            try
            {
                cmdPrint.Enabled = false;
                cmdHuy.Enabled = false;

                foreach (Control control in pnlTop.Controls)
                {
                    if (control is EditBox)
                    {
                        ((EditBox)(control)).Clear();
                    }
                    else if (control is MaskedEditBox)
                    {
                        control.Text = "";
                    }
                    else if (control is VNS.HIS.UCs.AutoCompleteTextbox)
                    {
                        ((VNS.HIS.UCs.AutoCompleteTextbox)control).SetDefaultItem();
                    }
                    else if (control is TextBox)
                    {
                        ((TextBox)(control)).Clear();
                    }
                }
                foreach (Control control in pnlFill.Controls)
                {
                    if (control is EditBox)
                    {
                        ((EditBox)(control)).Clear();
                    }
                    else if (control is MaskedEditBox)
                    {
                        control.Text = "";
                    }
                    else if (control is VNS.HIS.UCs.AutoCompleteTextbox)
                    {
                        ((VNS.HIS.UCs.AutoCompleteTextbox)control).SetDefaultItem();
                    }
                    else if (control is TextBox)
                    {
                        ((TextBox)(control)).Clear();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void cmdSearchBenhChinh_Click(object sender, EventArgs e)
        {
           
        }
        /// <summary>
        /// hàm thực hiện hsow thông tin cua bệnh
        /// </summary>
      

        private void cmdPrint_Click_1(object sender, EventArgs e)
        {

        }

        private void cmdChuyen_Click_1(object sender, EventArgs e)
        {

        }

        private void cmdHuy_Click_1(object sender, EventArgs e)
        {

        }

        private void cmdNew_Click(object sender, EventArgs e)
        {
            ClearControls();
            m_enAct = action.Insert;
            objPhieuchuyenvien = null;
            objLuotkham = null;
            cmdChuyen.Enabled = false;
            cmdHuy.Enabled = false;
            cmdPrint.Enabled = false;
            dtNgaychuyenvien.Value = DateTime.Now;
            ucThongtinnguoibenh1.txtMaluotkham.Clear();
           ucThongtinnguoibenh1.txtMaluotkham.Focus();
           ucThongtinnguoibenh1.txtMaluotkham.SelectAll();
        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (Utility.AcceptQuestion("Bạn có chắc chắn muốn lấy thông tin mã bệnh + chẩn đoán từ phiếu ra viện của người bệnh để làm thông tin chẩn đoán cho phiếu ra viện đang sửa?\nNhấn Yes để thực hiện(Muốn quay lại thông tin cũ thì xóa thông tin mới và nhấn Ctrl+V để hệ thống dán lại thông tin cũ).\nNhấn No để hủy bỏ", "Thông báo", true))
                {
                    NoitruPhieuravien _phieuravien = null;
                    if (objPhieuchuyenvien != null && objPhieuchuyenvien.IdRavien > 0)
                    {
                        _phieuravien = NoitruPhieuravien.FetchByID(objPhieuchuyenvien.IdRavien);
                    }
                    if (_phieuravien == null)
                    {
                        _phieuravien = new Select().From(NoitruPhieuravien.Schema)
                    .Where(NoitruPhieuravien.Columns.IdBenhnhan)
                    .IsEqualTo(objLuotkham.IdBenhnhan)
                    .And(NoitruPhieuravien.Columns.MaLuotkham)
                    .IsEqualTo(objLuotkham.MaLuotkham)
                    .ExecuteSingle<NoitruPhieuravien>();
                    }
                    if (_phieuravien != null)
                    {
                        if (Utility.sDbnull(txtChandoan.Text).Length > 0)
                        {
                            System.Windows.Forms.Clipboard.SetText(string.Format("{0}", txtChandoan.Text));
                        }
                        string idcname = "";
                        string idcCode = "";
                        Utility.GetChanDoan(_phieuravien.MabenhChinh, _phieuravien.MabenhPhu, _phieuravien.ChanDoan, ref idcname, ref idcCode);
                        txtChandoan.Text = string.Format("{0}:{1}", idcCode, idcname);
                        Utility.ShowMsg("Cập nhật thông tin mã bệnh + chẩn đoán cho phiếu ra viện thành công. Nhấn OK để kết thúc");
                    }
                }

            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        private void CmdRefreshSCV_Click(object sender, EventArgs e)
        {
            txtsochuyenvien.Text = Laysochuyenvien();
        }
    }
}
