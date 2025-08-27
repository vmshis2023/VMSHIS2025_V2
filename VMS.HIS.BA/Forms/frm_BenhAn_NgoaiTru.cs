using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Aspose.Words;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;
using VNS.HIS.BusRule.Classes;
using Janus.Windows.GridEX.EditControls;
using VNS.HIS.UI.Classess;
using VMS.HIS.Bus;
using VMS.HIS.Danhmuc.Dungchung;
using System.Transactions;
using VMS.HIS.EMR;
using VMS.HIS.Bus.Emr;
using System.Globalization;

namespace VMS.HIS.UI.EMR
{
    public partial class frm_BenhAn_NgoaiTru : Form
    {
        public delegate void OnCreated(long id,string ma_ba, action m_enAct);
        public event OnCreated _OnCreated;
        string lstLoaiBA = "";
        DataTable dt_ThongtinNguoibenh = new DataTable();
        public frm_BenhAn_NgoaiTru(string lstLoaiBA)
        {
            InitializeComponent();
            this.DialogResult = DialogResult.Cancel;
            this.lstLoaiBA = lstLoaiBA;
            Utility.SetVisualStyle(this);
            ucThongtinnguoibenh_emr_basic1.noitrungoaitru = 0;
            ucThongtinnguoibenh_emr_basic1.AutoLoad = false;
            chkQLNBTuDen.CheckedChanged += chkQLNBTuDen_CheckedChanged;
            chkQLNBCoQuanYTe.CheckedChanged += chkQLNBCoQuanYTe_CheckedChanged;
            chkQLNBKhac.CheckedChanged += chkQLNBKhac_CheckedChanged;
           
            txtIDBenhAn.KeyDown += txtIDBenhAn_KeyDown;
            txtMaBenhAn.KeyDown += txtMaBenhAn_KeyDown;
            ucThongtinnguoibenh_emr_basic1.trangthai_noitru = 5;
            ucThongtinnguoibenh_emr_basic1._OnEnterMe += UcThongtinnguoibenh_emr_basic1__OnEnterMe;
            Utility.setEnterEvent(this);
          
            txtB_CTScanner.TextChanged += soluongto_TextChanged;
            txtB_Khac.TextChanged += soluongto_TextChanged;
            txtB_SieuAm.TextChanged += soluongto_TextChanged;
            txtB_XetNghiem.TextChanged += soluongto_TextChanged;
            txtB_Xquang.TextChanged += soluongto_TextChanged;
            PhanquyenTinhnang();
            txtChieuCao.Leave += txtChieucao_Leave;
            txtCanNang.Leave += txtCannang_Leave;
        }
        private void txtChieucao_Leave(object sender, EventArgs e)
        {
            Utility.CalculateIBM(Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(txtChieuCao.Text), 0), Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(txtCanNang.Text), 0), txtBMI);
        }

        private void txtCannang_Leave(object sender, EventArgs e)
        {
            Utility.CalculateIBM(Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(txtChieuCao.Text), 0), Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(txtCanNang.Text), 0), txtBMI);
        }

        void PhanquyenTinhnang()
        {
            cmdKCB.Visible = cmdKCB.Enabled = Utility.Coquyen("EMR_THEM_PHIEUKCB");
          chkEditPKB.Visible = chkEditPKB.Enabled = Utility.Coquyen("EMR_SUA_PHIEUKCB");
            chkEditTKBA.Visible = chkEditTKBA.Enabled = Utility.Coquyen("EMR_SUA_TKBA");

        }
        private void UcThongtinnguoibenh_emr_basic1__OnEnterMe()
        {
            if (ucThongtinnguoibenh_emr_basic1.objLuotkham != null)
            {
                if (!ucThongtinnguoibenh_emr_basic1.objLuotkham.NgayKetthuc.HasValue)
                {
                    Utility.ShowMsg(string.Format("Người bệnh {0} với mã lần khám {1} chưa kết thúc khám nên bạn không thể thực hiện tạo Bệnh Án Ngoại trú được. Vui lòng kiểm tra lại", ucThongtinnguoibenh_emr_basic1.txtTenBN.Text, ucThongtinnguoibenh_emr_basic1.objLuotkham.MaLuotkham));
                    objLuotkham = null;
                    objBenhnhan = null;
                    objEmrBa = null;
                    ClearControl();
                    ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Focus();
                    ucThongtinnguoibenh_emr_basic1.txtMaluotkham.SelectAll();
                    return;
                }
                objEmrBa = null;
                objTsbDacdiemlienquan = null;
                objLuotkham = ucThongtinnguoibenh_emr_basic1.objLuotkham;
                objBenhnhan = Utility.getKcbDanhsachBenhnhan(objLuotkham);
                dt_ThongtinNguoibenh = ucThongtinnguoibenh_emr_basic1.dt_ThongtinNguoibenh;
                //if (!IsValidData()) return;
                ClearControl();
                FillData4Update();
                ModifyCommand();
            }
        }

         
        void soluongto_TextChanged(object sender, EventArgs e)
        {
            txtB_Tongso.Text =( Utility.Int32Dbnull(txtB_CTScanner.Text, 0) + Utility.Int32Dbnull(txtB_Khac.Text, 0) + Utility.Int32Dbnull(txtB_SieuAm.Text, 0) + Utility.Int32Dbnull(txtB_XetNghiem.Text, 0) + Utility.Int32Dbnull(txtB_Xquang.Text, 0)).ToString();
        }

       
       
        void txtMaBenhAn_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string maBA = Utility.AutoFillMaBA(txtMaBenhAn.Text);
                    txtMaBenhAn.Text = maBA;
                    if (objEmrBa != null && maBA != objEmrBa.MaBa)
                    {
                        if (!Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn tìm Bệnh án theo mã: {0}.\nHệ thống sẽ nạp lại dữ liệu của Bệnh án tìm được và các thông tin bạn đang làm việc chưa kịp lưu sẽ bị hủy.\nNhấn Ok để tiếp tục. Nhấn No để quay lại trạng thái làm việc trước đó", Utility.DoTrim(txtMaBenhAn.Text)), "", true))
                        {
                            return;
                        }
                    }
                    objEmrBa = new Select().From(EmrBa.Schema).Where(EmrBa.Columns.MaBa).IsEqualTo(Utility.DoTrim(txtMaBenhAn.Text)).ExecuteSingle<EmrBa>();
                    if (objEmrBa == null)
                        ClearControl();
                    else
                    {
                        ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Text = objEmrBa.MaLuotkham;
                        ucThongtinnguoibenh_emr_basic1.Refresh(true);
                        // FillData4Update();
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

        void txtIDBenhAn_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (objEmrBa != null)
                        if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn tìm Bệnh án theo ID: {0}.\nHệ thống sẽ nạp lại dữ liệu của Bệnh án tìm được và các thông tin bạn đang làm việc chưa kịp lưu sẽ bị hủy.\nNhấn Ok để tiếp tục. Nhấn No để quay lại trạng thái làm việc trước đó", Utility.DoTrim(txtIDBenhAn.Text)), "", true))
                        {
                            return;
                        }
                    objEmrBa = EmrBa.FetchByID(Utility.Int64Dbnull(txtIDBenhAn.Text));
                    if (objEmrBa == null)
                        ClearControl();
                    else
                    {
                        ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Text = objEmrBa.MaLuotkham;
                        ucThongtinnguoibenh_emr_basic1.Refresh(true);
                        // FillData4Update();
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

        //void ucThongtinnguoibenh_emr_basic1__OnEnterMe()
        //{
        //    if (ucThongtinnguoibenh_emr_basic1.objLuotkham != null)
        //    {
        //        if (ucThongtinnguoibenh_emr_basic1.objLuotkham.TrangthaiNoitru >0)
        //        {
        //            Utility.ShowMsg(string.Format("Người bệnh {0} với mã lần khám {1} đang ở trạng thái nội trú nên bạn không thể thực hiện tạo Bệnh án Ngoại trú được. Vui lòng kiểm tra lại", ucThongtinnguoibenh_emr_basic1.txtTenBN.Text, ucThongtinnguoibenh_emr_basic1.objLuotkham.MaLuotkham));
        //            objLuotkham = null;
        //            objBenhnhan = null;
        //            objEmrBa = null;
        //            ClearControl();
        //            ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Focus();
        //            ucThongtinnguoibenh_emr_basic1.txtMaluotkham.SelectAll();
        //            return;
        //        }
        //        objEmrBa = null;
        //        objTsbDacdiemlienquan = null;
        //        objLuotkham = ucThongtinnguoibenh_emr_basic1.objLuotkham;
        //        objBenhnhan = Utility.getKcbBenhnhan(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham);
        //        ClearControl();
        //        FillData4Update();
        //        dtpNgayTiepdon.Focus();
        //        ModifyCommand();
        //    }
        //}

        #region checkbox
        private void chkQLNBCoQuanYTe_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBCoQuanYTe.Checked == true)
            {
                chkQLNBTuDen.Checked = false;

                chkQLNBKhac.Checked = false;


            }
        }

        private void chkQLNBTuDen_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBTuDen.Checked == true)
            {


                chkQLNBKhac.Checked = false;
                chkQLNBCoQuanYTe.Checked = false;

            }
        }

        private void chkQLNBKhac_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBKhac.Checked == true)
            {
                chkQLNBTuDen.Checked = false;
                chkQLNBCoQuanYTe.Checked = false;

            }
        }

        #endregion
       

        private void GetChanDoanNoitru()
        {
            var dtPatient = new DataTable();
            dtPatient =
                new Select("*")
                    .From(KcbChandoanKetluan.Schema)
                    .Where(KcbChandoanKetluan.Columns.MaLuotkham)                    .IsEqualTo(objLuotkham.MaLuotkham)
                     .And(KcbChandoanKetluan.Columns.KieuChandoan).IsEqualTo(2)//Chẩn đoán trong quá trình điều trị nội trú.
                    .And(KcbChandoanKetluan.Columns.Noitru)                    .IsEqualTo(1)
                    .ExecuteDataSet()
                    .Tables[0];
            foreach (DataRow row in dtPatient.Rows)
            {
                ICD_Khoa_NoITru += row["mabenh_chinh"] + ";";
                Name_Khoa_NoITru += row["chandoan"] + ";";
            }
        }
      
        string Get_ChanDoan_KKB_CapCuu()
        {
            string _result = string.Empty;
            try
            {
                SqlQuery sqlQuery = new Select(KcbChandoanKetluan.Columns.Chandoan, KcbChandoanKetluan.Columns.ChandoanKemtheo, KcbChandoanKetluan.Columns.MabenhChinh, KcbChandoanKetluan.Columns.MabenhPhu)
                                            .From(KcbChandoanKetluan.Schema)
                                              .Where(KcbChandoanKetluan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                                                      //.And(KcbChandoanKetluan.Columns.KeyCode).IsEqualTo("NGOAITRU")
                                                      .And(KcbChandoanKetluan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).OrderAsc(KcbChandoanKetluan.Columns.NgayChandoan);
                var objInfoCollection = sqlQuery.ExecuteAsCollection<KcbChandoanKetluanCollection>();
                string chandoan = "";
                string mabenh = "";
                string tenbenhphu = "";
                string tenbenhchinh = "";
                string mabenhphu = "";
                foreach (KcbChandoanKetluan objDiagInfo in objInfoCollection)
                {
                    string ICD_Name = "";
                    string ICD_Code = "";
                    string ICD_Phu_Name = "";
                    string ICD_Phu_Code = "";
                    Utility.GetChanDoanChinhPhu(Utility.sDbnull(objDiagInfo.MabenhChinh, ""), Utility.sDbnull(objDiagInfo.MabenhPhu, ""), ref ICD_Name, ref ICD_Code, ref ICD_Phu_Name, ref ICD_Phu_Code);
                    chandoan += string.IsNullOrEmpty(objDiagInfo.Chandoan) ? "" : Utility.sDbnull(objDiagInfo.Chandoan);
                    tenbenhchinh += ICD_Name;
                    mabenh += ICD_Code;
                    tenbenhphu += ICD_Phu_Name;
                    mabenhphu += ICD_Phu_Code;
                }
                _result =THU_VIEN_CHUNG.Laygiatrithamsohethong("BA_SUDUNG_ICD_LAM_CHANDOANSOBO","0",true)=="1"  ? tenbenhchinh + tenbenhphu + chandoan : chandoan; //nếu dùng icd làm cdsb thì trên cdsb đã có tên bệnh rồi, ko cần cộng vào nữa
            }
            catch (Exception)
            {
                _result = string.Empty;
            }
            return _result;
        }

     
        private void ClearControl()
        {
            //txtMaBN.Clear();
            //txtMaLanKham.Clear();
            txtMaBenhAn.Clear();
           
            txtBenhAnToanThan.Clear();
            txtMach.Clear();
            txtNhietDo.Clear();
            txtha.Clear();
            txtNhipTho.Clear();
            txtCanNang.Clear();
            txtChieuCao.Clear();
            txtBMI.Clear();
         
            txtCacBoPhan.Clear();
          
            txtTKBAQuaTrinhBenhLy.Clear();
            txtTKBATTomTatKetQua.Clear();
            txtTKBAPhuongPhapDieuTri.Clear();
            txtTKBATinhTrangRaVien.Clear();
            txtTKBAHuongDieuTri.Clear();
            txtNguoiGiaoHoSo.Clear();
            txtNguoiNhanHoSo.Clear();
            txtBSDieuTri.Clear();
            txtB_CTScanner.Clear();
            txtB_Xquang.Clear();
            txtB_SieuAm.Clear();
            txtB_XetNghiem.Clear();
            txtB_Khac.Clear();


        }
       
        private bool IsValidData(int trangthai)
        {
            if (objLuotkham != null)
                objLuotkham = Utility.getKcbLuotkham(objLuotkham);
            Utility.SetMsg(lblMsg, "", false);
            if (objLuotkham == null)
            {
                Utility.SetMsg(lblMsg, "Cần chọn người bệnh trước khi làm Bệnh án. Vui lòng kiểm tra lại", true);
                ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Focus();
                return false;
            }
            if (Utility.sDbnull(cboLoaiBA.SelectedValue, "-1") == "-1")
            {
                uiTabBA.SelectedTab = tabpageTo1;
                Utility.SetMsg(lblMsg, "Cần chọn loại bệnh án", true);
                cboLoaiBA.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(txtBSlamBA.MyID, -1) <= 0)
            {
                uiTabBA.SelectedTab = tabpageTo1;
                Utility.SetMsg(lblMsg, "Bạn cần chọn Bác sĩ làm bệnh án từ danh mục Bác sĩ trong hệ thống", true);
                txtBSlamBA.Focus();
                return false;
            }
            if (trangthai == 2)
            {
                if (Utility.Int32Dbnull(txtBacsiKham.MyID, -1) <= 0)
                {
                    uiTabBA.SelectedTab = tabpageTo1;
                    Utility.SetMsg(lblMsg, "Bạn cần chọn Bác sĩ khám từ danh mục Bác sĩ trong hệ thống", true);
                    txtBacsiKham.Focus();
                    return false;
                }
                if (Utility.Int32Dbnull(txtNguoiGiaoHoSo.MyID, -1) <= 0)
                {
                    uiTabBA.SelectedTab = tabpageTo4;
                    Utility.SetMsg(lblMsg, "Bạn cần chọn Người giao hồ sơ trong danh mục hệ thống", true);
                    txtNguoiGiaoHoSo.Focus();
                    return false;
                }
                if (Utility.Int32Dbnull(txtNguoiNhanHoSo.MyID, -1) <= 0)
                {
                    uiTabBA.SelectedTab = tabpageTo4;
                    Utility.SetMsg(lblMsg, "Bạn cần chọn Người nhận hồ sơ trong danh mục hệ thống", true);
                    txtNguoiNhanHoSo.Focus();
                    return false;
                }
                if (Utility.Int32Dbnull(txtBSDieuTri.MyID, -1) <= 0)
                {
                    uiTabBA.SelectedTab = tabpageTo4;
                    Utility.SetMsg(lblMsg, "Bạn cần chọn Bác sĩ điều trị từ danh mục Bác sĩ trong hệ thống", true);
                    txtBSDieuTri.Focus();
                    return false;
                }
                if (Utility.Int32Dbnull(txtTruongkhoa.MyID, -1) <= 0)
                {
                    uiTabBA.SelectedTab = tabpageTo4;
                    Utility.SetMsg(lblMsg, "Bạn cần chọn Trưởng khoa điều trị từ danh mục Bác sĩ trong hệ thống", true);
                    txtTruongkhoa.Focus();
                    return false;
                }
                if (Utility.Int32Dbnull(txtGDBV.MyID, -1) <= 0)
                {
                    uiTabBA.SelectedTab = tabpageTo4;
                    Utility.SetMsg(lblMsg, "Bạn cần chọn Giám đốc bệnh viện", true);
                    txtGDBV.Focus();
                    return false;
                }
            }
            return true;
        }
        EmrDocuments emrdoc = new EmrDocuments();
        bool isSuccess = false;
        private void cmdSave_Click(object sender, EventArgs e)
        {
            LuuBA(2);
        }
       void LuuBA(int trangthai)
        {
            try
            {
                isSuccess = false;
                if (!IsValidData(trangthai)) return;
                objEmrBa = TaoEmrBa();
                if (objEmrBa.IdBa > 0)
                {
                    if (!Utility.isValidSignStatus4UpdateDelete(objLuotkham, objEmrBa.IdBa, Loaiphieu_HIS.BA_NGOAITRU, "Bệnh án Ngoại trú"))
                        return;
                }
                objEmrBa.TrangThai = Utility.ByteDbnull(trangthai);
                //if (objEmrBa.IdBa > 0 && objEmrBa.MaBa != maBA)
                //{
                //    if(Utility.AcceptQuestion("Mã bệnh án cũ :{0} đang khác với mã bệnh án nhập tay: {1}. Bạn có chắc chắn muốn cập nhật lại thành mã bệnh án mới","",))
                //    {
                //    }
                //}
                EmrHosoluutru hsba = null;
                if (objEmrBa.IdBa <= 0)
                {
                    hsba = new EmrHosoluutru();
                    hsba.IdBa = objEmrBa.IdBa;
                    hsba.LoaiBa = objEmrBa.LoaiBa;
                    hsba.MaBa = objEmrBa.MaBa;
                    hsba.IdBenhnhan = objEmrBa.IdBenhnhan;
                    hsba.MaLuotkham = objEmrBa.MaLuotkham;
                    hsba.MaCoso = objEmrBa.MaCoso;
                    hsba.NgayTao = objEmrBa.NgaylamBa.Value;
                    hsba.NguoiTao = objEmrBa.NguoiTao;
                    hsba.Nam = objEmrBa.NgayTao.Value.Year;
                    hsba.TrangThai = 0;
                }
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        objEmrBa.Save();
                        new Update(KcbLuotkham.Schema)
                            .Set(KcbLuotkham.Columns.IdBsDieutrinoitruChinh).EqualTo(objEmrBa.IdBacsiDieutri)
                            .Set(KcbLuotkham.Columns.IdBa).EqualTo(objEmrBa.IdBa)
                            .Set(KcbLuotkham.Columns.LoaiBenhAn).EqualTo(objEmrBa.LoaiBa)
                                 .Where(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                                 .And(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                                 .Execute();
                        if (hsba != null)
                        {
                            hsba.IdBa = objEmrBa.IdBa;
                            hsba.Save();
                        }
                        if (trangthai <= 0)
                        {
                            //Thực hiện hàm refresh EMR
                            int num = 0;
                            StoredProcedure sp = SPs.EmrLaydanhsachDocumentsFromTables(objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, "", 1, num);
                            sp.Execute();
                        }
                        else
                        {
                            //if (Utility.Coquyen("EMR_SUA_PHIEUKCB") && objEmrBa.IdBa > 0 && chkEditPKB.Checked)
                            //{
                            TaoPhieuKCB();
                            objPKB.Save();
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật thông tin phiếu khám toàn thân tại chức năng EMR cho người bệnh: {0}-{1} thành công", objEmrBa.MaLuotkham, objEmrBa.TenBenhnhan), objPKB.IsNew ? newaction.Insert : newaction.Update, "EMR");
                            //}
                            //if (Utility.Coquyen("EMR_SUA_TKBA") && objEmrBa.IdBa > 0 && chkEditTKBA.Checked)
                            //{
                            //    TaoPhieuTKBA();
                            //    objTKBA.Save();
                            //    if (objTKBA.IsNew)
                            //    {

                            //        emrdoc.InitDocument(objTKBA.IdBenhnhan, objTKBA.MaLuotkham, Utility.Int64Dbnull(objTKBA.Id), objTKBA.NgayTtba.Value, Loaiphieu_HIS.PHIEU_TKBA, "BA_TKBA", objTKBA.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true);
                            //        emrdoc.Save();
                            //    }
                            //    Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật thông tin Tổng kết BA tại chức năng EMR cho người bệnh: {0}-{1} thành công", objEmrBa.MaLuotkham, objEmrBa.TenBenhnhan), objTKBA.IsNew ? newaction.Insert : newaction.Update, "EMR");
                            //}
                        }


                        emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN_BIA, "BA15_BANGOAITRU_BIA", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", objEmrBa.LoaiBa);
                        emrdoc.Save();

                        emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN_TO1, "BA15_BANGOAITRU_TO1", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", objEmrBa.LoaiBa);
                        emrdoc.Save();
                        emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN_TO2, "BA15_BANGOAITRU_TO2", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", objEmrBa.LoaiBa);
                        emrdoc.Save();

                        emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BA_NGOAITRU, "BA15_BANGOAITRU", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "", true, false, "", objEmrBa.LoaiBa);
                        emrdoc.Save();


                    }
                    scope.Complete();
                    isSuccess = true;
                }
                txtIDBenhAn.Text = objEmrBa.IdBa.ToString();
                if (isSuccess)
                {
                    if (m_enAct == action.Insert)
                    {
                        Utility.Log(this.Name, globalVariables.UserName, string.Format("Thêm mới BA cho bệnh nhân: {0}-{1} thành công", objEmrBa.IdBa, objEmrBa.TenBenhnhan), objEmrBa.IsNew ? newaction.Insert : newaction.Update, "UI");
                        MessageBox.Show(trangthai == 0 ? "Đã khởi tạo Bệnh án thành công" : "Đã thêm mới Bệnh án thành công");
                        cmdXoaBenhAn.Enabled = cmdPrint.Enabled = true;
                        if (_OnCreated != null) _OnCreated(objEmrBa.IdBa, objEmrBa.MaBa, action.Insert);
                        m_enAct = action.Update;
                    }
                    else if (m_enAct == action.Update)
                    {
                        Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật Bệnh án Ngoại trú cho bệnh nhân: {0}-{1} thành công", objEmrBa.IdBa, objEmrBa.TenBenhnhan), objEmrBa.IsNew ? newaction.Insert : newaction.Update, "UI");
                        if (_OnCreated != null) _OnCreated(objEmrBa.IdBa, objEmrBa.MaBa, action.Update);
                        MessageBox.Show("Đã cập nhật Bệnh án thành công");
                        m_enAct = action.Update;
                    }
                }
                EnableBA();
                //Utility.ShowMsg("Lưu thông tin thành công", "Thông báo");
                dtDataBA = SPs.EmrBaLaythongtin(-1, "", objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham).GetDataSet().Tables[0];
                this.DialogResult = DialogResult.OK;
                _isSuccess = true;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
            finally
            {
                //if (objEmrBa != null && _isSuccess)
                //{
                //    new Update(KcbLuotkham.Schema)
                //        .Set(KcbLuotkham.Columns.IdBa).EqualTo(objEmrBa.IdBa)
                //        .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                //        .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).Execute();
                //   // EmrThemBenhAn();
                //}

            }
        }
        void TaoPhieuTKBA()
        {
             objTKBA = new Select().From(EmrTomtatBa.Schema).Where(EmrTomtatBa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).And(EmrTomtatBa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<EmrTomtatBa>();
            if (objTKBA == null) objTKBA = new EmrTomtatBa();
            if (objTKBA.Id > 0)
            {
                objTKBA.IsNew = false;
                objTKBA.MarkOld();
                objTKBA.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
                objTKBA.NguoiSua = globalVariables.UserName;
            }
            else
            {
                objTKBA.IsNew = true;
                objTKBA.NguoiTao = globalVariables.UserName;
                objTKBA.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
            }
            objTKBA.MaLuotkham = objLuotkham.MaLuotkham;
            objTKBA.IdBenhnhan = (int)objLuotkham.IdBenhnhan;
            objTKBA.IdKhoadieutri = Utility.Int32Dbnull(objEmrBa.IdKhoaravien, -1);
           
            objTKBA.QuatrinhbenhlyDienbienlamsang = objEmrBa.TongketbaQuatrinhbenhlyDienbienlamsang;
            objTKBA.TomtatKqcls = objEmrBa.TongketbaTomtatKqcls;
            objTKBA.TinhtrangRavienMota = objEmrBa.TongketbaTinhtrangNguoiravien;
            objTKBA.PhuongphapDieutri = objEmrBa.TongketbaPhuongphapdieutri;
            objTKBA.HuongDieutri = objEmrBa.TongketbaHuongdieutritieptheo;
            objTKBA.NgayTtba = objEmrBa.TongketbaNgay;
            objTKBA.NguoigiaoHoso = Utility.sDbnull(txtNguoiGiaoHoSo.Text);
            objTKBA.NguoiNhanhoso = Utility.sDbnull(txtNguoiNhanHoSo.Text);
            objTKBA.IdBacsiDieutri =Utility.Int16Dbnull( txtBSDieuTri.MyID);
            if (objTKBA.Id <= 0)//Không update lại các mục này nếu đã làm ở phần TKBA
            {
                objTKBA.TiensuBenh = "";
                objTKBA.TomtatKqcls = "";
                objTKBA.Noikhoa = 0;
                objTKBA.NoikhoaMota = "";
                objTKBA.Pttt = 0;
                objTKBA.PtttMota = "";
            }
            objTKBA.IdNguoigiaoHoso = Utility.Int16Dbnull(txtNguoiGiaoHoSo.MyID);
            objTKBA.MaNguoigiaohoso = txtNguoiGiaoHoSo.MyCode;
            objTKBA.IdNguoinhanHoso = Utility.Int16Dbnull(txtNguoiNhanHoSo.MyID);
            objTKBA.MaNguoinhanhoso = txtNguoiNhanHoSo.MyCode;
            objTKBA.IdGiamdoc = Utility.Int16Dbnull(txtGDBV.MyID);
            objTKBA.MaGiamdoc = txtGDBV.MyCode;
            objTKBA.IdBacsiDieutri = Utility.Int16Dbnull(txtBSDieuTri.MyID);
            objTKBA.MaBacsiDieutri = txtBSDieuTri.MyCode;
            objTKBA.IdTruongkhoadieutri = Utility.Int16Dbnull(txtTruongkhoa.MyID);
            objTKBA.MaTruongkhoadieutri = txtTruongkhoa.MyCode;
        }
        void TaoPhieuKCB()
        {
            //Refresh lại thông tin KCB
            objPKB = new Select().From(EmrPhieukhambenh.Schema)
                .Where(EmrPhieukhambenh.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                .And(EmrPhieukhambenh.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                .And(EmrPhieukhambenh.Columns.Noitru).IsEqualTo(1)
                .ExecuteSingle<EmrPhieukhambenh>();
            if (objPKB != null && objPKB.Id > 0)
            {
                objPKB.MarkOld();
                objPKB.NguoiSua = globalVariables.UserName;
                objPKB.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
            }
            else
            {
                objPKB = new EmrPhieukhambenh();
                objPKB.IsNew = true;
                objPKB.Noitru = 1;
                objPKB.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham);
                objPKB.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan);
                objPKB.NgayKham = dtpNgayKham.Value.Date;
                objPKB.NguoiTao = globalVariables.UserName;
                objPKB.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
            }
            objPKB.IdBacsi = Utility.Int16Dbnull(txtBacsiKham.MyID, -1);
            objPKB.HuyetAp = txtha.Text;
            objPKB.NhietDo = txtNhietDo.Text;
            objPKB.Mach = Utility.sDbnull(txtMach.Text);
            objPKB.NhipTho = Utility.sDbnull(txtNhipTho.Text);
            objPKB.ChieuCao = Utility.sDbnull(txtChieuCao.Text);
            objPKB.CanNang = Utility.sDbnull(txtCanNang.Text);
            objPKB.Bmi = Utility.sDbnull(txtBMI.Text);
            objPKB.MotaThem = "";
            objPKB.ToanThan = Utility.sDbnull(txtBenhAnToanThan.Text);
            objPKB.BoPhan = Utility.sDbnull(txtCacBoPhan.Text);
        }
        void EnableBA()
        {
            cboLoaiBA.Enabled = txtIDBenhAn.Enabled=cmdKhoitaoBA.Enabled= m_enAct == action.Insert;
            if (objEmrBa != null && objEmrBa.LoaiBa != Utility.sDbnull(cboLoaiBA.SelectedValue))
            {
                ThongbaoSaiBenhAn(objEmrBa);
                cmdPrint.Enabled = cmdKetthucBA.Enabled = cmdXoaBenhAn.Enabled = false;
            }
        }
        void ThongbaoSaiBenhAn(EmrBa objEmrBa)
        {
            string Msg = string.Format("Người bệnh {0} đang có hồ sơ Bệnh án {1} không khớp với loại Bệnh án bạn đang chọn. Vui lòng chọn lại đúng loại Bệnh án cần làm", ucThongtinnguoibenh_emr_basic1.txtTenBN.Text, objEmrBa.LoaiBa);
            Utility.ShowMsg(Msg);
        }
        private EmrBa TaoEmrBa()
        {
            if (objEmrBa == null) objEmrBa = new EmrBa();
            try
            {
                int id = Utility.Int32Dbnull(txtIDBenhAn.Text, -1);
                if (objEmrBa.IdBa > 0)
                {
                    objEmrBa.IsLoaded = true;
                    objEmrBa.MarkOld();
                    objEmrBa.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
                    objEmrBa.NguoiSua = globalVariables.UserName;
                }
                else
                {
                    SinhMaBenhAn();
                    objEmrBa.MaBa = Utility.sDbnull(txtMaBenhAn.Text);
                    objEmrBa.NguoiTao = globalVariables.UserName;
                    objEmrBa.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
                }
                objEmrBa.NgaylamBa = dtpNgayBA.Value;
                objEmrBa.TongketbaNgay = dtpNgayTKBA.Value;
                objEmrBa.LoaiBa = cboLoaiBA.SelectedValue.ToString();
                objEmrBa.Khoa = "KHOA KHÁM CHỮA BỆNH";
               
                //objEmrBa.BenhNgoaiKhoa = Utility.sDbnull(txtBenhNgoai_Khoa.Text);
                objEmrBa.MaCoso = objLuotkham.MaCoso;
                objEmrBa.IdBenhnhan = objLuotkham.IdBenhnhan;
                objEmrBa.TenBenhnhan = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0][KcbDanhsachBenhnhan.Columns.TenBenhnhan], "");
                objEmrBa.MaLuotkham = objLuotkham.MaLuotkham;
                objEmrBa.MaYte = objLuotkham.MaYte;
                objEmrBa.NgaySinh = DateTime.ParseExact(Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["ngay_sinh"], DateTime.Now.ToString("yyyyMMdd")), "yyyyMMdd", CultureInfo.InvariantCulture);
                objEmrBa.MaGioitinh = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["id_gioitinh"], "0") == "0" ? "M" : "F";
                objEmrBa.GioiTinh = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["gioi_tinh"], "");
                objEmrBa.Tuoi = Utility.ByteDbnull(dt_ThongtinNguoibenh.Rows[0]["Tuoi"], "0");
                objEmrBa.LoaiTuoi = (byte)objLuotkham.LoaiTuoi;


                objEmrBa.MaNghenghiep = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["nghe_nghiep"], "");
                objEmrBa.TenNghenghiep = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["ten_nghenghiep"], "");
                objEmrBa.MaDantoc = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["dan_toc"], "");
                objEmrBa.TenDantoc = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["ten_dantoc"], "");
                objEmrBa.MaTongiao = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["ton_giao"], "");
                objEmrBa.TenTongiao = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["ten_tongiao"], "");
                objEmrBa.MaQuocgia = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["ma_quocgia"], "VN");
                objEmrBa.TenQuocgia = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["ten_quocgia"], "");
                objEmrBa.NgoaiKieu = (Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["ma_quocgia"], "VN") == "VN" ? 0 : 1) == 1;

                objEmrBa.DiachiLienhe = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["diachi_lienhe"], "");
                objEmrBa.DienthoaiLienhe = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["dienthoai_lienhe"], "");
                objEmrBa.NguoiLienhe = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["nguoi_lienhe"], "");
                objEmrBa.CmtNguoilienhe = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["CMT_nguoilienhe"], "");
                objEmrBa.DiaChi = objLuotkham.DiaChi;
                objEmrBa.MaTinhtp = objLuotkham.MaTinhtp;
                objEmrBa.TenTinhtp = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["ten_tinhtp"], "");
                objEmrBa.MaQuanhuyen = objLuotkham.MaQuanhuyen;
                objEmrBa.TenQuanhuyen = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["ten_quanhuyen"], "");
                objEmrBa.MaXaphuong = objLuotkham.MaXaphuong;
                objEmrBa.TenXaphuong = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["ten_xaphuong"], "");
                objEmrBa.MaCoquan = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["co_quan"], "");
                objEmrBa.TenCoquan = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["ten_coquan"], "");
                objEmrBa.MatheBhyt = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["mathe_bhyt"], "");
                objEmrBa.MaDoituong = Utility.ByteDbnull(objLuotkham.IdDoituongKcb);
                objEmrBa.TenDoituong = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["ten_doituong_kcb"], "");

                objEmrBa.MatheBhyt = objLuotkham.MatheBhyt;
                objEmrBa.BhytTungay = objLuotkham.NgaybatdauBhyt;
                objEmrBa.BhytDenngay = objLuotkham.NgayketthucBhyt;
                objEmrBa.HotenBo = "";
                objEmrBa.TrinhdoVanhoaBo = "";
                objEmrBa.NghenghiepBo = "";
                objEmrBa.HotenMe = "";
                objEmrBa.TrinhdoVanhoaMe = "";
                objEmrBa.NghenghiepMe = "";

                objEmrBa.CmtCccd = objLuotkham.Cmt;
                objEmrBa.SoHochieu = objLuotkham.Cmt;
                objEmrBa.DienThoai = Utility.sDbnull(dt_ThongtinNguoibenh.Rows[0]["dien_thoai"], "");
                objEmrBa.Email = objLuotkham.Email;




                objEmrBa.NoigioithieuCoquanyte = chkQLNBCoQuanYTe.Checked;
                    objEmrBa.NoigioithieuTuden = chkQLNBTuDen.Checked;
                    objEmrBa.NoigioithieuKhac = chkQLNBKhac.Checked;
                   
                //Check lại
                objEmrBa.VaovienMakhoa = "KKB";
                objEmrBa.VaovienTenkhoa = "KHOA KHÁM CHỮA BỆNH";
                objEmrBa.VaovienNgayvaokhoa = dtpNgayTiepdon.Value;
                objEmrBa.IdBacsiLamBA =Utility.Int16Dbnull( txtBacsiKham.MyID);
                objEmrBa.TenbacsiLamBA = txtBacsiKham.Text;
                objEmrBa.MabacsiLamBA = txtBacsiKham.MyCode;
                //Chẩn đoán
                objEmrBa.CdNoichuyenden = Utility.sDbnull(txtChanDoanNoiGioiThieu.Text);
                objEmrBa.VaovienLydovaovien = Utility.sDbnull(txtBenhAnLyDoNhapVien.Text);
                objEmrBa.HoibenhQuatrinhbenhly= Utility.sDbnull(txtBenhAnQuaTrinhBenhLy.Text);
                objEmrBa.HoibenhTiensubanthan = Utility.sDbnull(txtTiensuBanthan.Text);
                objEmrBa.HoibenhTiensugiadinh = Utility.sDbnull(txtTiensuGiadinh.Text);
                
                objEmrBa.KbMach = txtMach.Text;
                objEmrBa.KbNhietdo = txtNhietDo.Text;
                objEmrBa.KbHuyetap = txtha.Text;
                objEmrBa.KbNhiptho = txtNhipTho.Text;
                objEmrBa.KbCannang = txtCanNang.Text;
                objEmrBa.KbChieucao = txtChieuCao.Text;
                tinhBMI();
               //Thông tin khám bệnh
                objEmrBa.KbBmi = Utility.sDbnull(txtBMI.Text, 0); 
                objEmrBa.KhambenhToanthan = Utility.sDbnull(txtBenhAnToanThan.Text);
                objEmrBa.KhambenhCacbophan = Utility.sDbnull(txtCacBoPhan.Text);
                objEmrBa.KhambenhTomtatbenhan= Utility.sDbnull(txtTomtatKQCLS.Text);
                objEmrBa.CdBandau = Utility.sDbnull(txtChandoanBandau.Text);
                objEmrBa.KhambenhDaxulychamsoc = Utility.sDbnull(txtDaXulyThuocChamsoc.Text);
                objEmrBa.RavienTenBenhchinh = Utility.sDbnull(txtTenbenhchinh.Text);
                objEmrBa.RavienMaBenhchinh = Utility.sDbnull(txtMabenhchinh.Text);
                objEmrBa.NgayDieutriTu = dtpDieutriTungay.Value;
                objEmrBa.NgayDieutriDen = dtpDieutriDenngay.Value;

                objEmrBa.TongketbaQuatrinhbenhlyDienbienlamsang = Utility.sDbnull(txtTKBAQuaTrinhBenhLy.Text);
                objEmrBa.TongketbaTomtatKqcls = Utility.sDbnull(txtTKBATTomTatKetQua.Text);
                objEmrBa.TongketbaPhuongphapdieutri = Utility.sDbnull(txtTKBAPhuongPhapDieuTri.Text);
                objEmrBa.TongketbaTinhtrangNguoiravien = Utility.sDbnull(txtTKBATinhTrangRaVien.Text);
                objEmrBa.TongketbaHuongdieutritieptheo = Utility.sDbnull(txtTKBAHuongDieuTri.Text);

                objEmrBa.IdNguoigiaoHoso = Utility.Int16Dbnull(txtNguoiGiaoHoSo.MyID);
                objEmrBa.TongketbaMaNguoigiaohoso = txtNguoiGiaoHoSo.Text;
                objEmrBa.IdNguoinhanHoso = Utility.Int16Dbnull(txtNguoiNhanHoSo.MyID);
                objEmrBa.TongketbaMaNguoiNhanhoso = txtNguoiNhanHoSo.Text;
                objEmrBa.MabacsiLamBA = txtBSlamBA.MyCode;
                objEmrBa.IdBacsiLamBA =Utility.Int16Dbnull( txtBSlamBA.MyID);
                objEmrBa.TenbacsiLamBA = txtBSlamBA.Text;

                
                objEmrBa.IdGiamdoc = Utility.Int16Dbnull(txtGDBV.MyID); 
                objEmrBa.MaGiamdoc = txtGDBV.MyCode;
                objEmrBa.IdTruongkhoadieutri = Utility.Int16Dbnull(txtBSDieuTri.MyID);
                objEmrBa.MaTruongkhoadieutri = txtBSDieuTri.MyCode;

                objEmrBa.IdBacsiKham = Utility.Int16Dbnull(txtBacsiKham.MyID);
                objEmrBa.MabacsiKham = txtBacsiKham.MyCode;

             

                objEmrBa.IdBacsiDieutri = Utility.Int16Dbnull(txtBSDieuTri.MyID);
                objEmrBa.MabacsiDieutri = txtBSDieuTri.MyCode;
                objEmrBa.TenbacsiDieutri = txtBSDieuTri.Text;
                objEmrBa.TongketbaSotoCt = Utility.Int16Dbnull(txtB_CTScanner.Text);
                objEmrBa.TongketbaSotoXquang = Utility.Int16Dbnull(txtB_Xquang.Text);
                objEmrBa.TongketbaSotoSieuam = Utility.Int16Dbnull(txtB_SieuAm.Text);
                objEmrBa.TongketbaSotoXetnghiem = Utility.Int16Dbnull(txtB_XetNghiem.Text);
                objEmrBa.TongketbaSotoKhac = Utility.Int16Dbnull(txtB_Khac.Text);
                objEmrBa.TongketbaNgay = dtpB_NgayTongKet.Value;
                return objEmrBa;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
                return objEmrBa;

            }
        }

        private void frm_BenhAn_NgoaiTru_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                #region "Xử lý multiline"
                if (tabpageTo1.ActiveControl != null)
                {
                    Control ctr = tabpageTo1.ActiveControl;
                    if (ctr.GetType().Equals(typeof(EditBox)))
                    {
                        EditBox box = ctr as EditBox;
                        if (box.Multiline)
                        {
                            return;
                        }
                        else
                            SendKeys.Send("{TAB}");
                    }
                    else if (ctr.GetType().Equals(typeof(TextBox)))
                    {
                        TextBox box = ctr as TextBox;
                        if (box.Multiline)
                        {
                            return;
                        }
                        else
                            SendKeys.Send("{TAB}");
                    }
                    else if (ctr.GetType().Equals(typeof(RichTextBox)))
                    {
                        RichTextBox box = ctr as RichTextBox;
                        if (box.Multiline)
                        {
                            return;
                        }
                        else
                            SendKeys.Send("{TAB}");
                    }
                    else
                        SendKeys.Send("{TAB}");
                }
                
               
                if (tabpageTo4.ActiveControl != null)
                {
                    Control ctr = tabpageTo4.ActiveControl;
                    if (ctr.GetType().Equals(typeof(EditBox)))
                    {
                        EditBox box = ctr as EditBox;
                        if (box.Multiline)
                        {
                            return;
                        }
                        else
                            SendKeys.Send("{TAB}");
                    }
                    else if (ctr.GetType().Equals(typeof(TextBox)))
                    {
                        TextBox box = ctr as TextBox;
                        if (box.Multiline)
                        {
                            return;
                        }
                        else
                            SendKeys.Send("{TAB}");
                    }
                    else if (ctr.GetType().Equals(typeof(RichTextBox)))
                    {
                        RichTextBox box = ctr as RichTextBox;
                        if (box.Multiline)
                        {
                            return;
                        }
                        else
                            SendKeys.Send("{TAB}");
                    }
                    else
                        SendKeys.Send("{TAB}");
                }
                #endregion
                
            }
            if (e.Control && e.KeyCode == Keys.S)
            {
                if (cmdKhoitaoBA.Enabled)
                    cmdKhoitaoBA.PerformClick();
                else
                    cmdSave.PerformClick();
            }
            //if (e.KeyCode == Keys.F4) cmdInBenhAn.PerformClick();
            if (e.KeyCode == Keys.Escape) Close();
            if ((e.Alt || e.Control) && e.KeyCode == Keys.NumPad1)
            {
                uiTabBA.SelectedIndex = 0;
                ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Focus();
            }
            else if ((e.Alt || e.Control) && e.KeyCode == Keys.NumPad1)
            {
                uiTabBA.SelectedIndex = 1;
            }
            else if ((e.Alt || e.Control) && e.KeyCode == Keys.NumPad1)
            {
                uiTabBA.SelectedIndex = 2;
            }
            else if ((e.Alt || e.Control) && e.KeyCode == Keys.NumPad1)
            {
                uiTabBA.SelectedIndex = 3;
            }
            else if(e.KeyCode==Keys.F5)
            {
                PhanquyenTinhnang();
            }    
        }
        public action m_enAct = action.Insert;
        private void frm_BenhAn_NgoaiTru_Load(object sender, EventArgs e)
        {
            try
            {
                dtpNgayBA.Value = dtpNgayTKBA.Value = DateTime.Now;
                txtBSDieuTri.Init(globalVariables.gv_dtDmucNhanvien,
                                             new List<string>
                                  {
                                      DmucNhanvien.Columns.IdNhanvien,
                                      DmucNhanvien.Columns.MaNhanvien,
                                      DmucNhanvien.Columns.TenNhanvien
                                  });
                txtBSlamBA.Init(txtBSDieuTri.AutoCompleteSource, txtBSDieuTri.defaultItem);
                txtBacsiKham.Init(txtBSDieuTri.AutoCompleteSource, txtBSDieuTri.defaultItem);
                txtNguoiGiaoHoSo.Init(txtBSDieuTri.AutoCompleteSource, txtBSDieuTri.defaultItem);
                txtNguoiNhanHoSo.Init(txtBSDieuTri.AutoCompleteSource, txtBSDieuTri.defaultItem);
                txtGDBV.Init(txtBSDieuTri.AutoCompleteSource, txtBSDieuTri.defaultItem);
                VMS.HIS.Danhmuc.Util.SetNguoiDaiDienDonVi(txtGDBV);
                txtTruongkhoa.Init(txtBSDieuTri.AutoCompleteSource, txtBSDieuTri.defaultItem);
                DataTable dtData =
                    new Select().From(DmucChung.Schema)
                        .Where(DmucChung.Columns.Loai).IsEqualTo("EMR_LOAIBA")
                        .And(DmucChung.Columns.TrangThai).IsEqualTo(1)
                        .And(DmucChung.Columns.Ma).In(lstLoaiBA.Split(',').ToList<string>())
                        .OrderAsc(DmucChung.Columns.SttHthi)
                        .ExecuteDataSet().Tables[0];
                if (dtData.Rows.Count > 1)
                {
                    DataRow dr = dtData.NewRow();
                    dr[DmucChung.Columns.Ten] = "---Chọn loại BA---";
                    dr[DmucChung.Columns.Ma] = "-1";

                    dtData.Rows.InsertAt(dr, 0);
                }
                DataBinding.BindDataCombobox(cboLoaiBA, dtData, "MA", "TEN");//, "---Chọn loại BA---", true);
                txtBenhAnLyDoNhapVien.Init();
                if (m_enAct != action.Insert) ucThongtinnguoibenh_emr_basic1.Refresh();
                //if (m_enAct == action.Insert)
                //{

                //}
                //else
                //{
                //    ucThongtinnguoibenh_emr_basic1.Refresh();
                //    objEmrBa = new Select().From(EmrBa.Schema)
                //     .Where(EmrBa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                //     .And(EmrBa.Columns.IdBenhnhan).IsEqualTo(Utility.Int32Dbnull(objLuotkham.IdBenhnhan))
                //     .ExecuteSingle<EmrBa>();
                //    dt_ThongtinNguoibenh = SPs.EmrLaythongtinnguoibenhMaluotkhamIdbenhnhan(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham).GetDataSet().Tables[0];
                //    objBenhnhan = Utility.getKcbDanhsachBenhnhan(objLuotkham);
                //    FillData4Update();

                //}

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
       

        string ICD_Khoa_NoITru = "";
        string Name_Khoa_NoITru = "";
        DataTable dtDataBA = new DataTable();
        public EmrBa objEmrBa;
        EmrTiensubenhDacdiemlienquan objTsbDacdiemlienquan;
        EmrPhieukhambenh objPKB;
        string maBA = "";
        private bool _isSuccess = false;
        void FillData4Update()
        {
            try
            {
                maBA = "";
             
                SqlQuery sqlQuery = new Select().From<EmrBa>()
                    .Where(EmrBa.Columns.MaLuotkham)
                    .IsEqualTo(objLuotkham.MaLuotkham)
                    .And(EmrBa.Columns.IdBenhnhan)
                    .IsEqualTo(Utility.Int32Dbnull(objLuotkham.IdBenhnhan));
                if (objEmrBa == null || (objEmrBa.IdBenhnhan != objLuotkham.IdBenhnhan && objEmrBa.MaLuotkham != objLuotkham.MaLuotkham))
                    objEmrBa = sqlQuery.ExecuteSingle<EmrBa>();
                //Autofill Data
                FillHoibenhChandoan();
                FillTongketBenhAn();

                FillPhieuKCB();
                dtpNgayTiepdon.Value = objLuotkham.NgayTiepdon;
               
                if (objEmrBa != null)
                {
                    m_enAct = action.Update;
                    cboLoaiBA.SelectedIndex = Utility.GetSelectedIndex(cboLoaiBA, objEmrBa.LoaiBa);
                    maBA = objEmrBa.MaBa;
                    dtDataBA = SPs.EmrBaLaythongtin(-1, "", objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham).GetDataSet().Tables[0];
                    DataRow dr = dtDataBA.Rows[0];
                    try
                    {
                        txtIDBenhAn.Text = Utility.sDbnull(objEmrBa.IdBa);
                        txtMaBenhAn.Text = Utility.sDbnull(objEmrBa.MaBa);
                        //txtBenhNgoai_Khoa.Text = Utility.sDbnull(objEmrBa.BenhNgoaiKhoa);
                        if (objEmrBa.VaovienNgay.HasValue)
                            dtpNgayTiepdon.Value = objEmrBa.VaovienNgay.Value;
                        else
                            dtpNgayTiepdon.ResetText();
                        txtBSlamBA.SetId(objEmrBa.IdBacsiLamBA);
                        txtBenhAnLyDoNhapVien._Text = objEmrBa.VaovienLydovaovien;
                        txtBenhAnQuaTrinhBenhLy.Text = objEmrBa.HoibenhQuatrinhbenhly;
                        txtTiensuBanthan.Text = objEmrBa.HoibenhTiensubanthan;
                        txtTiensuGiadinh.Text = objEmrBa.HoibenhTiensugiadinh;

                        txtBenhAnToanThan.Text = objEmrBa.KhambenhToanthan;
                        txtCacBoPhan.Text = objEmrBa.KhambenhCacbophan;

                        txtTomtatKQCLS.Text = objEmrBa.KhambenhTomtatbenhan;
                        txtChandoanBandau.Text = objEmrBa.CdBandau;
                        txtDaXulyThuocChamsoc.Text = objEmrBa.KhambenhDaxulychamsoc;
                        txtTenbenhchinh.Text = objEmrBa.RavienTenBenhchinh;
                        txtMabenhchinh.Text = objEmrBa.RavienMaBenhchinh;
                        if (objEmrBa.NgayDieutriTu.HasValue)
                            dtpDieutriTungay.Value = objEmrBa.NgayDieutriTu.Value;
                        else
                            dtpDieutriTungay.Value = objLuotkham.NgayTiepdon;
                        if (objEmrBa.NgayDieutriDen.HasValue)
                            dtpDieutriDenngay.Value = objEmrBa.NgayDieutriDen.Value;
                        else
                            dtpDieutriDenngay.ResetText();

                        chkQLNBCoQuanYTe.Checked = Utility.Bool2Bool(objEmrBa.NoigioithieuCoquanyte);
                        chkQLNBCoQuanYTe.Checked = Utility.Bool2Bool(objEmrBa.NoigioithieuTuden);
                        chkQLNBCoQuanYTe.Checked = Utility.Bool2Bool(objEmrBa.NoigioithieuKhac);
                       
                        string ICD_chinh_Name = "";
                        string ICD_chinh_Code = "";
                        string ICD_Phu_Name = "";
                        string ICD_Phu_Code = "";

                        Utility.GetChanDoanChinhPhu(objLuotkham.MabenhChinh,
                                            objLuotkham.MabenhPhu,
                                            ref ICD_chinh_Name,
                                            ref ICD_chinh_Code, ref ICD_Phu_Name,
                                            ref ICD_Phu_Code);
                        
                        txtMach.Text = Utility.sDbnull(objEmrBa.KbMach);
                        txtNhietDo.Text = Utility.sDbnull(objEmrBa.KbNhietdo);
                        txtha.Text = Utility.sDbnull(objEmrBa.KbHuyetap);
                        txtNhipTho.Text = Utility.sDbnull(objEmrBa.KbNhiptho);
                        txtCanNang.Text = Utility.sDbnull(objEmrBa.KbCannang);
                        txtChieuCao.Text = Utility.sDbnull(objEmrBa.KbChieucao);
                        tinhBMI();
                        txtBenhAnToanThan.Text = Utility.sDbnull(objEmrBa.KhambenhToanthan);// Utility.sDbnull(dr["KbToanThan"].ToString());
                        txtCacBoPhan.Text = Utility.sDbnull(objEmrBa.KhambenhCacbophan);
                       

                        txtTKBAQuaTrinhBenhLy.Text = Utility.sDbnull(objEmrBa.TongketbaQuatrinhbenhlyDienbienlamsang);
                        txtTKBATTomTatKetQua.Text = Utility.sDbnull(objEmrBa.TongketbaTomtatKqcls);
                        txtTKBAPhuongPhapDieuTri.Text = Utility.sDbnull(objEmrBa.TongketbaPhuongphapdieutri);
                        txtTKBATinhTrangRaVien.Text = Utility.sDbnull(objEmrBa.TongketbaTinhtrangNguoiravien);// Utility.sDbnull(dr["TkbaTtrv"].ToString());
                        txtTKBAHuongDieuTri.Text = Utility.sDbnull(objEmrBa.TongketbaHuongdieutritieptheo);// Utility.sDbnull(dr["TkbaHdt"].ToString());

                        txtNguoiGiaoHoSo.SetId(objEmrBa.IdNguoigiaoHoso);
                        txtNguoiNhanHoSo.SetId(objEmrBa.IdNguoinhanHoso);
                        txtBSDieuTri.SetId(objEmrBa.IdBacsiDieutri);
                        txtGDBV.SetId(objEmrBa.IdGiamdoc);
                        txtTruongkhoa.SetId(objEmrBa.IdTruongkhoadieutri);
                        txtBacsiKham.SetId(objEmrBa.IdBacsiKham);
                        txtBSlamBA.SetId(objEmrBa.IdBacsiLamBA);
                        // txtBSlamBA.SetCode(Utility.sDbnull(objEmrBa.b));
                        txtB_CTScanner.Text = Utility.sDbnull(objEmrBa.TongketbaSotoCt);
                        txtB_Xquang.Text = Utility.sDbnull(objEmrBa.TongketbaSotoXquang);
                        txtB_SieuAm.Text = Utility.sDbnull(objEmrBa.TongketbaSotoSieuam);
                        txtB_XetNghiem.Text = Utility.sDbnull(objEmrBa.TongketbaSotoXetnghiem);
                        txtB_Khac.Text = Utility.sDbnull(objEmrBa.TongketbaSotoKhac);
                        if (objEmrBa.TongketbaNgay.HasValue)
                            dtpB_NgayTongKet.Value = objEmrBa.TongketbaNgay.Value;
                        else
                            dtpB_NgayTongKet.Value = DateTime.Now;
                    }
                    catch (Exception ex)
                    {
                        Utility.ShowMsg(ex.ToString());
                    }
                }
                else//Auto fill
                {
                    //Điền các thông tin mặc định người bệnh
                    //Trang 1
                    m_enAct = action.Insert;
                    
                   
                    KcbDangkyKcb objCongkham = KcbDangkyKcb.FetchByID(objLuotkham.IdCongkhamNhapvien);
                    if (objCongkham != null)
                    {
                        if (objCongkham.ThoigianBatdau.HasValue)
                            dtpDieutriTungay.Value = objCongkham.ThoigianBatdau.Value;
                        else
                            dtpDieutriTungay.Value = objLuotkham.NgayTiepdon;
                        if (objCongkham.ThoigianKetthuc.HasValue)
                            dtpDieutriDenngay.Value = objCongkham.ThoigianKetthuc.Value;
                        else
                            dtpDieutriDenngay.ResetText();
                        //điền thông tin ngày khám, bác sĩ khám
                        dtpNgayKham.Value = objLuotkham.NgayKetthuc.Value;
                        txtBacsiKham.SetId(objCongkham.IdBacsikham);
                    }
                    else
                    {
                        dtpNgayKham.Value= objLuotkham.NgayTiepdon;
                        txtBacsiKham.SetId(-1);
                        dtpDieutriTungay.Value = objLuotkham.NgayTiepdon;
                        dtpDieutriDenngay.ResetText();
                    }    
                    KcbThongtinchung tef = new Select().From(KcbThongtinchung.Schema)
                        .Where(KcbThongtinchung.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        .And(KcbThongtinchung.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<KcbThongtinchung>();
                    if (tef != null)
                    {
                        txtMach.Text = Utility.sDbnull(tef.Mach);
                        txtNhietDo.Text = Utility.sDbnull(tef.Nhietdo);
                        txtha.Text = Utility.sDbnull(tef.Huyetap);
                        txtNhipTho.Text = Utility.sDbnull(tef.Nhiptho);
                        txtCanNang.Text = Utility.sDbnull(tef.Cannang);
                        txtChieuCao.Text = Utility.sDbnull(tef.Chieucao);
                        tinhBMI();
                    }

                    
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            finally
            {
                EnableBA();
            }
        }
        KcbChandoanKetluan objHoiBenhChanDoan;
        void FillHoibenhChandoan()
        {
            try
            {
                objHoiBenhChanDoan = new Select().From(KcbChandoanKetluan.Schema)
                       .Where(KcbChandoanKetluan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                       .And(KcbChandoanKetluan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                       .And(KcbChandoanKetluan.Columns.Noitru).IsEqualTo(0)
                       .ExecuteSingle<KcbChandoanKetluan>();
                if (objHoiBenhChanDoan != null)
                {
                    txtBenhAnLyDoNhapVien._Text = objHoiBenhChanDoan.TrieuchungBandau;
                    txtBenhAnQuaTrinhBenhLy.Text = objHoiBenhChanDoan.QuatrinhBenhly;
                    txtTiensuBanthan.Text = objHoiBenhChanDoan.TiensuBenh;
                    txtTiensuGiadinh.Text = objHoiBenhChanDoan.TiensuGiadinh;
                    txtTomtatKQCLS.Text = objHoiBenhChanDoan.TomtatCls;
                    txtChandoanBandau.Text = objHoiBenhChanDoan.Chandoan;
                    txtChanDoanNoiGioiThieu.Text = "";
                    DataRow dr = globalVariables.gv_dtDmucBenh.AsEnumerable().Where(c =>Utility.sDbnull( c[DmucBenh.Columns.MaBenh]) == objHoiBenhChanDoan.MabenhChinh).FirstOrDefault();
                    if (dr != null)
                        txtTenbenhchinh.Text =Utility.sDbnull( dr[DmucBenh.Columns.TenBenh]);
                    else
                        txtTenbenhchinh.Text = "";
                    txtMabenhchinh.Text = objHoiBenhChanDoan.MabenhChinh;
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        void FillPhieuKCB()
        {
            objPKB = new Select().From(EmrPhieukhambenh.Schema)
                 .Where(EmrPhieukhambenh.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                 .And(EmrPhieukhambenh.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                 .ExecuteSingle<EmrPhieukhambenh>();
            if (objPKB != null)
            {
                dtpNgayKham.Value = objPKB.NgayKham;
                txtBacsiKham.SetId(objPKB.IdBacsi);
                txtBenhAnToanThan.Text = Utility.sDbnull(objPKB.ToanThan);// Utility.sDbnull(dr["KbToanThan"].ToString());
                txtCacBoPhan.Text = Utility.sDbnull(objPKB.BoPhan);
               
            }
            else
            {
                dtpNgayKham.Value = DateTime.Now;
                txtBacsiKham.SetId(globalVariables.gv_intIDNhanvien);
            }    
        }
       
        //VKcbLuotkham objBenhnhan = null;
        public KcbLuotkham objLuotkham = null;
        KcbDanhsachBenhnhan objBenhnhan = null;
        private void SinhMaBenhAn()
        {
            //txtMaBenhAn.Text = THU_VIEN_CHUNG.SinhMaBenhAn_NoiTru();
            string MaxMaBenhAN = "";
            StoredProcedure sp = SPs.EmrBaSinhMaBA(cboLoaiBA.SelectedValue.ToString(), MaxMaBenhAN);
            sp.Execute();
            sp.OutputValues.ForEach(delegate(object objOutput) { MaxMaBenhAN = (String)objOutput; });

            txtMaBenhAn.Text = MaxMaBenhAN;

        }
        void ModifyCommand()
        {
            mnuInTo1.Enabled = mnuInTo2.Enabled = mnuInTomtatBA.Enabled = mnuInVoBA.Enabled = mnuInBA.Enabled = objLuotkham != null && objEmrBa!=null;
            cmdXoaBenhAn.Enabled = objLuotkham != null && objEmrBa != null;
            cmdKhoitaoBA.Enabled = objEmrBa == null;
            cmdSave.Enabled = objEmrBa != null && objEmrBa.TrangThai <= 1;
            //cmdSave.Tag = objEmrBa!=null &&  objEmrBa.TrangThai == 1 ? "HUY" : "LUU";
            //cmdSave.Text = objEmrBa != null && objEmrBa.TrangThai == 1 ? "2. Hủy Lưu" : "2. Lưu BA (Ctrl+S)";
            cmdKetthucBA.Enabled = objEmrBa != null && objEmrBa.TrangThai >= 1;
            cmdKetthucBA.Tag = objEmrBa != null && objEmrBa.TrangThai == 2 ? "HUY" : "HOANTAT";
            cmdKetthucBA.Text = objEmrBa != null && objEmrBa.TrangThai == 2 ? "3. Làm lại BA" : "3. Hoàn tất BA";
        }

        private void txtMaLanKham_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    objLuotkham.MaLuotkham = THU_VIEN_CHUNG.SinhMaHoSoKhiTimKiem(objLuotkham.MaLuotkham);
            //    if (!IsValidData()) return;
            //    FillBenhAnByPatientCode();
            //}
        }
       
        private DataTable getChitietCLS()
        {
            int status = 0;
            DataTable temdt = SPs.ClsKetQuaXetNghiem(-1,"",objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, 1, status).GetDataSet().Tables[0];

            return temdt;
        }

        private void cmdXoaBenhAn_Click(object sender, EventArgs e)
        {
            try
            {

                objEmrBa = EmrBa.FetchByID(Utility.Int64Dbnull(txtIDBenhAn.Text));
                if (objEmrBa == null)
                {
                    Utility.ShowMsg("Bạn chưa chọn bệnh án nào để xóa hoặc bệnh án muốn xóa không tồn tại trong hệ thống. Vui lòng gõ lại mã lượt khám để kiểm tra");
                    ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Focus();
                    ucThongtinnguoibenh_emr_basic1.txtMaluotkham.SelectAll();
                    return;
                }
                EmrHosoluutru hosoba = new Select().From(EmrHosoluutru.Schema)
                    .Where(EmrHosoluutru.Columns.IdBa).IsEqualTo(objEmrBa.IdBa)
                    .And(EmrHosoluutru.Columns.MaBa).IsEqualTo(objEmrBa.MaBa)
                    .And(EmrHosoluutru.Columns.LoaiBa).IsEqualTo(objEmrBa.LoaiBa)
                    .And(EmrHosoluutru.Columns.IdBenhnhan).IsEqualTo(objEmrBa.IdBenhnhan)
                     .And(EmrHosoluutru.Columns.MaLuotkham).IsEqualTo(objEmrBa.MaLuotkham)
                    .ExecuteSingle<EmrHosoluutru>();
                if (Utility.Int32Dbnull(hosoba.TrangThai, 0) == 1)
                {
                    Utility.ShowMsg("Bệnh án đang ở trạng thái đóng nên không thể xóa. Muốn xóa cần quay về trạng thái mở");
                    return;
                }
                if (Utility.Int32Dbnull(hosoba.TrangThai, 0) == 2)
                {
                    Utility.ShowMsg("Bệnh án đang ở trạng thái đóng và đã gửi KHTH phê duyệt nên không thể xóa");
                    return;
                }
                if (Utility.Int32Dbnull(hosoba.TrangThai, 0) == 3)
                {
                    Utility.ShowMsg("Bệnh án đang ở trạng thái đã được duyệt bởi KHTH và đưa vào lưu trữ nên không thể xóa");
                    return;
                }
                if (objEmrBa != null && (Utility.Coquyen("EMR_XOA_BA") || globalVariables.UserName == objEmrBa.NguoiTao))
                {
                    if (Utility.AcceptQuestion("Bạn có muốn xóa thông tin bệnh án đang chọn không ?", "Thông báo", true))
                    {
                        using (var Scope = new TransactionScope())
                        {
                            using (var dbScope = new SharedDbConnectionScope())
                            {
                                new Delete().From(EmrBa.Schema)
                                      .Where(EmrBa.Columns.IdBa).IsEqualTo(objEmrBa.IdBa)
                                      .And(EmrBa.Columns.LoaiBa).IsEqualTo(objEmrBa.LoaiBa)
                                      .And(EmrBa.Columns.MaCoso).IsEqualTo(objEmrBa.MaCoso)
                                      .Execute();
                                new Delete().From(EmrHosoluutru.Schema)
                                      .Where(EmrHosoluutru.Columns.IdBa).IsEqualTo(objEmrBa.IdBa)
                                      .And(EmrHosoluutru.Columns.LoaiBa).IsEqualTo(objEmrBa.LoaiBa)
                                      .And(EmrBa.Columns.MaCoso).IsEqualTo(objEmrBa.MaCoso)
                                      .Execute();
                                emrdoc.DeleteDocument_WithoutTransaction(objEmrBa.IdBa, new List<string>() { Utility.LayMaBA(objEmrBa.LoaiBa), "BENHAN_BIA", "BENHAN_TO1", "BENHAN_TO2", "BENHAN_TO3", "BENHAN_TO4" }, "");
                                Utility.Log("frm_BenhAn_NgoaiTru", globalVariables.UserName, string.Format("Xóa bệnh án id={0}, loại BA={1}, mã BA={2} của người bệnh id ={3}, mã lần khám {4} thành công", objEmrBa.IdBa, objEmrBa.LoaiBa, objEmrBa.MaBa, objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham), newaction.Delete, "UI");
                            }
                            Scope.Complete();
                        }
                        this.DialogResult = DialogResult.OK;
                        Utility.ShowMsg("Bạn xóa bệnh án thành công", "Thông báo");
                        ucThongtinnguoibenh_emr_basic1.Refresh();
                        ModifyCommand();



                    }
                }
                else
                {
                    Utility.ShowMsg("Bạn không có quyền xóa BA(EMR_XOA_BA) hoặc không phải là người tạo Bệnh án");
                    return;

                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }

        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            //var frm = new frm_TimKiem_BN();
            //frm.ShowDialog();
            //if (frm.b_Cancel)
            //{
            //    objLuotkham.MaLuotkham = Utility.sDbnull(frm.SoHSBA);
            //    FillBenhAnByPatientCode();
            //}
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
        private void cmdUpdateBNToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cmdLamMoi_Click(object sender, EventArgs e)
        {
            ClearControl();
            objEmrBa = null;
            //objBenhnhan = null;
            objLuotkham = null;
            m_enAct = action.Insert;
            ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Focus();
            ucThongtinnguoibenh_emr_basic1.txtMaluotkham.SelectAll();
            ModifyCommand();
        }

        bool _isCounterpart = false; //mục đích dùng để ktra xem quá tình bệnh lý ở tổng kết bệnh án đã chỉnh sửa chưa, nếu chỉnh sửa rồi thì ko cập nhật lại
        private void txtBenhAnQuaTrinhBenhLy_Enter(object sender, EventArgs e)
        {
           
        }

        private void txtBenhAnQuaTrinhBenhLy_TextChanged(object sender, EventArgs e)
        {
            
        }


        private void cmdPrint_Click(object sender, EventArgs e)
        {
            ctxIn.Show(cmdPrint, new Point(0, cmdPrint.Height));

        }

        private void cmdPrint_MouseHover(object sender, EventArgs e)
        {
        }

        private void cmdPrint_MouseLeave(object sender, EventArgs e)
        {
           
        }

        private void txtMaBenhAn_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtCanNang_TextChanged(object sender, EventArgs e)
        {
            tinhBMI();
        }

        private void txtChieuCao_TextChanged(object sender, EventArgs e)
        {
            tinhBMI();
        }
        void tinhBMI()
        {
            if (txtCanNang.Text.Trim() != string.Empty && txtChieuCao.Text.Trim() != string.Empty) //2 ô có giá trị thì mới tính
            {
                if (txtCanNang.Text.Trim().All(char.IsDigit) && txtChieuCao.Text.Trim().All(char.IsDigit)) //2 ô phải là kiểu số
                {
                    if (Utility.DecimaltoDbnull(txtCanNang.Text, 0) > 0 && Utility.DecimaltoDbnull(txtChieuCao.Text, 0) > 0) //2 giá trị > 0
                    {
                        decimal bmi = Utility.DecimaltoDbnull(txtCanNang.Text, 0) / (Utility.DecimaltoDbnull(txtChieuCao.Text, 0) / 100 * Utility.DecimaltoDbnull(txtChieuCao.Text, 0) / 100);
                        txtBMI.Text = Utility.sDbnull(Math.Round(bmi, 2));
                    }
                }
            }
        }

        private void mnuInVoBA_Click(object sender, EventArgs e)
        {
            clsInBA.InBA(objEmrBa.IdBa, objEmrBa.MaBa, objEmrBa.LoaiBa, objLuotkham, null, null, null, null, 0, false);
        }

        private void mnuInTomtatBA_Click(object sender, EventArgs e)
        {
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Chưa có thông tin người bệnh để thực hiện thao tác in tóm tắt bệnh án");
                return;
            }
            EmrTomtatBa objTKBA =new Select().From(EmrTomtatBa.Schema)
                .Where(EmrTomtatBa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                .And(EmrTomtatBa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                .ExecuteSingle<EmrTomtatBa>();
            if (objTKBA == null || objTKBA.Id <= 0)
            {
                Utility.ShowMsg("Bạn cần tạo Tóm tắt hồ sơ bệnh án trước khi thực hiện in");
                return;
            }
            clsInBA.InTomTatBA(objTKBA);
        }

        private void mnuInTo1_Click(object sender, EventArgs e)
        {
            clsInBA.InBA(objEmrBa.IdBa, objEmrBa.MaBa, objEmrBa.LoaiBa, objLuotkham, null, null, null, null, 1, false);
        }

        private void mnuInTo2_Click(object sender, EventArgs e)
        {
            clsInBA.InBA(objEmrBa.IdBa, objEmrBa.MaBa, objEmrBa.LoaiBa, objLuotkham, null, null, null, null, 2, false);
        }

        private void mnuInTo3_Click(object sender, EventArgs e)
        {
            clsInBA.InBA(objEmrBa.IdBa, objEmrBa.MaBa, objEmrBa.LoaiBa, objLuotkham, null, null, null, null, 3, false);
        }

        private void mnuInTo4_Click(object sender, EventArgs e)
        {
            clsInBA.InBA(objEmrBa.IdBa, objEmrBa.MaBa, objEmrBa.LoaiBa, objLuotkham, null, null, null, null, 4, false);
        }
      
        private void mnuInBA_Click(object sender, EventArgs e)
        {
            clsInBA.InBA(objEmrBa.IdBa, objEmrBa.MaBa, objEmrBa.LoaiBa, objLuotkham, null, null, null, null, 100, false);
        }

        private void cmdRefreshChucnangsong_Click(object sender, EventArgs e)
        {

            try
            {
                frm_XemthongtinChucnangsong _XemthongtinChucnangsong = new frm_XemthongtinChucnangsong(objLuotkham, true, 100);
                _XemthongtinChucnangsong._OnSelectMe += _XemthongtinChucnangsong__OnSelectMe;
                _XemthongtinChucnangsong.ShowDialog();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        void _XemthongtinChucnangsong__OnSelectMe(string mach, string nhietdo, string nhiptho, string huyetap, string chieucao, string cannang, string bmi, string nhommau, string SPO2)
        {
            txtMach.Text = mach;
            txtNhietDo.Text = nhietdo;
            txtNhipTho.Text = nhiptho;
            txtha.Text = huyetap;
            txtChieuCao.Text = chieucao;
            txtCanNang.Text = cannang;
            txtBMI.Text = bmi;
        }
        EmrTomtatBa objTKBA;
        void FillTongketBenhAn()
        {
            try
            {
                objTKBA=  new Select().From(EmrTomtatBa.Schema)
                    .Where(EmrTomtatBa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                    .And(EmrTomtatBa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                    .ExecuteSingle<EmrTomtatBa>();
                if (objTKBA != null)
                {
                    txtBSDieuTri.SetId(objTKBA.IdBacsiDieutri);
                    dtpNgayTKBA.Value = objTKBA.NgayTtba.Value;
                    txtTKBAQuaTrinhBenhLy.Text = objTKBA.QuatrinhbenhlyDienbienlamsang;
                    txtTKBATTomTatKetQua.Text = objTKBA.TomtatKqcls;
                    txtTKBAPhuongPhapDieuTri.Text = objTKBA.PhuongphapDieutri;
                    txtTKBATinhTrangRaVien.Text = objTKBA.TinhtrangRavienMota;
                    txtTKBAHuongDieuTri.Text = objTKBA.HuongDieutri;

                    txtNguoiGiaoHoSo.SetId(objTKBA.IdNguoigiaoHoso);
                    txtNguoiNhanHoSo.SetId(objTKBA.IdNguoinhanHoso);

                    txtB_CTScanner.Text = Utility.sDbnull(objTKBA.SotoCt);
                    txtB_Xquang.Text = Utility.sDbnull(objTKBA.SotoXquang);
                    txtB_SieuAm.Text = Utility.sDbnull(objTKBA.SotoSieuam);
                    txtB_XetNghiem.Text = Utility.sDbnull(objTKBA.SotoXetnghiem);
                    txtB_Khac.Text = Utility.sDbnull(objTKBA.SotoKhac);
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        private void cmdSyncTKBA_Click(object sender, EventArgs e)
        {
            FillTongketBenhAn();
        }

        private void cmdKhoitaoBA_Click(object sender, EventArgs e)
        {
            LuuBA(0);
        }

        private void cmdLaythongtinKCB_Click(object sender, EventArgs e)
        {
            FillHoibenhChandoan();
        }


        private void cmdKCB_Click(object sender, EventArgs e)
        {
            try
            {
                if ( objLuotkham == null)
                {
                    Utility.ShowMsg("Bạn cần chọn ít nhất một người bệnh trên danh sách người bệnh để bắt đầu công việc khám cơ bản");
                    return;
                }
                frm_KCBCoban _KCBCoban = new frm_KCBCoban(objLuotkham, null);
                _KCBCoban.ShowDialog();
                FillPhieuKCB();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void chkEditTKBA_CheckedChanged(object sender, EventArgs e)
        {
            //txtTKBAQuaTrinhBenhLy.ReadOnly = txtTKBATTomTatKetQua.ReadOnly
            //    = txtTKBAPhuongPhapDieuTri.ReadOnly = txtTKBATinhTrangRaVien.ReadOnly
            //    = txtTKBAHuongDieuTri.ReadOnly = txtB_Xquang.ReadOnly = txtB_CTScanner.ReadOnly = txtB_SieuAm.ReadOnly
            //    = txtB_XetNghiem.ReadOnly = txtB_Khac.ReadOnly = txtNguoiGiaoHoSo.ReadOnly = txtNguoiNhanHoSo.ReadOnly = txtBSDieuTri.ReadOnly
            //    = !chkEditTKBA.Checked && !chkEditTKBA.Visible;
        }

        private void chkEditPKB_CheckedChanged(object sender, EventArgs e)
        {
           // txtBacsiKham.ReadOnly = txtBenhAnToanThan.ReadOnly = txtCacBoPhan.ReadOnly = !chkEditPKB.Checked && !chkEditPKB.Visible;
        }

        private void cmdSave_Click_1(object sender, EventArgs e)
        {
            LuuBA(1);
        }
    }
}
