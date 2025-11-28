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
using VMS.HIS.Bus.Emr;
using VMS.HIS.UI.EMR;
using VMS.HIS.Bus;

namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_Quanlyfrm_BangKiemChuanBiVaBanGiaoNguoiBenhTruocPhauThuat : Form
    {
        private DataTable m_dtData=new DataTable();
        public TrangthaiNoitru TrangthaiNoitru = TrangthaiNoitru.NoiTru;
        DataTable _mDtKhoanoitru;
       
        public frm_Quanlyfrm_BangKiemChuanBiVaBanGiaoNguoiBenhTruocPhauThuat()
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
            Load += frm_Quanlyfrm_BangKiemChuanBiVaBanGiaoNguoiBenhTruocPhauThuat_Load;
            KeyDown += frm_Quanlyfrm_BangKiemChuanBiVaBanGiaoNguoiBenhTruocPhauThuat_KeyDown;
            grdList.MouseDoubleClick += GrdList_MouseDoubleClick;
        }

        private void GrdList_MouseDoubleClick(object sender, MouseEventArgs e)
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

        private void frm_Quanlyfrm_BangKiemChuanBiVaBanGiaoNguoiBenhTruocPhauThuat_Load(object sender, EventArgs e)
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
            dtpNgayin.Value = dtFromDate.Value = dtToDate.Value = globalVariables.SysDate;
            DataBinding.BindDataCombobox(cbo_nguoi_giao, globalVariables.gv_dtDmucNhanvien, DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.TenNhanvien, "----Chọn----", true);
            DataBinding.BindDataCombobox(cbo_nguoi_nhan, globalVariables.gv_dtDmucNhanvien, DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.TenNhanvien, "----Chọn----", true);
            DataTable dtKhoaPhong = THU_VIEN_CHUNG.Laydanhmuckhoa("ALL", 0);
            DataBinding.BindDataCombobox(cbo_khoa_nhan, dtKhoaPhong, DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong, "----Chọn----", true);
            DataBinding.BindDataCombobox(cbo_khoa_giao, dtKhoaPhong, DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong, "----Chọn----", true);
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
            cmdUpdate.Enabled = cmdDelete.Enabled =cmdPrint.Enabled=cmd_KySo.Enabled= isValid;
        }

        private void TimKiemThongTin()
        {
            DateTime tungay=chkByDate.Checked ? dtFromDate.Value.Date : new DateTime(1900,1,1);
            DateTime denngay =chkByDate.Checked ? dtToDate.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59) : new DateTime(1900, 1, 1);
            string ma_luotkham=(Utility.DoTrim(txtMaluotkham.Text));
            string ten_benhnhan=(Utility.DoTrim(txtTennguoibenh.Text));
            int id_khoa_giao = Utility.Int32Dbnull(cbo_khoa_giao.SelectedValue);
            int id_khoa_nhan = Utility.Int32Dbnull(cbo_khoa_nhan.SelectedValue);
            int id_nguoi_giao = Utility.Int32Dbnull(cbo_nguoi_giao.SelectedValue);
            int id_nguoi_nhan = Utility.Int32Dbnull(cbo_nguoi_nhan.SelectedValue);
            if (ma_luotkham.Length > 0)
            {
                tungay= new DateTime(1900, 1, 1);
                denngay = new DateTime(1900, 1, 1);
                ten_benhnhan = "";
                id_khoa_giao = -1;
                id_khoa_nhan = -1;
                id_nguoi_giao = -1;
                id_nguoi_nhan = -1;
            }
           
            m_dtData = SPs.EmrPt02BangkiemchuanbivabangiaonguoibenhtruocphauthuatLaydanhsach(-1,tungay, denngay, id_khoa_giao, id_khoa_nhan,id_nguoi_giao,id_nguoi_nhan, "", ma_luotkham,ten_benhnhan).GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdList, m_dtData, true, true, "1=1", "sngay_giao,ten_benhnhan");
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
        private void frm_Quanlyfrm_BangKiemChuanBiVaBanGiaoNguoiBenhTruocPhauThuat_KeyDown(object sender, KeyEventArgs e)
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
     
        KcbLuotkham objKcbLuotkham = null;
       

        private void cmdInsert_Click(object sender, EventArgs e)
        {
            frm_BangKiemChuanBiVaBanGiaoNguoiBenhTruocPhauThuat _bienban = new frm_BangKiemChuanBiVaBanGiaoNguoiBenhTruocPhauThuat();
            _bienban.mv_blnCallFromMenu = false;
            _bienban._OnCreated += _OnCreated;
            _bienban.m_enAct = action.Insert;
            _bienban.ShowDialog();
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            frm_BangKiemChuanBiVaBanGiaoNguoiBenhTruocPhauThuat _bienban = new frm_BangKiemChuanBiVaBanGiaoNguoiBenhTruocPhauThuat();
            _bienban.mv_blnCallFromMenu = false;
            _bienban.m_enAct = action.Update;
            _bienban._OnCreated += _OnCreated;
            _bienban.ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Text = grdList.GetValue(KcbLuotkham.Columns.MaLuotkham).ToString();
            _bienban.ucThongtinnguoibenh_emr_basic1.Refresh();
            _bienban.ShowDialog();

        }
        void _OnCreated(long id, action m_enAct)
        {
            try
            {
                DataTable dt_temp = SPs.EmrPt02BangkiemchuanbivabangiaonguoibenhtruocphauthuatLaydanhsach(id, new DateTime(1900, 1, 1), new DateTime(1900, 1, 1),-1,-1,-1,-1,"","","").GetDataSet().Tables[0];
                if (m_enAct == action.Delete)
                {
                    if (DeleteMe())
                    {
                        DataRow[] arrDr = m_dtData.Select(string.Format("{0}={1}", EmrPt02Bangkiemchuanbivabangiaonguoibenhtruocphauthuat.Columns.IdPhieu, grdList.GetValue(EmrPt02Bangkiemchuanbivabangiaonguoibenhtruocphauthuat.Columns.IdPhieu)));
                        if (arrDr.Length > 0)
                            m_dtData.Rows.Remove(arrDr[0]);
                        m_dtData.AcceptChanges();
                    }
                }
                if (m_enAct == action.Insert && m_dtData != null && m_dtData.Columns.Count > 0 && dt_temp.Rows.Count > 0)
                {
                    m_dtData.ImportRow(dt_temp.Rows[0]);
                    return;
                }
                if (m_enAct == action.Update && m_dtData != null && m_dtData.Columns.Count > 0 && dt_temp.Rows.Count > 0)
                {
                    DataRow[] arrDr = m_dtData.Select("Id_phieu=" + id);
                    if (arrDr.Length > 0)
                    {
                        arrDr[0]["sngayphauthuat"] = dt_temp.Rows[0]["sngayphauthuat"];
                        arrDr[0]["sngay_giao"] = dt_temp.Rows[0]["sngay_giao"];
                        arrDr[0]["sngay_nhan"] = dt_temp.Rows[0]["sngay_nhan"];

                        arrDr[0]["ma_phieu"] = dt_temp.Rows[0]["ma_phieu"];
                     
                    }
                    else
                        m_dtData.ImportRow(dt_temp.Rows[0]);

                }
                m_dtData.AcceptChanges();
                Utility.GotoNewRowJanus(grdList, EmrPt02Bangkiemchuanbivabangiaonguoibenhtruocphauthuat.Columns.IdPhieu, id.ToString());
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                ModifyCommand();
            }
        }
        EmrDocuments emrdoc = new EmrDocuments();
        bool DeleteMe()
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        long IdPhieu = Utility.Int32Dbnull(grdList.GetValue(EmrPt02Bangkiemchuanbivabangiaonguoibenhtruocphauthuat.Columns.IdPhieu), -1);
                        new Delete().From(EmrPt02Bangkiemchuanbivabangiaonguoibenhtruocphauthuat.Schema).Where(EmrPt02Bangkiemchuanbivabangiaonguoibenhtruocphauthuat.Columns.IdPhieu).IsEqualTo(IdPhieu).Execute();
                        emrdoc.DeleteDocument(IdPhieu, Loaiphieu_HIS.BANGKIEM_CHUANBI_VA_BANGIAO_NGUOIBENH_TRUOCPHAUTHUAT, Loaiphieu_HIS.BANGKIEM_CHUANBI_VA_BANGIAO_NGUOIBENH_TRUOCPHAUTHUAT);
                    }
                    scope.Complete();


                    return true;

                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private void cmdDelete_Click(object sender, EventArgs e)
        {
            try
            {
                EmrPt02Bangkiemchuanbivabangiaonguoibenhtruocphauthuat obj_bienban = EmrPt02Bangkiemchuanbivabangiaonguoibenhtruocphauthuat.FetchByID(Utility.Int32Dbnull(grdList.GetValue(EmrPt02Bangkiemchuanbivabangiaonguoibenhtruocphauthuat.Columns.IdPhieu), -1));
                if (obj_bienban == null)
                {
                    Utility.ShowMsg(string.Format("Bảng kiểm chuẩn bị và bàn giao người bệnh trước phẫu thuật của người bệnh {0} có thể đã bị người khác xóa ở chức năng khác. Vui lòng kiểm tra lại bằng cách nhấn nút tìm kiếm", grdList.GetValue("ten_benhnhan").ToString()));
                    return;
                }
                
                if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn xóa phiếu {0} của người bệnh {1} hay không?", grdList.GetValue(EmrPt02Bangkiemchuanbivabangiaonguoibenhtruocphauthuat.Columns.MaPhieu).ToString(), grdList.GetValue("ten_benhnhan").ToString()), "Xác nhận xóa", true))
                {
                    if (DeleteMe())
                    {
                        Utility.ShowMsg(string.Format("Xóa Bảng kiểm chuẩn bị và bàn giao người bệnh trước phẫu thuật cho người bệnh {0} thành công", grdList.GetValue("ten_benhnhan").ToString()));
                        DataRow[] arrDr = m_dtData.Select(string.Format("{0}={1}", EmrPt02Bangkiemchuanbivabangiaonguoibenhtruocphauthuat.Columns.IdPhieu, grdList.GetValue(EmrPt02Bangkiemchuanbivabangiaonguoibenhtruocphauthuat.Columns.IdPhieu)));
                        if (arrDr.Length > 0)
                            m_dtData.Rows.Remove(arrDr[0]);
                        m_dtData.AcceptChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void cmdPrint_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.WaitNow(this);
                try
                {
                    long Id = Utility.Int64Dbnull(grdList.GetValue(EmrPt02Bangkiemchuanbivabangiaonguoibenhtruocphauthuat.Columns.IdPhieu));

                    EmrPt02Bangkiemchuanbivabangiaonguoibenhtruocphauthuat _phieu = new Select().From(EmrPt02Bangkiemchuanbivabangiaonguoibenhtruocphauthuat.Schema)
                       .Where(EmrPt02Bangkiemchuanbivabangiaonguoibenhtruocphauthuat.Columns.IdPhieu).IsEqualTo(Id)
                       .ExecuteSingle<EmrPt02Bangkiemchuanbivabangiaonguoibenhtruocphauthuat>();
                    if (_phieu.IdPhieu <= 0)
                    {
                        Utility.ShowMsg("Bạn cần lưu thông tin Bảng kiểm chuẩn bị và bàn giao người bệnh trước phẫu thuật trước khi thực hiện in phiếu");
                        return;
                    }
                    DataTable dtData = SPs.EmrPt02BangkiemchuanbivabangiaonguoibenhtruocphauthuatLaythongtinIn(_phieu.IdPhieu).GetDataSet().Tables[0];
                    dtData.TableName = "BANGKIEM_CHUANBI_VA_BANGIAO_NGUOIBENH_TRUOCPHAUTHUAT";
                    dtData.Rows[0]["sngayphauthuat"] = _phieu != null ? Utility.FormatDateTime_gio_ngay_thang_nam(_phieu.Ngayphauthuat, "") : "....... giờ.......ngày................./............../20..............";
                    dtData.Rows[0]["sngay_giao"] = _phieu != null ? Utility.FormatDateTime_gio_ngay_thang_nam(_phieu.NgayGiao, "") : "........giờ...........phút, ngày........./........./20.........";
                    dtData.Rows[0]["sngay_nhan"] = _phieu != null ? Utility.FormatDateTime_gio_ngay_thang_nam(_phieu.NgayNhan, "") : "....... giờ.......ngày................./............../20..............";
                    dtData.Rows[0]["khangsinhduphong_giophut"] = _phieu != null ? Utility.GioPhut(Utility.sDbnull(_phieu.KhangsinhduphongGiophut)) : "..........giờ..........phút";
                    dtData.Rows[0]["chuanbivesinhvungdatruocmo_giophut"] = _phieu != null ? Utility.GioPhut(Utility.sDbnull(_phieu.ChuanbivesinhvungdatruocmoGiophut)) : "..........giờ..........phút";
                    dtData.Rows[0]["dungthuoctruocmochongnon_giophut"] = _phieu != null ? Utility.GioPhut(Utility.sDbnull(_phieu.DungthuoctruocmochongnonGiophut)) : "..........giờ..........phút";
                    dtData.Rows[0]["dungthuoctruocmothuocdieutrikhac_giophut"] = _phieu != null ? Utility.GioPhut(Utility.sDbnull(_phieu.DungthuoctruocmothuocdieutrikhacGiophut)) : "..........giờ..........phút";
                    dtData.Rows[0]["nhinantugio__giophut"] = _phieu != null ? Utility.GioPhut(Utility.sDbnull(_phieu.NhinantugioGiophut)) : "..........giờ..........phút";
                    WordPrinter.InPhieu(dtData, "BANGKIEM_CHUANBI_VA_BANGIAO_NGUOIBENH_TRUOCPHAUTHUAT.doc", "", false, @"\MergeFields\BANGKIEM_CHUANBI_VA_BANGIAO_NGUOIBENH_TRUOCPHAUTHUAT_CHECKED_FIELDS.txt");


                }
                catch (Exception ex)
                {
                    Utility.CatchException(ex);
                }

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                Utility.DefaultNow(this);
              
            }
        }

        private void lnkDelete_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            dtpNgayin.Value = dtToDate.Value = DateTime.Now;
            txtMaluotkham.Clear();
            txtSohoso.Clear();
            txtTennguoibenh.Clear();
            txtMaluotkham.Focus();
        }
     
    }
}
