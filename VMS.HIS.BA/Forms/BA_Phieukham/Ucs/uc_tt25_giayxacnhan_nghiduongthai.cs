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
    public partial class uc_tt25_giayxacnhan_nghiduongthai : UserControl
    {
        public delegate void OnMsg(string msg, bool IsSucess = false);
        public event OnMsg _OnMsg;
        public delegate void OnStatus(bool isNew);
        public event OnStatus _OnStatus;
        public Tt25GiayxacnhanNghiduongthai giayxacnhan;
        KcbLuotkham objLuotkham;
        public int id_bacsikham = -1;
        DmucNhanvien objNguoiXacnhan = null;
        DmucNhanvien objNguoiDaidien = null;
        bool isAllowDateChanged = false;
        public bool Force2Saved = false;
        public uc_tt25_giayxacnhan_nghiduongthai()
        {
            InitializeComponent();
            txtNguoiXacnhan._OnEnterMe += TxtBSDieuTri__OnEnterMe;

            txtDaidienDonvi._OnEnterMe += TxtDaidienDonvi__OnEnterMe;
        }
        
        public void Init(KcbLuotkham objLuotkham, Tt25GiayxacnhanNghiduongthai giayxacnhan)
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
            giayxacnhan = new Select().From(Tt25GiayxacnhanNghiduongthai.Schema)
                        .Where(Tt25GiayxacnhanNghiduongthai.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        .And(Tt25GiayxacnhanNghiduongthai.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .ExecuteSingle<Tt25GiayxacnhanNghiduongthai>();
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
            dtpNgayxacnhan.Value =dtpNgaynghiTu.Value=dtpNgaynghiDen.Value= globalVariables.SysDate;
            txtNguoiXacnhan.Init(globalVariables.gv_dtDmucNhanvien,
                                           new List<string>
                                {
                                      DmucNhanvien.Columns.IdNhanvien,
                                      DmucNhanvien.Columns.MaNhanvien,
                                      DmucNhanvien.Columns.TenNhanvien
                                });
            txtDaidienDonvi.Init(txtNguoiXacnhan.AutoCompleteSource, txtNguoiXacnhan.defaultItem);
            VMS.HIS.Danhmuc.Util.SetNguoiDaiDienDonVi(txtDaidienDonvi);
            
        }
        public void DisplayData()
        {
            try
            {
                isAllowDateChanged = false;
               
                if (giayxacnhan == null)
                    giayxacnhan = new Select().From(Tt25GiayxacnhanNghiduongthai.Schema)
                        .Where(Tt25GiayxacnhanNghiduongthai.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        .And(Tt25GiayxacnhanNghiduongthai.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .ExecuteSingle<Tt25GiayxacnhanNghiduongthai>();
               
                txtId.Text = "";
                if (giayxacnhan != null)
                {
                    txtId.Text = giayxacnhan.Id.ToString();
                    if (giayxacnhan.Ngayxacnhan.HasValue)
                        dtpNgayxacnhan.Value = giayxacnhan.Ngayxacnhan.Value;
                    else
                        dtpNgayxacnhan.Value = globalVariables.SysDate;
                    if (giayxacnhan.Tungay.HasValue)
                        dtpNgaynghiTu.Value = giayxacnhan.Tungay.Value;
                    else
                        dtpNgaynghiTu.Value = globalVariables.SysDate;
                    if (giayxacnhan.Denngay.HasValue)
                        dtpNgaynghiDen.Value = giayxacnhan.Denngay.Value;
                    else
                        dtpNgaynghiDen.Value = globalVariables.SysDate;
                    nmrSotuantuoithai.Text = Utility.sDbnull(giayxacnhan.Tuantuoithai,"0");
                    txt_chandoan.Text = Utility.sDbnull(giayxacnhan.Chandoan);
                    nmrSongaynghiduongthai.Text = Utility.sDbnull(giayxacnhan.Songaynghiduongthai, "0");
                  
                    txtNguoiXacnhan.SetId(giayxacnhan.IdBacsy);
                    txtNguoiXacnhan.RaiseEnterEvents();
                    txtDaidienDonvi.SetId(giayxacnhan.IdNguoidaidien);
                    txtDaidienDonvi.RaiseEnterEvents();
                }
                else
                    ClearControl();
                txtSoHoso.Text = giayxacnhan == null || string.IsNullOrEmpty(Utility.sDbnull(giayxacnhan.SoHoso, "")) ? THU_VIEN_CHUNG.TT25LaySohoso(2) : Utility.sDbnull(giayxacnhan.SoHoso, "");
                if (_OnStatus != null) _OnStatus(giayxacnhan == null || giayxacnhan.Id <= 0);
            }
            catch (System.Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                isAllowDateChanged = true;
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
            if (dtpNgaynghiDen.Value< dtpNgaynghiTu.Value)
            {
                Msg = "Ngày kết thúc phải >= ngày bắt đầu. Vui lòng kiểm tra lại";
                if (_OnMsg != null) _OnMsg(Msg,false);
                dtpNgaynghiDen.Focus();
                return false;
            }
            if (Utility.sDbnull(txtSoHoso.Text) == "")
            {
                Msg = "Bạn phải nhập số hồ sơ";
                if (_OnMsg != null) _OnMsg(Msg,false);
                txtSoHoso.Focus();
                return false;
            }
            DataTable dtData = new Select().From(Tt25GiayxacnhanNghiduongthai.Schema)
               .Where(Tt25GiayxacnhanNghiduongthai.Columns.SoHoso).IsEqualTo(Utility.DoTrim(txtSoHoso.Text))
               .And(Tt25GiayxacnhanNghiduongthai.Columns.Id).IsNotEqualTo(Utility.Int64Dbnull(txtId.Text, -1))
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
                if (_OnMsg != null) _OnMsg(Msg,false);
                txtNguoiXacnhan.SelectAll();
                txtNguoiXacnhan.Focus();
                return false;
            }
            if (txtDaidienDonvi.MyID == "-1")
            {
                Msg = "Bạn phải chọn người Đại diện đơn vị";
                if (_OnMsg != null) _OnMsg(Msg,false);
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
                        giayxacnhan = new Select().From(Tt25GiayxacnhanNghiduongthai.Schema)
                   .Where(Tt25GiayxacnhanNghiduongthai.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                   .And(Tt25GiayxacnhanNghiduongthai.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                   .ExecuteSingle<Tt25GiayxacnhanNghiduongthai>();
                      
                        if (giayxacnhan == null || giayxacnhan.Id <= 0)
                        {
                            isNew = true;
                            giayxacnhan = new Tt25GiayxacnhanNghiduongthai();
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
                        giayxacnhan.Songaynghiduongthai = Utility.ByteDbnull(nmrSongaynghiduongthai.Value);
                        giayxacnhan.Tuantuoithai = Utility.ByteDbnull(nmrSotuantuoithai.Value);
                        giayxacnhan.Tungay = dtpNgaynghiTu.Value;
                        giayxacnhan.Denngay = dtpNgaynghiDen.Value;
                        giayxacnhan.Ngayxacnhan = dtpNgayxacnhan.Value;
                        giayxacnhan.Chandoan = Utility.sDbnull(txt_chandoan.Text, "");
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
                        emrdoc.Force2Saved = Force2Saved;
                        emrdoc.InitDocument(giayxacnhan.IdBenhnhan, giayxacnhan.MaLuotkham, Utility.Int64Dbnull(giayxacnhan.Id), giayxacnhan.Ngayxacnhan.Value, Loaiphieu_HIS.TT25_GIAYXACNHAN_NGHIDUONGTHAI, "TT25_GIAYXACNHAN_NGHIDUONGTHAI", giayxacnhan.NguoiTao,Utility.Int16Dbnull( objNguoiXacnhan.IdKhoa), Utility.Int16Dbnull(objNguoiXacnhan.IdPhong), Utility.Byte2Bool(0),"");
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
                if (_OnMsg != null) _OnMsg(ex.Message,false);
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
                giayxacnhan = new Select().From(Tt25GiayxacnhanNghiduongthai.Schema)
                       .Where(Tt25GiayxacnhanNghiduongthai.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                       .And(Tt25GiayxacnhanNghiduongthai.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                       .ExecuteSingle<Tt25GiayxacnhanNghiduongthai>();
                if (giayxacnhan.Id <= 0)
                {
                    Utility.ShowMsg("Bạn cần lưu thông tin Giấy xác nhận nghỉ dưỡng thai trước khi thực hiện in phiếu");
                    return;
                }
                DataTable dtData = SPs.Tt25GiayxacnhanNghiduongthaiLaythongtinIn(giayxacnhan.Id).GetDataSet().Tables[0];
                dtData.TableName = "TT25_GIAYXACNHAN_NGHIDUONGTHAI";
                dtData.Rows[0]["sngayxacnhan"] = Utility.FormatDateTime(Utility.sDbnull(dtData.Rows[0]["sngayxacnhan"], ""), "ngày......tháng......năm.........");
                WordPrinter.InPhieu(dtData, "TT25_GIAYXACNHAN_NGHIDUONGTHAI.doc", "");


            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void nmrSongaynghiduongthai_ValueChanged(object sender, EventArgs e)
        {
            if (!isAllowDateChanged) return;
            isAllowDateChanged = false;
            dtpNgaynghiDen.Value = dtpNgaynghiTu.Value.AddDays(Utility.Int32Dbnull(nmrSongaynghiduongthai.Value));
            isAllowDateChanged = true;
        }

        private void dtpNgaynghiTu_ValueChanged(object sender, EventArgs e)
        {
            if (!isAllowDateChanged) return;
            isAllowDateChanged = false;
            dtpNgaynghiDen.Value = dtpNgaynghiTu.Value.AddDays(Utility.Int32Dbnull(nmrSongaynghiduongthai.Value));
            isAllowDateChanged = true;
        }

        private void dtpNgaynghiDen_ValueChanged(object sender, EventArgs e)
        {
            if (!isAllowDateChanged) return;
            isAllowDateChanged = false;
            dtpNgaynghiTu.Value = dtpNgaynghiDen.Value.AddDays(-1*Utility.Int32Dbnull(nmrSongaynghiduongthai.Value));
            isAllowDateChanged = true;
        }
    }
}
