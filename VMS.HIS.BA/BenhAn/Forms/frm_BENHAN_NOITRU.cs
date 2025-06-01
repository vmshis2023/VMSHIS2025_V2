using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Janus.Windows.CalendarCombo;
using Janus.Windows.EditControls;
using Janus.Windows.GridEX.EditControls;
using SubSonic;
using VNS.Libs;
using VNS.HIS.NGHIEPVU;
using VNS.HIS.DAL;

namespace VNS.HIS.UI.BENH_AN
{
    public partial class frm_BENHAN_N0ITRU : Form
    {
        public int Lan_Vao_Vien_Thu = 1;
        public string MaBenhAnNoiTru = "";
        public string PatientCode = "";
        public string PatientId = "";
        public DataTable dtThongTinBenhAnNoiTru = new DataTable();
        public DataTable dtThongTinBenhAnNoiTruCu = new DataTable();
        private DataTable dt_ThongTinBenhNhan = new DataTable();
        public action em_Action = action.Insert;
        public bool has_Cancel = true;

        public DataTable dtCacKhoa = new DataTable();
               

        public frm_BENHAN_N0ITRU()
        {
            InitializeComponent();
        }

        private void frm_BENHAN_NOITRU_Load(object sender, EventArgs e)
        {
            LoadDanToc();

        }

