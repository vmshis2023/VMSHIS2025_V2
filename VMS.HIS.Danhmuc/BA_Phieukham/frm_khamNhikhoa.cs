using System;
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
using System.Transactions;

namespace VMS.EMR.PHIEUKHAM
{
    public partial class frm_khamNhikhoa : Form
    {
        public KcbLuotkham objLuotkham;
        KcbDanhsachBenhnhan objBenhnhan;
        bool AllowedChanged = false;
        action m_enAct = action.FirstOrFinished;
        public frm_khamNhikhoa(KcbLuotkham objLuotkham, KcbDanhsachBenhnhan objBenhnhan)
        {
            InitializeComponent();
            this.KeyDown += frm_khamNhikhoa_KeyDown;
            this.objLuotkham = objLuotkham;
            this.objBenhnhan = objBenhnhan;
            //objNguoibenh = new Select().From(VKcbLuotkham.Schema).Where(VKcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).And(VKcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<VKcbLuotkham>();
            Utility.SetVisualStyle(this);
            ucThongtinnguoibenh1._OnEnterMe += ucThongtinnguoibenh1__OnEnterMe;
            ucThongtinnguoibenh1.txtMaluotkham.Text = objLuotkham.MaLuotkham;
            cmdxoa.Click += cmdxoa_Click;
            cmdIn.Click += cmdIn_Click;
            cmdGhi.Click += cmdGhi_Click;
            txtNhommau._OnShowDataV1 += __OnShowDataV1;
            txtCanNang.TextChanged += txtCanNang_TextChanged;
            txtChieuCao.TextChanged += txtChieuCao_TextChanged;
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
       
        void __OnShowDataV1(AutoCompleteTextbox_Danhmucchung obj)
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

        void cmdGhi_Click(object sender, EventArgs e)
        {
            if (!isValidData()) return;
            SaveData();
        }

       bool isValidData()
        {
            if(Utility.sDbnull( cboBacsi.SelectedValue,"-1")=="1")
            {
                Utility.ShowMsg("Bạn phải chọn bác sĩ khám");
                cboBacsi.Focus();
            }    
            return true;
        }
        void cmdIn_Click(object sender, EventArgs e)
        {
            
        }


        void cmdxoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (objLuotkham.TrangthaiNgoaitru == 1 || objLuotkham.NgayKetthuc != null || (Utility.Byte2Bool(objLuotkham.TthaiThopNoitru) && objLuotkham.TrangthaiNoitru == 6))
                {
                    Utility.ShowMsg("Bệnh nhân đã kết thúc khám nên bạn không thể thực hiện chức năng này");
                    ucThongtinnguoibenh1.txtMaluotkham.Focus();
                    ucThongtinnguoibenh1.txtMaluotkham.SelectAll();
                    return;
                }
                if (globalVariables.IsAdmin || objPKNhikhoa.NguoiTao == globalVariables.UserName.ToString())
                {
                    objPKNhikhoa = EmrPhieukhamNhikhoa.FetchByID(objPKNhikhoa.Id);
                    if (objPKNhikhoa != null)
                    {
                        if (Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa thông tin khám chữa bệnh ngày {0} của bác sĩ {1} thực hiện", "Cảnh báo", true))
                        {
                            EmrPhieukhamNhikhoa.Delete(objPKNhikhoa.Id);
                        }
                    }
                    else
                    {
                        Utility.ShowMsg(string.Format("Không thể xóa phiếu khám Nhi khoa.\nVui lòng kiểm tra lại vì có thể trong lúc bạn mở thao tác người khác đã xóa thông tin", objPKNhikhoa.NguoiTao));
                    }    
                }
                else
                {
                    Utility.ShowMsg(string.Format("Bạn không thể xóa thông tin khám được tạo bởi bác sĩ {0}.\nVui lòng kiểm tra lại", objPKNhikhoa.NguoiTao));
                }
            }
            catch (Exception)
            {


            }
            finally
            {
                ModifyCommmands();
            }
            
        }


