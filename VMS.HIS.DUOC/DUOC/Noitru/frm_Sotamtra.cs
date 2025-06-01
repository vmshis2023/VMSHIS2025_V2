using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;
using Aspose.Cells;
using System.IO;
using System.Drawing;
using Janus.Windows.GridEX;
namespace VNS.HIS.UI.THUOC
{
    public partial class frm_ChooseIn : Form
    {
        private string thamso = "KHOA";
        private DataTable m_dtGiuong = new DataTable();
        private DataTable m_dtKhoaNoItru = new DataTable();
        private DataTable m_dtZoom = new DataTable();
        public frm_ChooseIn(string sthamso)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.KeyPreview = true;
            this.thamso = sthamso;
            dtDenNgay.Value = dtTuNgay.Value = THU_VIEN_CHUNG.GetSysDateTime();
            grdBuong.RowCheckStateChanged += grdBuong_RowCheckStateChanged;
            //txtKhoanoitru._OnEnterMe += txtKhoanoitru__OnEnterMe;
            cmdPrint.Click += cmdPrint_Click;
        }

        void grdBuong_RowCheckStateChanged(object sender, Janus.Windows.GridEX.RowCheckStateChangeEventArgs e)
        {
            try
            {
                foreach (GridEXRow exRow in grdGiuong.GetDataRows())
                {
                    exRow.BeginEdit();
                    if (Utility.Int32Dbnull(exRow.Cells["id_buong"].Value, -1) == Utility.Int32Dbnull(grdBuong.GetValue("id_buong")))
                    {
                        exRow.CheckState = e.CheckState;
                    }

                    exRow.EndEdit();
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }

        }
        void txtKhoanoitru__OnEnterMe()
        {
            //m_dtZoom = THU_VIEN_CHUNG.NoitruTimkiembuongTheokhoa(Utility.Int32Dbnull(txtKhoanoitru.MyID));
            //Utility.SetDataSourceForDataGridEx_Basic(grdBuong, m_dtZoom, true, true, "1=1",
            //    "sluong_giuong_trong desc,ten_buong");
            //m_dtGiuong = THU_VIEN_CHUNG.NoitruTimkiemgiuongTheobuong(Utility.Int32Dbnull(txtKhoanoitru.MyID),
            //    -1, 0);
            //Utility.SetDataSourceForDataGridEx_Basic(grdGiuong, m_dtGiuong, true, true, "1=1",
            //    "isFull asc,dang_nam ASC,ten_giuong");
        }
        void LoadUserConfigs()
        {
            try
            {
                chkPrinpreview.Checked = Utility.getUserConfigValue(chkPrinpreview.Tag.ToString(), Utility.Bool2byte(chkPrinpreview.Checked)) == 1;
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
                Utility.SaveUserConfig(chkPrinpreview.Tag.ToString(), Utility.Bool2byte(chkPrinpreview.Checked));
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private DataTable m_DataTimKiem=new DataTable();
        private void cmdPrint_Click(object sender, EventArgs e)
        {
            try
            {
                int id_khoadieutri = Utility.Int32Dbnull(cboKhoadieutri.SelectedValue, -1);
                if (id_khoadieutri <= 0)
                {
                    Utility.ShowMsg("Bạn cần chọn khoa nội trú", "Thông báo", MessageBoxIcon.Error);
                    return;
                }
                string sfileName = AppDomain.CurrentDomain.BaseDirectory + "sotamtra\\sotamtra.xls";
                string sfileNameSave = AppDomain.CurrentDomain.BaseDirectory + "sotamtra\\" + string.Format("{0}_{1}", (Utility.Int32Dbnull(cboKhoadieutri.SelectedValue, -1)<=0 ? "Tất cả" : cboKhoadieutri.Text), dtDenNgay.Value.ToString("yyyyMMddHHmmss")) + ".xls";
                int isDaLinh = -1;
                if (radDaLinh.Checked) isDaLinh = 1;
                if (radChuaLinh.Checked) isDaLinh = 0;
                int room_id = -1;
                int bed_id = -1;

                string id_buong = "-1";

                var query = (from item in grdBuong.GetCheckedRows()
                             let x = Utility.sDbnull(item.Cells["id_buong"].Value)
                             select x).ToArray();


                if (query.Count() > 0)
                {
                    id_buong = string.Join(",", query);
                }
                string id_giuong = "-1";
                if (grdGiuong.GetCheckedRows().Length > 0)
                {
                    var query1 = from dv in grdGiuong.GetCheckedRows().AsEnumerable()
                                 let y = Utility.sDbnull(dv.Cells["id_giuong"].Value)
                                 select y;

                    if (query1.Any())
                    {
                        var array = query1.ToArray();
                        id_giuong = string.Join(",", array);
                    }
                }

                int loaiphieu = -1;
                string tenloaiphieu = "tất cả";
                if (radLinhBoSung.Checked) tenloaiphieu = radLinhBoSung.Text;
                if (radLinhThuong.Checked) tenloaiphieu = radLinhThuong.Text;
                if (radLinhThuong.Checked) loaiphieu = 0;
                if (radLinhBoSung.Checked) loaiphieu = 1;
                m_DataTimKiem =

                   SPs.ThuocSotamtraLaydulieuin(dtTuNgay.Value, dtDenNgay.Value, Utility.Int16Dbnull(cboKhoadieutri.SelectedValue,-1),
                        id_buong, id_giuong, loaiphieu, radNgayDieuTri.Checked ? 1 : 0, isDaLinh)
                        .GetDataSet()
                        .Tables[0];

                if (m_DataTimKiem.Rows.Count <= 0 | m_DataTimKiem.Columns.Count <= 1)
                {
                    Utility.ShowMsg("Không tìm thấy kết quả !");
                    return;
                }
                Utility.AddColumToDataTable(ref m_DataTimKiem, "STT", typeof(Int32));
                int idx = 1;
                foreach (DataRow drv in m_DataTimKiem.Rows)
                {
                    drv["STT"] = idx;
                    idx++;
                }
                m_DataTimKiem.AcceptChanges();
                int STTBatdau = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("SOTAMTRA_STTBATDAUTHUOC", "7", true), 7);
                //  obj.WriteDataTableToExcel_SoTamTra(m_DataTimKiem, "sotamtra", sfileNameSave, radLinhThuong.Checked ? radLinhThuong.Text : radLinhBoSung.Text, Utility.sDbnull(cboKhoa.Text), dtDenNgay.Text);
                ExcelUtlity.Insotamtra(m_DataTimKiem, "sotamtra", sfileNameSave, tenloaiphieu, Utility.sDbnull(cboKhoadieutri.Text), dtTuNgay.Text, dtDenNgay.Text, STTBatdau, chkPrinpreview.Checked);
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
           
          //  exportReport(3,)
        }
       
        private DataTable m_dtKhoaNoiTru = new DataTable();
        private void frm_ChooseIn_Load(object sender, EventArgs e)
        {
            LoadUserConfigs();
            m_dtKhoaNoItru  = THU_VIEN_CHUNG.LaydanhsachKhoanoitruTheoBacsi(globalVariables.UserName, Utility.Bool2byte(globalVariables.IsAdmin), 1);
            DataBinding.BindDataCombobox(cboKhoadieutri, m_dtKhoaNoItru,
                                    DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong,
                                    "---Khoa trả---", true);

            //txtKhoanoitru.Init(m_dtKhoaNoItru, new List<string>() { DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong });
        }

        private void frm_ChooseIn_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
            if(e.KeyCode==Keys.F4)cmdPrint.PerformClick();
        }

        private void cmdCauHinh_Click(object sender, EventArgs e)
        {
           
        }

       

        private void frm_ChooseIn_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveUserConfigs();
        }

        private void cboKhoadieutri_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_dtZoom = THU_VIEN_CHUNG.NoitruTimkiembuongTheokhoa(Utility.Int32Dbnull(cboKhoadieutri.SelectedValue));
            Utility.SetDataSourceForDataGridEx_Basic(grdBuong, m_dtZoom, true, true, "1=1",
                "sluong_giuong_trong desc,ten_buong");
            m_dtGiuong = THU_VIEN_CHUNG.NoitruTimkiemgiuongTheobuong(Utility.Int32Dbnull(cboKhoadieutri.SelectedValue),
                -1, 0);
            Utility.SetDataSourceForDataGridEx_Basic(grdGiuong, m_dtGiuong, true, true, "1=1",
                "isFull asc,dang_nam ASC,ten_giuong");
        }

        private void cmdExit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
