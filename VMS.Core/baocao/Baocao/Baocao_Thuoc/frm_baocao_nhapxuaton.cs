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
using VNS.HIS.DAL;


using VNS.Properties;

using VNS.HIS.BusRule.Classes;
using VNS.HIS.UI.Baocao;

namespace VNS.HIS.UI.BaoCao.Form_BaoCao
{
    public partial class frm_baocao_nhapxuaton : Form
    {
        private HisDuocProperties HisDuocProperties;
        string KIEU_THUOC_VT = "THUOC";
        TDmucKho _item = null;
        bool allowChanged = false;
        string KieuKho = "";
        public frm_baocao_nhapxuaton(string args)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.KieuKho = args.Split('-')[0];
            this.KIEU_THUOC_VT = args.Split('-')[1];
            
            dtNgayIn.Value = dtFromDate.Value = dtToDate.Value = globalVariables.SysDate;
            
         
            cmdExit.Click+=new EventHandler(cmdExit_Click);
            this.Load+=new EventHandler(frm_baocao_nhapxuaton_Load);
            chkByDate.CheckedChanged += chkByDate_CheckedChanged;
            cmdBaoCao.Click+=new EventHandler(cmdBaoCao_Click);
            this.KeyDown+=new KeyEventHandler(frm_baocao_nhapxuaton_KeyDown);
            txtKho.TextChanged +=txtKho_TextChanged;
           // cboKho.SelectedIndexChanged += new EventHandler(cboKho_SelectedIndexChanged);
            chkChanle.CheckedChanged += new EventHandler(chkChanle_CheckedChanged);
            chkTheoNhomThuoc.CheckedChanged += new EventHandler(chkTheoNhomThuoc_CheckedChanged);
            gridEXExporter1.GridEX = grdListKhoChan;
            CauHinh();
        }
        private void txtKho_TextChanged(object sender, EventArgs e)
        {
            if (!allowChanged) return;
            SelectStock();
            modifyTieude();
        }
        void modifyTieude()
        {
            if (KIEU_THUOC_VT == "THUOC")
            {
                if (chkTheoNhomThuoc.Checked)
                {
                    if (_item.KieuKho == "CHAN" || (chkChanle.Enabled && chkChanle.Checked))
                    {
                        baocaO_TIEUDE1.Init("thuoc_baocao_nhapxuatton_khochan_theonhom");
                        grdListKhoChan.BringToFront();
                    }
                    else
                    {
                        baocaO_TIEUDE1.Init("thuoc_baocao_nhapxuatton_khole_theonhom");
                        grdListKhole.BringToFront();
                    }
                }
                else
                {
                    if (_item.KieuKho == "CHAN" || (chkChanle.Enabled && chkChanle.Checked))
                    {
                        baocaO_TIEUDE1.Init("thuoc_baocao_nhapxuatton_khochan");
                        grdListKhoChan.BringToFront();
                    }
                    else
                    {
                        baocaO_TIEUDE1.Init("thuoc_baocao_nhapxuatton_khole");
                        grdListKhole.BringToFront();
                    }
                }
            }
            else
            {
                if (chkTheoNhomThuoc.Checked)
                {
                    if (_item.KieuKho == "CHAN" || (chkChanle.Enabled && chkChanle.Checked))
                    {
                        baocaO_TIEUDE1.Init("vt_baocao_nhapxuatton_khochan_theonhom");
                        grdListKhoChan.BringToFront();
                    }
                    else
                    {
                        baocaO_TIEUDE1.Init("vt_baocao_nhapxuatton_khole_theonhom");
                        grdListKhole.BringToFront();
                    }
                }
                else
                {
                    if (_item.KieuKho == "CHAN" || (chkChanle.Enabled && chkChanle.Checked))
                    {
                        baocaO_TIEUDE1.Init("vt_baocao_nhapxuatton_khochan");
                        grdListKhoChan.BringToFront();
                    }
                    else
                    {
                        baocaO_TIEUDE1.Init("vt_baocao_nhapxuatton_khole");
                        grdListKhole.BringToFront();
                    }
                }
            }
        }
        void chkTheoNhomThuoc_CheckedChanged(object sender, EventArgs e)
        {
            modifyTieude();
        }