        private void txtMaBN_KeyDown(object sender, KeyEventArgs e)
        {
            ClearControl();
            if (e.KeyCode == Keys.Enter)
            {
                FindPatientId(txtMaBN.Text);
                PatientId = txtMaBN.Text;
                PatientCode = txtMaLanKham.Text;
                try
                {
                    //Đã tồn tại bênh án
                    if (KT_TonTai_BenhAn())
                    {
                        em_Action = action.Update;
                        LayThongTinBenhAn(Utility.Int16Dbnull(txtMaBN.Text), txtMaLanKham.Text);
                        // LayThongTinLanKham(Utility.Int16Dbnull(txtMaBN.Text), txtMaLanKham.Text);
                        txtNgaySinh.Focus();

                        cmdInBenhAn.Enabled = true;
                    }
                        //chưa tồn tại thông tin ngoại tru. Them
                    else
                    {
                        em_Action = action.Insert;

                        txtNgaySinh.Focus();
                        LayThongTinLanKham(Utility.Int16Dbnull(txtMaBN.Text), txtMaLanKham.Text);
                        cmdInBenhAn.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    Utility.ShowMsg("FindPatient().Exception-->" + ex.Message);
                }
            }
        }

        private void txtMaLanKham_KeyDown(object sender, KeyEventArgs e)
        {
            ClearControl();
            if (e.KeyCode == Keys.Enter)
            {
                FindPatientCode(txtMaLanKham.Text);
                PatientId = txtMaBN.Text;
                PatientCode = txtMaLanKham.Text;
                try
                {
                    //Đã tồn tại bênh án
                    if (!KT_TonTai_BenhAn())
                    {
                        em_Action = action.Update;
                        LayThongTinBenhAn(Utility.Int16Dbnull(txtMaBN.Text), txtMaLanKham.Text);
                        txtNgaySinh.Focus();
                        cmdInBenhAn.Enabled = true;
                    }
                        //chưa tồn tại thông tin ngoại tru. Them
                    else
                    {
                        em_Action = action.Insert;

                        txtNgaySinh.Focus();
                        LayThongTinLanKham(Utility.Int16Dbnull(txtMaBN.Text), txtMaLanKham.Text);
                        cmdInBenhAn.Enabled = false;
                    }
                }
                catch (Exception ex)
                {
                    Utility.ShowMsg("FindPatient().Exception-->" + ex.Message);
                }

                // LayThongTinLanKham(Utility.Int16Dbnull(txtMaBN.Text), txtMaLanKham.Text);
            }
        }

        /// <summary>
        /// Tìm kiếm Mã Lần khám
        /// </summary>
        /// <param name="Patient_Code"></param>
        private void FindPatientCode(string Patient_Code)
        {
            var dtPatient = new DataTable();
            dtPatient =
                new Select(TPatientInfo.Columns.PatientId, TPatientExam.Columns.PatientCode,
                           TPatientInfo.Columns.PatientName, TPatientInfo.Columns.PatientAddr)
                    .From(TPatientExam.Schema)
                    .InnerJoin(TPatientInfo.PatientIdColumn, TPatientExam.PatientIdColumn)
                    .InnerJoin(TPatientDept.PatientCodeColumn, TPatientExam.PatientCodeColumn)
                    .Where(TPatientExam.Columns.PatientCode).ContainsString(
                        Patient_Code).And(TPatientDept.Columns.NoiTru).IsEqualTo(1).ExecuteDataSet().Tables[0];


            string PatientCodeFilter = globalVariables.SysDate.Year.ToString().Substring(2, 2) +
                                       Patient_Code.PadLeft(6, '0');
            DataRow[] arrPatients = dtPatient.Select("Patient_Code='" + PatientCodeFilter + "'");

            if (arrPatients.GetLength(0) <= 0)
            {
                // lọc được nhiều mã lần khám - Nhiều hơn 1 hàng
                if (dtPatient.Rows.Count > 1)
                {
                    var frm = new frm_DSACH_BN();

                    frm.PatientCode = txtMaLanKham.Text;
                    frm.dtPatient = dtPatient;
                    frm.ShowDialog();
                    if (!frm.has_Cancel)
                    {
                        txtMaLanKham.Text = frm.PatientCode;
                        txtMaBN.Text = frm.PatientId;
                        Lan_Vao_Vien_Thu = frm.Lan_Vao_Vien_Thu;
                    }
                }
            }
                //Nếu chỉ lọc được 1 mã bệnh nhân
            else
            {
                txtMaLanKham.Text = PatientCodeFilter;
                txtMaBN.Text = arrPatients[0][0].ToString();
            }
        }

        /// <summary>
        ///Tìm Kiếm các lần khám của một mã bệnh nhân
        /// </summary>
        /// <param name="Patient_Id"></param>
        private void FindPatientId(string Patient_Id) 
        {
            try
            {
                var dtPatient = new DataTable();
                dtPatient =
                    new Select(TPatientInfo.Columns.PatientId, TPatientExam.Columns.PatientCode,
                               TPatientInfo.Columns.PatientName, TPatientInfo.Columns.PatientAddr)
                        .From(TPatientExam.Schema)
                        .InnerJoin(TPatientInfo.PatientIdColumn, TPatientExam.PatientIdColumn)
                        .InnerJoin(TPatientDept.PatientCodeColumn, TPatientExam.PatientCodeColumn)
                        .Where(TPatientExam.Columns.PatientId).IsEqualTo(Patient_Id)
                        .And(TPatientDept.Columns.NoiTru).IsEqualTo(1).OrderAsc(TPatientExam.Columns.PatientCode).
                        ExecuteDataSet().Tables[0];


                if (dtPatient.Rows.Count >= 1)
                {
                    var frm = new frm_DSACH_BN();


                    frm.dtPatient = dtPatient;
                    frm.ShowDialog();
                    if (!frm.has_Cancel)
                    {
                        txtMaLanKham.Text = frm.PatientCode;
                        txtMaBN.Text = frm.PatientId;
                        Lan_Vao_Vien_Thu = frm.Lan_Vao_Vien_Thu;
                    }
                }
                else
                {
                    Utility.ShowMsg("Không tồn tại mã bệnh nhân vừa nhập");
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
            }
        }
         
        private void LayThongTinLanKham(int PatientID, string PatientCode)
        {
            dtThongTinBenhAnNoiTru = SPs.NoitietLayThongTinTaoBANoiTru(PatientID, PatientCode).GetDataSet().Tables[0];
            //TPatientInfo objThongTinBenhNhan = new Select("*")
            //    .From(TPatientInfo.Schema)
            //    .Where(TPatientInfo.Columns.PatientId).IsEqualTo(Utility.Int32Dbnull(PatientID)).ExecuteSingle
            //    <TPatientInfo>();
            DataRow dr = dtThongTinBenhAnNoiTru.Rows[0];

            try
            {
                txtHoTen.Text = dr["Patient_Name"].ToString();
                txtDiaChi.Text = dr["Patient_Addr"].ToString();
                txtNamSinh.Text = dr["Year_Of_Birth"].ToString();
                cboGioiTinh.SelectedValue = dr["Patient_Sex"].ToString();

                txtNgheNghiep.Text = dr["Patient_Job"].ToString();
                cboTTBNDanToc.SelectedValue = dr["Dan_Toc"].ToString();
                txtDoiTuong.Text = dr["Ma_DoiTuong"].ToString();
                dtInsToDate.Text = dr["Insurance_ToDate"].ToString();
                txtSoBaoHiemYte.Text = dr["Insurance_Num"].ToString();
                txtMaKhoaThucHien.Text = dr["MA_KHOA_THIEN"].ToString();
                dtQLNBVaoVien.Value = Convert.ToDateTime(dr["NGAY_NHAP_VIEN"].ToString());
                chkQLNBTuDen.Checked = Utility.Int16Dbnull(dr["Emergency_Hos"].ToString()) == 1;
                chkQLNBKKB.Checked = Utility.Int16Dbnull(dr["Emergency_Hos"].ToString()) == 0;
                chkQLNBKhoaDieuTri.Checked = Utility.Int16Dbnull(dr["Emergency_Hos"].ToString()) == 2;
                chkQLNBCoQuanYTe.Checked = Utility.Int16Dbnull(dr["CorrectLine"].ToString()) == 1;
                chkQLNBTuDen.Checked = Utility.Int16Dbnull(dr["CorrectLine"].ToString()) == 0;
                chkQLNBKhac.Checked = Utility.Int16Dbnull(dr["CorrectLine"].ToString()) == 3;
                txtQLNBLanVaoVien.Text = Convert.ToString(Lan_Vao_Vien_Thu);
                dtCacKhoa = SPs.NoitietCacKhoaBnChuyen(PatientCode).GetDataSet().Tables[0];
                grdQLNBKhoa.DataSource = null;
               
                grdQLNBKhoa.DataSource = dtCacKhoa;
                 DataRow drdep = dtCacKhoa.Rows[dtCacKhoa.Rows.Count - 1];
                 txtKhoa.Text = drdep["Khoa"].ToString();
                txtGiuong.Text = drdep["Bed"].ToString();
                txtPhong.Text = drdep["Room"].ToString();

                
                string ICD_chinh_Name = "";
                string ICD_chinh_Code = "";
                string ICD_Phu_Name = "";
                string ICD_Phu_Code = "";
                string ICD_Khoa_NoITru = "";
                string Name_Khoa_NoITru = "";
                GetChanDoanChinhPhu(dr["MA_CD_KKB"].ToString(),
                                    dr["MA_CD_PHU_KKB"].ToString(),
                                    ref ICD_chinh_Name,
                                    ref ICD_chinh_Code, ref ICD_Phu_Name,
                                    ref ICD_Phu_Code);
                GetChanDoan(PatientCode, ref ICD_Khoa_NoITru, ref Name_Khoa_NoITru);
                txtCDKKBCapCuu.Text = ICD_chinh_Name + ICD_Phu_Name;
                txtCDMaKKBCapCuu.Text = ICD_chinh_Code + ICD_Phu_Code;


                txtCDKhiVaoDieuTri.Text = Name_Khoa_NoITru;
                txtCDMaKhiVaoDieuTri.Text = ICD_Khoa_NoITru;
                chkQLNBTuyenTren.Checked = Utility.Int16Dbnull(dr["ID_KIEU_CVIEN"].ToString()) == 3;
                chkQLNBTuyenDuoi.Checked = Utility.Int16Dbnull(dr["ID_KIEU_CVIEN"].ToString()) == 1;
                chkQLNBCK.Checked = Utility.Int16Dbnull(dr["ID_KIEU_CVIEN"].ToString()) == 2;
                dtQLNBRaVien.Text = dr["NGAY_RA_VIEN"].ToString();
                chkQLNBRaVien.Checked = Utility.Int16Dbnull(dr["TTRANG_BNHAN_RVIEN"].ToString()) == 1;
                chkQLNBXinVe.Checked = Utility.Int16Dbnull(dr["TTRANG_BNHAN_RVIEN"].ToString()) == 3;
                chkQLNBBoVe.Checked = Utility.Int16Dbnull(dr["TTRANG_BNHAN_RVIEN"].ToString()) == 4;
                chkQLNBDuaVe.Checked = Utility.Int16Dbnull(dr["TTRANG_BNHAN_RVIEN"].ToString()) == 5;
                txtQLNBTongSoNgayDieuTri.Text = dr["Tong_SoNgay_Dtri"].ToString();

                chkCDThuThuat.Checked = Utility.Int16Dbnull(dr["Thu_Thuat"].ToString()) == 1;
                chkCDPhauThuat.Checked = Utility.Int16Dbnull(dr["Phau_Thuat"].ToString()) == 1;
                chkCDTaiBien.Checked = Utility.Int16Dbnull(dr["Tai_Bien"].ToString()) == 1;
                chkCDBienChung.Checked = Utility.Int16Dbnull(dr["Bien_Chung"].ToString()) == 1;


                chkTTRVKhoi.Checked = Utility.Int16Dbnull(dr["ID_KET_QUA_DTRI"].ToString()) == 1;
                chkTTRVDoGiam.Checked = Utility.Int16Dbnull(dr["ID_KET_QUA_DTRI"].ToString()) == 2;
                chkTTRVKhongThayDoi.Checked = Utility.Int16Dbnull(dr["ID_KET_QUA_DTRI"].ToString()) == 3;
                chkTTRVNangHon.Checked = Utility.Int16Dbnull(dr["ID_KET_QUA_DTRI"].ToString()) == 4;
                chkTTRVTuVong.Checked = Utility.Int16Dbnull(dr["ID_KET_QUA_DTRI"].ToString()) == 5;
            }
            catch (Exception ex)
            {
            }
        }

        private void LayThongTinBenhAn(int PatientID, string PatientCode)
        {
            dtThongTinBenhAnNoiTruCu = SPs.NoitietLayThongTinBenhAnCu(PatientID, PatientCode).GetDataSet().Tables[0];
            //TPatientInfo objThongTinBenhNhan = new Select("*")
            //    .From(TPatientInfo.Schema)
            //    .Where(TPatientInfo.Columns.PatientId).IsEqualTo(Utility.Int32Dbnull(PatientID)).ExecuteSingle
            //    <TPatientInfo>();
            DataRow dr = dtThongTinBenhAnNoiTruCu.Rows[0];

            try
            {
                txtKhoa.Text = dr["Khoa"].ToString();
                txtPhong.Text = dr["Phong"].ToString();
                txtGiuong.Text =  dr["Giuong"].ToString();
                txtID.Text = dr["ID"].ToString();
                txtHoTen.Text = dr["Patient_Name"].ToString();
                txtDiaChi.Text = dr["Patient_Addr"].ToString();
                txtNamSinh.Text = dr["Year_Of_Birth"].ToString();
                cboGioiTinh.SelectedValue = dr["Patient_Sex"].ToString();

                txtNgheNghiep.Text = dr["Patient_Job"].ToString();
                cboTTBNDanToc.SelectedValue = dr["Dan_Toc"].ToString();
                txtDoiTuong.Text = dr["Ma_DoiTuong"].ToString();
                dtInsToDate.Text = dr["Insurance_ToDate"].ToString();
                txtSoBaoHiemYte.Text = dr["Insurance_Num"].ToString();
                txtMaKhoaThucHien.Text = dr["MaKhoaThien"].ToString();
                txtMaBenhAn.Text = dr["MaBaNoitru"].ToString();
                txtNgaySinh.Text = dr["NgaySinh"].ToString();
                txtThangSinh.Text = dr["ThangSinh"].ToString();
                txtNgheNghiep.Text = dr["NgheNghiep"].ToString();
                txtNoiLamViec.Text = dr["NoiLamViec"].ToString();
                txtThongTinLienHe.Text = dr["ThongtinLhe"].ToString();
                txtDienThoai.Text = dr["DthoaiLhe"].ToString();

                dtQLNBRaVien.Text = dr["QlnbRaVien"].ToString();
                dtQLNBVaoVien.Value = Convert.ToDateTime(dr["NgayVaoVien"].ToString());
                chkQLNBTuDen.Checked = Utility.Int16Dbnull(dr["QlnbTtiepVao"].ToString()) == 1;
                chkQLNBKKB.Checked = Utility.Int16Dbnull(dr["QlnbTtiepVao"].ToString()) == 2;
                chkQLNBKhoaDieuTri.Checked = Utility.Int16Dbnull(dr["QlnbTtiepVao"].ToString()) == 3;
                chkQLNBCoQuanYTe.Checked = Utility.Int16Dbnull(dr["QlnbNgioiThieu"].ToString()) == 1;
                chkQLNBTuDen.Checked = Utility.Int16Dbnull(dr["QlnbNgioiThieu"].ToString()) == 2;
                chkQLNBKhac.Checked = Utility.Int16Dbnull(dr["QlnbNgioiThieu"].ToString()) == 3;
                txtQLNBLanVaoVien.Text = dr["QlnbLanVvien"].ToString();
                //var dtCacKhoa = new DataTable();
                dtCacKhoa = SPs.NoitietCacKhoaBnChuyen(txtMaLanKham.Text).GetDataSet().Tables[0];
                grdQLNBKhoa.DataSource = null;
                grdQLNBKhoa.DataSource = dtCacKhoa;
                //string ICD_chinh_Name = "";
                //string ICD_chinh_Code = "";
                //string ICD_Phu_Name = "";
                //string ICD_Phu_Code = "";
                //string ICD_Khoa_NoITru = "";
                //string Name_Khoa_NoITru = "";
                //GetChanDoanChinhPhu(dr["MA_CD_KKB"].ToString(),
                //                    dr["MA_CD_PHU_KKB"].ToString(),
                //                    ref ICD_chinh_Name,
                //                    ref ICD_chinh_Code, ref ICD_Phu_Name,
                //                    ref ICD_Phu_Code);
                //GetChanDoan(PatientCode, ref ICD_Khoa_NoITru, ref Name_Khoa_NoITru);
                //txtCDKKBCapCuu.Text = ICD_chinh_Name + ICD_Phu_Name;
                //txtCDMaKKBCapCuu.Text = ICD_chinh_Code + ICD_Phu_Code;
                //txtCDKhiVaoDieuTri.Text = Name_Khoa_NoITru;
                //txtCDMaKhiVaoDieuTri.Text = ICD_Khoa_NoITru;

                chkQLNBTuyenTren.Checked = Utility.Int16Dbnull(dr["QlnbChuyenVien"].ToString()) == 3;
                chkQLNBTuyenDuoi.Checked = Utility.Int16Dbnull(dr["QlnbChuyenVien"].ToString()) == 1;
                chkQLNBCK.Checked = Utility.Int16Dbnull(dr["QlnbChuyenVien"].ToString()) == 2;
                txtQLNBChuyenDen.Text = Utility.sDbnull(dr["QlnbChuyenDen"].ToString());
                dtQLNBRaVien.Text = dr["QlnbRaVien"].ToString();
                chkQLNBRaVien.Checked = Utility.Int16Dbnull(dr["QlnbLdoRvien"].ToString()) == 1;
                chkQLNBXinVe.Checked = Utility.Int16Dbnull(dr["QlnbLdoRvien"].ToString()) == 3;
                chkQLNBBoVe.Checked = Utility.Int16Dbnull(dr["QlnbLdoRvien"].ToString()) == 4;
                chkQLNBDuaVe.Checked = Utility.Int16Dbnull(dr["QlnbLdoRvien"].ToString()) == 5;
                txtQLNBTongSoNgayDieuTri.Text = dr["QlnbTongNgayDt"].ToString();
                txtCDNoiChuyenDen.Text = Utility.sDbnull(dr["CdNoiChuyenDen"].ToString());
                txtCDMaNoiChuyenDen.Text = Utility.sDbnull(dr["CdMaNoiChuyenDen"].ToString());
                txtCDKKBCapCuu.Text = Utility.sDbnull(dr["CdKkbCcuu"].ToString());
                txtCDMaKKBCapCuu.Text = Utility.sDbnull(dr["CdMaKkbCcuu"].ToString());
                txtCDKhiVaoDieuTri.Text = Utility.sDbnull(dr["CdKhoaDtri"].ToString());
                txtCDMaKhiVaoDieuTri.Text = Utility.sDbnull(dr["CdMaKhoaDtri"].ToString());
                txtCDBenhChinh.Text = Utility.sDbnull(dr["CdRvienBchinh"].ToString());
                txtCDMaBenhChinh.Text = Utility.sDbnull(dr["CdMaRvienBchinh"].ToString());
                txtCDBenhKemTheo.Text = Utility.sDbnull(dr["CdRvienBphu"].ToString());
                txtCDMaBenhKemTheo.Text = Utility.sDbnull(dr["CdMaRvienBphu"].ToString());
                chkCDThuThuat.Checked = Utility.Int16Dbnull(dr["CdThuThuat"].ToString()) == 1;
                chkCDPhauThuat.Checked = Utility.Int16Dbnull(dr["CdPhauThuat"].ToString()) == 1;
                chkCDTaiBien.Checked = Utility.Int16Dbnull(dr["CdTaiBien"].ToString()) == 1;
                chkCDBienChung.Checked = Utility.Int16Dbnull(dr["CdBienChung"].ToString()) == 1;
                chkTTRVKhoi.Checked = Utility.Int16Dbnull(dr["TtrvKquaDtri"].ToString()) == 1;
                chkTTRVDoGiam.Checked = Utility.Int16Dbnull(dr["TtrvKquaDtri"].ToString()) == 2;
                chkTTRVKhongThayDoi.Checked = Utility.Int16Dbnull(dr["TtrvKquaDtri"].ToString()) == 3;
                chkTTRVNangHon.Checked = Utility.Int16Dbnull(dr["TtrvKquaDtri"].ToString()) == 4;
                chkTTRVTuVong.Checked = Utility.Int16Dbnull(dr["TtrvKquaDtri"].ToString()) == 5;

                chkTTRVLanhTinh.Checked = Utility.Int16Dbnull(dr["TtrvGphauBenh"].ToString()) == 1;
                chkTTRVNghiNgo.Checked = Utility.Int16Dbnull(dr["TtrvGphauBenh"].ToString()) == 2;
                chkTTRVAcTinh.Checked = Utility.Int16Dbnull(dr["TtrvGphauBenh"].ToString()) == 3;

                txtTTRVNgayTuVong.Text = Utility.sDbnull(dr["TtrvTVong"].ToString());
                chkttrvDoBenh.Checked = Utility.Int16Dbnull(dr["TtrvLdoTvong"].ToString()) == 1;
                chkttrvTrong24GioVaoVien.Checked = Utility.Int16Dbnull(dr["TtrvLdoTvong"].ToString()) == 2;
                chkttrvDoTaiBien.Checked = Utility.Int16Dbnull(dr["TtrvLdoTvong"].ToString()) == 3;
                chkttrvSau24Gio.Checked = Utility.Int16Dbnull(dr["TtrvLdoTvong"].ToString()) == 4;
                chkttrvKhac.Checked = Utility.Int16Dbnull(dr["TtrvLdoTvong"].ToString()) == 5;
                txtTTRVNguyenNhanChinhTuVong.Text = Utility.sDbnull(dr["TtrvNnhanTvong"].ToString());
                chkTTRVKhamNgiemTuThi.Checked = Utility.Int16Dbnull(dr["TtrvKhamNghiem"].ToString()) == 1;
                txtTTRVChuanDoanGiaiPhau.Text = Utility.sDbnull(dr["TtrvCdoanGphau"].ToString());
                txtBenhAnLyDoNhapVien.Text = Utility.sDbnull(dr["BaLdvv"].ToString()); 
                 txtBenhAnVaoNgayThu.Text = Utility.sDbnull(dr["BaNgayThu"].ToString());
                 txtBenhAnQuaTrinhBenhLy.Text = Utility.sDbnull(dr["BaQtbl"].ToString()); 
                txtBenhAnTienSuBenh.Text = Utility.sDbnull(dr["BaTsb"].ToString());
            chkDiUng.Checked = Utility.Int16Dbnull(dr["BaDiUng"].ToString()) == 1; ;
                 chkMaTuy.Checked = Utility.Int16Dbnull(dr["BaMaTuy"].ToString()) == 1; ;
                 chkRuouBia.Checked= Utility.Int16Dbnull(dr["BaRuouBia"].ToString()) == 1;
               chkThuocLa.Checked  = Utility.Int16Dbnull(dr["BaThuocLa"].ToString()) == 1;
                chkThuocLao.Checked = Utility.Int16Dbnull(dr["BaThuocLao"].ToString()) == 1; ;
             chkKhac.Checked  = Utility.Int16Dbnull(dr["BaKhac"].ToString()) == 1; ;
                 txtDiUng.Text = Utility.sDbnull(dr["BaTgDiUng"].ToString());
                 txtMaTuy.Text = Utility.sDbnull(dr["BaTgMaTuy"].ToString()); 
                 txtRuouBia.Text = Utility.sDbnull(dr["BaTgRuouBia"].ToString()); 
                 txtThuocLa.Text = Utility.sDbnull(dr["BaTgThuocLa"].ToString());
              txtThuocLao.Text = Utility.sDbnull(dr["BaTgThuocLao"].ToString());
                 txtKhac.Text = Utility.sDbnull(dr["BaTgKhac"].ToString());
                 txtBenhAnGiaDinh.Text = Utility.sDbnull(dr["BaGiaDinh"].ToString());
                 txtBenhAnToanThan.Text = Utility.sDbnull(dr["KbToanThan"].ToString());
                 txtMach.Text = Utility.sDbnull(dr["KbMach"].ToString());
                txtNhietDo.Text = Utility.sDbnull(dr["KbNhietDo"].ToString());
                txtHuyetApTu.Text = Utility.sDbnull(dr["KbHuyetAp"].ToString());
                 txtNhipTho.Text = Utility.sDbnull(dr["KbNhipTho"].ToString());
                 txtCanNang.Text = Utility.sDbnull(dr["KbCanNang"].ToString());
                 txtBenhAnTuanHoan.Text = Utility.sDbnull(dr["KbTuanHoan"].ToString());
                txtBenhAnHoHap.Text = Utility.sDbnull(dr["KbHoHap"].ToString());
                 txtBenhAnTieuHoa.Text = Utility.sDbnull(dr["KbTieuHoa"].ToString());
                 txtBenhAnThanTietNieuSinhDuc.Text = Utility.sDbnull(dr["KbThan"].ToString());
                txtBenhAnThanKinh.Text = Utility.sDbnull(dr["KbThanKinh"].ToString());
                txtBenhAnCoXuongKhop.Text = Utility.sDbnull(dr["KbCo"].ToString());
                txtBenhAnTaiMuiHong.Text = Utility.sDbnull(dr["KbTai"].ToString());
                 txtBenhAnRangHamMat.Text = Utility.sDbnull(dr["KbRang"].ToString());
                 txtBenhAnMat.Text = Utility.sDbnull(dr["KbMat"].ToString());
                 txtBenhAnNoiTiet.Text = Utility.sDbnull(dr["KbNoiTiet"].ToString());
                 txtBenhAnCacXetNghiem.Text = Utility.sDbnull(dr["KbXnCls"].ToString());
                 txtBenhAnTomTatBenhAn.Text = Utility.sDbnull(dr["KbTtba"].ToString());
                txtBenhAnBenhChinh.Text = Utility.sDbnull(dr["KbBenhChinh"].ToString());
                 txtBenhAnBenhKemTheo.Text = Utility.sDbnull(dr["KbBenhPhu"].ToString());
                txtBenhAnPhanBiet.Text = Utility.sDbnull(dr["KbPhanBiet"].ToString());
                txtBenhAnTienLuong.Text = Utility.sDbnull(dr["KbTienLuong"].ToString());
                txtBenhAnHuongDieuTri.Text = Utility.sDbnull(dr["KbHuongDtri"].ToString());
                txtTKBAQuaTrinhBenhLy.Text = Utility.sDbnull(dr["TkbaQtbl"].ToString());
                txtTKBATTomTatKetQua.Text = Utility.sDbnull(dr["TkbaTtkqxn"].ToString());
                 txtTKBAPhuongPhapDieuTri.Text = Utility.sDbnull(dr["TkbaPpdt"].ToString());
                 txtTKBATinhTrangRaVien.Text = Utility.sDbnull(dr["TkbaTtrv"].ToString());
                 txtTKBAHuongDieuTri.Text = Utility.sDbnull(dr["TkbaHdt"].ToString());




                Utility.SetMessage(lblMess, "", true);
            }
            catch (Exception ex)
            {
            }
        }

        private void LoadDanToc()
        {
            DataBinding.BindData(cboTTBNDanToc, CommonBusiness.LayThongTinDanToc(),
                                 LDanToc.Columns.IdDanToc, LDanToc.Columns.TenDanToc);
        }

        private void GetChanDoanChinhPhu(string ICD_chinh, string IDC_Phu, ref string ICD_chinh_Name,
                                         ref string ICD_chinh_Code, ref string ICD_Phu_Name, ref string ICD_Phu_Code)
        {
            try
            {
                List<string> lstICD = ICD_chinh.Split(',').ToList();
                LDiseaseCollection _list =
                    new LDiseaseController().FetchByQuery(
                        LDisease.CreateQuery().AddWhere(LDisease.DiseaseCodeColumn.ColumnName, Comparison.In, lstICD));
                foreach (LDisease _objBenhAnNoiTru in _list)
                {
                    ICD_chinh_Name += _objBenhAnNoiTru.DiseaseName + ";";
                    ICD_chinh_Code += _objBenhAnNoiTru.DiseaseCode + ";";
                }
                lstICD = IDC_Phu.Split(',').ToList();
                _list =
                    new LDiseaseController().FetchByQuery(
                        LDisease.CreateQuery().AddWhere(LDisease.DiseaseCodeColumn.ColumnName, Comparison.In, lstICD));
                foreach (LDisease _objBenhAnNoiTru in _list)
                {
                    ICD_Phu_Name += _objBenhAnNoiTru.DiseaseName + ";";
                    ICD_Phu_Code += _objBenhAnNoiTru.DiseaseCode + ";";
                }
            }
            catch
            {
            }
        }

        private void GetChanDoan(String PatientCode, ref string ICD_Khoa_NoITru, ref string Name_Khoa_NoITru)
        {
            var dtPatient = new DataTable();
            dtPatient =
                new Select("*")
                    .From(TDiagInfo.Schema)
                    .Where(TDiagInfo.Columns.PatientCode).IsEqualTo(PatientCode).And(TDiagInfo.Columns.KeyCode).
                    IsEqualTo("NOITRU")
                    .ExecuteDataSet().Tables[0];
            foreach (DataRow row in dtPatient.Rows)
            {
                ICD_Khoa_NoITru += row["MainDisease_ID"] + ";";
                Name_Khoa_NoITru += row["Diag_Info"] + ";";
            }
        }

        private void ClearControl()
        {
            foreach (Control control in grpChanDoan.Controls)
            {
                if (control is EditBox)
                {
                    var txtControl = new EditBox();
                    txtControl = ((EditBox) control);
                    txtControl.Clear();
                }
                if (control is CalendarCombo)
                {
                    var txtControl = new CalendarCombo();
                    txtControl = ((CalendarCombo) control);
                    txtControl.NullValue = true;
                }

                if (control is UICheckBox)
                {
                    var txtControl = new UICheckBox();
                    txtControl = ((UICheckBox) control);
                    txtControl.Checked = false;
                }
                if (control is MaskedEditBox)
                {
                    var txtControl = new MaskedEditBox();
                    txtControl = ((MaskedEditBox) control);
                    txtControl.Clear();
                }
                if (control is RichTextBox)
                {
                    var txtControl = new RichTextBox();
                    txtControl = ((RichTextBox) control);
                    txtControl.Clear();
                }
            }
        }

        private bool KT_TonTai_BenhAn()
        {
            SqlQuery BenhAnNoiTru =
                new Select().From(TBenhanNoitru.Schema).
                    Where(TBenhanNoitru.Columns.PatientId).IsEqualTo(Utility.Int32Dbnull(txtMaBN.Text))
                    .And(TBenhanNoitru.Columns.PatientCode).IsEqualTo(Utility.sDbnull(txtMaLanKham.Text));


            //Đã Tồn tại thông tin bệnh . Sửa
            if (BenhAnNoiTru.GetRecordCount() > 0)
                return true;
            if (BenhAnNoiTru.GetRecordCount() <= 0)
                return false;

            return false;
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            PerformAction();
        }

        private void PerformAction()
        {
            switch (em_Action)
            {
                case action.Update:
                    UpdatePatient();
                    break;
                case action.Insert:
                    InsertPatient();
                    break;
                case action.Select:
                    // AddNewPatient();
                    break;
                case action.Delete:
                    DeletePatient();
                    break;
            }
        }

        //Sửa lại sau khi có yêu cầu của viện
        private void SinhMaBenhAn()
        {
            if (em_Action == action.Insert)
            {
                txtMaBenhAn.Text = BusinessHelper.SinhMaBenhAn_NoiTru();
            }
            if (BusinessHelper.GetAccountName() == "NOITIET")
            {
                txtMaBenhAn.Text = BusinessHelper.SinhMaBenhAn_NoiTru();
            }
            else
            {
            }
        }

        private TBenhanNoitru CreateBenhAnNoiTru()
        {
            var objBenhAnNoiTru = new TBenhanNoitru();
            try
            {
                if (em_Action == action.Update)
                {
                    objBenhAnNoiTru.IsLoaded = true;
                    objBenhAnNoiTru.MarkOld();
                    objBenhAnNoiTru.Id = Utility.Int32Dbnull(txtID.Text, -1);
                    objBenhAnNoiTru.NgaySua = BusinessHelper.GetSysDateTime();
                    objBenhAnNoiTru.NguoiSua = globalVariables.UserName;
                }
                if (em_Action == action.Insert)
                {
                    objBenhAnNoiTru.NguoiTao = globalVariables.UserName;
                    objBenhAnNoiTru.NgayTao = BusinessHelper.GetSysDateTime();
                }
                objBenhAnNoiTru.Phong = txtPhong.Text;
                objBenhAnNoiTru.Khoa = txtKhoa.Text;
                objBenhAnNoiTru.Giuong = txtGiuong.Text;
                objBenhAnNoiTru.MaBaNoitru = Utility.sDbnull(txtMaBenhAn.Text);
                objBenhAnNoiTru.PatientId = Utility.Int32Dbnull(txtMaBN.Text);
                objBenhAnNoiTru.PatientCode = Utility.sDbnull(txtMaLanKham.Text);
                objBenhAnNoiTru.NgaySinh = Utility.sDbnull(txtNgaySinh.Text);
                objBenhAnNoiTru.ThangSinh = Utility.sDbnull(txtThangSinh.Text);
                objBenhAnNoiTru.DanToc = Utility.sDbnull(cboTTBNDanToc.Text);
                objBenhAnNoiTru.NoiLamViec = Utility.sDbnull(txtNoiLamViec.Text);
                objBenhAnNoiTru.ThongtinLhe = Utility.sDbnull(txtThongTinLienHe.Text);
                objBenhAnNoiTru.MaKhoaThien = txtMaKhoaThucHien.Text;
                objBenhAnNoiTru.NgoaiKieu = Utility.Int16Dbnull(chkNgoaiKieu.Checked ? 1 : 0);
                objBenhAnNoiTru.NgheNghiep = Utility.sDbnull(txtNgheNghiep.Text);
                objBenhAnNoiTru.DthoaiLhe = Utility.sDbnull(txtDienThoai.Text);
                objBenhAnNoiTru.NgayVaoVien = dtQLNBVaoVien.Text != null
                                                  ? Convert.ToDateTime(dtQLNBVaoVien.Text)
                                                  : (DateTime?) null;
                // objBenhAnNoiTru.GioPhutVaoVien = GioPhutVaoVien;
                if (chkQLNBCapCuu.Checked) objBenhAnNoiTru.QlnbTtiepVao = 1;
                else if (chkQLNBKKB.Checked)
                {
                    objBenhAnNoiTru.QlnbTtiepVao = 2;
                }
                else if (chkQLNBKhoaDieuTri.Checked)
                {
                    objBenhAnNoiTru.QlnbTtiepVao = 3;
                }
                else objBenhAnNoiTru.QlnbTtiepVao = 0;
                //objBenhAnNoiTru.QlnbKhoa = QlnbKhoa;
                if (chkQLNBCoQuanYTe.Checked) objBenhAnNoiTru.QlnbNgioiThieu = 1;
                else if (chkQLNBTuDen.Checked)
                {
                    objBenhAnNoiTru.QlnbNgioiThieu = 2;
                }
                else if (chkQLNBKhac.Checked)
                {
                    objBenhAnNoiTru.QlnbNgioiThieu = 3;
                }
                else objBenhAnNoiTru.QlnbNgioiThieu = 0;
                objBenhAnNoiTru.QlnbLanVvien = Utility.sDbnull(txtQLNBLanVaoVien.Text);

                if (chkQLNBTuyenTren.Checked) objBenhAnNoiTru.QlnbChuyenVien = 3;
                else if (chkQLNBTuyenDuoi.Checked)
                {
                    objBenhAnNoiTru.QlnbChuyenVien = 1;
                }
                else if (chkQLNBCK.Checked)
                {
                    objBenhAnNoiTru.QlnbChuyenVien = 2;
                }
                else objBenhAnNoiTru.QlnbChuyenVien = 0;

                objBenhAnNoiTru.QlnbChuyenDen = Utility.sDbnull(txtQLNBChuyenDen.Text);
                objBenhAnNoiTru.QlnbRaVien = Utility.sDbnull( dtQLNBRaVien.Text);
                if (chkQLNBRaVien.Checked) objBenhAnNoiTru.QlnbLdoRvien = 1;
                else if (chkQLNBXinVe.Checked)
                {
                    objBenhAnNoiTru.QlnbLdoRvien = 3;
                }
                else if (chkQLNBBoVe.Checked)
                {
                    objBenhAnNoiTru.QlnbLdoRvien = 4;
                }

                else if (chkQLNBDuaVe.Checked)
                {
                    objBenhAnNoiTru.QlnbLdoRvien = 5;
                }
                else objBenhAnNoiTru.QlnbLdoRvien = 0;


                objBenhAnNoiTru.QlnbTongNgayDt = Utility.Int16Dbnull(txtQLNBTongSoNgayDieuTri.Text);

                objBenhAnNoiTru.CdNoiChuyenDen = Utility.sDbnull(txtQLNBChuyenDen.Text);

                //  objBenhAnNoiTru.CdMaNoiChuyenDen = Utility.sDbnull();

                objBenhAnNoiTru.CdKkbCcuu = txtCDKKBCapCuu.Text;

                objBenhAnNoiTru.CdMaKkbCcuu = txtCDMaKKBCapCuu.Text;

                objBenhAnNoiTru.CdKhoaDtri = txtCDKhiVaoDieuTri.Text;

                objBenhAnNoiTru.CdMaKhoaDtri = txtCDMaKhiVaoDieuTri.Text;
                objBenhAnNoiTru.CdNoiChuyenDen = txtCDNoiChuyenDen.Text;
                objBenhAnNoiTru.CdMaNoiChuyenDen = txtCDMaNoiChuyenDen.Text;
                objBenhAnNoiTru.CdThuThuat = Utility.Int16Dbnull(chkCDThuThuat.Checked ? 1 : 0);
                objBenhAnNoiTru.CdPhauThuat = Utility.Int16Dbnull(chkCDPhauThuat.Checked ? 1 : 0);

                objBenhAnNoiTru.CdRvienBchinh = txtCDBenhChinh.Text;

                objBenhAnNoiTru.CdMaRvienBchinh = txtCDMaBenhChinh.Text;

                objBenhAnNoiTru.CdRvienBphu = txtCDBenhKemTheo.Text;

                objBenhAnNoiTru.CdMaRvienBphu = txtCDMaBenhKemTheo.Text;

                objBenhAnNoiTru.CdTaiBien = Utility.Int16Dbnull(chkCDTaiBien.Checked ? 1 : 0);

                objBenhAnNoiTru.CdBienChung = Utility.Int16Dbnull(chkCDBienChung.Checked ? 1 : 0);


                if (chkTTRVKhoi.Checked) objBenhAnNoiTru.TtrvKquaDtri = 1;
                else if (chkTTRVDoGiam.Checked)
                {
                    objBenhAnNoiTru.TtrvKquaDtri = 2;
                }
                else if (chkTTRVKhongThayDoi.Checked)
                {
                    objBenhAnNoiTru.TtrvKquaDtri = 3;
                }

                else if (chkTTRVNangHon.Checked)
                {
                    objBenhAnNoiTru.TtrvKquaDtri = 4;
                }
                else if (chkTTRVTuVong.Checked)
                {
                    objBenhAnNoiTru.TtrvKquaDtri = 5;
                }
                else objBenhAnNoiTru.TtrvKquaDtri = 0;


                if (chkTTRVLanhTinh.Checked) objBenhAnNoiTru.TtrvGphauBenh = 1;
                else if (chkTTRVNghiNgo.Checked)
                {
                    objBenhAnNoiTru.TtrvGphauBenh = 2;
                }
                else if (chkTTRVAcTinh.Checked)
                {
                    objBenhAnNoiTru.TtrvGphauBenh = 3;
                }

                else objBenhAnNoiTru.TtrvGphauBenh = 0;

                objBenhAnNoiTru.TtrvTVong = txtTTRVNgayTuVong.Text;


                if (chkttrvDoBenh.Checked) objBenhAnNoiTru.TtrvLdoTvong = 1;
                else if (chkttrvTrong24GioVaoVien.Checked)
                {
                    objBenhAnNoiTru.TtrvLdoTvong = 2;
                }
                else if (chkttrvDoTaiBien.Checked)
                {
                    objBenhAnNoiTru.TtrvLdoTvong = 3;
                }
                else if (chkttrvSau24Gio.Checked)
                {
                    objBenhAnNoiTru.TtrvLdoTvong = 4;
                }
                else if (chkttrvKhac.Checked)
                {
                    objBenhAnNoiTru.TtrvLdoTvong = 5;
                }

                else objBenhAnNoiTru.TtrvLdoTvong = 0;


                objBenhAnNoiTru.TtrvNnhanTvong = txtTTRVNguyenNhanChinhTuVong.Text;

                objBenhAnNoiTru.TtrvKhamNghiem = Utility.Int16Dbnull(chkTTRVKhamNgiemTuThi.Checked ? 1 : 0);

                objBenhAnNoiTru.TtrvCdoanGphau = txtTTRVChuanDoanGiaiPhau.Text;

                objBenhAnNoiTru.BaLdvv = txtBenhAnLyDoNhapVien.Text;

                objBenhAnNoiTru.BaNgayThu = txtBenhAnVaoNgayThu.Text;

                objBenhAnNoiTru.BaQtbl = txtBenhAnQuaTrinhBenhLy.Text;

                objBenhAnNoiTru.BaTsb = txtBenhAnTienSuBenh.Text;

                objBenhAnNoiTru.BaDiUng = Utility.Int16Dbnull(chkDiUng.Checked ? 1 : 0);

                objBenhAnNoiTru.BaMaTuy = Utility.Int16Dbnull(chkMaTuy.Checked ? 1 : 0);

                objBenhAnNoiTru.BaRuouBia = Utility.Int16Dbnull(chkRuouBia.Checked ? 1 : 0);

                objBenhAnNoiTru.BaThuocLa = Utility.Int16Dbnull(chkThuocLa.Checked ? 1 : 0);

                objBenhAnNoiTru.BaThuocLao = Utility.Int16Dbnull(chkThuocLao.Checked ? 1 : 0);

                objBenhAnNoiTru.BaKhac = Utility.Int16Dbnull(chkKhac.Checked ? 1 : 0);

                objBenhAnNoiTru.BaTgDiUng = txtDiUng.Text;

                objBenhAnNoiTru.BaTgMaTuy = txtMaTuy.Text;

                objBenhAnNoiTru.BaTgRuouBia = txtRuouBia.Text;

                objBenhAnNoiTru.BaTgThuocLa = txtThuocLa.Text;

                objBenhAnNoiTru.BaTgThuocLao = txtThuocLao.Text;

                objBenhAnNoiTru.BaTgKhac = txtKhac.Text;

                objBenhAnNoiTru.BaGiaDinh = txtBenhAnGiaDinh.Text;

                objBenhAnNoiTru.KbToanThan = txtBenhAnToanThan.Text;

                objBenhAnNoiTru.KbMach = txtMach.Text;

                objBenhAnNoiTru.KbNhietDo = txtNhietDo.Text;

                objBenhAnNoiTru.KbHuyetAp = txtHuyetApTu.Text;

                objBenhAnNoiTru.KbNhipTho = txtNhipTho.Text;

                objBenhAnNoiTru.KbCanNang = txtCanNang.Text;

                objBenhAnNoiTru.KbTuanHoan = txtBenhAnTuanHoan.Text;

                objBenhAnNoiTru.KbHoHap = txtBenhAnHoHap.Text;

                objBenhAnNoiTru.KbTieuHoa = txtBenhAnTieuHoa.Text;

                objBenhAnNoiTru.KbThan = txtBenhAnThanTietNieuSinhDuc.Text;

                objBenhAnNoiTru.KbThanKinh = txtBenhAnThanKinh.Text;

                objBenhAnNoiTru.KbCo = txtBenhAnCoXuongKhop.Text;

                objBenhAnNoiTru.KbTai = txtBenhAnTaiMuiHong.Text;

                objBenhAnNoiTru.KbRang = txtBenhAnRangHamMat.Text;

                objBenhAnNoiTru.KbMat = txtBenhAnMat.Text;

                objBenhAnNoiTru.KbNoiTiet = txtBenhAnNoiTiet.Text;

                objBenhAnNoiTru.KbXnCls = txtBenhAnCacXetNghiem.Text;

                objBenhAnNoiTru.KbTtba = txtBenhAnTomTatBenhAn.Text;

                objBenhAnNoiTru.KbBenhChinh = txtBenhAnBenhChinh.Text;

                objBenhAnNoiTru.KbBenhPhu = txtBenhAnBenhKemTheo.Text;

                objBenhAnNoiTru.KbPhanBiet = txtBenhAnPhanBiet.Text;

                objBenhAnNoiTru.KbTienLuong = txtBenhAnTienLuong.Text;

                objBenhAnNoiTru.KbHuongDtri = txtBenhAnHuongDieuTri.Text;

                objBenhAnNoiTru.TkbaQtbl = txtTKBAQuaTrinhBenhLy.Text;

                objBenhAnNoiTru.TkbaTtkqxn = txtTKBATTomTatKetQua.Text;

                objBenhAnNoiTru.TkbaPpdt = txtTKBAPhuongPhapDieuTri.Text;

                objBenhAnNoiTru.TkbaTtrv = txtTKBATinhTrangRaVien.Text;

                objBenhAnNoiTru.TkbaHdt = txtTKBAHuongDieuTri.Text;


                //if (chkYTe.Checked) objBenhAnNoiTru.YTe = 1;
                //else if (chkTuDen.Checked)
                //{
                //    objBenhAnNoiTru.YTe = 2;
                //}
                //else
                //{
                //    objBenhAnNoiTru.YTe = 3;
                //}
                //objBenhAnNoiTru.LdoVaovienMm = Utility.Int16Dbnull(chkldvvMetMoi.Checked ? 1 : 0);
                //objBenhAnNoiTru.LdoVaovienGsc = Utility.Int16Dbnull(chkldvvGay.Checked ? 1 : 0);
                //objBenhAnNoiTru.LdoVaovienKndn = Utility.Int16Dbnull(chkldvvKhat.Checked ? 1 : 0);
                //objBenhAnNoiTru.LdoVaovienGtl = Utility.Int16Dbnull(chkldvvGiamThiLuc.Checked ? 1 : 0);
                //objBenhAnNoiTru.LdoVaovienKhac = Utility.Int16Dbnull(chkldvvKhac.Checked ? 1 : 0);

                //objBenhAnNoiTru.HbNam = Utility.sDbnull(txtNamChanDoanDTD.Text);
                //objBenhAnNoiTru.HbNoiCdoan = Utility.sDbnull(txtNoiChanDoanDTD.Text);
                //if (chkhbDeu.Checked) objBenhAnNoiTru.HbDieuTri = 1;
                //else if (chkhbKhongDeu.Checked)
                //{
                //    objBenhAnNoiTru.HbDieuTri = 2;
                //}
                //else
                //{
                //    objBenhAnNoiTru.HbDieuTri = 3;
                //}
                //objBenhAnNoiTru.HbTiemInsulin = Utility.Int16Dbnull(chkhbTiemInsulin.Checked ? 1 : 0);
                //objBenhAnNoiTru.HbInsulin1 = Utility.sDbnull(txtLuongInsulin.Text);
                //objBenhAnNoiTru.HbInsulin2 = Utility.sDbnull(txthbCachDungInsulin.Text);
                //objBenhAnNoiTru.HbThuochaDuonghuyet = Utility.sDbnull(txthbThuocHaDuongHuyet.Text);
                //objBenhAnNoiTru.HbHa = Utility.sDbnull(txthbThuocHaHA.Text);
                //objBenhAnNoiTru.HbRllp = Utility.sDbnull(txthbThuocRLLP.Text);
                //objBenhAnNoiTru.HbThuocChongdong = Utility.sDbnull(txthbThuocChongDong.Text);
                //objBenhAnNoiTru.HtaiMm = Utility.Int16Dbnull(chkhbMetMoi.Checked ? 1 : 0);
                //objBenhAnNoiTru.HtaiGaysc = Utility.Int16Dbnull(chkhbGaySutCan.Checked ? 1 : 0);
                //objBenhAnNoiTru.HtaiKgDau = Utility.sDbnull(txthbKgDau.Text);
                //objBenhAnNoiTru.HtaiKgSau = Utility.sDbnull(txthbKgSau.Text);
                //objBenhAnNoiTru.HtaiKhatnhieu = Utility.Int16Dbnull(chkhbKhat.Checked ? 1 : 0);
                //objBenhAnNoiTru.HtaiUong = Utility.sDbnull(txthbSoLanUongNuoc.Text);
                //objBenhAnNoiTru.HtaiDainhieu = Utility.Int16Dbnull(chkhbDai.Checked ? 1 : 0);
                //objBenhAnNoiTru.HtaiDai = Utility.sDbnull(txthbSoLanDai.Text);
                //objBenhAnNoiTru.HtaiGiamtl = Utility.Int16Dbnull(chkhbGiamThiLuc.Checked ? 1 : 0);
                //objBenhAnNoiTru.HtaiKhac = Utility.sDbnull(chkhbKhac.Checked ? 1 : 0);
                //objBenhAnNoiTru.TsbBtChuaphathien = Utility.Int16Dbnull(chktsbChuaPhatHienBenh.Checked ? 1 : 0);
                //objBenhAnNoiTru.TsbBtNhoimaucotim = Utility.Int16Dbnull(chktsbNhoiMauCoTim.Checked ? 1 : 0);
                //objBenhAnNoiTru.TsbBtNamNmct = Utility.sDbnull(txttsbNamNhoiMauCoTim.Text);
                //objBenhAnNoiTru.TsbBtTbmn = Utility.Int16Dbnull(chktsbTBMN.Checked ? 1 : 0);
                //objBenhAnNoiTru.TsbBtNamTbmn = Utility.sDbnull(txttsbNamTBMN.Text);
                //objBenhAnNoiTru.TsbHaNam = Utility.sDbnull(txttsbTangHuyetAp.Text);
                //objBenhAnNoiTru.TsbHaMax = Utility.sDbnull(txttsbHAmax.Text);
                //objBenhAnNoiTru.TsbBtCON4000 = Utility.Int16Dbnull(chktsbDeConLonHon4000.Checked ? 1 : 0);
                //objBenhAnNoiTru.TsbBtKhac = Utility.sDbnull(txttsbKhac.Text);

                //objBenhAnNoiTru.TsgdDtd = Utility.Int16Dbnull(chktsbDTD.Checked ? 1 : 0);
                //objBenhAnNoiTru.TsgdTanghuyetap = Utility.Int16Dbnull(chktsbTangHuyetAp.Checked ? 1 : 0);
                //objBenhAnNoiTru.TsbBtNhoimaucotim = Utility.Int16Dbnull(chktsgdNhoiMauCoTim.Checked ? 1 : 0);
                //objBenhAnNoiTru.TsgdKhac = Utility.sDbnull(txttsgdKhac.Text);

                //if (chkkbMatNuocCo.Checked) objBenhAnNoiTru.KcbMatnuoc = 1;
                //else if (chkkbMatNuocKhong.Checked)
                //{
                //    objBenhAnNoiTru.KcbMatnuoc = 2;
                //}
                //else
                //{
                //    objBenhAnNoiTru.KcbMatnuoc = 3;
                //}
                //if (chkkbXuatHuetDuoiDaCo.Checked) objBenhAnNoiTru.KcbXuathuyet = 1;
                //else if (chkkbXuatHuyetDuoiDaKhong.Checked)
                //{
                //    objBenhAnNoiTru.KcbXuathuyet = 2;
                //}
                //else
                //{
                //    objBenhAnNoiTru.KcbXuathuyet = 3;
                //}
                //if (chkkbPhuCo.Checked) objBenhAnNoiTru.KcbPhu = 1;
                //else if (chkkbPhuKhong.Checked)
                //{
                //    objBenhAnNoiTru.KcbPhu = 2;
                //}
                //else
                //{
                //    objBenhAnNoiTru.KcbPhu = 3;
                //}

                //objBenhAnNoiTru.KcbToanthanKhac = Utility.sDbnull(txtkbKhac.Text);
                //objBenhAnNoiTru.KcbNhiptim = Utility.sDbnull(txtkbNhipTim.Text);
                //if (chkkbNhipTimDeu.Checked) objBenhAnNoiTru.KcbTthaiNhiptim = 1;
                //else if (chkkbNhipTimKhong.Checked)
                //{
                //    objBenhAnNoiTru.KcbTthaiNhiptim = 2;
                //}
                //else
                //{
                //    objBenhAnNoiTru.KcbTthaiNhiptim = 3;
                //}
                //objBenhAnNoiTru.KcbTiengtim = Utility.sDbnull(txtkbTiengTim.Text);
                //objBenhAnNoiTru.KcbHohap = Utility.sDbnull(txtkbHoHap.Text);
                //objBenhAnNoiTru.KcbBung = Utility.sDbnull(txtkbBung.Text);

                //if (chkkbChanPhaiPXGXGiam.Checked) objBenhAnNoiTru.KcbChanphai = 1;
                //else if (chkkbChanPhaiBinhThuong.Checked)
                //{
                //    objBenhAnNoiTru.KcbChanphai = 2;
                //}
                //else if (chkkbChanPhaiTang.Checked)
                //{
                //    objBenhAnNoiTru.KcbChanphai = 3;
                //}
                //else
                //{
                //    objBenhAnNoiTru.KcbChanphai = 0;
                //}
                //objBenhAnNoiTru.KcbChanphaiKhac = Utility.sDbnull(txtkbChanPhaiKhac.Text);
                //if (chkkbChanTraiPXGXGiam.Checked) objBenhAnNoiTru.KcbChantrai = 1;
                //else if (chkkbChanTraiBinhThuong.Checked)
                //{
                //    objBenhAnNoiTru.KcbChantrai = 2;
                //}
                //else if (chkkbChanTraiTang.Checked)
                //{
                //    objBenhAnNoiTru.KcbChantrai = 3;
                //}
                //else
                //{
                //    objBenhAnNoiTru.KcbChantrai = 0;
                //}
                //objBenhAnNoiTru.KcbChantraiKhac = Utility.sDbnull(txtkbChanTraiKhac.Text);
                //objBenhAnNoiTru.KcbMatphai = Utility.sDbnull(txtkbMatPhai.Text);
                //objBenhAnNoiTru.KcbMattrai = Utility.sDbnull(txtkbMatTrai.Text);
                //objBenhAnNoiTru.KcbRanghammat = Utility.sDbnull(txtkbRangHamMat.Text);
                //objBenhAnNoiTru.KcbTkinhKhac = Utility.sDbnull(txtkbKhac1.Text);
                //objBenhAnNoiTru.KcbMach = Utility.sDbnull(txtMach.Text);
                //objBenhAnNoiTru.KcbNhietdo = Utility.sDbnull(txtNhietDo.Text);
                //objBenhAnNoiTru.KcbHuyetap1 = Utility.sDbnull(txtHuyetApTu.Text);
                //objBenhAnNoiTru.KcbHuyetap2 = Utility.sDbnull(txtHuyetApDen.Text);
                //objBenhAnNoiTru.KcbNhiptho = Utility.sDbnull(txtNhipTho.Text);
                //objBenhAnNoiTru.KcbCannang = Utility.sDbnull(txtCanNang.Text);
                //objBenhAnNoiTru.KcbChieucao = Utility.sDbnull(txtChieuCao.Text);
                //objBenhAnNoiTru.KcbBmi = Utility.sDbnull(txtBMI.Text);
                //objBenhAnNoiTru.TrangThai = 0;


                //objBenhAnNoiTru.KcbTomtatClsChinh = Utility.sDbnull(txtkbTomTatKQCLSC.Text);
                //objBenhAnNoiTru.KcbCdoanBdau = Utility.sDbnull(TXTKBChanDoanBD.Text);
                //objBenhAnNoiTru.KcbDaXly = Utility.sDbnull(TXTKBDaXuLy.Text);
                //objBenhAnNoiTru.KcbCdoanRavien = Utility.sDbnull(txtkbChanDoanRaVien.Text);
                //objBenhAnNoiTru.KcbDtriTungay = Convert.ToDateTime(dtDieuTriNgoaiTruTu.Value);
                //objBenhAnNoiTru.KcbDtriDenngay = Convert.ToDateTime(dtDieuTriNgoaiTruDen.Value);
                //objBenhAnNoiTru.TkbaQtrinhBenhlyDbienCls = Utility.sDbnull(txtQuaTrinhBenhLy.Text);
                //objBenhAnNoiTru.TkbaPphapDtri = Utility.sDbnull(txtTomTatCLS.Text);
                //objBenhAnNoiTru.TenBenhChinh = Utility.sDbnull(txtBenhChinh.Text);
                //objBenhAnNoiTru.TenBenhPhu = Utility.sDbnull(txtBenhPhu.Text);
                //objBenhAnNoiTru.TkbaPphapDtri = Utility.sDbnull(txtPPDT.Text);
                //objBenhAnNoiTru.TkbaTtRavien = Utility.sDbnull(txtTrangThaiRaVien.Text);
                //objBenhAnNoiTru.TkbaHuongTtTieptheo = Utility.sDbnull(txtHuongTiepTheo.Text);
                //objBenhAnNoiTru.DoiTuong = Utility.sDbnull(txtDoiTuong.Text);
                //objBenhAnNoiTru.SoBhyt = Utility.sDbnull(txtSoBaoHiemYte.Text);
                //objBenhAnNoiTru.DiaChi = Utility.sDbnull(txtDiaChi.Text);
                //objBenhAnNoiTru.NamSinh = Utility.Int16Dbnull(txtNamSinh.Text);
                //objBenhAnNoiTru.InPhieuLog = Utility.Int16Dbnull(0);
                //objBenhAnNoiTru.LoaiBenhAn = Utility.Int16Dbnull(1);


                return objBenhAnNoiTru;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
                return objBenhAnNoiTru;
                //throw;
            }
        }


        private void InsertPatient()
        {
            try
            {
                SinhMaBenhAn();


                MaBenhAnNoiTru = txtMaBenhAn.Text;
                SqlQuery KT_SO_BENH_AN =
                    new Select().From(TBenhanNoitru.Schema).
                        Where(TBenhanNoitru.Columns.MaBaNoitru).IsEqualTo(Utility.sDbnull(txtMaBenhAn.Text));
                if (KT_SO_BENH_AN.GetRecordCount() > 0)
                {
                    MessageBox.Show("Mã bệnh đã được sử dụng, Kiểm tra lại?");
                }
                else
                {
                    TBenhanNoitru objBenhAnNoiTru = CreateBenhAnNoiTru();
                    objBenhAnNoiTru.IsNew = true;
                    objBenhAnNoiTru.Save();
                    Utility.SetMessage(lblMess, "Lưu Thành Công", true);
               
                    em_Action = action.Update;

                    
                    cmdInBenhAn.Enabled = true;
                    


                   
                }
            }
            catch (Exception)
            {
                throw;
            }
            //TBenhAnNgoaitru objBenhAnNoiTru = new CreateBenhAnNgoaiTru();
        }

        private void UpdatePatient()
        {
            try
            {
                TBenhanNoitru objBenhAnNoiTru = CreateBenhAnNoiTru();
                objBenhAnNoiTru.Save();
                Utility.SetMessage(lblMess, "Sửa thành công", true);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
            }
        }

        private void cmdInBenhAn_Click(object sender, EventArgs e)
        {

            DataTable dsTable = SPs.NoitietLayThongTinBenhAnCu(Utility.Int32Dbnull(PatientId),PatientCode).GetDataSet().Tables[0];
            DataTable sub_dtData = SPs.NoitietCacKhoaBnChuyen(PatientCode).GetDataSet().Tables[0];
            CrystalDecisions.CrystalReports.Engine.ReportDocument crpt;
            crpt = new CRPT_NOITIET_BENHAN_NOITRU();
            frmPrintPreview objForm = new frmPrintPreview("bệnh án", crpt, true, true);
            crpt.SetDataSource(dsTable);
            crpt.Subreports[0].SetDataSource(sub_dtData);
            objForm.crptViewer.ReportSource = crpt;
            objForm.ShowDialog();




            //TBenhanNoitru objBenhAnNoiTru = new TBenhAnNgoaitru();
            //objBenhAnNoiTru.IsLoaded = true;
            //objBenhAnNoiTru.MarkOld();
            //objBenhAnNoiTru.Id = Utility.Int32Dbnull(txti.Text, -1);
            //// objBenhAnNoiTru.InPhieuLog = Utility.Int16Dbnull(1)
            //objBenhAnNoiTru.NgayIn = BusinessHelper.GetSysDateTime();
            //objBenhAnNoiTru.NguoiIn = globalVariables.UserName;
            //objBenhAnNoiTru.Save();
        }

       

     

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            em_Action = action.Delete;
            PerformAction();
        }
        private void DeletePatient()
        {
            if (Utility.AcceptQuestion("Bạn có chắc chắn xóa BỆNH ÁN", "Thông Báo", true))
            {
               
                try
                {
                    new Delete().From(TBenhanNoitru.Schema).Where(TBenhanNoitru.Columns.MaBaNoitru).IsEqualTo(
                        Utility.sDbnull(txtMaBenhAn.Text)).Execute();
                    ClearControl();
                    em_Action = action.Insert;

                   
                }
                catch (Exception)
                {

                    throw;
                }


            }
          
        }

        private void chkQLNBCapCuu_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBCapCuu.Checked == true)
            {
                chkQLNBKKB.Checked = false;
                chkQLNBKhoaDieuTri.Checked = false;
                

            }
        }

