using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using SubSonic;
using VMS.HIS.DAL;
using VNS.Libs;
using VNS.HIS.UI.DANHMUC;
using VNS.HIS.UCs;
using Janus.Windows.GridEX.EditControls;
using VMS.HIS.Danhmuc.Dungchung;
using System;
using System.Transactions;
using VMS.HIS.Bus;
using VMS.HIS.Bus.Emr;

namespace VMS.HIS.UI.EMR.Ucs
{
    public partial class uc_tt25_giayxacnhan_nguoimekhongdusuckhoe_chamsoccon : UserControl
    {
        public delegate void OnMsg(string msg,bool IsSucess=false);
        public event OnMsg _OnMsg;
        public delegate void OnStatus(bool isNew);
        public event OnStatus _OnStatus;
        public Tt25GiayxacnhanNguoimekhongdusuckhoeChamsoccon giayxacnhan;
        KcbLuotkham objLuotkham;
        public int id_bacsikham = -1;
        DmucNhanvien objNguoiDaidien = null;
        public uc_tt25_giayxacnhan_nguoimekhongdusuckhoe_chamsoccon()
        {
            InitializeComponent();
            txtDaidienDonvi._OnEnterMe += TxtBSDieuTri__OnEnterMe;
        }
               public void Init(KcbLuotkham objLuotkham, Tt25GiayxacnhanNguoimekhongdusuckhoeChamsoccon giayxacnhan)
        {
            dtpNgayxacnhan.Value = globalVariables.SysDate;
            this.objLuotkham = objLuotkham;
            this.giayxacnhan = giayxacnhan;
            txtDaidienDonvi.Init(globalVariables.gv_dtDmucNhanvien,
                                            new List<string>
                                 {
                                      DmucNhanvien.Columns.IdNhanvien,
                                      DmucNhanvien.Columns.MaNhanvien,
                                      DmucNhanvien.Columns.TenNhanvien
                                 });
          

        }

        private void TxtBSDieuTri__OnEnterMe()
        {
            objNguoiDaidien = DmucNhanvien.FetchByID(Utility.Int32Dbnull( txtDaidienDonvi.MyID));
        }

