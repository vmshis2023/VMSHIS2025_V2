using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using Microsoft.VisualBasic;
using VNS.Libs;
using VMS.HIS.DAL;
using VNS.HIS.BusRule.Classes;
using C1.C1Excel;
using Aspose.Cells;
using VNS.HIS.UI.Classess;


namespace VNS.HIS.UI.Baocao
{
    public partial class frm_sochitietbanhang_bvsg : Form
    {
        public DataTable _dtData = new DataTable();
        bool m_blnhasLoaded = false;
        string tieude = "", reportname = "";
        decimal tong_tien = 0m;
        public frm_sochitietbanhang_bvsg()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            Initevents();
            dtNgayInPhieu.Value = globalVariables.SysDate;
            dtToDate.Value = dtNgayInPhieu.Value = dtFromDate.Value = globalVariables.SysDate;
            
        }
        void Initevents()
        {
            this.KeyDown += new KeyEventHandler(frm_BAOCAO_TONGHOP_TAI_KKB_DTUONG_THUPHI_KeyDown);
            this.cmdExit.Click += new EventHandler(cmdExit_Click);
            chkByDate.CheckedChanged += new EventHandler(chkByDate_CheckedChanged);
            this.Load += new EventHandler(frm_BAOCAO_TONGHOP_TAI_KKB_DTUONG_THUPHI_Load);
           optVTTH.CheckedChanged+=_CheckedChanged;
           optCLS.CheckedChanged += _CheckedChanged;
           optThuoc.CheckedChanged += _CheckedChanged;
           optGiuong.CheckedChanged += _CheckedChanged;
           optCongkham.CheckedChanged += _CheckedChanged;

        }

       
        void chkChitiet_CheckedChanged(object sender, EventArgs e)
        {
            ShowGrid();
        }
        void ShowGrid()
        {
            if (optTonghop.Checked)
                baocaO_TIEUDE1.Init("baocao_sochitietbanhang_tonghop");
            else
                baocaO_TIEUDE1.Init("baocao_sochitietbanhang");
          
        }
        DataTable m_dtKhoathucHien=new DataTable();
        DataTable dtDichvu = new DataTable();
        private void frm_BAOCAO_TONGHOP_TAI_KKB_DTUONG_THUPHI_Load(object sender, EventArgs eventArgs)
        {
            try
            {
                ShowGrid();
                DataBinding.BindDataCombobox(cboDoituongKCB, THU_VIEN_CHUNG.LaydanhsachDoituongKcb(),
                                           DmucDoituongkcb.Columns.MaDoituongKcb, DmucDoituongkcb.Columns.TenDoituongKcb, "---Đối tượng---", true);
                DataBinding.BindDataCombobox(cboNhanvien, THU_VIEN_CHUNG.LaydanhsachThunganvien(),
                                      DmucNhanvien.Columns.UserName, DmucNhanvien.Columns.TenNhanvien, "---Thu ngân viên---", true);
                m_dtKhoathucHien = THU_VIEN_CHUNG.Laydanhmuckhoa("NGOAI", 0);
                DataBinding.BindDataCombobox(cboKhoa, m_dtKhoathucHien,
                                     DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong, "---Khoa thực hiện---", true);
                var query = from khoa in m_dtKhoathucHien.AsEnumerable()
                            where Utility.sDbnull(khoa[DmucKhoaphong.Columns.MaKhoaphong]) == globalVariables.MA_KHOA_THIEN
                            select khoa;
                if (query.Count() > 0)
                {
                    cboKhoa.SelectedValue = globalVariables.MA_KHOA_THIEN;
                }
                DataTable dt_loaiDvu = new Select().From(DmucDichvucl.Schema).ExecuteDataSet().Tables[0];
                LoadPtttNganhang();
                dtDichvu = SPs.BaocaoSochitietbanhangLaydanhmucdichvu().GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx_Basic(grdDvu, dtDichvu, true, true, "1=0", "loai,ten_dichvu");
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }

        }
        void FilterMe()
        {
            try
            {
                string Rowfilter="";
                if (optAll.Checked)
                {
                    id_loaithanhtoan = 0;
                    Rowfilter = "1=0";
                }
                else if (optCongkham.Checked)
                {
                    id_loaithanhtoan = 1;
                    Rowfilter = "LOAI=1";
                }
                else if (optCLS.Checked)
                {
                    id_loaithanhtoan = 2;
                    Rowfilter = "LOAI=2";
                }
                else if (optThuoc.Checked)
                {
                    id_loaithanhtoan = 3;
                    Rowfilter = "LOAI=3";
                }
                else if (optGiuong.Checked)
                {
                    id_loaithanhtoan = 4;
                    Rowfilter = "LOAI=4";
                }
                else if (optVTTH.Checked)
                {
                    id_loaithanhtoan = 5;
                    Rowfilter = "LOAI=5";
                }
                dtDichvu.DefaultView.RowFilter = Rowfilter;
                dtDichvu.AcceptChanges();
            }
            catch (Exception ex)
            {
                
                 Utility.CatchException(ex);
            }
        }
        DataTable dtPttt = new DataTable();
        DataTable dtNganhang = new DataTable();
        void LoadPtttNganhang()
        {
            DataTable dtData = THU_VIEN_CHUNG.LayDulieuDanhmucChung(new List<string>() { "PHUONGTHUCTHANHTOAN", "NGANHANG" }, true);
            dtPttt = THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, "PHUONGTHUCTHANHTOAN");
            dtNganhang = THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, "NGANHANG");
            DataBinding.BindDataCombobox(cboPttt, dtPttt,
                                     DmucChung.Columns.Ma, DmucChung.Columns.Ten, "---PTTT---", true);
            DataBinding.BindDataCombobox(cboNganhang, dtNganhang,
                                    DmucChung.Columns.Ma, DmucChung.Columns.Ten, "---Ngân hàng---", true);

            //cboPttt.DataSource = dtPttt;
            //cboPttt.ValueMember = DmucChung.Columns.Ma;
            //cboPttt.DisplayMember = DmucChung.Columns.Ten;
            //cboNganhang.DataSource = dtNganhang;
            //cboNganhang.ValueMember = DmucChung.Columns.Ma;
            //cboNganhang.DisplayMember = DmucChung.Columns.Ten;

            cboPttt.SelectedValue = "-1";
            cboNganhang.SelectedValue = "-1";
        }
        /// <summary>
        /// trạng thái của tìm kiếm từ ngày tới ngày
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Enabled = dtFromDate.Enabled = chkByDate.Checked;
        }
        /// <summary>
        /// hàm thực hiện việc phím tắt thông tin của form 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_BAOCAO_TONGHOP_TAI_KKB_DTUONG_THUPHI_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.F4) cmdInPhieuXN.PerformClick();
            if (e.KeyCode == Keys.F5) cmdExportToExcel.PerformClick();
            //  if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
        }
        /// <summary>
        /// hàm thực hiên việc thoát form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        MoneyByLetter _moneyByLetter = new MoneyByLetter();
        /// <summary>
        /// hàm thực hiện việc export excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                
                    saveFileDialog1.Filter = "Excel File(*.xls)|*.xls";
                    saveFileDialog1.FileName = string.Format("{0}.xls", tieude);
                    //saveFileDialog1.ShowDialog();
                    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string sPath = saveFileDialog1.FileName;
                        FileStream fs = new FileStream(sPath, FileMode.Create);
                        fs.CanWrite.CompareTo(true);
                        fs.CanRead.CompareTo(true);
                        gridEXExporter1.Export(fs);
                        fs.Dispose();
                    }
                    saveFileDialog1.Dispose();
                    saveFileDialog1.Reset();
                
            }
            catch (Exception exception)
            {

            }
        }
        byte id_loaithanhtoan = 0;
        void BaocaoSochitietbanhang()
        {
           
            try
            {
                string lstDichvu =string.Join(",", (from p in grdDvu.GetCheckedRows()
                                    select Utility.sDbnull(p.Cells["Id"].Value, "-1")).ToArray<string>());
                if (Utility.sDbnull( lstDichvu) == "")
                    lstDichvu = "-1";
                DateTime tungay = chkByDate.Checked ? dtFromDate.Value : Convert.ToDateTime("01/01/1900");
                DateTime dengay = chkByDate.Checked ? dtToDate.Value : Convert.ToDateTime("01/01/1900");
                string maDoituongKCB = Utility.sDbnull(cboDoituongKCB.SelectedValue, "-1");
                string manhanvien = Utility.sDbnull(cboNhanvien.SelectedValue, "-1");
                byte noitrungoaitru = Utility.ByteDbnull(cboLoaiDieutri.SelectedValue, 100);
                string khoa = Utility.sDbnull(cboKhoa.SelectedValue, -1);
                _dtData = BAOCAO_NGOAITRU.BaocaoSochitietbanhang_bvsg(tungay, dengay, maDoituongKCB, Utility.sDbnull(txtMaluotkham.Text),Utility.Int64Dbnull(txtIdbenhnhan.Text,-1), Utility.sDbnull(txtTenBN.Text), manhanvien, noitrungoaitru, khoa, Utility.sDbnull(cboPttt.SelectedValue, "-1"), Utility.sDbnull(cboNganhang.SelectedValue, "-1"),id_loaithanhtoan,lstDichvu, Utility.Bool2byte(optTonghop.Checked));
                if (optTonghop.Checked)
                    THU_VIEN_CHUNG.CreateXML(_dtData, "baocao_sochitietbanhang_tonghop.xml");
                    else
                THU_VIEN_CHUNG.CreateXML(_dtData, "baocao_sochitietbanhang.xml");
                Utility.SetDataSourceForDataGridEx_Basic(grdList, _dtData, true, true, "1=1", "");
                if (_dtData.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu báo cáo theo điều kiện bạn chọn", "Thông báo", MessageBoxIcon.Information);
                    return;
                }
                string dieukientimkiem =   string.Format("Từ ngày {0} đến {1} - Loại điều trị: {2} - Đối tượng: {3} - TNV: {4} - HTTT: {5} - Ngân hàng: {6}"
                                              , dtFromDate.Text, dtToDate.Text,cboLoaiDieutri.Text, cboDoituongKCB.SelectedIndex >= 0
                                                 ? Utility.sDbnull(cboDoituongKCB.Text)
                                                 : "Tất cả",
                                             cboNhanvien.SelectedIndex > 0
                                                 ? Utility.sDbnull(cboNhanvien.Text)
                                                 : "Tất cả"
                                                 ,Utility.sDbnull( cboPttt.SelectedValue)=="-1"?"Tất cả": cboPttt.Text
                                                 , Utility.sDbnull(cboNganhang.SelectedValue) == "-1" ? "Tất cả" : cboNganhang.Text
                                                 );
                if (optTonghop.Checked)
                    noitru_inphieu.InPhieu(_dtData, DateTime.Now, dieukientimkiem, true, "baocao_sochitietbanhang_tonghop");
                else
                    noitru_inphieu.InPhieu(_dtData, DateTime.Now, dieukientimkiem, true, "baocao_sochitietbanhang");
                //Xuất thẳng ra excel do quá nhiều cột
               // XuatExcel();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
               
            }
               
           
        }
        private void XuatExcel()
        {
            try
            {
                string template_exel = string.Format(@"{0}\Excels\baocaodoanhthu.xls.", Application.StartupPath);


                //C1XLBook book = new C1XLBook();
                DataTable dt = _dtData.Copy();
                Aspose.Cells.Workbook workbook = new Aspose.Cells.Workbook();
                workbook.Open(template_exel);
                Aspose.Cells.Worksheet worksheet = workbook.Worksheets[0];
                Cells objcells = worksheet.Cells;// Get a particular Cell.
                Cell objcell = objcells[2, 0];
                string Condition = string.Format("Từ ngày {0} đến {1} - Đối tượng : {2} - Nhân viên :{3}", dtFromDate.Text, dtToDate.Text, cboDoituongKCB.SelectedIndex >= 0
                                                 ? Utility.sDbnull(cboDoituongKCB.Text)
                                                 : "Tất cả",
                                             cboNhanvien.SelectedIndex > 0
                                                 ? Utility.sDbnull(cboNhanvien.Text)
                                                 : "Tất cả");

                objcell.PutValue(Condition);
                Aspose.Cells.Cell cell;
                int idxRow = 6;
                int idxCol_SH = 0;
                int idx = idxRow;
                int endfor = dt.Rows.Count + idx;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    cell = worksheet.Cells[idxRow, idxCol_SH]; cell.PutValue(Utility.sDbnull(i+1)); SetBorderStype(cell, TextAlignmentType.Left);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 1]; cell.PutValue(Utility.sDbnull(dt.Rows[i]["ngay_thuchien"])); SetBorderStype(cell, TextAlignmentType.Left);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 2]; cell.PutValue(Utility.sDbnull(dt.Rows[i]["id_benhnhan"])); SetBorderStype(cell, TextAlignmentType.Left);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 3]; cell.PutValue(Utility.sDbnull(dt.Rows[i]["ma_luotkham"])); SetBorderStype(cell, TextAlignmentType.Left);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 4]; cell.PutValue(Utility.sDbnull(dt.Rows[i]["ten_benhnhan"])); SetBorderStype(cell, TextAlignmentType.Left);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 5]; cell.PutValue(Utility.sDbnull(dt.Rows[i]["nam_sinh"])); SetBorderStype(cell, TextAlignmentType.Left);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 6]; cell.PutValue(Utility.sDbnull(dt.Rows[i]["dia_chi"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 7]; cell.PutValue(Utility.sDbnull(dt.Rows[i]["Noi_Dung"])); SetBorderStype(cell, TextAlignmentType.Right); cell.GetStyle().IsTextWrapped.ToString();
                    cell = worksheet.Cells[idxRow, idxCol_SH + 8]; cell.PutValue(Utility.sDbnull(dt.Rows[i]["dien_thoai"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 9]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["TONGCONG"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 10]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["tien_mat"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 11]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["tien_congno"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 12]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["tien_nganhang"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 13]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["tien_ck"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 14]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["tien_khac"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 15]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["KHAM"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 16]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["SPK"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 17]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["XN"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 18]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["SA"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 19]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["XQ"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 20]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["DT"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 21]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["THUTHUAT"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 22]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["PHAUTHUAT"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 23]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["IVF"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 24]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["KHAC"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 25]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["IUI"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 26]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["PRP"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 27]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["THUKHAC"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 28]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["TIEN_QUAY"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 29]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["TIEN_TRALAI_QUAY"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 30]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["Thuoc"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 31]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["VTTH"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 32]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["GIUONG"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 33]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["phu_thu"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 34]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["MG"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 35]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["TU"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 36]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["HUY_TU"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 37]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["HU"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 38]; cell.PutValue(Utility.DoubletoDbnull(dt.Rows[i]["Tongtien_tralai"])); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 39]; cell.PutValue(Utility.sDbnull(dt.Rows[i]["bacsi_chidinh"])); SetBorderStype(cell, TextAlignmentType.Left);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 40]; cell.PutValue(Utility.sDbnull(dt.Rows[i]["thongtin_nguongt"])); SetBorderStype(cell, TextAlignmentType.Left);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 41]; cell.PutValue(Utility.sDbnull(dt.Rows[i]["ten_doitac"])); SetBorderStype(cell, TextAlignmentType.Left);
                    cell = worksheet.Cells[idxRow, idxCol_SH + 42]; cell.PutValue(Utility.sDbnull(dt.Rows[i]["Bn_cu"])); SetBorderStype(cell, TextAlignmentType.Left);
                    idxRow = idxRow + 1;
                }
                if (optTonghop.Checked)
                {
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 5]; cell.PutValue("Tổng cộng: "); SetBorderStype(cell, TextAlignmentType.Center);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 9]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(TONGCONG)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 10]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(tien_mat)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 11]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(tien_congno)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 12]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(tien_nganhang)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 13]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(tien_ck)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 14]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(tien_khac)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 15]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(KHAM)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 16]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(SPK)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 17]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(XN)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 18]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(SA)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 19]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(XQ)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 20]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(DT)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 21]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(THUTHUAT)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 22]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(PHAUTHUAT)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 23]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(IVF)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 24]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(KHAC)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 25]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(IUI)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 26]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(PRP)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 27]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(THUKHAC)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 28]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(TIEN_QUAY)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 29]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(TIEN_TRALAI_QUAY)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 30]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(Thuoc)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 31]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(VTTH)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 32]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(GIUONG)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 33]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(phu_thu)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 34]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(MG)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 35]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(TU)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 36]; cell.PutValue(Utility.DoubletoDbnull(Math.Abs(Utility.DecimaltoDbnull( dt.Compute("SUM(HUY_TU)", "1=1"),0)))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 37]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(HU)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 38]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(Tongtien_tralai)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                }
                else//Dòng tổng của báo cáo chi tiết
                {
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 5]; cell.PutValue("Tổng cộng: "); SetBorderStype(cell, TextAlignmentType.Center);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 9]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(TONGCONG)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 10]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(tien_mat)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 11]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(tien_congno)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 12]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(tien_nganhang)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 13]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(tien_ck)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 14]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(tien_khac)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 15]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(KHAM)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 16]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(SPK)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 17]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(XN)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 18]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(SA)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 19]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(XQ)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 20]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(DT)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 21]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(THUTHUAT)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 22]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(PHAUTHUAT)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 23]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(IVF)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 24]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(KHAC)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 25]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(IUI)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 26]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(PRP)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 27]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(THUKHAC)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 28]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(TIEN_QUAY)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 29]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(TIEN_TRALAI_QUAY)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 30]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(Thuoc)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 31]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(VTTH)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 32]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(GIUONG)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 33]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(phu_thu)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 34]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(MG)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 35]; cell.PutValue(Utility.DoubletoDbnull( dt.Compute("SUM(TU)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 36]; cell.PutValue(Utility.DoubletoDbnull(Math.Abs(Utility.DecimaltoDbnull(dt.Compute("SUM(HUY_TU)", "1=1"),0)))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 37]; cell.PutValue(Utility.DoubletoDbnull(Math.Abs(Utility.DecimaltoDbnull(dt.Compute("SUM(HU)", "1=1"),0)))); SetBorderStype(cell, TextAlignmentType.Right);
                    cell = worksheet.Cells[idx - 1, idxCol_SH + 38]; cell.PutValue(Utility.DoubletoDbnull(dt.Compute("SUM(Tongtien_tralai)", "1=1"))); SetBorderStype(cell, TextAlignmentType.Right);
                }

                //workbook.CalculateFormula();
                //worksheet.AutoFitRows();
                //worksheet.AutoFitColumns();
                // vị trí dòng dữ liệu của table tiếp theo, vị trí cột bắt đầu từ 0
                int inDexRowTableNext = idxRow;
                int inDexColumn = 0;
                string getTime = Convert.ToString(DateTime.Now.ToString("yyyyMMddhhmmss"));
                string pathDirectory = AppDomain.CurrentDomain.BaseDirectory + "Excels\\Data\\";
                if (!Directory.Exists(pathDirectory))
                {
                    Directory.CreateDirectory(pathDirectory);
                }
                string saveAsLocation = AppDomain.CurrentDomain.BaseDirectory + "\\Excels\\Data\\" + "baocaodoanhthu" +
                          getTime + ".xls";
                workbook.Save(saveAsLocation);

                System.Diagnostics.Process.Start(saveAsLocation);

                //book.Save(AppDomain.CurrentDomain.BaseDirectory + "\\TemplateExcel\\ExportExcel\\" + "baocaodoanhthu" +
                //          getTime + ".xls");
                //Process.Start(
                //    new ProcessStartInfo(AppDomain.CurrentDomain.BaseDirectory + "\\TemplateExcel\\ExportExcel\\" +
                //                         "baocaodoanhthu" + getTime + ".xls"));
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
           

        }
        public static void SetBorderStype(Aspose.Cells.Cell cell, TextAlignmentType TextAlignment)
        {
            Aspose.Cells.Style style = cell.GetStyle();
            //Setting the line style of the top border
            style.Borders[BorderType.TopBorder].LineStyle = CellBorderType.Thin;

            //Setting the color of the top border
            style.Borders[BorderType.TopBorder].Color = Color.Black;

            //Setting the line style of the bottom border
            style.Borders[BorderType.BottomBorder].LineStyle = CellBorderType.Thin;

            //Setting the color of the bottom border
            style.Borders[BorderType.BottomBorder].Color = Color.Black;

            //Setting the line style of the left border
            style.Borders[BorderType.LeftBorder].LineStyle = CellBorderType.Thin;

            //Setting the color of the left border
            style.Borders[BorderType.LeftBorder].Color = Color.Black;

            //Setting the line style of the right border
            style.Borders[BorderType.RightBorder].LineStyle = CellBorderType.Thin;

            //Setting the color of the right border
            style.Borders[BorderType.RightBorder].Color = Color.Black;

            style.VerticalAlignment = TextAlignment;
            style.HorizontalAlignment = TextAlignment;
            //style.Font= new Aspose.Cells.Font("Times New Roman", 10, FontStyle.Bold);
            //  style.Font
            // Aspose.Cells.Font font = new Aspose.Cells.Font("Times New Roman", 10,);
            // var font = new System.Drawing.Font("Times New Roman", 10, FontStyle.Bold);
            // cell.SetStyle();
            //style.se(TextAlignmentType.CENTER);
            //Apply the border styles to the cell
            //style.Custom = "";
            cell.SetStyle(style);

        }
        /// <summary>
        /// hàm thực hiện việc in phiếu báo cáo tổng hợp
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInPhieuXN_Click(object sender, EventArgs e)
        {
            
                BaocaoSochitietbanhang();
              
        }
        

        private void cboKhoa_ThucHien_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void uiComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowGrid();
        }

        private void cboPttt_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> lstPTTT = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_CHONNGANHANG", false).Split(',').ToList<string>();
            cboNganhang.Enabled = lstPTTT.Contains(cboPttt.SelectedValue.ToString());
            if (!cboNganhang.Enabled) cboNganhang.SelectedIndex = -1;
        }

        private void _CheckedChanged(object sender, EventArgs e)
        {
            FilterMe();
        }

        private void optChitiet_CheckedChanged(object sender, EventArgs e)
        {
            ShowGrid();
        }

        private void optTonghop_CheckedChanged(object sender, EventArgs e)
        {
            ShowGrid();
        }
    }
}
