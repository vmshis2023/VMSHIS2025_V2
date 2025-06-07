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
using VMS.EMR.PHIEUKHAM;
using VMS.Emr;

namespace VNS.HIS.UI.BA
{
    public partial class frm_BenhAn_NgoaiTru : Form
    {
        public delegate void OnCreated(long id,string ma_ba, action m_enAct);
        public event OnCreated _OnCreated;
        string lstLoaiBA = "";
        public frm_BenhAn_NgoaiTru(string lstLoaiBA)
        {
            InitializeComponent();
            this.lstLoaiBA = lstLoaiBA;
            Utility.SetVisualStyle(this);
            chkQLNBTuDen.CheckedChanged += chkQLNBTuDen_CheckedChanged;
            chkQLNBCoQuanYTe.CheckedChanged += chkQLNBCoQuanYTe_CheckedChanged;
            chkQLNBKhac.CheckedChanged += chkQLNBKhac_CheckedChanged;
           
            txtIDBenhAn.KeyDown += txtIDBenhAn_KeyDown;
            txtMaBenhAn.KeyDown += txtMaBenhAn_KeyDown;
            ucThongtinnguoibenh_emr_basic1.trangthai_noitru = 5;
            Utility.setEnterEvent(this);
          
            txtB_CTScanner.TextChanged += soluongto_TextChanged;
            txtB_Khac.TextChanged += soluongto_TextChanged;
            txtB_SieuAm.TextChanged += soluongto_TextChanged;
            txtB_XetNghiem.TextChanged += soluongto_TextChanged;
            txtB_Xquang.TextChanged += soluongto_TextChanged;
        }
        void PhanquyenTinhnang()
        {
            cmdKCB.Visible =  Utility.Coquyen("EMR_THEM_PHIEUKCB");
            txtBenhAnToanThan.ReadOnly = txtCacBoPhan.ReadOnly = Utility.Coquyen("EMR_SUATRUCTIEP_THONGTINKHAMBENH");
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

        void ucThongtinnguoibenh_emr_basic1__OnEnterMe()
        {
            if (ucThongtinnguoibenh_emr_basic1.objLuotkham != null)
            {
                if (ucThongtinnguoibenh_emr_basic1.objLuotkham.TrangthaiNoitru <= 0)
                {
                    Utility.ShowMsg(string.Format("Người bệnh {0} với mã lần khám {1} đang ở trạng thái ngoại trú nên bạn không thể thực hiện tạo BA được. Vui lòng kiểm tra lại", ucThongtinnguoibenh_emr_basic1.txtTenBN.Text, ucThongtinnguoibenh_emr_basic1.objLuotkham.MaLuotkham));
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
                if (!KiemTraThongTin()) return;
                ClearControl();
                FillData4Update();
                dtpNgayTiepdon.Focus();
                ModifyCommand();
            }
        }

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
        private void GetChanDoanChinhPhu(string ICD_chinh, string IDC_Phu, ref string ICD_chinh_Name,
            ref string ICD_chinh_Code, ref string ICD_Phu_Name, ref string ICD_Phu_Code)
        {
            try
            {
                List<string> lstICD = ICD_chinh.Split(',').ToList();
                DmucBenhCollection _list = new Select().From(DmucBenh.Schema).Where(DmucBenh.Columns.MaBenh).In(lstICD).ExecuteAsCollection<DmucBenhCollection>();
                    //new DmucBenh().FetchByQuery(               DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, lstICD));
                foreach (DmucBenh _objEmrBa in _list)
                {
                    ICD_chinh_Name += _objEmrBa.TenBenh + ";";
                    ICD_chinh_Code += _objEmrBa.MaBenh + ";";
                }
                lstICD = IDC_Phu.Split(',').ToList();
                _list =
                    new DmucBenhController().FetchByQuery(
                        DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, lstICD));
                foreach (DmucBenh _objEmrBa in _list)
                {
                    ICD_Phu_Name += _objEmrBa.TenBenh + ";";
                    ICD_Phu_Code += _objEmrBa.MaBenh + ";";
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
            }
        }

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
                                                      .And(KcbChandoanKetluan.Columns.IdBenhnhan).IsEqualTo(objBenhnhan.IdBenhnhan).OrderAsc(KcbChandoanKetluan.Columns.NgayChandoan);
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
                    GetChanDoanChinhPhu(Utility.sDbnull(objDiagInfo.MabenhChinh, ""), Utility.sDbnull(objDiagInfo.MabenhPhu, ""), ref ICD_Name, ref ICD_Code, ref ICD_Phu_Name, ref ICD_Phu_Code);
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
       
        private bool KiemTraThongTin()
        {
            objLuotkham = Utility.getKcbLuotkham(objLuotkham);
            if (objLuotkham==null)
            {
                Utility.ShowMsg("Bệnh nhân không tồn tại trong CSDL. Vui lòng kiểm tra lại", "Thông báo", MessageBoxIcon.Warning);
                ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Focus();
                return false;
            }
            if (Utility.sDbnull(cboLoaiBA.SelectedValue, "-1") == "-1")
            {
                Utility.ShowMsg("Cần chọn loại bệnh án");
                cboLoaiBA.Focus();
                return false;
            }
            if (Utility.sDbnull( txtBSlamBA.Text).Length>0 && txtBSlamBA.MyCode=="-1")
            {
                Utility.ShowMsg("Bác sĩ làm Bệnh án cần thuộc danh mục bác sĩ. Mời bạn chọn từ danh mục có sẵn");
                txtBSlamBA.Focus();
                return false;
            }
            if (Utility.sDbnull(txtBSDieuTri.Text).Length > 0 && txtBSDieuTri.MyCode == "-1")
            {
                Utility.ShowMsg("Bác sĩ điều trị cần thuộc danh mục bác sĩ. Mời bạn chọn từ danh mục có sẵn");
                txtBSDieuTri.Focus();
                return false;
            }
            return true;
        }
        EmrDocuments emrdoc = new EmrDocuments();

        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!KiemTraThongTin()) return;
                objEmrBa = TaoEmrBa();
                //if (objEmrBa.IdBa > 0 && objEmrBa.MaBa != maBA)
                //{
                //    if(Utility.AcceptQuestion("Mã bệnh án cũ :{0} đang khác với mã bệnh án nhập tay: {1}. Bạn có chắc chắn muốn cập nhật lại thành mã bệnh án mới","",))
                //    {
                //    }
                //}
                 EmrHosoluutru hsba =null;
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
                
                if (Utility.Coquyen("EMR_SUA_PHIEUKCB") && objEmrBa.IdBa > 0)
                {
                    TaoPhieuKCB();
                    objPKB.Save();
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật thông tin phiếu khám toàn thân tại chức năng EMR cho người bệnh: {0}-{1} thành công", objEmrBa.MaLuotkham, objEmrBa.TenBenhnhan), objPKB.IsNew ? newaction.Insert : newaction.Update, "EMR");
                }
                if (Utility.Coquyen("EMR_SUA_TKBA") && objEmrBa.IdBa >0)
                {
                    TaoPhieuTKBA();
                    objTKBA.Save();
                    if(objTKBA.IsNew)
                    {
                       
                        emrdoc.InitDocument(objTKBA.IdBenhnhan, objTKBA.MaLuotkham, Utility.Int64Dbnull(objTKBA.Id), objTKBA.NgayTtba.Value, Loaiphieu_HIS.BENHAN, "BA_TKBA", objTKBA.NguoiTao, -1, -1, Utility.Byte2Bool(0), "");
                        emrdoc.Save();
                    }    
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật thông tin Tổng kết BA tại chức năng EMR cho người bệnh: {0}-{1} thành công", objEmrBa.MaLuotkham, objEmrBa.TenBenhnhan), objTKBA.IsNew ? newaction.Insert : newaction.Update, "EMR");
                }
               
                objEmrBa.Save();
                if (hsba != null )
                {
                    hsba.IdBa = objEmrBa.IdBa;
                    hsba.Save();
                }
                txtIDBenhAn.Text = objEmrBa.IdBa.ToString();
                if (m_enAct == action.Insert)
                {
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Thêm mới BA nội trú cho bệnh nhân: {0}-{1} thành công", objEmrBa.IdBa, objEmrBa.TenBenhnhan), objEmrBa.IsNew ? newaction.Insert : newaction.Update, "UI");
                    MessageBox.Show("Đã thêm mới Bệnh án thành công. Nhấn Ok để kết thúc");
                    cmdXoaBenhAn.Enabled = cmdPrint.Enabled = true;
                    if (_OnCreated != null) _OnCreated(objEmrBa.IdBa, objEmrBa.MaBa, action.Insert);
                    m_enAct = action.Update;
                   
                    emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN, "BA_TO_1", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "");
                    emrdoc.Save();
                    emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN, "BA_TO_2", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "");
                    emrdoc.Save();

