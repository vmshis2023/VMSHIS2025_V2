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
    public partial class frm_khamnoikhoa : Form
    {
        public KcbLuotkham objLuotkham;
        KcbDanhsachBenhnhan objBenhnhan;
        bool AllowedChanged = false;
        action m_enAct = action.FirstOrFinished;
        public frm_khamnoikhoa(KcbLuotkham objLuotkham, KcbDanhsachBenhnhan objBenhnhan)
        {
            InitializeComponent();
            this.KeyDown += frm_khamnoikhoa_KeyDown;
            this.objLuotkham = objLuotkham;
            this.objBenhnhan = objBenhnhan;
            // objNguoibenh = new Select().From(VKcbLuotkham.Schema).Where(VKcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).And(VKcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<VKcbLuotkham>();
            Utility.SetVisualStyle(this);
            //txt_KhamToanThan._OnSaveAs += txt_KhamToanThan__OnSaveAs;
            //txt_KhamToanThan._OnShowData += txt_KhamToanThan__OnShowData;
            //txt_KhamBoPhan._OnSaveAs += txt_KhamBoPhan__OnSaveAs;
            //txt_KhamBoPhan._OnShowData += txt_KhamBoPhan__OnShowData;
            ucThongtinnguoibenh1._OnEnterMe += ucThongtinnguoibenh1__OnEnterMe;
            ucThongtinnguoibenh1.txtMaluotkham.Text = objLuotkham.MaLuotkham;
            cmdthemmoi.Click += cmdthemmoi_Click;
            cmdSua.Click += cmdSua_Click;
            cmdxoa.Click += cmdxoa_Click;
            cmdHuy.Click += cmdHuy_Click;
            cmdIn.Click += cmdIn_Click;
            cmdGhi.Click += cmdGhi_Click;
            grdLichSu.SelectionChanged += grdLichSu_SelectionChanged;

           
            txtNhommau._OnShowDataV1 += __OnShowDataV1;
            chkDiUng.CheckedChanged += chkDiUng_CheckedChanged;
            chkMaTuy.CheckedChanged += chkMaTuy_CheckedChanged;
            chkRuouBia.CheckedChanged += chkRuouBia_CheckedChanged;
            chkThuocLa.CheckedChanged += chkThuocLa_CheckedChanged;
            chkThuocLao.CheckedChanged += chkThuocLao_CheckedChanged;
            chkKhac.CheckedChanged += chkKhac_CheckedChanged;
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
        void chkKhac_CheckedChanged(object sender, EventArgs e)
        {
            txtkhac.Enabled = chkKhac.Checked;
            txtBophanKhac.Focus();
        }

        void chkThuocLao_CheckedChanged(object sender, EventArgs e)
        {
            txtThuocLao.Enabled = chkThuocLao.Checked;
            txtThuocLao.Focus();
        }

        void chkThuocLa_CheckedChanged(object sender, EventArgs e)
        {
            txtThuocLa.Enabled = chkThuocLa.Checked;
            txtThuocLa.Focus();
        }

        void chkRuouBia_CheckedChanged(object sender, EventArgs e)
        {
            txtRuouBia.Enabled = chkRuouBia.Checked;
            txtRuouBia.Focus();
        }

        void chkMaTuy_CheckedChanged(object sender, EventArgs e)
        {
            txtMaTuy.Enabled = chkMaTuy.Checked;
            txtMaTuy.Focus();
        }

        void chkDiUng_CheckedChanged(object sender, EventArgs e)
        {
            txtDiUng.Enabled = chkDiUng.Checked;
            txtDiUng.Focus();
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
            if (!IsValidChucnangsong()) return;
            //Kiểm tra theo ngày nếu muốn
            SaveData();
            m_enAct = action.FirstOrFinished;
            SetControlStatus();
        }

        void grdLichSu_SelectionChanged(object sender, EventArgs e)
        {
            if (!AllowedChanged || !Utility.isValidGrid(grdLichSu))
            {
                ClearControls();
                ModifyCommmands();
                return;
            }
            if (grdLichSu.RowCount > 0 && grdLichSu.CurrentRow.RowType == RowType.Record)
            {
                objphieukham = EmrPhieukhamNoikhoa.FetchByID(Utility.Int64Dbnull(grdLichSu.GetValue("id")));
                FillData();
            }
        }

        void cmdIn_Click(object sender, EventArgs e)
        {
            
        }

        void cmdHuy_Click(object sender, EventArgs e)
        {
            m_enAct = action.FirstOrFinished;
            SetControlStatus();
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
                if (globalVariables.IsAdmin || Utility.sDbnull(grdLichSu.GetValue("id_bacsi"), "-1") == globalVariables.gv_intIDNhanvien.ToString())
                {
                    objphieukham = EmrPhieukhamNoikhoa.FetchByID(Utility.Int64Dbnull(grdLichSu.GetValue("Id")));
                    if (objphieukham != null)
                    {
                        if (Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa thông tin khám chữa bệnh ngày {0} của bác sĩ {1} thực hiện", "Cảnh báo", true))
                        {
                            EmrPhieukhamNoikhoa.Delete(objphieukham.Id);
                            Utility.RemoveRowfromDataTable("id=" + objphieukham.Id.ToString(), m_dtData);
                        }
                    }
                }
                else
                {
                    Utility.ShowMsg(string.Format("Bạn không thể xóa thông tin khám được tạo bởi bác sĩ {0}.\nVui lòng kiểm tra lại", Utility.sDbnull(grdLichSu.GetValue("ten_bacsi"), "")));
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

        void cmdSua_Click(object sender, EventArgs e)
        {
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Bạn cần chọn bệnh nhân cần KCB cơ bản");
                ucThongtinnguoibenh1.txtMaluotkham.Focus();
                ucThongtinnguoibenh1.txtMaluotkham.SelectAll();
                return;
            }

            //if (objLuotkham.TrangthaiNgoaitru == 1 || (Utility.Byte2Bool(objLuotkham.TthaiThopNoitru) && objLuotkham.TrangthaiNoitru == 6))
            //{
            //    Utility.ShowMsg("Bệnh nhân đã kết thúc khám nên bạn không thể thực hiện chức năng này");
            //    ucThongtinnguoibenh1.txtMaluotkham.Focus();
            //    ucThongtinnguoibenh1.txtMaluotkham.SelectAll();
            //    return;
            //}
            m_enAct = action.Update;
            SetControlStatus();
        }

        void cmdthemmoi_Click(object sender, EventArgs e)
        {
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Bạn cần chọn người bệnh trước khi thực hiện khám");
                ucThongtinnguoibenh1.txtMaluotkham.Focus();
                ucThongtinnguoibenh1.txtMaluotkham.SelectAll();
                return;
            }

            //if (objLuotkham.TrangthaiNgoaitru==1 ||( Utility.Byte2Bool(objLuotkham.TthaiThopNoitru) && objLuotkham.TrangthaiNoitru == 6))
            //{
            //    Utility.ShowMsg("Bệnh nhân đã kết thúc khám nên bạn không thể thực hiện chức năng này");
            //    ucThongtinnguoibenh1.txtMaluotkham.Focus();
            //    ucThongtinnguoibenh1.txtMaluotkham.SelectAll();
            //    return;
            //}
            m_enAct = action.Insert;
            objphieukham = new EmrPhieukhamNoikhoa();
            SetControlStatus();
        }
        void EnableControl( bool Enable)
        {
            foreach (TabPage tp in tabControl1.TabPages)
            {
                foreach (Control ctr in tp.Controls)
                    ctr.Enabled = Enable;
            }
            foreach (Control ctr in grpChucNangSong.Controls)
                ctr.Enabled = Enable;
        }
        void ClearControls()
        {
            chkDiUng.Checked = false;
            chkMaTuy.Checked = false;
            chkRuouBia.Checked = false;
            chkThuocLa.Checked = false;
            chkThuocLao.Checked = false;
            chkKhac.Checked = false;
            txtDiUng.Clear();
            txtMaTuy.Clear();
            txtRuouBia.Clear();
            txtThuocLa.Clear();
            txtThuocLao.Clear();
            txtkhac.Clear();
            foreach (TabPage tp in tabControl1.TabPages)
            {
                foreach (Control ctr in tp.Controls)
                    if (ctr.GetType().Equals(autoTxt.GetType()))
                        ((AutoCompleteTextbox_Danhmucchung)ctr).SetDefaultItem();
                    else if (ctr is EditBox)
                    {
                        ((EditBox)(ctr)).Clear();
                    }
            }
            
            foreach (Control ctr in grpChucNangSong.Controls)
                if (ctr.GetType().Equals(autoTxt.GetType()))
                    ((AutoCompleteTextbox_Danhmucchung)ctr).SetDefaultItem();
                else if (ctr is EditBox)
                {
                    ((EditBox)(ctr)).Clear();
                }
           
        }
        private void SetControlStatus()
        {
            try
            {
                grdLichSu.Enabled = false;
                AllowedChanged = false;
                switch (m_enAct)
                {
                    case action.Insert:
                        //Cho phép nhập liệu mã loại đối tượng,vị trí, tên loại đối tượng và mô tả thêm
                        Utility.DisabledTextBox(txtID);
                        ClearControls();
                        dtNgayKham.Enabled = true;
                        grdLichSu.Enabled = false;
                        EnableControl(true);
                        //--------------------------------------------------------------
                        //Thiết lập trạng thái các nút Insert, Update, Delete...
                        //Không cho phép nhấn Insert, Update,Delete
                        cmdthemmoi.Enabled = false;
                        cmdSua.Enabled = false;
                        cmdxoa.Enabled = false;
                        cmdGhi.Enabled = true;
                        cmdHuy.Enabled = true;
                        cmdExit.Enabled = false;
                        cmdIn.Enabled = false;
                        cmdGhi.BringToFront();
                        cmdHuy.BringToFront();
                       
                        //--------------------------------------------------------------
                        //Không cho phép chọn trên lưới dữ liệu được fill vào các Control
                        AllowedChanged = false;
                        //Tự động Focus đến mục ID để người dùng nhập liệu
                        txtID.Text = "Tự sinh";
                        cmdHuy.Text = "Hủy";
                        dtNgayKham.Focus();
                        break;
                    case action.Update:
                        //Không cho phép cập nhật lại mã loại đối tượng
                         Utility.DisabledTextBox(txtID);
                        dtNgayKham.Enabled = true;
                        grdLichSu.Enabled = false;
                        EnableControl(true);
                        //--------------------------------------------------------------
                        //Thiết lập trạng thái các nút Insert, Update, Delete...
                        //Không cho phép nhấn Insert, Update,Delete
                        cmdthemmoi.Enabled = false;
                        cmdSua.Enabled = false;
                        cmdxoa.Enabled = false;
                        cmdGhi.Enabled = true;
                        cmdHuy.Enabled = true;
                        cmdExit.Enabled = false;
                        cmdIn.Enabled = false;
                        cmdGhi.BringToFront();
                        cmdHuy.BringToFront();
                        cmdHuy.Text = "Hủy";
                        //--------------------------------------------------------------
                        //Không cho phép chọn trên lưới dữ liệu được fill vào các Control
                        AllowedChanged = false;
                        //Tự động Focus đến mục Position để người dùng nhập liệu
                        dtNgayKham.Focus();
                        break;
                    case action.FirstOrFinished://Hủy hoặc trạng thái ban đầu khi mới hiển thị Form
                        EnableControl(false);
                        grdLichSu.Enabled = true;
                        //Không cho phép nhập liệu mã loại đối tượng, tên loại đối tượng và mô tả thêm
                        Utility.DisabledTextBox(txtID);

                        //--------------------------------------------------------------
                        //Thiết lập trạng thái các nút Insert, Update, Delete...
                        //Sau khi nhấn Ghi thành công hoặc Hủy thao tác thì quay về trạng thái ban đầu
                        //Cho phép thêm mới
                        cmdGhi.Enabled = false;
                        cmdHuy.Enabled = false;
                        cmdExit.Enabled = true;
                        cmdGhi.SendToBack();
                        cmdHuy.SendToBack();
                        //Nút Hủy biến thành nút thoát
                        //cmdHuy.Text = "Thoát";
                        //--------------------------------------------------------------
                        //Cho phép chọn trên lưới dữ liệu được fill vào các Control
                        AllowedChanged = true;
                        //Tự động chọn dòng hiện tại trên lưới để hiển thị lại trên Control
                        grdLichSu_SelectionChanged(grdLichSu, new EventArgs());
                        //Tự động Focus đến nút thêm mới? 
                        cmdthemmoi.Focus();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
            }

        }
        void ModifyCommmands()
        {
            cmdthemmoi.Enabled = grdLichSu.RowCount <=0;
            cmdSua.Enabled = cmdIn.Enabled = cmdxoa.Enabled = grdLichSu.RowCount > 0;
            cmdExit.Enabled = true;
        }
        void frm_khamnoikhoa_KeyDown(object sender, KeyEventArgs e)
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
            else if (e.Control && e.KeyCode == Keys.N) cmdthemmoi.PerformClick();
            else if (e.Control && e.KeyCode == Keys.U) cmdSua.PerformClick();
            else if (e.Control && e.KeyCode == Keys.D) cmdHuy.PerformClick();
            else if (e.Control && e.KeyCode == Keys.P) cmdIn.PerformClick();
        }

        void ucThongtinnguoibenh1__OnEnterMe()
        {
            if (ucThongtinnguoibenh1.objLuotkham != null)
            {
                objLuotkham = ucThongtinnguoibenh1.objLuotkham;
                this.Text = string.Format("Phiếu khám nội khoa cho người bệnh {0} - {1} - {2} -{3}", ucThongtinnguoibenh1.txtTenBN.Text, ucThongtinnguoibenh1.txtgioitinh.Text, ucThongtinnguoibenh1.txttuoi.Text, ucThongtinnguoibenh1.txtDiachi.Text);
                AllowedChanged = false;
                LoadLichSu();
                AllowedChanged = true;
                grdLichSu_SelectionChanged(grdLichSu, new EventArgs());
            }
        }
       
       
        private void frm_khamnoikhoa_Load(object sender, EventArgs e)
        {
            InitDanhmucchung();
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
            if (globalVariables.IsAdmin)
            {
                txtBacsi.Enabled = true;
            }
            else
            {
                txtBacsi.Enabled = false;
            }
            ucThongtinnguoibenh1.Refresh();
            
            dtNgayKham.Value = DateTime.Now.Date;

            dtNgayKham.Focus();
            ModifyCommmands();
            SetControlStatus();
            //autoCompleteTextbox1.Init(,List<string> {yourId, yourCode, Name});
        }
        DataTable m_dtData = new DataTable();
        private void LoadLichSu()
        {
            grdLichSu.DataSource = null;
            m_dtData = SPs.KcbLaydanhsachPhieukhamnoikhoa(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham).GetDataSet().Tables[0];

            Utility.SetDataSourceForDataGridEx(grdLichSu, m_dtData, true, true, "1=1", "ngay_kham desc");
            objtsb = new Select().From(EmrTiensubenhDacdiemlienquan.Schema)
                .Where(EmrTiensubenhDacdiemlienquan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                .And(EmrTiensubenhDacdiemlienquan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                .ExecuteSingle<EmrTiensubenhDacdiemlienquan>();
        }
        EmrPhieukhamNoikhoa objphieukham = null;
        EmrTiensubenhDacdiemlienquan objtsb = null;
        private void FillData()
        {
            try
            {
                if(objtsb!=null)
                {
                    //Thông tin dị ứng
                    chkDiUng.Checked = Utility.Int16Dbnull(objtsb.TsbDiung) == 1;
                    chkMaTuy.Checked = Utility.Int16Dbnull(objtsb.TsbMatuy) == 1;
                    chkRuouBia.Checked = Utility.Int16Dbnull(objtsb.TsbRuoubia) == 1;
                    chkThuocLa.Checked = Utility.Int16Dbnull(objtsb.TsbThuocla) == 1;
                    chkThuocLao.Checked = Utility.Int16Dbnull(objtsb.TsbThuoclao) == 1;
                    chkKhac.Checked = Utility.Int16Dbnull(objtsb.TsbKhac) == 1;
                    txtDiUng.Text = Utility.sDbnull(objtsb.TsbThoigianDiung);
                    txtMaTuy.Text = Utility.sDbnull(objtsb.TsbThoigianMatuy);
                    txtRuouBia.Text = Utility.sDbnull(objtsb.TsbThoigianRuoubia);
                    txtThuocLa.Text = Utility.sDbnull(objtsb.TsbThoigianThuocla);
                    txtThuocLao.Text = Utility.sDbnull(objtsb.TsbThoigianThuoclao);
                    txtkhac.Text = Utility.sDbnull(objtsb.TsbKhac);
                }    
                if (objphieukham != null)
                {

                    txtID.Text = objphieukham.Id.ToString();
                   
                    txtNhietDo.Text = objphieukham.NhietDo;
                    txtha.Text = objphieukham.NhomMau;
                    txtMach.Text = objphieukham.Mach;
                    txtNhipTho.Text = objphieukham.NhipTho;
                    txtChieuCao.Text = objphieukham.ChieuCao;
                    txtCanNang.Text = objphieukham.CanNang;
                    txtBMI.Text = objphieukham.Bmi;
                    txtNhommau.SetCode(objphieukham.NhomMau);
                    //txtToanthan.Text = objphieukham.NoikhoaToanthan;
                    //txtTuanhoan.Text = objphieukham.NoikhoaTuanhoan;
                    //txtHohap.Text = objphieukham.NoikhoaHohap;
                    //txtTieuhoa.Text = objphieukham.NoikhoaTieuhoa;
                    //txtThantietnieu.Text = objphieukham.NoikhoaThantietnieusinhduc;
                    //txtThankinh.Text = objphieukham.NoikhoaThankinh;
                    //txtCoxuongkhop.Text = objphieukham.NoikhoaCoxuongkhop;
                    //txtTaimuihong.Text = objphieukham.NoikhoaTaimuihong;
                    //txtRanghammat.Text = objphieukham.NoikhoaRanghammat;
                    //txtMat.Text = objphieukham.NoikhoaMat;
                    //txtBophanKhac.Text = objphieukham.NoikhoaKhac;
                    
                    txtBacsi.SetId(objphieukham.IdBacsi);
                    //dtNgayKham.Value = Convert.ToDateTime(string.IsNullOrEmpty(objphieukham.NgayKham) ? dtNgayKham.Value : objphieukham.NgayKham);
                    dtNgayKham.Value = string.IsNullOrEmpty(objphieukham.NgayKham.ToString()) ? dtNgayKham.Value : Convert.ToDateTime(objphieukham.NgayKham);
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

        private void grdLichSu_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
        }
        void SaveData()
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        if (objphieukham != null && objphieukham.Id > 0)
                        {
                            objphieukham.MarkOld();
                            objphieukham.NguoiSua = globalVariables.UserName;
                            objphieukham.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
                        }
                        else
                        {
                            objphieukham = new EmrPhieukhamNoikhoa();
                            objphieukham.IsNew = true;
                            objphieukham.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham);
                            objphieukham.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan);
                            objphieukham.NgayKham = dtNgayKham.Value.Date;
                            objphieukham.NguoiTao = globalVariables.UserName;
                            objphieukham.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
                        }
                        if (objtsb != null && objtsb.IdTsb > 0)
                        {
                            objtsb.MarkOld();
                            objtsb.NguoiSua = globalVariables.UserName;
                            objtsb.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
                        }
                        else
                        {
                            objtsb = new EmrTiensubenhDacdiemlienquan();
                            objtsb.IsNew = true;
                            objtsb.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham);
                            objtsb.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan);
                            objtsb.NguoiTao = globalVariables.UserName;
                            objtsb.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
                        }

                        objphieukham.NhomMau = txtNhommau.myCode;

                        objtsb.TsbDiung = chkDiUng.Checked;
                        objtsb.TsbMatuy = chkMaTuy.Checked;
                        objtsb.TsbRuoubia = chkRuouBia.Checked;
                        objtsb.TsbThuocla = chkThuocLa.Checked;
                        objtsb.TsbThuoclao = chkThuocLao.Checked;
                        objtsb.TsbKhac = chkKhac.Checked;
                        if (chkDiUng.Checked) objtsb.TsbThoigianDiung = txtDiUng.Text;
                        else objtsb.TsbThoigianDiung = "";
                        if (chkMaTuy.Checked) objtsb.TsbThoigianMatuy = txtMaTuy.Text;
                        else objtsb.TsbThoigianMatuy = "";
                        if (chkRuouBia.Checked) objtsb.TsbThoigianRuoubia = txtRuouBia.Text;
                        else objtsb.TsbThoigianRuoubia = "";
                        if (chkThuocLa.Checked) objtsb.TsbThoigianThuocla = txtThuocLa.Text;
                        else objtsb.TsbThoigianThuocla = "";
                        if (chkThuocLao.Checked) objtsb.TsbThoigianThuoclao = txtThuocLao.Text;
                        else objtsb.TsbThoigianThuoclao = "";
                        if (chkKhac.Checked) objtsb.TsbThoigianKhac = txtkhac.Text;
                        else objtsb.TsbThoigianKhac = "";

                        objphieukham.IdBacsi = Utility.Int16Dbnull(txtBacsi.MyID, -1);
                        //objphieukham.NoikhoaToanthan = Utility.sDbnull(txtToanthan.Text);
                        //objphieukham.NoikhoaTuanhoan = Utility.sDbnull(txtTuanhoan.Text);
                        //objphieukham.NoikhoaHohap = Utility.sDbnull(txtHohap.Text);
                        //objphieukham.NoikhoaTieuhoa = Utility.sDbnull(txtTieuhoa.Text);
                        //objphieukham.NoikhoaThantietnieusinhduc = Utility.sDbnull(txtThantietnieu.Text);
                        //objphieukham.NoikhoaThankinh = Utility.sDbnull(txtThankinh.Text);
                        //objphieukham.NoikhoaCoxuongkhop = Utility.sDbnull(txtCoxuongkhop.Text);
                        //objphieukham.NoikhoaTaimuihong = Utility.sDbnull(txtTaimuihong.Text);
                        //objphieukham.NoikhoaRanghammat = Utility.sDbnull(txtRanghammat.Text);
                        //objphieukham.NoikhoaMat = Utility.sDbnull(txtMat.Text);
                        //objphieukham.NoikhoaKhac = Utility.sDbnull(txtBophanKhac.Text);

                        objphieukham.HuyetAp = txtha.Text;
                        objphieukham.NhietDo = txtNhietDo.Text;
                        objphieukham.Mach = Utility.sDbnull(txtMach.Text);
                        objphieukham.NhipTho = Utility.sDbnull(txtNhipTho.Text);
                        objphieukham.ChieuCao = Utility.sDbnull(txtChieuCao.Text);
                        objphieukham.CanNang = Utility.sDbnull(txtCanNang.Text);
                        objphieukham.Bmi = Utility.sDbnull(txtBMI.Text);
                        objphieukham.MotaThem = "";
                        objphieukham.Save();
                        objtsb.Save();
                    }
                    scope.Complete();

                }
                Utility.ShowMsg("Bạn đã lưu thông tin khám thành công. Nhấn nút OK để kết thúc");
                LoadLichSu();
                Utility.GonewRowJanus(grdLichSu, "Id", objphieukham.Id.ToString());
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                //throw;
            }
        }
        private void dtNgayKham_ValueChanged(object sender, EventArgs e)
        {

        }

        private void txtBacsi_TextChanged(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdxoa_Click_1(object sender, EventArgs e)
        {

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