        void ClearControls()
        {


            foreach (Control ctr in pnlInfor.Controls)
                if (ctr.GetType().Equals(autoTxt.GetType()))
                    ((AutoCompleteTextbox_Danhmucchung)ctr).SetDefaultItem();
                else if (ctr is EditBox)
                {
                    ((EditBox)(ctr)).Clear();
                }
            foreach (Control ctr in grpChucNangSong.Controls)
                if (ctr.GetType().Equals(autoTxt.GetType()))
                    ((AutoCompleteTextbox_Danhmucchung)ctr).SetDefaultItem();
                else if (ctr is EditBox)
                {
                    ((EditBox)(ctr)).Clear();
                }

        }
     
        void ModifyCommmands()
        {
            txt_KhamToanThan.Enabled = txt_KhamBoPhan.Enabled = objPKB == null || (objPKB != null && objPKB.NguoiTao == globalVariables.UserName);
            cmdxoa.Enabled = cmdIn.Enabled = objPKNhikhoa != null;
        }
        void frm_khamNhikhoa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Control activeCtrl = Utility.getActiveControl(this);
                if ((activeCtrl != null && (activeCtrl.Name == autoTxt.Name || activeCtrl.Name == autoTxt.Name )))
                    return;
                else
                {
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
                    else if (activeCtrl.Name == txtNhommau.Name)
                    {
                        //uiTabInfor.SelectedIndex = 1;
                        //txtCT.Focus();
                    }
                    else
                        SendKeys.Send("{TAB}");
                }


            }
            else if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            else if (e.Control && e.KeyCode == Keys.D) cmdxoa.PerformClick();
            else if (e.Control && e.KeyCode == Keys.P) cmdIn.PerformClick();
        }
        EmrPhieukhambenh objPKB;
        void ucThongtinnguoibenh1__OnEnterMe()
        {
            if (ucThongtinnguoibenh1.objLuotkham != null)
            {
                objLuotkham = ucThongtinnguoibenh1.objLuotkham;
                this.Text = string.Format("Phiếu khám nhi khoa cho người bệnh {0} - {1} - {2} -{3}", ucThongtinnguoibenh1.txtTenBN.Text, ucThongtinnguoibenh1.txtgioitinh.Text, ucThongtinnguoibenh1.txttuoi.Text, ucThongtinnguoibenh1.txtDiachi.Text);
                objPKB = new Select().From(EmrPhieukhambenh.Schema)
                   .Where(EmrPhieukhambenh.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                   .And(EmrPhieukhambenh.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                   .ExecuteSingle<EmrPhieukhambenh>();
                objPKNhikhoa= new Select().From(EmrPhieukhamNhikhoa.Schema)
                   .Where(EmrPhieukhamNhikhoa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                   .And(EmrPhieukhamNhikhoa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                   .ExecuteSingle<EmrPhieukhamNhikhoa>();
                FillData();
            }
            else
            {
                ClearControls();
                this.Text = "phiếu khám Nhi khoa";
            }    
        }
       
       
        private void frm_khamNhikhoa_Load(object sender, EventArgs e)
        {
            InitDanhmucchung();
            DataBinding.BindDataCombobox(cboBacsi, globalVariables.gv_dtDmucNhanvien.Copy(),
                                     DmucNhanvien.Columns.UserName, DmucNhanvien.Columns.TenNhanvien, "----Chọn bác sĩ khám----", true);

          
            ucThongtinnguoibenh1.Refresh();
            dtNgayKham.Value = DateTime.Now.Date;
            dtNgayKham.Focus();
            ModifyCommmands();
           
        }
        
        EmrPhieukhamNhikhoa objPKNhikhoa = null;
        private void FillData()
        {
            try
            {
                if (objPKNhikhoa != null)
                {

                    txtID.Text = objPKNhikhoa.Id.ToString();
                    txt_khamchieucao.Text = Utility.sDbnull(objPKNhikhoa.KhamChieucao);
                    txtvongdau.Text =Utility.sDbnull( objPKNhikhoa.Vongdau);
                    txtvongnguc.Text = Utility.sDbnull(objPKNhikhoa.Vongnguc);
                    
                    txtNhietDo.Text = objPKNhikhoa.NhietDo;
                    txtha.Text = objPKNhikhoa.NhomMau;
                    txtMach.Text = objPKNhikhoa.Mach;
                    txtNhipTho.Text = objPKNhikhoa.NhịpTho;
                    txtChieuCao.Text = objPKNhikhoa.ChieuCao;
                    txtCanNang.Text = objPKNhikhoa.CanNang;
                    txtBMI.Text = objPKNhikhoa.Bmi;
                    txtNhommau.SetCode(objPKNhikhoa.NhomMau);
                    //Tiền sử sản phụ khoa
                    nmrConthu.Text = Utility.sDbnull(objPKNhikhoa.NhikhoaConthu);
                    chkDethuong.Checked = Utility.Bool2Bool(objPKNhikhoa.TinhtrangkhisinhDethuong);
                    chkForceps.Checked = Utility.Bool2Bool(objPKNhikhoa.TinhtrangkhisinhForceps);
                    chkGiachut.Checked = Utility.Bool2Bool(objPKNhikhoa.TinhtrangkhisinhGiachut);
                    chkDephauthuat.Checked = Utility.Bool2Bool(objPKNhikhoa.TinhtrangkhisinhDephauthuat);
                    chkDechihuy.Checked = Utility.Bool2Bool(objPKNhikhoa.TinhtrangkhisinhDechihuy);
                    chkDekhac.Checked = Utility.Bool2Bool(objPKNhikhoa.TinhtrangkhisinhDekhac);
                    txtCannanglucsinh.Text = Utility.sDbnull(objPKNhikhoa.CannangLucsinh);

                    chkDitatbamsinh.Checked = Utility.Bool2Bool(objPKNhikhoa.DitatBamsinh);
                    txt_cuthetatbamsinh.Text = Utility.sDbnull(objPKNhikhoa.DitatBamsinhMota);
                    txt_phattrienvetinhthan.Text = Utility.sDbnull(objPKNhikhoa.PhattrienVetinhthan);
                    txt_phattrienvevandong.Text = Utility.sDbnull(objPKNhikhoa.PhattrienVevandong);
                    txt_cacbenhlykhac.Text = Utility.sDbnull(objPKNhikhoa.Cacbenhlykhac);

                    chkSuame.Checked = Utility.Bool2Bool(objPKNhikhoa.NuoiduongSuame);
                    chkNuoinhantao.Checked = Utility.Bool2Bool(objPKNhikhoa.NuoiduongNhantao);
                    chkNuoihonhop.Checked = Utility.Bool2Bool(objPKNhikhoa.NuoiduongHonhop);
                    nmrCaisuathangthu.Text = Utility.sDbnull(objPKNhikhoa.NuoidungCaisuathangthu);
                    chkTaivuontre.Checked = Utility.Bool2Bool(objPKNhikhoa.ChamsocTaivuontre);
                    chkTainha.Checked = Utility.Bool2Bool(objPKNhikhoa.ChamsocTainha);

                    chkLao.Checked = Utility.Bool2Bool(objPKNhikhoa.DatiemchungLao);
                    chkBailiet.Checked = Utility.Bool2Bool(objPKNhikhoa.DatiemchungBailiet);
                    chkSoi.Checked = Utility.Bool2Bool(objPKNhikhoa.DatiemchungSoi);
                    chkHoga.Checked = Utility.Bool2Bool(objPKNhikhoa.DatiemchungHoga);
                    chkUonvan.Checked = Utility.Bool2Bool(objPKNhikhoa.DatiemchungUonvan);
                    chkTiemchungkhac.Checked = Utility.Bool2Bool(objPKNhikhoa.DatiemchungKhac);

                    txt_cuthenhungbenhkhacduoctiemchung.Text = Utility.sDbnull(objPKNhikhoa.DatiemchungKhacMota);
                    cboBacsi.SelectedValue = Utility.sDbnull(objPKNhikhoa.IdBacsi, "-1");
                    //dtNgayKham.Value = Convert.ToDateTime(string.IsNullOrEmpty(objPKNhikhoa.NgayKham) ? dtNgayKham.Value : objPKNhikhoa.NgayKham);
                    dtNgayKham.Value = string.IsNullOrEmpty(objPKNhikhoa.NgayKham.ToString()) ? dtNgayKham.Value : Convert.ToDateTime(objPKNhikhoa.NgayKham);

                   
                }
                else
                {
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
                if(objPKB!=null)
                {
                    txt_KhamToanThan.Text = objPKB.ToanThan;
                    txt_KhamBoPhan.Text = objPKB.BoPhan;
                    
                }    
            }
            catch (Exception)
            {


            }
            finally
            {
                ModifyCommmands();
            }
           
           
        }
        private void InitDanhmucchung()
        {
           DataTable dtData= THU_VIEN_CHUNG.LayDulieuDanhmucChung(new List<string>() { txtNhommau.LOAI_DANHMUC},true);
            txtNhommau.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txtNhommau.LOAI_DANHMUC));
        }
        bool IsValidChucnangsong()
        {
            try
            {
                if (objLuotkham == null )
                {
                    Utility.ShowMsg("Bạn cần chọn một người bệnh trên danh sách phía bên trái màn hình để bắt đầu thực hiện khám");
                    return false;
                }
                if (Utility.Laygiatrithamsohethong("CANHBAO_CHUCNANGSONG", "0", true) == "1")
                {
                    decimal value2Conpare = Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(txtMach.Text), -1);
                    List<string> lstRange = Utility.Laygiatrithamsohethong("MACH", "5-70", true).Split('-').ToList<string>();
                    if (Utility.DoTrim(txtMach.Text).Length > 0 && value2Conpare < Utility.DecimaltoDbnull(lstRange[0]) || value2Conpare > Utility.DecimaltoDbnull(lstRange[1]))
                    {
                        Utility.ShowMsg(string.Format("Thông tin Mạch có thể chưa chuẩn xác. Hệ thống đang xác lập mức cho phép từ {0}-{1}. Ngoài mức này sẽ cảnh báo nhưng vẫn cho lưu. Vui lòng kiểm tra lại", lstRange[0], lstRange[1]), "Cảnh báo");
                        txtMach.Focus();
                    }
                    value2Conpare = Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(txtNhietDo.Text), -1);
                    lstRange = Utility.Laygiatrithamsohethong("NHIETDO", "34-43", true).Split('-').ToList<string>();
                    if (Utility.DoTrim(txtNhietDo.Text).Length > 0 && value2Conpare < Utility.DecimaltoDbnull(lstRange[0]) || value2Conpare > Utility.DecimaltoDbnull(lstRange[1]))
                    {
                        Utility.ShowMsg(string.Format("Thông tin Nhiệt độ có thể chưa chuẩn xác. Hệ thống đang xác lập mức cho phép từ {0}-{1}. Ngoài mức này sẽ cảnh báo nhưng vẫn cho lưu. Vui lòng kiểm tra lại", lstRange[0], lstRange[1]), "Cảnh báo");
                        txtNhietDo.Focus();
                    }
                    value2Conpare = Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(txtha.Text), -1);
                    lstRange = Utility.Laygiatrithamsohethong("HUYETAP", "40-250", true).Split('-').ToList<string>();
                    if (Utility.DoTrim(txtha.Text).Length > 0 && value2Conpare < Utility.DecimaltoDbnull(lstRange[0]) || value2Conpare > Utility.DecimaltoDbnull(lstRange[1]))
                    {
                        Utility.ShowMsg(string.Format("Thông tin Huyết áp có thể chưa chuẩn xác. Hệ thống đang xác lập mức cho phép từ {0}-{1}. Ngoài mức này sẽ cảnh báo nhưng vẫn cho lưu. Vui lòng kiểm tra lại", lstRange[0], lstRange[1]), "Cảnh báo");
                        txtha.Focus();
                    }
                    value2Conpare = Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(txtNhipTho.Text), -1);
                    lstRange = Utility.Laygiatrithamsohethong("NHIPTHO", "40-250", true).Split('-').ToList<string>();
                    if (Utility.DoTrim(txtNhipTho.Text).Length > 0 && value2Conpare < Utility.DecimaltoDbnull(lstRange[0]) || value2Conpare > Utility.DecimaltoDbnull(lstRange[1]))
                    {
                        Utility.ShowMsg(string.Format("Thông tin Nhịp thở có thể chưa chuẩn xác. Hệ thống đang xác lập mức cho phép từ {0}-{1}. Ngoài mức này sẽ cảnh báo nhưng vẫn cho lưu. Vui lòng kiểm tra lại", lstRange[0], lstRange[1]), "Cảnh báo");
                        txtNhipTho.Focus();
                    }
                    value2Conpare = Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(txtChieuCao.Text), -1);
                    lstRange = Utility.Laygiatrithamsohethong("CHIEUCAO", "10-250", true).Split('-').ToList<string>();
                    if (Utility.DoTrim(txtChieuCao.Text).Length > 0 && value2Conpare < Utility.DecimaltoDbnull(lstRange[0]) || value2Conpare > Utility.DecimaltoDbnull(lstRange[1]))
                    {
                        Utility.ShowMsg(string.Format("Thông tin Chiều cao có thể chưa chuẩn xác. Hệ thống đang xác lập mức cho phép chiều cao từ {0}(cm)-{1}(cm). Ngoài mức này sẽ cảnh báo nhưng vẫn cho lưu. Vui lòng kiểm tra lại", lstRange[0], lstRange[1]), "Cảnh báo");
                        txtChieuCao.Focus();
                    }
                    value2Conpare = Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(txtCanNang.Text), -1);
                    lstRange = Utility.Laygiatrithamsohethong("CANNANG", "1-150", true).Split('-').ToList<string>();
                    if (Utility.DoTrim(txtCanNang.Text).Length > 0 && value2Conpare < Utility.DecimaltoDbnull(lstRange[0]) || value2Conpare > Utility.DecimaltoDbnull(lstRange[1]))
                    {
                        Utility.ShowMsg(string.Format("Thông tin Cân nặng có thể chưa chuẩn xác. Hệ thống đang xác lập mức cân nặng từ {0}(kg)-{1}(kg). Ngoài mức này sẽ cảnh báo nhưng vẫn cho lưu. Vui lòng kiểm tra lại", lstRange[0], lstRange[1]), "Cảnh báo");
                        txtCanNang.Focus();
                    }
                    //value2Conpare = Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(txtMach.Text), -1);
                    //lstRange = Utility.Laygiatrithamsohethong("NHIPTIM", "40-130", true).Split('-').ToList<string>();
                    //if (Utility.DoTrim(txtMach.Text).Length > 0 && value2Conpare < Utility.DecimaltoDbnull(lstRange[0]) || value2Conpare > Utility.DecimaltoDbnull(lstRange[1]))
                    //{
                    //    Utility.ShowMsg(string.Format("Thông tin Nhịp tim có thể chưa chuẩn xác. Hệ thống đang xác lập mức cho phép từ {0}(kg)-{1}(kg). Ngoài mức này sẽ cảnh báo nhưng vẫn cho lưu. Vui lòng kiểm tra lại", lstRange[0], lstRange[1]), "Cảnh báo");
                    //    txtMach.Focus();
                    //}
                    if (Utility.DoTrim(txtNhommau.Text).Length > 0 && txtNhommau.MyCode == "-1")
                    {
                        Utility.ShowMsg(string.Format("Sai thông tin nhóm máu. Yêu cầu nhập lại hoặc xóa trắng nếu không muốn nhập"), "Cảnh báo");
                        txtNhommau.Focus();
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return true;
            }
        }
        private void txtCanNang_Leave(object sender, EventArgs e)
        {
            if (Utility.DecimaltoDbnull(txtChieuCao.Text, 0) > 0 && Utility.DecimaltoDbnull(txtChieuCao.Text, 0) > 0)
            {
                if (!string.IsNullOrEmpty(txtCanNang.Text) && !string.IsNullOrEmpty(txtChieuCao.Text))
                {
                    decimal cannang = Utility.DecimaltoDbnull(txtCanNang.Text);
                    decimal chieucao = Utility.DecimaltoDbnull(txtChieuCao.Text);
                    decimal bmi = Utility.DecimaltoDbnull(cannang / ((chieucao / 100) * (chieucao / 100)));
                    txtBMI.Text = bmi.ToString("0.00").Replace(".00", String.Empty);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        void SaveData()
        {
            try
            {
                long ActID = -1;
                //Lấy lại dữ liệu lần nữa đề phòng có người khác dùng chính tài khoản tạo ra các thông tin này và thực hiện xóa trên máy khác
                objPKB = new Select().From(EmrPhieukhambenh.Schema)
                   .Where(EmrPhieukhambenh.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                   .And(EmrPhieukhambenh.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                   .ExecuteSingle<EmrPhieukhambenh>();
                objPKNhikhoa = new Select().From(EmrPhieukhamNhikhoa.Schema)
                   .Where(EmrPhieukhamNhikhoa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                   .And(EmrPhieukhamNhikhoa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                   .ExecuteSingle<EmrPhieukhamNhikhoa>();
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
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
                            objPKB.NgayKham = dtNgayKham.Value.Date;
                            objPKB.NguoiTao = globalVariables.UserName;
                            objPKB.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
                        }
                        objPKB.BoPhan = Utility.sDbnull(txt_KhamBoPhan.Text);
                        objPKB.ToanThan = Utility.sDbnull(txt_KhamToanThan.Text);
                        if (objPKNhikhoa != null && objPKNhikhoa.Id > 0)
                        {
                            objPKNhikhoa.MarkOld();
                            objPKNhikhoa.NguoiSua = globalVariables.UserName;
                            objPKNhikhoa.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
                        }
                        else
                        {
                            objPKNhikhoa = new EmrPhieukhamNhikhoa();
                            objPKNhikhoa.IsNew = true;
                            objPKNhikhoa.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham);
                            objPKNhikhoa.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan);
                            objPKNhikhoa.NgayKham = dtNgayKham.Value.Date;
                            objPKNhikhoa.NguoiTao = globalVariables.UserName;
                            objPKNhikhoa.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
                        }
                        ActID = objPKNhikhoa.Id;
                        //Hỏi bệnh
                        objPKNhikhoa.IdBacsi = Utility.Int16Dbnull(cboBacsi.SelectedValue);
                        objPKNhikhoa.KhamChieucao = Utility.ByteDbnull(txtChieuCao.Text);
                        objPKNhikhoa.Vongdau = Utility.ByteDbnull(txtvongdau.Text);
                        objPKNhikhoa.Vongnguc = Utility.ByteDbnull(txtvongnguc.Text);
                        //Tiền sử sản phụ khoa
                        objPKNhikhoa.NhikhoaConthu = Utility.ByteDbnull(nmrConthu.Value);
                        objPKNhikhoa.TinhtrangkhisinhDethuong = chkDethuong.Checked;
                        objPKNhikhoa.TinhtrangkhisinhForceps = chkForceps.Checked;
                        objPKNhikhoa.TinhtrangkhisinhGiachut = chkGiachut.Checked;
                        objPKNhikhoa.TinhtrangkhisinhDephauthuat = chkDephauthuat.Checked;
                        objPKNhikhoa.TinhtrangkhisinhDechihuy = chkDechihuy.Checked;
                        objPKNhikhoa.TinhtrangkhisinhDekhac = chkDekhac.Checked;
                        objPKNhikhoa.CannangLucsinh = Utility.sDbnull(txtCannanglucsinh.Text);
                        objPKNhikhoa.DitatBamsinh = chkDitatbamsinh.Checked;
                        objPKNhikhoa.DitatBamsinhMota = Utility.sDbnull(txt_cuthetatbamsinh.Text);
                        objPKNhikhoa.PhattrienVetinhthan = Utility.sDbnull(txt_phattrienvetinhthan.Text);
                        objPKNhikhoa.PhattrienVevandong = Utility.sDbnull(txt_phattrienvevandong.Text);
                        objPKNhikhoa.Cacbenhlykhac = Utility.sDbnull(txt_cacbenhlykhac.Text);
                        objPKNhikhoa.NuoiduongSuame = chkSuame.Checked;
                        objPKNhikhoa.NuoiduongNhantao = chkNuoinhantao.Checked;
                        objPKNhikhoa.NuoiduongHonhop = chkNuoihonhop.Checked;
                        objPKNhikhoa.NuoidungCaisuathangthu = Utility.ByteDbnull(nmrCaisuathangthu.Text);
                        objPKNhikhoa.ChamsocTaivuontre = chkTaivuontre.Checked;
                        objPKNhikhoa.ChamsocTainha = chkTainha.Checked;
                        objPKNhikhoa.DatiemchungKhacMota = Utility.sDbnull(txt_cuthenhungbenhkhacduoctiemchung.Text);
                       

                      
                        //Chức năng sống
                        objPKNhikhoa.NhomMau = txtNhommau.myCode;
                        objPKNhikhoa.HuyetAp = txtha.Text;
                        objPKNhikhoa.NhietDo = txtNhietDo.Text;
                        objPKNhikhoa.Mach = Utility.sDbnull(txtMach.Text);
                        objPKNhikhoa.NhịpTho = Utility.sDbnull(txtNhipTho.Text);
                        objPKNhikhoa.ChieuCao = Utility.sDbnull(txtChieuCao.Text);
                        objPKNhikhoa.CanNang = Utility.sDbnull(txtCanNang.Text);
                        objPKNhikhoa.Bmi = Utility.sDbnull(txtBMI.Text);
                        //Lưu thông tin vào CSDL
                        objPKNhikhoa.Save();
                        if (txt_KhamToanThan.Enabled)
                            objPKB.Save();
                      
                        Utility.Log(Name, globalVariables.UserName, string.Format(
                                              "Lưu thông tin phiếu khám Nhi khoa cho người bệnh có mã lần khám {0} và ID bệnh nhân {1} ",
                                              objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan),
                                         ActID > 0? newaction.Update: newaction.Insert, this.GetType().Assembly.ManifestModule.Name);
                    }
                    scope.Complete();
                }
                Utility.ShowMsg("Bạn đã lưu thông tin khám thành công. Nhấn nút OK để kết thúc");
                if (chkCloseAfterSave.Checked)
                    this.Close();
                else
                    Utility.SetMsg(lblMsg, "Lưu thông tin thành công", false);

            }
            catch (Exception exception)
            {
                Utility.CatchException(string.Format("Lỗi trong quá trình Lưu thông tin khám"), exception);
                //throw;
            }
        }
        


        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
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

       
    }
}
