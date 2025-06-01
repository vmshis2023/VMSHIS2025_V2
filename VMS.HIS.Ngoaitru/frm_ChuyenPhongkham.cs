using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SubSonic;
using VNS.HIS.BusRule.Classes;
using VMS.HIS.DAL;
using VNS.HIS.UI.Classess;
using VNS.HIS.UI.DANHMUC;
using VNS.Libs;
using VNS.Properties;

namespace VNS.HIS.UI.Forms.NGOAITRU
{
    public partial class frm_ChuyenPhongkham : Form
    {
        private readonly KCB_THAMKHAM _KCB_THAMKHAM = new KCB_THAMKHAM();
        private readonly KCB_DANGKY _kcbDangky = new KCB_DANGKY();
        public string MA_DTUONG = "DV";
        public DmucDichvukcb _DmucDichvukcb = null;
        private DmucDoituongkcb _objDoituongKcb;
        private KcbLuotkham objLuotkham = new KcbLuotkham();
        public KcbDanhsachBenhnhan objBenhnhan = new KcbDanhsachBenhnhan();
        public decimal dongia = -1;
        private DataTable m_dtDanhsachDichvuKCB;
        public bool m_blnCancel = true;
        private DataTable m_dtDangkyPhongkham = new DataTable();
        public KcbDangkyKcb objCongkham_Cu = null;
        int id_congkham = -1;
        public frm_ChuyenPhongkham(bool chuyenPK)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            if (chuyenPK) optChange.Checked = true;
            else optThem.Checked = true;
            InitEvents();
        }

        private void InitEvents()
        {
            Load += Frm_ChuyenPhongkham_Load;
            KeyDown += frm_ChuyenPhongkham_KeyDown;
            cmdClose.Click += cmdClose_Click;
            txtLydo._OnShowData += txtLydo__OnShowData;
            txtLydo._OnSaveAs += txtLydo__OnSaveAs;
            optThem.CheckedChanged += optThem_CheckedChanged;
            optChange.CheckedChanged += optThem_CheckedChanged;
            autoComplete_Congkham._OnGridSelectionChanged += AutoComplete_Congkham__OnGridSelectionChanged;
        }

