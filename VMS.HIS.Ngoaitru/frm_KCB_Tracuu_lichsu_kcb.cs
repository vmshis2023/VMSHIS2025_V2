using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using NLog;
using SubSonic;
using VNS.HIS.BusRule.Classes;
using VMS.HIS.DAL;
using VNS.HIS.UCs;
using VNS.HIS.UI.Classess;
using VNS.HIS.UI.Forms.Cauhinh;
using VNS.Libs;
using VNS.Properties;

namespace VNS.HIS.UI.Forms.NGOAITRU
{
    public partial class FrmKcbTracuuLichsuKcb : Form
    {
        private readonly KCB_THAMKHAM _kcbThamkham = new KCB_THAMKHAM();
        private readonly DataTable _dtIcdPhu = new DataTable();

        private readonly List<string> _lstResultColumns = new List<string>
        {
            "ten_chitietdichvu",
            "ketqua_cls",
            "binhthuong_nam",
            "binhthuong_nu"
        };
        private bool _buttonClick;
        private KcbChandoanKetluan _kcbChandoanKetluan;
        private DataTable _mDtDanhsachbenhnhanthamkham = new DataTable();
        private DataSet _ds = new DataSet();
        private bool _isLike = true;
        private List<string> _lstVisibleColumns = new List<string>();
        private DataTable _mDtAssignDetail;
        private DataTable _mDtDoctorAssign;
        private DataTable _mDtDonthuocChitietView = new DataTable();
        private DataTable _mDtPresDetail = new DataTable();
        private string _malankham = "";
        public KcbDanhsachBenhnhan ObjBenhnhan = null;
        public KcbLuotkham ObjLuotkham = null;
        private KcbDangkyKcb _objkcbdangky;
        private string _thamso = "ALL";
        public FrmKcbTracuuLichsuKcb(string args)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            KeyPreview = true;
            LogManager.GetCurrentClassLogger();
            dtInput_Date.Value =
                dtpCreatedDate.Value = dtNgayNhapVien.Value = globalVariables.SysDate;
            dtFromDate.Value = globalVariables.SysDate;
            dtToDate.Value = globalVariables.SysDate;
            txtIdKhoaNoiTru.Visible = txtPatientDept_ID.Visible = globalVariables.IsAdmin;
            InitEvents();
            _thamso = args;
        }

        public bool AllowTextChanged { get; set; }

        private void InitEvents()
        {
            Load += frm_KCB_Tracuu_lichsu_kcb_Load;
            KeyDown += frm_KCB_Tracuu_lichsu_kcb_KeyDown;
            cmdSearch.Click += cmdSearch_Click;
        //    txtmaluotkham.KeyDown += txtPatient_Code_KeyDown;
            txtMaBenhChinh.TextChanged += txtMaBenhChinh_TextChanged;
            txtMaBenhphu.TextChanged += txtMaBenhphu_TextChanged;
            grdList.DoubleClick += grdList_DoubleClick;
            grdLuotkham.DoubleClick += grdLuotkham_DoubleClick;
            //grdList.SelectionChanged += grdList_SelectionChanged;
            //grdRegExam.SelectionChanged += grdRegExam_SelectionChanged;
            //grdLuotkham.SelectionChanged += grdLuotkham_SelectionChanged;
        }

        void grdLuotkham_DoubleClick(object sender, EventArgs e)
        {
            loadLuotkham();
        }

        void grdList_DoubleClick(object sender, EventArgs e)
        {
            LoadLSCKB();
        }
        void loadLuotkham()
        {
            try
            {
                if (PropertyLib._ThamKhamProperties.ViewAfterClick && !_buttonClick)
                {
                    if (!Utility.isValidGrid(grdLuotkham))
                    {
                        ClearControl();
                    }
                    string maluotkham = Utility.sDbnull(grdLuotkham.GetValue(KcbLuotkham.Columns.MaLuotkham), "");
                    DataTable dtData = _kcbThamkham.KcbLichsuKcbTimkiemphongkham(ObjLuotkham.IdBenhnhan, maluotkham,0);
                    Utility.SetDataSourceForDataGridEx_Basic(grdRegExam, dtData, true, true, "", "");
                    grbRegs.Height = grdRegExam.GetDataRows().Length <= 1 ? 0 : 50;
                    grdRegExam.MoveFirst();
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        private void grdLuotkham_SelectionChanged(object sender, EventArgs e)
        {
            loadLuotkham();
        }

        private void grdRegExam_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdRegExam))
                {
                    ClearControl();
                    return;
                }
                HienthithongtinBn();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        /// <summary>
        ///     hàm thực hiện việc tìm kiếm thông tin của thăm khám
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            SearchPatient();
        }

        /// <summary>
        ///     Hàm thực hiện load lên Khoa nội trú
        /// </summary>
        private void InitData()
        {
            
                THU_VIEN_CHUNG.Laydanhmuckhoa("NOI", 0);
        }