        void chkChanle_CheckedChanged(object sender, EventArgs e)
        {
            if (_item != null && _item.KieuKho == "CHANLE")
            {
                modifyTieude();
            }
        }
        void txtLoaithuoc__OnEnterMe()
        {

        }
        void txtLoaithuoc__OnSelectionChanged()
        {

        }
        void cboKho_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (!allowChanged) return;
            //SelectStock();
        }
        void SelectStock()
        {
            if (Utility.Int32Dbnull(txtKho.MyID, -1) < 0)
                _item = null;
            else
            {
                _item = new Select().From(TDmucKho.Schema).Where(TDmucKho.IdKhoColumn).IsEqualTo(Utility.Int32Dbnull(txtKho.MyID)).ExecuteSingle<TDmucKho>();
                GetKieuThuocVT();
                BindThuocVT();
            }
        }
        void BindThuocVT()
        {
            AutocompleteThuoc();
            AutocompleteLoaithuoc();
        }
        private void AutocompleteLoaithuoc()
        {
            DataTable dtLoaithuoc = SPs.ThuocLayDanhmucLoaiThuocTheokho(Utility.Int32Dbnull(txtKho.MyID, -1)).GetDataSet().Tables[0];
            txtLoaithuoc.Init(dtLoaithuoc, new List<string>() { DmucLoaithuoc.Columns.IdLoaithuoc, DmucLoaithuoc.Columns.MaLoaithuoc, DmucLoaithuoc.Columns.TenLoaithuoc });
        }
        private void AutocompleteThuoc()
        {

            try
            {
                DataTable _dataThuoc = SPs.ThuocLayDanhmucThuocTheokho(Utility.Int32Dbnull(txtKho.MyID, -1)).GetDataSet().Tables[0];
                if (_dataThuoc == null)
                {
                    txtthuoc.dtData = null;
                    return;
                }
                txtthuoc.dtData = _dataThuoc;
                txtthuoc.ChangeDataSource();
            }
            catch
            {
            }
        }
        void GetKieuThuocVT()
        {
            try
            {

                if (_item != null)
                {
                    KIEU_THUOC_VT = _item.KhoThuocVt;

                    lblLoaikho.Text = _item.KieuKho == "CHAN" ? "Kho chẵn" : (_item.KieuKho == "LE" ? "Kho lẻ" : "Kho chẵn lẻ");
                    chkChanle.Enabled = _item.KieuKho == "CHANLE";
                    modifyTieude();
                }
                else
                {
                    lblLoaikho.Text = "Không xác định";
                    KIEU_THUOC_VT = "ALL";
                }
            }
            catch
            {
                KIEU_THUOC_VT = "ALL";
            }
        }
        private void CauHinh()
        {
            HisDuocProperties =PropertyLib._HisDuocProperties;
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
        private DataTable m_dtDrugData = new DataTable();
        /// <summary>
        /// load thông tin 
        /// của form hiện tai
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_baocao_nhapxuaton_Load(object sender, EventArgs e)
        {
            DataTable dtkho = null;
            if (KIEU_THUOC_VT == "THUOC")
            {
                baocaO_TIEUDE1.Init("thuoc_baocao_nhapxuatton_khochan");
                dtkho = KieuKho == "ALL" ? CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_TATCA() : (KieuKho == "CHAN" ? CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_CHAN() : CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_LE());
            }
            else
            {
                baocaO_TIEUDE1.Init("vt_baocao_nhapxuatton_khochan");
                dtkho = KieuKho == "ALL" ? CommonLoadDuoc.LAYTHONGTIN_KHOVATTU_TATCA() : (KieuKho == "CHAN" ? CommonLoadDuoc.LAYTHONGTIN_KHOVATTU_CHAN() : CommonLoadDuoc.LAYTHONGTIN_KHOVATTU_LE(new List<string> { "TATCA", "NGOAITRU","NOITRU" }));
            }
            txtKho.Init(dtkho, new List<string>() { TDmucKho.Columns.IdKho, TDmucKho.Columns.MaKho, TDmucKho.Columns.TenKho });
        //    DataBinding.BindData(cboKho, dtkho, TDmucKho.Columns.IdKho, TDmucKho.Columns.TenKho);
            DataTable m_dtNhomThuoc = new Select().From(DmucLoaithuoc.Schema).Where(DmucLoaithuoc.Columns.KieuThuocvattu).IsEqualTo(KIEU_THUOC_VT)
                .OrderAsc(DmucLoaithuoc.Columns.SttHthi).ExecuteDataSet().Tables[0];
            allowChanged = true;

            cboKho_SelectedIndexChanged(cboKho, e);
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
                string nhomthuoc = "-1";

                nhomthuoc = txtLoaithuoc.MyID.ToString();


                DataTable m_dtReport = null;
                if (_item.KieuKho == "CHAN" || (chkChanle.Enabled && chkChanle.Checked))
               m_dtReport= BAOCAO_THUOC.ThuocBaocaoBiendongthuocTrongkhotong(chkByDate.Checked ? dtFromDate.Text : Utility.sDbnull("01/01/1900"),
                                           chkByDate.Checked ? dtToDate.Text : globalVariables.SysDate.ToString(),
                                           Utility.Int32Dbnull(txtKho.MyID), nhomthuoc, Utility.Int32Dbnull(txtthuoc.MyID, -1), chkBiendong.Checked ? 1 : 0);
                else
                  m_dtReport=  BAOCAO_THUOC.ThuocBaocaoBiendongthuocTrongkhole(chkByDate.Checked ? dtFromDate.Text : Utility.sDbnull("01/01/1900"),
                                         chkByDate.Checked ? dtToDate.Text : globalVariables.SysDate.ToString(),
                                         Utility.Int32Dbnull(txtKho.MyID), Utility.Int32Dbnull(txtthuoc.MyID, -1), nhomthuoc, chkBiendong.Checked ? 1 : 0);
               
                Utility.SetDataSourceForDataGridEx( _item.KieuKho == "CHAN" || (chkChanle.Enabled && chkChanle.Checked) ? grdListKhoChan : grdListKhole, m_dtReport, true, true, "1=1", "");
                if (m_dtReport.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                string FromDateToDate = Utility.FromToDateTime(dtFromDate.Text, dtToDate.Text);
                if (_item.KieuKho == "CHAN" || (chkChanle.Enabled && chkChanle.Checked))
                {
                    THU_VIEN_CHUNG.CreateXML(m_dtReport, "baocao_xuatnhapton_khochan.xml");
                    thuoc_baocao.BaocaoNhapxuattonKhochan(m_dtReport,KIEU_THUOC_VT, baocaO_TIEUDE1.TIEUDE,
                                                                                          dtNgayIn.Value, FromDateToDate,
                                                                                          Utility.sDbnull(txtKho.Text), chkTheoNhomThuoc.Checked);
                }
                else
                {
                    THU_VIEN_CHUNG.CreateXML(m_dtReport, "baocao_xuatnhapton_khole.xml");
                    thuoc_baocao.BaocaoNhapxuattonKhole(m_dtReport,KIEU_THUOC_VT, baocaO_TIEUDE1.TIEUDE,
                                                                                             dtNgayIn.Value, FromDateToDate,
                                                                                             Utility.sDbnull(txtKho.Text), chkTheoNhomThuoc.Checked);
                }
                //else
                //{
                //    thuoc_baocao.BaocaoNhapxuattonKhochanTheonhom(m_dtReport, baocaO_TIEUDE1.TIEUDE,
                //                                                                         dtNgayIn.Value, FromDateToDate,
                //                                                                         Utility.sDbnull(cboKho.Text));
                //}
            }
            catch (Exception)
            {


            }
        }
        /// <summary>
        /// hàm thực hiện việc phím tắt thông tin 
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_baocao_nhapxuaton_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.F4) cmdBaoCao.PerformClick();
            if (e.KeyCode == Keys.F5) cmdExportToExcel.PerformClick();
        }

       

       

        private void cmdBaoCao_Click_1(object sender, EventArgs e)
        {

        }

        private void cmdExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (_item.KieuKho == "CHAN" || (chkChanle.Enabled && chkChanle.Checked))
                {
                    if (grdListKhoChan.RowCount <= 0)
                    {
                        Utility.ShowMsg("Không có dữ liệu để xuất file excel", "Thông báo");
                        return;
                    }
                    gridEXExporter1.GridEX = grdListKhoChan;
                }
                else
                {
                    if (grdListKhole.RowCount <= 0)
                    {
                        Utility.ShowMsg("Không có dữ liệu để xuất file excel", "Thông báo");
                        return;
                    }
                    gridEXExporter1.GridEX = grdListKhole;
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

        private void frm_baocao_nhapxuaton_KeyDown_1(object sender, KeyEventArgs e)
        {

        }
        /// <summary>
        /// hàm thực hiện việc 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdGetDataDrug_Click(object sender, EventArgs e)
        {
           
        }
    }
}
