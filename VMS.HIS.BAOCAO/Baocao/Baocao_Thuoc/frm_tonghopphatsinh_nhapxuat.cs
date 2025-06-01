using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SubSonic;
using VNS.HIS.BusRule.Classes;
using VMS.HIS.DAL;
using VNS.HIS.UI.Baocao;
using VNS.Libs;
using VNS.Properties;

namespace VNS.HIS.UI.BaoCao.Form_BaoCao
{
    public partial class frm_tonghopphatsinh_nhapxuat : Form
    {
        private readonly string KieuKho = "ALL"; //ALL: Bốc cả kho thuốc chẵn lẻ; CHAN=BỐC KHO CHẴN;LE=BỐC KHO LẺ
        private string KIEU_THUOC_VT = "THUOC";
        private TDmucKho _item;
        private bool allowChanged;
        private DataTable m_dtDrugData = new DataTable();
        public frm_tonghopphatsinh_nhapxuat(string args)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            KieuKho = args.Split('-')[0];
            KIEU_THUOC_VT = args.Split('-')[1];
            
            dtNgayIn.Value = dtFromDate.Value = dtToDate.Value = dtpNam.Value=globalVariables.SysDate;
            
            cmdExit.Click += cmdExit_Click;
            Load += frm_tonghopphatsinh_nhapxuat_Load;
            cmdBaoCao.Click += cmdBaoCao_Click;
            cboKieutonghop.SelectedIndexChanged += cboKieutonghop_SelectedIndexChanged;
            KeyDown += frm_tonghopphatsinh_nhapxuat_KeyDown;
            txtKho._OnEnterMe += txtKho__OnEnterMe;
           // cboKho.SelectedIndexChanged += cboKho_SelectedIndexChanged;
            cboKieubangke.SelectedIndexChanged += cboKieubangke_SelectedIndexChanged;
        }

        void cboKieubangke_SelectedIndexChanged(object sender, EventArgs e)
        {
            modifyTieude();
        }

        void cboKieutonghop_SelectedIndexChanged(object sender, EventArgs e)
        {
            modifyTieude();
        }
        private void txtKho__OnEnterMe()
        {
            if (!allowChanged) return;
            SelectStock();
            modifyTieude();
        }
        //private void txtkho_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (!allowChanged) return;
        //    SelectStock();
        //    modifyTieude();
        //}

        private void SelectStock()
        {
            if (Utility.Int32Dbnull(txtKho.MyID, -1) < 0)
                _item = null;
            else
            {
                _item =
                    new Select().From(TDmucKho.Schema).Where(TDmucKho.IdKhoColumn).IsEqualTo(
                        Utility.Int32Dbnull(txtKho.MyID)).ExecuteSingle<TDmucKho>();
                GetKieuThuocVT();
                BindThuocVT();
            }
        }

        private void BindThuocVT()
        {
            AutocompleteThuoc();
            AutocompleteLoaithuoc();
        }

        private void AutocompleteLoaithuoc()
        {
            DataTable dtLoaithuoc =
                SPs.ThuocLayDanhmucLoaiThuocTheokho(Utility.Int32Dbnull(txtKho.MyID, -1)).GetDataSet().Tables[0
                    ];
            txtLoaithuoc.Init(dtLoaithuoc,
                              new List<string>
                                  {
                                      DmucLoaithuoc.Columns.IdLoaithuoc,
                                      DmucLoaithuoc.Columns.MaLoaithuoc,
                                      DmucLoaithuoc.Columns.TenLoaithuoc
                                  });
        }

