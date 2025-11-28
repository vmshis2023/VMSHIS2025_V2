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
    public partial class uc_phieutheodoi_tresosinh : UserControl
    {
        public delegate void OnMsg(string msg, bool IsSucess = false);
        public event OnMsg _OnMsg;
        public delegate void OnStatus(bool isNew);
        public event OnStatus _OnStatus;
        public EmrHosoTheodoiSosinh _phieu;
        KcbLuotkham objLuotkham;
        public int id_bacsikham = -1;
        public bool Force2Saved = false;
        public bool DaKhoiTaoDanhMuc = false;
        DmucNhanvien objNguoiDaidien = null;
        private DataTable dtApgar;
        public uc_phieutheodoi_tresosinh()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            grdList.MouseDoubleClick += GrdList_MouseDoubleClick;
            grdList.ColumnButtonClick += GrdList_ColumnButtonClick;
            grdList.SelectionChanged += GrdList_SelectionChanged;
          
            grd_bangdiem_apgar.CellUpdated += Grd_bangdiem_apgar_CellUpdated;
        }

        private void Grd_bangdiem_apgar_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            try
            {
                if (e.Column.Key == "id" || e.Column.Key == "ThoiDiem" || e.Column.Key == "TongSo")
                    return;

                GridEXRow row = grd_bangdiem_apgar.CurrentRow;
                if (row == null) return;
                int sum = 0;
                foreach (GridEXColumn col in grd_bangdiem_apgar.RootTable.Columns)
                {
                    if (col.Key != "id" && col.Key != "ThoiDiem" && col.Key != "TongSo")
                    {
                        object val = row.Cells[col.Key].Value;
                        if (val != DBNull.Value)
                            sum += Convert.ToInt32(val);
                    }
                }
                 //row.BeginEdit();
                
                row.Cells["TongSo"].Value = sum;              // cập nhật giá trị

               // row.EndEdit();
            }
            catch
            {
                // Bỏ qua lỗi nhỏ do nhập sai kiểu
            }
        }

       

        private void GrdList_SelectionChanged(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdList)) mv_id_phieu = -1;
            mv_id_phieu = Utility.Int64Dbnull(grdList.GetValue("id"));
        }


        private void GrdList_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            BeginUpdate();
            isAllowSelectionChanged = false;
        }
        private void GrdList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            BeginUpdate();
            isAllowSelectionChanged = false;
        }
        long mv_id_phieu = -1;
        void BeginUpdate()
        {
            try
            {
                if (!isAllowSelectionChanged || !Utility.isValidGrid(grdList))
                {
                    _phieu = null;
                    //ClearControl(this);
                    return;
                }
               
                _phieu = new Select().From(EmrHosoTheodoiSosinh.Schema)
                            .Where(EmrHosoTheodoiSosinh.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                            .And(EmrHosoTheodoiSosinh.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                            .And(EmrHosoTheodoiSosinh.Columns.Id).IsEqualTo(mv_id_phieu)
                            .ExecuteSingle<EmrHosoTheodoiSosinh>();
                m_enAct = action.Update;
                cmdthemmoi.Enabled = cmd_duplicate.Enabled =grdList.Enabled=cmdSua.Enabled= cmdxoa.Enabled =grdList.Enabled= false;
                cmdGhi.Enabled = grd_bangdiem_apgar.Enabled = cmd_ketthuc_hoso.Enabled= cmdHuy.Enabled = cmdIn.Enabled = true;
                FillData4Update();
            }
            catch (Exception ex)
            {

            }

        }
        private void txt_bacsi_pttt_OnEnterMe()
        {
           
        }

        //public void Init(KcbLuotkham objLuotkham, long id_giaychungsinh)
        //{
        //    this.id_giaychungsinh = id_giaychungsinh;
        //    dtp_ngayphieu.Value = globalVariables.SysDate;
        //    this.objLuotkham = objLuotkham;
        //    _phieu = new Select().From(EmrHosoTheodoiSosinh.Schema)
        //               .Where(EmrHosoTheodoiSosinh.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
        //               .And(EmrHosoTheodoiSosinh.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
        //               .And(EmrHosoTheodoiSosinh.Columns.IdGiaychungsinh).IsEqualTo(id_giaychungsinh)
        //               .ExecuteSingle<EmrHosoTheodoiSosinh>();
        //    InitDanhmuc();
        //    FillData4Update();

        //}
        DataTable m_dtData = new DataTable();
        public void Init(KcbLuotkham objLuotkham, EmrHosoTheodoiSosinh _phieu)
        {
            LoadGridApgar(true);
            dtp_ngayphieu.Value = globalVariables.SysDate;
            this.objLuotkham = objLuotkham;
            this._phieu = _phieu;
            InitDanhmuc();
            DateTime dtNgay = new DateTime(1900, 1, 1);
            m_dtData = SPs.EmrHosoTheodoiSosinhLaydanhsach(-1, dtNgay, dtNgay, "",  objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, "", "", "", 100).GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdList, m_dtData, false, true, "1=1", "ngay_phieu,ngaysinh_be,hoten_be");
            isAllowSelectionChanged = true;
            ModifyCommandButtons();

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
            dtp_ngayphieu.Value = globalVariables.SysDate;
            InitDanhmuc();
        }
        void InitDanhmuc()
        {
            if (DaKhoiTaoDanhMuc) return;
            txt_bacsi.Init(globalVariables.gv_dtDmucNhanvien,
                                           new List<string>
                                {
                                      DmucNhanvien.Columns.IdNhanvien,
                                      DmucNhanvien.Columns.MaNhanvien,
                                      DmucNhanvien.Columns.TenNhanvien
                                });
            txt_dieuduong.Init(txt_bacsi.AutoCompleteSource, txt_bacsi.defaultItem);
            txt_bacsy_kham.Init(txt_bacsi.AutoCompleteSource, txt_bacsi.defaultItem);
            txt_nguoichamsoc.Init(txt_bacsi.AutoCompleteSource, txt_bacsi.defaultItem);
            txt_nguoichidinh_duphong_HIV.Init(txt_bacsi.AutoCompleteSource, txt_bacsi.defaultItem);
            txt_nguoisangloc.Init(txt_bacsi.AutoCompleteSource, txt_bacsi.defaultItem);
            txt_nguoitiem.Init(txt_bacsi.AutoCompleteSource, txt_bacsi.defaultItem);
            txt_nguoitiemphonglao.Init(txt_bacsi.AutoCompleteSource, txt_bacsi.defaultItem);
            txt_nguoitiemviemganB.Init(txt_bacsi.AutoCompleteSource, txt_bacsi.defaultItem);
            txt_nghenghiep.Init();
            txtDantoc.Init();
        }
        private void LoadGridApgar(bool isInit)
        {
            if (dtApgar == null || dtApgar.Columns.Count <= 0)
            {
                // Tạo DataTable chứa dữ liệu APGAR
                dtApgar = new DataTable();
                dtApgar.Columns.Add("id", typeof(int));
                dtApgar.Columns.Add("ThoiDiem", typeof(string));
                dtApgar.Columns.Add("Tim_0", typeof(byte));
                dtApgar.Columns.Add("Tim_1", typeof(byte));
                dtApgar.Columns.Add("Tim_2", typeof(byte));
                dtApgar.Columns.Add("Tho_0", typeof(byte));
                dtApgar.Columns.Add("Tho_1", typeof(byte));
                dtApgar.Columns.Add("Tho_2", typeof(byte));
                dtApgar.Columns.Add("Mausacda_0", typeof(byte));
                dtApgar.Columns.Add("Mausacda_1", typeof(byte));
                dtApgar.Columns.Add("Mausacda_2", typeof(byte));
                dtApgar.Columns.Add("Truonglucco_0", typeof(byte));
                dtApgar.Columns.Add("Truonglucco_1", typeof(byte));
                dtApgar.Columns.Add("Truonglucco_2", typeof(byte));
                dtApgar.Columns.Add("Phanxa_0", typeof(byte));
                dtApgar.Columns.Add("Phanxa_1", typeof(byte));
                dtApgar.Columns.Add("Phanxa_2", typeof(byte));
                dtApgar.Columns.Add("TongSo", typeof(int));
              
                // Thêm 3 hàng mặc định
                dtApgar.Rows.Add(1, "1 phút", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                dtApgar.Rows.Add(5, "5 phút", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                dtApgar.Rows.Add(10, "10 phút", 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                // Gán vào GridEX
                Utility.SetDataSourceForDataGridEx(grd_bangdiem_apgar, dtApgar, false, true, "1=1", "id");
            }

        }
        List<string> lstCols = new List<string>() { "Tim_0", "Tim_1", "Tim_2",
         "Tho_0", "Tho_1", "Tho_2",
         "Mausacda_0", "Mausacda_1", "Mausacda_2",
         "Truonglucco_0", "Truonglucco_1", "Truonglucco_2",
         "Phanxa_0", "Phanxa_1", "Phanxa_2",
        "TongSo"};
        void resetData()
        {
            foreach (DataRow dr in dtApgar.Rows)
            {
                foreach (DataColumn col in dtApgar.Columns)
                    if(lstCols.Contains(col.ColumnName))
                    dr[col.ColumnName] = 0;
            }
        }
       
        void UpdateAgFa()
        {
            DataRow[] arrDr = dtApgar.Select("id=1");
            if (arrDr.Length > 0)
            {
                arrDr[0]["Tim_0"] = Utility.ByteDbnull(_phieu.Apgar1phutTim0);
                arrDr[0]["Tim_1"] = Utility.ByteDbnull(_phieu.Apgar1phutTim1);
                arrDr[0]["Tim_2"] = Utility.ByteDbnull(_phieu.Apgar1phutTim2);

                arrDr[0]["Tho_0"] = Utility.ByteDbnull(_phieu.Apgar1phutTho0);
                arrDr[0]["Tho_1"] = Utility.ByteDbnull(_phieu.Apgar1phutTho1);
                arrDr[0]["Tho_2"] = Utility.ByteDbnull(_phieu.Apgar1phutTho2);

                arrDr[0]["Mausacda_0"] = Utility.ByteDbnull(_phieu.Apgar1phutMauda0);
                arrDr[0]["Mausacda_1"] = Utility.ByteDbnull(_phieu.Apgar1phutMauda1);
                arrDr[0]["Mausacda_2"] = Utility.ByteDbnull(_phieu.Apgar1phutMauda2);

                arrDr[0]["Truonglucco_0"] = Utility.ByteDbnull(_phieu.Apgar1phutTruonglucco0);
                arrDr[0]["Truonglucco_1"] = Utility.ByteDbnull(_phieu.Apgar1phutTruonglucco1);
                arrDr[0]["Truonglucco_2"] = Utility.ByteDbnull(_phieu.Apgar1phutTruonglucco2);

                arrDr[0]["Phanxa_0"] = Utility.ByteDbnull(_phieu.Apgar1phutPhanxa0);
                arrDr[0]["Phanxa_1"] = Utility.ByteDbnull(_phieu.Apgar1phutPhanxa1);
                arrDr[0]["Phanxa_2"] = Utility.ByteDbnull(_phieu.Apgar1phutPhanxa2);
                 arrDr[0]["TongSo"] = Utility.Int16Dbnull(_phieu.Apgar1phutTongso);
            }
            //5 phút
            arrDr = dtApgar.Select("id=5");
            if (arrDr.Length > 0)
            {
                arrDr[0]["Tim_0"] = Utility.ByteDbnull(_phieu.Apgar5phutTim0);
                arrDr[0]["Tim_1"] = Utility.ByteDbnull(_phieu.Apgar5phutTim1);
                arrDr[0]["Tim_2"] = Utility.ByteDbnull(_phieu.Apgar5phutTim2);

                arrDr[0]["Tho_0"] = Utility.ByteDbnull(_phieu.Apgar5phutTho0);
                arrDr[0]["Tho_1"] = Utility.ByteDbnull(_phieu.Apgar5phutTho1);
                arrDr[0]["Tho_2"] = Utility.ByteDbnull(_phieu.Apgar5phutTho2);

                arrDr[0]["Mausacda_0"] = Utility.ByteDbnull(_phieu.Apgar5phutMauda0);
                arrDr[0]["Mausacda_1"] = Utility.ByteDbnull(_phieu.Apgar5phutMauda1);
                arrDr[0]["Mausacda_2"] = Utility.ByteDbnull(_phieu.Apgar5phutMauda2);

                arrDr[0]["Truonglucco_0"] = Utility.ByteDbnull(_phieu.Apgar5phutTruonglucco0);
                arrDr[0]["Truonglucco_1"] = Utility.ByteDbnull(_phieu.Apgar5phutTruonglucco1);
                arrDr[0]["Truonglucco_2"] = Utility.ByteDbnull(_phieu.Apgar5phutTruonglucco2);

                arrDr[0]["Phanxa_0"] = Utility.ByteDbnull(_phieu.Apgar5phutPhanxa0);
                arrDr[0]["Phanxa_1"] = Utility.ByteDbnull(_phieu.Apgar5phutPhanxa1);
                arrDr[0]["Phanxa_2"] = Utility.ByteDbnull(_phieu.Apgar5phutPhanxa2);
                arrDr[0]["TongSo"] = Utility.Int16Dbnull(_phieu.Apgar5phutTongso);
            }
            //10 phút
            arrDr = dtApgar.Select("id=10");
            if (arrDr.Length > 0)
            {
                arrDr[0]["Tim_0"] = Utility.ByteDbnull(_phieu.Apgar10phutTim0);
                arrDr[0]["Tim_1"] = Utility.ByteDbnull(_phieu.Apgar10phutTim1);
                arrDr[0]["Tim_2"] = Utility.ByteDbnull(_phieu.Apgar10phutTim2);

                arrDr[0]["Tho_0"] = Utility.ByteDbnull(_phieu.Apgar10phutTho0);
                arrDr[0]["Tho_1"] = Utility.ByteDbnull(_phieu.Apgar10phutTho1);
                arrDr[0]["Tho_2"] = Utility.ByteDbnull(_phieu.Apgar10phutTho2);

                arrDr[0]["Mausacda_0"] = Utility.ByteDbnull(_phieu.Apgar10phutMauda0);
                arrDr[0]["Mausacda_1"] = Utility.ByteDbnull(_phieu.Apgar10phutMauda1);
                arrDr[0]["Mausacda_2"] = Utility.ByteDbnull(_phieu.Apgar10phutMauda2);

                arrDr[0]["Truonglucco_0"] = Utility.ByteDbnull(_phieu.Apgar10phutTruonglucco0);
                arrDr[0]["Truonglucco_1"] = Utility.ByteDbnull(_phieu.Apgar10phutTruonglucco1);
                arrDr[0]["Truonglucco_2"] = Utility.ByteDbnull(_phieu.Apgar10phutTruonglucco2);

                arrDr[0]["Phanxa_0"] = Utility.ByteDbnull(_phieu.Apgar10phutPhanxa0);
                arrDr[0]["Phanxa_1"] = Utility.ByteDbnull(_phieu.Apgar10phutPhanxa1);
                arrDr[0]["Phanxa_2"] = Utility.ByteDbnull(_phieu.Apgar10phutPhanxa2);
                arrDr[0]["TongSo"] = Utility.Int16Dbnull(_phieu.Apgar10phutTongso);
            }
        }

        void UpdateAgFa2Object()
        {
            DataRow[] arrDr = dtApgar.Select("id=1");
            if (arrDr.Length > 0)
            {
                _phieu.Apgar1phutTim0 = Utility.ByteDbnull(arrDr[0]["Tim_0"]);
                _phieu.Apgar1phutTim1 = Utility.ByteDbnull(arrDr[0]["Tim_1"]);
                _phieu.Apgar1phutTim2 = Utility.ByteDbnull(arrDr[0]["Tim_2"]);

                _phieu.Apgar1phutTho0 = Utility.ByteDbnull(arrDr[0]["Tho_0"]);
                _phieu.Apgar1phutTho1 = Utility.ByteDbnull(arrDr[0]["Tho_1"]);
                _phieu.Apgar1phutTho2 = Utility.ByteDbnull(arrDr[0]["Tho_2"]);

                _phieu.Apgar1phutMauda0 = Utility.ByteDbnull(arrDr[0]["Mausacda_0"]);
                _phieu.Apgar1phutMauda1 = Utility.ByteDbnull(arrDr[0]["Mausacda_1"]);
                _phieu.Apgar1phutMauda2 = Utility.ByteDbnull(arrDr[0]["Mausacda_2"]);

                _phieu.Apgar1phutTruonglucco0 = Utility.ByteDbnull(arrDr[0]["Truonglucco_0"]);
                _phieu.Apgar1phutTruonglucco1 = Utility.ByteDbnull(arrDr[0]["Truonglucco_1"]);
                _phieu.Apgar1phutTruonglucco2 = Utility.ByteDbnull(arrDr[0]["Truonglucco_2"]);

                _phieu.Apgar1phutPhanxa0 = Utility.ByteDbnull(arrDr[0]["Phanxa_0"]);
                _phieu.Apgar1phutPhanxa1 = Utility.ByteDbnull(arrDr[0]["Phanxa_1"]);
                _phieu.Apgar1phutPhanxa2 = Utility.ByteDbnull(arrDr[0]["Phanxa_2"]);
                _phieu.Apgar1phutTongso = Utility.Int16Dbnull(arrDr[0]["TongSo"]);
            }

            // 5 phút
            arrDr = dtApgar.Select("id=5");
            if (arrDr.Length > 0)
            {
                _phieu.Apgar5phutTim0 = Utility.ByteDbnull(arrDr[0]["Tim_0"]);
                _phieu.Apgar5phutTim1 = Utility.ByteDbnull(arrDr[0]["Tim_1"]);
                _phieu.Apgar5phutTim2 = Utility.ByteDbnull(arrDr[0]["Tim_2"]);

                _phieu.Apgar5phutTho0 = Utility.ByteDbnull(arrDr[0]["Tho_0"]);
                _phieu.Apgar5phutTho1 = Utility.ByteDbnull(arrDr[0]["Tho_1"]);
                _phieu.Apgar5phutTho2 = Utility.ByteDbnull(arrDr[0]["Tho_2"]);

                _phieu.Apgar5phutMauda0 = Utility.ByteDbnull(arrDr[0]["Mausacda_0"]);
                _phieu.Apgar5phutMauda1 = Utility.ByteDbnull(arrDr[0]["Mausacda_1"]);
                _phieu.Apgar5phutMauda2 = Utility.ByteDbnull(arrDr[0]["Mausacda_2"]);

                _phieu.Apgar5phutTruonglucco0 = Utility.ByteDbnull(arrDr[0]["Truonglucco_0"]);
                _phieu.Apgar5phutTruonglucco1 = Utility.ByteDbnull(arrDr[0]["Truonglucco_1"]);
                _phieu.Apgar5phutTruonglucco2 = Utility.ByteDbnull(arrDr[0]["Truonglucco_2"]);

                _phieu.Apgar5phutPhanxa0 = Utility.ByteDbnull(arrDr[0]["Phanxa_0"]);
                _phieu.Apgar5phutPhanxa1 = Utility.ByteDbnull(arrDr[0]["Phanxa_1"]);
                _phieu.Apgar5phutPhanxa2 = Utility.ByteDbnull(arrDr[0]["Phanxa_2"]);
                _phieu.Apgar5phutTongso = Utility.Int16Dbnull(arrDr[0]["TongSo"]);
            }

            // 10 phút
            arrDr = dtApgar.Select("id=10");
            if (arrDr.Length > 0)
            {
                _phieu.Apgar10phutTim0 = Utility.ByteDbnull(arrDr[0]["Tim_0"]);
                _phieu.Apgar10phutTim1 = Utility.ByteDbnull(arrDr[0]["Tim_1"]);
                _phieu.Apgar10phutTim2 = Utility.ByteDbnull(arrDr[0]["Tim_2"]);

                _phieu.Apgar10phutTho0 = Utility.ByteDbnull(arrDr[0]["Tho_0"]);
                _phieu.Apgar10phutTho1 = Utility.ByteDbnull(arrDr[0]["Tho_1"]);
                _phieu.Apgar10phutTho2 = Utility.ByteDbnull(arrDr[0]["Tho_2"]);

                _phieu.Apgar10phutMauda0 = Utility.ByteDbnull(arrDr[0]["Mausacda_0"]);
                _phieu.Apgar10phutMauda1 = Utility.ByteDbnull(arrDr[0]["Mausacda_1"]);
                _phieu.Apgar10phutMauda2 = Utility.ByteDbnull(arrDr[0]["Mausacda_2"]);

                _phieu.Apgar10phutTruonglucco0 = Utility.ByteDbnull(arrDr[0]["Truonglucco_0"]);
                _phieu.Apgar10phutTruonglucco1 = Utility.ByteDbnull(arrDr[0]["Truonglucco_1"]);
                _phieu.Apgar10phutTruonglucco2 = Utility.ByteDbnull(arrDr[0]["Truonglucco_2"]);

                _phieu.Apgar10phutPhanxa0 = Utility.ByteDbnull(arrDr[0]["Phanxa_0"]);
                _phieu.Apgar10phutPhanxa1 = Utility.ByteDbnull(arrDr[0]["Phanxa_1"]);
                _phieu.Apgar10phutPhanxa2 = Utility.ByteDbnull(arrDr[0]["Phanxa_2"]);
                _phieu.Apgar10phutTongso = Utility.Int16Dbnull(arrDr[0]["TongSo"]);
            }

        }
        public void FillData4Update()
        {
            try
            {
               // LoadGridApgar(false);
                //EmrGiayChungsinh GCS = EmrGiayChungsinh.FetchByID(id_giaychungsinh);
                //if(GCS!=null)
                //{
                //    txt_hoten_be.Text = Utility.sDbnull(GCS.HotenBe);
                //    dtp_ngaysinh_be.Value = GCS.NgaysinhBe.Value;
                //    txtDantoc.SetCode(Utility.sDbnull(GCS.MaDantoc));
                //    opt_nam.Checked = Utility.Byte2Bool(GCS.IdGioitinh);
                //    opt_nu.Checked = Utility.Byte2Bool(GCS.IdGioitinh);
                //    chk_ngoaikieu.Checked = Utility.Byte2Bool(GCS.NgoaiKieu);
                //    txt_hoten_bo.Text = Utility.sDbnull(GCS.HotenBo);
                //    dtp_ngaysinh_bo.Value = GCS.NgaysinhBo.Value;
                //    txt_nghenghiep._Text = Utility.sDbnull(GCS.NghenghiepBo);
                //}    
                //if (_phieu == null)
                //    _phieu = new Select().From(EmrHosoTheodoiSosinh.Schema)
                //        .Where(EmrHosoTheodoiSosinh.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                //        .And(EmrHosoTheodoiSosinh.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                //        .And(EmrHosoTheodoiSosinh.Columns.IdGiaychungsinh).IsEqualTo(id_giaychungsinh)
                //        .ExecuteSingle<EmrHosoTheodoiSosinh>();

                txtId.Text = "";
                if (_phieu != null)
                {
                    UpdateAgFa();
                    txtId.Text = _phieu.Id.ToString();
                    txtSoHoso.Text = _phieu.MaPhieu;
                    dtp_ngayphieu.Value = _phieu.NgayPhieu.Value;

                    txt_khoa.SetId(_phieu.IdKhoa);
                    //txt_khoa._Text = Utility.sDbnull(_phieu.TenKhoa);
                    txt_buong.Text = Utility.sDbnull(_phieu.Buong);
                    txt_giuong.Text = Utility.sDbnull(_phieu.Giuong);

                    _phieu.IdKhoa = Utility.Int32Dbnull(txt_khoa.MyID);
                    _phieu.TenKhoa = Utility.sDbnull(txt_khoa.Text);
                    _phieu.Buong = Utility.sDbnull(txt_buong.Text);
                    _phieu.Giuong = Utility.sDbnull(txt_giuong.Text);

                    txt_hoten_be.Text= Utility.sDbnull(_phieu.HotenBe);
                    if (_phieu.NgaysinhBe.HasValue)
                        dtp_ngaysinh_be.Value = _phieu.NgaysinhBe.Value;
                    txtDantoc.SetCode(Utility.sDbnull(_phieu.MaDantoc));
                    opt_nam.Checked = Utility.Bool2Bool(_phieu.GioitinhNam);
                    opt_nu.Checked = Utility.Bool2Bool(_phieu.GioitinhNu);
                    chk_ngoaikieu.Checked= Utility.Byte2Bool(_phieu.NgoaiKieu);
                    txt_hoten_bo.Text = Utility.sDbnull(_phieu.HotenBo);
                    if (_phieu.NgaysinhBo.HasValue)
                        dtp_ngaysinh_bo.Value = _phieu.NgaysinhBo.Value;
                    txt_nghenghiep._Text = Utility.sDbnull(_phieu.NghenghiepBo);
                    txtNhommau._Text = Utility.sDbnull(_phieu.XnNhommau);
                    opt_daithaoduong_co.Checked = Utility.Bool2Bool(_phieu.DaithaoduongCo);
                    opt_daithaoduong_khong.Checked = Utility.Bool2Bool(_phieu.DaithaoduongKhong);
                   
                    opt_hbsag_amtinh.Checked = Utility.Bool2Bool(_phieu.HbsagAmtinh);
                    opt_hbsag_duongtinh.Checked = Utility.Bool2Bool(_phieu.HbsagDuongtinh);
                    opt_giangmai_amtinh.Checked = Utility.Bool2Bool(_phieu.GiangmaiAmtinh);
                    opt_giangmai_duongtinh.Checked = Utility.Bool2Bool(_phieu.GiangmaiDuongtinh);
                    opt_hiv_amtinh.Checked = Utility.Bool2Bool(_phieu.HivAmtinh);
                    opt_hiv_duongtinh.Checked = Utility.Bool2Bool(_phieu.HivDuongtinh);
                    opt_gbs_amtinh.Checked = Utility.Bool2Bool(_phieu.GbsAmtinh);
                    opt_gbs_duongtinh.Checked = Utility.Bool2Bool(_phieu.GbsDuongtinh);
                    if (_phieu.NgayKham.HasValue)
                        dtp_ngaykham.Value = _phieu.NgayKham.Value;
                    txt_bacsi.SetId(Utility.Int16Dbnull(_phieu.IdBacsy));
                    txt_bacsy_kham.SetId(Utility.Int16Dbnull(_phieu.IdBacsyKham));
                    txt_dieuduong.SetId(Utility.Int16Dbnull(_phieu.IdDieuduong));
                    txt_nguoichamsoc.SetId(Utility.Int16Dbnull(_phieu.IdNguoiChamsoc));
                    txt_nguoichidinh_duphong_HIV.SetId(Utility.Int16Dbnull(_phieu.IdNguoithuchienHiv));
                    txt_nguoisangloc.SetId(Utility.Int16Dbnull(_phieu.IdNguoiSangloc));
                    txt_nguoitiem.SetId(Utility.Int16Dbnull(_phieu.IdNguoitiem));
                    txt_nguoitiemphonglao.SetId(Utility.Int16Dbnull(_phieu.IdNguoitiemLao));
                    txt_nguoitiemviemganB.SetId(Utility.Int16Dbnull(_phieu.IdNguoithuchienTiemviemganB));



                    //Phần dành cho hộ sinh điều dưỡng
                    txt_para.Text = Utility.sDbnull(_phieu.Para);
                  
                    txt_sot_truocsinh.Text = Utility.sDbnull(_phieu.SotTruocsinhMota);
                    nmr_tuoithai_dukien.Text = Utility.sDbnull(_phieu.TuoithaiDukien);
                    nmr_tuoithai_danhgia.Text = Utility.sDbnull(_phieu.TuoithaiDanhgia);
                    opt_phuongphapde_thuong.Checked = Utility.Bool2Bool(_phieu.PhuongphapdeThuong);
                    opt_phuongphapde_mo.Checked = Utility.Bool2Bool(_phieu.PhuongphapdeMo);
                    opt_phuongphapde_mochudong.Checked = Utility.Bool2Bool(_phieu.PhuongphapdeMochudong);
                    opt_phuongphapde_giachut.Checked = Utility.Bool2Bool(_phieu.PhuongphapdeGiachut);
                    opt_phuongphapde_forceps.Checked = Utility.Bool2Bool(_phieu.PhuongphapdeForceps);
                    opt_phuongphapde_chihuy.Checked = Utility.Bool2Bool(_phieu.PhuongphapdeChihuy);

                    opt_vitri_dattre_dakeda.Checked = Utility.Bool2Bool(_phieu.VitriDattreDakeda);
                    opt_vitri_dattre_giuongsuoi.Checked = Utility.Bool2Bool(_phieu.VitriDattreGiuongsuoi);
                    opt_vitri_dattre_khac.Checked = Utility.Bool2Bool(_phieu.VitriDattreKhac);

                    dtp_oivo_luc.Value = _phieu.OivoLuc.Value;
                    opt_nuoc_oi_trong.Checked = Utility.Bool2Bool(_phieu.NuocOiTrong);
                    opt_nuoc_oi_xanhban.Checked = Utility.Bool2Bool(_phieu.NuocOiXanhban);
                    opt_nuoc_oi_lanmau.Checked = Utility.Bool2Bool(_phieu.NuocOiLanmau);
                    txt_nuoc_oi_mota.Text = Utility.sDbnull(_phieu.NuocOiMota);

                    nmr_cat_ron_muon.Text = Utility.sDbnull(_phieu.CatRonMuon);
                    txt_mauron.Text = Utility.sDbnull(_phieu.MauRon);
                    opt_bume_co.Checked = Utility.Bool2Bool(_phieu.BumeCo);
                    opt_bume_khong.Checked = Utility.Bool2Bool(_phieu.BumeKhong);
                    
                    opt_dakeda_khongthuchien.Checked = Utility.Bool2Bool(_phieu.DakedaKhongthuchien);
                    opt_dakeda_trong30.Checked = Utility.Bool2Bool(_phieu.DakedaTrong30);
                    opt_dakeda_30den90.Checked = Utility.Bool2Bool(_phieu.Dakeda30den90);
                    opt_dakeda_tu_90.Checked = Utility.Bool2Bool(_phieu.DakedaTu90);

                    txt_tresosinh_cannang.Text = Utility.sDbnull(_phieu.TresosinhCannang);
                    txt_tresosinh_cao.Text = Utility.sDbnull(_phieu.TresosinhCao);
                    txt_tresosinh_vongdau.Text = Utility.sDbnull(_phieu.TresosinhVongdau);
                    //Thông tin phiếu dành cho bác sỹ
                    //Bảng điểm APGAR
                    opt_ditat_bamsinh_co.Checked = Utility.Bool2Bool(_phieu.DitatBamsinhCo);
                    opt_ditat_bamsinh_khong.Checked = Utility.Bool2Bool(_phieu.DitatBamsinhKhong);
                    txt_ditat_bamsinh_mota.Text = Utility.sDbnull(_phieu.DitatBamsinhMota);

                    opt_hoisuc_co.Checked = Utility.Bool2Bool(_phieu.HoisucCo);
                    opt_hoisuc_khong.Checked = Utility.Bool2Bool(_phieu.HoisucKhong);
                    txt_hoisuc_co_mota.Text = Utility.sDbnull(_phieu.HoisucCoMota);

                    chk_tho_oxy.Checked = Utility.Bool2Bool(_phieu.ThoOxy);
                    nmr_thoigian_tho_oxy.Value = Utility.Int32Dbnull(_phieu.ThoigianThoOxy,0);

                    chk_bop_bong.Checked = Utility.Bool2Bool(_phieu.BopBong);
                    nmr_thoigian_bop_bong.Text = Utility.sDbnull(_phieu.ThoigianBopBong, 0);
                    txt_thuoc.Text = Utility.sDbnull(_phieu.Thuoc);

                    opt_spo2_co.Checked = Utility.Bool2Bool(_phieu.Spo2Co);
                    opt_spo2_khong.Checked = Utility.Bool2Bool(_phieu.Spo2Khong);
                    nmr_spo2.Text = Utility.sDbnull(_phieu.Spo2);

                    txt_ketqua_nhiptim.Text= Utility.sDbnull(_phieu.KetquaNhiptim);
                    txt_keyqua_nhiptho.Text = Utility.sDbnull(_phieu.KetquaNhiptho);
                    txt_ketqua_nhietdo.Text = Utility.sDbnull(_phieu.KetquaNhietdo);

                    chk_mausacda_honghao.Checked = Utility.Bool2Bool(_phieu.MausacdaHonghao);
                    chk_mausacda_xanhtai.Checked = Utility.Bool2Bool(_phieu.MausacdaXanhtai);
                    chk_mausacda_vang.Checked = Utility.Bool2Bool(_phieu.MausacdaVang);
                    chk_mausacda_tim.Checked = Utility.Bool2Bool(_phieu.MausacdaTim);
                    chk_mausacda_khac.Checked = Utility.Bool2Bool(_phieu.MausacdaKhac);
                    txt_mausacda_mota.Text = Utility.sDbnull(_phieu.MausacdaMota);

                    opt_ketqua_ondinh_namcungme.Checked = Utility.Bool2Bool(_phieu.KetquaOndinhNamcungme);
                    opt_cantheodothem.Checked = Utility.Bool2Bool(_phieu.Cantheodothem);
                    opt_canchuyendonvisosinh.Checked = Utility.Bool2Bool(_phieu.Canchuyendonvisosinh);
                    
                    //Chăm sóc sơ sinh tiếp
                    opt_tiem_vitamink1_co.Checked = Utility.Bool2Bool(_phieu.TiemVitamink1Co);
                    opt_tiem_vitamink1_khong.Checked = Utility.Bool2Bool(_phieu.TiemVitamink1Khong);
                    chk_tiem_vitamink1_uong.Checked = Utility.Bool2Bool(_phieu.TiemVitamink1Uong);
                    txt_tiem_vitamink1_uong_lieudung.Text = Utility.sDbnull(_phieu.TiemVitamink1UongLieudung);
                    chk_tiem_vitamink1_tiembap.Checked = Utility.Bool2Bool(_phieu.TiemVitamink1Tiembap);
                    txt_tiem_vitamink1_tiembap_lieudung.Text = Utility.sDbnull(_phieu.TiemVitamink1TiembapLieudung);
                    if (_phieu.NgayTiem.HasValue)
                        dtp_ngay_tiem.Value = _phieu.NgayTiem.Value;
                    txt_nguoitiem.SetId(_phieu.IdNguoitiem);


                    opt_tiemphong_lao_co.Checked = Utility.Bool2Bool(_phieu.TiemphongLaoCo);
                    opt_tiemphong_lao_khong.Checked = Utility.Bool2Bool(_phieu.TiemphongLaoKhong);
                   if (_phieu.NgayTiemLao.HasValue)
                        dtp_ngay_tiem_lao.Value = _phieu.NgayTiemLao.Value;
                    txt_nguoitiemphonglao.SetId(_phieu.IdNguoitiemLao);

                    opt_viemganB_co.Checked = Utility.Bool2Bool(_phieu.ViemganBCo);
                    opt_viemganB_khong.Checked = Utility.Bool2Bool(_phieu.ViemganBKhong);
                    chk_tiemviemganB.Checked = Utility.Bool2Bool(_phieu.TiemViemganB);
                    txt_tiem_viemganB_lieudung.Text = Utility.sDbnull(_phieu.TiemViemganBLieudung);
                    chk_tiemHBIG.Checked = Utility.Bool2Bool(_phieu.TiemHbig);
                    txt_tiem_HBIG_lieudung.Text = Utility.sDbnull(_phieu.TiemHbigLieudung);
                    if (_phieu.NgayTiemViemganB.HasValue)
                        dtp_ngay_tiem_viemganB.Value = _phieu.NgayTiemViemganB.Value;
                    txt_nguoitiemviemganB.SetId(_phieu.IdNguoithuchienTiemviemganB);

                    opt_hiv_chidinh_duphong_co.Checked = Utility.Bool2Bool(_phieu.HivChidinhDuphongCo);
                    opt_hiv_chidinh_duphong_khong.Checked = Utility.Bool2Bool(_phieu.HivChidinhDuphongKhong);
                    txt_hiv_loaithuoc.Text = Utility.sDbnull(_phieu.HivLoaithuoc);
                    txt_hiv_lieudung.Text = Utility.sDbnull(_phieu.HivLieudung);
                    if (_phieu.HivNgay.HasValue)
                        dtp_ngaychidinh_duphong_HIV.Value = _phieu.HivNgay.Value;
                    txt_nguoichidinh_duphong_HIV.SetId(_phieu.IdNguoithuchienHiv);

                    opt_sangloc_sosinh_co.Checked = Utility.Bool2Bool(_phieu.SanglocSosinhCo);
                    opt_sangloc_sosinh_khong.Checked = Utility.Bool2Bool(_phieu.SanglocSosinhKhong);
                    chk_sangloc_sosinh_maugotchan.Checked = Utility.Bool2Bool(_phieu.SanglocSosinhMaugotchan);
                    txt_sangloc_sosinh_khac.Text = Utility.sDbnull(_phieu.SanglocSosinhKhac);
                    chk_tiem_vitamink1_tiembap.Checked = Utility.Bool2Bool(_phieu.TiemVitamink1Tiembap);
                    txt_ketqua_sangloc.Text = Utility.sDbnull(_phieu.SanglocSosinhKetquasangloc);
                    opt_kq_sangloc_binhthuong.Checked = Utility.Bool2Bool(_phieu.KqSanglocBinhthuong);
                    opt_kq_sangloc_batthuong.Checked = Utility.Bool2Bool(_phieu.KqSanglocBatthuong);
                    txt_kq_sangloc_mota.Text = Utility.sDbnull(_phieu.KqSanglocBatthuongMota);

                    if (_phieu.NgaySangloc.HasValue)
                        dtp_ngay_sangloc.Value = _phieu.NgaySangloc.Value;
                    txt_nguoisangloc.SetId(_phieu.IdNguoiSangloc);

                    opt_chamsoc_kangaroo_khong.Checked = Utility.Bool2Bool(_phieu.ChamsocKangarooKhong);
                    opt_chamsoc_kangaroo_ngatquang.Checked = Utility.Bool2Bool(_phieu.ChamsocKangarooNgatquang);
                    opt_chamsoc_kangaroo_colientuc24gio.Checked = Utility.Bool2Bool(_phieu.ChamsocKangarooColientuc24gio);
                  
                    if (_phieu.NgayChamsoc.HasValue)
                        dtp_ngay_chamsoc.Value = _phieu.NgayChamsoc.Value;
                    txt_nguoichamsoc.SetId(_phieu.IdNguoiChamsoc);
                    //Khám ra viện
                    opt_tuoi_24.Checked = Utility.Bool2Bool(_phieu.Tuoi24);
                    opt_tuoi_24_48.Checked = Utility.Bool2Bool(_phieu.Tuoi2448);
                    opt_tuoi_48_72.Checked = Utility.Bool2Bool(_phieu.Tuoi4872);
                    opt_tuoi_72.Checked = Utility.Bool2Bool(_phieu.Tuoi72);

                    opt_bume_hoantoan.Checked = Utility.Bool2Bool(_phieu.BumeHoantoan);
                    opt_bume_motphan.Checked = Utility.Bool2Bool(_phieu.BumeMotphan);
                    opt_bume_ansua_congthuchoantoan.Checked = Utility.Bool2Bool(_phieu.BumeAnsuaCongthuchoantoan);
                    
                    //Da
                    opt_da_binhthuong.Checked = Utility.Bool2Bool(_phieu.DaBinhthuong);
                    opt_da_batthuong.Checked = Utility.Bool2Bool(_phieu.DaBatthuong);
                    txt_da_mota.Text = Utility.sDbnull(_phieu.DaMota);
                    //Đầu
                    opt_dau_binhthuong.Checked = Utility.Bool2Bool(_phieu.DauBinhthuong);
                    opt_dau_batthuong.Checked = Utility.Bool2Bool(_phieu.DauBatthuong);
                    txt_dau_mota.Text = Utility.sDbnull(_phieu.DauMota);
                    //Tai mũi họng
                    opt_taimuihong_batthuong.Checked = Utility.Bool2Bool(_phieu.TaimuihongBatthuong);
                    opt_taimuihong_binhthuong.Checked = Utility.Bool2Bool(_phieu.TaimuihongBinhthuong);
                    txt_taimuihong_mota.Text = Utility.sDbnull(_phieu.TaimuihongMota);
                    //hô hấp
                    opt_hohap_batthuong.Checked = Utility.Bool2Bool(_phieu.HohapBatthuong);
                    opt_hohap_binhthuong.Checked = Utility.Bool2Bool(_phieu.HohapBinhthuong);
                    txt_hohap_mota.Text = Utility.sDbnull(_phieu.HohapMota);
                    //tim mạch
                    opt_timmach_batthuong.Checked = Utility.Bool2Bool(_phieu.TimmachBatthuong);
                    opt_timmach_binhthuong.Checked = Utility.Bool2Bool(_phieu.TimmachBinhthuong);
                    txt_timmach_mota.Text = Utility.sDbnull(_phieu.TimmachMota);
                    //Tiêu hóa
                    opt_tieuhoa_batthuong.Checked = Utility.Bool2Bool(_phieu.TieuhoaBatthuong);
                    opt_tieuhoa_binhthuong.Checked = Utility.Bool2Bool(_phieu.TieuhoaBinhthuong);
                    txt_tieuhoa_mota.Text = Utility.sDbnull(_phieu.TieuhoaMota);
                    //Rốn
                    opt_cuongron_batthuong.Checked = Utility.Bool2Bool(_phieu.CuongronBatthuong);
                    opt_cuongron_binhthuong.Checked = Utility.Bool2Bool(_phieu.CuongronBinhthuong);
                    txt_cuongron_mota.Text = Utility.sDbnull(_phieu.CuongronMota);
                    //Sinh dục
                    opt_sinhduc_tietnieu_batthuong.Checked = Utility.Bool2Bool(_phieu.SinhducTietnieuBatthuong);
                    opt_sinhduc_tietnieu_binhthuong.Checked = Utility.Bool2Bool(_phieu.SinhducTietnieuBinhthuong);
                    txt_sinhduc_tietnieu_mota.Text = Utility.sDbnull(_phieu.SinhducTietnieuMota);
                    //thần kinh
                    opt_thankinh_batthuong.Checked = Utility.Bool2Bool(_phieu.ThankinhBatthuong);
                    opt_thankinh_binhthuong.Checked = Utility.Bool2Bool(_phieu.ThankinhBinhthuong);
                    txt_thankinh_mota.Text = Utility.sDbnull(_phieu.ThankinhMota);
                    //Cơ xương khớp
                    opt_coxuongkhop_batthuong.Checked = Utility.Bool2Bool(_phieu.CoxuongkhopBatthuong);
                    opt_coxuongkhop_binhthuong.Checked = Utility.Bool2Bool(_phieu.CoxuongkhopBinhthuong);
                    txt_coxuongkhop_mota.Text = Utility.sDbnull(_phieu.CoxuongkhopMota);

                    txt_khamravien_khac.Text = Utility.sDbnull(_phieu.KhamravienKhac);
                    txt_cantheodoitiep.Text = Utility.sDbnull(_phieu.Cantheodoitiep);
                    if (_phieu.NgayhenTaikham.HasValue)
                        dtp_ngayhen_taikham.Value = _phieu.NgayhenTaikham.Value;
                    

                }
                else
                {
                    ClearControl(this);
                    
                }
                txtSoHoso.Text = _phieu == null || string.IsNullOrEmpty(Utility.sDbnull(_phieu.MaPhieu, "")) ? THU_VIEN_CHUNG.TT25LaySohoso(8) : Utility.sDbnull(_phieu.MaPhieu, "");
                SinhMaSoEmbeMoi();
                if (_OnStatus != null) _OnStatus(_phieu == null || _phieu.Id <= 0);
            }
            catch (System.Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
       
        void ActivateMsg(Control ctrl)
        {
            errorProvider1.Clear();
            errorProvider1.SetError(ctrl, Msg);
            if (_OnMsg != null) _OnMsg(Msg);

        }
        string Msg = "";
        bool isValidData(int trang_thai)
        {
            Msg = "";
            //if (id_giaychungsinh<=0)
            //{
            //    Msg = "Bạn phải chọn em bé cần lập phiếu hồ sơ theo dõi. Vui lòng nhập mã lần khám của người mẹ để hệ thống dò tìm em bé";
            //    if (_OnMsg != null) _OnMsg(Msg);
            //    return false;
            //}
            if (Utility.sDbnull(txtSoHoso.Text) == "")
            {
                Msg = "Bạn phải nhập mã hồ sơ theo dõi";
                ActivateMsg(txtSoHoso);
                txtSoHoso.Focus();
                return false;
            }
            DataTable dtData = new Select().From(EmrHosoTheodoiSosinh.Schema)
              .Where(EmrHosoTheodoiSosinh.Columns.MaPhieu).IsEqualTo(Utility.DoTrim(txtSoHoso.Text))
              .And(EmrHosoTheodoiSosinh.Columns.Id).IsNotEqualTo(Utility.Int64Dbnull(txtId.Text, -1))
              .ExecuteDataSet().Tables[0];
            if (dtData.Rows.Count > 0)
            {
                Msg = "Mã phiếu theo dõi sơ sinh đã được sử dụng cho em bé khác. Vui lòng nhập mã phiếu khác. Hoặc nhấn nút refresh bên cạnh để sinh mã mới";
                txtSoHoso.Focus();
                return false;
            }
            
            if (Utility.sDbnull(txt_maso_embe.Text) == "")
            {
                Msg = "Bạn phải nhập mã trẻ sơ sinh";
                ActivateMsg(txt_maso_embe);
                txt_maso_embe.Focus();
                return false;
            }
             dtData = new Select().From(EmrHosoTheodoiSosinh.Schema)
              .Where(EmrHosoTheodoiSosinh.Columns.MasoTresosinh).IsEqualTo(Utility.DoTrim(txt_maso_embe.Text))
              .And(EmrHosoTheodoiSosinh.Columns.Id).IsNotEqualTo(Utility.Int64Dbnull(txtId.Text, -1))
              .ExecuteDataSet().Tables[0];
            if (dtData.Rows.Count > 0)
            {
                Msg = string.Format("Mã trẻ sơ sinh {0} đã được sử dụng cho em bé khác. Vui lòng nhập mã khác. Hoặc nhấn nút refresh bên cạnh để sinh mã mới", Utility.DoTrim(txt_maso_embe.Text));
                txt_maso_embe.Focus();
                return false;
            }
            if (Utility.sDbnull(txt_hoten_be.Text) == "")
            {
                Msg = "Bạn phải nhập thông tin Họ tên bé";
                ActivateMsg(txt_hoten_be);
                txt_hoten_be.SelectAll();
                txt_hoten_be.Focus();
                return false;
            }

            if (dtp_ngaysinh_be.Text == "")
            {
                Msg = "Phải nhập ngày sinh của bé";
                ActivateMsg(dtp_ngaysinh_be);
                dtp_ngaysinh_be.Focus();
                return false;
            }
            if (dtp_ngaysinh_be.Value.Date>DateTime.Now.Date)
            {
                Msg = "Ngày sinh của bé phải <= ngày hiện tại";
                ActivateMsg(dtp_ngaysinh_be);
                dtp_ngaysinh_be.Focus();
                return false;
            }
            if (dtp_ngaysinh_be.Value.Hour + dtp_ngaysinh_be.Value.Minute <= 0)
            {
                Msg = "Giờ phút lúc sinh bé phải có một thông tin khác 0";
                ActivateMsg(dtp_ngaysinh_be);
                dtp_ngaysinh_be.Focus();
                return false;
            }
            if (!opt_nam.Checked && !opt_nu.Checked)
            {
                Msg = "Bạn phải nhập giới tính bé";
                ActivateMsg(opt_nam);
                opt_nam.Focus();
                return false;
            }
            //if (Utility.sDbnull(txtDantoc.Text) == "")
            //{
            //    Msg = "Bạn phải nhập dân tộc";
            //    if (_OnMsg != null) _OnMsg(Msg);
            //    txtDantoc.SelectAll();
            //    txtDantoc.Focus();
            //    return false;
            //}
            if (Utility.sDbnull(txt_hoten_bo.Text) == "")
            {
                Msg = "Bạn phải nhập thông tin Họ tên Bố";
                ActivateMsg(txt_hoten_bo);
                txt_hoten_bo.SelectAll();
                txt_hoten_bo.Focus();
                return false;
            }
            if (dtp_ngaysinh_bo.Text == "")
            {
                Msg = "Phải nhập ngày sinh bố";
                ActivateMsg(dtp_ngaysinh_bo);
                dtp_ngaysinh_bo.Focus();
                return false;
            }
            if (dtp_ngaysinh_be.Value.Date > DateTime.Now.Date)
            {
                Msg = "Ngày sinh của bố phải <= ngày hiện tại";
                ActivateMsg(dtp_ngaysinh_be);
                dtp_ngaysinh_be.Focus();
                return false;
            }
            if (dtp_ngaysinh_be.Value.Date < dtp_ngaysinh_bo.Value.Date)
            {
                Msg = "Ngày sinh của bé phải sau ngày sinh của bố";
                ActivateMsg(dtp_ngaysinh_bo);
                dtp_ngaysinh_bo.Focus();
                return false;
            }

            
            if (txt_dieuduong.MyID == "-1")
            {
                uiTab1.SelectedTab = uiTabPage1;
                Msg = "Phải nhập người đỡ đẻ cho bé";
                ActivateMsg(txt_dieuduong);
                txt_dieuduong.Focus();
                return false;
            }
            if(nmr_tuoithai_dukien.Value<=0)
            {
                Msg = "Tuổi thai dự kiến phải >0";
                ActivateMsg(nmr_tuoithai_dukien);
                nmr_tuoithai_dukien.Focus();
                return false;
            }
            if (nmr_tuoithai_danhgia.Value <= 0)
            {
                Msg = "Tuổi thai theo đánh giá lâm sàng phải >0";
                ActivateMsg(nmr_tuoithai_danhgia);
                nmr_tuoithai_danhgia.Focus();
                return false;
            }
            if (dtp_oivo_luc.Text == "")
            {
                Msg = "Phải nhập thời điểm vỡ ối";
                ActivateMsg(dtp_oivo_luc);
                dtp_oivo_luc.Focus();
                return false;
            }
            if (dtp_oivo_luc.Value.Date > DateTime.Now.Date)
            {
                Msg = "Ngày vỡ ối phải <= ngày hiện tại";
                ActivateMsg(dtp_oivo_luc);
                dtp_oivo_luc.Focus();
                return false;
            }
            if (dtp_oivo_luc.Value.Hour+ dtp_oivo_luc.Value.Minute<=0)
            {
                Msg = "Giờ phút lúc ối vỡ phải có một thông tin khác 0";
                ActivateMsg(dtp_oivo_luc);
                dtp_oivo_luc.Focus();
                return false;
            }
            if (dtp_ngaysinh_be.Value.Date < dtp_oivo_luc.Value.Date)
            {
                Msg = "Thời điểm ối vỡ phải trước thời điểm sinh của bé";
                ActivateMsg(dtp_ngaysinh_bo);
                dtp_ngaysinh_bo.Focus();
                return false;
            }
            if (txt_tresosinh_cannang.Text=="" || Utility.DecimaltoDbnull(txt_tresosinh_cannang.Text)<=0)
            {
                Msg = "Phải nhập thông tin cân nặng bé";
                ActivateMsg(txt_tresosinh_cannang);
                txt_tresosinh_cannang.Focus();
                return false;
            }
            if (txt_tresosinh_cao.Text == "" || Utility.DecimaltoDbnull(txt_tresosinh_cao.Text) <= 0)
            {
                Msg = "Phải nhập thông tin chiều dài bé";
                ActivateMsg(txt_tresosinh_cao);
                txt_tresosinh_cao.Focus();
                return false;
            }
            if (txt_tresosinh_vongdau.Text == "" || Utility.DecimaltoDbnull(txt_tresosinh_vongdau.Text) <= 0)
            {
                Msg = "Phải nhập thông tin vòng đầu bé";
                ActivateMsg(txt_tresosinh_vongdau);
                txt_tresosinh_vongdau.Focus();
                return false;
            }
            if (txt_bacsi.MyID == "-1")
            {
                uiTab1.SelectedTab = uiTabPage2;
                Msg = "Phải nhập Bác sĩ khám";
                ActivateMsg(txt_bacsi);
                txt_bacsi.Focus();
                return false;
            }
            if (trang_thai == 1)
            {
                if (txt_bacsy_kham.MyID == "-1")
                {
                    uiTab1.SelectedTab = uiTabPage4;
                    Msg = "Phải nhập Bác sĩ khám ra viện";
                    ActivateMsg(txt_bacsi);
                    txt_bacsi.Focus();
                    return false;
                }
            }
            return true;
        }
        EmrDocuments emrdoc = new EmrDocuments();
        public bool Save(int trang_thai)
        {
            try
            {
                bool isNew = true;
                if (!isValidData(trang_thai)) return false;
                DateTime? dtp=null;
                Msg = "";
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                       
                       
                        if (_phieu == null || _phieu.Id <= 0)
                        {
                            isNew = true;
                            _phieu = new EmrHosoTheodoiSosinh();
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
                        //_phieu.IdGiaychungsinh = id_giaychungsinh;
                        _phieu.MaPhieu = Utility.sDbnull(txtSoHoso.Text);
                        _phieu.MasoTresosinh = Utility.sDbnull(txt_maso_embe.Text);
                        _phieu.NgayPhieu = dtp_ngayphieu.Value;
                        _phieu.IdKhoa = Utility.Int32Dbnull(txt_khoa.MyID);
                        _phieu.TenKhoa = Utility.sDbnull(txt_khoa.Text);
                        _phieu.Buong = Utility.sDbnull(txt_buong.Text);
                        _phieu.Giuong = Utility.sDbnull(txt_giuong.Text);

                        // Thông tin bé & cha mẹ
                        _phieu.HotenBe = Utility.sDbnull(txt_hoten_be.Text);
                        _phieu.NgaysinhBe = dtp_ngaysinh_be.Value;
                        _phieu.MaDantoc = txtDantoc.myCode;
                        _phieu.TenDantoc = txtDantoc.Text;
                        _phieu.GioitinhNam = opt_nam.Checked;
                        _phieu.GioitinhNu = opt_nu.Checked;
                        _phieu.NgoaiKieu = chk_ngoaikieu.Checked ? (byte)1 : (byte)0;
                        _phieu.HotenBo = Utility.sDbnull(txt_hoten_bo.Text);
                        _phieu.NgaysinhBo = dtp_ngaysinh_bo.Value;
                        _phieu.NghenghiepBo = Utility.sDbnull(txt_nghenghiep.Text);
                        _phieu.XnNhommau = Utility.sDbnull(txtNhommau.Text);
                        _phieu.DaithaoduongCo = opt_daithaoduong_co.Checked;
                        _phieu.DaithaoduongKhong = opt_daithaoduong_khong.Checked;

                        // Xét nghiệm, truyền nhiễm
                        _phieu.HbsagAmtinh = opt_hbsag_amtinh.Checked;
                        _phieu.HbsagDuongtinh = opt_hbsag_duongtinh.Checked;
                        _phieu.GiangmaiAmtinh = opt_giangmai_amtinh.Checked;
                        _phieu.GiangmaiDuongtinh = opt_giangmai_duongtinh.Checked;
                        _phieu.HivAmtinh = opt_hiv_amtinh.Checked;
                        _phieu.HivDuongtinh = opt_hiv_duongtinh.Checked;
                        _phieu.GbsAmtinh = opt_gbs_amtinh.Checked;
                        _phieu.GbsDuongtinh = opt_gbs_duongtinh.Checked;

                        // Thông tin khám / nhân sự
                        _phieu.NgayKham = dtp_ngaykham.Value;
                        _phieu.IdBacsy =Utility.Int32Dbnull( txt_bacsi.MyID);
                        _phieu.IdBacsyKham = Utility.Int32Dbnull(txt_bacsy_kham.MyID);
                        _phieu.IdDieuduong = Utility.Int32Dbnull(txt_dieuduong.MyID);
                        _phieu.IdNguoiChamsoc = Utility.Int32Dbnull(txt_nguoichamsoc.MyID);
                        _phieu.IdNguoithuchienHiv = Utility.Int32Dbnull(txt_nguoichidinh_duphong_HIV.MyID);
                        _phieu.IdNguoiSangloc = Utility.Int32Dbnull(txt_nguoisangloc.MyID);
                        _phieu.IdNguoitiem = Utility.Int32Dbnull(txt_nguoitiem.MyID);
                        _phieu.IdNguoitiemLao = Utility.Int32Dbnull(txt_nguoitiemphonglao.MyID);
                        _phieu.IdNguoithuchienTiemviemganB = Utility.Int32Dbnull(txt_nguoitiemviemganB.MyID);

                        // Hộ sinh / điều dưỡng
                        _phieu.Para = Utility.sDbnull(txt_para.Text);
                        _phieu.SotTruocsinhMota = Utility.sDbnull(txt_sot_truocsinh.Text);
                        _phieu.TuoithaiDukien = Utility.ByteDbnull(nmr_tuoithai_dukien.Text);
                        _phieu.TuoithaiDanhgia = Utility.ByteDbnull(nmr_tuoithai_danhgia.Text);

                        _phieu.PhuongphapdeThuong = opt_phuongphapde_thuong.Checked;
                        _phieu.PhuongphapdeMo = opt_phuongphapde_mo.Checked;
                        _phieu.PhuongphapdeMochudong = opt_phuongphapde_mochudong.Checked;
                        _phieu.PhuongphapdeGiachut = opt_phuongphapde_giachut.Checked;
                        _phieu.PhuongphapdeForceps = opt_phuongphapde_forceps.Checked;
                        _phieu.PhuongphapdeChihuy = opt_phuongphapde_chihuy.Checked;

                        _phieu.VitriDattreDakeda = opt_vitri_dattre_dakeda.Checked;
                        _phieu.VitriDattreGiuongsuoi = opt_vitri_dattre_giuongsuoi.Checked;
                        _phieu.VitriDattreKhac = opt_vitri_dattre_khac.Checked;

                        _phieu.OivoLuc = dtp_oivo_luc.Value;
                        _phieu.NuocOiTrong = opt_nuoc_oi_trong.Checked;
                        _phieu.NuocOiXanhban = opt_nuoc_oi_xanhban.Checked;
                        _phieu.NuocOiLanmau = opt_nuoc_oi_lanmau.Checked;
                        _phieu.NuocOiMota = Utility.sDbnull(txt_nuoc_oi_mota.Text);

                        _phieu.CatRonMuon = Utility.Int16Dbnull(nmr_cat_ron_muon.Text);
                        _phieu.MauRon = Utility.sDbnull(txt_mauron.Text);
                        _phieu.BumeCo = opt_bume_co.Checked;
                        _phieu.BumeKhong = opt_bume_khong.Checked;

                        _phieu.DakedaKhongthuchien = opt_dakeda_khongthuchien.Checked;
                        _phieu.DakedaTrong30 = opt_dakeda_trong30.Checked;
                        _phieu.Dakeda30den90 = opt_dakeda_30den90.Checked;
                        _phieu.DakedaTu90 = opt_dakeda_tu_90.Checked;

                        _phieu.TresosinhCannang = Utility.Int16Dbnull(txt_tresosinh_cannang.Text);
                        _phieu.TresosinhCao = Utility.Int16Dbnull(txt_tresosinh_cao.Text);
                        _phieu.TresosinhVongdau = Utility.Int16Dbnull(txt_tresosinh_vongdau.Text);

                        // APGAR / khám BS
                        _phieu.DitatBamsinhCo = opt_ditat_bamsinh_co.Checked;
                        _phieu.DitatBamsinhKhong = opt_ditat_bamsinh_khong.Checked;
                        _phieu.DitatBamsinhMota = opt_ditat_bamsinh_co.Checked? Utility.sDbnull(txt_ditat_bamsinh_mota.Text):"";

                        _phieu.HoisucCo = opt_hoisuc_co.Checked;
                        _phieu.HoisucKhong = opt_hoisuc_khong.Checked;
                        _phieu.HoisucCoMota = opt_hoisuc_co.Checked? Utility.sDbnull(txt_hoisuc_co_mota.Text):"";

                        // Hô hấp hỗ trợ / thời gian
                        _phieu.ThoOxy = chk_tho_oxy.Checked;
                        _phieu.ThoigianThoOxy = Utility.ByteDbnull(nmr_thoigian_tho_oxy.Value);

                        // Bóp bóng / thuốc
                        _phieu.BopBong =chk_bop_bong.Checked; // giữ nguyên vì trước gán Text
                        _phieu.ThoigianBopBong = Utility.ByteDbnull(nmr_thoigian_bop_bong.Text);
                        _phieu.Thuoc = Utility.sDbnull(txt_thuoc.Text);

                        // SPO2
                        _phieu.Spo2Co = opt_spo2_co.Checked;
                        _phieu.Spo2Khong = opt_spo2_khong.Checked;
                        _phieu.Spo2 = opt_spo2_co.Checked?Utility.sDbnull(nmr_spo2.Text):"";

                        // KQ đo đạc
                        _phieu.KetquaNhiptim = Utility.sDbnull(txt_ketqua_nhiptim.Text);
                        _phieu.KetquaNhiptho = Utility.sDbnull(txt_keyqua_nhiptho.Text);
                        _phieu.KetquaNhietdo = Utility.sDbnull(txt_ketqua_nhietdo.Text);

                        // Màu da
                        _phieu.MausacdaHonghao = chk_mausacda_honghao.Checked;
                        _phieu.MausacdaXanhtai = chk_mausacda_xanhtai.Checked;
                        _phieu.MausacdaVang = chk_mausacda_vang.Checked;
                        _phieu.MausacdaTim = chk_mausacda_tim.Checked;
                        _phieu.MausacdaKhac = chk_mausacda_khac.Checked;
                        _phieu.MausacdaMota = chk_mausacda_khac.Checked? Utility.sDbnull(txt_mausacda_mota.Text):"";

                        // Kết quả & chuyển đơn vị
                        _phieu.KetquaOndinhNamcungme = opt_ketqua_ondinh_namcungme.Checked;
                        _phieu.Cantheodothem = opt_cantheodothem.Checked;
                        _phieu.Canchuyendonvisosinh = opt_canchuyendonvisosinh.Checked;

                        // Chăm sóc sơ sinh tiếp
                        _phieu.TiemVitamink1Co = opt_tiem_vitamink1_co.Checked;
                        _phieu.TiemVitamink1Khong = opt_tiem_vitamink1_khong.Checked;
                        _phieu.TiemVitamink1Uong = opt_tiem_vitamink1_co.Checked? chk_tiem_vitamink1_uong.Checked:false;
                        _phieu.TiemVitamink1UongLieudung = _phieu.TiemVitamink1Uong .Value? Utility.sDbnull(txt_tiem_vitamink1_uong_lieudung.Text):"";
                        _phieu.TiemVitamink1Tiembap = opt_tiem_vitamink1_co.Checked ? chk_tiem_vitamink1_tiembap.Checked:false; // lưu flag từ checkbox (như trước)
                        _phieu.TiemVitamink1TiembapLieudung = _phieu.TiemVitamink1Tiembap .Value? Utility.sDbnull(txt_tiem_vitamink1_tiembap_lieudung.Text):"";

                        _phieu.NgayTiem = _phieu.TiemVitamink1Co.Value?(dtp_ngay_tiem.Value == null ? dtp : dtp_ngay_tiem.Value):dtp;
                        _phieu.IdNguoitiem = _phieu.TiemVitamink1Co.Value ? Utility.Int32Dbnull(txt_nguoitiem.MyID):0;

                        _phieu.TiemphongLaoCo = opt_tiemphong_lao_co.Checked;
                        _phieu.TiemphongLaoKhong = opt_tiemphong_lao_khong.Checked;
                        _phieu.NgayTiemLao = _phieu.TiemphongLaoCo .Value? dtp: (dtp_ngay_tiem_lao.Value == null ? dtp : dtp_ngay_tiem_lao.Value);
                        _phieu.IdNguoitiemLao = _phieu.TiemphongLaoCo.Value ? 0:Utility.Int32Dbnull(txt_nguoitiemphonglao.MyID);

                        _phieu.ViemganBCo = opt_viemganB_co.Checked;
                        _phieu.ViemganBKhong = opt_viemganB_khong.Checked;
                        _phieu.TiemViemganB = _phieu.ViemganBCo.Value? chk_tiemviemganB.Checked:false;
                        _phieu.TiemViemganBLieudung = _phieu.TiemViemganB .Value? Utility.sDbnull(txt_tiem_viemganB_lieudung.Text):"";
                        _phieu.TiemHbig = _phieu.ViemganBCo.Value ? chk_tiemHBIG.Checked : false;
                        _phieu.TiemHbigLieudung = _phieu.TiemHbig.Value? Utility.sDbnull(txt_tiem_HBIG_lieudung.Text):"";
                        _phieu.NgayTiemViemganB = opt_viemganB_co.Checked? (dtp_ngay_tiem_viemganB.Value == null ? dtp : dtp_ngay_tiem_viemganB.Value):dtp;
                        _phieu.IdNguoithuchienTiemviemganB = opt_viemganB_co.Checked?Utility.Int32Dbnull(txt_nguoitiemviemganB.MyID):0;

                        _phieu.HivChidinhDuphongCo = opt_hiv_chidinh_duphong_co.Checked;
                        _phieu.HivChidinhDuphongKhong = opt_hiv_chidinh_duphong_khong.Checked;
                        _phieu.HivLoaithuoc = _phieu.HivChidinhDuphongCo .Value? Utility.sDbnull(txt_hiv_loaithuoc.Text):"";
                        _phieu.HivLieudung = _phieu.HivChidinhDuphongCo.Value ? Utility.sDbnull(txt_hiv_lieudung.Text):"";
                        _phieu.HivNgay = _phieu.HivChidinhDuphongCo .Value? (dtp_ngaychidinh_duphong_HIV.Value == null ? dtp : dtp_ngaychidinh_duphong_HIV.Value):dtp;
                        _phieu.IdNguoithuchienHiv = _phieu.HivChidinhDuphongCo .Value? Utility.Int32Dbnull(txt_nguoichidinh_duphong_HIV.MyID):0;

                        _phieu.SanglocSosinhCo = opt_sangloc_sosinh_co.Checked;
                        _phieu.SanglocSosinhKhong = opt_sangloc_sosinh_khong.Checked;
                        _phieu.SanglocSosinhMaugotchan = _phieu.SanglocSosinhCo.Value? chk_sangloc_sosinh_maugotchan.Checked:false;
                        _phieu.SanglocSosinhKhac = _phieu.SanglocSosinhCo.Value ? Utility.sDbnull(txt_sangloc_sosinh_khac.Text):"";
                        _phieu.SanglocSosinhKetquasangloc = _phieu.SanglocSosinhCo.Value ? Utility.sDbnull(txt_ketqua_sangloc.Text):"";
                        _phieu.KqSanglocBinhthuong = opt_kq_sangloc_binhthuong.Checked;
                        _phieu.KqSanglocBatthuong = opt_kq_sangloc_batthuong.Checked;
                        _phieu.KqSanglocBatthuongMota = opt_sangloc_sosinh_co.Checked && opt_kq_sangloc_batthuong.Checked ? Utility.sDbnull(txt_kq_sangloc_mota.Text):"";

                        _phieu.NgaySangloc = opt_sangloc_sosinh_co.Checked?(dtp_ngay_sangloc.Value == null ? dtp : dtp_ngay_sangloc.Value):dtp;
                        _phieu.IdNguoiSangloc = opt_sangloc_sosinh_co.Checked? Utility.Int32Dbnull(txt_nguoisangloc.MyID):0;

                        _phieu.ChamsocKangarooKhong = opt_chamsoc_kangaroo_khong.Checked;
                        _phieu.ChamsocKangarooNgatquang = opt_chamsoc_kangaroo_ngatquang.Checked;
                        _phieu.ChamsocKangarooColientuc24gio = opt_chamsoc_kangaroo_colientuc24gio.Checked;

                        _phieu.NgayChamsoc = _phieu.ChamsocKangarooKhong.Value? dtp:(dtp_ngay_chamsoc.Value == null ? (DateTime?)null : dtp_ngay_chamsoc.Value);
                        _phieu.IdNguoiChamsoc = _phieu.ChamsocKangarooKhong.Value ? 0 : Utility.Int32Dbnull(txt_nguoichamsoc.MyID);

                        // Khám ra viện - tuổi
                        _phieu.Tuoi24 = opt_tuoi_24.Checked;
                        _phieu.Tuoi2448 = opt_tuoi_24_48.Checked;
                        _phieu.Tuoi4872 = opt_tuoi_48_72.Checked;
                        _phieu.Tuoi72 = opt_tuoi_72.Checked;

                        // Bú mẹ
                        _phieu.BumeHoantoan = opt_bume_hoantoan.Checked;
                        _phieu.BumeMotphan = opt_bume_motphan.Checked;
                        _phieu.BumeAnsuaCongthuchoantoan = opt_bume_ansua_congthuchoantoan.Checked;

                        // Da
                        _phieu.DaBinhthuong = opt_da_binhthuong.Checked;
                        _phieu.DaBatthuong = opt_da_batthuong.Checked;
                        _phieu.DaMota = opt_da_batthuong.Checked ? Utility.sDbnull(txt_da_mota.Text) : "";

                        // Đầu
                        _phieu.DauBinhthuong = opt_dau_binhthuong.Checked;
                        _phieu.DauBatthuong = opt_dau_batthuong.Checked;
                        _phieu.DauMota = opt_dau_batthuong.Checked ? Utility.sDbnull(txt_dau_mota.Text) : "";

                        // Tai - mũi - họng
                        _phieu.TaimuihongBatthuong = opt_taimuihong_batthuong.Checked;
                        _phieu.TaimuihongBinhthuong = opt_taimuihong_binhthuong.Checked;
                        _phieu.TaimuihongMota = opt_taimuihong_batthuong.Checked ? Utility.sDbnull(txt_taimuihong_mota.Text) : "";

                        // Hô hấp
                        _phieu.HohapBatthuong = opt_hohap_batthuong.Checked;
                        _phieu.HohapBinhthuong = opt_hohap_binhthuong.Checked;
                        _phieu.HohapMota = opt_hohap_batthuong.Checked ? Utility.sDbnull(txt_hohap_mota.Text) : "";

                        // Tim mạch
                        _phieu.TimmachBatthuong = opt_timmach_batthuong.Checked;
                        _phieu.TimmachBinhthuong = opt_timmach_binhthuong.Checked;
                        _phieu.TimmachMota = opt_timmach_batthuong.Checked ? Utility.sDbnull(txt_timmach_mota.Text) : "";

                        // Tiêu hóa
                        _phieu.TieuhoaBatthuong = opt_tieuhoa_batthuong.Checked;
                        _phieu.TieuhoaBinhthuong = opt_tieuhoa_binhthuong.Checked;
                        _phieu.TieuhoaMota = opt_tieuhoa_batthuong.Checked ? Utility.sDbnull(txt_tieuhoa_mota.Text) : "";

                        // Rốn
                        _phieu.CuongronBatthuong = opt_cuongron_batthuong.Checked;
                        _phieu.CuongronBinhthuong = opt_cuongron_binhthuong.Checked;
                        _phieu.CuongronMota = opt_cuongron_batthuong.Checked ? Utility.sDbnull(txt_cuongron_mota.Text) : "";

                        // Sinh dục
                        _phieu.SinhducTietnieuBatthuong = opt_sinhduc_tietnieu_batthuong.Checked;
                        _phieu.SinhducTietnieuBinhthuong = opt_sinhduc_tietnieu_binhthuong.Checked;
                        _phieu.SinhducTietnieuMota = opt_sinhduc_tietnieu_batthuong.Checked ? Utility.sDbnull(txt_sinhduc_tietnieu_mota.Text) : "";
                        // Thần kinh
                        _phieu.ThankinhBinhthuong = opt_thankinh_binhthuong.Checked;
                        _phieu.ThankinhBatthuong = opt_thankinh_batthuong.Checked;
                        _phieu.ThankinhMota = opt_thankinh_batthuong.Checked?Utility.sDbnull(txt_thankinh_mota.Text):"";

                        // Cơ xương khớp
                        _phieu.CoxuongkhopBatthuong = opt_coxuongkhop_batthuong.Checked;
                        _phieu.CoxuongkhopBinhthuong = opt_coxuongkhop_binhthuong.Checked;
                        _phieu.CoxuongkhopMota = opt_coxuongkhop_batthuong.Checked ? Utility.sDbnull(txt_coxuongkhop_mota.Text) : "";

                        // Khám ra viện khác / tái khám
                        _phieu.KhamravienKhac = Utility.sDbnull(txt_khamravien_khac.Text);
                        _phieu.Cantheodoitiep = Utility.sDbnull(txt_cantheodoitiep.Text);
                        _phieu.NgayhenTaikham = (dtp_ngayhen_taikham.Value == null) ? (DateTime?)null : dtp_ngayhen_taikham.Value;
                        //Set các giá trị Agfa 1 ,5,10 phút
                        UpdateAgFa2Object();
                        _phieu.Save();
                        emrdoc.Force2Saved = Force2Saved;
                        emrdoc.InitDocument(_phieu.IdBenhnhan, _phieu.MaLuotkham, Utility.Int64Dbnull(_phieu.Id), _phieu.NgayPhieu.Value, Loaiphieu_HIS.HOSOTHEODOI_SOSINH, "HOSOTHEODOI_SOSINH", _phieu.NguoiTao,Utility.Int16Dbnull( txt_khoa.MyID), -1, Utility.Byte2Bool(0),"",false,false,"",Loaiphieu_HIS.HOSOTHEODOI_SOSINH);
                        emrdoc.Save();
                       

                    }
                    scope.Complete();
                }
                txtId.Text = _phieu.Id.ToString();
                isAllowSelectionChanged = true;
                if (_OnStatus != null) _OnStatus(isNew);
                OnChangedData(_phieu.Id, m_enAct);
                Msg = "Lưu thông tin thành công";
                if (_OnMsg != null) _OnMsg(Msg, true);
                isAllowSelectionChanged = true;
                cmdHuy.PerformClick();
                return true;
            }
            catch (System.Exception ex)
            {
                if (_OnMsg != null) _OnMsg(ex.Message);
                Utility.CatchException(ex);
                return false;
            }
        }



        public void Print()
        {
            WordPrinter.InHosoTheodoiSosinh(mv_id_phieu, false);
        }

        private void cmdTuSinh_Click(object sender, EventArgs e)
        {
            txtSoHoso.Text = THU_VIEN_CHUNG.TT25LaySohoso(8);
        }

       
        private void opt_dau_co_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void opt_da_batthuong_CheckedChanged(object sender, EventArgs e)
        {

            txt_da_mota.Enabled = opt_da_batthuong.Checked;
            txt_da_mota.Focus();
        }

        private void opt_dau_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            txt_dau_mota.Enabled = opt_dau_batthuong.Checked;
            txt_dau_mota.Focus();
        }

        private void opt_taimuihong_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            txt_taimuihong_mota.Enabled = opt_taimuihong_batthuong.Checked;
            txt_taimuihong_mota.Focus();
        }

        private void opt_hohap_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            txt_hohap_mota.Enabled = opt_hohap_batthuong.Checked;
            txt_hohap_mota.Focus();
        }

        private void opt_timmach_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            txt_timmach_mota.Enabled = opt_timmach_batthuong.Checked;
            txt_timmach_mota.Focus();
        }

        private void opt_tieuhoa_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            txt_tieuhoa_mota.Enabled = opt_tieuhoa_batthuong.Checked;
            txt_tieuhoa_mota.Focus();
        }

        private void opt_cuongron_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            txt_cuongron_mota.Enabled = opt_cuongron_batthuong.Checked;
            txt_cuongron_mota.Focus();
        }

        private void opt_sinhduc_tietnieu_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            txt_sinhduc_tietnieu_mota.Enabled = opt_sinhduc_tietnieu_batthuong.Checked;
            txt_sinhduc_tietnieu_mota.Focus();
        }

        private void opt_thankinh_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            txt_thankinh_mota.Enabled = opt_thankinh_batthuong.Checked;
            txt_thankinh_mota.Focus();
        }

        private void opt_coxuongkhop_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            txt_coxuongkhop_mota.Enabled = opt_coxuongkhop_batthuong.Checked;
            txt_coxuongkhop_mota.Focus();
        }

        private void opt_tiem_vitamink1_co_CheckedChanged(object sender, EventArgs e)
        {
            chk_tiem_vitamink1_uong.Enabled = chk_tiem_vitamink1_tiembap.Enabled = chk_tiem_vitamink1_uong.Checked = chk_tiem_vitamink1_tiembap.Checked = dtp_ngay_tiem.Enabled =txt_nguoitiem.Enabled= opt_tiem_vitamink1_co.Checked;
            
        }

        private void opt_tiemphong_lao_co_CheckedChanged(object sender, EventArgs e)
        {
            dtp_ngay_tiem_lao.Enabled = txt_nguoitiemphonglao.Enabled = opt_tiemphong_lao_co.Checked;
        }

        private void opt_viemganB_co_CheckedChanged(object sender, EventArgs e)
        {
         chk_tiemviemganB.Enabled=chk_tiemHBIG.Enabled= chk_tiemviemganB.Checked = chk_tiemHBIG.Checked = dtp_ngay_tiem_viemganB.Enabled = txt_nguoitiemviemganB.Enabled = opt_viemganB_co.Checked;
        }

        private void opt_hiv_chidinh_duphong_co_CheckedChanged(object sender, EventArgs e)
        {
            dtp_ngaychidinh_duphong_HIV.Enabled = txt_nguoichidinh_duphong_HIV.Enabled = txt_hiv_loaithuoc.Enabled = txt_hiv_lieudung.Enabled = opt_hiv_chidinh_duphong_co.Checked;
        }

        private void opt_sangloc_sosinh_co_CheckedChanged(object sender, EventArgs e)
        {
            chk_sangloc_sosinh_maugotchan.Enabled = chk_sangloc_sosinh_maugotchan.Checked = txt_sangloc_sosinh_khac.Enabled = txt_ketqua_sangloc.Enabled =  opt_sangloc_sosinh_co.Checked;
            dtp_ngay_sangloc.Enabled = txt_nguoisangloc.Enabled = chk_sangloc_sosinh_maugotchan.Enabled;
        }

        private void opt_kq_sangloc_batthuong_CheckedChanged(object sender, EventArgs e)
        {
            txt_kq_sangloc_mota.Enabled = opt_kq_sangloc_batthuong.Checked;
        }

        private void opt_chamsoc_kangaroo_ngatquang_CheckedChanged(object sender, EventArgs e)
        {
            dtp_ngay_chamsoc.Enabled = txt_nguoichamsoc.Enabled = opt_chamsoc_kangaroo_ngatquang.Checked;
        }

        private void opt_chamsoc_kangaroo_colientuc24gio_CheckedChanged(object sender, EventArgs e)
        {
            dtp_ngay_chamsoc.Enabled = txt_nguoichamsoc.Enabled = opt_chamsoc_kangaroo_colientuc24gio.Checked;
        }

        private void opt_ditat_bamsinh_co_CheckedChanged(object sender, EventArgs e)
        {
            txt_ditat_bamsinh_mota.Enabled = opt_ditat_bamsinh_co.Checked;
            txt_ditat_bamsinh_mota.Focus();
        }

        private void opt_hoisuc_co_CheckedChanged(object sender, EventArgs e)
        {
            txt_hoisuc_co_mota.Enabled = opt_hoisuc_co.Checked;
            txt_hoisuc_co_mota.Focus();
        }

        private void chk_mausacda_khac_CheckedChanged(object sender, EventArgs e)
        {
            txt_mausacda_mota.Enabled = chk_mausacda_khac.Checked;
            txt_mausacda_mota.Focus();
      
        }

        private void chk_tiem_vitamink1_uong_CheckedChanged(object sender, EventArgs e)
        {
            txt_tiem_vitamink1_uong_lieudung.Enabled = chk_tiem_vitamink1_uong.Checked;
           if(chk_tiem_vitamink1_uong.Checked) txt_tiem_vitamink1_uong_lieudung.Focus();
        }

        private void chk_tiem_vitamink1_tiembap_CheckedChanged(object sender, EventArgs e)
        {
            txt_tiem_vitamink1_tiembap_lieudung.Enabled = chk_tiem_vitamink1_tiembap.Checked;
            if (chk_tiem_vitamink1_tiembap.Checked) txt_tiem_vitamink1_tiembap_lieudung.Focus();
        }

        private void chk_tiemviemganB_CheckedChanged(object sender, EventArgs e)
        {
            txt_tiem_viemganB_lieudung.Enabled = chk_tiemviemganB.Checked;
            if (chk_tiemviemganB.Checked) txt_tiem_viemganB_lieudung.Focus();
        }

        private void chk_tiemHBIG_CheckedChanged(object sender, EventArgs e)
        {
            txt_tiem_HBIG_lieudung.Enabled = chk_tiemHBIG.Checked;
            if (chk_tiemHBIG.Checked) txt_tiem_HBIG_lieudung.Focus();
        }

        private void label46_Click(object sender, EventArgs e)
        {

        }

        private void opt_nuoc_oi_lanmau_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void pnl3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chk_tho_oxy_CheckedChanged(object sender, EventArgs e)
        {
            nmr_thoigian_tho_oxy.Enabled = chk_tho_oxy.Checked;
        }

        private void chk_bop_bong_CheckedChanged(object sender, EventArgs e)
        {
            nmr_thoigian_bop_bong.Enabled = chk_bop_bong.Checked;
        }
        public action m_enAct = action.FirstOrFinished;
        void ModifyCommandButtons()
        {
            bool isValid = objLuotkham != null;
            bool isValid2 = Utility.isValidGrid(grdList);
            cmdSua.Enabled =  isValid && isValid2 && m_enAct == action.FirstOrFinished;
            cmdxoa.Enabled = isValid && isValid2 && m_enAct == action.FirstOrFinished;
            cmdIn.Enabled = isValid && isValid2;
            cmdthemmoi.Enabled= cmd_duplicate.Enabled = grdList.Enabled = isValid && m_enAct == action.FirstOrFinished;
            cmdGhi.Enabled = grd_bangdiem_apgar.Enabled = cmd_ketthuc_hoso.Enabled = m_enAct != action.FirstOrFinished;
            cmdHuy.Enabled = cmdGhi.Enabled;
            cmd_duplicate.Enabled = isValid && m_enAct == action.FirstOrFinished;
            grdList.Enabled= isValid && m_enAct == action.FirstOrFinished;
        }
        void ClearControl(Control parentCtrl)
        {
            errorProvider1.Clear();
            foreach (Control ctr in parentCtrl.Controls)
            {
                if (ctr.GetType().Equals(autoTxt.GetType()))
                    ((AutoCompleteTextbox_Danhmucchung)ctr).SetDefaultItem();
                else if (ctr is EditBox)
                {
                    ((EditBox)(ctr)).Clear();
                }
                else if (ctr is CheckBox)
                {
                    ((CheckBox)(ctr)).Checked = false;
                }
                else if (ctr is RadioButton)
                {
                    RadioButton opt = ctr as RadioButton;
                    if (Utility.sDbnull(opt) == "1")
                        opt.Checked = true;
                    else
                        opt.Checked = false;
                }
                else if (ctr is DateTimePicker)
                {
                    ((DateTimePicker)(ctr)).Value = globalVariables.SysDate;
                }
                else if (ctr is Janus.Windows.CalendarCombo.CalendarCombo)
                {
                    if (((Janus.Windows.CalendarCombo.CalendarCombo)(ctr)).IsNullDate)
                        ((Janus.Windows.CalendarCombo.CalendarCombo)(ctr)).ResetText();
                }
                if (ctr.Controls.Count > 0)
                    ClearControl(ctr);
            }
        }
        public bool isAllowSelectionChanged = true;
        private void cmdthemmoi_Click(object sender, EventArgs e)
        {
            ClearControl(this);
            LoadGridApgar(true);
            resetData();
            m_enAct = action.Insert;
            _phieu = null;
           
            ModifyCommandButtons();
            SinhMaPhieuMoi();
            SinhMaSoEmbeMoi();
            isAllowSelectionChanged = false;
            txt_hoten_be.Focus();
        }
        void SinhMaPhieuMoi()
        {
            txtSoHoso.Text = _phieu == null || string.IsNullOrEmpty(Utility.sDbnull(_phieu.MaPhieu, "")) ? THU_VIEN_CHUNG.TT25LaySohoso(8) : Utility.sDbnull(_phieu.MaPhieu, "");
        }
        void SinhMaSoEmbeMoi()
        {
            DataTable dtMaHoso = SPs.EmrHosoTheodoiSosinhSinhmaso(globalVariables.Ma_Coso, dtp_ngaysinh_be.Value.Year).GetDataSet().Tables[0];
            string ma_moi = dtMaHoso != null && dtMaHoso.Rows.Count > 0 ? dtMaHoso.Rows[0][0].ToString() : "";
            txt_maso_embe.Text = _phieu == null || string.IsNullOrEmpty(Utility.sDbnull(_phieu.MasoTresosinh, "")) ? ma_moi : Utility.sDbnull(_phieu.MasoTresosinh, "");
        }
        private void cmdxoa_Click(object sender, EventArgs e)
        {
            try
            {
                EmrHosoTheodoiSosinh _phieu = EmrHosoTheodoiSosinh.FetchByID(Utility.Int32Dbnull(grdList.GetValue(EmrHosoTheodoiSosinh.Columns.Id), -1));
                if (_phieu == null)
                {
                    Utility.ShowMsg(string.Format("Hồ sơ theo dõi sơ sinh của bé {0} con của sản phụ {1} có thể đã bị người khác xóa ở chức năng khác. Vui lòng bấm OK để hệ thống refresh lại dữ liệu", grdList.GetValue("hoten_be").ToString(), grdList.GetValue("ten_benhnhan").ToString()));
                    Init(objLuotkham, null);
                    return;
                }
                if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn xóa GCS có mã: {0} của bé {1} con của sản phụ {2} hay không?", grdList.GetValue(EmrGiayChungsinh.Columns.MasoGiaychungsinh).ToString(), grdList.GetValue("hoten_be").ToString(), grdList.GetValue("ten_benhnhan").ToString()), "Xác nhận xóa", true))
                {
                    if (DeleteMe())
                    {
                        Utility.ShowMsg(string.Format("Xóa Hồ sơ theo dõi sơ sinh cho bé {0} thành công", grdList.GetValue("hoten_be").ToString()));
                        DataRow[] arrDr = m_dtData.Select(string.Format("{0}={1}", EmrGiayChungsinh.Columns.Id, grdList.GetValue(EmrGiayChungsinh.Columns.Id)));
                        if (arrDr.Length > 0)
                            m_dtData.Rows.Remove(arrDr[0]);
                        m_dtData.AcceptChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

       

        private void cmdHuy_Click(object sender, EventArgs e)
        {
            m_enAct = action.FirstOrFinished;
            isAllowSelectionChanged = true;
            ModifyCommandButtons();
        }

        private void cmdIn_Click(object sender, EventArgs e)
        {
            Print();
        }

        private void cmdGhi_Click(object sender, EventArgs e)
        {
            Save(0);
        }

        private void cmd_duplicate_Click(object sender, EventArgs e)
        {
            if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn sao chép hồ sơ theo dõi sơ sinh có mã số {0} của bé {1} thành 1 phiếu khác.\nTính năng sao chép chỉ nên được dùng khi sản phụ sinh số con nhiều hơn 1 trong lần sinh này", Utility.sDbnull(grdList.GetValue("maso_tresosinh")), Utility.sDbnull(grdList.GetValue("hoten_be"))), "Xác nhận", true))
            {
                EmrHosoTheodoiSosinh _phieu = EmrHosoTheodoiSosinh.FetchByID(Utility.Int32Dbnull(grdList.GetValue(EmrHosoTheodoiSosinh.Columns.Id), -1));
                SinhMaPhieuMoi();
                SinhMaSoEmbeMoi();
                _phieu.IsNew = true;
                _phieu.MaPhieu = Utility.sDbnull(txtSoHoso.Text);
                _phieu.MasoTresosinh = Utility.sDbnull(txt_maso_embe.Text);
                _phieu.Save();
                Utility.ShowMsg("Đã sao chép phiếu thành công. Nhấn OK để kết thúc");
                OnChangedData(_phieu.Id, action.Insert);
            }
        }
        void OnChangedData(long id, action m_enAct)
        {
            try
            {
                DataTable dt_temp = SPs.EmrHosoTheodoiSosinhLaydanhsach(id, new DateTime(1900, 1, 1), new DateTime(1900, 1, 1), "", -1, "", "", "", "", 100).GetDataSet().Tables[0];
                if (m_enAct == action.Delete)
                {
                    if (DeleteMe())
                    {
                        DataRow[] arrDr = m_dtData.Select(string.Format("{0}={1}", EmrHosoTheodoiSosinh.Columns.Id, grdList.GetValue(EmrHosoTheodoiSosinh.Columns.Id)));
                        if (arrDr.Length > 0)
                            m_dtData.Rows.Remove(arrDr[0]);
                        m_dtData.AcceptChanges();
                    }
                }
                if (m_enAct == action.Insert && m_dtData != null && m_dtData.Columns.Count > 0 && dt_temp.Rows.Count > 0)
                {
                    m_dtData.ImportRow(dt_temp.Rows[0]);
                    return;
                }
                if (m_enAct == action.Update && m_dtData != null && m_dtData.Columns.Count > 0 && dt_temp.Rows.Count > 0)
                {
                    DataRow[] arrDr = m_dtData.Select("id=" + id);
                    if (arrDr.Length > 0)
                    {
                        foreach (DataColumn col in m_dtData.Columns)
                        {
                            arrDr[0][col.ColumnName] = dt_temp.Rows[0][col.ColumnName];
                        }

                    }
                    else
                        m_dtData.ImportRow(dt_temp.Rows[0]);

                }
                m_dtData.AcceptChanges();
                Utility.GotoNewRowJanus(grdList, EmrHosoTheodoiSosinh.Columns.Id, id.ToString());
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                ModifyCommandButtons();
            }
        }
        bool DeleteMe()
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        long IdPhieu = Utility.Int32Dbnull(grdList.GetValue(EmrHosoTheodoiSosinh.Columns.Id), -1);
                        new Delete().From(EmrHosoTheodoiSosinh.Schema).Where(EmrHosoTheodoiSosinh.Columns.Id).IsEqualTo(IdPhieu).Execute();
                        emrdoc.DeleteDocument(IdPhieu, Loaiphieu_HIS.HOSOTHEODOI_SOSINH, Loaiphieu_HIS.HOSOTHEODOI_SOSINH);
                        _phieu = null;
                        ClearControl(this);
                    }
                    scope.Complete();


                    return true;

                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                ModifyCommandButtons();
            }
        }

        private void cmd_laymaso_embe_Click(object sender, EventArgs e)
        {
            SinhMaSoEmbeMoi();
        }

        private void cmd_ketthuc_hoso_Click(object sender, EventArgs e)
        {
            Save(0);
        }

        private void cmd_update_Click(object sender, EventArgs e)
        {

        }

        private void cmdSua_Click(object sender, EventArgs e)
        {
            BeginUpdate();
            isAllowSelectionChanged = false;
        }
    }
}