        private void UpdateGroup()
        {
                if (!Utility.isValidGrid(grdLuotkham)) return;
                var counts = ((DataView) grdLuotkham.DataSource).Table.AsEnumerable()
                    .GroupBy(x => x.Field<string>("ma_doituong_kcb"))
                    .Select(g => new {g.Key, Count = g.Count()});
                if (counts.Count() >= 2)
                {
                    if (grdLuotkham.RootTable.Groups.Count <= 0)
                    {
                        GridEXColumn gridExColumn = grdList.RootTable.Columns["ma_doituong_kcb"];
                        var gridExGroup = new GridEXGroup(gridExColumn) {GroupPrefix = "Nhóm đối tượng KCB: "};
                        grdLuotkham.RootTable.Groups.Add(gridExGroup);
                    }
                }
                else
                {
                    GridEXColumn gridExColumn = grdLuotkham.RootTable.Columns["ma_doituong_kcb"];
                    var gridExGroup = new GridEXGroup(gridExColumn);
                    grdLuotkham.RootTable.Groups.Clear();
                }
                grdLuotkham.UpdateData();
                grdLuotkham.Refresh();
        }

        private void SearchPatient()
        {
            try
            {
                ClearControl();
                _malankham = "";
                _objkcbdangky = null;
                ObjBenhnhan = null;
                ObjLuotkham = null;
                if (_dtIcdPhu != null) _dtIcdPhu.Clear();
                if (_mDtAssignDetail != null) _mDtAssignDetail.Clear();
                if (_mDtPresDetail != null) _mDtPresDetail.Clear();
                _mDtDanhsachbenhnhanthamkham =
                    _kcbThamkham.KcbLichsuKcbTimkiemBenhnhan(dtFromDate .Value,dtToDate .Value ,
                        Utility.DoTrim(txtmaluotkham.Text),
                        Utility.Int32Dbnull(txtIdBenhnhan.Text, -1),
                        Utility.DoTrim(txtTenBN.Text), Utility.DoTrim(txtTheBHYT.Text),
                        Utility.Int32Dbnull(txtBacsikham.MyID, -1),_thamso);
                Utility.SetDataSourceForDataGridEx_Basic(grdList, _mDtDanhsachbenhnhanthamkham, true, true, "",
                    KcbDanhsachBenhnhan.Columns.TenBenhnhan);
                if (_dtIcdPhu != null && !_dtIcdPhu.Columns.Contains(DmucBenh.Columns.MaBenh))
                {
                    _dtIcdPhu.Columns.Add(DmucBenh.Columns.MaBenh, typeof (string));
                }
                if (_dtIcdPhu != null && !_dtIcdPhu.Columns.Contains(DmucBenh.Columns.TenBenh))
                {
                    _dtIcdPhu.Columns.Add(DmucBenh.Columns.TenBenh, typeof (string));
                }
                grd_ICD.DataSource = _dtIcdPhu;

                grdList.MoveFirst();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void BindDoctorAssignInfo()
        {
            try
            {
                _mDtDoctorAssign = THU_VIEN_CHUNG.LaydanhsachBacsi(-1, -1);
                DataBinding.BindDataCombox(cboDoctorAssign, _mDtDoctorAssign, DmucNhanvien.Columns.IdNhanvien,
                    DmucNhanvien.Columns.TenNhanvien, "---Bác sỹ chỉ định---", true);
                if (globalVariables.gv_intIDNhanvien <= 0)
                {
                    if (cboDoctorAssign.Items.Count > 0)
                        cboDoctorAssign.SelectedIndex = 0;
                }
                else
                {
                    if (cboDoctorAssign.Items.Count > 0)
                        cboDoctorAssign.SelectedIndex = Utility.GetSelectedIndex(cboDoctorAssign,
                            globalVariables.gv_intIDNhanvien.ToString(CultureInfo.InvariantCulture));
                }
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:"+ exception.Message);
            }
        }


        private void GetDataChiDinh()
        {
            _ds =
                _kcbThamkham.LaythongtinCLSVaThuoc(Utility.Int32Dbnull(txtPatient_ID.Text, -1),
                    Utility.sDbnull(_malankham, ""),
                    Utility.Int32Dbnull(txtExam_ID.Text));
            _mDtAssignDetail = _ds.Tables[0];
            _mDtPresDetail = _ds.Tables[1];

            Utility.SetDataSourceForDataGridEx(grdAssignDetail, _mDtAssignDetail, false, true, "",
                "stt_hthi_dichvu,stt_hthi_chitiet,ten_chitietdichvu");

            _mDtDonthuocChitietView = _mDtPresDetail.Clone();
            foreach (DataRow dr in _mDtPresDetail.Rows)
            {
                dr["CHON"] = 0;
                DataRow[] drview
                    = _mDtDonthuocChitietView
                        .Select(KcbDonthuocChitiet.Columns.IdThuoc + "=" +
                                Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.IdThuoc], "-1")
                                + "AND " + KcbDonthuocChitiet.Columns.DonGia + "=" +
                                Utility.sDbnull(dr[KcbDonthuocChitiet.Columns.DonGia], "-1"));
                if (drview.Length <= 0)
                {
                    _mDtDonthuocChitietView.ImportRow(dr);
                }
                else
                {
                    drview[0][KcbDonthuocChitiet.Columns.SoLuong] =
                        Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong], 0) +
                        Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.SoLuong], 0);
                    drview[0]["TT_KHONG_PHUTHU"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong])*
                                                   Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.DonGia]);
                    drview[0]["TT"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong])*
                                      (Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.DonGia]) +
                                       Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu]));
                    drview[0]["TT_BHYT"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong])*
                                           Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BhytChitra]);
                    drview[0]["TT_BN"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong])*
                                         (Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0) +
                                          Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu], 0));
                    drview[0]["TT_PHUTHU"] = Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong])*
                                             Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.PhuThu], 0);
                    drview[0]["TT_BN_KHONG_PHUTHU"] =
                        Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SoLuong])*
                        Utility.DecimaltoDbnull(drview[0][KcbDonthuocChitiet.Columns.BnhanChitra], 0);

                    drview[0][KcbDonthuocChitiet.Columns.SttIn] =
                        Math.Min(Utility.Int32Dbnull(dr[KcbDonthuocChitiet.Columns.SttIn], 0),
                            Utility.Int32Dbnull(drview[0][KcbDonthuocChitiet.Columns.SttIn], 0));
                    _mDtDonthuocChitietView.AcceptChanges();
                }
            }

            Utility.SetDataSourceForDataGridEx(grdPresDetail, _mDtDonthuocChitietView, false, true, "",
                KcbDonthuocChitiet.Columns.SttIn);
        }

        private void frm_KCB_Tracuu_lichsu_kcb_Load(object sender, EventArgs e)
        {
            try
            {
                AllowTextChanged = false;
                _lstVisibleColumns = Utility.GetVisibleColumns(grdAssignDetail);
                Load_DSach_ICD();
                txtBacsikham.Init(THU_VIEN_CHUNG.LaydanhsachBacsi(-1, -1));
                BindDoctorAssignInfo();
               // SearchPatient();

                AllowTextChanged = true;
                CauHinhThamKham();
                InitData();
                ClearControl();
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi:" + exception.Message);
            }
            finally
            {
                txtPatient_Code.Focus();
                txtPatient_Code.Select();
            }
        }

        private void Load_DSach_ICD()
        {
            try
            {
                _kcbThamkham.LaydanhsachBenh();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi: "+ ex.Message);
            }
        }

        private void txtMaBenhChinh_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataRow[] arrDr;
                if (_isLike)
                    arrDr =
                        globalVariables.gv_dtDmucBenh.Select(DmucBenh.Columns.MaBenh + " like '" +
                                                             Utility.sDbnull(txtMaBenhChinh.Text, "") +
                                                             "%'");
                else
                    arrDr =
                        globalVariables.gv_dtDmucBenh.Select(DmucBenh.Columns.MaBenh + " = '" +
                                                             Utility.sDbnull(txtMaBenhChinh.Text, "") +
                                                             "'");
                if (!string.IsNullOrEmpty(txtMaBenhChinh.Text))
                {
                    if (arrDr.GetLength(0) == 1)
                    {
                        txtMaBenhChinh.Text = arrDr[0][DmucBenh.Columns.MaBenh].ToString();
                        txtTenBenhChinh.Text = Utility.sDbnull(arrDr[0][DmucBenh.Columns.TenBenh], "");
                        //  txtDisease_ID.Text = Utility.sDbnull(arrDr[0][DmucBenh.Columns.DiseaseId], "-1");
                    }
                    else
                    {
                        //txtDisease_ID.Text = "-1";
                        txtTenBenhChinh.Text = "";
                    }
                }
                else
                {
                    //  txtDisease_ID.Text = "-1";

                    txtTenBenhChinh.Text = "";
                    //cmdSearchBenhChinh.PerformClick();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi: " + ex.Message);
            }
        }

        /// <summary>
        ///     hàm thực hiện việc mã bệnh phụ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMaBenhphu_TextChanged(object sender, EventArgs e)
        {
            DataRow[] arrDr;
            if (_isLike)
                arrDr =
                    globalVariables.gv_dtDmucBenh.Select(DmucBenh.Columns.MaBenh + " like '" +
                                                         Utility.sDbnull(txtMaBenhphu.Text, "") +
                                                         "%'");
            else
                arrDr =
                    globalVariables.gv_dtDmucBenh.Select(DmucBenh.Columns.MaBenh + " = '" +
                                                         Utility.sDbnull(txtMaBenhphu.Text, "") +
                                                         "'");
            if (!string.IsNullOrEmpty(txtMaBenhphu.Text))
            {
                if (arrDr.GetLength(0) == 1)
                {
                    txtMaBenhphu.Text = arrDr[0][DmucBenh.Columns.MaBenh].ToString();
                    txtTenBenhPhu.Text = Utility.sDbnull(arrDr[0][DmucBenh.Columns.TenBenh], "");
                    //  txtDisease_ID.Text = Utility.sDbnull(arrDr[0][DmucBenh.Columns.DiseaseId], "-1");
                }
                else
                {
                    //txtDisease_ID.Text = "-1";
                    txtTenBenhPhu.Text = "";
                }
            }
            else
            {
                //  txtDisease_ID.Text = "-1";

                txtMaBenhphu.Text = "";
                //cmdSearchBenhChinh.PerformClick();
            }
        }


        private void ClearControl()
        {
                txtReg_ID.Text = "";
                txtPatientDept_ID.Clear();
                txtIdKhoaNoiTru.Clear();
                txtKhoaNoiTru.Clear();

                foreach (Control control in pnlThongtinBNKCB.Controls)
                {
                    if (control is EditBox)
                    {
                        ((EditBox) (control)).Clear();
                    }
                    else if (control is MaskedEditBox)
                    {
                        control.Text = "";
                    }
                    else if (control is AutoCompleteTextbox)
                    {
                        ((AutoCompleteTextbox) control)._Text = "";
                    }
                    else if (control is TextBox)
                    {
                        ((TextBox) (control)).Clear();
                    }
                }

                foreach (Control control in pnlKetluan.Controls)
                {
                    if (control is EditBox)
                    {
                        ((EditBox) (control)).Clear();
                    }
                    else if (control is MaskedEditBox)
                    {
                        control.Text = "";
                    }
                    else if (control is AutoCompleteTextbox)
                    {
                        ((AutoCompleteTextbox) control)._Text = "";
                    }
                    else if (control is TextBox)
                    {
                        ((TextBox) (control)).Clear();
                    }
                }

                foreach (Control control in pnlother.Controls)
                {
                    if (control is EditBox)
                    {
                        ((EditBox) (control)).Clear();
                    }
                    else if (control is MaskedEditBox)
                    {
                        control.Text = "";
                    }
                    else if (control is AutoCompleteTextbox)
                    {
                        ((AutoCompleteTextbox) control)._Text = "";
                    }
                    else if (control is TextBox)
                    {
                        ((TextBox) (control)).Clear();
                    }
                }
                nmrSongayDT.Value = 0;
        }

        /// <summary>
        ///     Lấy về thông tin bệnh nhâ nội trú
        /// </summary>
        private void GetData()
        {
            try
            {
                // Utility.SetMsg(lblMsg, "", false);
                string patientCode = Utility.sDbnull(grdLuotkham.GetValue(KcbLuotkham.Columns.MaLuotkham), "");
                _malankham = patientCode;
                int patientId = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan), -1);
                ObjLuotkham = new Select().From(KcbLuotkham.Schema)
                    .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(patientCode)
                    .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(patientId).ExecuteSingle<KcbLuotkham>();

                ObjBenhnhan = KcbDanhsachBenhnhan.FetchByID(ObjLuotkham.IdBenhnhan);

                if (ObjLuotkham != null)
                {
                    ClearControl();
                    txt_idchidinhphongkham.Text = Utility.sDbnull(grdRegExam.GetValue(KcbDangkyKcb.Columns.IdKham));

                    _objkcbdangky = KcbDangkyKcb.FetchByID(Utility.Int32Dbnull(txt_idchidinhphongkham.Text));
                    if (_objkcbdangky != null)
                    {
                        DataTable mDtThongTin = _kcbThamkham.LayThongtinBenhnhanKCB(ObjLuotkham.MaLuotkham,
                            Utility.Int32Dbnull(ObjLuotkham.IdBenhnhan,
                                -1),
                            Utility.Int32Dbnull(txt_idchidinhphongkham.Text));
                        if (mDtThongTin.Rows.Count > 0)
                        {
                            DataRow dr = mDtThongTin.Rows[0];
                            if (dr != null)
                            {
                                dtInput_Date.Value = Convert.ToDateTime(dr[KcbLuotkham.Columns.NgayTiepdon]);
                                txtExam_ID.Text = Utility.sDbnull(grdRegExam.GetValue(KcbDangkyKcb.Columns.IdKham));

                                txtKhoaDieuTri.Text = Utility.sDbnull(grdLuotkham.GetValue("ten_khoanoitru"));
                                txtBuong.Text = Utility.sDbnull(grdLuotkham.GetValue("ten_buong"));
                                txtGiuong.Text = Utility.sDbnull(grdLuotkham.GetValue("ten_giuong"));

                                txtTrangthaiNgoaitru.Text =
                                    Utility.sDbnull(grdLuotkham.GetValue("trangthai_ngoaitru")) == "0"
                                        ? "Đang khám"
                                        : "Đã khám xong";
                                txtTrangthaiNoitru.Text = Utility.sDbnull(grdLuotkham.GetValue("ten_trangthai_noitru"));

                                Utility.Int32Dbnull(txtExam_ID.Text, -1);
                                txtGioitinh.Text =
                                    Utility.sDbnull(grdList.GetValue(KcbDanhsachBenhnhan.Columns.GioiTinh), "");
                                txt_idchidinhphongkham.Text =
                                    Utility.sDbnull(grdRegExam.GetValue(KcbDangkyKcb.Columns.IdKham));
                                lblSOkham.Text = Utility.sDbnull(grdRegExam.GetValue(KcbDangkyKcb.Columns.SttKham));
                                txtPatient_Name.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.TenBenhnhan], "");
                                txtPatient_ID.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.IdBenhnhan], "");
                                txtPatient_Code.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.MaLuotkham], "");
                                
                                txtDiaChi.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.DiaChi], "");
                                txtDiachiBHYT.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.DiachiBhyt], "");

                                txtObjectType_Name.Text = Utility.sDbnull(dr[DmucDoituongkcb.Columns.TenDoituongKcb], "");
                                txtSoBHYT.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.MatheBhyt], "");
                                txtBHTT.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.PtramBhyt], "0");
                                txtNgheNghiep.Text = Utility.sDbnull(dr[KcbDanhsachBenhnhan.Columns.NgheNghiep], "");
                                txtHanTheBHYT.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.NgayketthucBhyt], "");
                                dtpNgayhethanBHYT.Text = Utility.sDbnull(dr[KcbLuotkham.Columns.NgayketthucBhyt],
                                    globalVariables.SysDate.ToString("dd/MM/yyyy"));
                                var sqlbenhan =
                                    new Select().From(KcbBenhAn.Schema)
                                        .Where(KcbBenhAn.Columns.IdBnhan)
                                        .IsEqualTo(ObjLuotkham.IdBenhnhan)
                                        .ExecuteSingle<KcbBenhAn>();
                                txtSoBa.Text = sqlbenhan != null ? string.Format("{0}-{1}", sqlbenhan.LoaiBa, sqlbenhan.SoBenhAn) : "";
                                if (ObjBenhnhan.NgaySinh != null)
                                    txtTuoi.Text = Utility.sDbnull(Utility.Int32Dbnull(globalVariables.SysDate.Year) -
                                                                   ObjBenhnhan.NgaySinh.Value.Year);
                                //ThongBaoBenhAn(txtPatient_ID.Text);

                                if (_objkcbdangky != null)
                                {
                                    txtReg_ID.Text = Utility.sDbnull(_objkcbdangky.IdKham);
                                    dtpCreatedDate.Value = Convert.ToDateTime(_objkcbdangky.NgayDangky);
                                    txtDepartment_ID.Text = Utility.sDbnull(_objkcbdangky.IdPhongkham);
                                     var department = (from p in globalVariables.gv_dtDmucPhongban.AsEnumerable()
                                        where p[DmucKhoaphong.Columns.IdKhoaphong].Equals(_objkcbdangky.IdPhongkham)
                                        select p).FirstOrDefault();
                                    if (department != null)
                                    {
                                        txtPhongkham.Text = Utility.sDbnull(department["ten_khoaphong"], "");
                                    }
                                    txtTenDvuKham.Text = Utility.sDbnull(_objkcbdangky.TenDichvuKcb);
                                    txtNguoiTiepNhan.Text = Utility.sDbnull(_objkcbdangky.NguoiTao);
                                    try
                                    {
                                        cboDoctorAssign.SelectedIndex =
                                            Utility.GetSelectedIndex(cboDoctorAssign,
                                                Utility.sDbnull(
                                                    _objkcbdangky.IdBacsikham, -1));
                                    }
                                    catch (Exception exception)
                                    {
                                        if (globalVariables.IsAdmin)
                                            Utility.ShowMsg(exception.ToString());
                                    }
                                }
                                _kcbChandoanKetluan = new Select().From(KcbChandoanKetluan.Schema)
                                    .Where(KcbChandoanKetluan.Columns.IdKham)
                                    .IsEqualTo(_objkcbdangky.IdKham)
                                    .ExecuteSingle
                                    <KcbChandoanKetluan>();
                                if (_kcbChandoanKetluan != null)
                                {
                                    txtKet_Luan._Text = Utility.sDbnull(_kcbChandoanKetluan.Ketluan);
                                    txtHuongdieutri._Text = _kcbChandoanKetluan.HuongDieutri;
                                    nmrSongayDT.Value = Utility.DecimaltoDbnull(_kcbChandoanKetluan.SongayDieutri, 0);
                                    txtHa.Text = Utility.sDbnull(_kcbChandoanKetluan.Huyetap);
                                    txtMach.Text = Utility.sDbnull(_kcbChandoanKetluan.Mach);
                                    txtNhipTim.Text = Utility.sDbnull(_kcbChandoanKetluan.Nhiptim);
                                    txtNhipTho.Text = Utility.sDbnull(_kcbChandoanKetluan.Nhiptho);
                                    txtNhietDo.Text = Utility.sDbnull(_kcbChandoanKetluan.Nhietdo);
                                    txtCannang.Text = Utility.sDbnull(_kcbChandoanKetluan.Cannang);
                                    txtSoNgayHen.Text = Utility.sDbnull(_kcbChandoanKetluan.SoNgayhen);
                                    txtChieucao.Text = Utility.sDbnull(_kcbChandoanKetluan.Chieucao);
                                    if (!string.IsNullOrEmpty(Utility.sDbnull(_kcbChandoanKetluan.Nhommau)) &&
                                        Utility.sDbnull(_kcbChandoanKetluan.Nhommau) != "-1")
                                        txtNhommau._Text = Utility.sDbnull(_kcbChandoanKetluan.Nhommau);


                                    AllowTextChanged = true;
                                    _isLike = false;
                                    txtChanDoan._Text = Utility.sDbnull(_kcbChandoanKetluan.Chandoan);
                                    txtChanDoanKemTheo.Text = Utility.sDbnull(_kcbChandoanKetluan.ChandoanKemtheo);
                                    txtMaBenhChinh.Text = Utility.sDbnull(_kcbChandoanKetluan.MabenhChinh);
                                    string dataString = Utility.sDbnull(_kcbChandoanKetluan.MabenhPhu, "");
                                    _isLike = true;
                                    AllowTextChanged = false;
                                    _dtIcdPhu.Clear();
                                    if (!string.IsNullOrEmpty(dataString))
                                    {
                                        string[] rows = dataString.Split(',');
                                        foreach (string row in rows)
                                        {
                                            if (!string.IsNullOrEmpty(row))
                                            {
                                                DataRow newDr = _dtIcdPhu.NewRow();
                                                newDr[DmucBenh.Columns.MaBenh] = row;
                                                newDr[DmucBenh.Columns.TenBenh] = GetTenBenh(row);
                                                _dtIcdPhu.Rows.Add(newDr);
                                                _dtIcdPhu.AcceptChanges();
                                            }
                                        }
                                        grd_ICD.DataSource = _dtIcdPhu;
                                    }
                                }

                                GetDataChiDinh();
                            }
                        }
                    }
                    else
                    {
                        ClearControl();
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi: " + ex.Message);
            }
            finally
            {
                KiemTraDaInPhoiBhyt();
            }
        }

        private void KiemTraDaInPhoiBhyt()
        {
            lblMessage.Visible = ObjLuotkham != null && ObjLuotkham.MaDoituongKcb == "BHYT";
            if (ObjLuotkham != null && ObjLuotkham.MaDoituongKcb == "BHYT")
            {
                SqlQuery sqlQuery = new Select().From(KcbPhieuDct.Schema)
                    .Where(KcbPhieuDct.Columns.MaLuotkham).IsEqualTo(Utility.sDbnull(txtPatient_Code.Text))
                    .And(KcbPhieuDct.Columns.IdBenhnhan).IsEqualTo(Utility.Int32Dbnull(txtPatient_ID.Text))
                    .And(KcbPhieuDct.Columns.KieuThanhtoan).IsEqualTo(Utility.Int32Dbnull(KieuThanhToan.NgoaiTru));
                if (sqlQuery.GetRecordCount() > 0)
                {
                    var objPhieuDct = sqlQuery.ExecuteSingle<KcbPhieuDct>();
                    if (objPhieuDct != null)
                    {
                        Utility.SetMsg(lblMessage,
                            string.Format("Đã in phôi bởi {0}, vào lúc: {1}", objPhieuDct.NguoiTao,
                                objPhieuDct.NgayTao), true);
                    }
                }
                else
                {
                    lblMessage.Visible = false;
                }
            } //Đối tượng dịch vụ sẽ luôn hiển thị nút lưu
        }

        private string GetTenBenh(string maBenh)
        {
            string tenBenh = "";
            DataRow[] arrMaBenh =
                globalVariables.gv_dtDmucBenh.Select(string.Format(DmucBenh.Columns.MaBenh + "='{0}'", maBenh));
            if (arrMaBenh.GetLength(0) > 0) tenBenh = Utility.sDbnull(arrMaBenh[0][DmucBenh.Columns.TenBenh], "");
            return tenBenh;
        }


        private void HienthithongtinBn()
        {
            try
            {
                if (!Utility.isValidGrid(grdRegExam))
                {
                    return;
                }
                AllowTextChanged = false;
                if (_dtIcdPhu != null) _dtIcdPhu.Rows.Clear();
                GetData();
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                    Utility.ShowMsg(exception.ToString());
            }
            finally
            {
                AllowTextChanged = true;
            }
        }
        void LoadLSCKB()
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    ClearControl();
                }
                int idbenhnhan = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan), -1);
                DataTable dtData = _kcbThamkham.KcbLichsuKcbLuotkham(idbenhnhan);
                Utility.SetDataSourceForDataGridEx_Basic(grdLuotkham, dtData, true, true, "", "");
                UpdateGroup();
                grdLuotkham.MoveFirst();
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                    Utility.ShowMsg(exception.ToString());
            }
        }
        private void grdList_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            LoadLSCKB();
        }

        private void Unlock()
        {
            try
            {
                if (ObjLuotkham == null)
                    return;
                //Kiểm tra nếu đã in phôi thì cần hủy in phôi
                var item = new Select().From(KcbPhieuDct.Schema)
                    .Where(KcbPhieuDct.Columns.MaLuotkham).IsEqualTo(ObjLuotkham.MaLuotkham)
                    .And(KcbPhieuDct.Columns.IdBenhnhan).IsEqualTo(ObjLuotkham.IdBenhnhan).ExecuteSingle<KcbPhieuDct>();
                if (item != null)
                {
                    Utility.ShowMsg(
                        "Bệnh nhân này thuộc đối tượng BHYT đã được in phôi. Bạn cần liên hệ bộ phận thanh toán hủy in phôi để mở khóa bệnh nhân");
                    return;
                }
                new Update(KcbLuotkham.Schema)
                    .Set(KcbLuotkham.Columns.Locked).EqualTo(0)
                    .Set(KcbLuotkham.Columns.NguoiKetthuc).EqualTo(string.Empty)
                    .Set(KcbLuotkham.Columns.NgayKetthuc).EqualTo(null)
                    .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(
                        ObjLuotkham.MaLuotkham)
                    .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(ObjLuotkham.IdBenhnhan).Execute();
                ObjLuotkham.Locked = 0;
                GetData();
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                    Utility.ShowMsg(exception.ToString());
            }
        }

        /// <summary>
        ///     hàm thực hiện việc dùng phím tắt in phiếu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_KCB_Tracuu_lichsu_kcb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                if ((ActiveControl != null && ActiveControl.Name == grdList.Name) ||
                    (tabPageChanDoan.ActiveControl != null && tabPageChanDoan.ActiveControl.Name == txtMaBenhphu.Name))
                    return;
                else
                    SendKeys.Send("{TAB}");
            if (e.Control && e.KeyCode == Keys.P)
            {
            }

            if (e.Control & e.KeyCode == Keys.F) cmdSearch.PerformClick();
            if (e.KeyCode == Keys.Escape) Close();
            if (e.Control && e.KeyCode == Keys.U)
                Unlock();
            if (e.Control && e.KeyCode == Keys.F5)
            {
                //splitContainer1.Panel1Collapsed = !splitContainer1.Panel1Collapsed;
            }
            if (e.KeyCode == Keys.F11) if (ActiveControl != null) Utility.ShowMsg(ActiveControl.Name);

            if (e.KeyCode == Keys.F1)
            {
                tabDiagInfo.SelectedTab = tabPageChanDoan;
                txtMach.Focus();
            }
            if (e.KeyCode == Keys.F2)
            {
                tabDiagInfo.SelectedTab = tabPageChiDinhCLS;
            }
            if (e.KeyCode == Keys.F6)
            {
                txtPatient_Code.SelectAll();
                txtPatient_Code.Focus();
                return;
            }
            if (e.KeyCode == Keys.F3)
            {
            }
            if (e.KeyCode == Keys.F4)
            {
            }

            if (e.KeyCode == Keys.A && e.Control)
            {
            }
            if (e.Control && e.KeyCode == Keys.N)
            {
            }
        }
        private void CauHinhThamKham()
        {
            if (PropertyLib._ThamKhamProperties != null)
            {
                uiStatusBar1.Visible = !PropertyLib._ThamKhamProperties.HideStatusBar;
                groupBox2.Height = PropertyLib._ThamKhamProperties.ChieucaoluoidanhsachBenhnhanLSKCB <= 0
                                     ? 0
                                     : PropertyLib._ThamKhamProperties.ChieucaoluoidanhsachBenhnhanLSKCB;
            }
        }
        private void grdList_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (PropertyLib._ThamKhamProperties.ViewAfterClick && !_buttonClick)
                {
                    if (!Utility.isValidGrid(grdList))
                    {
                        ClearControl();
                    }
                    int idbenhnhan = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan), -1);
                    DataTable dtData = _kcbThamkham.KcbLichsuKcbLuotkham(idbenhnhan);
                    Utility.SetDataSourceForDataGridEx_Basic(grdLuotkham, dtData, true, true, "","");
                    UpdateGroup();
                    grdLuotkham.MoveFirst();
                }
                
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        private void txtPatient_Code_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    string patientCode = Utility.AutoFullPatientCode(txtmaluotkham.Text);
                    ClearControl();
                    txtmaluotkham.Text = patientCode;
                    DataTable dtBenhNhan= _kcbThamkham.TimkiemThongtinBenhnhansaukhigoMaBN(txtmaluotkham.Text,
                        -1, globalVariables.MA_KHOA_THIEN);
                    grdList.DataSource = null;
                    grdList.DataSource = dtBenhNhan;
                    if (dtBenhNhan.Rows.Count > 0)
                    {
                        AllowTextChanged = false;
                        if (_dtIcdPhu != null) _dtIcdPhu.Rows.Clear();
                        if (!Utility.isValidGrid(grdList))
                        {
                            ClearControl();
                        }
                        grdList.DataSource = null;
                        int idbenhnhan = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan), -1);
                        DataTable dtData = _kcbThamkham.KcbLichsuKcbLuotkham(idbenhnhan);
                        Utility.SetDataSourceForDataGridEx_Basic(grdLuotkham, dtData, true, true, "", "");
                        UpdateGroup();
                        grdLuotkham.MoveFirst();
                        if (!Utility.isValidGrid(grdLuotkham))
                        {
                            ClearControl();
                        }
                        grdLuotkham.DataSource = null;
                        string maluotkham = Utility.sDbnull(grdLuotkham.GetValue(KcbLuotkham.Columns.MaLuotkham), "");
                        DataTable dtPhongKham = _kcbThamkham.KcbLichsuKcbTimkiemphongkham(ObjLuotkham.IdBenhnhan, maluotkham,0);
                        Utility.SetDataSourceForDataGridEx_Basic(grdRegExam, dtPhongKham, true, true, "", "");
                        grbRegs.Height = grdRegExam.GetDataRows().Length <= 1 ? 0 : 50;
                        grdRegExam.MoveFirst();

                        //grdList.ColumnButtonClick += new ColumnActionEventHandler(grdList_ColumnButtonClick);
                       // grdLuotkham.ColumnButtonClick += new ColumnActionEventHandler(grdLuotkham_ColumnButtonClick); 
                       // GetData();
                        txtPatient_Code.SelectAll();
                    }
                    else
                    {
                        string sPatientTemp = txtPatient_Code.Text;
                        ClearControl();
                        txtmaluotkham.Text = sPatientTemp;
                        txtmaluotkham.SelectAll();
                    }
                }
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy thông tin bệnh nhân");
            }
            finally
            {
                AllowTextChanged = true;
            }
        }

        private void mnuxemketqua_Click(object sender, EventArgs e)
        {
            mnuxemketqua.Tag = mnuxemketqua.Checked ? "1" : "0";
            if (PropertyLib._ThamKhamProperties.HienthiKetquaCLSTrongluoiChidinh)
            {
                Utility.ShowColumns(grdAssignDetail, mnuxemketqua.Checked ? _lstResultColumns : _lstVisibleColumns);
            }
        }

        private void cmdInphieuhen_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dtphienhen =
                    SPs.KcbThamkhamInphieuhenBenhnhan(Utility.sDbnull(txtPatient_Code.Text, ""),
                        Utility.Int64Dbnull(txtPatient_ID.Text, -1)).
                        GetDataSet().Tables[0];
                THU_VIEN_CHUNG.CreateXML(dtphienhen, "thamkham_inphieuhen_benhnhan.xml");
                KcbInphieu.INPHIEU_HEN(dtphienhen, "PHIẾU HẸN KHÁM");
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void grdLuotkham_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdLuotkham))
                {
                    ClearControl();
                }
                string maluotkham = Utility.sDbnull(grdLuotkham.GetValue(KcbLuotkham.Columns.MaLuotkham), "");
                DataTable dtData = _kcbThamkham.KcbLichsuKcbTimkiemphongkham(ObjLuotkham.IdBenhnhan, maluotkham,0);
                if (dtData.Rows.Count > 0)
                {
                    grdRegExam.DataSource = dtData;
                    grbRegs.Height = grdRegExam.GetDataRows().Length <= 1 ? 0 : 50;
                    grdRegExam.MoveFirst();
                }
               
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void cmdLuuChandoan_Click(object sender, EventArgs e)
        {
            if (Utility.Int32Dbnull(txtSoNgayHen.Text) > 0)
            {
                new Update(KcbChandoanKetluan.Schema).Set(KcbChandoanKetluan.Columns.SoNgayhen)
                    .EqualTo(Utility.Int32Dbnull(txtSoNgayHen.Text)).Where(KcbChandoanKetluan.Columns.MaLuotkham).IsEqualTo(Utility.sDbnull(txtPatient_Code.Text))
                    .Execute();
            }
        }

        private void cmdSearch_Click_1(object sender, EventArgs e)
        {

        }
    }
}