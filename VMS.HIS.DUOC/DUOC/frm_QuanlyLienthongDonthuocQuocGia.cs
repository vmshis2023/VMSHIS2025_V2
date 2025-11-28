using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using Newtonsoft.Json;

using SubSonic;

using NLog;
using VNS.Libs;
using VMS.HIS.DAL;
using VNS.HIS.UI.Forms.Dungchung;
using VNS.HIS.BusRule.Classes;
using System.IO;
using VNS.HIS.UI.Forms.Cauhinh;
using VNS.Libs.AppUI;
using VNS.HIS.NGHIEPVU.THUOC;

namespace VNS.HIS.UI.THUOC
{
    public partial class frm_QuanlyLienthongDonthuocQuocGia : Form
    {
        private DataTable dtData;
        private Logger log;
       
    
       
        string SplitterPath = "";
        public frm_QuanlyLienthongDonthuocQuocGia()
        {
            InitializeComponent();
            SplitterPath = string.Format(@"{0}\{1}.splitter", Application.StartupPath, this.Name);
            this.FormClosing += frm_QuanlyLienthongDonthuocQuocGia_FormClosing;
            this.Shown += frm_QuanlyLienthongDonthuocQuocGia_Shown;
            log = LogManager.GetCurrentClassLogger();
            Utility.SetVisualStyle(this);
            dtDenNgay.Value = dtTuNgay.Value = THU_VIEN_CHUNG.GetSysDateTime();
            txt_IdBenhnhan.KeyDown += Txt_IdBenhnhan_KeyDown;
            txtMaLanKham.KeyDown += TxtMaLanKham_KeyDown;
            grd_DonThuoc.SelectionChanged += Grd_DonThuoc_SelectionChanged;
        }

