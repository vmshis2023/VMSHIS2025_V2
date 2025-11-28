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
    public partial class uc_PhieuChamSoc : UserControl
    {
        public delegate void OnMsg(string msg, bool IsSucess = false);
        public event OnMsg _OnMsg;
        public delegate void OnAction(bool AllowSave);
        public event OnAction _OnAction;
        public EmrPhieuchamsoc _phieu;
        KcbLuotkham objLuotkham;
        public int id_bacsikham = -1;
        public bool Force2Saved = false;
        action m_enAct = action.FirstOrFinished;
        action m_enAct_CD = action.FirstOrFinished;
        bool AllowedChanged = false;
        bool AllowedChanged_ChanDoanMucTieu = false;
        bool isInit = false;
        string msg = "";
        int num = 0;
        public uc_PhieuChamSoc()
        {
            InitializeComponent();
            txtCanNang.TextChanged += txtCanNang_TextChanged;
            txtChieuCao.TextChanged += txtChieuCao_TextChanged;
            //grdChamsoc.MouseDoubleClick += GrdChamsoc_MouseDoubleClick;
            grdChamsoc.ColumnButtonClick += GrdChamsoc_ColumnButtonClick;
            grdChamsoc.SelectionChanged += GrdChamsoc_SelectionChanged;
            txt_toan_than_da_niem_mac._OnShowDataV1 += _OnShowDataV1;
            txt_toan_than_tri_giac._OnShowDataV1 += _OnShowDataV1;
            txt_toan_than_khac._OnShowDataV1 += _OnShowDataV1;

            txt_ho_hap._OnShowDataV1 += _OnShowDataV1;
            txt_tuan_hoan._OnShowDataV1 += _OnShowDataV1;
            txt_dinh_duong._OnShowDataV1 += _OnShowDataV1;

            txt_giac_ngu._OnShowDataV1 += _OnShowDataV1;
            txt_ve_sinh_ca_nhan._OnShowDataV1 += _OnShowDataV1;
            txt_tinh_than._OnShowDataV1 += _OnShowDataV1;

            txt_van_dong_phcn._OnShowDataV1 += _OnShowDataV1;
            txt_gdsk._OnShowDataV1 += _OnShowDataV1;
            txt_theo_doi_khac_dau._OnShowDataV1 += _OnShowDataV1;

            txt_theo_doi_khac_loet._OnShowDataV1 += _OnShowDataV1;
            txt_theo_doi_khac_nguy_co_nga._OnShowDataV1 += _OnShowDataV1;
            txt_theo_doi_khac_canh_bao_som._OnShowDataV1 += _OnShowDataV1;

            txt_thuc_hien_theo_chi_dinh_cls._OnShowDataV1 += _OnShowDataV1;
            txt_thuc_hien_thuoc_theo_chi_dinh._OnShowDataV1 += _OnShowDataV1;
            txt_cham_soc_dieu_duong._OnShowDataV1 += _OnShowDataV1;

            txt_tu_van_giao_duc_suc_khoe._OnShowDataV1 += _OnShowDataV1;
            grd_chandoan_muctieu.SelectionChanged += Grd_chandoan_muctieu_SelectionChanged;
            grd_chandoan_muctieu.ColumnButtonClick += Grd_chandoan_muctieu_ColumnButtonClick;
        }

        private void Grd_chandoan_muctieu_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            if (e.Column.Key == "XOA")
            {
                if (!Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn xóa thông tin chẩn đoán và mục tiêu đang chọn hay không?"), "Xác nhận xóa", true))
                {
                    return;
                }
                long id_cd = Utility.Int64Dbnull(grd_chandoan_muctieu.GetValue("id_chandoan"));
                num = new Delete().From(EmrPhieuchamsocChandoan.Schema).Where(EmrPhieuchamsocChandoan.Columns.IdChandoan).IsEqualTo(id_cd).Execute();
                if (num > 0)
                {
                    grd_chandoan_muctieu.CurrentRow.Delete();
                    dtChanDoanMucTieu.AcceptChanges();
                }
            }
        }
        EmrPhieuchamsocChandoan _phieuchandoanmuctieu;
        private void Grd_chandoan_muctieu_SelectionChanged(object sender, EventArgs e)
        {
            if (!AllowedChanged_ChanDoanMucTieu || !Utility.isValidGrid(grd_chandoan_muctieu)) return;
            long id_cd = Utility.Int64Dbnull(grd_chandoan_muctieu.GetValue("id_chandoan"));
            _phieuchandoanmuctieu = EmrPhieuchamsocChandoan.FetchByID(id_cd);
            if(_phieuchandoanmuctieu!=null)
            {
                nmr_stt_chandoan.Value = Utility.DecimaltoDbnull(_phieuchandoanmuctieu.SttChandoan);
                txt_chandoan.Text = _phieuchandoanmuctieu.ChanDoan;
                txt_muctieu_1.Text = _phieuchandoanmuctieu.MucTieu1;
                txt_muctieu_2.Text = _phieuchandoanmuctieu.MucTieu2;
            }    
        }

        private void _OnShowDataV1(AutoCompleteTextbox_Danhmucchung obj)
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

        public void Reset()
        {
          if(dtChanDoanMucTieu!=null)  dtChanDoanMucTieu.Clear();
            if (dtPhieuChamSoc!=null)  dtPhieuChamSoc.Clear();
            EnableControl(grb_ThongtinChamSoc, false, true);
        }
        private void GrdChamsoc_SelectionChanged(object sender, EventArgs e)
        {
            if (!AllowedChanged || !Utility.isValidGrid(grdChamsoc)) return;
            FillData4Update();
        }
        private void GrdChamsoc_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            try
            {
                if (e.Column.Key == "XOA")
                {
                    if (!Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn xóa khung thông tin chăm sóc lúc {0} không?", Utility.sDbnull(grdChamsoc.GetValue("sngay_thuchien"))), "Xác nhận xóa", true))
                    {
                        return;
                    }
                    long id_phieu = Utility.Int64Dbnull(grdChamsoc.GetValue("id_phieu"));
                    num = new Delete().From(EmrPhieuchamsoc.Schema).Where(EmrPhieuchamsoc.Columns.IdPhieu).IsEqualTo(id_phieu).Execute();
                    if (num > 0)
                    {
                        grdChamsoc.CurrentRow.Delete();
                        dtPhieuChamSoc.AcceptChanges();
                    }
                }
            }
            catch (Exception ex)
            {

            }
            
        }

        private void GrdChamsoc_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!AllowedChanged || !Utility.isValidGrid(grdChamsoc)) return;
            FillData4Update();
            //if (_OnAction != null) _OnAction(true);
        }
        #region Thêm Sửa Xóa
        public void Huythaotac()
        {
            m_enAct = action.FirstOrFinished;
            SetControlStatus();
        }
        public void Sua()
        {
            m_enAct = action.Update;
            FillData4Update();
            SetControlStatus();
        }
        public void Themmoi()
        {
            if (objLuotkham == null)
            {
                msg = "Bạn cần chọn người bệnh trước khi thực hiện thêm mới thông tin phiếu chăm sóc";
                if (_OnMsg != null) _OnMsg(msg,false) ;
                return;
            }
            if (objLuotkham.TrangthaiNoitru >= 6)
            {
                msg = "Bệnh nhân đã thanh toán ra viện nên bạn không thể nhập chẩn đoán";
                if (_OnMsg != null) _OnMsg(msg, false);
                return;
            }
            m_enAct = action.Insert;
            _phieu = null;
            AllowedChanged = true;
            SetControlStatus();
        }
        private void SetControlStatus()
        {
            try
            {
               
              
                switch (m_enAct)
                {
                    case action.Insert:
                        grdChamsoc.Enabled = false;
                        dtp_ngay_thuchien.Enabled = true;
                        dtp_ngay_thuchien.Value = globalVariables.SysDate;
                        EnableControl(grb_ThongtinChamSoc, true, true);
                        //--------------------------------------------------------------
                        //Không cho phép chọn trên lưới dữ liệu được fill vào các Control
                        AllowedChanged = false;
                        //Tự động Focus đến mục ID để người dùng nhập liệu
                        dtp_ngay_thuchien.Focus();
                        break;
                    case action.Update:
                        EnableControl(grb_ThongtinChamSoc, true, false);
                        //--------------------------------------------------------------
                        //Không cho phép chọn trên lưới dữ liệu được fill vào các Control
                        AllowedChanged = false;
                        //Tự động Focus đến mục Position để người dùng nhập liệu
                        dtp_ngay_thuchien.Focus();
                        break;
                    case action.FirstOrFinished://Hủy hoặc trạng thái ban đầu khi mới hiển thị Form

                        grdChamsoc.Enabled = true;
                        EnableControl(grb_ThongtinChamSoc, false, false);
                        AllowedChanged = true;
                        //Tự động chọn dòng hiện tại trên lưới để hiển thị lại trên Control
                        GrdChamsoc_SelectionChanged(grdChamsoc, new EventArgs());
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
        #endregion
       

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
        public void Init()
        {
            dtp_ngay_thuchien.Value = globalVariables.SysDate;
            DataTable dtData = THU_VIEN_CHUNG.LayDulieuDanhmucChung(new List<string>() { txt_toan_than_da_niem_mac.LOAI_DANHMUC, txt_toan_than_tri_giac.LOAI_DANHMUC
                , txt_toan_than_khac.LOAI_DANHMUC, txt_ho_hap.LOAI_DANHMUC,txt_tuan_hoan.LOAI_DANHMUC,txt_dinh_duong.LOAI_DANHMUC
            , txt_giac_ngu.LOAI_DANHMUC, txt_ve_sinh_ca_nhan.LOAI_DANHMUC,txt_tinh_than.LOAI_DANHMUC,txt_van_dong_phcn.LOAI_DANHMUC
            , txt_gdsk.LOAI_DANHMUC, txt_theo_doi_khac_dau.LOAI_DANHMUC,txt_theo_doi_khac_loet.LOAI_DANHMUC,txt_theo_doi_khac_nguy_co_nga.LOAI_DANHMUC
            , txt_theo_doi_khac_canh_bao_som.LOAI_DANHMUC, txt_thuc_hien_theo_chi_dinh_cls.LOAI_DANHMUC,txt_thuc_hien_thuoc_theo_chi_dinh.LOAI_DANHMUC
            ,txt_cham_soc_dieu_duong.LOAI_DANHMUC,txt_tu_van_giao_duc_suc_khoe.LOAI_DANHMUC,txt_chandoan.LOAI_DANHMUC,txt_muctieu_1.LOAI_DANHMUC}, true);
           
            txt_toan_than_da_niem_mac.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txt_toan_than_da_niem_mac.LOAI_DANHMUC));
            txt_toan_than_tri_giac.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txt_toan_than_tri_giac.LOAI_DANHMUC));
            txt_toan_than_khac.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txt_toan_than_khac.LOAI_DANHMUC));

            txt_ho_hap.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txt_ho_hap.LOAI_DANHMUC));
            txt_tuan_hoan.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txt_tuan_hoan.LOAI_DANHMUC));
            txt_dinh_duong.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txt_dinh_duong.LOAI_DANHMUC));

            txt_giac_ngu.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txt_giac_ngu.LOAI_DANHMUC));
            txt_ve_sinh_ca_nhan.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txt_ve_sinh_ca_nhan.LOAI_DANHMUC));
            txt_tinh_than.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txt_tinh_than.LOAI_DANHMUC));

            txt_van_dong_phcn.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txt_van_dong_phcn.LOAI_DANHMUC));
            txt_gdsk.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txt_gdsk.LOAI_DANHMUC));
            txt_theo_doi_khac_dau.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txt_theo_doi_khac_dau.LOAI_DANHMUC));

            txt_theo_doi_khac_loet.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txt_theo_doi_khac_loet.LOAI_DANHMUC));
            txt_theo_doi_khac_nguy_co_nga.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txt_theo_doi_khac_nguy_co_nga.LOAI_DANHMUC));
            txt_theo_doi_khac_canh_bao_som.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txt_theo_doi_khac_canh_bao_som.LOAI_DANHMUC));

            txt_thuc_hien_theo_chi_dinh_cls.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txt_thuc_hien_theo_chi_dinh_cls.LOAI_DANHMUC));
            txt_thuc_hien_thuoc_theo_chi_dinh.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txt_thuc_hien_thuoc_theo_chi_dinh.LOAI_DANHMUC));
            txt_cham_soc_dieu_duong.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txt_cham_soc_dieu_duong.LOAI_DANHMUC));

            txt_tu_van_giao_duc_suc_khoe.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txt_tu_van_giao_duc_suc_khoe.LOAI_DANHMUC));

            txt_chandoan.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txt_chandoan.LOAI_DANHMUC));
            txt_muctieu_1.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txt_muctieu_1.LOAI_DANHMUC));
            txt_muctieu_2.Init(THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, txt_muctieu_1.LOAI_DANHMUC));

           // DataTable dtKhoaPhong = THU_VIEN_CHUNG.Laydanhmuckhoa("ALL", 0);
            DataTable dtKhoaPhong = Utility.ExecuteSql("select id_khoaphong, ma_khoaphong, ten_khoaphong from dmuc_khoaphong", CommandType.Text).Tables[0];
            DataTable dtNhanVien = Utility.ExecuteSql("select id_nhanvien,ma_nhanvien,ten_nhanvien from dmuc_nhanvien order by ten_nhanvien", CommandType.Text).Tables[0];
            DataBinding.BindDataCombobox(cbo_khoa, dtKhoaPhong, DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong, "----Chọn----", true);
            DataBinding.BindDataCombobox(cbo_Yta, dtNhanVien, DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.TenNhanvien, "----Chọn----", true);
            isInit = true;
           
        }
        public void Init(KcbLuotkham objLuotkham, EmrPhieuchamsoc _phieu)
        {
           
            this.objLuotkham = objLuotkham;
            this._phieu = _phieu;
            if (!isInit)
                Init();
            FillData4Update();
        }


        public void Init(KcbLuotkham objLuotkham)
        {
            if (!isInit)
                Init();
            this.objLuotkham = objLuotkham;
            GetData();
            SetControlStatus();
            SetControlStatus_ChanDoanMucTieu();
        }
        void ModifyButtons()
        {
            cmd_them_chandoan_muctieu.Enabled = true;
            cmd_sua_chandoan_muctieu.Enabled = dtChanDoanMucTieu != null && dtChanDoanMucTieu.Rows.Count > 0;
        }
        //public void HandleKeyEnter()
        //{
        //    var active = Utility.getActiveControl(this);
        //    if (active == null) return;

        //    bool isMultiline = false;

        //    if (active is TextBox txt)
        //        isMultiline = txt.Multiline;

        //    if (active is EditBox ed)
        //        isMultiline = ed.Multiline;

        //    if (isMultiline)
        //        return;

        //    // nhảy tới control tiếp theo theo TabIndex TOÀN CỤC (tự hiểu container)
        //    this.SelectNextControl(active, forward: true, tabStopOnly: true, nested: true, wrap: true);
        //}
        //public void HandleKeyEnter(Control activeCtrl)
        //{
        //    if (activeCtrl == null) return;

        //    bool isMultiline =
        //        (activeCtrl is TextBox txt && txt.Multiline) ||
        //        (activeCtrl is EditBox ed && ed.Multiline);

        //    if (isMultiline)
        //        return;

        //    // nhảy đúng control theo TabIndex TOÀN DỰ ÁN
        //    this.Parent.SelectNextControl(activeCtrl, true, true, true, true);
        //}
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
                    ProcessTabKey(true);
            }
            else if (activeCtrl.GetType().Equals(typeof(TextBox)))
            {
                TextBox box = activeCtrl as TextBox;
                if (box.Multiline)
                {
                    return;
                }
                else
                    ProcessTabKey(true); //SendKeys.Send("{TAB}");
            }
            else
                ProcessTabKey(true); // SendKeys.Send("{TAB}");
        }

        public DataTable dtPhieuChamSoc = new DataTable();
         DataTable dtChanDoanMucTieu = new DataTable();
        public void GetData()
        {
            DataSet dsData= SPs.EmrPhieuchamsocLaydanhsach(-1, -1, objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham).GetDataSet();
            dtPhieuChamSoc = dsData.Tables[0];
            dtChanDoanMucTieu= dsData.Tables[1];
            Utility.SetDataSourceForDataGridEx(grdChamsoc, dtPhieuChamSoc, true, true, "1=1", "ngay_thuchien");
            Utility.SetDataSourceForDataGridEx(grd_chandoan_muctieu, dtChanDoanMucTieu, true, true, "1=1", "stt_chandoan");
            AllowedChanged = true;
            AllowedChanged_ChanDoanMucTieu = true;
            GrdChamsoc_SelectionChanged(grdChamsoc, new EventArgs());
            Grd_chandoan_muctieu_SelectionChanged(grd_chandoan_muctieu, new EventArgs());
            ModifyButtons();
        }
        public void FillData4Update()
        {
            try
            {

                long id_phieu = Utility.Int64Dbnull(grdChamsoc.GetValue("id_phieu"));
                _phieu = new Select().From(EmrPhieuchamsoc.Schema)
                        .Where(EmrPhieuchamsoc.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        .And(EmrPhieuchamsoc.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .And(EmrPhieuchamsoc.Columns.IdPhieu).IsEqualTo(id_phieu)
                        .ExecuteSingle<EmrPhieuchamsoc>();

                txtId.Text = "";
                if (_phieu != null)
                {
                    txtId.Text = _phieu.IdPhieu.ToString();
                    dtp_ngay_thuchien.Value = _phieu.NgayThuchien;
                    cbo_khoa.SelectedValue =Utility.Int32Dbnull( _phieu.IdKhoa);
                    cbo_Yta.SelectedValue= Utility.Int32Dbnull(_phieu.IdNhanvien);
                    txtMach.Text=Utility.sDbnull(_phieu.Mach);
                    txtNhietDo.Text = Utility.sDbnull(_phieu.NhietDo);
                    txtha.Text = Utility.sDbnull(_phieu.HuyetAp);
                    txtNhipTho.Text = Utility.sDbnull(_phieu.NhipTho);
                    txtCanNang.Text = Utility.sDbnull(_phieu.CanNang);
                    txtChieuCao.Text = Utility.sDbnull(_phieu.ChieuCao);
                    txtMach.Text = Utility.sDbnull(_phieu.Mach);
                    txt_SPO2.Text = Utility.sDbnull(_phieu.SPO2);
                    tinhBMI();

                    txt_toan_than_da_niem_mac._Text = Utility.sDbnull(_phieu.ToanThanDaNiemMac);
                    txt_toan_than_tri_giac._Text = Utility.sDbnull(_phieu.ToanThanTriGiac);
                    txt_toan_than_khac._Text = Utility.sDbnull(_phieu.ToanThanKhac);

                    txt_ho_hap._Text = Utility.sDbnull(_phieu.HoHap);
                    txt_tuan_hoan._Text = Utility.sDbnull(_phieu.TuanHoan);
                    txt_dinh_duong._Text = Utility.sDbnull(_phieu.DinhDuong);

                    txt_giac_ngu._Text = Utility.sDbnull(_phieu.GiacNgu);
                    txt_ve_sinh_ca_nhan._Text = Utility.sDbnull(_phieu.VeSinhCaNhan);
                    txt_tinh_than._Text = Utility.sDbnull(_phieu.TinhThan);

                    txt_van_dong_phcn._Text = Utility.sDbnull(_phieu.VanDongPhcn);
                    txt_gdsk._Text = Utility.sDbnull(_phieu.Gdsk);
                    txt_theo_doi_khac_dau._Text = Utility.sDbnull(_phieu.TheoDoiKhacDau);

                    txt_theo_doi_khac_loet._Text = Utility.sDbnull(_phieu.TheoDoiKhacLoet);
                    txt_theo_doi_khac_nguy_co_nga._Text = Utility.sDbnull(_phieu.TheoDoiKhacNguyCoNga);
                    txt_theo_doi_khac_canh_bao_som._Text = Utility.sDbnull(_phieu.TheoDoiKhacCanhBaoSom);

                    txt_thuc_hien_theo_chi_dinh_cls._Text = Utility.sDbnull(_phieu.ThucHienTheoChiDinhCls);
                    txt_thuc_hien_thuoc_theo_chi_dinh._Text = Utility.sDbnull(_phieu.ThucHienThuocTheoChiDinh);
                    txt_cham_soc_dieu_duong._Text = Utility.sDbnull(_phieu.ChamSocDieuDuong);
                    txt_tu_van_giao_duc_suc_khoe._Text = Utility.sDbnull(_phieu.TuVanGiaoDucSucKhoe);

                }
                else
                {
                    EnableControl(grb_ThongtinChamSoc, false, true);

                }
                
            }
            catch (System.Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        public void EnableControl(Control _parent,bool isEnable,bool isClear)
        {
            try
            {
                foreach (Control ctr in _parent.Controls)
                {
                    if (ctr.GetType().Equals(txt_giac_ngu.GetType()) || ctr.GetType().Equals(txt_SPO2.GetType()))
                    {
                        ctr.Enabled = isEnable;
                        if (isClear)
                        {
                            if (ctr.GetType().Equals(txt_giac_ngu.GetType()))
                                ((AutoCompleteTextbox_Danhmucchung)ctr).SetDefaultItem();
                            else if (ctr is EditBox)
                            {
                                ((EditBox)(ctr)).Clear();
                            }
                            else if (ctr is TextBox)
                            {
                                ((EditBox)(ctr)).Clear();
                            }
                        }
                    }
                    if (ctr.Controls.Count > 0)
                        EnableControl(ctr, isEnable, isClear);
                }
            }
            catch (Exception)
            {
            }

        }
      
        string Msg = "";
        bool isValidData()
        {
            Msg = "";
            
            if (dtp_ngay_thuchien.Text == "")
            {
                Msg = "Phải nhập thời gian chăm sóc";
                if (_OnMsg != null) _OnMsg(Msg, false);
                dtp_ngay_thuchien.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(cbo_khoa.SelectedValue) <= 0)
            {
                Msg = "Bạn phải chọn Khoa thực hiện";
                if (_OnMsg != null) _OnMsg(Msg);
                cbo_khoa.Focus();
                return false;
            }
            if (Utility.Int32Dbnull( cbo_Yta.SelectedValue)<=0)
            {
                Msg = "Bạn phải chọn Y tá/ Điều dưỡng thực hiện";
                if (_OnMsg != null) _OnMsg(Msg);
                cbo_Yta.Focus();
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
                       
                        if (_phieu == null || _phieu.IdPhieu <= 0)
                        {
                            isNew = true;
                            _phieu = new EmrPhieuchamsoc();
                            _phieu.IsNew = true;
                            _phieu.NgayTao = DateTime.Now;
                            _phieu.NguoiTao = globalVariables.UserName;
                        }
                        else
                        {
                            isNew = false;
                            _phieu.IsNew = false;
                            _phieu.MarkOld();
                            _phieu.NgaySua = DateTime.Now;
                            _phieu.NguoiSua = globalVariables.UserName;
                        }
                        _phieu.IdBenhnhan = objLuotkham.IdBenhnhan;
                        _phieu.MaLuotkham = objLuotkham.MaLuotkham;
                        _phieu.NgayThuchien = dtp_ngay_thuchien.Value;
                        _phieu.GioThuchien= dtp_ngay_thuchien.Value.ToString("HH:mm");
                        _phieu.IdKhoa = Utility.Int32Dbnull(cbo_khoa.SelectedValue);
                        _phieu.TenKhoa = cbo_khoa.Text;
                        _phieu.IdNhanvien = Utility.Int32Dbnull(cbo_Yta.SelectedValue);

                        _phieu.Mach = Utility.sDbnull(txtMach.Text);
                        _phieu.NhietDo = Utility.sDbnull(txtNhietDo.Text);
                        _phieu.HuyetAp = Utility.sDbnull(txtha.Text);
                        _phieu.NhipTho = Utility.sDbnull(txtNhipTho.Text);
                        _phieu.CanNang = Utility.sDbnull(txtCanNang.Text);
                        _phieu.ChieuCao = Utility.sDbnull(txtChieuCao.Text);
                        _phieu.SPO2 = Utility.sDbnull(txt_SPO2.Text);
                        _phieu.Bmi = Utility.sDbnull(txtBMI.Text);
                      
                        _phieu.ToanThanDaNiemMac = Utility.sDbnull(txt_toan_than_da_niem_mac.Text);
                        _phieu.ToanThanTriGiac = Utility.sDbnull(txt_toan_than_tri_giac.Text);
                        _phieu.ToanThanKhac = Utility.sDbnull(txt_toan_than_khac.Text);

                        _phieu.HoHap = Utility.sDbnull(txt_ho_hap.Text);
                        _phieu.TuanHoan = Utility.sDbnull(txt_tuan_hoan.Text);
                        _phieu.DinhDuong = Utility.sDbnull(txt_dinh_duong.Text);

                        _phieu.GiacNgu = Utility.sDbnull(txt_giac_ngu.Text);
                        _phieu.VeSinhCaNhan = Utility.sDbnull(txt_ve_sinh_ca_nhan.Text);
                        _phieu.TinhThan = Utility.sDbnull(txt_tinh_than.Text);

                        _phieu.VanDongPhcn = Utility.sDbnull(txt_van_dong_phcn.Text);
                        _phieu.Gdsk = Utility.sDbnull(txt_gdsk.Text);
                        _phieu.TheoDoiKhacDau = Utility.sDbnull(txt_theo_doi_khac_dau.Text);

                        _phieu.TheoDoiKhacLoet = Utility.sDbnull(txt_theo_doi_khac_loet.Text);
                        _phieu.TheoDoiKhacNguyCoNga = Utility.sDbnull(txt_theo_doi_khac_nguy_co_nga.Text);
                        _phieu.TheoDoiKhacCanhBaoSom = Utility.sDbnull(txt_theo_doi_khac_canh_bao_som.Text);

                        _phieu.ThucHienTheoChiDinhCls = Utility.sDbnull(txt_thuc_hien_theo_chi_dinh_cls.Text);
                        _phieu.ThucHienThuocTheoChiDinh = Utility.sDbnull(txt_thuc_hien_thuoc_theo_chi_dinh.Text);
                        _phieu.ChamSocDieuDuong = Utility.sDbnull(txt_cham_soc_dieu_duong.Text);
                        _phieu.TuVanGiaoDucSucKhoe = Utility.sDbnull(txt_tu_van_giao_duc_suc_khoe.Text);

                        _phieu.Save();

                    }
                    scope.Complete();
                }
                txtId.Text = _phieu.IdPhieu.ToString();
                if(m_enAct==action.Insert)
                {
                    DataRow newDr = dtPhieuChamSoc.NewRow();
                    Utility.FromObjectToDatarow(_phieu, ref newDr);
                    newDr["ten_nhanvien"] = cbo_Yta.Text;
                    dtPhieuChamSoc.Rows.Add(newDr);
                    dtPhieuChamSoc.AcceptChanges();
                }
                else
                {
                    DataRow currDr = Utility.getCurrentDataRow(grdChamsoc);
                    if(currDr!=null)
                    {
                        currDr["ten_nhanvien"]= cbo_Yta.Text;
                        currDr["ten_khoa"] = cbo_khoa.Text;
                        currDr["ngay_thuchien"] = dtp_ngay_thuchien.Value;
                        currDr["gio_thuchien"] = dtp_ngay_thuchien.Value.ToString("HH:mm");
                        currDr["ngay_sua"] = _phieu.NgaySua;
                        currDr["nguoi_sua"] = _phieu.NguoiSua;
                        dtPhieuChamSoc.AcceptChanges();
                    }    
                }    
                m_enAct = action.FirstOrFinished;

                SetControlStatus();
                if (_OnAction != null) _OnAction(true);
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
                _phieu = new Select().From(EmrPhieuchamsoc.Schema)
                       .Where(EmrPhieuchamsoc.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                       .And(EmrPhieuchamsoc.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                       .ExecuteSingle<EmrPhieuchamsoc>();
                if (_phieu.IdPhieu <= 0)
                {
                    Utility.ShowMsg("Bạn cần lưu thông tin Phiếu chăm sóc trước khi thực hiện in phiếu");
                    return;
                }
                DataTable dtData = SPs.EmrPhieuchamsocLaydulieuinphieu(_phieu.MaLuotkham, _phieu.IdBenhnhan, _phieu.IdPhieu).GetDataSet().Tables[0];
                dtData.TableName = "PHIEU_CHAMSOC_CAP_2_3";
                //dtData.Rows[0]["sngaygio_nhapvien"] = PDT != null ? Utility.FormatDateTime_giophut_ngay_thang_nam(PDT.NgayVaovien, "") : ".......... giờ ....... ngày ........./........./.............";
                //dtData.Rows[0]["sngaygio_ravien"] = PDT != null ? Utility.FormatDateTime_giophut_ngay_thang_nam(PDT.NgayRavien, "") : ".......... giờ ....... ngày ........./........./.............";
                //dtData.Rows[0]["sngayxacnhan"] = Utility.FormatDateTime(Utility.sDbnull(dtData.Rows[0]["sngayxacnhan"], ""), "ngày......tháng......năm.........");
                WordPrinter.InPhieuChamSoc(dtData, "PHIEU_CHAMSOC_CAP_2_3.doc", "");


            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
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

        private void groupBox4_Enter(object sender, EventArgs e)
        {

        }

        private void pnlFunctions_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cmd_them_chandoan_muctieu_Click(object sender, EventArgs e)
        {
            cmd_them_chandoan_muctieu.Enabled = false;
            cmd_sua_chandoan_muctieu.Enabled = false;
            cmd_luu_chandoan_muctieu.Enabled = true;
            cmd_huy_chandoan_muctieu.Enabled = true;
            m_enAct_CD = action.Insert;
            SetControlStatus_ChanDoanMucTieu();
        }

        private void cmd_sua_chandoan_muctieu_Click(object sender, EventArgs e)
        {
            cmd_them_chandoan_muctieu.Enabled = false;
            cmd_sua_chandoan_muctieu.Enabled = false;
            cmd_luu_chandoan_muctieu.Enabled = true;
            cmd_huy_chandoan_muctieu.Enabled = true;
            m_enAct_CD = action.Update;
            SetControlStatus_ChanDoanMucTieu();

        }

        private void cmd_luu_chandoan_muctieu_Click(object sender, EventArgs e)
        {
            try
            {
                if (Utility.DoTrim(txt_chandoan.Text) == "")
                {
                    Utility.ShowMsg("Bạn cần nhập thông tin chẩn đoán");
                    txt_chandoan.Focus();
                    return;
                }
                if (Utility.DoTrim(txt_muctieu_1.Text) == "" && Utility.DoTrim(txt_muctieu_2.Text) == "")
                {
                    Utility.ShowMsg("Bạn cần nhập thông tin một trong 2 mục tiêu kèm chẩn đoán");
                    txt_muctieu_1.Focus();
                    return;
                }
                LuuChanDoanMucTieu();
            }
            catch (Exception ex)
            {
            }
        }
        public bool LuuChanDoanMucTieu()
        {
            try
            {
                bool isNew = true;
                Msg = "";
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {

                        if (_phieuchandoanmuctieu == null || _phieuchandoanmuctieu.IdChandoan <= 0)
                        {
                            isNew = true;
                            _phieuchandoanmuctieu = new EmrPhieuchamsocChandoan();
                            _phieuchandoanmuctieu.IsNew = true;
                            _phieuchandoanmuctieu.NgayTao = DateTime.Now;
                            _phieuchandoanmuctieu.NguoiTao = globalVariables.UserName;
                        }
                        else
                        {
                            isNew = false;
                            _phieuchandoanmuctieu.IsNew = false;
                            _phieuchandoanmuctieu.MarkOld();
                            _phieuchandoanmuctieu.NgaySua = DateTime.Now;
                            _phieuchandoanmuctieu.NguoiSua = globalVariables.UserName;
                        }
                        _phieuchandoanmuctieu.IdBenhnhan = objLuotkham.IdBenhnhan;
                        _phieuchandoanmuctieu.MaLuotkham = objLuotkham.MaLuotkham;
                        
                        _phieuchandoanmuctieu.SttChandoan = Utility.ByteDbnull(nmr_stt_chandoan.Value);
                        _phieuchandoanmuctieu.ChanDoan = Utility.sDbnull(txt_chandoan.Text);
                        _phieuchandoanmuctieu.MucTieu1 = Utility.sDbnull(txt_muctieu_1.Text);
                        _phieuchandoanmuctieu.MucTieu2 = Utility.sDbnull(txt_muctieu_2.Text);
                        _phieuchandoanmuctieu.Save();

                    }
                    scope.Complete();
                }
                txt_IdCd.Text = _phieuchandoanmuctieu.IdChandoan.ToString();
                if (isNew)
                {
                    DataRow newDr = dtChanDoanMucTieu.NewRow();
                    Utility.FromObjectToDatarow(_phieuchandoanmuctieu, ref newDr);
                    dtChanDoanMucTieu.Rows.Add(newDr);
                    dtChanDoanMucTieu.AcceptChanges();
                }
                else
                {
                    DataRow currDr = Utility.getCurrentDataRow(grd_chandoan_muctieu);
                    if (currDr != null)
                    {
                        currDr["chan_doan"] = Utility.sDbnull(txt_chandoan.Text);
                        currDr["muc_tieu_1"] = Utility.sDbnull(txt_muctieu_1.Text);
                        currDr["muc_tieu_2"] = Utility.sDbnull(txt_muctieu_2.Text);
                        currDr["ngay_sua"] = _phieuchandoanmuctieu.NgaySua;
                        currDr["nguoi_sua"] = _phieuchandoanmuctieu.NguoiSua;
                        dtChanDoanMucTieu.AcceptChanges();
                    }
                }
                m_enAct_CD = action.FirstOrFinished;
                SetControlStatus_ChanDoanMucTieu();
                return true;
            }
            catch (System.Exception ex)
            {
                Utility.CatchException(ex);
                return false;
            }
        }
        private void cmd_huy_chandoan_muctieu_Click(object sender, EventArgs e)
        {
            cmd_them_chandoan_muctieu.Enabled = true;
            cmd_sua_chandoan_muctieu.Enabled = dtChanDoanMucTieu != null && dtChanDoanMucTieu.Rows.Count > 0;
            cmd_luu_chandoan_muctieu.Enabled = false;
            cmd_huy_chandoan_muctieu.Enabled = false;
            m_enAct_CD = action.FirstOrFinished;
            SetControlStatus_ChanDoanMucTieu();
        }
        private void SetControlStatus_ChanDoanMucTieu()
        {
            try
            {


                switch (m_enAct_CD)
                {
                    case action.Insert:
                        grd_chandoan_muctieu.Enabled = false;
                        EnableControl(grb_ThongTinChanDoanMucTieu, true, true);
                        //--------------------------------------------------------------
                        //Không cho phép chọn trên lưới dữ liệu được fill vào các Control
                        AllowedChanged_ChanDoanMucTieu = false;
                       
                        txt_chandoan.Focus();
                        break;
                    case action.Update:
                        grd_chandoan_muctieu.Enabled = false;
                        EnableControl(grb_ThongTinChanDoanMucTieu, true, false);
                        //--------------------------------------------------------------
                        //Không cho phép chọn trên lưới dữ liệu được fill vào các Control
                        AllowedChanged_ChanDoanMucTieu = false;
                      
                        txt_chandoan.Focus();
                        break;
                    case action.FirstOrFinished://Hủy hoặc trạng thái ban đầu khi mới hiển thị Form

                        grd_chandoan_muctieu.Enabled = true;
                        EnableControl(grb_ThongTinChanDoanMucTieu, false, false);
                        AllowedChanged_ChanDoanMucTieu = true;
                       
                        Grd_chandoan_muctieu_SelectionChanged(grd_chandoan_muctieu, new EventArgs());
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
    }
}
