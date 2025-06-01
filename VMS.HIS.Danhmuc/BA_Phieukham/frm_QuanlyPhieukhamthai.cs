using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using VNS.HIS.UI.Baocao;
using VNS.HIS.UI.Forms.Noitru;
using VNS.Libs;
using VMS.HIS.DAL;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;
using SubSonic;
using VNS.HIS.UI.Forms.NGOAITRU;
using System.Transactions;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using VNS.HIS.UI.Classess;
using Aspose.Words;
using System.Diagnostics;
using System.Drawing;
namespace VMS.EMR.PHIEUKHAM
{
    public partial class frm_QuanlyPhieukhamthai : Form
    {
        private DataTable m_dtData=new DataTable();
        public TrangthaiNoitru TrangthaiNoitru = TrangthaiNoitru.NoiTru;
        DataTable _mDtKhoanoitru;
        long id_pkt = -1;
        public frm_QuanlyPhieukhamthai()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            dtToDate.Value = dtFromDate.Value =globalVariables.SysDate;
            Utility.VisiableGridEx(grdList,"ID",globalVariables.IsAdmin);
            InitEvents();
        }
        void InitEvents()
        {
           
            cmdExit.Click += cmdExit_Click;
            cmdTimKiem.Click += cmdTimKiem_Click;
            txtMaluotkham.KeyDown += txtPatientCode_KeyDown;
            chkByDate.CheckedChanged += chkByDate_CheckedChanged;
            Load += frm_QuanlyPhieukhamthai_Load;
            KeyDown += frm_QuanlyPhieukhamthai_KeyDown;
            grdList.MouseDoubleClick += grdList_MouseDoubleClick;
            grdList.SelectionChanged += grdList_SelectionChanged;
        }

