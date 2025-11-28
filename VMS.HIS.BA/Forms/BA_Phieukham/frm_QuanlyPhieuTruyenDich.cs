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
using Janus.Windows.GridEX;
using VNS.HIS.UI.DANHMUC.PHIEU;

namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_QuanlyPhieuTruyenDich : Form
    {
        private DataTable m_dtData=new DataTable();
        public TrangthaiNoitru TrangthaiNoitru = TrangthaiNoitru.NoiTru;
        DataTable _mDtKhoanoitru;
        KcbLuotkham objLuotkham;
        long id_phieu;
        public frm_QuanlyPhieuTruyenDich()
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
            Load += frm_QuanlyPhieuTruyenDich_Load;
            KeyDown += frm_QuanlyPhieuTruyenDich_KeyDown;
            grdList.SelectionChanged += GrdList_SelectionChanged;
            grdList.MouseDoubleClick += GrdList_MouseDoubleClick;
            grd_thuoc_dichtruyen.MouseDoubleClick += Grd_thuoc_dichtruyen_MouseDoubleClick;
            grd_thuoc_dichtruyen.SelectionChanged += Grd_thuoc_dichtruyen_SelectionChanged;
        }
        bool AllowSelectionChanged = false;
        private long id_chitietdonthuoc;
        private long id_donthuoc;
        private int id_chitietdonthuoc_Thuoc_Thu;
        private decimal soluong_conlai = 0;
        private decimal soluong_ke = 0;
        decimal soluongdatruyen = 0;
        private int idthuockho;
        private int doctorid;
        private string tenthuoc;
        private string solo;
        private int id_thuoc;
        private int Patientdeptid;
        private int id_khoadieutri;
       
        void modifyCommandPhieutruyendich()
        {
             soluongdatruyen = m_dtPhieuchitiet.AsEnumerable().Where(c=>Utility.Int32Dbnull( c["id_chitietdonthuoc"])== id_chitietdonthuoc).Sum(c => Utility.DecimaltoDbnull(c["so_luong"]));
            soluong_conlai = soluong_ke - soluongdatruyen;
            bool hasPTD = m_dtPhieuchitiet.Select("id_chitietdonthuoc=" + id_chitietdonthuoc).Length > 0;
            cmdThemoiPTD.Enabled = Utility.isValidGrid(grd_thuoc_dichtruyen);//&& soluongdatruyen < soluong_ke;
            cmdSuaPTD.Enabled = Utility.isValidGrid(grd_thuoc_dichtruyen) && hasPTD;
            cmdXoaPTD.Enabled = Utility.isValidGrid(grd_thuoc_dichtruyen) && hasPTD;
            cmdInPTD.Enabled = Utility.isValidGrid(grd_thuoc_dichtruyen) && hasPTD;
        }
        private void Grd_thuoc_dichtruyen_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LocPhieuDichTruyenTheoThuoc(Utility.GetValueFromGridColumn(grd_thuoc_dichtruyen, "id_thuoc"));
        }

        private void Grd_thuoc_dichtruyen_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grd_thuoc_dichtruyen) || !AllowSelectionChanged) return;
                if (!Utility.isValidGrid(grd_thuoc_dichtruyen))
                {
                    LocPhieuDichTruyenTheoThuoc("-1");
                    return;
                }
                else
                {
                   
                    id_chitietdonthuoc = Utility.Int64Dbnull(grd_thuoc_dichtruyen.GetValue("id_chitietdonthuoc"), -1);
                    id_donthuoc = Utility.Int64Dbnull(grd_thuoc_dichtruyen.GetValue("id_donthuoc"), -1);
                    soluong_ke = Utility.DecimaltoDbnull(grd_thuoc_dichtruyen.GetValue("so_luong"), -1);
                    idthuockho = Utility.Int32Dbnull(grd_thuoc_dichtruyen.GetValue("Id_ThuocKho"), -1);
                    tenthuoc = Utility.sDbnull(grd_thuoc_dichtruyen.GetValue("ten_thuoc"));
                    solo = Utility.sDbnull(grd_thuoc_dichtruyen.GetValue("so_lo"));
                    id_thuoc = Utility.Int32Dbnull(grd_thuoc_dichtruyen.GetValue("id_thuoc"), -1);
                    doctorid = Utility.Int32Dbnull(grd_thuoc_dichtruyen.GetValue("id_bacsi"), -1);
                    
                    LocPhieuDichTruyenTheoThuoc(Utility.GetValueFromGridColumn(grd_thuoc_dichtruyen, "id_thuoc"));
                }
                modifyCommandPhieutruyendich();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
           
        void LocPhieuDichTruyenTheoThuoc(string id_thuoc)
        {
            try
            {
                m_dtPhieuchitiet.DefaultView.RowFilter = "id_thuoc=" + id_thuoc;
            }
            catch (Exception ex)
            {


            }
        }
        private void GrdList_SelectionChanged(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdList))
            {
                grd_thuoc_dichtruyen.DataSource = null;
                grd_chitiet_dichtruyen.DataSource = null;
               
            }
            else
            {
                LoadThuocTheoDoiTruyenDich();
            }
            ModifyCommand();
            modifyCommandPhieutruyendich();
        }
        string _rowFilter = "1=1";
        private void LoadThuocTheoDoiTruyenDich()
        {
            try
            {
                AllowSelectionChanged = false;
                _rowFilter = "1=1";
                id_khoadieutri = Utility.Int32Dbnull(grdList.GetValue("id_khoadieutri"), -1);
                id_phieu = Utility.Int64Dbnull(grdList.GetValue("id_phieu"));
                objLuotkham = Utility.getKcbLuotkham(grdList.CurrentRow);
                DataTable dtThuocDichTruyen = SPs.NoitruLayThongTinThuocTruyenDich(objLuotkham.MaLuotkham, (int)objLuotkham.IdBenhnhan).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grd_thuoc_dichtruyen, dtThuocDichTruyen, false, true, "1=1", "");
                LoadPhieuTheoDoiTruyenDich(-1);
               
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                AllowSelectionChanged = true;
                Grd_thuoc_dichtruyen_SelectionChanged(grd_thuoc_dichtruyen, new EventArgs());
            }
        }
        DataTable m_dtPhieuchitiet = new DataTable();
        private void LoadPhieuTheoDoiTruyenDich(int id_thuoc)
        {
            try
            {
                m_dtPhieuchitiet = SPs.NoitruPhieutruyendichLaydanhsach(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, id_khoadieutri, id_thuoc).GetDataSet().Tables[0];
                _rowFilter = "1=1";

                m_dtPhieuchitiet.DefaultView.RowFilter = _rowFilter;
                m_dtPhieuchitiet.AcceptChanges();
                Utility.SetDataSourceForDataGridEx(grd_chitiet_dichtruyen, m_dtPhieuchitiet, false, true, _rowFilter, "");
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(string.Format("Lỗi trong quá trình lấy thông tin phiếu chăm sóc :{0}", exception));
                }
            }
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

        private void frm_QuanlyPhieuTruyenDich_Load(object sender, EventArgs e)
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
           dtFromDate.Value = dtToDate.Value = globalVariables.SysDate;
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
            DateTime tungay=chkByDate.Checked ? dtFromDate.Value.Date : new DateTime(1900,1,1);
            DateTime denngay =chkByDate.Checked ? dtToDate.Value.Date.AddHours(23).AddMinutes(59).AddSeconds(59) : new DateTime(1900, 1, 1);
            string ma_luotkham=(Utility.DoTrim(txtMaluotkham.Text));
            string ten_benhnhan=(Utility.DoTrim(txtTennguoibenh.Text));
          
            if (ma_luotkham.Length > 0)
            {
                tungay= new DateTime(1900, 1, 1);
                denngay = new DateTime(1900, 1, 1);
                ten_benhnhan = "";
            }
            m_dtData = SPs.NoitruPhieudichtruyenLaydanhsach(-1,tungay, denngay, "", ma_luotkham,ten_benhnhan,"",-1,-1).GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdList, m_dtData, true, true, "1=1", "ten_benhnhan");
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
        private void frm_QuanlyPhieuTruyenDich_KeyDown(object sender, KeyEventArgs e)
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
            frm_themphieutruyendich phieu = new frm_themphieutruyendich();
            phieu.mv_blnCallFromMenu = false;
            phieu._OnCreated += _OnCreated;
            phieu.m_enAct = action.Insert;
            phieu.ShowDialog();
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            frm_themphieutruyendich phieu = new frm_themphieutruyendich();
            phieu.mv_blnCallFromMenu = false;
            phieu.m_enAct = action.Update;
            phieu._OnCreated += _OnCreated;
            phieu.objPhieu = NoitruPhieudichtruyen.FetchByID(Utility.Int64Dbnull(grdList.GetValue(NoitruPhieudichtruyen.Columns.IdPhieu)));
            //phieu.ucThongtinnguoibenh_emr_basic1.txtMaluotkham.Text = grdList.GetValue(KcbLuotkham.Columns.MaLuotkham).ToString();
            //phieu.ucThongtinnguoibenh_emr_basic1.Refresh();
            phieu.ShowDialog();

        }
        void _OnCreated(long id, action m_enAct)
        {
            try
            {
                DataTable dt_temp = SPs.NoitruPhieudichtruyenLaydanhsach(id, new DateTime(1900, 1, 1), new DateTime(1900, 1, 1),"","","","",-1,-1).GetDataSet().Tables[0];
                if (m_enAct == action.Delete)
                {
                    if (DeleteMe())
                    {
                        DataRow[] arrDr = m_dtData.Select(string.Format("{0}={1}", NoitruPhieudichtruyen.Columns.IdPhieu, grdList.GetValue(NoitruPhieudichtruyen.Columns.IdPhieu)));
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
                    DataRow[] arrDr = m_dtData.Select("id_phieu=" + id);
                    if (arrDr.Length > 0)
                    {
                        arrDr[0]["id_khoadieutri"] = dt_temp.Rows[0]["id_khoadieutri"];
                        arrDr[0]["ma_phieu"] = dt_temp.Rows[0]["ma_phieu"];
                        arrDr[0]["khoa"] = dt_temp.Rows[0]["khoa"];
                        arrDr[0]["buong"] = dt_temp.Rows[0]["buong"];
                        arrDr[0]["giuong"] = dt_temp.Rows[0]["giuong"];
                        arrDr[0]["chan_doan"] = dt_temp.Rows[0]["chan_doan"];

                    }
                    else
                        m_dtData.ImportRow(dt_temp.Rows[0]);

                }
                m_dtData.AcceptChanges();
                Utility.GotoNewRowJanus(grdList, NoitruPhieudichtruyen.Columns.IdPhieu, id.ToString());
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
                        long IdPhieu = Utility.Int32Dbnull(grdList.GetValue(NoitruPhieudichtruyen.Columns.IdPhieu), -1);
                        new Delete().From(NoitruPhieudichtruyen.Schema).Where(NoitruPhieudichtruyen.Columns.IdPhieu).IsEqualTo(IdPhieu).Execute();
                        emrdoc.DeleteDocument(IdPhieu, Loaiphieu_HIS.PHIEUTHEODOI_TRUYENDICH, Loaiphieu_HIS.PHIEUTHEODOI_TRUYENDICH);
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
                NoitruPhieudichtruyen objGiayXacnhan = NoitruPhieudichtruyen.FetchByID(Utility.Int32Dbnull(grdList.GetValue(NoitruPhieudichtruyen.Columns.IdPhieu), -1));
                if (objGiayXacnhan == null)
                {
                    Utility.ShowMsg(string.Format("Phiếu truyền dịch của người bệnh {0} có thể đã bị người khác xóa ở chức năng khác. Vui lòng kiểm tra lại bằng cách nhấn nút tìm kiếm", grdList.GetValue("ten_benhnhan").ToString()));
                    return;
                }
                
                if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn xóa phiếu {0} của người bệnh {1} hay không?", grdList.GetValue(NoitruPhieudichtruyen.Columns.MaPhieu).ToString(), grdList.GetValue("ten_benhnhan").ToString()), "Xác nhận xóa", true))
                {
                    if (DeleteMe())
                    {
                        Utility.ShowMsg(string.Format("Xóa phiếu truyền dịch cho người bệnh {0} thành công", grdList.GetValue("ten_benhnhan").ToString()));
                        DataRow[] arrDr = m_dtData.Select(string.Format("{0}={1}", NoitruPhieudichtruyen.Columns.IdPhieu, grdList.GetValue(NoitruPhieudichtruyen.Columns.IdPhieu)));
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
                //Utility.WaitNow(this);
                //try
                //{
                //    NoitruPhieudichtruyen phieucamket    = new Select().From(NoitruPhieudichtruyen.Schema)
                //           .Where(NoitruPhieudichtruyen.Columns.IdPhieu).IsEqualTo(Utility.Int64Dbnull(grdList.GetValue(NoitruPhieudichtruyen.Columns.IdPhieu)))
                //           .ExecuteSingle<NoitruPhieudichtruyen>();
                //    if (phieucamket.IdPhieu <= 0)
                //    {
                //        Utility.ShowMsg("Bạn cần lưu thông tin phiếu chấp thuận PTTT và Gây mê hồi sức trước khi thực hiện in phiếu");
                //        return;
                //    }
                //    DataTable dtData = SPs.NoitruPhieudichtruyenLaythongtinIn(phieucamket.IdPhieu).GetDataSet().Tables[0];
                //    dtData.TableName = "phieucamketchapnhan_pttt";
                //    dtData.Rows[0]["sngay_camket"] = phieucamket != null ? Utility.FormatDateTime_gio_ngay_thang_nam(phieucamket.NgayCamket, "") : "Ngày ......./......./..........";
                //    WordPrinter.InPhieu(dtData, "phieucamketchapnhan_pttt.doc", "");


                //}
                //catch (Exception ex)
                //{
                //    Utility.CatchException(ex);
                //}

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
            dtToDate.Value = DateTime.Now;
            txtBacSyCD.SetId(-1);
            txt_dieuduong_thuchien.SetId(-1);
            txt_dichtruyen.Clear();
            txtMaluotkham.Clear();
            txtSohoso.Clear();
            txtTennguoibenh.Clear();
            txtMaluotkham.Focus();

        }

        private void chk_themtudo_CheckedChanged(object sender, EventArgs e)
        {
            //grd_phieu_truyendich.RootTable.Columns["ten_bacsy_chidinh"].EditType = Janus.Windows.GridEX.EditType.TextBox;
            //grd_phieu_truyendich.RootTable.Columns["ten_yta_thuchien"].EditType = Janus.Windows.GridEX.EditType.TextBox;
            //grd_phieu_truyendich.RootTable.Columns["so_luong"].EditType = Janus.Windows.GridEX.EditType.TextBox;
            //grd_phieu_truyendich.RootTable.Columns["ten_dichtruyen"].EditType = Janus.Windows.GridEX.EditType.TextBox;
            //grd_phieu_truyendich.RootTable.Columns["toc_do"].EditType = Janus.Windows.GridEX.EditType.TextBox;
            //grd_phieu_truyendich.RootTable.Columns["thoigian_batdau"].EditType = Janus.Windows.GridEX.EditType.TextBox;
            //grd_phieu_truyendich.RootTable.Columns["thoigian_ketthuc"].EditType = Janus.Windows.GridEX.EditType.TextBox;
            //grd_phieu_truyendich.RootTable.Columns["ten_yta_thuchien"].EditType = Janus.Windows.GridEX.EditType.TextBox;
        }

        private void cmdThemoiPTD_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grd_thuoc_dichtruyen))
                {
                    Utility.ShowMsg("Bạn cần chọn ít nhất một thuốc để thêm mới phiếu truyền dịch");
                    return;
                }
                if (m_dtPhieuchitiet.AsEnumerable().Where(c => Utility.Int32Dbnull(c["id_chitietdonthuoc"]) == id_chitietdonthuoc).Sum(c => Utility.DecimaltoDbnull(c["so_luong"])) >= soluong_ke)
                {
                    Utility.ShowMsg(string.Format("Số lượng truyền dịch đã bằng số lượng kê đơn {0} nên bạn không được tạo phiếu truyền dịch thêm cho thuốc đang chọn. Vui lòng chọn thuốc khác", soluong_ke));
                    return;
                }
                frm_ThemChitietTruyendich frm = new frm_ThemChitietTruyendich();
                frm.em_Action = action.Insert;
                frm.IdPhieu = id_phieu;
                frm.m_dtPhieuchitiet = m_dtPhieuchitiet;
                frm.id_chitietdonthuoc = id_chitietdonthuoc;
                frm.id_donthuoc = id_donthuoc;
                frm.id_thuoc = id_thuoc;
                frm.grdList = grd_chitiet_dichtruyen;
                frm.soluong_conlai = soluong_conlai;
                frm.soluong_ke = soluong_ke;
                frm.soluongdatruyen = soluongdatruyen;
                frm.Id_ThuocKho = idthuockho;
                frm.TenThuoc = tenthuoc;
                frm.solo = solo;
                frm.txtID.Text = "-1";
                frm.Doctor_ID = doctorid;
                frm.id_BG = Patientdeptid;
                frm.id_khoadieutri = id_khoadieutri;
                frm.objLuotkham = objLuotkham;
                frm.ShowDialog();
                modifyCommandPhieutruyendich();
            }
            catch (Exception exception)
            {
                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(string.Format("Lỗi :{0}", exception));
                }
            }
        }

        private void cmdSuaPTD_Click(object sender, EventArgs e)
        {
            try
            {
                if (grd_chitiet_dichtruyen.CurrentRow != null)
                {
                    frm_ThemChitietTruyendich frm = new frm_ThemChitietTruyendich();
                    NoitruPhieudichtruyenChitiet objChitietDichTruyen = NoitruPhieudichtruyenChitiet.FetchByID(Utility.Int32Dbnull(grd_chitiet_dichtruyen.GetValue("id"), -1));
                    if (objChitietDichTruyen != null)
                    {
                        KcbDonthuocChitiet objDonthuocchitiet = KcbDonthuocChitiet.FetchByID(Utility.Int32Dbnull(objChitietDichTruyen.IdChitietdonthuoc));
                        if (objDonthuocchitiet != null)
                        {

                            frm.em_Action = action.Update;
                            frm.objphieu = NoitruPhieudichtruyen.FetchByID(Utility.Int64Dbnull(grdList.GetValue("id_phieu")));
                            frm.txtID.Text = objChitietDichTruyen.Id.ToString();
                            frm.m_dtPhieuchitiet = m_dtPhieuchitiet;
                            frm.id_chitietdonthuoc = id_chitietdonthuoc;
                            frm.id_donthuoc = id_donthuoc;
                            frm.id_thuoc = id_thuoc;
                            frm.grdList = grd_chitiet_dichtruyen;
                            frm.soluong_conlai = soluong_conlai;
                            frm.soluong_ke = soluong_ke;
                            frm.soluongdatruyen = soluongdatruyen;
                            frm.Id_ThuocKho = idthuockho;
                            frm.TenThuoc = tenthuoc;
                            frm.solo = solo;
                            frm.Doctor_ID = doctorid;
                            frm.id_BG = Patientdeptid;
                            frm.objPhieudichtruyen_chitiet = objChitietDichTruyen;
                            frm.id_khoadieutri = id_khoadieutri;
                            frm.objLuotkham = objLuotkham;
                            frm.ShowDialog();
                        }

                    }
                    else
                    {
                        Utility.ShowMsg("Phiếu truyền dịch bạn vừa chọn sửa có thể đã bị người khác xóa mất. Vui lòng kiểm tra lại");
                    }

                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        int num = 0;
        private void cmdXoaPTD_Click(object sender, EventArgs e)
        {
            Utility.AutoCheckGrid(grd_chitiet_dichtruyen); 
            if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn xóa các thông tin truyền dịch đang chọn hay không?",Utility.sDbnull(grd_chitiet_dichtruyen.GetValue("ten_thuoc"))), "Thông báo", true))
            {
                foreach (GridEXRow row in grd_chitiet_dichtruyen.GetCheckedRows())
                {
                    num = NoitruPhieudichtruyenChitiet.Delete(Utility.Int64Dbnull(row.Cells["id"].Value));
                    if(num>0)
                    {
                        row.Delete();
                    }    
                }
                grd_chitiet_dichtruyen.UpdateData(); // commit lại vào DataSource
            }
        }

        private void cmdInPTD_Click(object sender, EventArgs e)
        {
            try
            {
                NoitruPhieudichtruyen PDT = new Select().From(NoitruPhieudichtruyen.Schema)
                       .Where(NoitruPhieudichtruyen.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                       .And(NoitruPhieudichtruyen.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                       .And(NoitruPhieudichtruyen.Columns.IdPhieu).IsEqualTo(Utility.Int64Dbnull(grdList.GetValue("id_phieu")))
                       .ExecuteSingle<NoitruPhieudichtruyen>();
                if (PDT.IdPhieu <= 0)
                {
                    Utility.ShowMsg("Phiếu truyền dịch không tồn tại. Vui lòng kiểm tra xem có bị xóa hay không?");
                    //cmdGhi.Focus();
                    return;
                }
                DataTable dtData = SPs.NoitruPhieutruyendichLaydulieuinphieu(PDT.MaLuotkham, PDT.IdBenhnhan,PDT.IdPhieu).GetDataSet().Tables[0];
                dtData.TableName = "PHIEUTHEODOI_TRUYENDICH";
                //dtData.Rows[0]["sngaygio_nhapvien"] = PDT != null ? Utility.FormatDateTime_giophut_ngay_thang_nam(PDT.NgayVaovien, "") : ".......... giờ ....... ngày ........./........./.............";
                //dtData.Rows[0]["sngaygio_ravien"] = PDT != null ? Utility.FormatDateTime_giophut_ngay_thang_nam(PDT.NgayRavien, "") : ".......... giờ ....... ngày ........./........./.............";
                //dtData.Rows[0]["sngayxacnhan"] = Utility.FormatDateTime(Utility.sDbnull(dtData.Rows[0]["sngayxacnhan"], ""), "ngày......tháng......năm.........");
                WordPrinter.InPhieuTruyenDich(dtData, "PHIEUTHEODOI_TRUYENDICH.doc", "");


            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
    }
}
