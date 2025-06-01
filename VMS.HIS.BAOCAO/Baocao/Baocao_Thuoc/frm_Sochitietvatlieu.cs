using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SubSonic;
using VNS.HIS.BusRule.Classes;
using VNS.HIS.DAL;
using VNS.HIS.UI.Baocao;
using VNS.Libs;
using VNS.Properties;

namespace VNS.HIS.UI.BaoCao.Form_BaoCao
{
    public partial class frm_Sochitietvatlieu : Form
    {
        private readonly string KieuKho = "ALL"; //ALL: Bốc cả kho thuốc chẵn lẻ; CHAN=BỐC KHO CHẴN;LE=BỐC KHO LẺ
        private string KIEU_THUOC_VT = "THUOC";
        private TDmucKho _item;
        private bool allowChanged;
        private DataTable m_dtDrugData = new DataTable();

        public frm_Sochitietvatlieu(string args)
        {
            InitializeComponent();
            KieuKho = args.Split('-')[0];
            KIEU_THUOC_VT = args.Split('-')[1];

            dtNgayIn.Value = dtFromDate.Value = dtToDate.Value = globalVariables.SysDate;
            cmdExit.Click += cmdExit_Click;
            Load += frm_Sochitietvatlieu_Load;
            cmdBaoCao.Click += cmdBaoCao_Click;
            KeyDown += frm_Sochitietvatlieu_KeyDown;
            cboKho.SelectedIndexChanged += cboKho_SelectedIndexChanged;
        }

        private void chkSimple_CheckedChanged(object sender, EventArgs e)
        {
            modifyTieude();
        }

        private void chkChanle_CheckedChanged(object sender, EventArgs e)
        {
            modifyTieude();
        }

        private void chkThekhochitiet_CheckedChanged(object sender, EventArgs e)
        {
            if (!allowChanged) return;
            if (_item == null)
                _item =
                    new Select().From(TDmucKho.Schema).Where(TDmucKho.IdKhoColumn).IsEqualTo(
                        Utility.Int32Dbnull(cboKho.SelectedValue)).ExecuteSingle<TDmucKho>();
            GetKieuThuocVT();
        }

        private void txtLoaithuoc__OnEnterMe()
        {
        }


        private void txtLoaithuoc__OnSelectionChanged()
        {
        }