        private void AutocompleteThuoc()
        {
            try
            {
                DataTable _dataThuoc =
                    SPs.ThuocLayDanhmucThuocTheokho(Utility.Int32Dbnull(txtKho.MyID, -1)).GetDataSet().Tables[0
                        ];
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
        private void modifyTieude()
        {
            if (cboKieutonghop.SelectedIndex == 0)
            {
                if(cboKieubangke.SelectedIndex==0)
                    baocaO_TIEUDE1.Init("thuoc_baocaophatsinhnhap_chitiet");
                else
                    baocaO_TIEUDE1.Init("thuoc_baocaophatsinhnhap_tonghop");
            }
            else
            {
                if (cboKieubangke.SelectedIndex == 0)
                    baocaO_TIEUDE1.Init("thuoc_baocaophatsinhxuat_chitiet");
                else
                    baocaO_TIEUDE1.Init("thuoc_baocaophatsinhxuat_tonghop");
            }
            if (cboKieubangke.SelectedIndex == 0)
                  grdChitiet.BringToFront();
            else
                grdTonghop.BringToFront();
        }

        private void GetKieuThuocVT()
        {
            try
            {
                if (_item != null)
                {
                    KIEU_THUOC_VT = _item.KhoThuocVt;

                    lblLoaikho.Text = _item.KieuKho == "CHAN"
                                          ? "Kho chẵn"
                                          : (_item.KieuKho == "LE" ? "Kho lẻ" : "Kho chẵn lẻ");
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

        /// <summary>
        /// hàm thực hiện việc đống form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// load thông tin 
        /// của form hiện tai
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_tonghopphatsinh_nhapxuat_Load(object sender, EventArgs e)
        {
            modifyTieude();
            txtKho.Init(KieuKho == "ALL"
                                     ? CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_TATCA()
                                     : (KieuKho == "CHAN"
                                            ? CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_CHAN()
                                            : CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_LE()), new List<string>() { TDmucKho.Columns.IdKho, TDmucKho.Columns.MaKho, TDmucKho.Columns.TenKho });
            txtKhoXuat.Init(KieuKho == "ALL"
                                   ? CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_TATCA()
                                   : (KieuKho == "CHAN"
                                          ? CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_CHAN()
                                          : CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_LE()), new List<string>() { TDmucKho.Columns.IdKho, TDmucKho.Columns.MaKho, TDmucKho.Columns.TenKho });

            //DataBinding.BindData(cboKho,
            //                     KieuKho == "ALL"
            //                         ? CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_TATCA()
            //                         : (KieuKho == "CHAN"
            //                                ? CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_CHAN()
            //                                : CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_LE()), TDmucKho.Columns.IdKho,
            //                     TDmucKho.Columns.TenKho);
            DataTable m_dtNhomThuoc = new Select().From(DmucLoaithuoc.Schema)
                .OrderAsc(DmucLoaithuoc.Columns.SttHthi).ExecuteDataSet().Tables[0];
            allowChanged = true;
          //  cboKho_SelectedIndexChanged(cboKho, e);
            cboThang.SelectedIndex = globalVariables.SysDate.Month - 1;
            cboKieubangke.SelectedIndex = 0;
            cboKieutonghop.SelectedIndex = 0;
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
                if (Utility.Int32Dbnull(txtKho.MyID,-1) < 0)
                {
                    Utility.ShowMsg("Bạn phải chọn Kho thuốc");
                    txtKho.Focus();
                    return;
                }
                nhomthuoc = txtLoaithuoc.MyID.ToString();
                DataTable m_dtReport = null;
                string fromdate = "01/01/1900";
                string todate = "01/01/1900";
                string _value = "1";
                if (optThang.Checked)
                {
                    if (cboThang.SelectedIndex < 0)
                    {
                        Utility.ShowMsg("Bạn phải chọn Tháng báo cáo");
                        cboThang.Focus();
                        return;
                    }
                    _value = cboThang.SelectedValue.ToString();
                    switch (_value)
                    {
                        case "2":
                            fromdate = new DateTime(dtpNam.Value.Year, Utility.Int32Dbnull(_value, 2), 1).ToString("dd/MM/yyyy");
                            todate = new DateTime(dtpNam.Value.Year, Utility.Int32Dbnull(_value, 2), 29).ToString("dd/MM/yyyy");
                            break;
                        case "4":
                        case "6":
                        case "9":
                        case "11":
                            fromdate = new DateTime(dtpNam.Value.Year, Utility.Int32Dbnull(_value, 2), 1).ToString("dd/MM/yyyy");
                            todate = new DateTime(dtpNam.Value.Year, Utility.Int32Dbnull(_value, 2), 30).ToString("dd/MM/yyyy");
                            break;
                        default:
                            fromdate = new DateTime(dtpNam.Value.Year, Utility.Int32Dbnull(_value, 2), 1).ToString("dd/MM/yyyy");
                            todate = new DateTime(dtpNam.Value.Year, Utility.Int32Dbnull(_value, 2), 31).ToString("dd/MM/yyyy");
                            break;
                    }
                }
                else if (optQuy.Checked)
                {
                    if (cboQuy.SelectedIndex < 0)
                    {
                        Utility.ShowMsg("Bạn phải chọn Quý báo cáo");
                        cboQuy.Focus();
                        return;
                    }
                    _value = cboQuy.SelectedValue.ToString();
                    switch (_value)
                    {
                        case "1":
                            fromdate = new DateTime(dtpNam.Value.Year, 1, 1).ToString("dd/MM/yyyy");
                            todate = new DateTime(dtpNam.Value.Year, 3, 31).ToString("dd/MM/yyyy");
                            break;
                        case "2":
                            fromdate = new DateTime(dtpNam.Value.Year, 4, 1).ToString("dd/MM/yyyy");
                            todate = new DateTime(dtpNam.Value.Year, 6, 30).ToString("dd/MM/yyyy");
                            break;
                        case "3":
                            fromdate = new DateTime(dtpNam.Value.Year, 7, 1).ToString("dd/MM/yyyy");
                            todate = new DateTime(dtpNam.Value.Year, 9, 30).ToString("dd/MM/yyyy");
                            break;
                        case "4":
                            fromdate = new DateTime(dtpNam.Value.Year, 10, 1).ToString("dd/MM/yyyy");
                            todate = new DateTime(dtpNam.Value.Year, 12, 31).ToString("dd/MM/yyyy");
                            break;
                        default:
                            fromdate = new DateTime(dtpNam.Value.Year, 1, 1).ToString("dd/MM/yyyy");
                            todate = new DateTime(dtpNam.Value.Year, 12, 31).ToString("dd/MM/yyyy");
                            break;
                    }
                }
                else if (optNam.Checked)
                {
                    fromdate = new DateTime(dtpNam.Value.Year, 1, 1).ToString("dd/MM/yyyy");
                    todate = new DateTime(dtpNam.Value.Year, 12, 31).ToString("dd/MM/yyyy");
                }
                else
                {
                    fromdate = dtFromDate.Value.ToString("dd/MM/yyyy");
                    todate = dtToDate.Value.ToString("dd/MM/yyyy");
                }
                byte kieubiendong = (byte)cboKieutonghop.SelectedIndex;
                string xmlFile = "ThuocBaocaophatsinhTonghop.xml";
                if (cboKieubangke.SelectedIndex == 0)
                {
                    xmlFile = "ThuocBaocaophatsinhChitiet.xml";
                    m_dtReport = BAOCAO_THUOC.ThuocBaocaophatsinhChitiet(fromdate, todate,
                   Utility.Int32Dbnull(txtKho.MyID), Utility.Int32Dbnull(txtthuoc.MyID, -1), kieubiendong, 
                   nhomthuoc, 1,Utility.Int32Dbnull(txtKhoXuat.MyID,-1));
                }
                else
                {
                    
                    xmlFile = "ThuocBaocaophatsinhTonghop.xml";
                    m_dtReport = BAOCAO_THUOC.ThuocBaocaophatsinhTonghop(fromdate, todate,
                    Utility.Int32Dbnull(txtKho.MyID), Utility.Int32Dbnull(txtthuoc.MyID, -1), kieubiendong,
                    nhomthuoc, 1,Utility.Int32Dbnull(txtKhoXuat.MyID,-1));
                }
                THU_VIEN_CHUNG.CreateXML(m_dtReport, xmlFile);

                Utility.SetDataSourceForDataGridEx(cboKieutonghop.SelectedIndex==0?grdTonghop: grdChitiet, m_dtReport, true, true, "1=1", "");
                if (m_dtReport == null || m_dtReport.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu báo cáo", "Thông báo");
                    return;
                }
                string FromDateToDate = Utility.FromToDateTime(dtFromDate.Text, dtToDate.Text);
                thuoc_baocao.ThuocBaocaophatsinh(m_dtReport, baocaO_TIEUDE1.MA_BAOCAO, baocaO_TIEUDE1.TIEUDE,
                                             dtNgayIn.Value, FromDateToDate,
                                             Utility.sDbnull(txtKho.Text));
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

       
       
        /// <summary>
        /// hàm thực hiện việc phím tắt thông tin 
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_tonghopphatsinh_nhapxuat_KeyDown(object sender, KeyEventArgs e)
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

                if (grdChitiet.RowCount <= 0)
                {
                    Utility.ShowMsg("Không có dữ liệu chi tiết để xuất file excel", "Thông báo");
                    return;
                }
                gridEXExporter1.GridEX = grdChitiet;
                saveFileDialog1.Filter = "Excel File(*.xls)|*.xls";
                saveFileDialog1.FileName = string.Format("{0}.xls", baocaO_TIEUDE1.TIEUDE);
                //saveFileDialog1.ShowDialog();
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string sPath = saveFileDialog1.FileName;
                    var fs = new FileStream(sPath, FileMode.Create);
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

        private void frm_tonghopphatsinh_nhapxuat_KeyDown_1(object sender, KeyEventArgs e)
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
        private void txtKho_TextChanged(object sender, EventArgs e)
        {
            //if (!allowChanged) return;
            //SelectStock();
            //modifyTieude();
        }

        private void cboKieutonghop_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if(cboKieutonghop.SelectedIndex==0)
            {
                lblKhoXuat.Visible = false;
                lblKhonhap.Text = "Chọn kho nhập:";
                txtKhoXuat.MyID = -1;
                txtKhoXuat._Text = "";
                txtKhoXuat.Visible = false;
            }
            else
            {
                lblKhoXuat.Visible = true;
                lblKhonhap.Text = "Chọn kho xuất:";
                lblKhoXuat.Text = "Chọn kho nhập:";
                txtKhoXuat.MyID = -1;
                txtKhoXuat._Text = "";
                txtKhoXuat.Visible = true;
            }
        }

        private void cboQuy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (optQuy.Checked)
            {
                var fromdate = new DateTime();
                var todate = new DateTime();
                switch (Utility.sDbnull(cboQuy.SelectedValue))
                {
                    case "1":
                        fromdate = new DateTime(dtpNam.Value.Year, 1, 1);
                        dtFromDate.Value = fromdate;

                        todate = new DateTime(dtpNam.Value.Year, 3, 31);
                        dtToDate.Value = todate;
                        break;
                    case "2":
                        fromdate = new DateTime(dtpNam.Value.Year, 4, 1);
                        dtFromDate.Value = fromdate;

                        todate = new DateTime(dtpNam.Value.Year, 6, 30);
                        dtToDate.Value = todate;
                        break;
                    case "3":
                        fromdate = new DateTime(dtpNam.Value.Year, 7, 1);
                        dtFromDate.Value = fromdate;

                        todate = new DateTime(dtpNam.Value.Year, 9, 30);
                        dtToDate.Value = todate;
                        break;
                    case "4":
                        fromdate = new DateTime(dtpNam.Value.Year, 10, 1);
                        dtFromDate.Value = fromdate;

                        todate = new DateTime(dtpNam.Value.Year, 12, 31);
                        dtToDate.Value = todate;
                        break;
                    default:
                        fromdate = new DateTime(dtpNam.Value.Year, 1, 1);
                        dtFromDate.Value = fromdate;

                        todate = new DateTime(dtpNam.Value.Year, 12, 31);
                        dtToDate.Value = todate;
                        break;
                }
            }
        }

        private void cboThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (optThang.Checked)
            {
                var myDate = cboThang.SelectedValue;
                //  fromdate = new DateTime(dtpNam.Value.Year, 1, 1).ToString("dd/MM/yyyy");
                // todate = new DateTime(dtpNam.Value.Year, 3, 31).ToString("dd/MM/yyyy");
                var startOfMonth = new DateTime(dtpNam.Value.Year, Utility.Int32Dbnull(myDate), 1);
                dtFromDate.Value = startOfMonth;
                var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);
                dtToDate.Value = endOfMonth;
            }
        }

        private void dtpNam_ValueChanged(object sender, EventArgs e)
        {
            //if (optNam.Checked)
            //{
            //    var myDate = dtpNam.Value;
            //    //  fromdate = new DateTime(dtpNam.Value.Year, 1, 1).ToString("dd/MM/yyyy");
            //    // todate = new DateTime(dtpNam.Value.Year, 3, 31).ToString("dd/MM/yyyy");
            //    var startOfMonth = new DateTime(dtpNam.Value.Year, 1, 1);
            //    dtFromDate.Value = startOfMonth;
            //    var endOfMonth = new DateTime(dtpNam.Value.Year, 12, 31);
            //    dtToDate.Value = endOfMonth;
            //}
        }

        private void optNam_CheckedChanged(object sender, EventArgs e)
        {
            if (optNam.Checked)
            {
                var myDate = dtpNam.Value;
                //  fromdate = new DateTime(dtpNam.Value.Year, 1, 1).ToString("dd/MM/yyyy");
                // todate = new DateTime(dtpNam.Value.Year, 3, 31).ToString("dd/MM/yyyy");
                var startOfMonth = new DateTime(dtpNam.Value.Year, 1, 1);
                dtFromDate.Value = startOfMonth;
                var endOfMonth = new DateTime(dtpNam.Value.Year, 12, 31);
                dtToDate.Value = endOfMonth;
            }
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            txtthuoc.ShowMe();
        }
    }
}