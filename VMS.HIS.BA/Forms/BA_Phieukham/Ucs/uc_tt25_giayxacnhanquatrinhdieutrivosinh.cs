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
    public partial class uc_tt25_giayxacnhanquatrinhdieutrivosinh : UserControl
    {
        public delegate void OnMsg(string msg, bool IsSucess = false);
        public event OnMsg _OnMsg;
        public delegate void OnStatus(bool isNew);
        public event OnStatus _OnStatus;
        public Tt25Giayxacnhanquatrinhdieutrivosinh giayxacnhan;
        KcbLuotkham objLuotkham;
        public int id_bacsikham = -1;
        DmucNhanvien objNguoiXacnhan = null;
        DmucNhanvien objNguoiDaidien = null;
        public uc_tt25_giayxacnhanquatrinhdieutrivosinh()
        {
            InitializeComponent();
            txtNguoiXacnhan._OnEnterMe += TxtBSDieuTri__OnEnterMe;

            txtDaidienDonvi._OnEnterMe += TxtDaidienDonvi__OnEnterMe;
        }
        public bool VisibleFunctionButtons
        {
            get { return cmdGhi.Visible; }
            set { cmdGhi.Visible =cmdInphieu.Visible= value; }
        }
        public void Init(KcbLuotkham objLuotkham, Tt25Giayxacnhanquatrinhdieutrivosinh giayxacnhan)
        {
            dtpNgayxacnhan.Value = globalVariables.SysDate;
            this.objLuotkham = objLuotkham;
            this.giayxacnhan = giayxacnhan;
            txtNguoiXacnhan.Init(globalVariables.gv_dtDmucNhanvien,
                                            new List<string>
                                 {
                                      DmucNhanvien.Columns.IdNhanvien,
                                      DmucNhanvien.Columns.MaNhanvien,
                                      DmucNhanvien.Columns.TenNhanvien
                                 });
          

        }

        private void TxtDaidienDonvi__OnEnterMe()
        {
            objNguoiDaidien = DmucNhanvien.FetchByID(Utility.Int32Dbnull(txtDaidienDonvi.MyID));
        }

        private void TxtBSDieuTri__OnEnterMe()
        {
            objNguoiXacnhan = DmucNhanvien.FetchByID(Utility.Int32Dbnull( txtNguoiXacnhan.MyID));
        }

        public void Init(KcbLuotkham objLuotkham)
        {
            dtpNgayxacnhan.Value = globalVariables.SysDate;
            this.objLuotkham = objLuotkham;
            giayxacnhan = new Select().From(Tt25Giayxacnhanquatrinhdieutrivosinh.Schema)
                        .Where(Tt25Giayxacnhanquatrinhdieutrivosinh.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        .And(Tt25Giayxacnhanquatrinhdieutrivosinh.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .ExecuteSingle<Tt25Giayxacnhanquatrinhdieutrivosinh>();
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
            txtNguoiXacnhan.Init(globalVariables.gv_dtDmucNhanvien,
                                           new List<string>
                                {
                                      DmucNhanvien.Columns.IdNhanvien,
                                      DmucNhanvien.Columns.MaNhanvien,
                                      DmucNhanvien.Columns.TenNhanvien
                                });
            txtDaidienDonvi.Init(txtNguoiXacnhan.AutoCompleteSource, txtNguoiXacnhan.defaultItem);
            VMS.HIS.Danhmuc.Util.SetNguoiDaiDienDonVi(txtDaidienDonvi);
            dtpNgayvaovien.Value = dtpNgayravien.Value = globalVariables.SysDate;
           
        }
        public void DisplayData()
        {
            try
            {
               
                if (giayxacnhan == null)
                    giayxacnhan = new Select().From(Tt25Giayxacnhanquatrinhdieutrivosinh.Schema)
                        .Where(Tt25Giayxacnhanquatrinhdieutrivosinh.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        .And(Tt25Giayxacnhanquatrinhdieutrivosinh.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .ExecuteSingle<Tt25Giayxacnhanquatrinhdieutrivosinh>();
               
                txtId.Text = "";
                if (giayxacnhan != null)
                {
                    txtId.Text = giayxacnhan.Id.ToString();

                    dtpNgayxacnhan.Value = giayxacnhan.Ngayxacnhan;

                    if (giayxacnhan.NgayVaovien.HasValue)
                        dtpNgayvaovien.Value = giayxacnhan.NgayVaovien.Value;
                    else
                        dtpNgayvaovien.Value = globalVariables.SysDate;
                    if (giayxacnhan.NgayRavien.HasValue)
                        dtpNgayravien.Value = giayxacnhan.NgayRavien.Value;
                    else
                        dtpNgayravien.Value = globalVariables.SysDate;
                    chkDaravien.Checked = Utility.Bool2Bool(giayxacnhan.Davarien);
                    chkDangdieutrivosinh.Checked = Utility.Bool2Bool(giayxacnhan.Hiendangdieutrivosinh);
                    txt_hiendangdieutrivosinhtai.Text = Utility.sDbnull(giayxacnhan.Hiendangdieutrivosinhtai);
                    txt_chandoan.Text = Utility.sDbnull(giayxacnhan.Chandoan);
                    txt_phuongphapdieutri.Text = Utility.sDbnull(giayxacnhan.Phuongphapdieutri);
                    txt_ghichu.Text = Utility.sDbnull(giayxacnhan.Ghichu);

                    txtNguoiXacnhan.SetId(giayxacnhan.IdBacsy);
                    txtNguoiXacnhan.RaiseEnterEvents();
                    txtDaidienDonvi.SetId(giayxacnhan.IdNguoidaidien);
                    txtDaidienDonvi.RaiseEnterEvents();
                }
                else
                {
                    ClearControl();
                    if (objLuotkham != null)
                    {
                        dtpNgayvaovien.Value = objLuotkham.NgayTiepdon;
                        if (Utility.Byte2Bool(objLuotkham.Noitru))
                        {

                            if (objLuotkham.NgayRavien.HasValue)
                                dtpNgayravien.Value = objLuotkham.NgayRavien.Value;
                            else
                                dtpNgayravien.Value = globalVariables.SysDate;
                        }
                        else
                        {

                            if (objLuotkham.NgayKetthuc.HasValue)
                                dtpNgayravien.Value = objLuotkham.NgayKetthuc.Value;
                            else
                                dtpNgayravien.Value = globalVariables.SysDate;
                        }
                    }
                }
                txtSoHoso.Text = giayxacnhan == null || string.IsNullOrEmpty(Utility.sDbnull(giayxacnhan.SoHoso, "")) ? THU_VIEN_CHUNG.TT25LaySohoso(5) : Utility.sDbnull(giayxacnhan.SoHoso, "");
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
            if (chkDaravien.Checked && dtpNgayravien.Value<= dtpNgayvaovien.Value)
            {
                Msg = "Ngày ra viện phải > ngày vào viện. Vui lòng kiểm tra lại";
                if (_OnMsg != null) _OnMsg(Msg);
                dtpNgayravien.Focus();
                return false;
            }
            if (Utility.sDbnull(txtSoHoso.Text)=="")
            {
                Msg = "Bạn phải nhập số hồ sơ";
                if (_OnMsg != null) _OnMsg(Msg);
                txtSoHoso.Focus();
                return false;
            }
            DataTable dtData = new Select().From(Tt25Giayxacnhanquatrinhdieutrivosinh.Schema)
              .Where(Tt25Giayxacnhanquatrinhdieutrivosinh.Columns.SoHoso).IsEqualTo(Utility.DoTrim(txtSoHoso.Text))
              .And(Tt25Giayxacnhanquatrinhdieutrivosinh.Columns.Id).IsNotEqualTo(Utility.Int64Dbnull(txtId.Text, -1))
              .ExecuteDataSet().Tables[0];
            if (dtData.Rows.Count > 0)
            {
                Msg = "Số hồ sơ đã được sử dụng. Vui lòng nhập số phiếu khác";
                txtSoHoso.Focus();
                return false;
            }
            if (txtNguoiXacnhan.MyID=="-1")
            {
                Msg = "Bạn phải chọn người xác nhận làm phiếu này";
                if (_OnMsg != null) _OnMsg(Msg);
                txtNguoiXacnhan.SelectAll();
                txtNguoiXacnhan.Focus();
                return false;
            }
            if (txtDaidienDonvi.MyID == "-1")
            {
                Msg = "Bạn phải chọn người Đại diện đơn vị";
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
                DateTime? dtp=null;
                Msg = "";
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        giayxacnhan = new Select().From(Tt25Giayxacnhanquatrinhdieutrivosinh.Schema)
                   .Where(Tt25Giayxacnhanquatrinhdieutrivosinh.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                   .And(Tt25Giayxacnhanquatrinhdieutrivosinh.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                   .ExecuteSingle<Tt25Giayxacnhanquatrinhdieutrivosinh>();
                       
                        if (giayxacnhan == null || giayxacnhan.Id <= 0)
                        {
                            isNew = true;
                            giayxacnhan = new Tt25Giayxacnhanquatrinhdieutrivosinh();
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
                        giayxacnhan.NgayVaovien = dtpNgayvaovien.Value;
                        giayxacnhan.NgayRavien = chkDaravien.Checked ? dtpNgayravien.Value : dtp;
                        giayxacnhan.Ngayxacnhan = dtpNgayxacnhan.Value;
                        giayxacnhan.Hiendangdieutrivosinh = chkDangdieutrivosinh.Checked;
                        giayxacnhan.Davarien = chkDaravien.Checked;
                        giayxacnhan.Hiendangdieutrivosinhtai = chkDangdieutrivosinh.Checked? Utility.sDbnull(txt_hiendangdieutrivosinhtai.Text, ""):"";
                        giayxacnhan.Chandoan = Utility.sDbnull(txt_chandoan.Text, "");
                        giayxacnhan.Phuongphapdieutri = Utility.sDbnull(txt_phuongphapdieutri.Text, "");
                        giayxacnhan.Ghichu = Utility.sDbnull(txt_ghichu.Text, "");
                        if (objNguoiXacnhan != null)
                        {
                            giayxacnhan.IdBacsy = objNguoiXacnhan.IdNhanvien;
                            giayxacnhan.MaBacsy = objNguoiXacnhan.MaNhanvien;
                            giayxacnhan.UserBacsy = objNguoiXacnhan.UserName;
                        }
                        if (objNguoiDaidien != null)
                        {
                            giayxacnhan.IdNguoidaidien = objNguoiDaidien.IdNhanvien;
                            giayxacnhan.MaNguoidaidien = objNguoiDaidien.MaNhanvien;
                            giayxacnhan.UserNguoidaidien = objNguoiDaidien.UserName;
                        }
                        giayxacnhan.Save();
                        emrdoc.InitDocument(giayxacnhan.IdBenhnhan, giayxacnhan.MaLuotkham, Utility.Int64Dbnull(giayxacnhan.Id), giayxacnhan.Ngayxacnhan, Loaiphieu_HIS.TT25_GIAYXACNHAN_QUATRINHDIEUTRIVOSINH, "TT25_GIAYXACNHAN_QUATRINHDIEUTRIVOSINH", giayxacnhan.NguoiTao,Utility.Int16Dbnull( objNguoiXacnhan.IdKhoa), Utility.Int16Dbnull(objNguoiXacnhan.IdPhong), Utility.Byte2Bool(0),"");
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
                giayxacnhan = new Select().From(Tt25Giayxacnhanquatrinhdieutrivosinh.Schema)
                       .Where(Tt25Giayxacnhanquatrinhdieutrivosinh.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                       .And(Tt25Giayxacnhanquatrinhdieutrivosinh.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                       .ExecuteSingle<Tt25Giayxacnhanquatrinhdieutrivosinh>();
                if (giayxacnhan.Id <= 0)
                {
                    Utility.ShowMsg("Bạn cần lưu thông tin Giấy chứng nhận tai nạn thương tích trước khi thực hiện in phiếu");
                    cmdGhi.Focus();
                    return;
                }
                DataTable dtData = SPs.Tt25GiayxacnhanquatrinhdieutrivosinhLaythongtinIn(giayxacnhan.Id).GetDataSet().Tables[0];
                dtData.TableName = "TT25_GIAYXACNHAN_QUATRINHDIEUTRIVOSINH";
                dtData.Rows[0]["sngaygio_nhapvien"] = giayxacnhan != null ? Utility.FormatDateTime_giophut_ngay_thang_nam(giayxacnhan.NgayVaovien, "") : ".......... giờ ....... ngày ........./........./.............";
                dtData.Rows[0]["sngaygio_ravien"] = giayxacnhan != null ? Utility.FormatDateTime_giophut_ngay_thang_nam(giayxacnhan.NgayRavien, "") : ".......... giờ ....... ngày ........./........./.............";
                dtData.Rows[0]["sngayxacnhan"] = Utility.FormatDateTime(Utility.sDbnull(dtData.Rows[0]["sngayxacnhan"], ""), "ngày......tháng......năm.........");
                WordPrinter.InPhieu(dtData, "TT25_GIAYXACNHAN_QUATRINHDIEUTRIVOSINH.doc", "");


            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void chkDangdieutrivosinh_CheckedChanged(object sender, EventArgs e)
        {
            txt_hiendangdieutrivosinhtai.Enabled = chkDangdieutrivosinh.Checked;
        }

        private void chkDaravien_CheckedChanged(object sender, EventArgs e)
        {
            dtpNgayravien.Enabled = chkDaravien.Checked;
        }

        private void cmdTuSinh_Click(object sender, EventArgs e)
        {
            txtSoHoso.Text = THU_VIEN_CHUNG.TT25LaySohoso(5);
        }
    }
}