        private void cboKho_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!allowChanged) return;
            SelectStock();
            modifyTieude();
        }

        private void SelectStock()
        {
            if (Utility.Int32Dbnull(cboKho.SelectedValue, -1) < 0)
                _item = null;
            else
            {
                _item =
                    new Select().From(TDmucKho.Schema).Where(TDmucKho.IdKhoColumn).IsEqualTo(
                        Utility.Int32Dbnull(cboKho.SelectedValue)).ExecuteSingle<TDmucKho>();
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
                SPs.ThuocLayDanhmucLoaiThuocTheokho(Utility.Int32Dbnull(cboKho.SelectedValue, -1)).GetDataSet().Tables[0
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
                    SPs.ThuocLayDanhmucThuocTheokho(Utility.Int32Dbnull(cboKho.SelectedValue, -1)).GetDataSet().Tables[0
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
            baocaO_TIEUDE1.Init("thuoc_sochitietvatlieu");
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
        private void frm_Sochitietvatlieu_Load(object sender, EventArgs e)
        {
            if (KIEU_THUOC_VT == "THUOC")
                baocaO_TIEUDE1.Init("thuoc_sochitietvatlieu");
            else
                baocaO_TIEUDE1.Init("vt_sochitietvatlieu");

            DataBinding.BindData(cboKho,
                                 KieuKho == "ALL"
                                     ? CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_TATCA()
                                     : (KieuKho == "CHAN"
                                            ? CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_CHAN()
                                            : CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_LE()), TDmucKho.Columns.IdKho,
                                 TDmucKho.Columns.TenKho);
            DataTable m_dtNhomThuoc = new Select().From(DmucLoaithuoc.Schema)
                .OrderAsc(DmucLoaithuoc.Columns.SttHthi).ExecuteDataSet().Tables[0];
            allowChanged = true;
            cboKho_SelectedIndexChanged(cboKho, e);
            cboThang.SelectedIndex = globalVariables.SysDate.Month - 1;
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
                if (cboKho.SelectedIndex < 0)
                {
                    Utility.ShowMsg("Bạn phải chọn Kho thuốc để xem thẻ thuốc");
                    cboKho.Focus();
                    return;
                }
                if (txtthuoc.MyCode == "-1")
                {
                    Utility.ShowMsg("Bạn phải chọn thuốc để xem thẻ thuốc");
                    txtthuoc.Focus();
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
                m_dtReport =
                    BAOCAO_THUOC.ThuocThethuocChitiet(fromdate, todate,
                        Utility.Int32Dbnull(cboKho.SelectedValue), Utility.Int32Dbnull(txtthuoc.MyID, -1),
                        nhomthuoc, chkBiendong.Checked ? 1 : 0);
                if (_item != null && Utility.Byte2Bool(_item.LaTuthuoc))
                    THU_VIEN_CHUNG.CreateXML(m_dtReport, "Thuoc_sochitietvatlieu.xml");

                Utility.SetDataSourceForDataGridEx(grdListChitiet, m_dtReport, true, true, "1=1", "");
                if (m_dtReport == null || m_dtReport.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu báo cáo", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                string FromDateToDate = Utility.FromToDateTime(dtFromDate.Text, dtToDate.Text);


                ProcessDataChitiet(ref m_dtReport);
                thuoc_baocao.ThethuocChitiet(m_dtReport, KIEU_THUOC_VT, baocaO_TIEUDE1.TIEUDE,
                                             dtNgayIn.Value, FromDateToDate,
                                             Utility.sDbnull(cboKho.Text));



            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

       

        private void ProcessDataChitiet(ref DataTable m_dtReport)
        {
            try
            {
                var lstAdded = new List<DataRow>();
                int startDate = Utility.Int32Dbnull(Utility.GetYYYYMMDD(dtFromDate.Value), 0);
                int EndDate = Utility.Int32Dbnull(Utility.GetYYYYMMDD(dtToDate.Value), 0);

                int iFound = 0;
                int idx = 0;
                foreach (DataRow dr in m_dtReport.Rows)
                {
                    DateTime _ngaybiendong = Convert.ToDateTime(dr[TBiendongThuoc.Columns.NgayBiendong]);
                    long _myId = Utility.Int64Dbnull(dr[TBiendongThuoc.Columns.IdBiendong], 0);
                    iFound++;
                    if (iFound == 1) //Tính tồn cuối dòng đầu tiên tìm thấy
                    {
                        dr["Toncuoi"] = Utility.Int32Dbnull(dr["Tondau"], 0) + Utility.Int32Dbnull(dr["SoLuongNhap"], 0)
                                        - Utility.Int32Dbnull(dr["SoLuongXuat"], 0);
                    }
                    else
                    {
                        OrderedEnumerableRowCollection<DataRow> q = from p in m_dtReport.AsEnumerable()
                                                                    where
                                                                        Convert.ToDateTime(
                                                                            p[TBiendongThuoc.Columns.NgayBiendong]) <=
                                                                        _ngaybiendong
                                                                        &&
                                                                        Utility.Int64Dbnull(
                                                                            p[TBiendongThuoc.Columns.IdBiendong], 0) !=
                                                                        _myId
                                                                        && Utility.Int32Dbnull(p["processed"], 0) > 0
                                                                    orderby
                                                                        Utility.Int32Dbnull(p["processed"], 0)
                                                                        descending
                                                                    //Convert.ToDateTime(p[TBiendongThuoc.Columns.NgayBiendong]) descending
                                                                    select p;
                        if (q.Count() > 0)
                        {
                            DataRow drPrevious = q.FirstOrDefault();
                            dr["Tondau"] = Utility.Int32Dbnull(drPrevious["Toncuoi"], 0);
                            dr["Toncuoi"] = Utility.Int32Dbnull(dr["Tondau"], 0) +
                                            Utility.Int32Dbnull(dr["SoLuongNhap"], 0)
                                            - Utility.Int32Dbnull(dr["SoLuongXuat"], 0);
                        }
                    }
                    idx++;
                    dr["processed"] = idx;
                    m_dtReport.AcceptChanges();
                }
            }
            catch
            {
            }
        }

       
        /// <summary>
        /// hàm thực hiện việc phím tắt thông tin 
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_Sochitietvatlieu_KeyDown(object sender, KeyEventArgs e)
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

                if (grdListChitiet.RowCount <= 0)
                {
                    Utility.ShowMsg("Không có dữ liệu chi tiết để xuất file excel", "Thông báo");
                    return;
                }
                gridEXExporter1.GridEX = grdListChitiet;
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

        private void frm_Sochitietvatlieu_KeyDown_1(object sender, KeyEventArgs e)
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