        public void Init(KcbLuotkham objLuotkham)
        {
            dtpNgayxacnhan.Value = globalVariables.SysDate;
            this.objLuotkham = objLuotkham;
            giayxacnhan = new Select().From(Tt25GiayxacnhanNguoimekhongdusuckhoeChamsoccon.Schema)
                        .Where(Tt25GiayxacnhanNguoimekhongdusuckhoeChamsoccon.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        .And(Tt25GiayxacnhanNguoimekhongdusuckhoeChamsoccon.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .ExecuteSingle<Tt25GiayxacnhanNguoimekhongdusuckhoeChamsoccon>();
            DisplayData();

        }
        public void HandleKeyEnter()
        {
            Control activeCtrl = Utility.getActiveControl(this);
            if (activeCtrl == null) return;
            if (activeCtrl.GetType().Equals(typeof(EditBox)))
            {
                EditBox box = activeCtrl as EditBox;
                if (box.Multiline)
                {
                    return;
                }
                else
                    SendKeys.Send("{TAB}");
            }
            else if (activeCtrl.GetType().Equals(typeof(TextBox)))
            {
                TextBox box = activeCtrl as TextBox;
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
        public void Init()
        {
            dtpNgayxacnhan.Value = globalVariables.SysDate;
            txtDaidienDonvi.Init(globalVariables.gv_dtDmucNhanvien,
                                           new List<string>
                                {
                                      DmucNhanvien.Columns.IdNhanvien,
                                      DmucNhanvien.Columns.MaNhanvien,
                                      DmucNhanvien.Columns.TenNhanvien
                                });
          
            VMS.HIS.Danhmuc.Util.SetNguoiDaiDienDonVi(txtDaidienDonvi);
            dtpNgayvaovien.Value = globalVariables.SysDate;
           
        }
        public void DisplayData()
        {
            try
            {
              
                if (giayxacnhan == null)
                    giayxacnhan = new Select().From(Tt25GiayxacnhanNguoimekhongdusuckhoeChamsoccon.Schema)
                        .Where(Tt25GiayxacnhanNguoimekhongdusuckhoeChamsoccon.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        .And(Tt25GiayxacnhanNguoimekhongdusuckhoeChamsoccon.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .ExecuteSingle<Tt25GiayxacnhanNguoimekhongdusuckhoeChamsoccon>();
               
                txtId.Text = "";
                if (giayxacnhan != null)
                {
                    txtId.Text = giayxacnhan.Id.ToString();

                    dtpNgayxacnhan.Value = giayxacnhan.Ngayxacnhan;
                   
                   
                        dtpNgayvaovien.Value = giayxacnhan.Ngayvaovien;
                  
                    txt_chandoan.Text = Utility.sDbnull(giayxacnhan.Chandoan);
                    txt_phuongphapdieutri.Text = Utility.sDbnull(giayxacnhan.Phuongphapdieutri);
                    txt_tinhtrangbenh.Text = Utility.sDbnull(giayxacnhan.Tinhtrangbenh);
                    txt_ketluan.Text = Utility.sDbnull(giayxacnhan.Ketluan);
                    txtDaidienDonvi.SetId(giayxacnhan.IdNguoidaidien);
                    txtDaidienDonvi.RaiseEnterEvents();
                }
                else
                    ClearControl();
                txtSoHoso.Text = giayxacnhan == null || string.IsNullOrEmpty(Utility.sDbnull(giayxacnhan.SoHoso, "")) ? THU_VIEN_CHUNG.TT25LaySohoso(3) : Utility.sDbnull(giayxacnhan.SoHoso, "");
                if (_OnStatus != null) _OnStatus(giayxacnhan == null || giayxacnhan.Id <= 0);
            }
            catch (System.Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
       void ClearControl()
        {
            foreach (Control ctr in this.Controls)
                if (ctr.GetType().Equals(autoTxt.GetType()))
                    ((AutoCompleteTextbox_Danhmucchung)ctr).SetDefaultItem();
                else if (ctr is EditBox)
                {
                    ((EditBox)(ctr)).Clear();
                }
                else if (ctr is CheckBox)
                {
                    ((CheckBox)(ctr)).Checked=false;
                }
                else if (ctr is DateTimePicker)
                {
                    ((DateTimePicker)(ctr)).Value = globalVariables.SysDate;
                }
        }
        string Msg = "";
        bool isValidData()
        {
            Msg = "";
            if (Utility.sDbnull(txtSoHoso.Text) == "")
            {
                Msg = "Bạn phải nhập số hồ sơ";
                if (_OnMsg != null) _OnMsg(Msg);
                txtSoHoso.Focus();
                return false;
            }
            DataTable dtData = new Select().From(Tt25GiayxacnhanNguoimekhongdusuckhoeChamsoccon.Schema)
               .Where(Tt25GiayxacnhanNguoimekhongdusuckhoeChamsoccon.Columns.SoHoso).IsEqualTo(Utility.DoTrim(txtSoHoso.Text))
               .And(Tt25GiayxacnhanNguoimekhongdusuckhoeChamsoccon.Columns.Id).IsNotEqualTo(Utility.Int64Dbnull(txtId.Text, -1))
               .ExecuteDataSet().Tables[0];
            if (dtData.Rows.Count > 0)
            {
                Msg = "Số hồ sơ đã được sử dụng. Vui lòng nhập số phiếu khác";
                txtSoHoso.Focus();
                return false;
            }
            if (txtDaidienDonvi.MyID=="-1")
            {
                Msg = "Bạn phải chọn người đại diện đơn vị";
                if (_OnMsg != null) _OnMsg(Msg);
                txtDaidienDonvi.SelectAll();
                txtDaidienDonvi.Focus();
                return false;
            }    
            return true;
        }
        EmrDocuments emrdoc = new EmrDocuments();
        public bool Save()
        {
            try
            {
               bool isNew = true;
                if (!isValidData()) return false;
                Msg = "";
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        giayxacnhan = new Select().From(Tt25GiayxacnhanNguoimekhongdusuckhoeChamsoccon.Schema)
                   .Where(Tt25GiayxacnhanNguoimekhongdusuckhoeChamsoccon.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                   .And(Tt25GiayxacnhanNguoimekhongdusuckhoeChamsoccon.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                   .ExecuteSingle<Tt25GiayxacnhanNguoimekhongdusuckhoeChamsoccon>();
                        
                        if (giayxacnhan == null || giayxacnhan.Id <= 0)
                        {
                            isNew = true;
                            giayxacnhan = new Tt25GiayxacnhanNguoimekhongdusuckhoeChamsoccon();
                            giayxacnhan.IsNew = true;
                            giayxacnhan.NgayTao = DateTime.Now;
                            giayxacnhan.NguoiTao = globalVariables.UserName;
                        }
                        else
                        {
                            isNew = false;
                            giayxacnhan.IsNew = false;
                            giayxacnhan.MarkOld();
                            giayxacnhan.NgaySua = DateTime.Now;
                            giayxacnhan.NguoiSua = globalVariables.UserName;
                        }
                        giayxacnhan.SoHoso = Utility.sDbnull(txtSoHoso.Text);
                        giayxacnhan.IdBenhnhan = objLuotkham.IdBenhnhan;
                        giayxacnhan.MaLuotkham = objLuotkham.MaLuotkham;
                        giayxacnhan.Ngayvaovien = dtpNgayvaovien.Value;
                        giayxacnhan.Ngayxacnhan = dtpNgayxacnhan.Value;

                        giayxacnhan.Chandoan = Utility.sDbnull(txt_chandoan.Text, "");
                        giayxacnhan.Phuongphapdieutri = Utility.sDbnull(txt_phuongphapdieutri.Text, "");
                        giayxacnhan.Ketluan = Utility.sDbnull(txt_ketluan.Text, "");
                        giayxacnhan.Tinhtrangbenh = Utility.sDbnull(txt_tinhtrangbenh.Text, "");
                        if (objNguoiDaidien != null)
                        {
                            giayxacnhan.IdNguoidaidien = objNguoiDaidien.IdNhanvien;
                            giayxacnhan.MaNguoidaidien = objNguoiDaidien.MaNhanvien;
                            giayxacnhan.UserNguoidaidien = objNguoiDaidien.UserName;
                        }
                        giayxacnhan.Save();
                        emrdoc.InitDocument(giayxacnhan.IdBenhnhan, giayxacnhan.MaLuotkham, Utility.Int64Dbnull(giayxacnhan.Id), giayxacnhan.Ngayxacnhan, Loaiphieu_HIS.TT25_GIAYXACNHAN_NGUOIMEKHONGDUSUCKHOE_CHAMSOCCON, "TT25_GIAYXACNHAN_NGUOIMEKHONGDUSUCKHOE_CHAMSOCCON", giayxacnhan.NguoiTao,Utility.Int16Dbnull( objNguoiDaidien.IdKhoa), Utility.Int16Dbnull(objNguoiDaidien.IdPhong), Utility.Byte2Bool(0),"");
                        emrdoc.Save();
                    }
                    scope.Complete();
                }
                txtId.Text = giayxacnhan.Id.ToString();
                if (_OnStatus != null) _OnStatus(isNew);
                Msg = "Lưu thông tin thành công";
                if (_OnMsg != null) _OnMsg(Msg,true);
                return true;
            }
            catch (System.Exception ex)
            {
                if (_OnMsg != null) _OnMsg(ex.Message);
                Utility.CatchException(ex);
                return false;
            }
        }

        private void cmdGhi_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void cmdInphieu_Click(object sender, EventArgs e)
        {
            Print();
        }
        public  void Print()
        {
            try
            {
                giayxacnhan = new Select().From(Tt25GiayxacnhanNguoimekhongdusuckhoeChamsoccon.Schema)
                       .Where(Tt25GiayxacnhanNguoimekhongdusuckhoeChamsoccon.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                       .And(Tt25GiayxacnhanNguoimekhongdusuckhoeChamsoccon.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                       .ExecuteSingle<Tt25GiayxacnhanNguoimekhongdusuckhoeChamsoccon>();
                if (giayxacnhan.Id <= 0)
                {
                    Utility.ShowMsg("Bạn cần lưu thông tin Giấy xác nhận người mẹ không đủ điều kiện chăm sóc con trước khi thực hiện in phiếu");
                  
                    return;
                }
                DataTable dtData = SPs.Tt25GiayxacnhanNguoimekhongdusuckhoeChamsocconLaythongtinIn(giayxacnhan.Id).GetDataSet().Tables[0];
                dtData.TableName = "TT25_GIAYXACNHAN_NGUOIMEKHONGDUSUCKHOE_CHAMSOCCON";
                dtData.Rows[0]["sngaygio_nhapvien"] = giayxacnhan != null ? Utility.FormatDateTime_giophut_ngay_thang_nam(giayxacnhan.Ngayvaovien, "") : ".......... giờ ....... ngày ........./........./.............";
               
                dtData.Rows[0]["sngayxacnhan"] = Utility.FormatDateTime(Utility.sDbnull(dtData.Rows[0]["sngayxacnhan"], ""), "ngày......tháng......năm.........");
                WordPrinter.InPhieu(dtData, "TT25_GIAYXACNHAN_NGUOIMEKHONGDUSUCKHOE_CHAMSOCCON.doc", "");


            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void cmdTuSinh_Click(object sender, EventArgs e)
        {
            txtSoHoso.Text = THU_VIEN_CHUNG.TT25LaySohoso(3);
        }
    }
}
