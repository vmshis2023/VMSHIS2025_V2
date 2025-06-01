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


namespace VNS.HIS.UI.NGOAITRU
{
    public partial class frm_KCBCoban : Form
    {
        public KcbLuotkham objLuotkham;
        VKcbLuotkham objNguoibenh;
        bool AllowedChanged = false;
        action m_enAct = action.FirstOrFinished;
        public frm_KCBCoban(KcbLuotkham objLuotkham)
        {
            InitializeComponent();
            this.KeyDown += frm_KCBCoban_KeyDown;
            this.objLuotkham = objLuotkham;
            objNguoibenh = new Select().From(VKcbLuotkham.Schema).Where(VKcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).And(VKcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).ExecuteSingle<VKcbLuotkham>();
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

            txt_KhamBoPhan._OnShowDataV1 += __OnShowDataV1;
            txt_KhamToanThan._OnShowDataV1 += __OnShowDataV1;
            txtBoPhan_Bung._OnShowDataV1 += __OnShowDataV1;
            txtBoPhan_Co._OnShowDataV1 += __OnShowDataV1;
            txtBoPhan_CotSong._OnShowDataV1 += __OnShowDataV1;
            txtBoPhan_ChatThai._OnShowDataV1 += __OnShowDataV1;
            txtBoPhan_Dau._OnShowDataV1 += __OnShowDataV1;
            txtBoPhan_NghiCoBenh._OnShowDataV1 += __OnShowDataV1;
            txtBoPhan_Nguc._OnShowDataV1 += __OnShowDataV1;
            txtNhommau._OnShowDataV1 += __OnShowDataV1;
            txtToanThan_DangDi._OnShowDataV1 += __OnShowDataV1;
            txtToanThan_HeThongLongToc._OnShowDataV1 += __OnShowDataV1;
            txtToanThan_HinhDangChung._OnShowDataV1 += __OnShowDataV1;
            txtToanThan_MauSacDa._OnShowDataV1 += __OnShowDataV1;

            txtToanThan_TinhThan._OnShowDataV1 += __OnShowDataV1;
            txtToanThan_TinhTrangDa._OnShowDataV1 += __OnShowDataV1;
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
                objphieukham = KcbPhieukhamtoanthan.FetchByID(Utility.Int64Dbnull(grdLichSu.GetValue("id")));
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
                    objphieukham = KcbPhieukhamtoanthan.FetchByID(Utility.Int64Dbnull(grdLichSu.GetValue("Id")));
                    if (objphieukham != null)
                    {
                        if (Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa thông tin khám chữa bệnh ngày {0} của bác sĩ {1} thực hiện", "Cảnh báo", true))
                        {
                            KcbPhieukhamtoanthan.Delete(objphieukham.Id);
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

            if (objLuotkham.TrangthaiNgoaitru == 1 || (Utility.Byte2Bool(objLuotkham.TthaiThopNoitru) && objLuotkham.TrangthaiNoitru == 6))
            {
                Utility.ShowMsg("Bệnh nhân đã kết thúc khám nên bạn không thể thực hiện chức năng này");
                ucThongtinnguoibenh1.txtMaluotkham.Focus();
                ucThongtinnguoibenh1.txtMaluotkham.SelectAll();
                return;
            }
            m_enAct = action.Update;
            SetControlStatus();
        }

        void cmdthemmoi_Click(object sender, EventArgs e)
        {
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Bạn cần chọn bệnh nhân cần KCB cơ bản");
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
            objphieukham = new KcbPhieukhamtoanthan();
            SetControlStatus();
        }
        void EnableControl( bool Enable)
        {
            foreach (TabPage tp in tabControl1.TabPages)
            {
                foreach (Control ctr in tp.Controls)
                    ctr.Enabled = Enable;
            }
            foreach (TabPage tp in tabControl2.TabPages)
            {
                foreach (Control ctr in tp.Controls)
                    ctr.Enabled = Enable;
            }
            foreach (Control ctr in grpChucNangSong.Controls)
                ctr.Enabled = Enable;
        }
        void ClearControls()
        {
            foreach (TabPage tp in tabControl1.TabPages)
            {
                foreach (Control ctr in tp.Controls)
                    if (ctr.GetType().Equals(txt_KhamBoPhan.GetType()))
                        ((AutoCompleteTextbox_Danhmucchung)ctr).SetDefaultItem();
                    else if (ctr is EditBox)
                    {
                        ((EditBox)(ctr)).Clear();
                    }
            }
            foreach (TabPage tp in tabControl2.TabPages)
            {
                foreach (Control ctr in tp.Controls)
                    if (ctr.GetType().Equals(txt_KhamBoPhan.GetType()))
                        ((AutoCompleteTextbox_Danhmucchung)ctr).SetDefaultItem();
                    else if (ctr is EditBox)
                    {
                        ((EditBox)(ctr)).Clear();
                    }
            }
            foreach (Control ctr in grpChucNangSong.Controls)
                if (ctr.GetType().Equals(txt_KhamBoPhan.GetType()))
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
        void frm_KCBCoban_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if ((ActiveControl != null && ActiveControl.Name == txt_KhamToanThan.Name))
                {
                    tabControl1.SelectedIndex = 1;
                    txtToanThan_DangDi.Focus();

                }
                else if ((ActiveControl != null && ActiveControl.Name == txt_KhamBoPhan.Name))
                {
                    tabControl2.SelectedIndex = 1;
                    txtBoPhan_NghiCoBenh.Focus();

                }
                else if ((ActiveControl != null && ActiveControl.Name == txtToanThan_HeThongLongToc.Name))
                {
                    tabControl2.SelectedIndex = 0;
                    txt_KhamBoPhan.Focus();

                }
                else if ((ActiveControl != null && ActiveControl.Name == txtBoPhan_ChatThai.Name))
                {
                    txtHa.Focus();

                }
                else if ((ActiveControl != null && ActiveControl.Name == txtNhommau.Name))
                {
                    tabControl1.SelectedIndex = 0;
                    tabControl2.SelectedIndex = 0;
                    txt_KhamToanThan.Focus();

                }
                else
                    SendKeys.Send("{TAB}");
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
                AllowedChanged = false;
                LoadLichSu();
                AllowedChanged = true;
                grdLichSu_SelectionChanged(grdLichSu, new EventArgs());
            }
        }
        private void txt_KhamToanThan__OnSaveAs()
        {
            if (Utility.DoTrim(txt_KhamToanThan.Text) == "") return;
            var dmucDchung = new DMUC_DCHUNG(txt_KhamToanThan.LOAI_DANHMUC);
            dmucDchung.SetStatus(true, txt_KhamToanThan.Text);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txt_KhamToanThan.myCode;
                txt_KhamToanThan.Init();
                txt_KhamToanThan.SetCode(oldCode);
                txt_KhamToanThan.Focus();
            }
        }

        private void txt_KhamToanThan__OnShowData()
        {
            var dmucDchung = new DMUC_DCHUNG(txt_KhamToanThan.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txt_KhamToanThan.myCode;
                txt_KhamToanThan.Init();
                txt_KhamToanThan.SetCode(oldCode);
                txt_KhamToanThan.Focus();
            }
        }
        private void txt_KhamBoPhan__OnSaveAs()
        {
            if (Utility.DoTrim(txt_KhamBoPhan.Text) == "") return;
            var dmucDchung = new DMUC_DCHUNG(txt_KhamBoPhan.LOAI_DANHMUC);
            dmucDchung.SetStatus(true, txt_KhamBoPhan.Text);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txt_KhamBoPhan.myCode;
                txt_KhamBoPhan.Init();
                txt_KhamBoPhan.SetCode(oldCode);
                txt_KhamBoPhan.Focus();
            }
        }