        private void Frm_ChuyenPhongkham_Load(object sender, EventArgs e)
        {
            try
            {

                objLuotkham = KcbLuotkham.FetchByID(objCongkham_Cu.MaLuotkham);
                _objDoituongKcb =
                  new Select().From(DmucDoituongkcb.Schema)
                      .Where(DmucDoituongkcb.MaDoituongKcbColumn)
                      .IsEqualTo(objCongkham_Cu.MaDoituongkcb)
                      .ExecuteSingle<DmucDoituongkcb>();
                objCongkham_Cu = KcbDangkyKcb.FetchByID(objCongkham_Cu.IdKham);//Lấy lại để load đúng các phòng khám tương đương để chuyển khi bên ngoài bấm thanh toán và  khi bác sĩ chưa chọn l bệnh nhân trên lưới thăm khám
                m_dtDangkyPhongkham = _kcbDangky.LayDsachCongkhamDadangki(objCongkham_Cu.MaLuotkham,
                    Utility.Int64Dbnull(objCongkham_Cu.IdBenhnhan), 0);
                txtLydo.Init();
                lblLydo.Enabled = txtLydo.Enabled = cmdChuyen.Enabled = optChange.Checked;
                cmdDangkyKCB.Enabled = optThem.Checked;
                RefreshKCB();
                autoComplete_Congkham.SelectAll();
                autoComplete_Congkham.Focus();
              
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void AutoComplete_Congkham__OnGridSelectionChanged(int id_congkham, string ma_congkham, string ten_congkham, string ten_phongkham)
        {
            objDichvuKcb = DmucDichvukcb.FetchByID(id_congkham);
            if (objDichvuKcb != null)
                txtPKMoi.Text = ten_phongkham;
            else
                txtPKMoi.Text = "";
        }

        void optThem_CheckedChanged(object sender, EventArgs e)
        {
            lblLydo.Enabled = txtLydo.Enabled =cmdChuyen.Enabled= optChange.Checked;
            cmdDangkyKCB.Enabled = optThem.Checked;
            RefreshKCB();
            if (objDichvuKcb != null)
                autoComplete_Congkham.SetId(objDichvuKcb.IdDichvukcb);
            else
                autoComplete_Congkham.SetId(-1);
            autoComplete_Congkham.SelectAll();
            autoComplete_Congkham.Focus();
           
        }

        private void txtLydo__OnSaveAs()
        {
            if (Utility.DoTrim(txtLydo.Text) == "") return;
            var _DMUC_DCHUNG = new DMUC_DCHUNG(txtLydo.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtLydo.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtLydo.myCode;
                txtLydo.Init();
                txtLydo.SetCode(oldCode);
                txtLydo.Focus();
            }
        }

        private void txtLydo__OnShowData()
        {
            var _DMUC_DCHUNG = new DMUC_DCHUNG(txtLydo.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtLydo.myCode;
                txtLydo.Init();
                txtLydo.SetCode(oldCode);
                txtLydo.Focus();
            }
        }

        private bool IsvalidDoiPhong()
        {
            //SqlQuery sqlQuery = new Select().From(KcbDonthuoc.Schema)
            //    .Where(KcbDonthuoc.Columns.IdKham).IsEqualTo(objCongkham_Cu.IdKham);
            //if (sqlQuery.GetRecordCount() > 0)
            //{
            //    Utility.ShowMsg("Bác sĩ đã kê đơn thuốc cho lần khám này nên không thể đổi phòng. Đề nghị hủy đơn thuốc trước khi đổi phòng khám", "");
            //    return false;
            //}
            //sqlQuery = new Select().From(KcbChidinhcl.Schema)
            //    .Where(KcbChidinhcl.Columns.IdKham).IsEqualTo(objCongkham_Cu.IdKham);
            //if (sqlQuery.GetRecordCount() > 0)
            //{
            //    Utility.ShowMsg("Bác sĩ đã chỉ định cận lâm sàng cho lần khám này nên không thể đổi phòng. Đề nghị hủy chỉ định cận lâm sàng trước khi đổi phòng khám", "");
            //    return false;
            //}
            return true;
        }
        private void cmdClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdChuyen_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.SetMsg(lblMsg, "", true);

                if (Utility.Int32Dbnull( autoComplete_Congkham.MyID)  <= 0)
                {
                    Utility.SetMsg(lblMsg, "Bạn cần chọn công khám mới cần chuyển đổi", true);
                    autoComplete_Congkham.Focus();
                    autoComplete_Congkham.SelectAll();
                    return;
                }
                if (Utility.Int32Dbnull(objDichvuKcb.IdPhongkham) == objCongkham_Cu.IdPhongkham)
                {
                    Utility.SetMsg(lblMsg, "Bạn phải chọn phòng khám khác ", true);
                    autoComplete_Congkham.Focus();
                    autoComplete_Congkham.SelectAll();
                    return;
                }
                if (Utility.DoTrim(txtLydo.Text) == "")
                {
                    Utility.SetMsg(lblMsg, "Bạn cần nhập lý do chuyển công khám", true);
                    txtLydo.Focus();
                    return;
                }
                
                if (optThem.Checked)
                {
                    KcbDangkyKcb objCongkham = TaoDangkyKcb();
                    long idKham = _kcbDangky.AddRegExam(objCongkham, objLuotkham, false, -1);
                    if (idKham > 0)
                    {
                        lblMsg.Text = @"Thêm phòng khám thành công";
                        InphieuKham();
                        cmdChuyen.Enabled = false;
                        Utility.Log(Name, globalVariables.UserName,
                            string.Format("Bác sĩ thêm công khám mới {0} cho người bệnh id={1}{2}, {3}",
                                objCongkham.TenDichvuKcb, objBenhnhan.IdBenhnhan, objCongkham_Cu.MaLuotkham, objBenhnhan.TenBenhnhan), newaction.Insert, this.GetType().Assembly.ManifestModule.Name);
                    }
                }
                else
                {
                    //if (!IsvalidDoiPhong())
                    //{
                    //    return;
                    //}
                    if (
                        ChuyenPhongkham.ChuyenPhong(objCongkham_Cu, Utility.DoTrim(txtLydo.Text), objDichvuKcb) ==
                        ActionResult.Success)
                    {
                        lblMsg.Text = @"Đổi phòng khám thành công";
                        InphieuKham();
                        cmdChuyen.Enabled = false;
                        Utility.Log(Name, globalVariables.UserName,
                            string.Format("Đổi công khám cho bệnh nhân {0},{1} từ công khám {2} sang công khám {3}",
                                objCongkham_Cu.MaLuotkham, objBenhnhan.TenBenhnhan, txtCongkhamCu.Text, objDichvuKcb.TenDichvukcb), newaction.Move, this.GetType().Assembly.ManifestModule.Name);
                       
                        m_blnCancel = false;
                        Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi" + ex.Message);
                //throw;
            }
        }

        private void InMauChuyenKhoa(string maLuotkham, long idBenhnhan)
        {
            InphieuKham();
            return;
            try
            {
                DataTable _dtInphieu = _KCB_THAMKHAM.InMauPhieuChuyenKhoa(maLuotkham, idBenhnhan).Tables[0];
                THU_VIEN_CHUNG.CreateXML(_dtInphieu, "thamkham_phieukham_chuyenkhoa.xml");
                string reportcode = "";
                if (optThem.Checked)
                {
                    reportcode = "PHIEUKHAM_CHUYENKHOA";
                }
                else
                {
                    reportcode = "PHIEUKHAM_BENHPHAM";
                }
                KcbInphieu.INMAU_CHUYENKHAM_CHUYENKHOA(_dtInphieu, "PHIẾU KHÁM CHUYÊN KHOA", reportcode, txtLydo.Text);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }
        KCB_QMS _KCB_QMS = new KCB_QMS();
        private void InphieuKham()
        {
            int IdKham = id_congkham;
            KcbDangkyKcb objCongkham = KcbDangkyKcb.FetchByID(id_congkham);
            if (objCongkham != null)
            {
                new Update(KcbDangkyKcb.Schema)
                    .Set(KcbDangkyKcb.Columns.TrangthaiIn).EqualTo(1)
                    .Set(KcbDangkyKcb.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                    .Set(KcbDangkyKcb.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                    .Where(KcbDangkyKcb.Columns.IdKham).IsEqualTo(objCongkham.IdKham).Execute();
                
                DataTable v_dtData = new KCB_DANGKY().LayThongtinInphieuKCB(IdKham);
                THU_VIEN_CHUNG.CreateXML(v_dtData, Application.StartupPath + @"\Xml4Reports\PhieuKCB.XML");
                Utility.CreateBarcodeData(ref v_dtData,objLuotkham.MaLuotkham);
                if (v_dtData.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy bản ghi nào", "Thông báo");
                    return;
                }
                objLuotkham = Utility.getKcbLuotkham(objCongkham.IdBenhnhan, objCongkham.MaLuotkham);
                if (objLuotkham != null)
                    KcbInphieu.INPHIEU_KHAM(Utility.sDbnull(objLuotkham.MaDoituongKcb), v_dtData,
                                                  "PHIẾU KHÁM BỆNH", PropertyLib._MayInProperties.CoGiayInPhieuKCB == Papersize.A5 ? "A5" : "A4");

                //Thêm thông tin QMS cho phòng khám
                if ( objLuotkham != null && objCongkham != null)
                    _KCB_QMS.QmsPhongkhamInsert((int)objCongkham.SttKham.Value, objCongkham.MaPhongStt, DateTime.Now, objCongkham.NgayTiepdon.Value, objCongkham.MaLuotkham, objBenhnhan.IdBenhnhan, objBenhnhan.TenBenhnhan, (int)objBenhnhan.NamSinh,(DateTime.Now.Year- objBenhnhan.NamSinh.Value)+1, objBenhnhan.GioiTinh, objCongkham.MaKhoaThuchien, (int)objCongkham.IdPhongkham, objCongkham.IdKham, (int)objCongkham.IdDichvuKcb, objCongkham.TenDichvuKcb);
            }
        }
        private KcbDangkyKcb TaoDangkyKcb()
        {
            bool b_HasKham = false;
            EnumerableRowCollection<DataRow> query = from phong in m_dtDangkyPhongkham.AsEnumerable().Cast<DataRow>()
                where
                    Utility.Int32Dbnull(phong[KcbDangkyKcb.Columns.IdDichvuKcb],
                        -100) ==
                    Utility.Int32Dbnull(autoComplete_Congkham.MyID, -1)
                select phong;
            if (query.Count() > 0)
            {
                Utility.ShowMsg("Bệnh nhân đang đăng ký công khám này. Đề nghị chọn công khám khác");
                b_HasKham = true;
            }
            else
            {
                b_HasKham = false;
            }

            if (!b_HasKham)
            {
                var objCongkham = new KcbDangkyKcb();
                int min = 1;
                int max = 255;
                //List<int> levels = m_dtDangkyPhongkham.AsEnumerable().Select(al => al.Field<int>(KcbDangkyKcb.Columns.SttTt37)).Distinct().ToList();
                // min = levels.Min();
                // max = levels.Max();
                ////Hoặc
                min = Convert.ToInt32(m_dtDangkyPhongkham.AsEnumerable()
                       .Min(row => row[KcbDangkyKcb.Columns.SttTt37]));
                max = Convert.ToInt32(m_dtDangkyPhongkham.AsEnumerable()
                       .Max(row => row[KcbDangkyKcb.Columns.SttTt37]));
                // KcbQm objQMS = KcbQm.FetchByID(IdQMS);
                //DmucDichvukcb objDichvuKcb =
                //    new Select().From(DmucDichvukcb.Schema)
                //        .Where(DmucDichvukcb.Columns.IdPhongkham)
                //        .IsEqualTo(Utility.Int32Dbnull(txtPhongkham.MyID))
                //        .And(DmucDichvukcb.Columns.MaDoituongKcb)
                //        .IsEqualTo(objLuotkham.MaDoituongKcb)
                //        .ExecuteSingle<DmucDichvukcb>();
                DmucDichvukcb objDichvuKcb = DmucDichvukcb.FetchByID(Utility.Int32Dbnull(autoComplete_Congkham.MyID));
                _objDoituongKcb =
                    new Select().From(DmucDoituongkcb.Schema)
                        .Where(DmucDoituongkcb.MaDoituongKcbColumn)
                        .IsEqualTo(objCongkham_Cu.MaDoituongkcb)
                        .ExecuteSingle<DmucDoituongkcb>();
                if (objDichvuKcb != null)
                {
                   var objdepartment =
                    new Select().From(DmucKhoaphong.Schema).Where(DmucKhoaphong.IdKhoaphongColumn).IsEqualTo(
                        Utility.Int16Dbnull(objDichvuKcb.IdKhoaphong, -1)).ExecuteSingle<DmucKhoaphong>();
               
                    objCongkham.IdDichvuKcb = Utility.Int16Dbnull(objDichvuKcb.IdDichvukcb, -1);
                    objCongkham.IdKieukham = objDichvuKcb.IdKieukham;
                    objCongkham.NhomBaocao = objDichvuKcb.NhomBaocao;
                    objCongkham.DonGia = Utility.DecimaltoDbnull(objDichvuKcb.DonGia, 0);
                    if (min > 1)
                        objCongkham.SttTt37 = Utility.ByteDbnull(min - 1);
                    else
                        objCongkham.SttTt37 = Utility.ByteDbnull(max + 1);
                    objCongkham.TyleTt = THU_VIEN_CHUNG.Bhyt_Laytyle_tt_congkham(THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb), objCongkham.SttTt37);
                    objCongkham.MadoituongGia = objCongkham_Cu.MaDoituongkcb;
                    objCongkham.NguoiTao = globalVariables.UserName;
                    objCongkham.LaPhidichvukemtheo = 0;
                    objCongkham.SttKham = -1;
                    objCongkham.IdKhoakcb = objDichvuKcb.IdKhoaphong;
                    objCongkham.SttKham =
                        THU_VIEN_CHUNG.LaySothutuKCB(Utility.Int32Dbnull(objDichvuKcb.IdPhongkham, -1));
                    objCongkham.IdCha = -1;
                    if (objdepartment != null)
                    {
                     
                        objCongkham.MaPhongStt = objdepartment.MaPhongStt;
                    }
                    if (_objDoituongKcb != null)
                    {
                        objCongkham.IdLoaidoituongkcb = _objDoituongKcb.IdLoaidoituongKcb;
                        objCongkham.MaDoituongkcb = _objDoituongKcb.MaDoituongKcb;
                        objCongkham.IdDoituongkcb = _objDoituongKcb.IdDoituongKcb;
                    }
                    if (Utility.Int16Dbnull(objDichvuKcb.IdPhongkham, -1) > -1)
                        objCongkham.IdPhongkham = Utility.Int16Dbnull(objDichvuKcb.IdPhongkham, -1);
                    else
                        objCongkham.IdPhongkham = Utility.Int16Dbnull(objDichvuKcb.IdPhongkham, -1);

                    objCongkham.PhuThu = objLuotkham.DungTuyen == 1
                        ? Utility.DecimaltoDbnull(objDichvuKcb.PhuthuDungtuyen)
                        : Utility.DecimaltoDbnull(objDichvuKcb.PhuthuTraituyen);

                    if (!THU_VIEN_CHUNG.IsBaoHiem(objCongkham.IdLoaidoituongkcb))
                        objCongkham.PhuThu = 0;
                    objCongkham.NgayDangky = globalVariables.SysDate;
                    objCongkham.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan, -1);
                    objCongkham.TrangthaiThanhtoan = 0;
                    objCongkham.TrangthaiHuy = 0;
                    objCongkham.Noitru = 0;
                    objCongkham.TrangthaiIn = 0;
                    objCongkham.IpMaytao = globalVariables.gv_strIPAddress;
                    objCongkham.TenMaytao = globalVariables.gv_strComputerName;

                    objCongkham.TuTuc = Utility.ByteDbnull(objDichvuKcb.TuTuc, 0);
                    if (objLuotkham.DungTuyen == 0 && THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_TRAITUYENNGOAITRU_GIADICHVU", false) == "1")
                        objCongkham.TuTuc = 1;
                    objCongkham.MaKhoaThuchien = objdepartment.MaKhoaphong;// globalVariables.MA_KHOA_THIEN;
                    objCongkham.TenDichvuKcb = Utility.sDbnull(objDichvuKcb.TenDichvukcb);
                    objCongkham.NgayTiepdon = globalVariables.SysDate;
                    objCongkham.MaLuotkham = Utility.sDbnull(objCongkham_Cu.MaLuotkham, "");
                    //Bỏ đi do sinh lại ở mục business
                    if (THU_VIEN_CHUNG.IsNgoaiGio())
                    {
                        objCongkham.KhamNgoaigio = 1;
                        objCongkham.DonGia = Utility.DecimaltoDbnull(objDichvuKcb.DongiaNgoaigio, 0);
                        objCongkham.PhuThu = objLuotkham.DungTuyen == 1
                            ? Utility.DecimaltoDbnull(objDichvuKcb.PhuthuNgoaigio, 0)
                            : Utility.DecimaltoDbnull(objDichvuKcb.PhuthuDungtuyen);
                    }
                    else
                    {
                        objCongkham.KhamNgoaigio = 0;
                    }
                }
                else
                {
                    objCongkham = null;
                }
                //if (THU_VIEN_CHUNG.Laygiatrithamsohethong("TIEPDON_TINHGIAKHAM_THONGTU37", "0", false) == "1")
                //{
                //    THU_VIEN_CHUNG.TinhlaiGiaChiphiKcb(m_dtDangkyPhongkham, ref objCongkham);
                //}
                return objCongkham;
            }


            return null;
        }

        private void frm_ChuyenPhongkham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                cmdClose_Click(cmdClose, new EventArgs());
            else if (e.Control &&(e.KeyCode == Keys.S || e.KeyCode==Keys.A) ) cmdChuyen.PerformClick();
            else if (e.Control && (e.KeyCode == Keys.T || e.KeyCode == Keys.N)) cmdDangkyKCB.PerformClick();
            else if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
        }

        private DataTable _mDtDanhsachDichvuKcb = new DataTable();
       
        void RefreshKCB()
        {
            m_dtDanhsachDichvuKCB = THU_VIEN_CHUNG.LayDsach_Dvu_KCB(MA_DTUONG, "ALL", optThem.Checked ? -1 : objCongkham_Cu.TrangthaiThanhtoan > 0 ? objCongkham_Cu.DonGia : -1,-1);
            DataRow[] arrDr = null;
            if (m_dtDanhsachDichvuKCB == null) return;
            if (!m_dtDanhsachDichvuKCB.Columns.Contains("ShortCut"))
                m_dtDanhsachDichvuKCB.Columns.Add(new DataColumn("ShortCut", typeof(string)));
            arrDr = m_dtDanhsachDichvuKCB.Select(DmucDoituongkcb.Columns.MaDoituongKcb + "='ALL' OR " + DmucDoituongkcb.Columns.MaDoituongKcb + "='" + MA_DTUONG + "'");
            if (arrDr.Length <= 0)
            {
                this.autoComplete_Congkham.AutoCompleteList = new List<string>();
                return;
            }
            autoComplete_Congkham.dtData = m_dtDanhsachDichvuKCB;
            autoComplete_Congkham.ChangeDataSource();
        }

        private void cmdInPhieukhamchuyenkhoa_Click(object sender, EventArgs e)
        {
            try
            {
                InMauChuyenKhoa(Utility.sDbnull(objCongkham_Cu.MaLuotkham, ""),
                    Utility.Int64Dbnull(objCongkham_Cu.IdBenhnhan, -1));
                m_blnCancel = true;
                Close();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi: " + ex.Message);
            }
        }

        private void cmdDangkyKCB_Click(object sender, EventArgs e)
        {
            if (Utility.Int32Dbnull(autoComplete_Congkham.MyID) <= 0)
            {
                Utility.ShowMsg("Bạn phải chọn công khám mới", "Thông báo", MessageBoxIcon.Warning);
                return;
            }
            objDichvuKcb = DmucDichvukcb.FetchByID(Utility.Int32Dbnull(autoComplete_Congkham.MyID));
           
            
            if (objDichvuKcb != null)
            {
                //if (chkThutienkhamsau.Checked && !Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn thêm công khám mới {0} và thanh toán tiền khám sau hay không?", objDichvuKcb.TenDichvukcb), "Xác nhận thanh toán công khám sau", true))
                //    return;
                //Utility.SetMsg(lblDonGia, Utility.sDbnull(objDichvuKcb.DonGia), true);
                //Utility.SetMsg(lblPhuThu, Utility.sDbnull(Utility.Int32Dbnull(objLuotkham.DungTuyen.Value, 0) == 1 ? objDichvuKcb.PhuthuDungtuyen : objDichvuKcb.PhuthuTraituyen), true);
                if ((m_dtDangkyPhongkham.Select(KcbDangkyKcb.Columns.IdPhongkham + "=" + objDichvuKcb.IdPhongkham + "").GetLength(0) <= 0))
                {
                    ProcessData();
                }
                else
                {
                    if (Utility.AcceptQuestion("Bệnh nhân đã được đăng ký khám chữa bệnh tại phòng khám này. Bạn có muốn tiếp tục đăng ký dịch vụ KCB mới vừa chọn hay không?", "Thông báo", true))
                    {
                        ProcessData();
                    }
                }
            }
        }
        public DmucDichvukcb objDichvuKcb = null;
        void ProcessData()
        {
            long v_RegId = -1;
            if (objLuotkham == null) objLuotkham = new Select().From(KcbLuotkham.Schema)
                .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objCongkham_Cu.MaLuotkham)
                .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(objCongkham_Cu.IdBenhnhan).ExecuteSingle<KcbLuotkham>();
            if (objLuotkham != null)
            {
                 KcbDangkyKcb objCongkham = TaoCongKham();
                if (objCongkham != null)
                {
                    objCongkham.TinhChiphi = 1;
                    objCongkham.MaLuotkham = objLuotkham.MaLuotkham;
                    objCongkham.IdBenhnhan = objLuotkham.IdBenhnhan;

                    ActionResult actionResult = _kcbDangky.InsertRegExam(objCongkham, objLuotkham, ref v_RegId, Utility.Int32Dbnull(autoComplete_Congkham.MyID));
                    if (actionResult == ActionResult.Success)
                    {
                        id_congkham = Utility.Int32Dbnull(v_RegId);
                        Utility.ShowMsg(string.Format("Bạn đã đăng ký phòng khám cho người bệnh thành công. Vui lòng hướng dẫn người bệnh sang phòng khám mới để tiếp tục Khám chữa bệnh"), "Thông báo", MessageBoxIcon.Information);
                        InphieuKham();
                        this.Close();
                        return;
                    }
                    if (actionResult == ActionResult.Error)
                    {
                        Utility.ShowMsg("Lỗi trong quá trình cập nhập thông tin ", "Thông báo lỗi", MessageBoxIcon.Error);
                        return;
                    }
                }
            }
            else
            {
                Utility.ShowMsg(string.Format("Không tìm được thông tin người bệnh dựa vào Id người bệnh={0} và Mã lượt khám={1}. Vui lòng liên hệ để được trợ giúp", objCongkham_Cu.IdBenhnhan, objCongkham_Cu.MaLuotkham), "Thông báo lỗi", MessageBoxIcon.Error);
            }
        }
        private KcbDangkyKcb TaoCongKham()
        {
            bool b_HasKham = false;
            var query = from phong in m_dtDangkyPhongkham.AsEnumerable().Cast<DataRow>()
                        where
                            Utility.Int32Dbnull(phong[KcbDangkyKcb.Columns.IdDichvuKcb], -100) ==
                            Utility.Int32Dbnull(autoComplete_Congkham.MyID, -1)
                        select phong;
            if (query.Count() > 0)
            {
                Utility.ShowMsg("Bệnh nhân đã đăng ký công khám này. Đề nghị bạn xem lại");
                b_HasKham = true;
            }
            else
            {
                b_HasKham = false;
            }

            if (!b_HasKham)
            {
                m_dtDangkyPhongkham = _kcbDangky.LayDsachCongkhamDadangki(objCongkham_Cu.MaLuotkham, objCongkham_Cu.IdBenhnhan, 0);
                int min = 1;
                int max = 255;
                //List<int> levels = m_dtDangkyPhongkham.AsEnumerable().Select(al => al.Field<int>(KcbDangkyKcb.Columns.SttTt37)).Distinct().ToList();
                // min = levels.Min();
                // max = levels.Max();
                ////Hoặc
                min = Convert.ToInt32(m_dtDangkyPhongkham.AsEnumerable()
                       .Min(row => row[KcbDangkyKcb.Columns.SttTt37]));
                max = Convert.ToInt32(m_dtDangkyPhongkham.AsEnumerable()
                       .Max(row => row[KcbDangkyKcb.Columns.SttTt37]));
                KcbDangkyKcb objCongkham = new KcbDangkyKcb();
                DmucDoituongkcb objDoituongKCB = new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.MaDoituongKcbColumn).IsEqualTo(MA_DTUONG).ExecuteSingle<DmucDoituongkcb>();
                if (objDichvuKcb != null)
                {

                    DmucKhoaphong objdepartment = new Select().From(DmucKhoaphong.Schema).Where(DmucKhoaphong.IdKhoaphongColumn).IsEqualTo(objDichvuKcb.IdPhongkham).ExecuteSingle<DmucKhoaphong>();

                    objCongkham.IdDichvuKcb = Utility.Int16Dbnull(objDichvuKcb.IdDichvukcb, -1);
                    objCongkham.IdKieukham = objDichvuKcb.IdKieukham;
                    objCongkham.NhomBaocao = objDichvuKcb.NhomBaocao;
                    //Thêm công khám tại phòng khám sẽ không tính tiền khám
                    objCongkham.DonGia = 0;//  THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb) ? Utility.DecimaltoDbnull(objDichvuKcb.DonGia, 0) : Utility.DecimaltoDbnull(objDichvuKcb.DonGia, 0) * (1 + Utility.DecimaltoDbnull(_objDoituongKcb.MotaThem, 0) / 100);
                    if (min > 1)
                        objCongkham.SttTt37 = Utility.ByteDbnull(min - 1);
                    else
                        objCongkham.SttTt37 = Utility.ByteDbnull(max + 1);
                    objCongkham.TyleTt = THU_VIEN_CHUNG.Bhyt_Laytyle_tt_congkham(THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb), objCongkham.SttTt37);
                    objCongkham.MadoituongGia = MA_DTUONG;
                    objCongkham.NguoiTao = globalVariables.UserName;
                    objCongkham.LaPhidichvukemtheo = 0;
                    //Nếu khám thị lực thì STT công khám chính sinh khi kết thúc khám thị lực
                    objCongkham.SttKham = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_COKHAM_DOTHILUC", "0", false) == "1" ? (Int16)0 : THU_VIEN_CHUNG.LaySothutuKCB(Utility.Int32Dbnull(objDichvuKcb.IdPhongkham, -1));
                    objCongkham.IdCha = -1;
                    objCongkham.IdKhoakcb = objDichvuKcb.IdKhoaphong;
                    objCongkham.KhamThiluc = 0;
                    if (objdepartment != null)
                    {

                        objCongkham.MaPhongStt = objdepartment.MaPhongStt;
                    }
                    objdepartment = new Select().From(DmucKhoaphong.Schema).Where(DmucKhoaphong.IdKhoaphongColumn).IsEqualTo(objDichvuKcb.IdKhoaphong).ExecuteSingle<DmucKhoaphong>();
                    if (_objDoituongKcb != null)
                    {
                        objCongkham.IdLoaidoituongkcb = _objDoituongKcb.IdLoaidoituongKcb;
                        objCongkham.MaDoituongkcb = _objDoituongKcb.MaDoituongKcb;
                        objCongkham.IdDoituongkcb = _objDoituongKcb.IdDoituongKcb;
                    }
                    if (Utility.Int16Dbnull(objDichvuKcb.IdPhongkham, -1) > -1)
                        objCongkham.IdPhongkham = Utility.Int16Dbnull(objDichvuKcb.IdPhongkham, -1);

                    objCongkham.PhuThu = Utility.Byte2Bool( objLuotkham.DungTuyen)
                                                    ? Utility.DecimaltoDbnull(objDichvuKcb.PhuthuDungtuyen)
                                                    : Utility.DecimaltoDbnull(objDichvuKcb.PhuthuTraituyen);

                    if (!THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb))
                        objCongkham.PhuThu = 0;
                    objCongkham.NgayDangky = globalVariables.SysDate;
                    objCongkham.IdBenhnhan = objLuotkham.IdBenhnhan;
                    objCongkham.TrangthaiThanhtoan = 0;
                    objCongkham.TrangthaiHuy = 0;
                    objCongkham.Noitru = 0;
                    objCongkham.TrangthaiIn = 0;
                    objCongkham.MaCoso = globalVariables.Ma_Coso;
                    objCongkham.IpMaytao = globalVariables.gv_strIPAddress;
                    objCongkham.TenMaytao = globalVariables.gv_strComputerName;
                    objCongkham.ThanhtoanCongkhamsau = Utility.Bool2byte(chkThutienkhamsau.Checked);
                    objCongkham.TuTuc = !THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb) ? (byte)0 : Utility.ByteDbnull(objDichvuKcb.TuTuc, 0);//Đối tượng dịch vụ thì tự túc luôn =0
                    if (THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb) && !Utility.Byte2Bool(objLuotkham.DungTuyen) && THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_TRAITUYENNGOAITRU_GIADICHVU", false) == "1")
                        objCongkham.TuTuc = 1;
                    objCongkham.MaKhoaThuchien = objdepartment.MaKhoaphong;
                    objCongkham.TenDichvuKcb = autoComplete_Congkham.MyText;
                    objCongkham.NgayTiepdon = globalVariables.SysDate;
                    objCongkham.MaLuotkham = objLuotkham.MaLuotkham;
                    //Bỏ đi do sinh lại ở mục business
                    if (THU_VIEN_CHUNG.IsNgoaiGio() && !THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb))
                    {
                        objCongkham.KhamNgoaigio = 1;
                        objCongkham.DonGia = Utility.DecimaltoDbnull(objDichvuKcb.DongiaNgoaigio, 0);
                        objCongkham.PhuThu = !Utility.Byte2Bool(objLuotkham.DungTuyen) ? Utility.DecimaltoDbnull(objDichvuKcb.PhuthuNgoaigio, 0) : Utility.DecimaltoDbnull(objDichvuKcb.PhuthuDungtuyen);
                    }
                    else
                    {
                        objCongkham.KhamNgoaigio = 0;
                    }
                    objCongkham.TinhChiphi = 1;
                    objCongkham.CapKinh = objDichvuKcb.CapKinh;
                }
                else
                {
                    objCongkham = null;
                }
                //if (THU_VIEN_CHUNG.Laygiatrithamsohethong("TIEPDON_TINHGIAKHAM_THONGTU37", "0", false) == "1" && objLuotkham.IdLoaidoituongKcb==0)//0= BHYT;1= DV
                //{
                //    m_dtDangkyPhongkham = _kcbDangky.LayDsachCongkhamDadangki(objCongkham_Cu.MaLuotkham, objCongkham_Cu.IdBenhnhan,0);
                //    THU_VIEN_CHUNG.TinhlaiGiaChiphiKcb(m_dtDangkyPhongkham, ref objCongkham);
                //}
                return objCongkham;
            }
            return null;
        }

      
    }
}