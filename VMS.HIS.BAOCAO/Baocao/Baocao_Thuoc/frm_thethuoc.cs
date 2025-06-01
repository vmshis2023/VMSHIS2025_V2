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
    public partial class frm_thethuoc : Form
    {
        private readonly string _kieuKho = "ALL"; //ALL: Bốc cả kho thuốc chẵn lẻ; CHAN=BỐC KHO CHẴN;LE=BỐC KHO LẺ
        private string _kieuThuocVt = "THUOC";
        private TDmucKho _item;
        private bool _allowChanged;
        private DataTable _mDtDrugData = new DataTable();

        public frm_thethuoc(string args)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            _kieuKho = args.Split('-')[0];
            _kieuThuocVt = args.Split('-')[1];

            dtNgayIn.Value = dtFromDate.Value = dtToDate.Value = globalVariables.SysDate;
            cmdExit.Click += cmdExit_Click;
            Load += frm_thethuoc_Load;
            KeyDown += frm_thethuoc_KeyDown;
            cboKho.SelectedIndexChanged += cboKho_SelectedIndexChanged;
            gridEXExporter1.GridEX = grdListKhoChan;
            radThekhochitiet.CheckedChanged += radThekhochitiet_CheckedChanged;
            chkChanle.CheckedChanged += chkChanle_CheckedChanged;
            radTheXuatNhapTon.CheckedChanged += radSimple_CheckedChanged;
            CauHinh();
        }

       

        private void chkChanle_CheckedChanged(object sender, EventArgs e)
        {
            ModifyTieude();
        }

        private void radThekhochitiet_CheckedChanged(object sender, EventArgs e)
        {
            if (!_allowChanged) return;
            if (_item == null)
                _item =
                    new Select().From(TDmucKho.Schema).Where(TDmucKho.IdKhoColumn).IsEqualTo(
                        Utility.Int32Dbnull(cboKho.SelectedValue)).ExecuteSingle<TDmucKho>();
            GetKieuThuocVt();
           //radSimple.Checked= radThekhoThang.Checked = !radThekhochitiet.Checked;
        }

        private void cboKho_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_allowChanged) return;
            SelectStock();
            ModifyTieude();
        }

        private void SelectStock()
        {
            if (Utility.Int32Dbnull(txtkho.MyID, -1) < 0)
                _item = null;
            else
            {
                _item =
                    new Select().From(TDmucKho.Schema).Where(TDmucKho.IdKhoColumn).IsEqualTo(
                        Utility.Int32Dbnull(txtkho.MyID,-1)).ExecuteSingle<TDmucKho>();
                GetKieuThuocVt();
                BindThuocVt();
            }
        }

        private void BindThuocVt()
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
                    SPs.ThuocLayDanhmucThuocTheokho(Utility.Int32Dbnull(txtkho.MyID, -1)).GetDataSet().Tables[0
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

        private void ModifyTieude()
        {
            if (_kieuThuocVt == "THUOC")
            {
                if (radTheXuatNhapTon.Checked)
                {
                    baocaO_TIEUDE1.Init("thuoc_thethuoc");
                    grdThethuoc.BringToFront();
                }
                else
                {
                    if (radThekhochitiet.Checked)
                    {
                        baocaO_TIEUDE1.Init("thuoc_thethuoc_chitiet");
                        grdListChitiet.BringToFront();
                    }
                    else
                    {
                        if (_item != null && Utility.Byte2Bool(_item.LaTuthuoc))
                        {
                            baocaO_TIEUDE1.Init("thuoc_thethuoc_tutruc");
                            grdThethuoctutruc.BringToFront();
                        }
                        else if (_item.KieuKho == "CHAN" || (chkChanle.Enabled && chkChanle.Checked))
                        {
                            baocaO_TIEUDE1.Init("thuoc_thethuoc_khochan");
                            grdListKhoChan.BringToFront();
                        }
                        else
                        {
                            baocaO_TIEUDE1.Init("thuoc_thethuoc_khole");
                            grdListKhole.BringToFront();
                        }
                    }
                }
            }
            else //VTTH
            {
                if (radTheXuatNhapTon.Checked)
                {
                    baocaO_TIEUDE1.Init("vt_thevt");
                    grdThethuoc.BringToFront();
                }
                else
                {
                    if (radThekhochitiet.Checked)
                    {
                        baocaO_TIEUDE1.Init("vt_thevt_chitiet");
                        grdListChitiet.BringToFront();
                    }
                    else
                    {
                        if (_item != null && Utility.Byte2Bool(_item.LaTuthuoc))
                        {
                            baocaO_TIEUDE1.Init("thuoc_thevt_tutruc");
                            grdThethuoctutruc.BringToFront();
                        }
                        else if (_item.KieuKho == "CHAN" || (chkChanle.Enabled && chkChanle.Checked))
                        {
                            baocaO_TIEUDE1.Init("vt_thevt_khochan");
                            grdListKhoChan.BringToFront();
                        }
                        else
                        {
                            baocaO_TIEUDE1.Init("vt_thevt_khole");
                            grdListKhole.BringToFront();
                        }
                    }
                }
            }
        }

        private void GetKieuThuocVt()
        {
            try
            {
                if (_item != null)
                {
                    _kieuThuocVt = _item.KhoThuocVt;

                    lblLoaikho.Text = _item.KieuKho == "CHAN"
                                          ? "Kho chẵn"
                                          : (_item.KieuKho == "LE" ? "Kho lẻ" : "Kho chẵn lẻ");
                    chkChanle.Enabled = _item.KieuKho == "CHANLE";
                    ModifyTieude();
                }
                else
                {
                    lblLoaikho.Text = "Không xác định";
                    _kieuThuocVt = "ALL";
                }
            }
            catch
            {
                _kieuThuocVt = "ALL";
            }
        }

        private void CauHinh()
        {
            HisDuocProperties = PropertyLib._HisDuocProperties;
        }
      
        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frm_thethuoc_Load(object sender, EventArgs e)
        {
            if (_kieuThuocVt == "THUOC")
                baocaO_TIEUDE1.Init("thuoc_thethuoc");
            else
                baocaO_TIEUDE1.Init("vt_thevt");

            txtkho.Init(CommonLoadDuoc.LAYDANHMUCKHO(-1,"ALL", _kieuThuocVt,_kieuKho, 100, 100, 1),
                                         new List<string>() { TDmucKho.Columns.IdKho, TDmucKho.Columns.MaKho, TDmucKho.Columns.TenKho });
            //DataBinding.BindData(cboKho,
            //                     _kieuKho == "ALL"
            //                         ? CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_TATCA()
            //                         : (_kieuKho == "CHAN"
            //                                ? CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_CHAN()
            //                                : CommonLoadDuoc.LAYTHONGTIN_KHOTHUOC_LE()), TDmucKho.Columns.IdKho,
            //                     TDmucKho.Columns.TenKho);
            DataTable m_dtNhomThuoc = new Select().From(DmucLoaithuoc.Schema)
                .OrderAsc(DmucLoaithuoc.Columns.SttHthi).ExecuteDataSet().Tables[0];
            _allowChanged = true;
            //cboKho_SelectedIndexChanged(cboKho, e);
            cboThang.SelectedIndex = globalVariables.SysDate.Month - 1;
            dtpNam.Value = globalVariables.SysDate;
        }

        private void cmdBaoCao_Click(object sender, EventArgs e)
        {
            try
            {
                if (_kieuThuocVt == "THUOCVT")
                    _kieuThuocVt = "THUOC";
                string nhomthuoc = "-1";
                if (Utility.Int32Dbnull(txtkho.MyID,-1) < 0)
                {
                    Utility.ShowMsg("Bạn phải chọn Kho thuốc để xem thẻ thuốc");
                    txtkho.Focus();
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
               
                    fromdate = dtFromDate.Value.ToString("dd/MM/yyyy");
                    todate = dtToDate.Value.ToString("dd/MM/yyyy");
                if (radTheXuatNhapTon.Checked)
                {
                    m_dtReport =
                        BAOCAO_THUOC.ThuocThethuoc(fromdate, todate,
                                                 Utility.Int32Dbnull(txtkho.MyID),
                                                   Utility.Int32Dbnull(txtthuoc.MyID, -1), nhomthuoc,
                                                   chkBiendong.Checked ? 1 : 0);
                }
                else
                {
                    if (radThekhochitiet.Checked )
                    {
                        if (_item != null && Utility.Byte2Bool(_item.LaTuthuoc))
                            m_dtReport =
                                BAOCAO_THUOC.ThuocThethuocTutrucChitiet(fromdate, todate,
                               Utility.Int32Dbnull(txtkho.MyID), Utility.Int32Dbnull(txtthuoc.MyID, -1),
                                    nhomthuoc, chkBiendong.Checked ? 1 : 0,Utility.Int64Dbnull(txtIdthuockho.Text, -1));
                        else
                            m_dtReport =
                                BAOCAO_THUOC.ThuocThethuocChitiet(fromdate, todate,
                                 Utility.Int32Dbnull(txtkho.MyID), Utility.Int32Dbnull(txtthuoc.MyID, -1),
                                    nhomthuoc, chkBiendong.Checked ? 1 : 0, Utility.Int64Dbnull(txtIdthuockho.Text, -1));
                    }
                    else
                    {
                        if (radThekhoThang.Checked)
                        {
                            m_dtReport =
                              BAOCAO_THUOC.ThuocThethuocThang(fromdate, todate,
                                                 Utility.Int32Dbnull(txtkho.MyID),
                                                  Utility.Int32Dbnull(txtthuoc.MyID, -1), nhomthuoc,
                                                  chkBiendong.Checked ? 1 : 0);
                        }
                        else
                        {
                            if (_item != null && Utility.Byte2Bool(_item.LaTuthuoc))
                                m_dtReport =
                                    BAOCAO_THUOC.ThuocThethuocTutruc(fromdate, todate,
                                        Utility.Int32Dbnull(txtkho.MyID), Utility.Int32Dbnull(txtthuoc.MyID, -1),
                                        nhomthuoc, chkBiendong.Checked ? 1 : 0);
                            else
                            {
                                if (_item.KieuKho == "CHAN" || (chkChanle.Enabled && chkChanle.Checked))
                                    m_dtReport =
                                        BAOCAO_THUOC.ThuocThethuocKhochan(fromdate, todate,
                                            Utility.Int32Dbnull(txtkho.MyID),
                                            Utility.Int32Dbnull(txtthuoc.MyID, -1), nhomthuoc, chkBiendong.Checked ? 1 : 0);
                                else
                                    m_dtReport =
                                        BAOCAO_THUOC.ThuocThethuocKhole(fromdate, todate,
                                        Utility.Int32Dbnull(txtkho.MyID),
                                            Utility.Int32Dbnull(txtthuoc.MyID, -1), nhomthuoc, chkBiendong.Checked ? 1 : 0);
                            }
                        }
                        
                    }
                }
                if (_item != null && Utility.Byte2Bool(_item.LaTuthuoc))
                    THU_VIEN_CHUNG.CreateXML(m_dtReport,
                                             radThekhochitiet.Checked
                                                 ? "ThuocThethuocTutrucChitiet.xml"
                                                 : "ThuocThethuocTutruc.xml");
                else
                    THU_VIEN_CHUNG.CreateXML(m_dtReport,
                                             radThekhochitiet.Checked
                                                 ? "thethuoc_chitiet.xml"
                                                 : (_item.KieuKho == "CHAN" || (chkChanle.Enabled && chkChanle.Checked)
                                                        ? "Thethuoc_khochan.xml"
                                                        : "Thethuoc_khole.xml"));
                Utility.SetDataSourceForDataGridEx(
                    radTheXuatNhapTon.Checked
                        ? grdThethuoc
                        : (
                              radThekhochitiet.Checked
                                  ? grdListChitiet
                                  : (
                                        _item != null && Utility.Byte2Bool(_item.LaTuthuoc)
                                            ? grdThethuoctutruc
                                            : (_item.KieuKho == "CHAN" || (chkChanle.Enabled && chkChanle.Checked)
                                                   ? grdListKhoChan
                                                   : grdListKhole)
                                    )
                          )
                    , m_dtReport, true, true, "1=1", "");
                if (m_dtReport==null || m_dtReport.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu báo cáo", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                string fromDateToDate = Utility.FromToDateTime(dtFromDate.Text, dtToDate.Text);
                if (radTheXuatNhapTon.Checked)
                {
                    
                    ProcessDataThethuoc(ref m_dtReport);
                    thuoc_baocao.Thethuoc(m_dtReport, _kieuThuocVt, baocaO_TIEUDE1.TIEUDE,
                                        dtNgayIn.Value, fromDateToDate,
                                        Utility.sDbnull(txtkho.Text));
                  
                }
                else
                {
                    if (radThekhochitiet.Checked )
                    {
                        ProcessDataChitiet(ref m_dtReport);
                        thuoc_baocao.ThethuocChitiet(m_dtReport, _kieuThuocVt, baocaO_TIEUDE1.TIEUDE,
                                                     dtNgayIn.Value, fromDateToDate,
                                                     Utility.sDbnull(txtkho.Text));
                    }
                    else
                    {
                        if (radThekhoThang.Checked)
                        {
                            ProcessDataThethuoc_Thang(ref m_dtReport);
                          
                                thuoc_baocao.ThethuocThang(m_dtReport, _kieuThuocVt, baocaO_TIEUDE1.TIEUDE,
                                                dtNgayIn.Value, fromDateToDate,
                                                Utility.sDbnull(txtkho.Text));
                            
                        }
                        else
                        {
                            if (_item != null && Utility.Byte2Bool(_item.LaTuthuoc))
                            {
                                ProcessDataTutruc(ref m_dtReport);
                                thuoc_baocao.Thethuoctutruc(m_dtReport, _kieuThuocVt, baocaO_TIEUDE1.TIEUDE,
                                                            dtNgayIn.Value, fromDateToDate,
                                                            Utility.sDbnull(txtkho.Text));
                            }
                            else
                            {
                                if (_item.KieuKho == "CHAN" || (chkChanle.Enabled && chkChanle.Checked))
                                {
                                    ProcessDataKhochan(ref m_dtReport);
                                    thuoc_baocao.Thethuockhochan(m_dtReport, _kieuThuocVt, baocaO_TIEUDE1.TIEUDE,
                                                                 dtNgayIn.Value, fromDateToDate,
                                                                 Utility.sDbnull(txtkho.Text));
                                }
                                else
                                {
                                    ProcessDataKhole(ref m_dtReport);
                                    thuoc_baocao.ThethuocKhole(m_dtReport, _kieuThuocVt, baocaO_TIEUDE1.TIEUDE,
                                                               dtNgayIn.Value, fromDateToDate,
                                                               Utility.sDbnull(txtkho.Text));
                                }
                            }
                        }
                       
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void ProcessDataTutruc(ref DataTable mDtReport)
        {
            DataTable dtTemp = mDtReport.Clone();
            try
            {
                var lstAdded = new List<DataRow>();
                int startDate = Utility.Int32Dbnull(Utility.GetYYYYMMDD(dtFromDate.Value), 0);
                int EndDate = Utility.Int32Dbnull(Utility.GetYYYYMMDD(dtToDate.Value), 0);
                int iFound = 0;
                if (mDtReport.Rows.Count == 1 && Utility.sDbnull(mDtReport.Rows[0]["YYYYMMDD"], "") == "")
                {
                    mDtReport.Rows[0]["YYYYMMDD"] = startDate.ToString();
                    mDtReport.Rows[0][TBiendongThuoc.Columns.NgayBiendong] = dtFromDate.Value.ToString("dd/MM/yyyy");
                }
                DataRow rowdata = mDtReport.Rows[0];
                DateTime _dtmStartDate = dtFromDate.Value.Date;// Utility.FromYYYYMMDD2Datetime(startDate.ToString());
                //Vong lap nay tao cac du lieu tu tuong lai
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_THETHUOC_TUDONGTHEMDULIEU_TUONGLAI", "0", false) == "1")
                {
                    while (_dtmStartDate <= dtToDate.Value.Date)
                    {
                        DataRow[] arrDr = mDtReport.Select("YYYYMMDD=" + Utility.GetYYYYMMDD(_dtmStartDate));
                        if (arrDr.Length > 0)
                        {
                            //Không cần làm gì cả
                            rowdata = arrDr[0];
                        }
                        else
                        {
                            DataRow newDr = dtTemp.NewRow();
                            Utility.CopyData(rowdata, ref newDr);
                            newDr["YYYYMMDD"] = Utility.GetYYYYMMDD(_dtmStartDate);
                            newDr[TBiendongThuoc.Columns.NgayBiendong] = _dtmStartDate.ToString("dd/MM/yyyy");

                            newDr["Tontruoc"] = 0;
                            newDr["NHAP_KLE"] = 0;
                            newDr["XUAT_BN"] = 0;
                            newDr["TRA_KHOLE"] = 0;
                            newDr["TONKC"] = 0;
                            dtTemp.Rows.Add(newDr);
                        }
                        _dtmStartDate = _dtmStartDate.AddDays(1);
                    }
                }
                else
                {
                    dtTemp = mDtReport.Copy();
                }
                 _dtmStartDate = dtFromDate.Value.Date;
                while (_dtmStartDate <= dtToDate.Value.Date)
                {
                    DataRow[] arrDr = dtTemp.Select("YYYYMMDD=" + Utility.GetYYYYMMDD(_dtmStartDate));
                    if (arrDr.Length > 0)
                    {
                        iFound++;
                        if (iFound == 1) //Tính tồn cuối dòng đầu tiên tìm thấy
                        {
                            arrDr[0]["TONKC"] = Utility.DecimaltoDbnull(arrDr[0]["Tontruoc"], 0) +
                                                Utility.DecimaltoDbnull(arrDr[0]["NHAP_KLE"], 0)
                                                - Utility.DecimaltoDbnull(arrDr[0]["XUAT_BN"], 0) -
                                                Utility.DecimaltoDbnull(arrDr[0]["TRA_KHOLE"], 0);
                        }
                        else
                        {
                            OrderedEnumerableRowCollection<DataRow> q = from p in dtTemp.AsEnumerable()
                                                                        where Utility.Int32Dbnull(p["YYYYMMDD"], 0) < Utility.Int32Dbnull(Utility.GetYYYYMMDD(_dtmStartDate))
                                                                        orderby p["YYYYMMDD"] descending
                                                                        select p;
                            if (q.Count() > 0)
                            {
                                DataRow drPrevious = q.FirstOrDefault();
                                arrDr[0]["Tontruoc"] = Utility.DecimaltoDbnull(drPrevious["TONKC"], 0);
                                arrDr[0]["TONKC"] = Utility.DecimaltoDbnull(arrDr[0]["Tontruoc"], 0) +
                                                    Utility.DecimaltoDbnull(arrDr[0]["NHAP_KLE"], 0)
                                                    - Utility.DecimaltoDbnull(arrDr[0]["XUAT_BN"], 0) -
                                                    Utility.DecimaltoDbnull(arrDr[0]["TRA_KHOLE"], 0);
                            }
                        }
                        dtTemp.AcceptChanges();
                    }
                    else //Ca
                    {
                    }
                    _dtmStartDate = _dtmStartDate.AddDays(1);
                }
            }
            catch
            {
            }
            finally
            {
                mDtReport = dtTemp.Copy();
            }
        }

        private void ProcessDataKhole(ref DataTable mDtReport)
        {
            DataTable dtTemp = mDtReport.Clone();
            try
            {
                var lstAdded = new List<DataRow>();
                int startDate = Utility.Int32Dbnull(Utility.GetYYYYMMDD(dtFromDate.Value), 0);
                int EndDate = Utility.Int32Dbnull(Utility.GetYYYYMMDD(dtToDate.Value), 0);
                int iFound = 0;
                if (mDtReport.Rows.Count == 1 && Utility.sDbnull(mDtReport.Rows[0]["YYYYMMDD"], "") == "")
                {
                    mDtReport.Rows[0]["YYYYMMDD"] = startDate.ToString();
                    mDtReport.Rows[0][TBiendongThuoc.Columns.NgayBiendong] = dtFromDate.Value.Date;
                }
                DataRow rowdata = mDtReport.Rows[0];
                DateTime _dtmStartDate = dtFromDate.Value.Date;// Utility.FromYYYYMMDD2Datetime(startDate.ToString());
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_THETHUOC_TUDONGTHEMDULIEU_TUONGLAI", "0", false) == "1")
                {
                    while (_dtmStartDate <= dtToDate.Value.Date)
                    {
                        DataRow[] arrDr = mDtReport.Select("YYYYMMDD=" + Utility.GetYYYYMMDD(_dtmStartDate));
                        if (arrDr.Length > 0)
                        {
                            dtTemp.ImportRow(arrDr[0]);
                        }
                        else
                        {
                            DataRow newDr = dtTemp.NewRow();
                            Utility.CopyData(rowdata, ref newDr);
                            newDr["YYYYMMDD"] = Utility.GetYYYYMMDD(_dtmStartDate);
                            newDr[TBiendongThuoc.Columns.NgayBiendong] = _dtmStartDate.ToString("dd/MM/yyyy");

                            newDr["Tontruoc"] = 0;
                            newDr["NhapTuKhoChan"] = 0;
                            newDr["KPTRALAI"] = 0;
                            newDr["XUATKP"] = 0;
                            newDr["XuatBN"] = 0;
                            newDr["XuatTraKhoChan"] = 0;
                            newDr["Xuatkhac"] = 0;
                            newDr["TONKC"] = 0;
                            dtTemp.Rows.Add(newDr);
                        }
                        _dtmStartDate = _dtmStartDate.AddDays(1);
                    }
                }
                else
                {
                    dtTemp = mDtReport.Copy();
                }

                _dtmStartDate = dtFromDate.Value.Date;
                while (_dtmStartDate <= dtToDate.Value.Date)
                {
                    DataRow[] arrDr = dtTemp.Select("YYYYMMDD=" + Utility.GetYYYYMMDD(_dtmStartDate));
                    if (arrDr.Length > 0)
                    {
                        iFound++;
                        if (iFound == 1) //Tính tồn cuối dòng đầu tiên tìm thấy
                        {
                            arrDr[0]["TONKC"] = Utility.DecimaltoDbnull(arrDr[0]["Tontruoc"], 0) +
                                                Utility.DecimaltoDbnull(arrDr[0]["NhapTuKhoChan"], 0)
                                                + Utility.DecimaltoDbnull(arrDr[0]["KPTRALAI"], 0)
                                                - Utility.DecimaltoDbnull(arrDr[0]["XUATKP"], 0) -
                                                Utility.DecimaltoDbnull(arrDr[0]["XuatBN"], 0) -
                                                Utility.DecimaltoDbnull(arrDr[0]["XuatTraKhoChan"], 0) -
                                                Utility.DecimaltoDbnull(arrDr[0]["Xuatkhac"], 0);
                        }
                        else
                        {
                            OrderedEnumerableRowCollection<DataRow> q = from p in dtTemp.AsEnumerable()
                                                                        where Utility.Int32Dbnull(p["YYYYMMDD"], 0) < Utility.Int32Dbnull(Utility.GetYYYYMMDD(_dtmStartDate))
                                                                        orderby p["YYYYMMDD"] descending
                                                                        select p;
                            if (q.Count() > 0)
                            {
                                DataRow drPrevious = q.FirstOrDefault();
                                arrDr[0]["Tontruoc"] = Utility.DecimaltoDbnull(drPrevious["TONKC"], 0);
                                arrDr[0]["TONKC"] = Utility.DecimaltoDbnull(arrDr[0]["Tontruoc"], 0) +
                                                    Utility.DecimaltoDbnull(arrDr[0]["NhapTuKhoChan"], 0)
                                                    + Utility.DecimaltoDbnull(arrDr[0]["KPTRALAI"], 0)
                                                    - Utility.DecimaltoDbnull(arrDr[0]["XUATKP"], 0) -
                                                    Utility.DecimaltoDbnull(arrDr[0]["XuatBN"], 0) -
                                                    Utility.DecimaltoDbnull(arrDr[0]["XuatTraKhoChan"], 0)
                                                    - Utility.DecimaltoDbnull(arrDr[0]["Xuatkhac"], 0);
                            }
                        }
                        dtTemp.AcceptChanges();
                    }
                    else
                    {
                    }
                    _dtmStartDate = _dtmStartDate.AddDays(1);
                }
            }
            catch
            {
            }
            finally
            {
                mDtReport = dtTemp.Copy();
            }
        }

        private void ProcessDataChitiet(ref DataTable mDtReport)
        {
            try
            {
                var lstAdded = new List<DataRow>();
                int startDate = Utility.Int32Dbnull(Utility.GetYYYYMMDD(dtFromDate.Value), 0);
                int endDate = Utility.Int32Dbnull(Utility.GetYYYYMMDD(dtToDate.Value), 0);

                int iFound = 0;
                int idx = 0;
                //Bảng đã được sắp xếp trong câu truy vấn
                DataRow drPrevious;
                foreach (DataRow dr in mDtReport.Rows)
                {
                    DateTime ngaybiendong = Convert.ToDateTime(dr[TBiendongThuoc.Columns.NgayBiendong]);
                    long myId = Utility.Int64Dbnull(dr[TBiendongThuoc.Columns.IdBiendong], 0);
                    iFound++;
                    if (iFound == 1) //Tính tồn cuối dòng đầu tiên tìm thấy
                    {
                        dr["Toncuoi"] = Utility.DecimaltoDbnull(dr["Tondau"], 0) + Utility.DecimaltoDbnull(dr["SoLuongNhap"], 0)
                                        - Utility.DecimaltoDbnull(dr["SoLuongXuat"], 0);
                    }
                    else
                    {
                        drPrevious = mDtReport.Rows[idx - 1];
                        //OrderedEnumerableRowCollection<DataRow> q = from p in mDtReport.AsEnumerable()
                        //                                            where
                        //                                                Convert.ToDateTime(p[TBiendongThuoc.Columns.NgayBiendong]) <= ngaybiendong
                        //                                                && Utility.Int64Dbnull(p[TBiendongThuoc.Columns.IdBiendong], 0) != myId
                        //                                                && Utility.Int32Dbnull(p["processed"], 0) > 0
                        //                                            orderby Utility.Int32Dbnull(p["id_biendong"], 0) ascending, Utility.Int32Dbnull(p["processed"], 0) descending
                        //                                            //Convert.ToDateTime(p[TBiendongThuoc.Columns.NgayBiendong]) descending
                        //                                            select p;
                        //if (q.Count() > 0)
                        //{
                        //    DataRow drPrevious = q.FirstOrDefault();
                        dr["Tondau"] = Utility.DecimaltoDbnull(drPrevious["Toncuoi"], 0);
                        dr["Toncuoi"] = Utility.DecimaltoDbnull(dr["Tondau"], 0) +
                                            Utility.DecimaltoDbnull(dr["SoLuongNhap"], 0)
                                            - Utility.DecimaltoDbnull(dr["SoLuongXuat"], 0);
                        //}
                    }
                    idx++;
                    dr["processed"] = idx;
                    mDtReport.AcceptChanges();
                }
                ////Backup 230828
                //foreach (DataRow dr in mDtReport.Rows)
                //{
                //    DateTime ngaybiendong = Convert.ToDateTime(dr[TBiendongThuoc.Columns.NgayBiendong]);
                //    long myId = Utility.Int64Dbnull(dr[TBiendongThuoc.Columns.IdBiendong], 0);
                //    iFound++;
                //    if (iFound == 1) //Tính tồn cuối dòng đầu tiên tìm thấy
                //    {
                //        dr["Toncuoi"] = Utility.Int32Dbnull(dr["Tondau"], 0) + Utility.Int32Dbnull(dr["SoLuongNhap"], 0)
                //                        - Utility.Int32Dbnull(dr["SoLuongXuat"], 0);
                //    }
                //    else
                //    {
                //        OrderedEnumerableRowCollection<DataRow> q = from p in mDtReport.AsEnumerable()
                //            where
                //                Convert.ToDateTime(p[TBiendongThuoc.Columns.NgayBiendong]) <= ngaybiendong
                //                &&  Utility.Int64Dbnull(p[TBiendongThuoc.Columns.IdBiendong], 0) != myId
                //                && Utility.Int32Dbnull(p["processed"], 0) > 0
                //            orderby Utility.Int32Dbnull(p["id_biendong"], 0) ascending,Utility.Int32Dbnull(p["processed"], 0) descending
                //            //Convert.ToDateTime(p[TBiendongThuoc.Columns.NgayBiendong]) descending
                //            select p;
                //        if (q.Count() > 0)
                //        {
                //            DataRow drPrevious = q.FirstOrDefault();
                //            dr["Tondau"] = Utility.Int32Dbnull(drPrevious["Toncuoi"], 0);
                //            dr["Toncuoi"] = Utility.Int32Dbnull(dr["Tondau"], 0) +
                //                            Utility.Int32Dbnull(dr["SoLuongNhap"], 0)
                //                            - Utility.Int32Dbnull(dr["SoLuongXuat"], 0);
                //        }
                //    }
                //    idx++;
                //    dr["processed"] = idx;
                //    mDtReport.AcceptChanges();
                //}
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
        }

        private void ProcessDataKhochan(ref DataTable mDtReport)
        {
            DataTable dtTemp = mDtReport.Clone();
            try
            {
                var lstAdded = new List<DataRow>();
                int startDate = Utility.Int32Dbnull(Utility.GetYYYYMMDD(dtFromDate.Value), 0);
                int EndDate = Utility.Int32Dbnull(Utility.GetYYYYMMDD(dtToDate.Value), 0);
                if (mDtReport.Rows.Count == 1 && Utility.sDbnull(mDtReport.Rows[0]["YYYYMMDD"], "") == "")
                {
                    mDtReport.Rows[0]["YYYYMMDD"] = startDate.ToString();
                    mDtReport.Rows[0][TBiendongThuoc.Columns.NgayBiendong] = dtFromDate.Value.Date;
                }
                int iFound = 0;
                DataRow rowdata = mDtReport.Rows[0];
                DateTime _dtmStartDate = dtFromDate.Value.Date;// Utility.FromYYYYMMDD2Datetime(startDate.ToString());
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_THETHUOC_TUDONGTHEMDULIEU_TUONGLAI", "0", false) == "1")
                {
                    while (_dtmStartDate <= dtToDate.Value.Date)
                    {
                        DataRow[] arrDr = mDtReport.Select("YYYYMMDD=" + Utility.GetYYYYMMDD(_dtmStartDate));
                        if (arrDr.Length > 0)
                        {
                            dtTemp.ImportRow(arrDr[0]);
                        }
                        else
                        {
                            DataRow newDr = dtTemp.NewRow();
                            Utility.CopyData(rowdata, ref newDr);
                            newDr["YYYYMMDD"] = Utility.GetYYYYMMDD(_dtmStartDate);
                            newDr[TBiendongThuoc.Columns.NgayBiendong] = _dtmStartDate.ToString("dd/MM/yyyy");

                            newDr["TonThangKetChuyen"] = 0;
                            newDr["TonThangTruocKetChuyen"] = 0;
                            newDr["SoLuongNhap"] = 0;
                            newDr["Tralaitukhole"] = 0;
                            newDr["SoLuongXuat"] = 0;
                            newDr["Tranhacungcap"] = 0;
                            newDr["Xuatkhac"] = 0;
                            dtTemp.Rows.Add(newDr);
                        }
                        _dtmStartDate = _dtmStartDate.AddDays(1);
                    }
                }
                else
                {
                    dtTemp = mDtReport.Copy();
                }
                _dtmStartDate = dtFromDate.Value.Date;
                while (_dtmStartDate <= dtToDate.Value.Date)
                {
                    DataRow[] arrDr = dtTemp.Select("YYYYMMDD=" + Utility.GetYYYYMMDD(_dtmStartDate));
                    if (arrDr.Length > 0)
                    {
                        iFound++;
                        if (iFound == 1) //Tính tồn cuối dòng đầu tiên tìm thấy
                        {
                            arrDr[0]["TonThangKetChuyen"] = Utility.DecimaltoDbnull(arrDr[0]["TonThangTruocKetChuyen"], 0) +
                                                            Utility.DecimaltoDbnull(arrDr[0]["SoLuongNhap"], 0)
                                                            + Utility.DecimaltoDbnull(arrDr[0]["Tralaitukhole"], 0)
                                                            - Utility.DecimaltoDbnull(arrDr[0]["SoLuongXuat"], 0) -
                                                            Utility.DecimaltoDbnull(arrDr[0]["Tranhacungcap"], 0) -
                                                            Utility.DecimaltoDbnull(arrDr[0]["Xuatkhac"], 0);
                        }
                        else
                        {
                            OrderedEnumerableRowCollection<DataRow> q = from p in dtTemp.AsEnumerable()
                                                                        where Utility.Int32Dbnull(p["YYYYMMDD"], 0) <  Utility.Int32Dbnull(Utility.GetYYYYMMDD(_dtmStartDate))
                                                                        orderby p["YYYYMMDD"] descending
                                                                        select p;
                            if (q.Count() > 0)
                            {
                                DataRow drPrevious = q.FirstOrDefault();
                                arrDr[0]["TonThangTruocKetChuyen"] = Utility.DecimaltoDbnull(
                                    drPrevious["TonThangKetChuyen"], 0);
                                arrDr[0]["TonThangKetChuyen"] =
                                    Utility.DecimaltoDbnull(arrDr[0]["TonThangTruocKetChuyen"], 0) +
                                    Utility.DecimaltoDbnull(arrDr[0]["SoLuongNhap"], 0)
                                    + Utility.DecimaltoDbnull(arrDr[0]["Tralaitukhole"], 0)
                                    - Utility.DecimaltoDbnull(arrDr[0]["SoLuongXuat"], 0) -
                                    Utility.DecimaltoDbnull(arrDr[0]["Tranhacungcap"], 0)
                                    - Utility.DecimaltoDbnull(arrDr[0]["Xuatkhac"], 0);
                            }
                        }
                        dtTemp.AcceptChanges();
                    }
                    else
                    {
                    }
                    _dtmStartDate = _dtmStartDate.AddDays(1);
                }
            }
            catch
            {
            }
            finally
            {
                mDtReport = dtTemp.Copy();
            }
        }

        private void ProcessDataThethuoc(ref DataTable mDtReport)
        {
           
            DataTable dtTemp = mDtReport.Clone();
            try
            {
                var lstAdded = new List<DataRow>();
                int startDate = Utility.Int32Dbnull(Utility.GetYYYYMMDD(dtFromDate.Value), 0);
                int EndDate = Utility.Int32Dbnull(Utility.GetYYYYMMDD(dtToDate.Value), 0);
                if (mDtReport.Rows.Count == 1 && Utility.sDbnull(mDtReport.Rows[0]["YYYYMMDD"], "") == "")
                {
                    mDtReport.Rows[0]["YYYYMMDD"] = startDate.ToString();
                    mDtReport.Rows[0][TBiendongThuoc.Columns.NgayBiendong] = dtFromDate.Value.Date.ToString("dd/MM/yyyy");
                }
                int iFound = 0;
                DataRow rowdata = mDtReport.Rows[0];
                DateTime _dtmStartDate = dtFromDate.Value.Date;// Utility.FromYYYYMMDD2Datetime(startDate.ToString());
                
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_THETHUOC_TUDONGTHEMDULIEU_TUONGLAI", "0", false) == "1")
                {
                    while (_dtmStartDate <= dtToDate.Value.Date)
                    {
                        DataRow[] arrDr = mDtReport.Select("YYYYMMDD=" +Utility.GetYYYYMMDD( _dtmStartDate));
                        if (arrDr.Length > 0)
                        {
                            dtTemp.ImportRow(arrDr[0]);
                        }
                        else
                        {
                            DataRow newDr = dtTemp.NewRow();
                            Utility.CopyData(rowdata, ref newDr);
                            newDr["YYYYMMDD"] = Utility.GetYYYYMMDD(_dtmStartDate);
                            newDr[TBiendongThuoc.Columns.NgayBiendong] =_dtmStartDate.ToString("dd/MM/yyyy");
                            newDr["Xuat"] = 0;
                            newDr["Tondau"] = 0;
                            newDr["Toncuoi"] = 0;
                            newDr["Nhap"] = 0;
                            dtTemp.Rows.Add(newDr);
                        }
                        _dtmStartDate = _dtmStartDate.AddDays(1);
                    }
                }
                else
                {
                    dtTemp = mDtReport.Copy();
                }
                _dtmStartDate = dtFromDate.Value.Date;
                while (_dtmStartDate <= dtToDate.Value.Date)
                {
                    DataRow[] arrDr = dtTemp.Select("YYYYMMDD=" + Utility.GetYYYYMMDD(_dtmStartDate));
                    if (arrDr.Length > 0)
                    {
                        iFound++;
                        if (iFound == 1) //Tính tồn cuối dòng đầu tiên tìm thấy
                        {
                            arrDr[0]["Toncuoi"] = Utility.DecimaltoDbnull(arrDr[0]["Tondau"], 0) +
                                                  Utility.DecimaltoDbnull(arrDr[0]["Nhap"], 0)
                                                  - Utility.DecimaltoDbnull(arrDr[0]["Xuat"], 0);
                        }
                        else
                        {
                            OrderedEnumerableRowCollection<DataRow> q = from p in dtTemp.AsEnumerable()
                                                                        where Utility.Int32Dbnull(p["YYYYMMDD"], 0) < Utility.Int32Dbnull(Utility.GetYYYYMMDD(_dtmStartDate))
                                                                        orderby p["YYYYMMDD"] descending
                                                                        select p;
                            if (q.Count() > 0)
                            {
                                DataRow drPrevious = q.FirstOrDefault();
                                arrDr[0]["Tondau"] = Utility.DecimaltoDbnull(drPrevious["Toncuoi"], 0);
                                arrDr[0]["Toncuoi"] = Utility.DecimaltoDbnull(arrDr[0]["Tondau"], 0) +
                                                      Utility.DecimaltoDbnull(arrDr[0]["Nhap"], 0)
                                                      - Utility.DecimaltoDbnull(arrDr[0]["Xuat"], 0);
                            }
                        }
                        dtTemp.AcceptChanges();
                    }
                    else
                    {
                    }
                    _dtmStartDate = _dtmStartDate.AddDays(1);
                }
            }
            catch (Exception ex)
            {
                //Utility.CatchException(ex);
            }
            finally{
                mDtReport = dtTemp.Copy();
            }
        }

        private void ProcessDataThethuoc_Thang(ref DataTable mDtReport)
        {

            DataTable dtTemp = mDtReport.Clone();
            try
            {
                var lstAdded = new List<DataRow>();
                int startDate = Utility.Int32Dbnull(Utility.GetYYYYMM(dtFromDate.Value), 0);
                int endDate = Utility.Int32Dbnull(Utility.GetYYYYMMDD(dtToDate.Value), 0);
                if (mDtReport.Rows.Count == 1 && Utility.sDbnull(mDtReport.Rows[0]["YYYYMM"], "") == "")
                {
                    mDtReport.Rows[0]["YYYYMM"] = startDate.ToString();
                    mDtReport.Rows[0]["thang"] = startDate.ToString().Substring(4,2);
                    mDtReport.Rows[0]["nam"] = startDate.ToString().Substring(0, 4);
                 //   mDtReport.Rows[0][TBiendongThuoc.Columns.NgayBiendong] = dtFromDate.Value.Date.ToString("dd/MM/yyyy");
                }
                int iFound = 0;
                DataRow rowdata = mDtReport.Rows[0];
                DateTime dtmStartDate = dtFromDate.Value.Date;// Utility.FromYYYYMMDD2Datetime(startDate.ToString());

                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_THETHUOC_TUDONGTHEMDULIEU_TUONGLAI", "0", false) == "1")
                {
                    while (dtmStartDate <= dtToDate.Value.Date)
                    {
                        DataRow[] arrDr = mDtReport.Select("YYYYMM=" + Utility.GetYYYYMM(dtmStartDate));
                        if (arrDr.Length > 0)
                        {
                            dtTemp.ImportRow(arrDr[0]);
                        }
                        else
                        {
                                DataRow newDr = dtTemp.NewRow();
                                Utility.CopyData(rowdata, ref newDr);
                                newDr["YYYYMM"] = Utility.GetYYYYMM(dtmStartDate);
                                // newDr[TBiendongThuoc.Columns.NgayBiendong] = dtmStartDate.ToString("dd/MM/yyyy");
                                newDr["Xuat"] = 0;
                                newDr["Tondau"] = mDtReport.Rows[0]["Tondau"];
                                newDr["Toncuoi"] = 0;
                                newDr["Nhap"] = 0;
                                newDr["nam"] = Utility.GetYYYYMM(dtmStartDate).Substring(0, 4);
                                newDr["thang"] = Utility.GetYYYYMM(dtmStartDate).Substring(4, 2);
                                dtTemp.Rows.Add(newDr);
                        }
                        dtmStartDate = dtmStartDate.AddMonths(1);
                    }
                }
                else
                {
                    dtTemp = mDtReport.Copy();
                }
                dtmStartDate = dtFromDate.Value.Date;
                while (dtmStartDate <= dtToDate.Value.Date)
                {
                    DataRow[] arrDr = dtTemp.Select("YYYYMM=" + Utility.GetYYYYMM(dtmStartDate));
                    if (arrDr.Length > 0)
                    {
                        iFound++;
                        if (iFound == 1) //Tính tồn cuối dòng đầu tiên tìm thấy
                        {
                            arrDr[0]["Xuat"] = Utility.DecimaltoDbnull(arrDr[0]["Xuat"], 0);
                            arrDr[0]["Toncuoi"] = Utility.DecimaltoDbnull(arrDr[0]["Tondau"], 0) +
                                                  Utility.DecimaltoDbnull(arrDr[0]["Nhap"], 0)
                                                  - Utility.DecimaltoDbnull(arrDr[0]["Xuat"], 0);
                        }
                        else
                        {
                            DateTime date = dtmStartDate;
                            OrderedEnumerableRowCollection<DataRow> q = from p in dtTemp.AsEnumerable()
                                where
                                    Utility.Int32Dbnull(p["YYYYMM"], 0) <
                                    Utility.Int32Dbnull(Utility.GetYYYYMM(date))
                                orderby p["YYYYMM"] descending
                                select p;
                            if (q.Any())
                            {
                                DataRow drPrevious = q.FirstOrDefault();
                                if (drPrevious != null)
                                    arrDr[0]["Tondau"] = Utility.DecimaltoDbnull(drPrevious["Toncuoi"], 0);
                                arrDr[0]["Toncuoi"] = Utility.DecimaltoDbnull(arrDr[0]["Tondau"], 0) +
                                                      Utility.DecimaltoDbnull(arrDr[0]["Nhap"], 0)
                                                      - Utility.DecimaltoDbnull(arrDr[0]["Xuat"], 0);
                            }
                        }
                        dtTemp.AcceptChanges();
                    }
                    dtmStartDate = dtmStartDate.AddMonths(1);
                }
            }
            catch (Exception ex)
            {
                //Utility.CatchException(ex);
            }
            finally
            {
                mDtReport = dtTemp.Copy();
            }
        }
        private void frm_thethuoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.F4) cmdBaoCao.PerformClick();
            if (e.KeyCode == Keys.F5) cmdExportToExcel.PerformClick();
        }

        private void cmdExportToExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (radThekhochitiet.Checked)
                {
                    if (grdListChitiet.RowCount <= 0)
                    {
                        Utility.ShowMsg("Không có dữ liệu chi tiết để xuất file excel", "Thông báo");
                        return;
                    }
                    gridEXExporter1.GridEX = grdListChitiet;
                }
                else
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
                    else if (grdListKhole.RowCount <= 0)
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

        private void radThekhoThang_CheckedChanged(object sender, EventArgs e)
        {
            radTheXuatNhapTon.Checked = radThekhochitiet.Checked = !radThekhoThang.Checked;
        }

        private void radSimple_CheckedChanged(object sender, EventArgs e)
        {
            ModifyTieude();
        }

        private void optThang_CheckedChanged(object sender, EventArgs e)
        {
            if (optThang.Checked)
            {
                cboThang.SelectedIndex = 0;
                var myDate = cboThang.SelectedValue;
                var startOfMonth = new DateTime(dtpNam.Value.Year, Utility.Int32Dbnull(myDate), 1);
                dtFromDate.Value = startOfMonth;
                var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);
                dtToDate.Value = endOfMonth;
            }
        }

        private void optQuy_CheckedChanged(object sender, EventArgs e)
        {
            if (optQuy.Checked)
            {
                var fromdate = new DateTime();
                var todate = new DateTime();
                switch (Utility.sDbnull(cboQuy.SelectedValue))
                {
                    case "1":
                        fromdate = new DateTime(dtpNam.Value.Year, 1, 1);
                        todate = new DateTime(dtpNam.Value.Year, 3, 31);
                        break;
                    case "2":
                        fromdate = new DateTime(dtpNam.Value.Year, 4, 1);
                        todate = new DateTime(dtpNam.Value.Year, 6, 30);
                        break;
                    case "3":
                        fromdate = new DateTime(dtpNam.Value.Year, 7, 1);
                        todate = new DateTime(dtpNam.Value.Year, 9, 30);
                        break;
                    case "4":
                        fromdate = new DateTime(dtpNam.Value.Year, 10, 1);
                        todate = new DateTime(dtpNam.Value.Year, 12, 31);
                        break;
                    default:
                        fromdate = new DateTime(dtpNam.Value.Year, 1, 1);
                        todate = new DateTime(dtpNam.Value.Year, 12, 31);
                        break;
                }
                dtFromDate.Value = fromdate;
                dtToDate.Value = todate;
            }
        }

        private void optNam_CheckedChanged(object sender, EventArgs e)
        {
            if (optNam.Checked)
            {
                var myDate = dtpNam.Value;
                var startOfMonth = new DateTime(dtpNam.Value.Year, 1, 1);
                dtFromDate.Value = startOfMonth;
                var endOfMonth = new DateTime(dtpNam.Value.Year, 12, 31);
                dtToDate.Value = endOfMonth;
            }
        }

        private void cboThang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (optThang.Checked)
            {
                var myDate = cboThang.SelectedValue;
                var startOfMonth = new DateTime(dtpNam.Value.Year, Utility.Int32Dbnull(myDate), 1);
                dtFromDate.Value = startOfMonth;
                var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);
                dtToDate.Value = endOfMonth;
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
                        todate = new DateTime(dtpNam.Value.Year, 3, 31);
                        break;
                    case "2":
                        fromdate = new DateTime(dtpNam.Value.Year, 4, 1);
                        todate = new DateTime(dtpNam.Value.Year, 6, 30);
                        break;
                    case "3":
                        fromdate = new DateTime(dtpNam.Value.Year, 7, 1);
                        todate = new DateTime(dtpNam.Value.Year, 9, 30);
                        break;
                    case "4":
                        fromdate = new DateTime(dtpNam.Value.Year, 10, 1);
                        todate = new DateTime(dtpNam.Value.Year, 12, 31);
                        break;
                    default:
                        fromdate = new DateTime(dtpNam.Value.Year, 1, 1);
                        todate = new DateTime(dtpNam.Value.Year, 12, 31);
                        break;
                }
                dtFromDate.Value = fromdate;
                dtToDate.Value = todate;
            }
        }

        private void txtkho__OnSelectionChanged()
        {
            if (!_allowChanged) return;
            SelectStock();
            ModifyTieude();
        }

        private void txtkho__OnEnterMe()
        {
            if (!_allowChanged) return;
            SelectStock();
            ModifyTieude();
        }

        private void txtkho_TextChanged(object sender, EventArgs e)
        {
            if (!_allowChanged) return;
            SelectStock();
            ModifyTieude();
        }

        private void cmdComboDown_Click(object sender, EventArgs e)
        {
            txtkho.ShowMe();
        }

        private void uiButton1_Click(object sender, EventArgs e)
        {
            txtthuoc.ShowMe();
        }
    }
}