                    emrdoc.InitDocument(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham, Utility.Int64Dbnull(objEmrBa.IdBa), objEmrBa.NgaylamBa.Value, Loaiphieu_HIS.BENHAN, "BA_FULL", objEmrBa.NguoiTao, -1, -1, Utility.Byte2Bool(0), "");
                    emrdoc.Save();
                }
                else if (m_enAct == action.Update)
                {
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật Bệnh án Ngoại trú cho bệnh nhân: {0}-{1} thành công", objEmrBa.IdBa, objEmrBa.TenBenhnhan), objEmrBa.IsNew ? newaction.Insert : newaction.Update, "UI");
                    if (_OnCreated != null) _OnCreated(objEmrBa.IdBa, objEmrBa.MaBa, action.Update);
                    MessageBox.Show("Đã cập nhật Bệnh án thành công. Nhấn Ok để kết thúc");
                    m_enAct = action.Update;
                }
                EnableBA();
                //Utility.ShowMsg("Lưu thông tin thành công", "Thông báo");
                dtDataBA = SPs.EmrBaLaythongtinIn(-1, "", objBenhnhan.IdBenhnhan, objLuotkham.MaLuotkham).GetDataSet().Tables[0];
                _isSuccess = true;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
            finally
            {
                if (objEmrBa != null && _isSuccess)
                {
                    new Update(KcbLuotkham.Schema)
                        .Set(KcbLuotkham.Columns.SoBenhAn).EqualTo(objEmrBa.MaBa)
                        .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).Execute();
                   // EmrThemBenhAn();
                }
              
            }
        }
       
        void TaoPhieuTKBA()
        {
             objTKBA = new Select().From(EmrTongketBenhan.Schema).Where(EmrTongketBenhan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).And(EmrTongketBenhan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<EmrTongketBenhan>();
            if (objTKBA == null) objTKBA = new EmrTongketBenhan();
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
            objTKBA.TiensuBenh = "";
            objTKBA.TomtatKqcls = "";
            objTKBA.QuatrinhbenhlyDienbienlamsang = objEmrBa.TongketbaQuatrinhbenhlyDienbienlamsang;
            objTKBA.TomtatKqcls = objEmrBa.TongketbaTomtatKqcls;
            objTKBA.TinhtrangRavienMota = objEmrBa.TongketbaTinhtrangNguoiravien;
            objTKBA.PhuongphapDieutri = objEmrBa.TongketbaPhuongphapdieutri;
            objTKBA.HuongDieutri = objEmrBa.TongketbaHuongdieutritieptheo;
            objTKBA.NgayTtba = objEmrBa.TongketbaNgay;
            objTKBA.Noikhoa = 0;
            objTKBA.NoikhoaMota = "";
            objTKBA.Pttt = 0;
            objTKBA.PtttMota = "";
        }
        void TaoPhieuKCB()
        {
            //Refresh lại thông tin KCB
            objPKB = new Select().From(EmrPhieukhambenh.Schema)
                .Where(EmrPhieukhambenh.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                .And(EmrPhieukhambenh.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
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
            objPKB.Tieuhoa = Utility.sDbnull(txtCacBoPhan.Text);
        }
        void EnableBA()
        {
            cboLoaiBA.Enabled = txtIDBenhAn.Enabled=cmdKhoitaoBA.Enabled= m_enAct == action.Insert;

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
                objEmrBa.TenBenhnhan = objBenhnhan.TenBenhnhan;
                objEmrBa.MaLuotkham = objLuotkham.MaLuotkham;
                objEmrBa.MaYte = objLuotkham.MaYte;
                objEmrBa.NgaySinh = objBenhnhan.NgaySinh.Value;
                objEmrBa.MaGioitinh =Utility.ByteDbnull( objBenhnhan.IdGioitinh)==0?"M":"F";
                objEmrBa.GioiTinh = objBenhnhan.GioiTinh;
                objEmrBa.Tuoi =(byte) objBenhnhan.Tuoi;
                objEmrBa.LoaiTuoi = (byte)objLuotkham.LoaiTuoi;
                
                
                objEmrBa.MaNghenghiep = objBenhnhan.NgheNghiep;
                objEmrBa.TenNghenghiep = objBenhnhan.TenNghenghiep;
                objEmrBa.MaDantoc = objBenhnhan.DanToc;
                objEmrBa.TenDantoc = objBenhnhan.TenDantoc; 
                objEmrBa.MaTongiao = objBenhnhan.TonGiao;
                objEmrBa.TenTongiao = objBenhnhan.TonGiao; 
                objEmrBa.MaQuocgia = objBenhnhan.MaQuocgia;// Utility.Int16Dbnull(objBenhnhan.MaQuocgia != "" && objBenhnhan.MaQuocgia != "VN" ? 1 : 0);
                objEmrBa.TenQuocgia = objBenhnhan.TenQuocgia;
                objEmrBa.NgoaiKieu = (Utility.sDbnull(objBenhnhan.MaQuocgia) == "VN" ? 0 : 1) == 1;
               
                objEmrBa.DiachiLienhe = objBenhnhan.DiachiLienhe;
                objEmrBa.DienthoaiLienhe = objBenhnhan.DienthoaiLienhe;
                objEmrBa.NguoiLienhe = objBenhnhan.NguoiLienhe;
                objEmrBa.CmtNguoilienhe = objBenhnhan.CmtNguoilienhe;
                objEmrBa.DiaChi = objLuotkham.DiaChi;
                objEmrBa.MaTinhtp = objLuotkham.MaTinhtp;
                objEmrBa.TenTinhtp = objBenhnhan.TenTinhtp;
                objEmrBa.MaQuanhuyen = objLuotkham.MaQuanhuyen;
                objEmrBa.TenQuanhuyen = objBenhnhan.TenQuanhuyen;
                objEmrBa.MaXaphuong = objLuotkham.MaXaphuong;
                objEmrBa.TenXaphuong = objBenhnhan.TenXaphuong; 
                objEmrBa.MaCoquan = objBenhnhan.CoQuan;
                objEmrBa.TenCoquan = objBenhnhan.CoQuan;
                objEmrBa.MatheBhyt = objBenhnhan.MatheBhyt;
                objEmrBa.MaDoituong =Utility.ByteDbnull( objLuotkham.IdDoituongKcb);
                objEmrBa.TenDoituong = objBenhnhan.TenDoituongKcb;

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
                objEmrBa.DienThoai = objBenhnhan.DienThoai;
                objEmrBa.Email = objLuotkham.Email;

              
                   
                    
                    objEmrBa.NoigioithieuCoquanyte = chkQLNBCoQuanYTe.Checked;
                    objEmrBa.NoigioithieuTuden = chkQLNBTuDen.Checked;
                    objEmrBa.NoigioithieuKhac = chkQLNBKhac.Checked;
                   
                //Check lại
                objEmrBa.VaovienMakhoa = "KKB";
                objEmrBa.VaovienTenkhoa = "KHOA KHÁM CHỮA BỆNH";
                objEmrBa.VaovienNgayvaokhoa = null;
                
                
              //Chẩn đoán
              

               
                objEmrBa.VaovienLydovaovien = Utility.sDbnull(txtBenhAnLyDoNhapVien.Text);

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
               
                
                objEmrBa.TongketbaQuatrinhbenhlyDienbienlamsang = Utility.sDbnull(txtTKBAQuaTrinhBenhLy.Text);
                objEmrBa.TongketbaTomtatKqcls = Utility.sDbnull(txtTKBATTomTatKetQua.Text);
                objEmrBa.TongketbaPhuongphapdieutri = Utility.sDbnull(txtTKBAPhuongPhapDieuTri.Text);
                objEmrBa.TongketbaTinhtrangNguoiravien = Utility.sDbnull(txtTKBATinhTrangRaVien.Text);
                objEmrBa.TongketbaHuongdieutritieptheo = Utility.sDbnull(txtTKBAHuongDieuTri.Text);

                objEmrBa.TongketbaMaNguoigiaohoso = txtNguoiGiaoHoSo.Text;
                objEmrBa.TongketbaMaNguoiNhanhoso = txtNguoiNhanHoSo.Text;
                objEmrBa.MabacsiLamBA = txtBSlamBA.MyCode;
                objEmrBa.IdBacsiLamBA =Utility.Int16Dbnull( txtBSlamBA.MyID);
                objEmrBa.TenbacsiLamBA = txtBSlamBA.Text;
                objEmrBa.TenbacsiDieutri = txtBSDieuTri.Text;
                objEmrBa.IdBacsiDieutri = Utility.Int16Dbnull(txtBSDieuTri.MyID);
                objEmrBa.MabacsiDieutri = txtBSDieuTri.MyCode;
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
            if (e.Control && e.KeyCode == Keys.S) cmdSave.PerformClick();
            //if (e.KeyCode == Keys.F4) cmdInBenhAn.PerformClick();
            if (e.KeyCode == Keys.Escape) Close();
            if ((e.Alt || e.Control) && e.KeyCode == Keys.NumPad1)
            {
                uiTab1.SelectedIndex = 0;
                ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Focus();
            }
            else if ((e.Alt || e.Control) && e.KeyCode == Keys.NumPad1)
            {
                uiTab1.SelectedIndex = 1;
            }
            else if ((e.Alt || e.Control) && e.KeyCode == Keys.NumPad1)
            {
                uiTab1.SelectedIndex = 2;
            }
            else if ((e.Alt || e.Control) && e.KeyCode == Keys.NumPad1)
            {
                uiTab1.SelectedIndex = 3;
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
                DataTable dtData =
                    new Select().From(DmucChung.Schema)
                        .Where(DmucChung.Columns.Loai).IsEqualTo("EMR_LOAIBA")
                        .And(DmucChung.Columns.TrangThai).IsEqualTo(1)
                        .And(DmucChung.Columns.Ma).In(lstLoaiBA.Split(',').ToList<string>())
                        .OrderAsc(DmucChung.Columns.SttHthi)
                        .ExecuteDataSet().Tables[0];
                DataBinding.BindDataCombobox(cboLoaiBA, dtData, "MA", "TEN", "---Chọn loại BA---", true);
                txtBenhAnLyDoNhapVien.Init();
                if (m_enAct == action.Insert)
                {
                    ucThongtinnguoibenh_emr_basic1.Refresh();
                }
                else
                {
                    objLuotkham = Utility.getKcbLuotkham(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham);
                    objBenhnhan = Utility.getKcbBenhnhan(objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham);
                    FillData4Update();

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

            
               
               
                    dtpNgayTiepdon.Value = objLuotkham.NgayTiepdon;
               
              
              
                FillTongketBenhAn();
              
                
                if (objEmrBa != null)
                {
                    m_enAct = action.Update;
                    cboLoaiBA.SelectedIndex = Utility.GetSelectedIndex(cboLoaiBA, objEmrBa.LoaiBa);
                    maBA = objEmrBa.MaBa;
                    dtDataBA = SPs.EmrBaLaythongtin(-1, "", objBenhnhan.IdBenhnhan, objLuotkham.MaLuotkham).GetDataSet().Tables[0];
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
                      
                        chkQLNBCoQuanYTe.Checked = Utility.Bool2Bool(objEmrBa.NoigioithieuCoquanyte);
                        chkQLNBCoQuanYTe.Checked = Utility.Bool2Bool(objEmrBa.NoigioithieuTuden);
                        chkQLNBCoQuanYTe.Checked = Utility.Bool2Bool(objEmrBa.NoigioithieuKhac);
                       
                        string ICD_chinh_Name = "";
                        string ICD_chinh_Code = "";
                        string ICD_Phu_Name = "";
                        string ICD_Phu_Code = "";

                        GetChanDoanChinhPhu(objLuotkham.MabenhChinh,
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

                        txtNguoiGiaoHoSo.Text = Utility.sDbnull(objEmrBa.TongketbaNguoigiaoHoso);
                        txtNguoiNhanHoSo.Text = Utility.sDbnull(objEmrBa.TongketbaNguoiNhanhoso);
                        txtBSDieuTri.SetCode( Utility.sDbnull(objEmrBa.MabacsiDieutri));
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
                 
                  
                    FillPhieuKCB();
                   
                    KcbThongtinchung tef = new Select().From(KcbThongtinchung.Schema)
                        .Where(KcbThongtinchung.Columns.IdBenhnhan).IsEqualTo(objBenhnhan.IdBenhnhan)
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
        }
       
        VKcbLuotkham objBenhnhan = null;
        KcbLuotkham objLuotkham = null;
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
            btnInto2.Enabled = btnInto3.Enabled = Into1.Enabled = btnInto4.Enabled = button1.Enabled = btnInVoBA.Enabled = objLuotkham != null && objEmrBa!=null;
            cmdXoaBenhAn.Enabled = objLuotkham != null && objEmrBa != null;
        }

        private void txtMaLanKham_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    objLuotkham.MaLuotkham = THU_VIEN_CHUNG.SinhMaHoSoKhiTimKiem(objLuotkham.MaLuotkham);
            //    if (!KiemTraThongTin()) return;
            //    FillBenhAnByPatientCode();
            //}
        }
       
        private DataTable getChitietCLS()
        {
            int status = 0;
            DataTable temdt = SPs.ClsKetQuaXetNghiem(-1,"",objLuotkham.MaLuotkham, objBenhnhan.IdBenhnhan, 1, status).GetDataSet().Tables[0];

            return temdt;
        }

        private void cmdXoaBenhAn_Click(object sender, EventArgs e)
        {
            try
            {
               
                objEmrBa = EmrBa.FetchByID(Utility.Int64Dbnull( txtIDBenhAn.Text));
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
                if (Utility.Int32Dbnull( hosoba.TrangThai,0) == 1)
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
                              Utility.Log("frm_BenhAn_NgoaiTru", globalVariables.UserName, string.Format("Xóa bệnh án id={0}, loại BA={1}, mã BA={2} của người bệnh id ={3}, mã lần khám {4} thành công",objEmrBa.IdBa,objEmrBa.LoaiBa,objEmrBa.MaBa,objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham), newaction.Delete, "UI");
                            }
                            Scope.Complete();
                        }
                       
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

        private void Into1_Click(object sender, EventArgs e)
        {

            DataTable sub_dtData = new KCB_THAMKHAM().NoitruTimkiemlichsuBuonggiuong(objLuotkham.MaLuotkham, objBenhnhan.IdBenhnhan, "-1",-1);
            string reportCode = "BA_NOITRU_TO1";
            THU_VIEN_CHUNG.CreateXML(dtDataBA, reportCode + ".XML");
            THU_VIEN_CHUNG.CreateXML(sub_dtData,  "BA_noitru_khoachuyen.XML");
            noitru_inphieu.BA_noitru_Into1(dtDataBA, sub_dtData, true, reportCode, "");
        }

        private void btnInto2_Click(object sender, EventArgs e)
        {
            string reportCode = "BA_NOITRU_TO2";
            THU_VIEN_CHUNG.CreateXML(dtDataBA, reportCode + ".XML");
            noitru_inphieu.BA_noitru_Into234_voba_tkba(dtDataBA, true, reportCode, "");
        }

        private void btnInto3_Click(object sender, EventArgs e)
        {
            string reportCode = "BA_NOITRU_TO3";
            THU_VIEN_CHUNG.CreateXML(dtDataBA, reportCode + ".XML");
            noitru_inphieu.BA_noitru_Into234_voba_tkba(dtDataBA, true, reportCode, "");
        }

        private void btnInto4_Click(object sender, EventArgs e)
        {
            string reportCode = "BA_NOITRU_TO4";
            THU_VIEN_CHUNG.CreateXML(dtDataBA, reportCode + ".XML");
            noitru_inphieu.BA_noitru_Into234_voba_tkba(dtDataBA, true, reportCode, "");
        }

        private void btnInVoBA_Click(object sender, EventArgs e)
        {
            string reportCode = "BA_NOITRU_VOBA";
            THU_VIEN_CHUNG.CreateXML(dtDataBA, reportCode + ".XML");
            noitru_inphieu.BA_noitru_Into234_voba_tkba(dtDataBA, true, reportCode, "");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string reportCode = "BA_noitru_tongketBA";
            THU_VIEN_CHUNG.CreateXML(dtDataBA, reportCode + ".XML");
            noitru_inphieu.BA_noitru_Into234_voba_tkba(dtDataBA, true, reportCode, "");
        }

        private void cmdUpdateBNToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cmdLamMoi_Click(object sender, EventArgs e)
        {
            ClearControl();
            objEmrBa = null;
            objBenhnhan = null;
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
            InBA(0);
        }

        private void mnuInTomtatBA_Click(object sender, EventArgs e)
        {
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Chưa có thông tin người bệnh để thực hiện thao tác in tóm tắt bệnh án");
                return;
            }
            EmrTongketBenhan objTKBA =new Select().From(EmrTongketBenhan.Schema)
                .Where(EmrTongketBenhan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                .And(EmrTongketBenhan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                .ExecuteSingle<EmrTongketBenhan>();
            if (objTKBA == null || objTKBA.Id <= 0)
            {
                Utility.ShowMsg("Bạn cần tạo Tóm tắt hồ sơ bệnh án trước khi thực hiện in");
                return;
            }
            clsInBA.InTomTatBA(objTKBA);
        }

        private void mnuInTo1_Click(object sender, EventArgs e)
        {
            InBA(1);
        }

        private void mnuInTo2_Click(object sender, EventArgs e)
        {
            InBA(2);
        }

        private void mnuInTo3_Click(object sender, EventArgs e)
        {
            InBA(3);
        }

        private void mnuInTo4_Click(object sender, EventArgs e)
        {
            InBA(4);
        }
        private void InBA(int toBA)
        {
            try
            {
                if (objLuotkham == null)
                {
                    Utility.ShowMsg("Chưa có thông tin người bệnh để thực hiện thao tác in tóm tắt bệnh án");
                    return;
                }
               
                if (objEmrBa == null || objEmrBa.IdBa <= 0)
                {
                    Utility.ShowMsg("Bạn cần tạo Bệnh án Ngoại trú trước khi thực hiện in");
                    return;
                }
                DataTable dtData = SPs.EmrBaLaythongtinIn(objEmrBa.IdBa, objEmrBa.MaBa, objEmrBa.IdBenhnhan, objEmrBa.MaLuotkham).GetDataSet().Tables[0];
                DataRow drData = dtData.Rows[0];
                List<string> lstcheckboxfields = new List<string>();
                Dictionary<string, string> dicMF = new Dictionary<string, string>();
                foreach (string chkField in lstcheckboxfields)
                {
                    dicMF.Add(chkField, Utility.Byte2Bool(drData[chkField]) ? "0" : "1");
                }
                string checkboxFieldsFile = AppDomain.CurrentDomain.BaseDirectory + "MAUBA\\BA_CHECKED_FIELDS.txt";
                lstcheckboxfields = Utility.GetFirstValueFromFile(checkboxFieldsFile).Split(',').ToList<string>();
                NoitruPhieuravien objPhieuRavien = new Select().From(NoitruPhieuravien.Schema)
               .Where(NoitruPhieuravien.Columns.IdBenhnhan).IsEqualTo(objBenhnhan.IdBenhnhan)
               .And(NoitruPhieuravien.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<NoitruPhieuravien>();
                NoitruPhieunhapvien _phieunv = new Select().From(NoitruPhieunhapvien.Schema)
               .Where(NoitruPhieunhapvien.Columns.IdBenhnhan).IsEqualTo(objBenhnhan.IdBenhnhan)
               .And(NoitruPhieunhapvien.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<NoitruPhieunhapvien>();
                dtData.TableName = "BA_NOITRU";
                Document doc;
                drData["ten_dvicaptren"] = globalVariables.ParentBranch_Name;
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["p102"] = globalVariables.Branch_Name;
                drData["p101"] = globalVariables.ParentBranch_Name;
                drData["p132"] = _phieunv!=null?Utility.FormatDateTime_giophut_ngay_thang_nam(_phieunv.NgayNhapvien, ""):".......... giờ ....... ngày ........./........./.............";//Vào viện

                //REM lại do đã xử lý ở bước fillData trước khi ghi
                //drData["p103"] = drData["p140"];
                //if (dtkhoanhapvienCoGiuong.Rows.Count > 0 && THU_VIEN_CHUNG.Laygiatrithamsohethong("BA_LAYKHOANOITRU_COGIUONG", "0", false) == "1")
                //{
                //    drData["p103"] = Utility.sDbnull(dtkhoanhapvienCoGiuong.Rows[0]["ten_khoanoitru"], "");
                //    drData["p104"] = Utility.sDbnull(dtkhoanhapvienCoGiuong.Rows[0]["ten_giuong"], "");
                //}
                drData["p128"] = Utility.FormatDateTime(Utility.sDbnull(drData["p128"], ""), "ngày......tháng......năm.........");//BHYT giá trị đến
                drData["p145_1"] =objPhieuRavien!=null? Utility.FormatDateTime_giophut_ngay_thang_nam(objPhieuRavien.NgayRavien, ""):".......... giờ ....... ngày ........./........./.............";//ra viện
               

                List<string> fieldNames = new List<string>();
                string tenToBA = "";
                if (toBA == 1) tenToBA = "BA01_BANOIKHOA_TO1.doc";
                else if (toBA == 0) tenToBA = "BA01_BANOIKHOA_BIA.doc";
                else if (toBA == 2) tenToBA = "BA01_BANOIKHOA_TO2.doc";
                else if (toBA == 3) tenToBA = "BA01_BANOIKHOA_TO3.doc";
                else if (toBA == 4) tenToBA = "BA01_BANOIKHOA_TO4.doc";
                else tenToBA = "BA01_BANOIKHOA.doc";
                string PathDoc = string.Format(AppDomain.CurrentDomain.BaseDirectory + "MAUBA\\{0}", tenToBA);
                string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg(string.Format("Không tìm thấy File {0}", PathDoc), "Thông báo không tìm thấy File",
                      MessageBoxIcon.Warning);
                    return;
                }
                SysSystemParameter sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("logosize").ExecuteSingle<SysSystemParameter>();

                string fileKetqua = string.Format("{0}{1}{2}{3}{4}_{5}_{6}_{7}",
                               Path.GetDirectoryName(writePathdoc), Path.DirectorySeparatorChar,
                               Path.GetFileNameWithoutExtension(PathDoc), "EmrBa", objLuotkham.MaLuotkham, Utility.sDbnull(objEmrBa.IdBa), Guid.NewGuid().ToString(), Path.GetExtension(PathDoc));


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
                    Utility.MergeFieldsCheckBox2Doc(builder, null, lstcheckboxfields, drData);
                    


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
        private void mnuInBA_Click(object sender, EventArgs e)
        {
            InBA(100);
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
        EmrTongketBenhan objTKBA;
        void FillTongketBenhAn()
        {
            try
            {
                objTKBA=  new Select().From(EmrTongketBenhan.Schema)
                    .Where(EmrTongketBenhan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                    .And(EmrTongketBenhan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                    .ExecuteSingle<EmrTongketBenhan>();
                if (objTKBA != null)
                {
                    dtpNgayTKBA.Value = objTKBA.NgayTtba.Value;
                    txtTKBAQuaTrinhBenhLy.Text = objTKBA.QuatrinhbenhlyDienbienlamsang;
                    txtTKBATTomTatKetQua.Text = objTKBA.TomtatKqcls;
                    txtTKBAPhuongPhapDieuTri.Text = objTKBA.PhuongphapDieutri;
                    txtTKBATinhTrangRaVien.Text = objTKBA.TinhtrangRavienMota;
                    txtTKBAHuongDieuTri.Text = objTKBA.HuongDieutri;

                    txtNguoiGiaoHoSo.Text = Utility.sDbnull(objTKBA.NguoigiaoHoso);
                    txtNguoiNhanHoSo.Text = Utility.sDbnull(objTKBA.NguoiNhanhoso);

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
            cmdSave.PerformClick();
        }

        private void cmdLaythongtinKCB_Click(object sender, EventArgs e)
        {

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

    }
}