        private void chkQLNBKKB_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBKKB.Checked == true)
            {
               
                chkQLNBKhoaDieuTri.Checked = false;
                chkQLNBCapCuu.Checked = false;

            }
        }

        private void chkQLNBKhoaDieuTri_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBKhoaDieuTri.Checked == true)
            {
                chkQLNBKKB.Checked = false;
                
                chkQLNBCapCuu.Checked = false;

            }
        }

        private void chkQLNBCoQuanYTe_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBCoQuanYTe.Checked == true)
            {
                chkQLNBTuDen.Checked = false;

                chkQLNBKhac.Checked = false;
               

            }
        }

        private void chkQLNBTuDen_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBTuDen.Checked == true)
            {
                

                chkQLNBKhac.Checked = false;
                chkQLNBCoQuanYTe.Checked = false;

            }
        }

        private void chkQLNBKhac_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBKhac.Checked == true)
            {
                chkQLNBTuDen.Checked = false;
                chkQLNBCoQuanYTe.Checked = false;

            }
        }

        private void chkQLNBTuyenTren_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBTuyenTren.Checked == true)
            {
                
                chkQLNBTuyenDuoi.Checked = false;
                chkQLNBCK.Checked = false;

            }
        }

        private void chkQLNBTuyenDuoi_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBTuyenDuoi.Checked == true)
            {
                chkQLNBTuyenTren.Checked = false;
               
                chkQLNBCK.Checked = false;

            }
        }

        private void chkQLNBCK_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBCK.Checked == true)
            {
                chkQLNBTuyenTren.Checked = false;
                chkQLNBTuyenDuoi.Checked = false;
               

            }
        }

        private void chkQLNBRaVien_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBRaVien.Checked == true)
            {
                
                chkQLNBXinVe.Checked = false;
                chkQLNBBoVe.Checked = false;
                chkQLNBDuaVe.Checked = false;

            }
        }

        private void chkQLNBXinVe_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBXinVe.Checked == true)
            {
                chkQLNBRaVien.Checked = false;
               
                chkQLNBBoVe.Checked = false;
                chkQLNBDuaVe.Checked = false;

            }
        }

        private void chkQLNBBoVe_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBBoVe.Checked == true)
            {
                chkQLNBRaVien.Checked = false;
                chkQLNBXinVe.Checked = false;
               
                chkQLNBDuaVe.Checked = false;

            }
        }

        private void chkQLNBDuaVe_CheckedChanged(object sender, EventArgs e)
        {
            if (chkQLNBDuaVe.Checked == true)
            {
                chkQLNBRaVien.Checked = false;
                chkQLNBXinVe.Checked = false;
                chkQLNBBoVe.Checked = false;
              

            }
        }

        private void chkTTRVKhoi_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTTRVKhoi.Checked == true)
            {
               
                chkTTRVDoGiam.Checked = false;
                chkTTRVKhongThayDoi.Checked = false;
                chkTTRVNangHon.Checked = false;
                chkTTRVTuVong.Checked = false;


            }
        }

        private void chkTTRVDoGiam_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTTRVDoGiam.Checked == true)
            {
                chkTTRVKhoi.Checked = false;
               
                chkTTRVKhongThayDoi.Checked = false;
                chkTTRVNangHon.Checked = false;
                chkTTRVTuVong.Checked = false;


            }
        }

        private void chkTTRVKhongThayDoi_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTTRVKhongThayDoi.Checked == true)
            {
                chkTTRVKhoi.Checked = false;
                chkTTRVDoGiam.Checked = false;
               
                chkTTRVNangHon.Checked = false;
                chkTTRVTuVong.Checked = false;


            }
        }

        private void chkTTRVNangHon_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTTRVNangHon.Checked == true)
            {
                chkTTRVKhoi.Checked = false;
                chkTTRVDoGiam.Checked = false;
                chkTTRVKhongThayDoi.Checked = false;
               
                chkTTRVTuVong.Checked = false;


            }
        }

        private void chkTTRVTuVong_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTTRVTuVong.Checked == true)
            {
                chkTTRVKhoi.Checked = false;
                chkTTRVDoGiam.Checked = false;
                chkTTRVKhongThayDoi.Checked = false;
                chkTTRVNangHon.Checked = false;
                


            }
        }

        private void chkTTRVLanhTinh_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTTRVLanhTinh.Checked == true)
            {
                
                chkTTRVNghiNgo.Checked = false;
                chkTTRVAcTinh.Checked = false;
                
            }
        }

        private void chkTTRVNghiNgo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTTRVNghiNgo.Checked == true)
            {
                chkTTRVLanhTinh.Checked = false;
               
                chkTTRVAcTinh.Checked = false;

            }
        }

        private void chkTTRVAcTinh_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTTRVAcTinh.Checked == true)
            {
                chkTTRVLanhTinh.Checked = false;
                chkTTRVNghiNgo.Checked = false;
                

            }
        }

        private void chkttrvDoBenh_CheckedChanged(object sender, EventArgs e)
        {
            if (chkttrvDoBenh.Checked == true)
            {
                
                chkttrvTrong24GioVaoVien.Checked = false;
                chkttrvDoTaiBien.Checked = false;
                chkttrvSau24Gio.Checked = false;
                chkttrvKhac.Checked = false;
            }
        }

        private void chkttrvTrong24GioVaoVien_CheckedChanged(object sender, EventArgs e)
        {
            if (chkttrvTrong24GioVaoVien.Checked == true)
            {
                chkttrvDoBenh.Checked = false;
               
                chkttrvDoTaiBien.Checked = false;
                chkttrvSau24Gio.Checked = false;
                chkttrvKhac.Checked = false;
            }
        }

        private void chkttrvDoTaiBien_CheckedChanged(object sender, EventArgs e)
        {
            if (chkttrvDoTaiBien.Checked == true)
            {
                chkttrvDoBenh.Checked = false;
                chkttrvTrong24GioVaoVien.Checked = false;
               
                chkttrvSau24Gio.Checked = false;
                chkttrvKhac.Checked = false;
            }
        }

        private void chkttrvSau24Gio_CheckedChanged(object sender, EventArgs e)
        {
            if (chkttrvSau24Gio.Checked == true)
            {
                chkttrvDoBenh.Checked = false;
                chkttrvTrong24GioVaoVien.Checked = false;
                chkttrvDoTaiBien.Checked = false;
               
                chkttrvKhac.Checked = false;
            }
        }

        private void chkttrvKhac_CheckedChanged(object sender, EventArgs e)
        {
            if (chkttrvKhac.Checked == true)
            {
                chkttrvDoBenh.Checked = false;
                chkttrvTrong24GioVaoVien.Checked = false;
                chkttrvDoTaiBien.Checked = false;
                chkttrvSau24Gio.Checked = false;
               
            }
        }

        private void frm_BENHAN_N0ITRU_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ProcessTabKey(true);
            if (e.Control && e.KeyCode == Keys.S) cmdSave.PerformClick();
            if (e.KeyCode == Keys.F4) cmdInBenhAn.PerformClick();
            if (e.KeyCode == Keys.Escape) Close();
            if (e.KeyCode == Keys.F1)
            {
                txtMaBN.SelectAll();
                txtMaBN.Focus();
            }
        }
    }
}