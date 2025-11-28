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
using VNS.Libs;
using VMS.HIS.DAL;
using VNS.HIS.BusRule.Classes;
using VNS.HIS.UI.Baocao;
using VMS.HIS.Danhmuc.Dungchung;

namespace VNS.HIS.UI.BaoCao.Form_BaoCao
{
    public partial class frm_baocao_nhapkho_chitiet : Form
    {
        string KIEU_THUOC_VT = "THUOC";
        /// <summary>
        /// hàm thực hiện việc nhập kho chi tiết
        /// </summary>
        public frm_baocao_nhapkho_chitiet(string KIEU_THUOC_VT)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.KIEU_THUOC_VT = KIEU_THUOC_VT;
            
            dtNgayIn.Value = dtFromDate.Value = dtToDate.Value = globalVariables.SysDate;
            
        }

        void txtKho__OnEnterMe()
        {
            AutocompleteThuoc();  
        }
        private void txtKho_TextChanged(object sender, EventArgs e)
        {
           
        }
        /// <summary>
        /// hàm thực hiện việc đống form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// load thông tin 
        /// của form hiện tai
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_baocao_nhapkho_chitiet_Load(object sender, EventArgs e)
        {
            baocaO_TIEUDE1.Init("thuoc_baocaochitiet_nhapkho");
            txtNhacungcap.Init();
            DataTable dtKho = KIEU_THUOC_VT == "THUOC" ? CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_CHAN() : CommonLoadDuoc.LAYTHONGTIN_KHOVATTU_CHAN();
            DataBinding.BindDataCombobox(cbo_kho, dtKho, TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho,"---Chọn kho---",true);
            
            AutocompleteThuoc();
        }
       
        private void AutocompleteThuoc()
        {

            try
            {
                DataTable _dataThuoc = SPs.ThuocLayDanhmucThuocTheokho(Utility.Int32Dbnull(cbo_kho.SelectedValue, -1)).GetDataSet().Tables[0];
                DataBinding.BindDataCombobox(cbo_thuoc, _dataThuoc, DmucThuoc.Columns.IdThuoc, DmucThuoc.Columns.TenThuoc);
            }
            catch
            {
            }
        }
        /// <summary>
        /// hamfm thực hiện việc 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtFromDate.Enabled = dtToDate.Enabled = chkByDate.Checked;
        }
        /// <summary>
        /// hàm thực hiện in phiếu báo cáo 
        /// thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdBaoCao_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_dtReport == null || m_dtReport.Columns.Count <= 0) cmd_TimKiem.PerformClick();
                THU_VIEN_CHUNG.CreateXML(m_dtReport, "thuoc_baocaochitiet_nhapkho.xml");
                if (m_dtReport == null || m_dtReport.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                string FromDateToDate = Utility.FromToDateTime(dtFromDate.Text, dtToDate.Text);
                thuoc_baocao.BaocaoNhapkhoChitiet(m_dtReport, KIEU_THUOC_VT == "THUOC" ? "thuoc_baocaochitiet_nhapkho" : "vt_baocaochitiet_nhapkho", baocaO_TIEUDE1.TIEUDE, dtNgayIn.Value, FromDateToDate);
            }
            catch (Exception)
            { 
            }
        }
        private int Trangthai()
        {
            int trangthainho = -1;
            if (rdoTatCa.Checked) trangthainho = -1;
            if (rdoDaXacNhan.Checked) trangthainho = 1;
            if (rdoChuaXacNhan.Checked) trangthainho = 0;
            return trangthainho;
        }
        /// <summary>
        /// hàm thực hiện việc phím tắt thông tin 
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_baocao_nhapkho_chitiet_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
            if(e.KeyCode==Keys.F4)cmdBaoCao.PerformClick();
        }

      

        private void cmdExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                //Janus.Windows.GridEX.GridEXRow[] gridExRows = grdList.GetCheckedRows();
                if (grdList.RowCount <= 0)
                {
                    Utility.ShowMsg("Không có dữ liệu", "Thông báo");
                    grdList.Focus();
                    return;
                }
                saveFileDialog1.Filter = "Excel File(*.xls)|*.xls";
                saveFileDialog1.FileName = string.Format("{0}.xls", baocaO_TIEUDE1.TIEUDE);
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

        private void cmdComboDown_Click(object sender, EventArgs e)
        {
          
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
           
        }
        DataTable m_dtReport = null;
        private void cmd_TimKiem_Click(object sender, EventArgs e)
        {
            try
            {
                int trangthai = Trangthai();
                int kieungaytimkiem = chkKieungaytimkiem.Checked ? 1 : 0;

                m_dtReport = BAOCAO_THUOC.ThuocBaocaoTinhhinhnhapkhothuoc(chkByDate.Checked ? dtFromDate.Value.ToString("dd/MM/yyyy") : "01/01/1900",
                                           chkByDate.Checked ? dtToDate.Value.ToString("dd/MM/yyyy") : "01/01/1900", trangthai,
                                           Utility.Int32Dbnull(cbo_kho.SelectedValue), Utility.Int32Dbnull(cbo_thuoc.SelectedValue, -1), (byte)LoaiPhieu.PhieuNhapKho, kieungaytimkiem, "", txtNhacungcap.myCode, KIEU_THUOC_VT);
                THU_VIEN_CHUNG.CreateXML(m_dtReport, "thuoc_baocaochitiet_nhapkho.xml");
                Utility.SetDataSourceForDataGridEx(grdList, m_dtReport, true, true, "1=1", "");
               
            }
            catch (Exception)
            {
            }
        }

        private void cmdHoiDongKiemNhap_Click(object sender, EventArgs e)
        {
            if(Utility.Int32Dbnull( cbo_kho.SelectedValue,0)<=0)
            {
                Utility.ShowMsg("Bạn cần chọn kho trước khi nhập hội đồng kiểm nhập cho kho");
            }
            long id_phieu = Utility.Int32Dbnull(cbo_kho.SelectedValue, 0);
            byte loai_phieu = 1;//0=Hội đồng kiểm nhập theo phiếu nhập kho;1= hội đồng kiểm nhập theo
            frm_danhsach_hoidong _hoidong = new frm_danhsach_hoidong(id_phieu, loai_phieu);
            _hoidong.ShowDialog();
        }

        private void cmdInBienBanKiemNhap_Click(object sender, EventArgs e)
        {
            try
            {
                if (m_dtReport == null || m_dtReport.Columns.Count <= 0) cmd_TimKiem.PerformClick();
                THU_VIEN_CHUNG.CreateXML(m_dtReport, "thuoc_bienban_kiemnhap_theokho.xml");
                if (m_dtReport == null || m_dtReport.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                string FromDateToDate = Utility.FromToDateTime(dtFromDate.Text, dtToDate.Text);
                thuoc_baocao.BienBanKiemNhapKho(m_dtReport, KIEU_THUOC_VT == "THUOC" ? "thuoc_bienban_kiemnhap_theokho" : "vt_bienban_kiemnhap_theokho",  dtNgayIn.Value, FromDateToDate);
            }
            catch (Exception)
            {
            }
        }
    }
}
