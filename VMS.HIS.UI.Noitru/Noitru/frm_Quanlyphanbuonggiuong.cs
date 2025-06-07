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
using System.IO;
using System.Transactions;
namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_Quanlyphanbuonggiuong : BaseForm
    {
        private DataTable _mDtTimKiembenhNhan=new DataTable();
        public TrangthaiNoitru TrangthaiNoitru = TrangthaiNoitru.NoiTru;
        DataTable _mDtKhoanoitru;
        string SplitterPath = "";
        string thamso = "DTRI_NOITRU";//ALL,DTRI_NOITRU,DTRI_NGOAITRU
        public frm_Quanlyphanbuonggiuong(string thamso)
        {
            InitializeComponent();
            this.thamso = thamso;
            mnuChuyenNoitru.Visible = thamso == "DTRI_NGOAITRU";
            mnuChuyendieutringoaitru.Visible = thamso == "DTRI_NOITRU";
            SplitterPath = string.Format(@"{0}\{1}.splitter", Application.StartupPath, this.Name);
            Utility.SetVisualStyle(this);
            dtToDate.Value = dtFromDate.Value =globalVariables.SysDate;
            Utility.VisiableGridEx(grdList,"ID",globalVariables.IsAdmin);
            InitEvents();
        }
        void InitEvents()
        {
            Shown += frm_Quanlyphanbuonggiuong_Shown;
            FormClosing += frm_Quanlyphanbuonggiuong_FormClosing;
            cmdPhanGiuong.Click += cmdPhanGiuong_Click;
            cmdHuyphangiuong.Click += cmdHuyphangiuong_Click;
            cmdChuyenKhoa.Click += cmdChuyenKhoa_Click;
            cmdHuychuyenkhoa.Click += cmdHuychuyenkhoa_Click;
            cmdChuyenGiuong.Click += cmdChuyenGiuong_Click;
            cmdConfig.Click += cmdConfig_Click;
            cmdExit.Click += cmdExit_Click;
            cmdTimKiem.Click += cmdTimKiem_Click;
            txtPatientCode.KeyDown += txtPatientCode_KeyDown;
            chkByDate.CheckedChanged += chkByDate_CheckedChanged;
            Load += frm_Quanlyphanbuonggiuong_Load;
            KeyDown += frm_Quanlyphanbuonggiuong_KeyDown;
            grdList.SelectionChanged+=grdList_SelectionChanged;
            grdBuongGiuong.MouseDoubleClick += grdBuongGiuong_MouseDoubleClick;
            cmdThemMoiBN.Click+=cmdThemMoiBN_Click;
            cmdSuaThongTinBN.Click+=cmdSuaThongTinBN_Click;
            cmdXoaBN.Click+=cmdXoaBN_Click;

            mnuAdd.Click += mnuAdd_Click;
            mnuEdit.Click += mnuEdit_Click;
            mnuDelete.Click += mnuDelete_Click;

            mnuPhangiuong.Click += mnuPhangiuong_Click;
            mnuChuyengiuong.Click += mnuChuyengiuong_Click;
            mnuHuygiuong.Click += mnuHuygiuong_Click;

            mnuChuyenkhoa.Click += mnuChuyenkhoa_Click;
            mnuHuychuyenkhoa.Click += mnuHuychuyenkhoa_Click;
            mnuChuyenkhoasua.Click += mnuChuyenkhoasua_Click;
        }

        void grdBuongGiuong_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            cmdsuagiuong.PerformClick();
        }

        void frm_Quanlyphanbuonggiuong_FormClosing(object sender, FormClosingEventArgs e)
        {
            Utility.SaveValue2Lines(SplitterPath, new List<string>() { splitContainer1.SplitterDistance.ToString()});
        }
        void Try2Splitter()
        {
            try
            {


                List<int> lstSplitterSize = (from p in File.ReadLines(SplitterPath)
                                             select Utility.Int32Dbnull(p)).ToList<int>();
                if (lstSplitterSize != null && lstSplitterSize.Count >= 1)
                {
                    splitContainer1.SplitterDistance = lstSplitterSize[0];
                }
            }
            catch (Exception)
            {

            }
        }
        void frm_Quanlyphanbuonggiuong_Shown(object sender, EventArgs e)
        {
            Try2Splitter();
        }

        void mnuChuyenkhoasua_Click(object sender, EventArgs e)
        {
            Chuyenkhoasua();
        }

        void mnuHuychuyenkhoa_Click(object sender, EventArgs e)
        {
            HuychuyenKhoa();
        }

        void mnuChuyenkhoa_Click(object sender, EventArgs e)
        {
            ChuyenKhoa();
        }

        void mnuHuygiuong_Click(object sender, EventArgs e)
        {
            HuyPhangiuong();
        }

        void mnuChuyengiuong_Click(object sender, EventArgs e)
        {
            ChuyenGiuong();
        }

        void mnuPhangiuong_Click(object sender, EventArgs e)
        {
            PhanBG();
        }

        void mnuDelete_Click(object sender, EventArgs e)
        {
            XoaBN();
        }

        void mnuEdit_Click(object sender, EventArgs e)
        {
            SuaBN();
        }

        void mnuAdd_Click(object sender, EventArgs e)
        {
            ThemBN();
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

        private void frm_Quanlyphanbuonggiuong_Load(object sender, EventArgs e)
        {
            
            InitData();
            TimKiemThongTin();
            
        }
        /// <summary>
        /// hàm thực hiện việc lấy thông tin khoa nội trú
        /// </summary>
        private void InitData()
        {
            _mDtKhoanoitru= THU_VIEN_CHUNG.LaydanhsachKhoanoitruTheoBacsi(globalVariables.UserName, Utility.Bool2byte(globalVariables.IsAdmin), 1);
            DataBinding.BindDataCombobox(cboKhoanoitru, _mDtKhoanoitru,
                                                 DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong,
                                                 "---Chọn khoa nội trú---", false,true);
            dtpNgayin.Value = globalVariables.SysDate;
            //ToolStripItem[] lstItems = new ToolStripItem[_mDtKhoanoitru.Rows.Count];
            ToolStripMenuItem mnuKhoa = new ToolStripMenuItem("Chuyển khoa điều trị do nhập viện sai");
            int idx = 0;
            foreach (DataRow dr in _mDtKhoanoitru.Rows)
            {
                ToolStripMenuItem item = new ToolStripMenuItem(Utility.sDbnull( dr["ten_khoaphong"],"UKN"));
                item.Tag = dr["id_khoaphong"];
                item.Click += item_Click;
                mnuKhoa.DropDownItems.Add(item);
                
            }
            ctxFuntions.Items.Add(mnuKhoa);
        }

        void item_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Bạn cần chọn người bệnh trên lưới danh sách để thực hiện chức năng chuyển khoa nội trú do nhập viện sai");
                    return;
                }

                if (grdBuongGiuong.GetRows().Count() > 1)
                {
                    Utility.ShowMsg("Hệ thống chỉ áp dụng chức năng này cho các người bệnh mới nhập viện và chưa phân buồng giường");
                    return;
                }
                else//Mới nhập viện chưa phân buồng giường
                {
                    DataTable dtCheck = new Select().From(NoitruPhanbuonggiuong.Schema).Where(NoitruPhanbuonggiuong.Columns.Id).IsEqualTo(Utility.Int64Dbnull(grdBuongGiuong.GetValue("id")) ).ExecuteDataSet().Tables[0];
                    if (dtCheck.Rows.Count == 1 )
                    {
                        if (Utility.Int32Dbnull(dtCheck.Rows[0]["id_buong"], -1) > 0 || Utility.Int32Dbnull(dtCheck.Rows[0]["id_giuong"], -1) > 0)
                        {
                            Utility.ShowMsg("Hệ thống chỉ áp dụng chức năng này cho các người bệnh mới nhập viện và chưa phân buồng giường");
                            return;
                        }
                    }
                    else
                    {
                        Utility.ShowMsg("Không tìm thấy dữ liệu buồng giường của người bệnh đang chọn trên lưới. Vui lòng bấm lại nút tìm kiếm");
                        return;
                    }


                }
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn cập nhật lại khoa nội trú: {0} cho người bệnh: {1} hay không?",item.Text, grdList.GetValue("ten_benhnhan").ToString()), "Xác nhận", true))
                {
                    using (var Scope = new TransactionScope())
                    {
                        using (var dbScope = new SharedDbConnectionScope())
                        {
                            new Update(NoitruPhanbuonggiuong.Schema).Set(NoitruPhanbuonggiuong.Columns.IdKhoanoitru).EqualTo(Utility.Int32Dbnull(item.Tag)).Where(NoitruPhanbuonggiuong.Columns.Id).IsEqualTo(Utility.Int64Dbnull(grdBuongGiuong.GetValue("id"))).Execute();
                            new Update(KcbLuotkham.Schema).Set(KcbLuotkham.Columns.IdKhoanoitru).EqualTo(Utility.Int32Dbnull(item.Tag)).Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(Utility.sDbnull(grdList.GetValue("ma_luotkham"))).Execute();
                        }
                        Scope.Complete();
                    }
                    
                    grdList.CurrentRow.BeginEdit();
                    grdList.CurrentRow.Cells["id_khoanoitru"].Value =Utility.Int32Dbnull( item.Tag,-1);
                    grdList.CurrentRow.Cells["ten_khoanoitru"].Value = Utility.sDbnull(item.Text, "UKN");
                    grdList.CurrentRow.EndEdit();
                    Utility.ShowMsg("Cập nhật khoa nội trú thành công. Nhấn OK để kết thúc");
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
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
            List<Int16> lstIdkhoadieutri = (from p in grdBuongGiuong.GetDataRows()
                                            select Utility.Int16Dbnull(p.Cells[NoitruPhanbuonggiuong.Columns.IdKhoanoitru].Value, -1)).Distinct().ToList<Int16>();
            cmdSuaThongTinBN.Enabled = isValid &&  Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.TrangthaiCapcuu)) > 0;
            cmdXoaBN.Enabled = isValid && Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.TrangthaiCapcuu)) > 0;
            cmdHuyNhapvien.Enabled = isValid && Utility.Int32Dbnull(grdList.GetValue(NoitruDmucGiuongbenh.Columns.IdKhoanoitru)) > 0 && grdBuongGiuong.RowCount <= 1 && Utility.Int32Dbnull(grdList.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong)) <= 0;
            //cmdNhapvien.Text=isValid &&  Utility.Int32Dbnull(grdList.GetValue(NoitruDmucGiuongbenh.Columns.IdKhoanoitru)) >0 && grdBuongGiuong.RowCount==1 && Utility.Int32Dbnull(grdList.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong)) <=0?"Hủy nhập viện":"Nhập viện";
            //cmdPhanGiuong.Enabled = isValid && Utility.Int32Dbnull(grdList.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong)) <= 0;
            cmdHuyphangiuong.Enabled = isValid && Utility.Int32Dbnull(grdList.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong)) > 0;
            cmdHuyphangiuong.Enabled = isValid && Utility.Int32Dbnull(grdList.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong)) > 0;
            cmdChuyenKhoa.Enabled = isValid && Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.IdKhoanoitru)) > 0 ;//&& Utility.Int32Dbnull(grdList.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong)) > 0;
            cmdHuychuyenkhoa.Enabled = isValid && Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.IdChuyen)) > 0 && Utility.Int32Dbnull(grdList.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong)) <= 0;
            cmdChuyenGiuong.Enabled = isValid && Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.IdKhoanoitru)) > 0 && Utility.Int32Dbnull(grdList.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong)) > 0;
            cmdChuyenkhoasua.Enabled = isValid && grdBuongGiuong.RowCount > 0 && lstIdkhoadieutri.Count > 1;
            mnuBBanhoichan.Enabled = mnuChonBSDieutri.Enabled = mnuPhieusoket15ngay.Enabled = mnuTongketBA.Enabled = objLuotkham != null && isValid;
            mnuTaoBNCapcuu.Enabled = cmdThemMoiBN.Enabled;
            mnuEdit.Enabled = cmdSuaThongTinBN.Enabled;
            mnuDelete.Enabled = cmdXoaBN.Enabled;
            mnuPhangiuong.Enabled = cmdPhanGiuong.Enabled;
            mnuChuyengiuong.Enabled = cmdChuyenGiuong.Enabled;
            mnuHuygiuong.Enabled = cmdHuyphangiuong.Enabled;
            mnuChuyenkhoa.Enabled = cmdChuyenKhoa.Enabled;
            mnuHuychuyenkhoa.Enabled = cmdHuychuyenkhoa.Enabled;
            mnuChuyenkhoasua.Enabled = cmdChuyenkhoasua.Enabled;
        }

        private void TimKiemThongTin()
        {
            if (cboKhoanoitru.Items.Count <= 0)
            {
                Utility.ShowMsg("Người dùng đang sử dụng chưa được gắn với khoa nội trú nào nên không thể tìm kiếm. Đề nghị kiểm tra lại");
                return;
            }
            _mDtTimKiembenhNhan =SPs.NoitruTimkiembenhnhanPhanBG(Utility.Int32Dbnull(cboKhoanoitru.SelectedValue,-1),
                                                txtPatientCode.Text, 1,
                                                chkByDate.Checked ? dtFromDate.Value.ToString("dd/MM/yyyy") : "01/01/1900",
                                                chkByDate.Checked ? dtToDate.Value.ToString("dd/MM/yyyy") : "01/01/1900",
                                                string.Empty, (Int16)(chkCapcuu.Checked ? 1 : -1), -1, 0, globalVariables.gv_intIDNhanvien,thamso).
                    GetDataSet().Tables[0];
                if (m_dtBG != null) m_dtBG.Clear();
            Utility.SetDataSourceForDataGridEx(grdList, _mDtTimKiembenhNhan, true, true, "1=1", "");
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
        /// hàm thực hiện việc thêm mới nhập vện cấp cứu
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdThemMoiBN_Click(object sender, EventArgs e)
        {
            ThemBN();
        }
        void ThemBN()
        {
            var taobenhnhancapcuu = new frm_Taobenhnhancapcuu
            {
                m_enAction = action.Insert,
                m_dtPatient = _mDtTimKiembenhNhan,
                grdList = grdList
            };
            taobenhnhancapcuu._OnActionSuccess += _Taobenhnhancapcuu__OnActionSuccess;
            taobenhnhancapcuu.ShowDialog();
        }
        void _Taobenhnhancapcuu__OnActionSuccess()
        {
            ModifyCommand();
        }
        void SuaBN()
        {
            var taobenhnhancapcuu = new frm_Taobenhnhancapcuu
            {
                txtMaBN = { Text = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan)) },
                txtMaLankham = { Text = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham)) },
                m_enAction = action.Update,
                m_dtPatient = _mDtTimKiembenhNhan,
                grdList = grdList
            };
            taobenhnhancapcuu._OnActionSuccess += _Taobenhnhancapcuu__OnActionSuccess;
            taobenhnhancapcuu.ShowDialog();
        }
        /// <summary>
        /// hàm thực hiện việc sửa thông tin bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSuaThongTinBN_Click(object sender, EventArgs e)
        {
            SuaBN();
        }
        /// <summary>
        /// hàm thực hiện việc ký quĩ thông tin 
        /// </summary>
        private bool isValidData_ChuyenKhoa()
        {
           string  maluotkham = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
           int idBenhnhan = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
           KcbLuotkham kcbLuotkham = Utility.getKcbLuotkham(idBenhnhan, maluotkham);
           if (kcbLuotkham ==null)
           {
               Utility.ShowMsg("Không lấy được thông tin Bệnh nhân. Đề nghị bạn cần chọn ít nhất 1 Bệnh nhân trên lưới");
               grdList.Focus();
               return false;
           }
           if (Utility.Int32Dbnull(kcbLuotkham.TrangthaiNoitru, -1) <= 0)
           {
               Utility.ShowMsg("Bệnh nhân chưa vào viện, Bạn không thể thực hiện chức năng chuyển khoa", "Thông báo", MessageBoxIcon.Warning);
               grdList.Focus();
               return false;
           }
           if (Utility.Int32Dbnull(kcbLuotkham.TrangthaiNoitru, -1) == 4)
           {
               Utility.ShowMsg("Bệnh nhân đã được xác nhận dữ liệu nội trú để chuyển thanh toán nên không thể Hủy giường. Đề nghị bạn kiểm tra lại");
               grdList.Focus();
               return false;
           }
           if (kcbLuotkham.TrangthaiNoitru == 5)
           {
               Utility.ShowMsg("Bệnh nhân đã được duyệt thanh toán nội trú nên bạn không thể sửa lại trạng thái xác nhận dữ liệu nội trú");
               return false;
           }
           if (Utility.Byte2Bool(kcbLuotkham.TthaiThanhtoannoitru) || kcbLuotkham.TrangthaiNoitru == 6)
           {
               Utility.ShowMsg("Bệnh nhân đã được thanh toán nội trú nên bạn không thể sửa lại trạng thái xác nhận dữ liệu nội trú");
               return false;
           }
            return true;
        }
        private bool isValidData_ChuyenGiuong()
        {
            string maluotkham = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
            int idBenhnhan = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
            KcbLuotkham kcbLuotkham = Utility.getKcbLuotkham(idBenhnhan, maluotkham);
            if (kcbLuotkham == null)
            {
                Utility.ShowMsg("Không lấy được thông tin Bệnh nhân. Đề nghị bạn cần chọn ít nhất 1 Bệnh nhân trên lưới");
                grdList.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(kcbLuotkham.TrangthaiNoitru, -1) <= 0)
            {
                Utility.ShowMsg("Bệnh nhân chưa vào viện nên không thể chuyển giường", "Thông báo", MessageBoxIcon.Warning);
                grdList.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(kcbLuotkham.TrangthaiNoitru, -1) == 4)
            {
                Utility.ShowMsg("Bệnh nhân đã được xác nhận dữ liệu nội trú để chuyển thanh toán nên không thể Hủy giường. Đề nghị bạn kiểm tra lại");
                grdList.Focus();
                return false;
            }
            if (kcbLuotkham.TrangthaiNoitru == 5)
            {
                Utility.ShowMsg("Bệnh nhân đã được duyệt thanh toán nội trú nên bạn không thể sửa lại trạng thái xác nhận dữ liệu nội trú");
                return false;
            }
            if (Utility.Byte2Bool(kcbLuotkham.TthaiThanhtoannoitru) || kcbLuotkham.TrangthaiNoitru == 6)
            {
                Utility.ShowMsg("Bệnh nhân đã được thanh toán nội trú nên bạn không thể sửa lại trạng thái xác nhận dữ liệu nội trú");
                return false;
            }
            int id = Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.Id));
            var noitruPhanbuonggiuong = NoitruPhanbuonggiuong.FetchByID(id);
            if (noitruPhanbuonggiuong != null && Utility.Int32Dbnull(noitruPhanbuonggiuong.IdBuong, -1) < 0)
            {
                Utility.ShowMsg("Bệnh nhân chưa phân buồng giường nên bạn không thể chuyển giường", "Thông báo", MessageBoxIcon.Warning);
                grdList.Focus();
                return false;
            }
            return true;
        }
       
        private bool isValidData_Phanbuonggiuong()
        {
            string maluotkham = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
            int idBenhnhan = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
            int idKhoanoitru = Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.IdKhoanoitru));
            KcbLuotkham kcbLuotkham = Utility.getKcbLuotkham(idBenhnhan, maluotkham);
            if (kcbLuotkham == null)
            {
                Utility.ShowMsg("Không lấy được thông tin Bệnh nhân. Đề nghị bạn cần chọn ít nhất 1 Bệnh nhân trên lưới");
                grdList.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(kcbLuotkham.TrangthaiNoitru, -1) <= 0)
            {
                Utility.ShowMsg("Bệnh nhân chưa vào viện nên không thể phân buồng giường", "Thông báo", MessageBoxIcon.Warning);
                grdList.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(kcbLuotkham.TrangthaiNoitru, -1) == 4)
            {
                Utility.ShowMsg("Bệnh nhân đã được xác nhận dữ liệu nội trú để chuyển thanh toán nên không thể Hủy giường. Đề nghị bạn kiểm tra lại");
                grdList.Focus();
                return false;
            }
            if (kcbLuotkham.TrangthaiNoitru == 5)
            {
                Utility.ShowMsg("Bệnh nhân đã được duyệt thanh toán nội trú nên bạn không thể sửa lại trạng thái xác nhận dữ liệu nội trú");
                return false;
            }
            if (Utility.Byte2Bool(kcbLuotkham.TthaiThanhtoannoitru) || kcbLuotkham.TrangthaiNoitru == 6)
            {
                Utility.ShowMsg("Bệnh nhân đã được thanh toán nội trú nên bạn không thể sửa lại trạng thái xác nhận dữ liệu nội trú");
                return false;
            }
            if (_mDtKhoanoitru == null || _mDtKhoanoitru.Rows.Count <= 0 || _mDtKhoanoitru.Select(DmucKhoaphong.Columns.IdKhoaphong + "=" + idKhoanoitru).Length <= 0)
            {
                Utility.ShowMsg("Bạn không được phân buồng giường cho Bệnh nhân của khoa khác. Đề nghị bạn kiểm tra lại");
                grdList.Focus();
                return false;
            }
            int id = Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.Id));
            _noitruPhanbuonggiuong = new Select().From<NoitruPhanbuonggiuong>()
                  .Where(NoitruPhanbuonggiuong.Columns.Id).IsEqualTo(id).ExecuteSingle<NoitruPhanbuonggiuong>();
            if (_noitruPhanbuonggiuong != null && Utility.Int32Dbnull(_noitruPhanbuonggiuong.TrangThai, -1) == 1)
            {
                Utility.ShowMsg("Bạn không được phép phân buồng giường cho trạng thái đã chuyển khoa hoặc chuyển buồng giường", "Thông báo", MessageBoxIcon.Warning);
                grdList.Focus();
                return false;
            }
            return true;
        }
        void ChuyenKhoa()
        {
            try
            {
                if (!isValidData_ChuyenKhoa()) return;
                frm_ChuyenKhoa frm = new frm_ChuyenKhoa();
                frm.IDBuonggiuong = Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.Id));
                frm.p_DanhSachPhanBuongGiuong = _mDtTimKiembenhNhan;
                // frm.m_enAction = action.Insert;
                frm.b_CallParent = true;

                frm.txtMaLanKham.Text = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
                frm.txtPatient_ID.Text = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
                frm.grdList = grdList;
                frm.ShowDialog();
                if (!frm.b_Cancel)
                {
                    int newid = Utility.Int32Dbnull(frm.txtPatientDept_ID.Text);
                    if (newid > 0)
                    {
                        DataTable dtTemp = SPs.NoitruTimkiembenhnhanTheoid(newid).GetDataSet().Tables[0];
                        if (dtTemp.Rows.Count > 0)
                        {
                            DataRow dr = ((DataRowView)grdList.CurrentRow.DataRow).Row;
                            Utility.CopyData(dtTemp.Rows[0], ref dr);
                            _mDtTimKiembenhNhan.AcceptChanges();
                        }
                    }
                    else//Xóa dòng hiện tại
                    {
                        DataRow dr = ((DataRowView)grdList.CurrentRow.DataRow).Row;
                        _mDtTimKiembenhNhan.Rows.Remove(dr);
                        _mDtTimKiembenhNhan.AcceptChanges();
                    }
                }

            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);

                //throw;
            }
            finally
            {
                ModifyCommand();
            }
        }
        /// <summary>
        /// hàm thực hiện việc chuyển khoa cho bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdChuyenKhoa_Click(object sender, EventArgs e)
        {
            ChuyenKhoa();
        }
        /// <summary>
        /// hàm thực hên việc chuyển giường bệnh cho bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdChuyenGiuong_Click(object sender, EventArgs e)
        {
            ChuyenGiuong(); 
        }
        void ChuyenGiuong()
        {
            try
            {
                if (!isValidData_ChuyenGiuong()) return;
                int id = Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.Id));
                NoitruPhanbuonggiuong objPhanbuonggiuong = NoitruPhanbuonggiuong.FetchByID(id);
                frm_Chuyengiuong frm = new frm_Chuyengiuong();
                frm.objLuotkham = this.objLuotkham;
                frm.p_DanhSachPhanBuongGiuong = _mDtTimKiembenhNhan;
                frm.b_CallParent = true;
                frm.objPhanbuonggiuong = objPhanbuonggiuong;
                // frm.m_enAction = action.Insert;
                frm.IDBuonggiuong = Utility.Int32Dbnull(grdBuongGiuong.GetValue(NoitruPhanbuonggiuong.Columns.Id));
                frm.ucThongtinnguoibenh1.txtMaluotkham.Text = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
                frm.grdList = grdList;
                frm.ShowDialog();
                if (!frm.b_Cancel)
                {
                    grdList_SelectionChanged(grdList, new EventArgs());

                }

            }
            catch (Exception exception)
            {

                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
                //throw;
            }
            finally
            {
                ModifyCommand();
            }
           
        }
        void PhanBG()
        {
            try
            {
                if (!isValidData_Phanbuonggiuong()) return;
                int id = Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.Id));
                NoitruPhanbuonggiuong objPhanbuonggiuong = NoitruPhanbuonggiuong.FetchByID(id);
                if (objPhanbuonggiuong != null)
                {
                    frm_phanbuonggiuong frm = new frm_phanbuonggiuong
                    {
                        PDanhSachPhanBuongGiuong = _mDtTimKiembenhNhan,
                        txtPatientDept_ID = { Text = Utility.sDbnull(objPhanbuonggiuong.Id) },
                        ObjPhanbuonggiuong = objPhanbuonggiuong
                        
                    };
                    frm.ucThongtinnguoibenh1.txtMaluotkham.Text = objLuotkham.MaLuotkham;
                    //frm.ucThongtinnguoibenh1.Refresh();
                    frm.ShowDialog();
                    if (!frm.BCancel)
                    {
                        grdList_SelectionChanged(grdList, new EventArgs());
                        LayLichsuBuongGiuong();

                    }
                }

            }
            catch (Exception exception)
            {

                if (globalVariables.IsAdmin)
                {
                    Utility.ShowMsg(exception.ToString());
                }
            }
            finally
            {
                ModifyCommand();
            }
        }
        private void cmdPhanGiuong_Click(object sender, EventArgs e)
        {
            PhanBG();
        }
        void SuaBG()
        {
            try
            {
                if (!isValidData_Phanbuonggiuong()) return;
                if (_noitruPhanbuonggiuong != null)
                {
                    var frm = new frm_phanbuonggiuong
                    {
                        txtPatientDept_ID = { Text = Utility.sDbnull(_noitruPhanbuonggiuong.Id) },
                        ObjPhanbuonggiuong = _noitruPhanbuonggiuong
                    };
                    frm.ShowDialog();
                    if (!frm.BCancel)
                    {
                        LayLichsuBuongGiuong();
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi: " + ex.Message);
            }
            finally
            {
                ModifyCommand();
            }
        }
        private void cmdsuabuonggiuong_Click(object sender, EventArgs e)
        {
            SuaBG();

        }

        private bool isValidData_Huygiuong()
        {
            int id = Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.Id));
            int idKhoanoitru = Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.IdKhoanoitru));
            string maluotkham = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
            int idBenhnhan = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
            KcbLuotkham kcbLuotkham = Utility.getKcbLuotkham(idBenhnhan, maluotkham);
            if (kcbLuotkham == null)
            {
                Utility.ShowMsg("Không lấy được thông tin Bệnh nhân. Đề nghị bạn cần chọn ít nhất 1 Bệnh nhân trên lưới");
                grdList.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(kcbLuotkham.TrangthaiNoitru, -1) == 4)
            {
                Utility.ShowMsg("Bệnh nhân đã được xác nhận dữ liệu nội trú để chuyển thanh toán nên không thể Hủy giường. Đề nghị bạn kiểm tra lại");
                grdList.Focus();
                return false;
            }
            if (kcbLuotkham.TrangthaiNoitru == 5)
            {
                Utility.ShowMsg("Bệnh nhân đã được duyệt thanh toán nội trú nên bạn không thể sửa lại trạng thái xác nhận dữ liệu nội trú");
                return false;
            }
            if (Utility.Byte2Bool(kcbLuotkham.TthaiThanhtoannoitru) || kcbLuotkham.TrangthaiNoitru == 6)
            {
                Utility.ShowMsg("Bệnh nhân đã được thanh toán nội trú nên bạn không thể sửa lại trạng thái xác nhận dữ liệu nội trú");
                return false;
            }
            if (_mDtKhoanoitru == null || _mDtKhoanoitru.Rows.Count<=0 || _mDtKhoanoitru.Select(DmucKhoaphong.Columns.IdKhoaphong + "=" + idKhoanoitru).Length <= 0)
            {
                Utility.ShowMsg("Bạn không được quyền hủy giường của khoa này. Đề nghị bạn kiểm tra lại");
                grdList.Focus();
                return false;
            }
            //var noitruPhieudieutri = new Select().From<NoitruPhieudieutri>()
            //    .Where(NoitruPhieudieutri.Columns.IdBuongGiuong).IsEqualTo(id)
            //    .And(NoitruPhieudieutri.Columns.MaLuotkham).IsEqualTo(maluotkham)
            //    .And(NoitruPhieudieutri.Columns.IdBenhnhan).IsEqualTo(idBenhnhan).ExecuteSingle<NoitruPhieudieutri>();
            //if (noitruPhieudieutri != null)
            //{
            //    Utility.ShowMsg("Đã có phiếu điều trị nội trú gắn với bệnh nhân tại buồng-giường đang chọn nên bạn không thể hủy. Đề nghị xem lại", "Thông báo", MessageBoxIcon.Warning);
            //    grdList.Focus();
            //    return false;
            //}
            var noitruPhanbuonggiuong = new Select().From<NoitruPhanbuonggiuong>()
                .Where(NoitruPhanbuonggiuong.Columns.Id).IsEqualTo(id).ExecuteSingle<NoitruPhanbuonggiuong>();
            if (noitruPhanbuonggiuong != null && Utility.Int32Dbnull(noitruPhanbuonggiuong.TrangThai, -1) == 1)
            {
                Utility.ShowMsg("Bạn không được phép hủy giường cho trạng thái đã chuyển khoa hoặc chuyển buồng giường", "Thông báo", MessageBoxIcon.Warning);
                grdList.Focus();
                return false;
            }
            return true;
        }
        void UpdateGridBG()
        {
            try
            {
                 DataRow dr = ((DataRowView)grdBuongGiuong.CurrentRow.DataRow).Row;
                 dr["ten_buong"] = "";
                 dr["ten_giuong"] = "";
                 dr["id_giuong"] = -1;
                 dr["id_buong"] = -1;
                 m_dtBG.AcceptChanges();
            }
            catch (Exception)
            {
                
                
            }
        }
        void HuyPhangiuong()
        {
            try
            {
                if (!isValidData_Huygiuong()) return;
                if (Utility.AcceptQuestion("Bạn có muốn hủy buồng giường cho bệnh nhân đang chọn không?", "Thông báo", true))
                {
                    int id = Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.Id));
                    NoitruPhanbuonggiuong objPhanbuonggiuong = NoitruPhanbuonggiuong.FetchByID(id);
                    if (objPhanbuonggiuong != null)
                    {

                        int idChuyen = -1;
                        //objPhanbuonggiuong.IdBuong = -1;
                        //objPhanbuonggiuong.IdGiuong = -1;
                        string id_buong_cu = Utility.sDbnull(objPhanbuonggiuong.IdBuong, "NAN");
                        string id_giuong_cu = Utility.sDbnull(objPhanbuonggiuong.IdGiuong, "NAN");
                        ActionResult actionResult = new noitru_nhapvien().HuyBenhNhanVaoBuongGuong(objPhanbuonggiuong, ref idChuyen);
                        switch (actionResult)
                        {
                            case ActionResult.Success:
                                Utility.Log(this.Name, globalVariables.UserName, string.Format("Hủy buồng giường cho người bệnh ID bệnh nhân={0}, PID={1}, từ id buồng cũ={2} ,id giường cũ={3} thành công ", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham, id_buong_cu,id_giuong_cu), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);

                                UpdateGridBG();
                               
                                if (idChuyen > 0)
                                {
                                    DataTable dtTemp = SPs.NoitruTimkiembenhnhanTheoid(idChuyen).GetDataSet().Tables[0];
                                    if (dtTemp.Rows.Count > 0)
                                    {
                                        DataRow dr = ((DataRowView)grdList.CurrentRow.DataRow).Row;
                                        Utility.CopyData(dtTemp.Rows[0], ref dr);
                                        _mDtTimKiembenhNhan.AcceptChanges();
                                    }
                                }
                                else//Xóa dòng hiện tại
                                {
                                    ProcessChuyenKhoa(id);
                                }
                                break;
                            case ActionResult.Error:
                                Utility.ShowMsg("Lỗi trong quá trình chuyển khoa", "Thông báo", MessageBoxIcon.Error);
                                break;
                        }
                    }
                }
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
        /// <summary>
        /// hàm thực hiện việc xóa thông tin phần buồng giường
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdHuyphangiuong_Click(object sender, EventArgs e)
        {

            HuyPhangiuong();
           
        }

        private void ProcessChuyenKhoa(int id)
        {
            DataRow query = (from khoa in _mDtTimKiembenhNhan.AsEnumerable()
                where
                    Utility.Int32Dbnull(khoa[NoitruPhanbuonggiuong.Columns.Id]) ==
                    Utility.Int32Dbnull(Utility.Int32Dbnull(id))
                select khoa).FirstOrDefault();
            if (query != null)
            {
                query["id_buong"] = -1;
                query["ten_buong"] = string.Empty;
                query[NoitruDmucGiuongbenh.Columns.IdGiuong] = -1;
                query["ten_giuong"] = string.Empty;
                query.AcceptChanges();
            }
        }

        private void grdList_SelectionChanged(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdList)) return;

            LayLichsuBuongGiuong();
            ModifyCommand();
        }
        DataTable m_dtBG;
        void LayLichsuBuongGiuong()
        {
            try
            {
                objLuotkham = Utility.getKcbLuotkham(Utility.Int64Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan)), Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham)));
                //Lấy tất cả lịch sử buồng giường
                m_dtBG =
                    new KCB_THAMKHAM().NoitruTimkiemlichsuBuonggiuong(
                        Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham)),
                        Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan)), "-1",-1);
                Utility.SetDataSourceForDataGridEx_Basic(grdBuongGiuong, m_dtBG, false, true, "1=1",
                    NoitruPhanbuonggiuong.Columns.NgayVaokhoa + " desc");
                grdBuongGiuong.MoveFirst();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Loi :"+ ex.Message);
            }
            finally
            {
                ShowLSuBuongGiuong();
            }
        }
        void ShowLSuBuongGiuong()
        {
            if (!Utility.isValidGrid(grdList) || grdBuongGiuong.GetDataRows().Length <= 1)
            {
                grdBuongGiuong.Width = 0;
            }
            else
            {
                grdBuongGiuong.Width = 425;
            }
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
            MaLuotkham = Utility.sDbnull(txtPatientCode.Text.Trim());
            if (!string.IsNullOrEmpty(MaLuotkham) && txtPatientCode.Text.Length < 8)
            {
                MaLuotkham = Utility.AutoFullPatientCode(txtPatientCode.Text);
                txtPatientCode.Text = MaLuotkham;
                txtPatientCode.Select(txtPatientCode.Text.Length, txtPatientCode.Text.Length);
            }
         
        }
        /// <summary>
        /// hàm thực hiện việc phím tắt thông tin 
        /// </summary>
        private string MaLuotkham { get; set; }
        private void frm_Quanlyphanbuonggiuong_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.F3)cmdTimKiem.PerformClick();
            if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
            if (e.KeyCode == Keys.F2)
            {
                txtPatientCode.Focus();
                txtPatientCode.SelectAll();
            }
            if(e.KeyCode==Keys.N&&e.Control)cmdThemMoiBN.PerformClick();
            if(e.KeyCode==Keys.U&&e.Control)cmdSuaThongTinBN.PerformClick();
        }
        private bool isValidData_HuyKhoa()
        {
            int id = Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.Id));
            string maluotkham = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
            int idBenhnhan = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
            KcbLuotkham kcbLuotkham = Utility.getKcbLuotkham(idBenhnhan, maluotkham);
            if (kcbLuotkham == null)
            {
                Utility.ShowMsg("Không lấy được thông tin Bệnh nhân. Đề nghị bạn cần chọn ít nhất 1 Bệnh nhân trên lưới");
                grdList.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(kcbLuotkham.TrangthaiNoitru, -1) == 4)
            {
                Utility.ShowMsg("Bệnh nhân đã được xác nhận dữ liệu nội trú để chuyển thanh toán nên không thể Hủy giường. Đề nghị bạn kiểm tra lại");
                grdList.Focus();
                return false;
            }
            if (kcbLuotkham.TrangthaiNoitru == 5)
            {
                Utility.ShowMsg("Bệnh nhân đã được duyệt thanh toán nội trú nên bạn không thể sửa lại trạng thái xác nhận dữ liệu nội trú");
                return false;
            }
            if (Utility.Byte2Bool(kcbLuotkham.TthaiThanhtoannoitru) || kcbLuotkham.TrangthaiNoitru == 6)
            {
                Utility.ShowMsg("Bệnh nhân đã được thanh toán nội trú nên bạn không thể sửa lại trạng thái xác nhận dữ liệu nội trú");
                return false;
            }
            var noitruPhieudieutri = new Select().From<NoitruPhieudieutri>()
                .Where(NoitruPhieudieutri.Columns.IdBuongGiuong).IsEqualTo(id)
                .And(NoitruPhieudieutri.Columns.MaLuotkham).IsEqualTo(maluotkham)
                .And(NoitruPhieudieutri.Columns.IdBenhnhan).IsEqualTo(idBenhnhan).ExecuteSingle<NoitruPhieudieutri>();
            if (noitruPhieudieutri != null)
            {
                Utility.ShowMsg("Đã có phiếu điều trị nội trú gắn với bệnh nhân tại khoa nội trú đang chọn nên bạn không thể hủy. Đề nghị xem lại", "Thông báo", MessageBoxIcon.Warning);
                grdList.Focus();
                return false;
            }
            var noitruPhanbuonggiuong = new Select().From<NoitruPhanbuonggiuong>()
                .Where(NoitruPhanbuonggiuong.Columns.Id).IsEqualTo(id).ExecuteSingle<NoitruPhanbuonggiuong>();
            if (noitruPhanbuonggiuong != null && Utility.Int32Dbnull(noitruPhanbuonggiuong.TrangThai, -1) == 1)
            {
                Utility.ShowMsg("Bạn không được phép hủy chuyển khoa cho trạng thái đã chuyển khoa hoặc chuyển buồng giường", "Thông báo", MessageBoxIcon.Warning);
                grdList.Focus();
                return false;
            }
            return true;
        }
        void HuychuyenKhoa()
        {
            try
            {
                if (!isValidData_HuyKhoa()) return;
                if (Utility.AcceptQuestion("Bạn có chắc chắn muốn hủy chuyển khoa nội trú. Sau khi hủy, Bệnh nhân sẽ quay về trạng thái khoa-buồng-giường trước đó", "Thông báo", true))
                {
                    int idChuyen = -1;
                    int id = Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.Id));
                    NoitruPhanbuonggiuong objPhanbuonggiuong = NoitruPhanbuonggiuong.FetchByID(id);
                    if (objPhanbuonggiuong != null)
                    {
                        string id_khoa_htai = Utility.sDbnull(objPhanbuonggiuong.IdKhoanoitru, "NAN");
                        string id_khoa_cu = Utility.sDbnull(objPhanbuonggiuong.IdKhoachuyen, "NAN");

                        objPhanbuonggiuong.IdBuong = -1;
                        objPhanbuonggiuong.IdGiuong = -1;
                        ActionResult actionResult = new noitru_nhapvien().HuyKhoanoitru(objPhanbuonggiuong, ref idChuyen);
                        switch (actionResult)
                        {
                            case ActionResult.Success:
                                Utility.Log(this.Name, globalVariables.UserName, string.Format("Hủy chuyển khoa cho người bệnh ID bệnh nhân={0}, PID={1}, từ id khoa hiện tại ={2} về id khoa cũ ={3} thành công", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham, id_khoa_htai, id_khoa_cu), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                                if (idChuyen > 0)
                                {
                                    DataTable dtTemp = SPs.NoitruTimkiembenhnhanTheoid(idChuyen).GetDataSet().Tables[0];
                                    if (dtTemp.Rows.Count > 0)
                                    {
                                        DataRow dr = ((DataRowView)grdList.CurrentRow.DataRow).Row;
                                        Utility.CopyData(dtTemp.Rows[0], ref dr);
                                        _mDtTimKiembenhNhan.AcceptChanges();
                                    }
                                }
                                else//Xóa dòng hiện tại
                                {
                                    DataRow dr = ((DataRowView)grdList.CurrentRow.DataRow).Row;
                                    _mDtTimKiembenhNhan.Rows.Remove(dr);
                                    _mDtTimKiembenhNhan.AcceptChanges();
                                }

                                break;
                            case ActionResult.Error:
                                Utility.ShowMsg("Lỗi trong quá trình chuyển khoa", "Thông báo", MessageBoxIcon.Error);
                                break;
                            case ActionResult.DataUsed:
                                NoitruPhanbuonggiuong _item = new Select().From(NoitruPhanbuonggiuong.Schema)
                                  .Where(NoitruPhanbuonggiuong.Columns.Id).IsEqualTo(idChuyen).ExecuteSingle<NoitruPhanbuonggiuong>();
                                if (_item != null &&  _item.IdBuong > 0 && _item.IdGiuong > 0)
                                {

                                    frm_lichsubuonggiuong _history = new frm_lichsubuonggiuong(_item.IdKhoanoitru, (int)_item.IdBuong, (int)_item.IdGiuong, true);
                                    _history.ShowDialog();
                                }
                                break;
                        }
                    }
                }
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
        private void cmdHuychuyenkhoa_Click(object sender, EventArgs e)
        {
            HuychuyenKhoa();
        }
        bool IsValidDeleteData()
        {
            try
            {
                string vMaLuotkham =
              Utility.sDbnull(
                grdList.GetValue(KcbLuotkham.Columns.MaLuotkham),
                  "");
                int vPatientId =
                     Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan), -1);
                var lstNoitruPhanbuonggiuong=new Select().From(NoitruPhanbuonggiuong.Schema)
                    .Where(NoitruPhanbuonggiuong.Columns.IdBenhnhan).IsEqualTo(vPatientId)
                    .And(NoitruPhanbuonggiuong.Columns.MaLuotkham).IsEqualTo(vMaLuotkham)
                    .And(NoitruPhanbuonggiuong.Columns.NoiTru).IsEqualTo(1)
                    .ExecuteAsCollection<NoitruPhanbuonggiuongCollection>();
               
                if (lstNoitruPhanbuonggiuong != null && lstNoitruPhanbuonggiuong.Count > 1)
                {
                    Utility.ShowMsg( "Bệnh nhân đã chuyển khoa hoặc chuyển giường nên bạn không thể xóa thông tin");
                    return false;
                }

                var objNoitruTamung = new Select().From(NoitruTamung.Schema)
                   .Where(NoitruTamung.Columns.IdBenhnhan).IsEqualTo(vPatientId)
                   .And(NoitruTamung.Columns.MaLuotkham).IsEqualTo(vMaLuotkham)
                   .ExecuteSingle<NoitruTamung>();

                if (objNoitruTamung != null )
                {
                    Utility.ShowMsg("Bệnh nhân đã nộp tiền tạm ứng nên bạn không thể xóa thông tin");
                    return false;
                }
                var objNoitruPhieudieutri = new Select().From(NoitruPhieudieutri.Schema)
                  .Where(NoitruPhieudieutri.Columns.IdBenhnhan).IsEqualTo(vPatientId)
                  .And(NoitruPhieudieutri.Columns.MaLuotkham).IsEqualTo(vMaLuotkham)
                  .ExecuteSingle<NoitruPhieudieutri>();

                if (objNoitruPhieudieutri != null)
                {
                    Utility.ShowMsg("Bệnh nhân đã Lập phiếu điều trị nên bạn không thể xóa thông tin");
                    return false;
                }
                var objKcbDonthuoc = new Select().From(KcbDonthuoc.Schema)
                  .Where(KcbDonthuoc.Columns.IdBenhnhan).IsEqualTo(vPatientId)
                  .And(KcbDonthuoc.Columns.MaLuotkham).IsEqualTo(vMaLuotkham)
                  .ExecuteSingle<KcbDonthuoc>();

                if (objKcbDonthuoc != null)
                {
                    Utility.ShowMsg("Bệnh nhân đã được kê đơn thuốc nên bạn không thể xóa thông tin");
                    return false;
                }
                var objKcbChidinhcl = new Select().From(KcbChidinhcl.Schema)
                  .Where(KcbChidinhcl.Columns.IdBenhnhan).IsEqualTo(vPatientId)
                  .And(KcbChidinhcl.Columns.MaLuotkham).IsEqualTo(vMaLuotkham)
                  .ExecuteSingle<KcbChidinhcl>();

                if (objKcbChidinhcl != null)
                {
                    Utility.ShowMsg("Bệnh nhân đã được lập phiếu chỉ định nên bạn không thể xóa thông tin");
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                Utility.CatchException("Lỗi khi kiểm tra hợp lệ dữ liệu trước khi cập nhật Bệnh nhân", ex);
                return false;
            }
        }
        void XoaBN()
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Bạn phải chọn ít nhất 1 bệnh nhân cấp cứu để xóa");
                    return;
                }
                string errMgs = "";
                string vMaLuotkham =
                   Utility.sDbnull(
                     grdList.GetValue(KcbLuotkham.Columns.MaLuotkham),
                       "");
                int vPatientId =
                     Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan), -1);

                if (!IsValidDeleteData()) return;
                if (Utility.AcceptQuestion("Bạn có muốn xóa Bệnh nhân cấp cứu này không", "Thông báo", true))
                {
                    myTrace.FunctionID = globalVariables.FunctionID;
                    myTrace.FunctionName = globalVariables.FunctionName;
                    ActionResult actionResult = new KCB_DANGKY().PerformActionDeletePatientExam(myTrace, vMaLuotkham,
                                                                                                       vPatientId, ref errMgs);
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            grdList.CurrentRow.BeginEdit();
                            grdList.CurrentRow.Delete();
                            grdList.CurrentRow.EndEdit();
                            grdList.UpdateData();
                            grdList_SelectionChanged(grdList, new EventArgs());
                            _mDtTimKiembenhNhan.AcceptChanges();
                            Utility.ShowMsg("Xóa Bệnh nhân cấp cứu thành công", "Thành công");
                            break;
                        case ActionResult.Exception:
                            if (errMgs != "")
                                Utility.ShowMsg(errMgs);
                            else
                                Utility.ShowMsg("Bệnh nhân đã có thông tin chỉ định dịch vụ hoặc đơn thuốc... /n bạn không thể xóa lần khám này", "Thông báo");
                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Có lỗi trong quá trình xóa thông tin", "Thông báo");
                            break;
                    }
                }

            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Có lỗi trong quá trình xóa thông tin" + ex.Message, "Thông báo");
            }
            finally
            {
                ModifyCommand();
            }
        }
        private void cmdXoaBN_Click(object sender, EventArgs e)
        {

            XoaBN();
        }

        private void cmdConfig_Click(object sender, EventArgs e)
        {
            var properties = new frm_Properties(PropertyLib._NoitruProperties);
            properties.ShowDialog();
        }
        private NoitruPhanbuonggiuong _noitruPhanbuonggiuong;
        private void cmdsuagiuong_Click(object sender, EventArgs e)
        {
            PhanBG();
            //try
            //{
            //    if (!isValidData_Phanbuonggiuong()) return;
            //    if (_noitruPhanbuonggiuong != null)
            //    {
            //        var frm = new frm_phanbuonggiuong
            //        {
            //            txtPatientDept_ID = { Text = Utility.sDbnull(_noitruPhanbuonggiuong.Id) },
            //            ObjPhanbuonggiuong = _noitruPhanbuonggiuong
            //        };
            //        frm.ShowDialog();
            //        if (!frm.BCancel)
            //        {
            //            LayLichsuBuongGiuong();
            //        }
            //    }
            //}
            //catch (Exception exception)
            //{
            //    Utility.ShowMsg("Lỗi:"+ exception.Message);
            //}
        }

        private bool isValidData_Xoagiuong()
        {
            int id = Utility.Int32Dbnull(grdBuongGiuong.GetValue(NoitruPhanbuonggiuong.Columns.Id));
            int idKhoanoitru = Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.IdKhoanoitru));
            string maluotkham = Utility.sDbnull(grdList.GetValue(KcbLuotkham.Columns.MaLuotkham));
            int idBenhnhan = Utility.Int32Dbnull(grdList.GetValue(KcbLuotkham.Columns.IdBenhnhan));
            KcbLuotkham kcbLuotkham = Utility.getKcbLuotkham(idBenhnhan, maluotkham);
            if (kcbLuotkham == null)
            {
                Utility.ShowMsg("Không lấy được thông tin Bệnh nhân. Đề nghị bạn cần chọn ít nhất 1 Bệnh nhân trên lưới");
                grdList.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(kcbLuotkham.TrangthaiNoitru, -1) == 4)
            {
                Utility.ShowMsg("Bệnh nhân đã được xác nhận dữ liệu nội trú để chuyển thanh toán nên không thể Hủy giường. Đề nghị bạn kiểm tra lại");
                grdList.Focus();
                return false;
            }
            if (kcbLuotkham.TrangthaiNoitru == 5)
            {
                Utility.ShowMsg("Bệnh nhân đã được duyệt thanh toán nội trú nên bạn không thể sửa lại trạng thái xác nhận dữ liệu nội trú");
                return false;
            }
            if (Utility.Byte2Bool(kcbLuotkham.TthaiThanhtoannoitru) || kcbLuotkham.TrangthaiNoitru == 6)
            {
                Utility.ShowMsg("Bệnh nhân đã được thanh toán nội trú nên bạn không thể sửa lại trạng thái xác nhận dữ liệu nội trú");
                return false;
            }
            if (_mDtKhoanoitru == null || _mDtKhoanoitru.Rows.Count <= 0 || _mDtKhoanoitru.Select(DmucKhoaphong.Columns.IdKhoaphong + "=" + idKhoanoitru).Length <= 0)
            {
                Utility.ShowMsg("Bạn không được quyền hủy giường của khoa này. Đề nghị bạn kiểm tra lại");
                grdList.Focus();
                return false;
            }
            var noitruPhieudieutri = new Select().From<NoitruPhieudieutri>()
                .Where(NoitruPhieudieutri.Columns.IdBuongGiuong).IsEqualTo(id)
                .And(NoitruPhieudieutri.Columns.MaLuotkham).IsEqualTo(maluotkham)
                .And(NoitruPhieudieutri.Columns.IdBenhnhan).IsEqualTo(idBenhnhan).ExecuteSingle<NoitruPhieudieutri>();
            if (noitruPhieudieutri != null)
            {
                Utility.ShowMsg("Đã có phiếu điều trị nội trú gắn với bệnh nhân tại buồng-giường đang chọn nên bạn không thể hủy. Đề nghị xem lại", "Thông báo", MessageBoxIcon.Warning);
                grdList.Focus();
                return false;
            }
            //var noitruPhanbuonggiuong = new Select().From<NoitruPhanbuonggiuong>()
            //    .Where(NoitruPhanbuonggiuong.Columns.Id).IsEqualTo(id).ExecuteSingle<NoitruPhanbuonggiuong>();
            //if (noitruPhanbuonggiuong != null && Utility.Int32Dbnull(noitruPhanbuonggiuong.TrangThai, -1) == 1)
            //{
            //    Utility.ShowMsg("Bạn không được phép hủy giường cho trạng thái đã chuyển khoa hoặc chuyển buồng giường", "Thông báo", MessageBoxIcon.Warning);
            //    grdList.Focus();
            //    return false;
            //}
            return true;
        }
        void XoaGiuong()
        {
            try
            {
                if (!isValidData_Xoagiuong()) return;
                int id = Utility.Int32Dbnull(grdBuongGiuong.CurrentRow.Cells[NoitruPhanbuonggiuong.Columns.Id].Value);
                if (id > 0)
                {
                    new Delete().From(NoitruPhanbuonggiuong.Schema)
                   .Where(NoitruPhanbuonggiuong.Columns.Id)
                   .IsEqualTo(id)
                   .Execute();
                    Utility.ShowMsg("Xóa giường của bệnh nhân thành công!", "Thông báo", MessageBoxIcon.Information);
                    LayLichsuBuongGiuong();
                }
                else
                {
                    Utility.ShowMsg("Không tồn tại mã phân buồng giường!", "Thông báo", MessageBoxIcon.Warning);
                }

            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi : " + ex.Message);
            }
        }
        private void cmdxoagiuong_Click(object sender, EventArgs e)
        {
            HuyPhangiuong();
        }

        
        private void cmdconfig2_Click(object sender, EventArgs e)
        {
            var properties = new frm_Properties(PropertyLib._NoitruProperties);
            properties.ShowDialog();
        }

        private void cmdInCamKet_Click(object sender, EventArgs e)
        {
            try
            {
                string malankham = Utility.sDbnull(grdList.CurrentRow.Cells[KcbLuotkham.Columns.MaLuotkham].Value);
                int idbenhnhan = Utility.Int32Dbnull(grdList.CurrentRow.Cells[KcbLuotkham.Columns.IdBenhnhan].Value);
                DataTable dt = SPs.NoitruLaythongtinbenhnhan(malankham, idbenhnhan).GetDataSet().Tables[0];
                noitru_baocao.InBanCamKetPhauThuat(dt,"GIẤY CAM ĐOAN PHẪU THUẬT",dtpNgayin.Value);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi: "+ ex.Message);
            }
        }

        private void cmdBienBanHoiChan_Click(object sender, EventArgs e)
        {
            try
            {
                string malankham = Utility.sDbnull(grdList.CurrentRow.Cells[KcbLuotkham.Columns.MaLuotkham].Value);
                int idbenhnhan = Utility.Int32Dbnull(grdList.CurrentRow.Cells[KcbLuotkham.Columns.IdBenhnhan].Value);
                var frm = new FrmTrichBienBanHoiChan();
                frm.idbenhnhan = idbenhnhan;
                frm.malankham = malankham;
                frm.ShowDialog();
                //DataTable dt = SPs.NoitruLaythongtinbenhnhan(malankham, idbenhnhan).GetDataSet().Tables[0];
                //noitru_baocao.InBanCamKetPhauThuat(dt, "GIẤY CAM ĐOAN PHẪU THUẬT", dtpNgayin.Value);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi: " + ex.Message);
            }
          
        }

        private void cmdInPhieuVaoVien_Click(object sender, EventArgs e)
        {
            try
            {
                string malankham = Utility.sDbnull(grdList.CurrentRow.Cells[KcbLuotkham.Columns.MaLuotkham].Value);
                int idbenhnhan = Utility.Int32Dbnull(grdList.CurrentRow.Cells[KcbLuotkham.Columns.IdBenhnhan].Value);
                DataTable dsTable =
               new noitru_nhapvien().NoitruLaythongtinInphieunhapvien(malankham, Utility.Int32Dbnull(idbenhnhan));
                if (dsTable.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy bản ghi nào\n Mời bạn xem lại", "Thông báo", MessageBoxIcon.Error);
                    return;
                }

                SqlQuery sqlQuery = new Select().From(KcbChandoanKetluan.Schema)
                    .Where(KcbChandoanKetluan.Columns.MaLuotkham).IsEqualTo(malankham)
                    .And(KcbChandoanKetluan.Columns.IdBenhnhan).IsEqualTo(idbenhnhan).OrderAsc(
                        KcbChandoanKetluan.Columns.NgayChandoan);
                var objInfoCollection = sqlQuery.ExecuteAsCollection<KcbChandoanKetluanCollection>();
                string chandoan = "";
                string machandoan = "";
                string mabenh = "";
                string phongkhamvaovien = "";
                string khoanoitru = "";
                string ten_benhcp = "";
                foreach (KcbChandoanKetluan objDiagInfo in objInfoCollection)
                {
                    string ICD_Name = "";
                    string ICD_Code = "";
                    GetChanDoan(Utility.sDbnull(objDiagInfo.MabenhChinh, ""),
                                Utility.sDbnull(objDiagInfo.MabenhPhu, ""), ref ICD_Name, ref ICD_Code);
                    chandoan += string.IsNullOrEmpty(objDiagInfo.Chandoan)
                                    ? ICD_Name
                                    : Utility.sDbnull(objDiagInfo.Chandoan);
                    mabenh += ICD_Code;
                }
                //DataTable dtDataChandoan = SPs.ThamkhamLaythongtinchandoan(machandoan).GetDataSet().Tables[0];
                //txtkbMa.Text = Utility.sDbnull(mabenh);
                //if (dtDataChandoan.Rows.Count > 0) chandoan = Utility.sDbnull(dtDataChandoan.Rows[0][0], "");
                chandoan += "," + ten_benhcp;
                //txtkbMa.Text = Utility.sDbnull(mabenh);

                DataSet ds = new noitru_nhapvien().KcbLaythongtinthuocKetquaCls(malankham, Utility.Int32Dbnull(idbenhnhan),(byte)0);
                DataTable dtThuoc = ds.Tables[0];
                DataTable dtketqua = ds.Tables[1];

                string[] query = (from thuoc in dtThuoc.AsEnumerable()
                                  let y = Utility.sDbnull(thuoc["ten_thuoc"])
                                  select y).ToArray();
                string donthuoc = string.Join(";", query);
                string[] querykq = (from kq in dtketqua.AsEnumerable()
                                    let y = Utility.sDbnull(kq["ketqua"])
                                    select y).ToArray();
                string ketquaCLS = string.Join("; ", querykq);




                bool donthuoclaytubangdulieu = THU_VIEN_CHUNG.Laygiatrithamsohethong("NHAPVIEN_THUOCDADUNG_LAYTUBANGDULIEU", "0", true) == "1";
                bool chandoanlaytubangdulieu = THU_VIEN_CHUNG.Laygiatrithamsohethong("NHAPVIEN_CHANDOAN_LAYTUBANGDULIEU", "0", true) == "1";
                bool kqclslaytubangdulieu = THU_VIEN_CHUNG.Laygiatrithamsohethong("NHAPVIEN_KQCLS_LAYTUBANGDULIEU", "0", true) == "1";

                DataRow dr = dsTable.Rows[0];
                if (dr != null)
                {
                    if (donthuoclaytubangdulieu)
                        dr["thuockedon"] = donthuoc;
                    if (chandoanlaytubangdulieu)
                        dr["CHANDOAN_VAOVIEN"] = chandoan;
                    if (kqclslaytubangdulieu)
                        dr["KETQUA_CLS"] = ketquaCLS;
                }
                NoitruPhieunhapvien objPNV = new Select().From(NoitruPhieunhapvien.Schema)
                          .Where(NoitruPhieunhapvien.Columns.MaLuotkham).IsEqualTo(malankham)
                          .And(NoitruPhieunhapvien.Columns.IdBenhnhan).IsEqualTo(idbenhnhan)
                          .ExecuteSingle<NoitruPhieunhapvien>();

                dsTable.AcceptChanges();
                VNS.HIS.UI.Baocao.noitru_baocao.Inphieunhapvien(objPNV,dsTable, "PHIẾU NHẬP VIỆN", globalVariables.SysDate);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi: " + ex.Message);
            }
        }

        private void GetChanDoan(string ICD_chinh, string IDC_Phu, ref string ICD_Name, ref string ICD_Code)
        {
            try
            {
                List<string> lstICD = ICD_chinh.Split(',').ToList();
                DmucBenhCollection _list =
                    new DmucBenhController().FetchByQuery(
                        DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, lstICD));
                foreach (DmucBenh _item in _list)
                {
                    ICD_Name += _item.TenBenh + ";";
                    ICD_Code += _item.MaBenh + ";";
                }
                lstICD = IDC_Phu.Split(',').ToList();
                _list =
                    new DmucBenhController().FetchByQuery(
                        DmucBenh.CreateQuery().AddWhere(DmucBenh.MaBenhColumn.ColumnName, Comparison.In, lstICD));
                foreach (DmucBenh _item in _list)
                {
                    ICD_Name += _item.TenBenh + ";";
                    ICD_Code += _item.MaBenh + ";";
                }
                if (ICD_Name.Trim() != "") ICD_Name = ICD_Name.Substring(0, ICD_Name.Length - 1);
                if (ICD_Code.Trim() != "") ICD_Code = ICD_Code.Substring(0, ICD_Code.Length - 1);
            }
            catch
            {
            }
        }

        private void cmdHuyphangiuong_Click_1(object sender, EventArgs e)
        {

        }

        private void cmdChuyenGiuong_Click_1(object sender, EventArgs e)
        {

        }

        private void cmdPhanGiuong_Click_1(object sender, EventArgs e)
        {

        }
        KcbLuotkham objKcbLuotkham = null;
        private void cmdHuyNhapvien_Click(object sender, EventArgs e)
        {
            try
            {
               
                if (!InValiHuyVaoVien()) return;
                if (Utility.AcceptQuestion("Bạn có muốn thực hiện việc hủy nhập viện cho bệnh nhân {0} không?\n Nếu hủy thì bệnh nhân sẽ trở lại ngoại trú?", "Thông báo", true))
                {

                    if (objKcbLuotkham != null)
                    {
                        ActionResult actionResult =
                      new noitru_nhapvien().Huynhapvien(
                     objKcbLuotkham);
                        switch (actionResult)
                        {
                            case ActionResult.Success:
                               
                                Utility.ShowMsg("Bạn hủy nội trú cho bệnh nhân thành công");
                                break;
                            case ActionResult.Error:
                                Utility.ShowMsg("Lỗi trong quá trình hủy nội trú cho bệnh nhân");
                                break;
                        }
                    }
                }
            }
            catch
            { }
            finally
            {
                ModifyCommand();
            }
        }
        private bool InValiHuyVaoVien()
        {
            int IdBenhnhan = Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.IdBenhnhan));
            int MaLuotkham = Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.MaLuotkham));
            objKcbLuotkham = new Select().From(KcbLuotkham.Schema)
                       .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(MaLuotkham)
                       .And(KcbLuotkham.Columns.IdBenhnhan).IsEqualTo(IdBenhnhan)
                       .ExecuteSingle<KcbLuotkham>();

            if (objKcbLuotkham != null && objKcbLuotkham.TrangthaiNoitru <= 0)
            {
                Utility.ShowMsg("Người bệnh đang ở trạng thái ngoại trú nên bạn không thể hủy nhập viện. Vui lòng kiểm tra lại");
                return false;
            }
            DataTable dtTU = SPs.NoitruTimkiemlichsuNoptientamung(objKcbLuotkham.MaLuotkham, objKcbLuotkham.IdBenhnhan, 0, -1, 1).GetDataSet().Tables[0];
            if (dtTU.Select("tthai_huy=0").Length > 0)
            {
                Utility.ShowMsg("Người bệnh đã phát sinh tiền tạm ứng nội trú. Đề nghị hủy tạm ứng trước khi hủy nhập viện");
                return false;
            }
            if (objKcbLuotkham != null && objKcbLuotkham.TrangthaiNoitru > 1)
            {
                Utility.ShowMsg("Bệnh nhân đã có dữ liệu nội trú nên không thể hủy nhập viện được. Mời bạn kiểm tra lại");
                return false;
            }

            NoitruPhanbuonggiuongCollection objNoitruPhanbuonggiuong = new Select().From(NoitruPhanbuonggiuong.Schema)
               .Where(NoitruPhanbuonggiuong.Columns.NoiTru).IsEqualTo(1)
               .And(NoitruPhanbuonggiuong.Columns.MaLuotkham).IsEqualTo(objKcbLuotkham.MaLuotkham).ExecuteAsCollection<NoitruPhanbuonggiuongCollection>();
            if (objNoitruPhanbuonggiuong != null && objNoitruPhanbuonggiuong.Count > 1)
            {
                Utility.ShowMsg("Bệnh nhân đã chuyển khoa hoặc chuyển giường nên bạn không thể hủy thông tin nhập viện");
                return false;
            }
            if (objNoitruPhanbuonggiuong != null && objNoitruPhanbuonggiuong.Count == 1 && Utility.Int32Dbnull(objNoitruPhanbuonggiuong[0].IdBuong, -1) > 0)
            {
                Utility.ShowMsg("Bệnh nhân đã phân buồng giường nên bạn không thể hủy thông tin nhập viện ", "Thông báo", MessageBoxIcon.Warning);
                return false;
            }

            NoitruTamung _NoitruTamung = new Select().From(NoitruTamung.Schema)
              .Where(NoitruTamung.Columns.MaLuotkham).IsEqualTo(objKcbLuotkham.MaLuotkham)
              .And(NoitruTamung.Columns.IdBenhnhan).IsEqualTo(objKcbLuotkham.IdBenhnhan).ExecuteSingle<NoitruTamung>();
            if (_NoitruTamung != null)
            {
                Utility.ShowMsg("Bệnh nhân này đã đóng tiền tạm ứng , Bạn không thể hủy nhập viện");
                return false;
            }
            return true;
        }
        void ChonBSDieutri()
        {
            try
            {
                if (!isValidData_Phanbuonggiuong()) return;
                int id = Utility.Int32Dbnull(grdList.GetValue(NoitruPhanbuonggiuong.Columns.Id));
                NoitruPhanbuonggiuong objPhanbuonggiuong = NoitruPhanbuonggiuong.FetchByID(id);
                objLuotkham = Utility.getKcbLuotkham(objPhanbuonggiuong.IdBenhnhan, objPhanbuonggiuong.MaLuotkham);
                //if (Utility.Int32Dbnull(objLuotkham.IdBsDieutrinoitruChinh, -1) > 0)
                //{
                //    DmucNhanvien nhanvien = DmucNhanvien.FetchByID(objLuotkham.IdBsDieutrinoitruChinh);
                //    if(nhanvien!=null)
                //        if (!Utility.AcceptQuestion(string.Format("Người bệnh {0} đang được giao cho bác sĩ điều trị chính {1}. Bạn có chắc chắn muốn đổi bác sĩ điều trị cho người bệnh này?", grdList.GetValue(KcbDanhsachBenhnhan.Columns.TenBenhnhan), nhanvien.TenNhanvien), "Cảnh báo", true)) return;
                //}
                frm_chonbacsidieutri _chonbacsidieutri = new frm_chonbacsidieutri(objPhanbuonggiuong.IdKhoanoitru, objLuotkham);
                _chonbacsidieutri._OnAccept += _chonbacsidieutri__OnAccept;
                _chonbacsidieutri.ShowDialog();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
               
            }
        }
        KcbLuotkham objLuotkham = null;
        private void cmdChonBs_Click(object sender, EventArgs e)
        {
            ChonBSDieutri();
        }

        void _chonbacsidieutri__OnAccept(object ID)
        {
            try
            {
                if (objLuotkham != null)
                {
                    objLuotkham.MarkOld();
                    objLuotkham.IsNew = false;
                    objLuotkham.IdBsDieutrinoitruChinh = Utility.Int32Dbnull( ID);
                    objLuotkham.IdBsDieutrinoitruPhu = -1;
                    objLuotkham.Save();
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
            
        }
        void Chuyenkhoasua()
        {
            try
            {
                if (objLuotkham == null)
                {
                    Utility.ShowMsg("Bạn cần chọn một người bệnh trên Danh sách Bệnh nhân bên tay trái để bắt đầu thực hiện công việc Chuyển khoa sửa");
                    return;
                }
                DmucKhoaphong _khoaphonghientai = DmucKhoaphong.FetchByID(objLuotkham.IdKhoanoitru);
                if (_khoaphonghientai == null)
                {
                    Utility.ShowMsg(string.Format("Không tìm được thông tin khoa điều trị nội trú hiện tại của người bệnh theo id khoa nội trú={0}. Vui lòng liên hệ IT để được hỗ trợ", Utility.sDbnull(objLuotkham.IdKhoanoitru)));
                    return;
                }
                string ten_nguoibenh = grdList.GetValue("ten_benhnhan").ToString();
                if (objLuotkham != null && Utility.Int16Dbnull(objLuotkham.IdKhoanoitru, -1) == Utility.Int16Dbnull(cboKhoanoitru.SelectedValue, -1))
                {
                    frm_ChuyenKhoa_Sua _chuyenkhoa = new frm_ChuyenKhoa_Sua(objLuotkham, ten_nguoibenh);
                    _chuyenkhoa.ShowDialog();
                }
                else
                {
                    Utility.ShowMsg(string.Format("Chỉ bác sĩ ở khoa bệnh nhân đang điều trị: {0} mới được phép chuyển quyền sửa cho các khoa khác. Vui lòng kiểm tra xem tài khoản đăng nhập có được phân quyền điều trị tại khoa: {1} hay không?", _khoaphonghientai.TenKhoaphong.ToUpper(), _khoaphonghientai.TenKhoaphong.ToUpper()));
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        private void cmdChuyenkhoasua_Click(object sender, EventArgs e)
        {
            Chuyenkhoasua();
        }

        private void cmdPhieuKhamVaoVien_Click(object sender, EventArgs e)
        {
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Vui lòng chọn một người bệnh trên danh sách bệnh nhân để bắt đầu thực hiện chức năng này");
                return;
            }
            frm_Nhapvien frm = new frm_Nhapvien();
            frm.CallfromParent = true;
            frm.objLuotkham = objLuotkham;
            frm.isReadonly = true;
            frm.ShowDialog();
        }

        private void cmdChanDoan_Click(object sender, EventArgs e)
        {
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Vui lòng chọn một người bệnh trên danh sách bệnh nhân để bắt đầu thực hiện chức năng này");
                return;
            }
            frm_ChandoanICD frm = new frm_ChandoanICD();
            frm.objLuotkham = objLuotkham;
            frm.CallfromParent = true;
            frm.ShowDialog();
        }

        private void cmdNgayNhapVien_Click(object sender, EventArgs e)
        {
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Vui lòng chọn một người bệnh trên danh sách bệnh nhân để bắt đầu thực hiện chức năng này");
                return;
            }
            frm_CapnhatNgaynhapvien _CapnhatNgaynhapvien = new frm_CapnhatNgaynhapvien();
            _CapnhatNgaynhapvien.ucThongtinnguoibenh1.txtMaluotkham.Text = objLuotkham.MaLuotkham;
            _CapnhatNgaynhapvien.ucThongtinnguoibenh1.Refresh();
            _CapnhatNgaynhapvien.ShowDialog();;
          
        }

        private void mnuChonBSDieutri_Click(object sender, EventArgs e)
        {
            ChonBSDieutri();
        }

        private void mnuBBanhoichan_Click(object sender, EventArgs e)
        {
            frm_ThemBbanhoichan ThemBbanhoichan = new frm_ThemBbanhoichan();
            ThemBbanhoichan.m_enAct = action.Insert;
            ThemBbanhoichan.ucThongtinnguoibenh_doc_v61.txtMaluotkham.Text = objLuotkham == null ? "" : objLuotkham.MaLuotkham;
            ThemBbanhoichan.CallfromParent = true;
            ThemBbanhoichan.ShowDialog();
        }

        private void mnuPhieusoket15ngay_Click(object sender, EventArgs e)
        {
            frm_PhieuSoKetDieuTri _PhieuSoKetDieuTri = new frm_PhieuSoKetDieuTri();
            _PhieuSoKetDieuTri.m_enAct = action.Insert;

            _PhieuSoKetDieuTri.ucThongtinnguoibenh_v21.txtMaluotkham.Text = objLuotkham == null ? "" : objLuotkham.MaLuotkham;
            _PhieuSoKetDieuTri.CallfromParent = true;
            _PhieuSoKetDieuTri.ShowDialog();
        }

        private void mnuTongketBA_Click(object sender, EventArgs e)
        {
            frm_TongKetBenhAn _TongKetBenhAn = new frm_TongKetBenhAn();
            _TongKetBenhAn.m_enAct = action.Insert;
            
            _TongKetBenhAn.ucThongtinnguoibenh_v21.txtMaluotkham.Text = objLuotkham == null ? "" : objLuotkham.MaLuotkham;
            _TongKetBenhAn.CallfromParent = true;
            _TongKetBenhAn.ShowDialog();
        }

        private void grdList_FormattingRow(object sender, Janus.Windows.GridEX.RowLoadEventArgs e)
        {

        }

        private void mnuHuygiuong_Click_1(object sender, EventArgs e)
        {

        }

        private void mnuIngiaynhapvien_Click(object sender, EventArgs e)
        {
            IN_PHIEU_KHAM_VAO_VIEN();
        }
        private void IN_PHIEU_KHAM_VAO_VIEN()
        {
            try
            {
                DataTable dsTable =
              new noitru_nhapvien().NoitruLaythongtinInphieunhapvien(objLuotkham.MaLuotkham, Utility.Int32Dbnull(objLuotkham.IdBenhnhan));
                if (dsTable.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy phiếu nhập viện của người bệnh đang chọn.\nVui lòng kiểm tra lại", "Thông báo", MessageBoxIcon.Error);
                    return;
                }

                SqlQuery sqlQuery = new Select().From(KcbChandoanKetluan.Schema)
                    .Where(KcbChandoanKetluan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                    .And(KcbChandoanKetluan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan).OrderAsc(
                        KcbChandoanKetluan.Columns.NgayChandoan);
                var objInfoCollection = sqlQuery.ExecuteAsCollection<KcbChandoanKetluanCollection>();
                string chandoan = "";
                string mabenh = "";
                string phongkhamvaovien = "";
                string khoanoitru = "";
                foreach (KcbChandoanKetluan objDiagInfo in objInfoCollection)
                {
                    string ICD_Name = "";
                    string ICD_Code = "";
                    GetChanDoan(Utility.sDbnull(objDiagInfo.MabenhChinh, ""),
                                Utility.sDbnull(objDiagInfo.MabenhPhu, ""), ref ICD_Name, ref ICD_Code);
                    chandoan += string.IsNullOrEmpty(objDiagInfo.Chandoan)
                                    ? ICD_Name
                                    : Utility.sDbnull(objDiagInfo.Chandoan);
                    mabenh += ICD_Code;
                }

                //txtkbMa.Text = Utility.sDbnull(mabenh);

                DataSet ds = new noitru_nhapvien().KcbLaythongtinthuocKetquaCls(objLuotkham.MaLuotkham, Utility.Int32Dbnull(objLuotkham.IdBenhnhan), (byte)0);
                DataTable dtThuoc = ds.Tables[0];
                DataTable dtketqua = ds.Tables[1];

                string[] query = (from thuoc in dtThuoc.AsEnumerable()
                                  let y = Utility.sDbnull(thuoc["ten_thuoc"])
                                  select y).ToArray();
                string donthuoc = string.Join(";", query);
                string[] querykq = (from kq in dtketqua.AsEnumerable()
                                    let y = Utility.sDbnull(kq["ketqua"])
                                    select y).ToArray();
                string ketquaCLS = string.Join("; ", querykq);


                bool tudongnaplai_thuoc_cls_khiin = THU_VIEN_CHUNG.Laygiatrithamsohethong("NHAPVIEN_TUDONGNAP_THUOC_KQCLS_KHIIN", "0", true) == "1";
                bool donthuoclaytubangdulieu = THU_VIEN_CHUNG.Laygiatrithamsohethong("NHAPVIEN_THUOCDADUNG_LAYTUBANGDULIEU", "0", true) == "1";
                bool chandoanlaytubangdulieu = THU_VIEN_CHUNG.Laygiatrithamsohethong("NHAPVIEN_CHANDOAN_LAYTUBANGDULIEU", "0", true) == "1";
                bool kqclslaytubangdulieu = THU_VIEN_CHUNG.Laygiatrithamsohethong("NHAPVIEN_KQCLS_LAYTUBANGDULIEU", "0", true) == "1";

                if (tudongnaplai_thuoc_cls_khiin)
                {
                    DataRow dr = dsTable.Rows[0];
                    if (dr != null)
                    {
                        if (donthuoclaytubangdulieu)
                            dr["thuockedon"] = donthuoc;
                        if (chandoanlaytubangdulieu)
                            dr["CHANDOAN_VAOVIEN"] = chandoan;
                        if (kqclslaytubangdulieu)
                            dr["KETQUA_CLS"] = ketquaCLS;
                    }
                }

                dsTable.AcceptChanges();
                NoitruPhieunhapvien objPNV = new Select().From(NoitruPhieunhapvien.Schema)
                        .Where(NoitruPhieunhapvien.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .And(NoitruPhieunhapvien.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        .ExecuteSingle<NoitruPhieunhapvien>();
                VNS.HIS.UI.Baocao.noitru_baocao.Inphieunhapvien(objPNV,dsTable, "PHIẾU NHẬP VIỆN", globalVariables.SysDate);
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
           
        }

        private void ctxFuntions_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void mnuHuychuyenkhoa_Click_1(object sender, EventArgs e)
        {

        }

        private void mnuChuyenkhoa_Click_1(object sender, EventArgs e)
        {

        }

        private void mnuChuyenNoitru_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Bạn cần chọn người bệnh điều trị ngoại trú trước khi thực hiện tính năng này");
                    return;
                }
                if(!Utility.AcceptQuestion(string.Format( "Bạn có chắc chắn muốn chuyển người bệnh {0} từ hướng điều trị ngoại trú sang hướng điều trị nội trú hay không?",grdList.GetValue("ten_benhnhan").ToString()),"Xác nhận chuyển hướng điều trị",true))
                    return;
                objLuotkham = Utility.getKcbLuotkham(grdList.CurrentRow);
                if (objLuotkham != null)
                {
                    new Update(KcbLuotkham.Schema).Set(KcbLuotkham.Columns.HuongDieutri).EqualTo("3").Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).Execute();
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Đã chuyển người bệnh ID bệnh nhân={0}, PID={1} vào hướng điều trị nội trú thành công", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                    Utility.ShowMsg("Đã chuyển người bệnh vào hướng điều trị nội trú thành công. Vui lòng thực hiện tính năng chuyển khoa(nếu có) hoặc báo cho khoa hiện tại thực hiện phân buồng giường và lập y lệnh thường qui");
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
              
            }
           
        }

        private void mnuChuyendieutringoaitru_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdList))
                {
                    Utility.ShowMsg("Bạn cần chọn người bệnh điều trị nội trú trước khi thực hiện tính năng này");
                    return;
                }
                if (!Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn chuyển người bệnh {0} từ hướng điều trị nội trú sang hướng điều trị ngoại trú hay không?",grdList.GetValue("ten_benhnhan").ToString()), "Xác nhận chuyển hướng điều trị", true))
                    return;
                objLuotkham = Utility.getKcbLuotkham(grdList.CurrentRow);
                if (objLuotkham != null)
                {
                    new Update(KcbLuotkham.Schema).Set(KcbLuotkham.Columns.HuongDieutri).EqualTo("4").Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham).Execute();
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Đã chuyển người bệnh ID bệnh nhân={0}, PID={1} về hướng điều trị ngoại trú thành công", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham), newaction.Update, this.GetType().Assembly.ManifestModule.Name);

                    Utility.ShowMsg("Đã chuyển người bệnh về hướng điều trị ngoại trú thành công. Vui lòng thực hiện tính năng chuyển khoa(nếu có) hoặc báo cho khoa hiện tại thực hiện y lệnh thường qui");
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);

            }
        }

        private void mnuBarcode_Click(object sender, EventArgs e)
        {
            try
            {
                if (objLuotkham != null)
                {
                    QueryCommand cmd = SysMultiReport.CreateQuery().BuildCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandSql = string.Format("select * from v_kcb_luotkham where id_benhnhan={0} and ma_luotkham='{1}'", objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham);
                    DataTable dt = DataService.GetDataSet(cmd).Tables[0];
                    if (!dt.Columns.Contains("barcodeID")) dt.Columns.Add("barcodeID", typeof(byte[]));
                    if (!dt.Columns.Contains("barcode")) dt.Columns.Add("barcode", typeof(byte[]));
                    THU_VIEN_CHUNG.CreateXML(dt, "barcode.xml");
                    if (dt.Rows.Count > 0)
                    {
                        VNS.HIS.UI.Forms.Dungchung.FrmBarCodePrint frm = new VNS.HIS.UI.Forms.Dungchung.FrmBarCodePrint(3);
                        frm.m_dtReport = dt;
                        frm.ShowDialog();
                    }
                }
                else
                {
                    Utility.ShowMsg("Bạn cần chọn người bệnh để thực hiện in tem");
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void mnuSuaPhieuNhapvien_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.Coquyen("noitru_suaphieunhapvien_trongkhoanoitru"))
                {
                    Utility.thongbaokhongcoquyen("noitru_suaphieunhapvien_trongkhoanoitru", "sửa thông tin phiếu nhập viện từ khoa nội trú");
                    return;
                }

                var frm = new frm_Nhapvien();
                frm.CallfromParent = true;
                frm.id_kham = -1;//Load lại từ phiếu nhập viện
                frm.id_bskham = -1;//Load lại từ phiếu nhập viện
                frm.objLuotkham = objLuotkham;
                frm.ucThongtinnguoibenh1.txtMaluotkham.Text = objLuotkham.MaLuotkham;
                frm.ShowDialog();
                if (frm.b_Cancel)
                {
                   
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        private void cmdInGiayRaVien_Click(object sender, EventArgs e)
        {
            try
            {

                Utility.WaitNow(this);
                DataTable dtData =
                    SPs.NoitruInphieuravien(Utility.DoTrim(objLuotkham.MaLuotkham)).GetDataSet().Tables[0];
                VMS.HIS.Bus.WordPrinter.InPhieu(null,dtData, "PHIEU_RAVIEN.doc"); 

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
}
}