        private void Grd_DonThuoc_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grd_DonThuoc) || !isAllowChanged)
                {
                    grd_donthuoc_chitiet.DataSource = null;
                    return;
                }
                long id_donthuoc = Utility.Int64Dbnull(grd_DonThuoc.GetValue("id_donthuoc"));
                KcbLuotkham objLuotkham = Utility.getKcbLuotkham(grd_DonThuoc.CurrentRow);
                //KcbDonthuoc objDonthuoc = KcbDonthuoc.FetchByID(id_donthuoc);
                DataTable dt_chitiet = SPs.DonthuocXemtruockhiGui(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, 0, "THUOC",(int) id_donthuoc).GetDataSet().Tables[0];
                //DataTable dt_chitiet = SPs.DonthuocXemtruockhiGui(id_donthuoc).GetDataSet().Tables[0];
                List<string> lstmatinhchat = (from p in dt_chitiet.AsEnumerable()
                                              select Utility.sDbnull(p["ma_tinhchat"], "")).Distinct().ToList<string>();
                DataRow[] arrMatinhChat_Khongco = dt_chitiet.Select("ma_tinhchat='' or ma_tinhchat='-1' or ma_tinhchat is null");
                if (arrMatinhChat_Khongco.Length > 0)
                {
                    string danhsach_thuoc = string.Join(Environment.NewLine,
                           arrMatinhChat_Khongco.Select(r => r["ten_thuoc"].ToString()));
                    Utility.ShowMsg(string.Format("Một số thuốc dưới đây chưa được gán Tính chất nên hệ thống cấm gửi.\nVui lòng vào danh mục Thuốc-VTTH và gán tính chất cho các hạng mục này trước khi gửi:\n{0}", danhsach_thuoc));
                }
                
                    Utility.SetDataSourceForDataGridEx(grd_donthuoc_chitiet, dt_chitiet, true, true, "1=1", "ten_thuoc");
                foreach (GridEXRow row in grd_donthuoc_chitiet.GetDataRows())
                {
                    row.BeginEdit();
                    if (row.RowType == RowType.Record && Utility.Int16Dbnull(row.Cells["tthai_gui"].Value) == 0)
                    {
                        row.IsChecked = true;
                    }
                    else
                        row.IsChecked = false;
                    row.EndEdit();
                } 
                Utility.focusCellofCurrentRow(grd_donthuoc_chitiet, "stt_in");
                Utility.focusCellofCurrentRow(grd_DonThuoc, "id_benhnhan");
            }
            catch (Exception ex)
            {


            }
        }

        private void frm_QuanlyLienthongDonthuocQuocGia_Shown(object sender, EventArgs e)
        {
            Try2Splitter();
        }

        void Try2Splitter()
        {
            try
            {
                List<int> lstSplitterSize = (from p in File.ReadLines(SplitterPath)
                                             select Utility.Int32Dbnull(p)).ToList<int>();
                if (lstSplitterSize != null && lstSplitterSize.Count >=1)
                {
                
                    if (lstSplitterSize[0] == 1)
                        splitContainer3.Orientation = Orientation.Horizontal;
                    else
                        splitContainer3.Orientation = Orientation.Vertical;
                    splitContainer3.SplitterDistance = lstSplitterSize[1];

                }
            }
            catch (Exception)
            {

            }
        }
        private void frm_QuanlyLienthongDonthuocQuocGia_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveUserConfigs();
            Utility.SaveValue2Lines(SplitterPath, new List<string>() {  (splitContainer3.Orientation == Orientation.Horizontal ? 1 : 0).ToString(), splitContainer3.SplitterDistance.ToString()});
        }

       

        private void TxtMaLanKham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string Maluotkham = Utility.sDbnull(txtMaLanKham.Text.Trim());
                if (!string.IsNullOrEmpty(Maluotkham) && txtMaLanKham.Text.Length < 8)
                {
                    Maluotkham = Utility.AutoFullPatientCode(txtMaLanKham.Text);
                    txtMaLanKham.Text = Maluotkham;
                    txtMaLanKham.Select(txtMaLanKham.Text.Length, txtMaLanKham.Text.Length);
                }
                if (!string.IsNullOrEmpty(txtMaLanKham.Text))
                {
                    _Malankham_keydown = true;
                    cmdTimKiem.PerformClick();
                    _Malankham_keydown = false;

                }
            }
        }

       
        private void _MisaInvoices__OnStatus(string status,bool isErr)
        {
           
            LogText(status, isErr ? Color.Red: Color.DarkBlue);
        }

        void SetUI()
        {
            try
            {
                //List<string> lstArgs = Args.Split('|');
            }
            catch (Exception ex)
            {

               
            }
        }
       
        bool _ID_keydown = false;
        private void Txt_IdBenhnhan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                if (!string.IsNullOrEmpty(txt_IdBenhnhan.Text))
                {
                    _ID_keydown = true;
                    cmdTimKiem.PerformClick();
                    _ID_keydown = false;
                   
                }
            }
        }
        bool isAllowChanged = false;
        private void cmdTimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.WaitNow(this);
                isAllowChanged = false;
                Int16 id_bacsichidinh = Utility.Int16Dbnull(cboNhanvien.SelectedValue);
                
                byte tthai_duyet = 100;
                if (opt_tthai_daduyet.Checked) tthai_duyet = 1;
                else if (opt_tthai_chuaduyet.Checked) tthai_duyet = 0;
                else if (opt_tthai_tatca.Checked) tthai_duyet = 100;
                byte tthai_gui = 100;
                if (opt_dagui.Checked) tthai_gui = 1;
                else if (opt_chuagui.Checked) tthai_gui = 0;
                else if (opt_gui_tatca.Checked) tthai_gui = 100;
                byte kieu_donthuoc = 100;
                if (optNgoaitru.Checked) kieu_donthuoc = 0;
                else if (optNoitru.Checked) kieu_donthuoc = 1;
                else if (opt_ravien.Checked) kieu_donthuoc = 2;
                string kieuthanhtoan = "";
               
                DateTime tu_ngay = dtTuNgay.Value.Date;
                DateTime den_ngay = dtDenNgay.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59);
                int id_benhnhan = Utility.Int32Dbnull(txt_IdBenhnhan.Text, -1);
                string ma_lankham = Utility.sDbnull(txtMaLanKham.Text);
                string ten_benhnhan = Utility.sDbnull(txtTenBenhNhan.Text);
                
                if ( !chkTuNgay.Checked)
                {

                    tu_ngay = Convert.ToDateTime("01/01/1900");
                    den_ngay = THU_VIEN_CHUNG.GetSysDateTime();
                }
                
               
                if (_Malankham_keydown)//Nếu gõ theo mã BN-->Bỏ điều kiện tìm kiếm theo ngày
                {
                    tu_ngay = new DateTime(1990, 1, 1);
                    den_ngay = globalVariables.SysDate;
                    id_benhnhan = -1;
                    ten_benhnhan = "";
                }

                if (_ID_keydown)//Nếu gõ theo mã BN-->Bỏ điều kiện tìm kiếm theo ngày
                {
                    tu_ngay = new DateTime(1990, 1, 1);
                    den_ngay = globalVariables.SysDate;
                    ma_lankham = "";
                    ten_benhnhan = "";
                }
                
               
                    dtData = SPs.ThongtuLaydanhsachDonthuoc(tu_ngay, den_ngay, ma_lankham, ten_benhnhan, kieu_donthuoc, tthai_duyet, tthai_gui, id_bacsichidinh,Utility.Bool2byte(chk_DonTongHop.Checked)).GetDataSet().Tables[0];
                    Utility.SetDataSourceForDataGridEx(grd_DonThuoc, dtData, true, true, chk_AnCacDonThuocDaGui.Checked ? "tthai_gui= 0 or tthai_gui is null" : "1=1", "ngay_kedon,ten_benhnhan");
                    if (!Utility.isValidGrid(grd_DonThuoc))
                    {
                        grd_donthuoc_chitiet.DataSource = null;
                    }
                    isAllowChanged = true;
                    Grd_DonThuoc_SelectionChanged(isAllowChanged, e);
              
                
            }
            catch (Exception ex)
            { 
                Utility.ShowMsg(ex.Message);
            }
            finally
            {

                isAllowChanged = true;
                Utility.DefaultNow(this);
            }

        }

        

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_QuanlyLienthongDonthuocQuocGia_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                ProcessTabKey(true);
                return;
            }    
            else if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            else if (e.KeyCode == Keys.S && e.Control) cmd_guidonthuoc.PerformClick();
         
            else if ((e.Control && e.KeyCode==Keys.T) || e.KeyCode == Keys.F3) cmdTimKiem.PerformClick();
        }
        void LoadUserConfigs()
        {
            try
            {
               
                chk_AnCacDonThuocDaGui.Checked= Utility.getUserConfigValue(chk_AnCacDonThuocDaGui.Tag.ToString(), Utility.Bool2byte(chk_AnCacDonThuocDaGui.Checked)) == 1;
               

            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        void SaveUserConfigs()
        {
            try
            {
              
                Utility.SaveUserConfig(chk_AnCacDonThuocDaGui.Tag.ToString(), Utility.Bool2byte(chk_AnCacDonThuocDaGui.Checked));
               

            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        DataTable dtMau;
        private void frm_QuanlyLienthongDonthuocQuocGia_Load(object sender, EventArgs e)
        {
            LoadBacsi();
            DataBinding.BindDataCombobox(cboMaCoso, THU_VIEN_CHUNG.LayDulieuDanhmucChung("COSOKCB", true),
                                     DmucChung.Columns.Ma, DmucChung.Columns.Ten, "Chọn mã cơ sở KCB", true);
          
            PhanQuyenChucNang();
            LoadUserConfigs();
        }
        void LoadBacsi()
        {
            DataTable dtBacsi = globalVariables.gv_dtDmucNhanvien.Clone();
            if (chk_bacsi_lienthong.Checked)
            {
                //DataView dv = new DataView(globalVariables.gv_dtDmucNhanvien);
                //dv.RowFilter = "ma_lien_thong_bac_si IS NOT NULL AND LTRIM(RTRIM(ma_lien_thong_bac_si)) <> ''";
                //dtBacsi = dv.ToTable(); // Mặc định giữ nguyên cấu trúc và copy dữ liệu đã lọc
                var rows = globalVariables.gv_dtDmucNhanvien.AsEnumerable()
            .Where(r => !r.IsNull("ma_lien_thong_bac_si") && !string.IsNullOrWhiteSpace(r.Field<string>("ma_lien_thong_bac_si")))
            .ToList(); // ToList để tránh enumerate 2 lần

                dtBacsi = rows.Count > 0 ? rows.CopyToDataTable() : globalVariables.gv_dtDmucNhanvien.Clone();
                DataBinding.BindDataCombobox(cboNhanvien, dtBacsi,
                                        DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.TenNhanvien, "Chọn bác sĩ", true);
            }
            else
            {
                DataBinding.BindDataCombobox(cboNhanvien, globalVariables.gv_dtDmucNhanvien,
                                          DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.TenNhanvien, "Chọn bác sĩ", true);
            }
        }
        private void TimKiem()
        {
           
        }
        private DataTable dtCapPhat;
        private int HOADON_CAPPHAT_ID = -1;
        bool _Malankham_keydown = false;
        private void txtPatient_Code_KeyDown(object sender, KeyEventArgs e)
        {
           
                
        }

        private int HoaDon_Mau_ID = -1;

        private BackgroundWorker m_oWorker;
        int num = 0;
        DataTable dtCheck = new DataTable();
        /// <summary>
        /// hàm thưc hiện lưu thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            bool nochecked = grd_DonThuoc.GetCheckedRows().Count()<=0;
            try
            {
               
                // Reset dữ liệu bảng tạm
                int i = 0, j = 0;

                Utility.AutoCheckGrid(grd_DonThuoc);
                UIAction._Visible(ProgressBar, true);
                Utility.ResetProgressBarJanus(ProgressBar, grd_DonThuoc.GetCheckedRows().Count(), true);
                foreach (GridEXRow row in grd_DonThuoc.GetCheckedRows())
                {
                    string maluotKham = Utility.sDbnull(row.Cells["ma_luotkham"].Value);
                    long idBenhnhan = Utility.Int32Dbnull(row.Cells["id_benhnhan"].Value);
                    
                    string malienthong = Utility.sDbnull(row.Cells["ma_lien_thong_bac_si"].Value);
                    string ten_bacsi_kedon = Utility.sDbnull(row.Cells["ten_bacsi_kedon"].Value);
                    string ten_benhnhan = Utility.sDbnull(row.Cells["ten_benhnhan"].Value);
                    string msg = "";
                    if (string.IsNullOrEmpty(malienthong))
                    {
                        msg = string.Format("Bác sĩ {0} chưa có mã liên thông ở đơn thuốc của người bệnh {1} nên hệ thống không cho gửi", ten_bacsi_kedon, ten_benhnhan);
                        LogText(msg, Color.Red);
                        Utility.ShowMsg(msg);
                        continue;
                    }
                    //Lấy các loại đơn thuốc c,n,h từ các chi tiết đang chọn
                    List<string> lstLoaiDonThuoc = grd_donthuoc_chitiet.GetCheckedRows().Select(c => Utility.sDbnull(c.Cells["loai_don_thuoc"].Value)).Distinct().ToList<string>();
                    foreach (string loaidonthuoc in lstLoaiDonThuoc)
                    {
                        string ma_donthuoc = grd_donthuoc_chitiet.GetCheckedRows().FirstOrDefault().Cells["ma_donthuoc_yte"].Value.ToString();
                        //Lấy chi tiết checked theo loại đơn thuốc
                        List<long> lstIdchitiet = grd_donthuoc_chitiet.GetCheckedRows()
                            .Where(c => Utility.sDbnull(c.Cells["loai_don_thuoc"].Value) == loaidonthuoc)
                            .Select(c => Utility.Int64Dbnull(c.Cells["id_chitietdonthuoc"].Value))
                            .Distinct().ToList<long>();
                        long idDonthuoc =Utility.Int64Dbnull( grd_donthuoc_chitiet.GetCheckedRows().FirstOrDefault().Cells["id_donthuoc"].Value);
                        TThongtuDonthuoc ttdt = new Select().From(TThongtuDonthuoc.Schema)
                            .Where(TThongtuDonthuoc.Columns.MaDonthuoc).IsEqualTo(ma_donthuoc)
                             //.And(TThongtuDonthuoc.Columns.IdDonthuoc).IsEqualTo(idDonthuoc)
                             .And(TThongtuDonthuoc.Columns.IdBenhnhan).IsEqualTo(idBenhnhan)
                              .And(TThongtuDonthuoc.Columns.MaLuotkham).IsEqualTo(maluotKham)
                            .And(TThongtuDonthuoc.Columns.LoaiDonThuoc).IsEqualTo(loaidonthuoc)
                            .ExecuteSingle<TThongtuDonthuoc>();//.And(TThongtuDonthuoc.Columns.TrangThai).IsEqualTo(2);
                        if (ttdt != null)
                        {
                            LogText(string.Format("Đơn thuốc {0}-{1} đã được gửi trước đó nên không thể gửi lại", ma_donthuoc, loaidonthuoc), Color.Red);
                            j = j + 1;
                            continue;
                            //if (!Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn gửi lại đơn thuốc id={0} mã đơn={1}{2} lên Cổng đơn thuốc quốc gia?", objDonthuoc.IdDonthuoc, objDonthuoc.MaDonthuoc, loaidonthuoc), "Xác nhận gửi lại", true))
                            //{
                            //    j = j + 1;
                            //    continue;
                            //}

                        }


                        StoredProcedure sp = SPs.ThongtuLaythongtinDonthuoc(idDonthuoc, string.Join(",", lstIdchitiet.ConvertAll(x => x.ToString()).ToArray<string>()), idBenhnhan, "", maluotKham);
                        DataTable dt = sp.GetDataSet().Tables[0];
                        if (dt.Rows.Count > 0)
                        {
                            bool ketqua = false;
                            string messga = "";
                            string data = Utility.sDbnull(dt.Rows[0]["data1"]);
                            var objthongtin = JsonConvert.DeserializeObject<thongtin_donthuoc>(data);
                            bool ktra = IsValidData(objthongtin, ref messga);
                            if (!ktra)
                            {
                                LogText(string.Format("{0}. {1}", maluotKham, messga), Color.Red);
                                continue;
                            }
                            string response = "";
                            string result = new DuocTT27().Guidonthuoc(data, ref ketqua, ref messga, ref response);
                            long IdGui = -1;
                            if (ketqua)
                            {

                                if (ttdt != null)
                                {

                                    new Update(TThongtuDonthuoc.Schema)
                                        .Set(TThongtuDonthuoc.Columns.JsonMessga).EqualTo(response)
                                        .Set(TThongtuDonthuoc.Columns.JsonData).EqualTo(data)
                                        .Set(TThongtuDonthuoc.Columns.NguoiGui).EqualTo(globalVariables.UserName)
                                        .Set(TThongtuDonthuoc.Columns.NgayGui).EqualTo(globalVariables.SysDate)
                                        .Set(TThongtuDonthuoc.Columns.IpMaygui).EqualTo(globalVariables.gv_strIPAddress)
                                        .Set(TThongtuDonthuoc.Columns.TrangThai)
                                        .EqualTo(2) // 1: gửi không thành công, 2: Gửi thành công 
                                        .Where(TThongtuDonthuoc.Columns.IdBenhnhan).IsEqualTo(idBenhnhan)
                                        .And(TThongtuDonthuoc.Columns.MaLuotkham).IsEqualTo(maluotKham)
                                        .And(TThongtuDonthuoc.Columns.MaDonthuoc).IsEqualTo(ma_donthuoc)
                                        .And(TThongtuDonthuoc.Columns.LoaiDonThuoc).IsEqualTo(loaidonthuoc)
                                        //.And(TThongtuDonthuoc.Columns.IdDonthuoc).IsEqualTo(objDonthuoc.IdDonthuoc)
                                        .Execute();

                                    LogText(string.Format("{0}. Gửi dữ liệu thành công", maluotKham), Color.DarkBlue);
                                }
                                else
                                {
                                    TThongtuDonthuoc newItem = new TThongtuDonthuoc();
                                    newItem.IdDonthuoc = idDonthuoc;
                                    newItem.MaDonthuoc = ma_donthuoc;
                                    newItem.LoaiDonThuoc = loaidonthuoc;
                                    newItem.IdBenhnhan = idBenhnhan;
                                    newItem.MaLuotkham = maluotKham;
                                    newItem.JsonData = data;
                                    newItem.JsonMessga = response;
                                    newItem.NguoiTao = globalVariables.UserName;// objDonthuoc.NguoiTao;
                                    newItem.NgayTao = globalVariables.SysDate;// objDonthuoc.NgayTao;
                                    newItem.IpMaytao = globalVariables.gv_strIPAddress; //objDonthuoc.IpMaytao;
                                    newItem.NguoiGui = globalVariables.UserName;
                                    newItem.NgayGui = globalVariables.SysDate;
                                    newItem.IpMaygui = globalVariables.gv_strIPAddress;
                                    newItem.Save();
                                    IdGui = newItem.IdGui;
                                }
                                new Update(KcbDonthuocChitiet.Schema)
                                  .Set(KcbDonthuocChitiet.Columns.TthaiGui).EqualTo(1)
                                  .Set(KcbDonthuocChitiet.Columns.IdGui).EqualTo(IdGui)
                                    .Where(KcbDonthuocChitiet.Columns.IdChitietdonthuoc).In(lstIdchitiet)
                                    .Execute();
                                i = i + 1;
                                Utility.Log(this.Name, globalVariables.UserName, string.Format("Gửi thông tin đơn thuốc: {0}  - Message: {1} ", idDonthuoc, ""), newaction.Upload, "UI");
                            }
                            else
                            {
                                j = j + 1;
                            }


                        }
                        else
                        {
                            j = j + 1;
                            LogText(string.Format("{0}. Không tồn tại dữ liệu đơn thuốc Id= {1}", maluotKham, idDonthuoc), Color.Red);
                        }

                    }
                    UIAction.SetValue4PrgJanus(ProgressBar, 1);
                    Application.DoEvents();
                    row.IsChecked = false;
                }
                LogText(string.Format("Tạo dữ liệu thành công {0} hồ sơ, thất bại {1} hồ sơ", i, j), Color.DarkBlue);
                if (nochecked)
                    grd_DonThuoc.UnCheckAllRecords();
            }
            catch (Exception ex)
            {
                LogText(string.Format("Lỗi khi tạo dữ liệu" + ex.Message), Color.Red);
            }
            finally
            {
                UIAction._Visible(ProgressBar, false);
            }

        }
        List<int> lstTuoi = new List<int>() { 1, 2, 3 };
        private bool IsValidData(thongtin_donthuoc objDonthuoc, ref string message)
        {
            if (objDonthuoc != null)
            {
                if(globalVariables.Ma_Coso!= objDonthuoc.ma_don_thuoc.Substring(0,5))
                {
                    message = string.Format("5 kí tự đầu của mã đơn thuốc {0} phải là mã cơ sở KCB {1}", objDonthuoc.ma_don_thuoc, globalVariables.Ma_Coso);
                    return false;
                }
                if (!Utility.IsNumeric( objDonthuoc.so_dien_thoai_nguoi_kham_benh))
                {
                    message = string.Format("Trường số điện thoại bệnh nhân {0} phải là số", objDonthuoc.so_dien_thoai_nguoi_kham_benh);
                    return false;
                }
               
                if (objDonthuoc.chan_doan != null)
                {
                    foreach (Chan_doan rowchandoan in objDonthuoc.chan_doan)
                    {
                        if (string.IsNullOrEmpty(rowchandoan.ma_chan_doan) ||
                            string.IsNullOrEmpty(rowchandoan.ten_chan_doan)
                            || string.IsNullOrEmpty(rowchandoan.ket_luan)
                            )
                        {
                            message = "Chẩn đoán đơn thuốc hoặc lời dặn/xử trí/kết luận không được để trống";
                            return false;
                        }
                    }
                }
                else
                {
                    message = "Đơn thuốc bắt buộc phải có ít nhất 1 chẩn đoán bao gồm mã chẩn đoán/tên chẩn đoán/kết luận trước khi gửi";
                    return false;
                }
                if (objDonthuoc.thong_tin_don_thuoc != null)
                {
                    foreach (thong_tin_don_thuoc rowdonthuoc in objDonthuoc.thong_tin_don_thuoc)
                    {
                        if (string.IsNullOrEmpty(rowdonthuoc.ma_thuoc))
                        {
                            message = "Mã thuốc  không được để trống";
                            return false;
                        }
                        else if (string.IsNullOrEmpty(rowdonthuoc.biet_duoc))
                        {
                            message = "Biệt dược  không được để trống";
                            return false;
                        }
                        else if (string.IsNullOrEmpty(rowdonthuoc.ten_thuoc))
                        {
                            message = "Tên thuốc không được để trống";
                            return false;
                        }
                        else if (string.IsNullOrEmpty(rowdonthuoc.don_vi_tinh))
                        {
                            message = "Đơn vị tính không được để trống";
                            return false;
                        }
                        else if (Utility.DecimaltoDbnull(rowdonthuoc.so_luong)<=0)
                        {
                            message = "Số lượng kê phải lớn hơn 0";
                            return false;
                        }
                        else if (string.IsNullOrEmpty(rowdonthuoc.cach_dung))
                        {
                            message = "Cách dùng không được để trống";
                            return false;
                        }
                    }
                }
                else
                {
                    message = "Thông tin đơn thuốc đang trống";
                    return false;
                }
                if (!lstTuoi.Contains( Utility.Int32Dbnull(objDonthuoc.gioi_tinh,-1)))
                {
                    message = string.Format("Trường giới tính {0} phải có giá trị 1(Chưa xác định), 2(Giới tính Nam), hoặc 3(Giới tính Nữ)", objDonthuoc.gioi_tinh);
                    return false;
                }
                
                if (string.IsNullOrEmpty(objDonthuoc.ma_don_thuoc) || objDonthuoc.ma_don_thuoc.Length > 14)
                {
                    message = string.Format("Mã đơn thuốc {0} không được để trống và phải <= 14 ký tự", objDonthuoc.ma_don_thuoc);
                    return false;
                }
                //if (string.IsNullOrEmpty(objDonthuoc.so_dien_thoai_nguoi_kham_benh))
                //{
                //    message = "Điện thoại liên hệ không được bỏ trống";
                //    return false;
                //}
                if (Utility.sDbnull(objDonthuoc.ma_dinh_danh_cong_dan).Length > 12)
                {
                    message = string.Format("Mã định danh công dân {0} không được vượt quá 12 ký tự", Utility.sDbnull(objDonthuoc.ma_dinh_danh_cong_dan));
                    return false;
                }
                if (Utility.sDbnull(objDonthuoc.ma_dinh_danh_y_te).Length >= 10)
                {
                    message = string.Format("Mã định danh y tế {0} không được vượt quá 10 ký tự", Utility.sDbnull(objDonthuoc.ma_dinh_danh_y_te));
                    return false;
                }
                if (Utility.sDbnull(objDonthuoc.so_dien_thoai_nguoi_kham_benh).Length > 12)
                {
                    message = string.Format("Điện thoại của người bệnh {0} không được vượt quá 12 ký tự", Utility.sDbnull(objDonthuoc.so_dien_thoai_nguoi_kham_benh));
                    return false;
                }

                string loaidonthuoc = "c,h,n,y";
                if (!loaidonthuoc.Contains(objDonthuoc.loai_don_thuoc))
                {
                    message = string.Format("Loại đơn thuốc {0} bắt buộc phải thuộc 1 trong 4 loại sau [c,h,n,y]", loaidonthuoc);
                    return false;
                }
                if (string.IsNullOrEmpty(objDonthuoc.dia_chi) && objDonthuoc.dia_chi.Length >= 500)
                {
                    message = string.Format("Địa chỉ bệnh nhân không được để trống và phải <= 500 ký tự");
                    return false;
                }
                if (objDonthuoc.thong_tin_don_thuoc.Count <= 0)
                {
                    message = string.Format("Chi tiết đơn thuốc không được trống");
                    return false;
                }
            }
            else
            {
                message = "Thông tin đơn thuốc đang null";
                return false;
            }

            return true;
        }
        private delegate void SetPrgValue(Janus.Windows.EditControls.UIProgressBar Prg, int _Value);
        private void SetValue4Prg(Janus.Windows.EditControls.UIProgressBar Prg, int _Value)
        {
            try
            {
                if (Prg.InvokeRequired)
                {
                    Prg.Invoke(new SetPrgValue(SetValue4Prg), new object[] { Prg, _Value });
                }
                else
                {
                    if (Prg.Value + _Value <= Prg.Maximum) Prg.Value += _Value;
                    Prg.Refresh();
                    Thread.Sleep(10);
                }
            }
            catch (Exception ex)
            {
                if (globalVariables.IsAdmin)
                    Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }
        private const string _sNewline = "\r\n";
        private void AddAction(string sLogText, Color color)
        {
            if (sLogText.Length > 0)
            {
                Color oldColor = rtxtLogs.SelectionColor;
                rtxtLogs.SelectionLength = 0;
                rtxtLogs.SelectionStart = rtxtLogs.Text.Length;
                rtxtLogs.SelectionColor = color;
                rtxtLogs.SelectionFont = new Font(rtxtLogs.SelectionFont, FontStyle.Bold);
                rtxtLogs.AppendText(sLogText);
                rtxtLogs.SelectionColor = oldColor;
            }
        }
        public delegate void AddLog(string logText, Color sActionColor);
        public void LogText(string sLogText, Color sActionColor)
        {
            if (InvokeRequired)
            {
                Invoke(new AddLog(LogText), new object[] { sLogText, sActionColor });
            }
            else
            {
                VNS.Libs.AppUI.UIAction.SetText(lblStatus, sLogText);
                AddAction(sLogText, sActionColor);
                rtxtLogs.AppendText(_sNewline);
            }
        }
        private void PhanQuyenChucNang()
        {
            //cmd_guidonthuoc.Visible = Utility.Coquyen("DONTHUOC_GUI");
            
        }

       

       

        private void chkTuNgay_CheckedChanged(object sender, EventArgs e)
        {
            if(chkTuNgay.Checked)
            {
                dtTuNgay.Enabled = true;
                dtDenNgay.Enabled = true;
            }
            else
            {
                dtTuNgay.Enabled = false;
                dtDenNgay.Enabled = false;
            }
        }

      

        private void lnkReset_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dtTuNgay.Value = dtDenNgay.Value = DateTime.Now;
            txtMaLanKham.Clear();
            txtTenBenhNhan.Clear();
            txt_IdBenhnhan.Clear();
            cboNhanvien.SelectedIndex = 0;
            optTatca.Checked=true;
            opt_tthai_chuaduyet.Checked = true;
            dtTuNgay.Focus();
        }

       

        private void cmdChange_Click(object sender, EventArgs e)
        {
            if (splitContainer3.Orientation == Orientation.Horizontal)
                splitContainer3.Orientation = Orientation.Vertical;
            else
                splitContainer3.Orientation = Orientation.Horizontal;
        }

        private void chk_AnCacDonThuocDaGui_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (grd_DonThuoc.DataSource != null)
                {
                    //grd_DonThuoc.UnCheckAllRecords();
                    ((DataView)grd_DonThuoc.DataSource).RowFilter = chk_AnCacDonThuocDaGui.Checked ? "tthai_gui= 0 or tthai_gui is null" : "1=1";
                   // grd_DonThuoc.CheckAllRecords();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void chk_bacsi_lienthong_CheckedChanged(object sender, EventArgs e)
        {
            LoadBacsi();
        }

        private void cmdClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
    public class thongtin_donthuoc
    {
        public int id_donthuoc_tt { get; set; }
        public string ma_lien_thong_bac_si { get; set; }
        public string password { get; set; }
        public string ma_lien_thong_co_so_kham_chua_benh { get; set; }
        public string loai_don_thuoc { get; set; }
        public string ma_don_thuoc { get; set; }
        public string ho_ten_benh_nhan { get; set; }
        public string ma_dinh_danh_y_te { get; set; }
        public string ma_dinh_danh_cong_dan { get; set; }
        public string ngay_sinh_benh_nhan { get; set; }
        public decimal can_nang { get; set; }
        public int gioi_tinh { get; set; }
        public string ma_so_the_bao_hiem_y_te { get; set; }
        public string thong_tin_nguoi_giam_ho { get; set; }
        public string dia_chi { get; set; }
        public List<Chan_doan> chan_doan { get; set; }
        public string luu_y { get; set; }
        public int hinh_thuc_dieu_tri { get; set; }
        public List<dot_dung_thuoc> dot_dung_thuoc { get; set; }
        public List<thong_tin_don_thuoc> thong_tin_don_thuoc { get; set; }
        public string loi_dan { get; set; }
        public string so_dien_thoai_nguoi_kham_benh { get; set; }
        public string ngay_tai_kham { get; set; }
        public string ngay_gio_ke_don { get; set; }
        public string signature { get; set; }

    }

    public class thong_tin_don_thuoc
    {
        public string ma_thuoc { get; set; }
        public string biet_duoc { get; set; }
        public string ten_thuoc { get; set; }
        public string don_vi_tinh { get; set; }
        public decimal so_luong { get; set; }
        public string cach_dung { get; set; }
    }

    public class dot_dung_thuoc
    {
        public string dot { get; set; }
        public string tu_ngay { get; set; }
        public string den_ngay { get; set; }
    }

    public class Chan_doan
    {
        public string ma_chan_doan { get; set; }
        public string ten_chan_doan { get; set; }
        public string ket_luan { get; set; }
    }
}