        private void txt_KhamBoPhan__OnShowData()
        {
            var dmucDchung = new DMUC_DCHUNG(txt_KhamBoPhan.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = txt_KhamBoPhan.myCode;
                txt_KhamBoPhan.Init();
                txt_KhamBoPhan.SetCode(oldCode);
                txt_KhamBoPhan.Focus();
            }
        }
       
        private void frm_KCBCoban_Load(object sender, EventArgs e)
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
            m_dtData = SPs.KcbLaydanhsachphieuKCBcoban(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham).GetDataSet().Tables[0];

            Utility.SetDataSourceForDataGridEx(grdLichSu, m_dtData, true, true, "1=1", "ngay_kham desc");
        }
        KcbPhieukhamtoanthan objphieukham = null;
        private void FillData()
        {
            try
            {
                if (objphieukham != null)
                {

                    txtID.Text = objphieukham.Id.ToString();
                    txt_KhamToanThan._Text = objphieukham.ToanThan;
                    txt_KhamBoPhan._Text = objphieukham.BoPhan;
                    txtNhietDo.Text = objphieukham.NhietDo;
                    txtHa.Text = objphieukham.NhomMau;
                    txtNhipTim.Text = objphieukham.Mach;
                    txtNhipTho.Text = objphieukham.NhịpTho;
                    txtChieucao.Text = objphieukham.ChieuCao;
                    txtCannang.Text = objphieukham.CanNang;
                    txtBmi.Text = objphieukham.Bmi;
                    txtNhommau.SetCode(objphieukham.NhomMau);
                    txtToanThan_DangDi._Text = objphieukham.DangDi;
                    txtToanThan_TinhThan._Text = objphieukham.TinhThan;
                    txtToanThan_HinhDangChung._Text = objphieukham.HinhdangChung;
                    txtToanThan_MauSacDa._Text = objphieukham.MausacDa;
                    txtToanThan_TinhTrangDa._Text = objphieukham.TinhtrangDa;
                    txtToanThan_HeThongLongToc._Text = objphieukham.HethongLongtoc;
                    txtBoPhan_NghiCoBenh._Text = objphieukham.NghiCobenh;
                    txtBoPhan_Dau._Text = objphieukham.BophanDau;
                    txtBoPhan_Co._Text = objphieukham.BophanCo;
                    txtBoPhan_Nguc._Text = objphieukham.BophanNguc;
                    txtBoPhan_Bung._Text = objphieukham.BophanBung;
                    txtBoPhan_CotSong._Text = objphieukham.BophanCotsong;
                    txtBoPhan_ChatThai._Text = objphieukham.BophanChatthai;
                    txt_KhamBoPhan._Text = objphieukham.BoPhan;
                    txt_KhamToanThan._Text = objphieukham.ToanThan;
                    txtBacsi.SetId(objphieukham.IdBacsi);
                    //dtNgayKham.Value = Convert.ToDateTime(string.IsNullOrEmpty(objphieukham.NgayKham) ? dtNgayKham.Value : objphieukham.NgayKham);
                    dtNgayKham.Value = string.IsNullOrEmpty(objphieukham.NgayKham.ToString()) ? dtNgayKham.Value : Convert.ToDateTime(objphieukham.NgayKham);
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
           DataTable dtData= THU_VIEN_CHUNG.LayDulieuDanhmucChung(new List<string>() { txtNhommau.LOAI_DANHMUC,txt_KhamToanThan.LOAI_DANHMUC,txtBoPhan_Bung.LOAI_DANHMUC,txtBoPhan_Co.LOAI_DANHMUC,txtBoPhan_CotSong.LOAI_DANHMUC
            ,txtBoPhan_ChatThai.LOAI_DANHMUC,txtBoPhan_Dau.LOAI_DANHMUC,txtBoPhan_NghiCoBenh.LOAI_DANHMUC,txtBoPhan_Nguc.LOAI_DANHMUC,txtToanThan_DangDi.LOAI_DANHMUC,txtToanThan_HeThongLongToc.LOAI_DANHMUC,
            txtToanThan_HinhDangChung.LOAI_DANHMUC,txtToanThan_MauSacDa.LOAI_DANHMUC,txtToanThan_TinhThan.LOAI_DANHMUC,txtToanThan_TinhTrangDa.LOAI_DANHMUC,txt_KhamBoPhan.LOAI_DANHMUC},true);
           txt_KhamBoPhan.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txt_KhamBoPhan.LOAI_DANHMUC));
            txtNhommau.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txtNhommau.LOAI_DANHMUC));
           txt_KhamToanThan.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txt_KhamToanThan.LOAI_DANHMUC));
           txtBoPhan_Bung.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txtBoPhan_Bung.LOAI_DANHMUC));
           txtBoPhan_Co.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txtBoPhan_Co.LOAI_DANHMUC));
           txtBoPhan_CotSong.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txtBoPhan_CotSong.LOAI_DANHMUC));
           txtToanThan_HinhDangChung.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txtToanThan_HinhDangChung.LOAI_DANHMUC));
           txtToanThan_MauSacDa.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txtToanThan_MauSacDa.LOAI_DANHMUC));
           txtToanThan_TinhThan.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txtToanThan_TinhThan.LOAI_DANHMUC));
           txtToanThan_TinhTrangDa.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txtToanThan_TinhTrangDa.LOAI_DANHMUC));
           txtBoPhan_ChatThai.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txtBoPhan_ChatThai.LOAI_DANHMUC));
           txtBoPhan_Dau.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txtBoPhan_Dau.LOAI_DANHMUC));
           txtBoPhan_NghiCoBenh.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txtBoPhan_NghiCoBenh.LOAI_DANHMUC));
           txtBoPhan_Nguc.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txtBoPhan_Nguc.LOAI_DANHMUC));

           txtToanThan_DangDi.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txtToanThan_DangDi.LOAI_DANHMUC));
           txtToanThan_HeThongLongToc.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txtToanThan_HeThongLongToc.LOAI_DANHMUC));


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
                    decimal value2Conpare = Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(txtNhipTim.Text), -1);
                    List<string> lstRange = Utility.Laygiatrithamsohethong("MACH", "5-70", true).Split('-').ToList<string>();
                    if (Utility.DoTrim(txtNhipTim.Text).Length > 0 && value2Conpare < Utility.DecimaltoDbnull(lstRange[0]) || value2Conpare > Utility.DecimaltoDbnull(lstRange[1]))
                    {
                        Utility.ShowMsg(string.Format("Thông tin Mạch có thể chưa chuẩn xác. Hệ thống đang xác lập mức cho phép từ {0}-{1}. Ngoài mức này sẽ cảnh báo nhưng vẫn cho lưu. Vui lòng kiểm tra lại", lstRange[0], lstRange[1]), "Cảnh báo");
                        txtNhipTim.Focus();
                    }
                    value2Conpare = Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(txtNhietDo.Text), -1);
                    lstRange = Utility.Laygiatrithamsohethong("NHIETDO", "34-43", true).Split('-').ToList<string>();
                    if (Utility.DoTrim(txtNhietDo.Text).Length > 0 && value2Conpare < Utility.DecimaltoDbnull(lstRange[0]) || value2Conpare > Utility.DecimaltoDbnull(lstRange[1]))
                    {
                        Utility.ShowMsg(string.Format("Thông tin Nhiệt độ có thể chưa chuẩn xác. Hệ thống đang xác lập mức cho phép từ {0}-{1}. Ngoài mức này sẽ cảnh báo nhưng vẫn cho lưu. Vui lòng kiểm tra lại", lstRange[0], lstRange[1]), "Cảnh báo");
                        txtNhietDo.Focus();
                    }
                    value2Conpare = Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(txtHa.Text), -1);
                    lstRange = Utility.Laygiatrithamsohethong("HUYETAP", "40-250", true).Split('-').ToList<string>();
                    if (Utility.DoTrim(txtHa.Text).Length > 0 && value2Conpare < Utility.DecimaltoDbnull(lstRange[0]) || value2Conpare > Utility.DecimaltoDbnull(lstRange[1]))
                    {
                        Utility.ShowMsg(string.Format("Thông tin Huyết áp có thể chưa chuẩn xác. Hệ thống đang xác lập mức cho phép từ {0}-{1}. Ngoài mức này sẽ cảnh báo nhưng vẫn cho lưu. Vui lòng kiểm tra lại", lstRange[0], lstRange[1]), "Cảnh báo");
                        txtHa.Focus();
                    }
                    value2Conpare = Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(txtNhipTho.Text), -1);
                    lstRange = Utility.Laygiatrithamsohethong("NHIPTHO", "40-250", true).Split('-').ToList<string>();
                    if (Utility.DoTrim(txtNhipTho.Text).Length > 0 && value2Conpare < Utility.DecimaltoDbnull(lstRange[0]) || value2Conpare > Utility.DecimaltoDbnull(lstRange[1]))
                    {
                        Utility.ShowMsg(string.Format("Thông tin Nhịp thở có thể chưa chuẩn xác. Hệ thống đang xác lập mức cho phép từ {0}-{1}. Ngoài mức này sẽ cảnh báo nhưng vẫn cho lưu. Vui lòng kiểm tra lại", lstRange[0], lstRange[1]), "Cảnh báo");
                        txtNhipTho.Focus();
                    }
                    value2Conpare = Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(txtChieucao.Text), -1);
                    lstRange = Utility.Laygiatrithamsohethong("CHIEUCAO", "10-250", true).Split('-').ToList<string>();
                    if (Utility.DoTrim(txtChieucao.Text).Length > 0 && value2Conpare < Utility.DecimaltoDbnull(lstRange[0]) || value2Conpare > Utility.DecimaltoDbnull(lstRange[1]))
                    {
                        Utility.ShowMsg(string.Format("Thông tin Chiều cao có thể chưa chuẩn xác. Hệ thống đang xác lập mức cho phép chiều cao từ {0}(cm)-{1}(cm). Ngoài mức này sẽ cảnh báo nhưng vẫn cho lưu. Vui lòng kiểm tra lại", lstRange[0], lstRange[1]), "Cảnh báo");
                        txtChieucao.Focus();
                    }
                    value2Conpare = Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(txtCannang.Text), -1);
                    lstRange = Utility.Laygiatrithamsohethong("CANNANG", "1-150", true).Split('-').ToList<string>();
                    if (Utility.DoTrim(txtCannang.Text).Length > 0 && value2Conpare < Utility.DecimaltoDbnull(lstRange[0]) || value2Conpare > Utility.DecimaltoDbnull(lstRange[1]))
                    {
                        Utility.ShowMsg(string.Format("Thông tin Cân nặng có thể chưa chuẩn xác. Hệ thống đang xác lập mức cân nặng từ {0}(kg)-{1}(kg). Ngoài mức này sẽ cảnh báo nhưng vẫn cho lưu. Vui lòng kiểm tra lại", lstRange[0], lstRange[1]), "Cảnh báo");
                        txtCannang.Focus();
                    }
                    value2Conpare = Utility.DecimaltoDbnull(Utility.chuanhoaDecimal(txtNhipTim.Text), -1);
                    lstRange = Utility.Laygiatrithamsohethong("NHIPTIM", "40-130", true).Split('-').ToList<string>();
                    if (Utility.DoTrim(txtNhipTim.Text).Length > 0 && value2Conpare < Utility.DecimaltoDbnull(lstRange[0]) || value2Conpare > Utility.DecimaltoDbnull(lstRange[1]))
                    {
                        Utility.ShowMsg(string.Format("Thông tin Nhịp tim có thể chưa chuẩn xác. Hệ thống đang xác lập mức cho phép từ {0}(kg)-{1}(kg). Ngoài mức này sẽ cảnh báo nhưng vẫn cho lưu. Vui lòng kiểm tra lại", lstRange[0], lstRange[1]), "Cảnh báo");
                        txtNhipTim.Focus();
                    }
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
            if (Utility.DecimaltoDbnull(txtChieucao.Text, 0) > 0 && Utility.DecimaltoDbnull(txtChieucao.Text, 0) > 0)
            {
                if (!string.IsNullOrEmpty(txtCannang.Text) && !string.IsNullOrEmpty(txtChieucao.Text))
                {
                    decimal cannang = Utility.DecimaltoDbnull(txtCannang.Text);
                    decimal chieucao = Utility.DecimaltoDbnull(txtChieucao.Text);
                    decimal bmi = Utility.DecimaltoDbnull(cannang / ((chieucao / 100) * (chieucao / 100)));
                    txtBmi.Text = bmi.ToString("0.00").Replace(".00", String.Empty);
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
                if (objphieukham != null && objphieukham.Id>0)
                {
                    objphieukham.MarkOld();
                    objphieukham.NguoiSua = globalVariables.UserName;
                    objphieukham.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
                }
                else
                {
                    objphieukham = new KcbPhieukhamtoanthan();
                    objphieukham.IsNew = true;
                    objphieukham.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham);
                    objphieukham.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan);
                    objphieukham.NgayKham = dtNgayKham.Value.Date;
                    objphieukham.NguoiTao = globalVariables.UserName;
                    objphieukham.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
                }


                objphieukham.NhomMau = txtNhommau.myCode;

                objphieukham.DangDi = Utility.sDbnull(txtToanThan_DangDi.Text);
                objphieukham.TinhThan = Utility.sDbnull(txtToanThan_TinhThan.Text);
                objphieukham.HinhdangChung = Utility.sDbnull(txtToanThan_HinhDangChung.Text);
                objphieukham.MausacDa = Utility.sDbnull(txtToanThan_MauSacDa.Text);
                objphieukham.TinhtrangDa = Utility.sDbnull(txtToanThan_TinhTrangDa.Text);
                objphieukham.HethongLongtoc = Utility.sDbnull(txtToanThan_HeThongLongToc.Text);
                objphieukham.NghiCobenh = Utility.sDbnull(txtBoPhan_NghiCoBenh.Text);
                objphieukham.BophanDau = Utility.sDbnull(txtBoPhan_Dau.Text);
                objphieukham.BophanCo = Utility.sDbnull(txtBoPhan_Co.Text);
                objphieukham.BophanNguc = Utility.sDbnull(txtBoPhan_Nguc.Text);
                objphieukham.BophanBung = Utility.sDbnull(txtBoPhan_Bung.Text);
                objphieukham.BophanCotsong = Utility.sDbnull(txtBoPhan_CotSong.Text);
                objphieukham.BophanChatthai = Utility.sDbnull(txtBoPhan_ChatThai.Text);
                objphieukham.IdBacsi = Utility.Int16Dbnull(txtBacsi.MyID, -1);
                objphieukham.BoPhan = Utility.sDbnull(txt_KhamBoPhan.Text);
                objphieukham.ToanThan = Utility.sDbnull(txt_KhamToanThan.Text);
                objphieukham.HuyetAp = txtHa.Text;
                objphieukham.NhietDo = txtNhietDo.Text;
                objphieukham.Mach = Utility.sDbnull(txtNhipTim.Text);
                objphieukham.NhịpTho = Utility.sDbnull(txtNhipTho.Text);
                objphieukham.ChieuCao = Utility.sDbnull(txtChieucao.Text);
                objphieukham.CanNang = Utility.sDbnull(txtCannang.Text);
                objphieukham.Bmi = Utility.sDbnull(txtBmi.Text);
                objphieukham.MotaThem = "";
                objphieukham.Save();
                Utility.ShowMsg("Bạn đã lưu thông tin khám cơ bản thành công. Nhấn nút OK để kết thúc");
                LoadLichSu();
                Utility.GonewRowJanus(grdLichSu, "Id", objphieukham.Id.ToString());
            }
            catch (Exception exception)
            {
                Utility.CatchException(string.Format("Lỗi trong quá trình Lưu thông tin khám cơ bản"), exception);
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
    }
}
