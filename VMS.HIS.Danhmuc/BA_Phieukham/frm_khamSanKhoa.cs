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
    public partial class frm_khamSanKhoa : Form
    {
        public KcbLuotkham objLuotkham;
        KcbDanhsachBenhnhan objBenhnhan;
        DataTable dt_tssk;
        bool AllowedChanged = false;
        action m_enAct = action.FirstOrFinished;
        public frm_khamSanKhoa(KcbLuotkham objLuotkham, KcbDanhsachBenhnhan objBenhnhan)
        {
            InitializeComponent();
            this.KeyDown += frm_khamSanKhoa_KeyDown;
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
            grdTiensuSankhoa.MouseDoubleClick += GrdTiensuSankhoa_MouseDoubleClick;
            grdTiensuSankhoa.ColumnButtonClick += GrdTiensuSankhoa_ColumnButtonClick;
        }

        private void GrdTiensuSankhoa_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            if (e.Column.Key == "")
            {
                int num = new Delete().From(EmrTiensuSankhoa.Schema).Where(EmrTiensuSankhoa.Columns.Id).IsEqualTo(Utility.Int32Dbnull(grdTiensuSankhoa.GetValue("id"))).Execute();
                if (num > 0)
                {
                    grdTiensuSankhoa.CurrentRow.Delete();
                }
            }
        }

        private void GrdTiensuSankhoa_MouseDoubleClick(object sender, MouseEventArgs e)
        {
          if(Utility.isValidGrid(grdTiensuSankhoa))
            {
                EmrTiensuSankhoa tssk = EmrTiensuSankhoa.FetchByID(Utility.Int64Dbnull(grdTiensuSankhoa.GetValue("Id")));
                frm_ThemtiensuSankhoa f = new frm_ThemtiensuSankhoa(objLuotkham, tssk);
                if (f.ShowDialog() == DialogResult.OK)
                {
                    
                }
            }    
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
            if (Utility.sDbnull(cboBacsi.SelectedValue, "-1") == "1")
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
                if (globalVariables.IsAdmin || objPhieukhamSankhoa.NguoiTao == globalVariables.UserName.ToString())
                {
                    objPhieukhamSankhoa = EmrPhieukhamSankhoa.FetchByID(objPhieukhamSankhoa.Id);
                    if (objPhieukhamSankhoa != null)
                    {
                        if (Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa thông tin khám chữa bệnh ngày {0} của bác sĩ {1} thực hiện", "Cảnh báo", true))
                        {
                            EmrPhieukhamSankhoa.Delete(objPhieukhamSankhoa.Id);
                        }
                    }
                    else
                    {
                        Utility.ShowMsg(string.Format("Không thể xóa phiếu khám phụ khoa.\nVui lòng kiểm tra lại vì có thể trong lúc bạn mở thao tác người khác đã xóa thông tin", objPhieukhamSankhoa.NguoiTao));
                    }    
                }
                else
                {
                    Utility.ShowMsg(string.Format("Bạn không thể xóa thông tin khám được tạo bởi bác sĩ {0}.\nVui lòng kiểm tra lại", objPhieukhamSankhoa.NguoiTao));
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
            cmdxoa.Enabled = cmdIn.Enabled = objPhieukhamSankhoa != null;
        }
        void frm_khamSanKhoa_KeyDown(object sender, KeyEventArgs e)
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
                this.Text = string.Format("Phiếu khám phụ khoa cho người bệnh {0} - {1} - {2} -{3}", ucThongtinnguoibenh1.txtTenBN.Text, ucThongtinnguoibenh1.txtgioitinh, ucThongtinnguoibenh1.txttuoi, ucThongtinnguoibenh1.txtDiachi);
                objPKB = new Select().From(EmrPhieukhambenh.Schema)
                   .Where(EmrPhieukhambenh.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                   .And(EmrPhieukhambenh.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                   .ExecuteSingle<EmrPhieukhambenh>();
                objPhieukhamSankhoa= new Select().From(EmrPhieukhamSankhoa.Schema)
                   .Where(EmrPhieukhamSankhoa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                   .And(EmrPhieukhamSankhoa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                   .ExecuteSingle<EmrPhieukhamSankhoa>();
                objChandoanSankhoa = new Select().From(EmrChandoanSankhoa.Schema)
                  .Where(EmrChandoanSankhoa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                  .And(EmrChandoanSankhoa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                  .ExecuteSingle<EmrChandoanSankhoa>();
                objTspk = new Select().From(EmrTiensusanphukhoa.Schema)
                  .Where(EmrTiensusanphukhoa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                  .And(EmrTiensusanphukhoa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                  .ExecuteSingle<EmrTiensusanphukhoa>();
                objQttk = new Select().From(EmrQuatrinhThaiky.Schema)
                 .Where(EmrQuatrinhThaiky.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                 .And(EmrQuatrinhThaiky.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                 .ExecuteSingle<EmrQuatrinhThaiky>();
                dt_tssk = new Select().From(EmrTiensuSankhoa.Schema)
                     .Where(EmrQuatrinhThaiky.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                 .And(EmrQuatrinhThaiky.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                 .ExecuteDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx_Basic(grdTiensuSankhoa, dt_tssk, true, true, "",
                                                       EmrTiensuSankhoa.Columns.Nam); //"locked=0", "");
                FillData();
            }
            else
            {
                ClearControls();
                this.Text = "Phiếu khám phụ khoa";
            }    
        }
       
       
        private void frm_khamSanKhoa_Load(object sender, EventArgs e)
        {
            InitDanhmucchung();
            DataBinding.BindDataCombobox(cboBacsi, globalVariables.gv_dtDmucNhanvien.Copy(),
                                     DmucNhanvien.Columns.UserName, DmucNhanvien.Columns.TenNhanvien, "----Chọn bác sĩ khám----", true);
            ucThongtinnguoibenh1.Refresh();
            dtNgayKham.Value = DateTime.Now.Date;
            dtNgayKham.Focus();
            ModifyCommmands();
           
        }
        EmrChandoanSankhoa objChandoanSankhoa;
        EmrTiensusanphukhoa objTspk = null;
        EmrPhieukhamSankhoa objPhieukhamSankhoa = null;
        EmrQuatrinhThaiky objQttk;
        private void FillData()
        {
            try
            {
                if (objPhieukhamSankhoa != null)
                {

                    txtID.Text = objPhieukhamSankhoa.Id.ToString();
                    txtNhietDo.Text = objPhieukhamSankhoa.NhietDo;
                    txtha.Text = objPhieukhamSankhoa.NhomMau;
                    txtMach.Text = objPhieukhamSankhoa.Mach;
                    txtNhipTho.Text = objPhieukhamSankhoa.NhịpTho;
                    txtChieuCao.Text = objPhieukhamSankhoa.ChieuCao;
                    txtCanNang.Text = objPhieukhamSankhoa.CanNang;
                    txtBMI.Text = objPhieukhamSankhoa.Bmi;
                    txtNhommau.SetCode(objPhieukhamSankhoa.NhomMau);
                    //khám ngoài
                    chkBungcoseophauthuatcu.Checked= Utility.Bool2Bool(objPhieukhamSankhoa.KhamngoaiBungcoseophauthuatcu);
                    txtHinhdangTucung.Text = Utility.sDbnull(objPhieukhamSankhoa.KhamngoaiHinhdangtucung);
                    txtTutheTucung.Text = Utility.sDbnull(objPhieukhamSankhoa.KhamngoaiTuthe);
                    txtChieucaoTC.Text = Utility.sDbnull(objPhieukhamSankhoa.KhamngoaiChieucaotucung);
                    txtVongbung.Text = Utility.sDbnull(objPhieukhamSankhoa.KhamngoaiVongbung);
                    txtConcoTC.Text = Utility.sDbnull(objPhieukhamSankhoa.KhamngoaiConcotucung);
                    txtTimthai.Text = Utility.sDbnull(objPhieukhamSankhoa.KhamngoaiTimthai);
                    txtVu.Text = Utility.sDbnull(objPhieukhamSankhoa.KhamngoaiVu);
                   
                    //Khám trong
                    txtChisoBishop.Text = Utility.sDbnull(objPhieukhamSankhoa.KbChisoBishop);
                    txtAmho.Text = Utility.sDbnull(objPhieukhamSankhoa.KbAmho);
                    txtAmdao.Text = Utility.sDbnull(objPhieukhamSankhoa.KbAmdao);
                    txtTangsinhmon.Text = Utility.sDbnull(objPhieukhamSankhoa.KbTangsinhmon);
                    txtCoTC.Text = Utility.sDbnull(objPhieukhamSankhoa.KbCotucung);
                    txtPhanphu.Text = Utility.sDbnull(objPhieukhamSankhoa.KbPhanphu);

                    optOiphong.Checked = Utility.Bool2Bool(objPhieukhamSankhoa.KbTinhtrangoiPhong);
                    optOidet.Checked = Utility.Bool2Bool(objPhieukhamSankhoa.KbTinhtrangoiDet);
                    optOiquale.Checked = Utility.Bool2Bool(objPhieukhamSankhoa.KbTinhtrangoiQuale);
                   
                    optOivoTunhien.Checked = Utility.Bool2Bool(objPhieukhamSankhoa.KbTinhtrangoivoTunhien);
                    optOivoBamoi.Checked = Utility.Bool2Bool(objPhieukhamSankhoa.KbTinhtrangoivoBamoi);
                   
                    optDolotCao.Checked = Utility.Bool2Bool(objPhieukhamSankhoa.KbDolotCao);
                    optDolotChuc.Checked = Utility.Bool2Bool(objPhieukhamSankhoa.KbDolotChuc);
                    optDolotChat.Checked = Utility.Bool2Bool(objPhieukhamSankhoa.KbDolotChat);
                    optDolotLot.Checked = Utility.Bool2Bool(objPhieukhamSankhoa.KbDolotLot);

                    txtMausacNuocoi.Text = Utility.sDbnull(objPhieukhamSankhoa.KbMausacnuocoi);
                    txtNuocoiNhieuhayIt.Text = Utility.sDbnull(objPhieukhamSankhoa.KbNuocoinhieuit);
                    txtKbNgoi.Text = Utility.sDbnull(objPhieukhamSankhoa.KbNgoi);
                    txtThe.Text = Utility.sDbnull(objPhieukhamSankhoa.KbThe);
                    txtKieuthe.Text = Utility.sDbnull(objPhieukhamSankhoa.KbKieuthe);
                    txtDuongkinhnhoHave.Text = Utility.sDbnull(objPhieukhamSankhoa.KbDuongkinhnhohave);
                    
                   
                    cboBacsi.SelectedValue = Utility.sDbnull(objPhieukhamSankhoa.IdBacsi, "-1");
                    //dtNgayKham.Value = Convert.ToDateTime(string.IsNullOrEmpty(objPhieukhamSankhoa.NgayKham) ? dtNgayKham.Value : objPhieukhamSankhoa.NgayKham);
                    dtNgayKham.Value = string.IsNullOrEmpty(objPhieukhamSankhoa.NgayKham.ToString()) ? dtNgayKham.Value : Convert.ToDateTime(objPhieukhamSankhoa.NgayKham);

                   
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
                //Khám bộ phận
                if(objPKB!=null)
                {
                    txt_KhamToanThan.Text = objPKB.ToanThan;
                    txt_KhamBoPhan.Text = objPKB.BoPhan;
                    
                }  
                //Chẩn đoán sản khoa
                if(objChandoanSankhoa!=null)
                {
                    lnkChandoanSankhoa_UserName.Visible = objChandoanSankhoa.NguoiTao != globalVariables.UserName;
                    pnlChanDoanSankhoa.Enabled= objChandoanSankhoa.NguoiTao == globalVariables.UserName;
                    if (lnkChandoanSankhoa_UserName.Visible)
                    {
                        lnkChandoanSankhoa_UserName.Text = string.Format("Dữ liệu này được tạo bởi người dùng: {0}", objChandoanSankhoa.NguoiTao);
                    }    
                        txtLucvaode.Text = Utility.sDbnull(objChandoanSankhoa.CdLucvaode);
                    txtNgoithai.Text = Utility.sDbnull(objChandoanSankhoa.CdNgoithai);
                    txtCachthucde.Text = Utility.sDbnull(objChandoanSankhoa.CdCachthucde);
                    txtKiemsoattucung.Text = Utility.sDbnull(objChandoanSankhoa.CdKiemsoattucung);
                    txtDitat.Text = Utility.sDbnull(objChandoanSankhoa.CdDitatThainhi);
                    nmrCannang.Text = Utility.sDbnull(objChandoanSankhoa.CdCannangThainhi);
                    if (objChandoanSankhoa.CdNgaymode.HasValue)
                        dtpNgaymode.Value = objChandoanSankhoa.CdNgaymode.Value;
                    else
                        dtpNgaymode.ResetText();
                    optDonthai.Checked = Utility.Bool2Bool(objChandoanSankhoa.CdDonthai);
                    optDathai.Checked = Utility.Bool2Bool(objChandoanSankhoa.CdDathai);
                    optTrai.Checked = Utility.Bool2Bool(objChandoanSankhoa.CdTrai);
                    optGai.Checked = Utility.Bool2Bool(objChandoanSankhoa.CdGai);
                    optSong.Checked = Utility.Bool2Bool(objChandoanSankhoa.CdSong);
                    optChet.Checked = Utility.Bool2Bool(objChandoanSankhoa.CdChet);
                }
                else
                    lnkChandoanSankhoa_UserName.Visible = false;
                //Quá trình thai kỳ
                if (objQttk != null)
                {
                    lnkQuatrinhthaiky_UserName.Visible = objQttk.NguoiTao != globalVariables.UserName;
                    pnlChanDoanSankhoa.Enabled = objQttk.NguoiTao == globalVariables.UserName;
                    if (lnkChandoanSankhoa_UserName.Visible)
                    {
                        lnkQuatrinhthaiky_UserName.Text = string.Format("Dữ liệu này được tạo bởi người dùng: {0}", objQttk.NguoiTao);
                    }

                    if (objQttk.Kinhcuoitungay.HasValue)
                        dtpKinhcuoitungay.Value = objQttk.Kinhcuoitungay.Value;
                    else
                        dtpKinhcuoitungay.ResetText();
                    if (objQttk.Kinhcuoidenngay.HasValue)
                        dtpKinhcuoidenngay.Value = objQttk.Kinhcuoidenngay.Value;
                    else
                        dtpKinhcuoidenngay.ResetText();
                    txtKhamthaitai.Text = Utility.sDbnull(objQttk.Khamthaitai);
                    chkTiemphonguonvan.Checked = Utility.Bool2Bool(objQttk.TiemphongUonvan);
                    txtDuoctiemphonguonvanSolan.Text = Utility.sDbnull(objQttk.TiemphongUonvanSolan);
                    if (objQttk.Batdauchuyenda.HasValue)
                        dtpBatdauchuyendatu.Value = objQttk.Batdauchuyenda.Value;
                    txtDauhieuLucdau.Text = Utility.sDbnull(objQttk.Dauhieulucdau);
                    txtBienchuyen.Text = Utility.sDbnull(objQttk.Bienchuyen);
                }
                else
                    lnkQuatrinhthaiky_UserName.Visible = false;
                //Tiền sử sản phụ khoa
                if(objTspk!=null)
                {
                    lnkTiensuphukhoa.Visible = objTspk.NguoiTao != globalVariables.UserName;
                    pnlChanDoanSankhoa.Enabled = objTspk.NguoiTao == globalVariables.UserName;
                    if (lnkChandoanSankhoa_UserName.Visible)
                    {
                        lnkTiensuphukhoa.Text = string.Format("Dữ liệu này được tạo bởi người dùng: {0}", objTspk.NguoiTao);
                    }
                    txtBatdauthaykinhnam.Text = Utility.sDbnull(objTspk.BaTsspkBatdauthaykinhNam);
                    txt_batdauthaykinhtuoi.Text = Utility.sDbnull(objTspk.BaTsspkBatdauthaykinhTuoi);
                    txt_tinhchatkinhnguyet.Text = Utility.sDbnull(objTspk.BaTsspkTinhchatkinhnguyet);
                    txt_chuky.Text = Utility.sDbnull(objTspk.BaTsspkChuky);
                    txt_songaythaykinh.Text = Utility.sDbnull(objTspk.BaTsspkSongaythaykinh);
                    txt_luongkinh.Text = Utility.sDbnull(objTspk.BaTsspkLuongkinh);
                    if (objTspk.BaTsspkKinhlancuoingay.HasValue)
                        dtpKinhlancuoingay.Value = objTspk.BaTsspkKinhlancuoingay.Value;
                    else
                        dtpKinhlancuoingay.ResetText();
                    chkCodaubung.Checked = Utility.Bool2Bool(objTspk.BaTsspkCodaubung);
                    chk_thoigiantruoc.Checked = Utility.Bool2Bool(objTspk.BaTsspkThoigianTruoc);
                    chk_thoigiantrong.Checked = Utility.Bool2Bool(objTspk.BaTsspkThoigianTrong);
                    chk_thoigiansau.Checked = Utility.Bool2Bool(objTspk.BaTsspkThoigianSau);
                    txt_laychongnam.Text = Utility.sDbnull(objTspk.BaTsspkLaychongNam);
                    txt_laychongtuoi.Text = Utility.sDbnull(objTspk.BaTsspkLaychongTuoi);
                    txt_hetkinhnam.Text = Utility.sDbnull(objTspk.BaTsspkHetkinhnam);
                    txt_hetkinhtuoi.Text = Utility.sDbnull(objTspk.BaTsspkHetkinhtuoi);
                    txt_benhphukhoadadieutri.Text = Utility.sDbnull(objTspk.BaTsspkBenhphukhoadadieutri);
                    txt_para.Text = Utility.sDbnull(objTspk.BaTsspkPara);
                }
                else
                    lnkTiensuphukhoa.Visible = false;
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
                objPhieukhamSankhoa = new Select().From(EmrPhieukhamSankhoa.Schema)
                   .Where(EmrPhieukhamSankhoa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                   .And(EmrPhieukhamSankhoa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                   .ExecuteSingle<EmrPhieukhamSankhoa>();
                objChandoanSankhoa = new Select().From(EmrChandoanSankhoa.Schema)
                  .Where(EmrChandoanSankhoa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                  .And(EmrChandoanSankhoa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                  .ExecuteSingle<EmrChandoanSankhoa>();
                objTspk = new Select().From(EmrTiensusanphukhoa.Schema)
                  .Where(EmrTiensusanphukhoa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                  .And(EmrTiensusanphukhoa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                  .ExecuteSingle<EmrTiensusanphukhoa>();
                objQttk = new Select().From(EmrQuatrinhThaiky.Schema)
                 .Where(EmrQuatrinhThaiky.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                 .And(EmrQuatrinhThaiky.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                 .ExecuteSingle<EmrQuatrinhThaiky>();
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        //Phiếu khám bệnh toàn thân
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
                        //Chẩn đoán
                        if (objChandoanSankhoa != null && objChandoanSankhoa.Id > 0)
                        {
                            objChandoanSankhoa.MarkOld();
                            objChandoanSankhoa.NguoiSua = globalVariables.UserName;
                            objChandoanSankhoa.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
                        }
                        else
                        {
                            objChandoanSankhoa = new EmrChandoanSankhoa();
                            objChandoanSankhoa.IsNew = true;
                            objChandoanSankhoa.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham);
                            objChandoanSankhoa.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan);
                            objChandoanSankhoa.NguoiTao = globalVariables.UserName;
                            objChandoanSankhoa.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
                        }
                        //Quá trình thai kì
                        if (objQttk != null && objQttk.Id > 0)
                        {
                            objQttk.MarkOld();
                            objQttk.NguoiSua = globalVariables.UserName;
                            objQttk.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
                        }
                        else
                        {
                            objQttk = new EmrQuatrinhThaiky();
                            objQttk.IsNew = true;
                            objQttk.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham);
                            objQttk.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan);
                            objQttk.NguoiTao = globalVariables.UserName;
                            objQttk.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
                        }
                        //Tiền sử sản phụ khoa
                        if (objTspk != null && objTspk.IdTsspk > 0)
                        {
                            objTspk.MarkOld();
                            objTspk.NguoiSua = globalVariables.UserName;
                            objTspk.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
                        }
                        else
                        {
                            objTspk = new EmrTiensusanphukhoa();
                            objTspk.IsNew = true;
                            objTspk.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham);
                            objTspk.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan);
                            //objTspk.NgayKham = dtNgayKham.Value.Date;
                            objTspk.NguoiTao = globalVariables.UserName;
                            objTspk.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
                        }
                        //Phiếu khám sản khoa
                        if (objPhieukhamSankhoa != null && objPhieukhamSankhoa.Id > 0)
                        {
                            objPhieukhamSankhoa.MarkOld();
                            objPhieukhamSankhoa.NguoiSua = globalVariables.UserName;
                            objPhieukhamSankhoa.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
                        }
                        else
                        {
                            objPhieukhamSankhoa = new EmrPhieukhamSankhoa();
                            objPhieukhamSankhoa.IsNew = true;
                            objPhieukhamSankhoa.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham);
                            objPhieukhamSankhoa.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan);
                            objPhieukhamSankhoa.NgayKham = dtNgayKham.Value.Date;
                            objPhieukhamSankhoa.NguoiTao = globalVariables.UserName;
                            objPhieukhamSankhoa.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
                        }

                        ActID = objPhieukhamSankhoa.Id;
                        //Hỏi bệnh
                        objPhieukhamSankhoa.IdBacsi = Utility.Int16Dbnull(cboBacsi.SelectedValue, -1);

                        //khám ngoài
                        objPhieukhamSankhoa.KhamngoaiBungcoseophauthuatcu = chkBungcoseophauthuatcu.Checked;
                        objPhieukhamSankhoa.KhamngoaiHinhdangtucung = Utility.sDbnull(txtHinhdangTucung.Text);
                        objPhieukhamSankhoa.KhamngoaiTuthe = Utility.sDbnull(txtTutheTucung.Text);
                        objPhieukhamSankhoa.KhamngoaiChieucaotucung = Utility.ByteDbnull(txtChieucaoTC.Text, 0);
                        objPhieukhamSankhoa.KhamngoaiVongbung = Utility.ByteDbnull(txtVongbung.Text, 0);
                        objPhieukhamSankhoa.KhamngoaiConcotucung = Utility.sDbnull(txtConcoTC.Text);
                        objPhieukhamSankhoa.KhamngoaiTimthai = Utility.ByteDbnull(txtTimthai.Text, 0);
                        objPhieukhamSankhoa.KhamngoaiVu = Utility.sDbnull(txtVu.Text);

                        objPhieukhamSankhoa.KbTinhtrangoiPhong = optOiphong.Checked;
                        objPhieukhamSankhoa.KbTinhtrangoiDet = optOidet.Checked;
                        objPhieukhamSankhoa.KbTinhtrangoiQuale = optOiquale.Checked;

                        objPhieukhamSankhoa.KbTinhtrangoivoTunhien = optOivoTunhien.Checked;
                        objPhieukhamSankhoa.KbTinhtrangoivoBamoi = optOivoBamoi.Checked;

                        objPhieukhamSankhoa.KbDolotCao = optDolotCao.Checked;
                        objPhieukhamSankhoa.KbDolotChuc = optDolotChuc.Checked;
                        objPhieukhamSankhoa.KbDolotChat = optDolotChat.Checked;
                        objPhieukhamSankhoa.KbDolotLot = optDolotLot.Checked;

                        objPhieukhamSankhoa.KbChisoBishop = Utility.sDbnull(txtChisoBishop.Text);
                        objPhieukhamSankhoa.KbAmho = Utility.sDbnull(txtAmho.Text);
                        objPhieukhamSankhoa.KbAmdao = Utility.sDbnull(txtAmdao.Text);
                        objPhieukhamSankhoa.KbTangsinhmon = Utility.sDbnull(txtTangsinhmon.Text);
                        objPhieukhamSankhoa.KbCotucung = Utility.sDbnull(txtCoTC.Text);
                        objPhieukhamSankhoa.KbPhanphu = Utility.sDbnull(txtPhanphu.Text);
                        objPhieukhamSankhoa.KbMausacnuocoi = Utility.sDbnull(txtMausacNuocoi.Text);
                        objPhieukhamSankhoa.KbNuocoinhieuit = Utility.sDbnull(txtNuocoiNhieuhayIt.Text);
                        objPhieukhamSankhoa.KbNgoi = Utility.sDbnull(txtKbNgoi.Text);
                        objPhieukhamSankhoa.KbThe = Utility.sDbnull(txtThe.Text);
                        objPhieukhamSankhoa.KbKieuthe = Utility.sDbnull(txtKieuthe.Text);
                        objPhieukhamSankhoa.KbDuongkinhnhohave = Utility.sDbnull(txtDuongkinhnhoHave.Text);
                        //Chức năng sống
                        objPhieukhamSankhoa.NhomMau = txtNhommau.myCode;
                        objPhieukhamSankhoa.HuyetAp = txtha.Text;
                        objPhieukhamSankhoa.NhietDo = txtNhietDo.Text;
                        objPhieukhamSankhoa.Mach = Utility.sDbnull(txtMach.Text);
                        objPhieukhamSankhoa.NhịpTho = Utility.sDbnull(txtNhipTho.Text);
                        objPhieukhamSankhoa.ChieuCao = Utility.sDbnull(txtChieuCao.Text);
                        objPhieukhamSankhoa.CanNang = Utility.sDbnull(txtCanNang.Text);
                        objPhieukhamSankhoa.Bmi = Utility.sDbnull(txtBMI.Text);
                        objPhieukhamSankhoa.MotaThem = "";
                        //Lưu thông tin vào CSDL
                        objPhieukhamSankhoa.Save();
                        if (txt_KhamToanThan.Enabled)
                            objPKB.Save();
                        if (pnkQuatrinhThaiky.Enabled)
                            objQttk.Save();
                        if (pnlChanDoanSankhoa.Enabled)
                            objChandoanSankhoa.Save();
                        if (pnlTiensuPhukhoa.Enabled)
                            objTspk.Save();
                        Utility.Log(Name, globalVariables.UserName, string.Format(
                                              "Lưu thông tin phiếu khám phụ khoa cho người bệnh có mã lần khám {0} và ID bệnh nhân {1} ",
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

        private void cmdDiungkhac_Click(object sender, EventArgs e)
        {
            frm_Tiensubenh_Cacdacdiemlienquan _Tiensubenh_Cacdacdiemlienquan = new frm_Tiensubenh_Cacdacdiemlienquan(objLuotkham);
            _Tiensubenh_Cacdacdiemlienquan.ShowDialog();
        }

        private void cmdThem_Click(object sender, EventArgs e)
        {
            frm_ThemtiensuSankhoa f = new frm_ThemtiensuSankhoa(objLuotkham, null);
            f.dt_tssk = dt_tssk;
            if(f.ShowDialog()==DialogResult.OK)
            {
                //DataRow newdr = dt_tssk.NewRow();
                //Utility.FromObjectToDatarow(f.tssk, ref newdr);
                //dt_tssk.Rows.Add(newdr);
            }    

        }
    }
}