        void grdList_SelectionChanged(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdList)) return;
            id_pkt = Utility.Int64Dbnull(grdList.GetValue("id_phieukhamthai"),-1);
        }

        void grdList_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            cmdUpdate.PerformClick();
        }
        /// <summary>
        /// hàm thực hiện việc thoát Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_QuanlyPhieukhamthai_Load(object sender, EventArgs e)
        {
            
            InitData();
            TimKiemThongTin();
            ModifyCommand();
            
        }
        /// <summary>
        /// hàm thực hiện việc lấy thông tin khoa nội trú
        /// </summary>
        private void InitData()
        {

        }
        
        /// <summary>
        /// hàm thực hiện việc tìm kiếm thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdTimKiem_Click(object sender, EventArgs e)
        {
            TimKiemThongTin();
        }
        private void ModifyCommand()
        {
            bool isValid = Utility.isValidGrid(grdList);
            cmdUpdate.Enabled = cmdDelete.Enabled =cmdPrint.Enabled= isValid;
           
        }

        private void TimKiemThongTin()
        {
            DateTime tungay = chkByDate.Checked ? dtFromDate.Value : new DateTime(1900, 1, 1);
            DateTime denngay = chkByDate.Checked ? dtToDate.Value : new DateTime(1900, 1, 1);
            string ma_luotkham=(Utility.DoTrim(txtMaluotkham.Text));
            string ten_benhnhan=(Utility.DoTrim(txtTennguoibenh.Text));
            if (ma_luotkham.Length > 0)
            {
                tungay = denngay = new DateTime(1900, 1, 1);
                ten_benhnhan = "";
            }
            m_dtData = SPs.KcbTimkiemdanhsachphieukhamthai( tungay, denngay,ma_luotkham,ten_benhnhan).GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdList, m_dtData, true, true, "1=1", "ngay_kham,ten_benhnhan");
            ModifyCommand();
        }

        /// <summary>
        /// hàm thực hiện trạng thái của tmf kiếm từ ngày đến ngày
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkByDate_CheckedChanged(object sender, EventArgs e)
        {
            dtToDate.Enabled = dtFromDate.Enabled = chkByDate.Checked;
        }
      
        /// <summary>
        /// hàm thưc hiện việc tìm kiếm htoong tin nhanh cho bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPatientCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                LoadMaLanKham();
                chkByDate.Checked = false;
                cmdTimKiem.PerformClick();
            }
        }
        private void LoadMaLanKham()
        {
            MaLuotkham = Utility.sDbnull(txtMaluotkham.Text.Trim());
            if (!string.IsNullOrEmpty(MaLuotkham) && txtMaluotkham.Text.Length < 8)
            {
                MaLuotkham = Utility.AutoFullPatientCode(txtMaluotkham.Text);
                txtMaluotkham.Text = MaLuotkham;
                txtMaluotkham.Select(txtMaluotkham.Text.Length, txtMaluotkham.Text.Length);
            }
         
        }
        /// <summary>
        /// hàm thực hiện việc phím tắt thông tin 
        /// </summary>
        private string MaLuotkham { get; set; }
        private void frm_QuanlyPhieukhamthai_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.F3)cmdTimKiem.PerformClick();
            if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
            if (e.KeyCode == Keys.F2)
            {
                txtMaluotkham.Focus();
                txtMaluotkham.SelectAll();
            }
            if(e.KeyCode==Keys.N&&e.Control)cmdInsert.PerformClick();
            if(e.KeyCode==Keys.U&&e.Control)cmdUpdate.PerformClick();
            if (e.KeyCode == Keys.D && e.Control) cmdDelete.PerformClick();
            if (e.KeyCode == Keys.P && e.Control) cmdPrint.PerformClick();
        }
     
      
       

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            frm_ThemPhieukhamthai _pkt = new frm_ThemPhieukhamthai();
            _pkt.m_enAct = action.Insert;
            _pkt.ucThongtinnguoibenh_doc_v61.txtMaluotkham.Focus();
            _pkt.ShowDialog();
            modifyCommandButtons();
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            frm_ThemPhieukhamthai _pkt = new frm_ThemPhieukhamthai();
            _pkt._pkt = KcbPhieukhamthai.FetchByID(Utility.Int64Dbnull(grdList.GetValue("id_phieukhamthai")));
            _pkt.ucThongtinnguoibenh_doc_v61.txtMaluotkham.Text = Utility.sDbnull(grdList.GetValue("ma_luotkham"));
            _pkt.ucThongtinnguoibenh_doc_v61.Refresh();
            _pkt.m_enAct = action.Update;
            _pkt.ShowDialog();
        }
        void modifyCommandButtons()
        {
            cmdDelete.Enabled = cmdUpdate.Enabled = cmdPrint.Enabled =  Utility.isValidGrid(grdList);
        }
        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Bạn cần chọn phiếu khám thai cần xóa");
                    return;
                }
                if (!Utility.Coquyen("kcb_phieukhamthai_xoa"))
                {
                    Utility.ShowMsg("Bạn không có quyền xóa phiếu khám thai");
                    return;
                }
                KcbLuotkham objLuotkham = Utility.getKcbLuotkham(Utility.Int64Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan)), grdList.GetValue(KcbLuotkham.Columns.MaLuotkham).ToString());
                if (objLuotkham == null)
                {
                    Utility.ShowMsg("Bạn cần chọn phiếu khám thai trên lưới trước khi thực hiện xóa phiếu khám thai");
                    return;
                }
                if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn xóa phiếu khám thai của người bệnh {0} hay không?", grdList.GetValue("ten_benhnhan").ToString()), "Xác nhận hủy chuyển viện", true))
                {
                    try
                    {
                        using (var scope = new TransactionScope())
                        {
                            using (var dbscope = new SharedDbConnectionScope())
                            {
                                new Delete().From(KcbPhieukhamthai.Schema).Where(KcbPhieukhamthai.Columns.IdPhieukhamthai).IsEqualTo(Utility.Int32Dbnull(grdList.GetValue(KcbPhieukhamthai.Columns.IdPhieukhamthai), -1)).Execute();
                                new Delete().From(KcbPhieukhamthaiTiensusankhoa.Schema).Where(KcbPhieukhamthaiTiensusankhoa.Columns.IdPhieukhamthai).IsEqualTo(Utility.Int32Dbnull(grdList.GetValue(KcbPhieukhamthai.Columns.IdPhieukhamthai), -1)).Execute();
                            }
                            scope.Complete();
                            Utility.ShowMsg(string.Format("Xóa phiếu khám thai cho người bệnh {0} thành công", grdList.GetValue("ten_benhnhan").ToString()));
                            DataRow[] arrDr = m_dtData.Select(string.Format("{0}={1}", KcbPhieukhamthai.Columns.IdPhieukhamthai, grdList.GetValue(KcbPhieukhamthai.Columns.IdPhieukhamthai)));
                            if (arrDr.Length > 0)
                                m_dtData.Rows.Remove(arrDr[0]);
                            m_dtData.AcceptChanges();

                        }
                    }
                    catch (Exception ex)
                    {
                        Utility.CatchException(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                modifyCommandButtons();
            }
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
           
            try
            {
                KcbPhieukhamthai _pkt = KcbPhieukhamthai.FetchByID(Utility.Int64Dbnull(grdList.GetValue("id_phieukhamthai")));
                if (_pkt == null || _pkt.IdPhieukhamthai <= 0)
                {
                    Utility.ShowMsg("Bạn cần tạo Phiếu khám thai trước khi thực hiện in");
                    return;
                }
                DataTable dtData = SPs.KcbLaythongtinPhieukhamthaiIn(_pkt.IdPhieukhamthai).GetDataSet().Tables[0];

                List<string> lstAddedFields = new List<string>()  {"doituong_thuphi","doituong_mienphi","doituong_khac", "toanthan_binhthuong", "toanthan_batthuong", "diung_khong",
                "diung_co", "tsb_khong", "tsb_co",
                "huyetap_khong","huyetap_co",  "tuyengiap_khong","tuyengiap_co","benhtim_khong","benhtim_co","benhthan_khong","benhthan_co","benhhohap_khong","benhhohap_co","benhdaithaoduong_khong"
                ,"benhdaithaoduong_co", "thuocdangdung_khong","thuocdangdung_co","tiensuphauthuat_khong","tiensuphauthuat_co","chukykinh_deu","chukykinh_khongdeu"
                ,"phauthuatphukhoa_khong","phauthuatphukhoa_co","khoiubuongtrung_khong","khoiubuongtrung_co","didangsinhduc_co","didangsinhduc_khong"
                ,"khoiutucung_co","khoiutucung_khong","tangsinhmon_khong","tangsinhmon_co","satangchau_khong","satangchau_co"
                ,"tsgd_khong","tsgd_co","tsgddathai_khong","tsgddathai_co","tsgddaithaoduong_khong","tsgddaithaoduong_co","tsgddidang_khong","tsgddidang_co"
                ,"tsgdtanghuyetap_khong","tsgdtanghuyetap_co","tsgdbenhditruyen_khong","tsgdbenhditruyen_co"
                ,"kcbtinhthan_tinhtao","kcbtinhthan_honme","kcbtinhthan_khac","kcbphu_khong","kcbphu_co","kcbproteinnieu_khong","kcbproteinnieu_co"
                ,"kcbseomocu_khong","kcbseomocu_co","kcbseomocu_dauvetmo","kcbkhungchau_binhthuong","kcbkhungchau_batthuong"
                ,"kcbngoithai_binhthuong","kcbngoithai_batthuong","kcbconcotucung_khong","kcbconcotucung_co","kcbtimthai_khong","kcbtimthai_co"
                ,"kcbcotucung_dong","kcbcotucung_xoa","kcbcotucung_mo","kcbdauoi_phong","kcbdauoi_det","kcbdauoi_quale","kcbdauoi_ri","kcbdauoi_vo"
                ,"kcbnuocoi_trong","kcbnuocoi_xanhban","kcbnuocoi_lanmau","xnmaungoaivi_thieumau","xnmaungoaivi_khong","xnmaungoaivi_co"
                ,"xndongmau_dongmau","xndongmau_binhthuong","xndongmau_batthuong"
                ,"xnsinhhoamau_duongmau","xnsinhhoamau_binhthuong","xnsinhhoamau_cao"
                ,"xnhvi_hiv","xnhvi_amtinh","xnhvi_duongtinh"
                ,"xnviemganb_viemganb","xnviemganb_amtinh","xnviemganb_duongtinh"
                ,"xmgiangmai_giangmai","xmgiangmai_amtinh","xmgiangmai_duongtinh"
                ,"xmprotein_proteinnieu","xmprotein_amtinh","xmprotein_duongtinh"
                ,"sieuam_tinhtrang","sieuam_binhthuong","sieuam_batthuong",
                "tuvan_khong","tuvan_co","tienluong_sinhthuong","tienluong_sinhconguyco","tienluong_chidinhmolaythai"
                ,"bacsisanphukhoa","hosinh","ysysannhi","nguoikham_khac"
                };
                Dictionary<string, string> dicMF = new Dictionary<string, string>();
                dicMF.Add("toanthan_binhthuong",Utility.Byte2Bool( _pkt.Toanthan) ? "0" : "1");
                dicMF.Add("toanthan_batthuong", Utility.Byte2Bool(_pkt.Toanthan) ? "1" : "0");
                dicMF.Add("diung_khong", Utility.Byte2Bool(_pkt.TsbDiung) ? "0" : "1");
                dicMF.Add("diung_co", Utility.Byte2Bool(_pkt.TsbDiung) ? "1" : "0");
                dicMF.Add("tsb_khong", Utility.Byte2Bool(_pkt.TsbTsb) ? "0" : "1");
                dicMF.Add("tsb_co", Utility.Byte2Bool(_pkt.TsbTsb) ? "1" : "0");
                dicMF.Add("huyetap_khong", Utility.Byte2Bool(_pkt.Ha)? "0" : "1");
                dicMF.Add("huyetap_co", Utility.Byte2Bool(_pkt.Ha) ? "1" : "0");
                dicMF.Add("tuyengiap_khong", Utility.Byte2Bool(_pkt.TsbBenhtuyengiap) ? "0" : "1");
                dicMF.Add("tuyengiap_co", Utility.Byte2Bool(_pkt.TsbBenhtuyengiap) ? "1" : "0");

                dicMF.Add("benhtim_khong",Utility.Byte2Bool(_pkt.TsbBenhtim) ? "0" : "1");
                dicMF.Add("benhtim_co", Utility.Byte2Bool(_pkt.TsbBenhtim) ? "1" : "0");
                dicMF.Add("benhthan_khong",Utility.Byte2Bool(_pkt.TsbBenhthan) ? "0" : "1");
                dicMF.Add("benhthan_co", Utility.Byte2Bool(_pkt.TsbBenhthan) ? "1" : "0");
                dicMF.Add("benhhohap_khong",Utility.Byte2Bool(_pkt.TsbBenhhohap) ? "0" : "1");
                dicMF.Add("benhhohap_co", Utility.Byte2Bool(_pkt.TsbBenhhohap)? "1" : "0");
                dicMF.Add("benhdaithaoduong_khong",Utility.Byte2Bool(_pkt.TsbBenhdaithaoduong) ? "0" : "1");
                dicMF.Add("benhdaithaoduong_co",Utility.Byte2Bool(_pkt.TsbBenhdaithaoduong) ? "1" : "0");
                dicMF.Add("thuocdangdung_khong",Utility.Byte2Bool(_pkt.TsbThuocdangdung) ? "0" : "1");
                dicMF.Add("thuocdangdung_co", Utility.Byte2Bool(_pkt.TsbThuocdangdung) ? "1" : "0");

                          

                dicMF.Add("tiensuphauthuat_khong", Utility.Byte2Bool(_pkt.TsbTiensuphauthuat)? "0" : "1");
                dicMF.Add("tiensuphauthuat_co",Utility.Byte2Bool(_pkt.TsbTiensuphauthuat) ? "1" : "0");
                dicMF.Add("chukykinh_deu", Utility.Byte2Bool(_pkt.TsbChukykinh) ? "0" : "1");
                dicMF.Add("chukykinh_khongdeu", Utility.Byte2Bool(_pkt.TsbChukykinh)? "1" : "0");
                dicMF.Add("phauthuatphukhoa_khong",Utility.Byte2Bool(_pkt.TsbPhauthuatphukhoa) ? "0" : "1");
                dicMF.Add("phauthuatphukhoa_co",Utility.Byte2Bool(_pkt.TsbPhauthuatphukhoa) ? "1" : "0");
                dicMF.Add("khoiubuongtrung_khong", Utility.Byte2Bool( _pkt.TsbKhoiubuongtrung.Value) ? "0" : "1");
                dicMF.Add("khoiubuongtrung_co", Utility.Byte2Bool(_pkt.TsbKhoiubuongtrung) ? "1" : "0");
                dicMF.Add("didangsinhduc_co",Utility.Byte2Bool(_pkt.TsbDidangsinhduc) ? "1" : "0");
                dicMF.Add("didangsinhduc_khong", Utility.Byte2Bool(_pkt.TsbDidangsinhduc) ? "0" : "1");

                dicMF.Add("khoiutucung_co", Utility.Byte2Bool(_pkt.TsbKhoiutucung)  ? "1" : "0");
                dicMF.Add("khoiutucung_khong", Utility.Byte2Bool(_pkt.TsbKhoiutucung) ? "0" : "1");
                dicMF.Add("tangsinhmon_khong", Utility.Byte2Bool(_pkt.TsbTangsinhmon)  ? "0" : "1");
                dicMF.Add("tangsinhmon_co", Utility.Byte2Bool(_pkt.TsbTangsinhmon) ? "1" : "0");
                dicMF.Add("satangchau_khong", Utility.Byte2Bool(_pkt.TsbSatangchau)  ? "0" : "1");
                dicMF.Add("satangchau_co", Utility.Byte2Bool(_pkt.TsbSatangchau) ? "1" : "0");
                dicMF.Add("tsgd_khong",Utility.Byte2Bool(_pkt.Tsgd)  ? "0" : "1");
                dicMF.Add("tsgd_co", Utility.Byte2Bool(_pkt.Tsgd) ? "1" : "0");
                dicMF.Add("tsgddathai_khong", Utility.Byte2Bool(_pkt.TsgdDathai)  ? "0" : "1");
                dicMF.Add("tsgddathai_co", Utility.Byte2Bool(_pkt.TsgdDathai) ? "1" : "0");

             
                 dicMF.Add("tsgddaithaoduong_khong", Utility.Byte2Bool(_pkt.TsgdDaithaoduong)  ? "0" : "1");
                 dicMF.Add("tsgddaithaoduong_co", Utility.Byte2Bool(_pkt.TsgdDaithaoduong) ? "1" : "0");
                dicMF.Add("tsgddidang_khong", Utility.Byte2Bool(_pkt.TsgdDidang)  ? "0" : "1");
                dicMF.Add("tsgddidang_co", Utility.Byte2Bool(_pkt.TsgdDidang) ? "1" : "0");
                dicMF.Add("tsgdtanghuyetap_khong", Utility.Byte2Bool(_pkt.TsgdTanghuyetap)  ? "0" : "1");
                dicMF.Add("tsgdtanghuyetap_co", Utility.Byte2Bool(_pkt.TsgdTanghuyetap) ? "1" : "0");
                dicMF.Add("tsgdbenhditruyen_khong", Utility.Byte2Bool(_pkt.TsgdBenhditruyen) ? "0" : "1");
                dicMF.Add("tsgdbenhditruyen_co", Utility.Byte2Bool(_pkt.TsgdBenhditruyen) ? "1" : "0");
                dicMF.Add("kcbtinhthan_tinhtao", Utility.ByteDbnull(_pkt.KcbTinhthan)==0 ? "1" : "0");
                dicMF.Add("kcbtinhthan_honme", Utility.ByteDbnull(_pkt.KcbTinhthan) == 1 ? "1" : "0");

                dicMF.Add("kcbtinhthan_khac", Utility.ByteDbnull(_pkt.KcbTinhthan) == 2 ? "1" : "0");
                dicMF.Add("kcbphu_khong", Utility.Byte2Bool(_pkt.KcbPhu) ? "0" : "1");
                dicMF.Add("kcbphu_co", Utility.Byte2Bool(_pkt.KcbPhu) ? "1" : "0");
                dicMF.Add("kcbproteinnieu_khong", Utility.Byte2Bool(_pkt.KcbProteinnieu) ? "0" : "1");
                dicMF.Add("kcbproteinnieu_co", Utility.Byte2Bool(_pkt.KcbProteinnieu) ? "1" : "0");
                dicMF.Add("kcbseomocu_khong", Utility.ByteDbnull(_pkt.KcbSeomocu) == 0 ? "1" : "0");
                dicMF.Add("kcbseomocu_co", Utility.ByteDbnull(_pkt.KcbSeomocu) == 1 ? "1" : "0");
                dicMF.Add("kcbseomocu_dauvetmo", Utility.ByteDbnull(_pkt.KcbSeomocu) == 2 ? "1" : "0");
                dicMF.Add("kcbkhungchau_batthuong", Utility.Byte2Bool(_pkt.KcbKhungchau) ? "1" : "0");
                dicMF.Add("kcbkhungchau_binhthuong", Utility.Byte2Bool(_pkt.KcbKhungchau) ? "0" : "1");
                dicMF.Add("kcbngoithai_binhthuong", Utility.Byte2Bool(_pkt.KcbNgoithai) ? "0" : "1");
                dicMF.Add("kcbngoithai_batthuong", Utility.Byte2Bool(_pkt.KcbNgoithai) ? "1" : "0");
                dicMF.Add("kcbconcotucung_khong", Utility.Byte2Bool(_pkt.KcbConcotucung) ? "0" : "1");
                dicMF.Add("kcbconcotucung_co", Utility.Byte2Bool(_pkt.KcbConcotucung) ? "1" : "0");
                dicMF.Add("kcbcotucung_dong", Utility.ByteDbnull(_pkt.KcbTimthai)==0 ? "1" : "0");
                dicMF.Add("kcbcotucung_mo", Utility.ByteDbnull(_pkt.KcbTimthai) ==2? "1" : "0");
                dicMF.Add("kcbcotucung_xoa", Utility.ByteDbnull(_pkt.KcbConcotucung) == 1 ? "0" : "1");
                dicMF.Add("kcbtimthai_khong", Utility.Byte2Bool(_pkt.KcbTimthai) ? "0" : "1");
                dicMF.Add("kcbtimthai_co", Utility.Byte2Bool(_pkt.KcbConcotucung) ? "1" : "0");

                
                dicMF.Add("kcbdauoi_phong", Utility.ByteDbnull(_pkt.KcbDauoi) == 0 ? "1" : "0");
                dicMF.Add("kcbdauoi_det", Utility.ByteDbnull(_pkt.KcbDauoi) == 1 ? "1" : "0");
                dicMF.Add("kcbdauoi_quale", Utility.ByteDbnull(_pkt.KcbDauoi) == 2 ? "1" : "0");

                dicMF.Add("kcbdauoi_ri", Utility.ByteDbnull(_pkt.KcbDauoi) == 3 ? "1" : "0");
                dicMF.Add("kcbdauoi_vo", Utility.ByteDbnull(_pkt.KcbDauoi) == 4 ? "1" : "0");
                dicMF.Add("kcbnuocoi_trong", Utility.ByteDbnull(_pkt.KcbNuocoi) == 0 ? "1" : "0");
                dicMF.Add("kcbnuocoi_xanhban", Utility.ByteDbnull(_pkt.KcbNuocoi) == 1 ? "1" : "0");
                dicMF.Add("kcbnuocoi_lanmau", Utility.ByteDbnull(_pkt.KcbNuocoi) == 2 ? "1" : "0");
                dicMF.Add("xnmaungoaivi_thieumau", Utility.Byte2Bool(_pkt.ClsXnmaungoaivi) ? "1" : "0");
                dicMF.Add("xnmaungoaivi_khong", Utility.Byte2Bool(_pkt.ClsXnmaungoaiviKq) ? "0" : "1");
                dicMF.Add("xnmaungoaivi_co", Utility.Byte2Bool(_pkt.ClsXnmaungoaiviKq) ? "1" : "0");
               
                dicMF.Add("xndongmau_dongmau", Utility.Byte2Bool(_pkt.ClsXndongmau) ? "1" : "0");
                dicMF.Add("xndongmau_binhthuong", Utility.Byte2Bool(_pkt.ClsXndongmauKq) ? "0" : "1");
                dicMF.Add("xndongmau_batthuong", Utility.Byte2Bool(_pkt.ClsXndongmauKq) ? "1" : "0");

                dicMF.Add("xnsinhhoamau_duongmau", Utility.Byte2Bool(_pkt.ClsXnsinhoa) ? "1" : "0");
                dicMF.Add("xnsinhhoamau_binhthuong", Utility.Byte2Bool(_pkt.ClsXnsinhoaKq) ? "0" : "1");
                dicMF.Add("xnsinhhoamau_cao", Utility.Byte2Bool(_pkt.ClsXnsinhoaKq) ? "1" : "0");
               
                dicMF.Add("xnhvi_hiv", Utility.Byte2Bool(_pkt.ClsXnHIV) ? "1" : "0");
                dicMF.Add("xnhvi_amtinh", Utility.Byte2Bool(_pkt.ClsXnHIVKq) ? "0" : "1");
                dicMF.Add("xnhvi_duongtinh", Utility.Byte2Bool(_pkt.ClsXnHIVKq) ? "1" : "0");
               
                dicMF.Add("xnviemganb_viemganb", Utility.Byte2Bool(_pkt.ClsViemganB) ? "1" : "0");
                dicMF.Add("xnviemganb_amtinh", Utility.Byte2Bool(_pkt.ClsViemganBKq) ? "0" : "1");
                dicMF.Add("xnviemganb_duongtinh", Utility.Byte2Bool(_pkt.ClsViemganBKq) ? "1" : "0");
                
                dicMF.Add("xmgiangmai_giangmai", Utility.Byte2Bool(_pkt.ClsGiangmai) ? "1" : "0");
                dicMF.Add("xmgiangmai_amtinh", Utility.Byte2Bool(_pkt.ClsGiangmaiKq) ? "0" : "1");
                dicMF.Add("xmgiangmai_duongtinh", Utility.Byte2Bool(_pkt.ClsGiangmaiKq) ? "1" : "0");
                
                dicMF.Add("xmprotein_proteinnieu", Utility.Byte2Bool(_pkt.ClsProteinnuoctieu) ? "1" : "0");
                dicMF.Add("xmprotein_amtinh", Utility.Byte2Bool(_pkt.ClsProteinnuoctieukq) ? "0" : "1");
                dicMF.Add("xmprotein_duongtinh", Utility.Byte2Bool(_pkt.ClsProteinnuoctieukq) ? "1" : "0");
                
                dicMF.Add("sieuam_tinhtrang", Utility.Byte2Bool(_pkt.ClsSieuam) ? "1" : "0");
                dicMF.Add("sieuam_binhthuong", Utility.Byte2Bool(_pkt.ClsSieuamKq) ? "0" : "1");
                dicMF.Add("sieuam_batthuong", Utility.Byte2Bool(_pkt.ClsSieuamKq) ? "1" : "0");
                
                dicMF.Add("tuvan_khong", Utility.Byte2Bool(_pkt.Tuvan) ? "0" : "1");
                dicMF.Add("tuvan_co", Utility.Byte2Bool(_pkt.Tuvan) ? "1" : "0");
                dicMF.Add("tienluong_sinhthuong", Utility.ByteDbnull(_pkt.TienLuong) == 0 ? "1" : "0");
                dicMF.Add("tienluong_sinhconguyco", Utility.ByteDbnull(_pkt.TienLuong) == 1 ? "1" : "0");
                dicMF.Add("tienluong_chidinhmolaythai", Utility.ByteDbnull(_pkt.TienLuong) == 2 ? "1" : "0");
                dicMF.Add("bacsisanphukhoa", Utility.ByteDbnull(_pkt.LoaiNhanvien) == 0 ? "1" : "0");
                dicMF.Add("hosinh", Utility.ByteDbnull(_pkt.LoaiNhanvien) == 1 ? "1" : "0");
                dicMF.Add("ysysannhi", Utility.ByteDbnull(_pkt.LoaiNhanvien) == 2 ? "1" : "0");
                dicMF.Add("nguoikham_khac", Utility.ByteDbnull(_pkt.LoaiNhanvien) == 3 ? "1" : "0");
                dicMF.Add("doituong_thuphi", "1");
                dicMF.Add("doituong_mienphi",  "0");
                dicMF.Add("doituong_khac", "0");


                dtData.TableName = "kcb_phieukhamthai";
                DataTable dtMergeField = dtData.Clone();
                Utility.AddColums2DataTable(ref dtMergeField, lstAddedFields, typeof(string));
                Document doc;
                DataRow drData = dtData.Rows[0];
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["ten_SYT"] = globalVariables.ParentBranch_Name;
                drData["ten_benhvien"] = globalVariables.Branch_Name;
                drData["diahchi_benhvien"] = globalVariables.Branch_Address;
                drData["SDT_bv"] = globalVariables.Branch_Phone;
                drData["Hotline_bv"] = globalVariables.Branch_Hotline;
                drData["Fax_bv"] = globalVariables.Branch_Fax;
                drData["website_bv"] = globalVariables.Branch_Website;
                drData["email_bv"] = globalVariables.Branch_Email;
                drData["ten_phieu"] = "Phiếu khám thai";
                drData["sngay_kham_full"] = Utility.FormatDateTime_giophut_ngay_thang_nam(_pkt.NgayKham, "");
                drData["sngay_kham"] = Utility.FormatDateTime(_pkt.NgayKham.Value);
                drData["sNgaykykinh_cuoi"] =_pkt.NgayDaukykinhcuoi.HasValue?_pkt.NgayDaukykinhcuoi.Value.ToString("dd/MM/yyyy"):"";
                drData["sngaydukien_sinh"] = _pkt.NgayDukiensinh.HasValue ? _pkt.NgayDukiensinh.Value.ToString("dd/MM/yyyy") : "";
                drData["sngay_kham"] = Utility.FormatDateTime(_pkt.NgayKham.Value);
                drData["ngay_in"] = Utility.FormatDateTime(DateTime.Now);
                
                List<string> fieldNames = new List<string>();

                string PathDoc = AppDomain.CurrentDomain.BaseDirectory + "Doc\\PHIEUKHAMTHAI.doc";
                string writePathdoc = AppDomain.CurrentDomain.BaseDirectory + "tempDoc\\";
                if (!Directory.Exists(writePathdoc)) Directory.CreateDirectory(writePathdoc);
                string mergeFields = AppDomain.CurrentDomain.BaseDirectory + "MergeFields\\";
                if (!Directory.Exists(mergeFields)) Directory.CreateDirectory(mergeFields);
                Utility.CreateMergeFields(dtMergeField);
                if (!File.Exists(PathDoc))
                {
                    string tieude = "";
                    Utility.GetReport("PHIEUKHAMTHAI", ref tieude, ref PathDoc);
                }
                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg("Không tìm thấy file mẫu in phiếu Phiếu khám thai tại thư mục sau :" + PathDoc);
                    return;
                }


                if (!File.Exists(PathDoc))
                {
                    Utility.ShowMsg(string.Format("Không tìm thấy File {0}", PathDoc), "Thông báo không tìm thấy File",
                      MessageBoxIcon.Warning);
                    return;
                }
                SysSystemParameter sysLogosize = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("logosize").ExecuteSingle<SysSystemParameter>();

                string fileKetqua = string.Format("{0}{1}{2}{3}{4}_{5}_{6}_{7}",
                               Path.GetDirectoryName(writePathdoc), Path.DirectorySeparatorChar,
                               Path.GetFileNameWithoutExtension(PathDoc), "PHIEUKHAMTHAI", Utility.sDbnull(grdList.GetValue("ma_luotkham")), Utility.sDbnull(_pkt.IdPhieukhamthai), Guid.NewGuid().ToString(), Path.GetExtension(PathDoc));


                if ((drData != null) && File.Exists(PathDoc))
                {
                    doc = new Document(PathDoc);
                    DocumentBuilder builder = new DocumentBuilder(doc);
                    if (doc == null)
                    {
                        Utility.ShowMsg("Không nạp được file word.", "Thông báo"); return;
                    }
                    if (builder.MoveToMergeField("logo") && globalVariables.SysLogo != null)
                        if (sysLogosize != null)
                        {
                            int w = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[0], 0);
                            int h = Utility.Int32Dbnull(sysLogosize.SValue.Split('x')[1], 0);
                            if (w > 0 && h > 0)
                                builder.InsertImage(globalVariables.SysLogo, w, h);
                            else
                                builder.InsertImage(globalVariables.SysLogo);
                        }
                        else
                            if (globalVariables.SysLogo != null)
                                builder.InsertImage(globalVariables.SysLogo);
                    Utility.MergeFieldsCheckBox2Doc(builder, dicMF, null, drData);
                    //Nạp tiền sử sản khoa. Nhảy đến bảng số 3 trong file doc mẫu
                    Aspose.Words.Tables.Table tab = doc.FirstSection.Body.Tables[3];
                    int idx = 1;
                    DataTable dtTiensusankhoa=new Select(KcbPhieukhamthaiTiensusankhoa.Schema.Name + ".*", "'' as guid").From(KcbPhieukhamthaiTiensusankhoa.Schema).Where(KcbPhieukhamthaiTiensusankhoa.Columns.IdPhieukhamthai).IsEqualTo(_pkt.IdPhieukhamthai).ExecuteDataSet().Tables[0];
                    foreach (DataRow dr in dtTiensusankhoa.Rows)
                    {
                        Aspose.Words.Tables.Row newRow = (Aspose.Words.Tables.Row)tab.LastRow.Clone(true);
                        newRow.RowFormat.Borders.Shadow = false;
                        newRow.Cells[0].CellFormat.Shading.BackgroundPatternColor = Color.White;
                        newRow.Cells[1].CellFormat.Shading.BackgroundPatternColor = Color.White;
                        newRow.Cells[2].CellFormat.Shading.BackgroundPatternColor = Color.White;
                        newRow.Cells[3].CellFormat.Shading.BackgroundPatternColor = Color.White;
                        newRow.Cells[4].CellFormat.Shading.BackgroundPatternColor = Color.White;
                        newRow.Cells[5].CellFormat.Shading.BackgroundPatternColor = Color.White;

                        newRow.Cells[0].FirstParagraph.Runs.Clear();
                        newRow.Cells[1].FirstParagraph.Runs.Clear();
                        newRow.Cells[2].FirstParagraph.Runs.Clear();
                        newRow.Cells[3].FirstParagraph.Runs.Clear();
                        newRow.Cells[4].FirstParagraph.Runs.Clear();
                        newRow.Cells[5].FirstParagraph.Runs.Clear();
                        Run r = new Run(doc);
                        r.Font.Name = "Arial";
                        r.Font.Size = 9d;
                        r.Font.Bold = false;
                        r.Font.Color = Color.FromArgb(102, 0, 102);
                        r.Text = Utility.sDbnull(dr["thoigian_noi_ketthuc"], "");
                        newRow.Cells[0].FirstParagraph.AppendChild(r);
                        newRow.Cells[0].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Left;

                        r = new Run(doc);
                        r.Font.Name = "Arial";
                        r.Font.Bold = false;
                        r.Font.Size = 9d;
                        r.Font.Color = Color.FromArgb(102, 0, 102);
                        r.Text = Utility.sDbnull(dr["tuoi_thai"], "");
                        newRow.Cells[1].FirstParagraph.AppendChild(r);
                        newRow.Cells[1].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Left;

                        r = new Run(doc);
                        r.Font.Name = "Arial";
                        r.Font.Bold = false;
                        r.Font.Size = 9d;
                        r.Font.Color = Color.FromArgb(102, 0, 102);
                        r.Text = Utility.sDbnull(dr["dien_bien"],"");
                        newRow.Cells[2].FirstParagraph.AppendChild(r);
                        newRow.Cells[2].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Left;
                        
                        r = new Run(doc);
                        r.Font.Name = "Arial";
                        r.Font.Bold = false;
                        r.Font.Size = 9d;
                        r.Font.Color = Color.FromArgb(102, 0, 102);
                        r.Text = Utility.sDbnull(dr["cach_sinh"], "");
                        newRow.Cells[3].FirstParagraph.AppendChild(r);
                        newRow.Cells[3].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Left;

                        r = new Run(doc);
                        r.Font.Name = "Arial";
                        r.Font.Bold = false;
                        r.Font.Size = 9d;
                        r.Font.Color = Color.FromArgb(102, 0, 102);
                        r.Text = Utility.sDbnull(dr["mota_tresosinh"], "");
                        newRow.Cells[4].FirstParagraph.AppendChild(r);
                        newRow.Cells[4].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Left;

                        r = new Run(doc);
                        r.Font.Name = "Arial";
                        r.Font.Size = 9d;
                        r.Font.Bold = false;
                        r.Font.Color = Color.FromArgb(102, 0, 102);
                        r.Text = Utility.sDbnull(dr["hau_san"], "");
                        newRow.Cells[5].FirstParagraph.AppendChild(r);
                        newRow.Cells[5].FirstParagraph.ParagraphFormat.Alignment = ParagraphAlignment.Left;

                        tab.AppendChild(newRow);
                        idx += 1;
                    }

                    //Các hàm MoveToMergeField cần thực hiện trước dòng MailMerge.Execute bên dưới
                    doc.MailMerge.Execute(drData);

                    if (File.Exists(fileKetqua))
                    {
                        File.Delete(fileKetqua);
                    }
                    doc.Save(fileKetqua, SaveFormat.Doc);
                    string path = fileKetqua;

                    if (File.Exists(path))
                    {
                        Process process = new Process();
                        try
                        {
                            process.StartInfo.FileName = path;
                            process.Start();
                            process.WaitForInputIdle();
                        }
                        catch
                        {
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Không tìm thấy biểu mẫu", "TThông báo", MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
           
        
        }

        private void lnkDelete_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dtpNgayin.Value = dtToDate.Value = DateTime.Now;
            txtMaluotkham.Clear();
            txtTennguoibenh.Clear();

        }

       
     
    }
}
