using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using NLog;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;

using VNS.HIS.UI.NGOAITRU;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;

using VNS.HIS.UI.Forms.NGOAITRU;
using VNS.HIS.UI.NOITRU;
using VNS.HIS.UI.DANHMUC;
using VNS.HIS.Classes;
using System.Globalization;
namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_Saochep_phieudieutri : Form
    {
        public delegate void OnCopyComplete();
        public DataTable m_dtPhieudieutri;
        public event OnCopyComplete _OnCopyComplete;
        public KcbLuotkham objLuotkham;
        public bool b_Cancel = false;
        bool mv_blnHasLoaded = false;
        public frm_Saochep_phieudieutri()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            dtNgaylapphieu.Value = globalVariables.SysDate;
            dtSaoChepNgay.CurrentDate = globalVariables.SysDate;
            
            InitEvents();
            InitTableStructure();
            CauHinh();
        }
        void InitEvents()
        {
            this.cmdPhieuDieuTri.Click += new System.EventHandler(this.cmdPhieuDieuTri_Click);
            this.dtSaoChepNgay.DatesChanged += new System.EventHandler(this.dtSaoChepNgay_DatesChanged);
            this.dtSaoChepNgay.Click += new System.EventHandler(this.dtSaoChepNgay_Click);
            this.chkViewAll.CheckedChanged += new System.EventHandler(this.chkViewAll_CheckedChanged);
            this.dtNgaylapphieu.ValueChanged += new System.EventHandler(this.dtNgayLapYLenh_ValueChanged);
            this.chkThuoc.CheckedChanged += new System.EventHandler(this.chkThuoc_CheckedChanged);
            this.chkVatTu.CheckedChanged += new System.EventHandler(this.chkVatTu_CheckedChanged);
            this.chkCLS.CheckedChanged += new System.EventHandler(this.chkCLS_CheckedChanged);
            chkchedotheodoi.CheckedChanged += chkchedotheodoi_CheckedChanged;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frm_Saochep_phieudieutri_FormClosing);
            this.Load += new System.EventHandler(this.frm_Saochep_phieudieutri_Load);
            cmdSaoChepPhieu.Click += cmdSaoChepPhieu_Click;
            cmdExit.Click += cmdExit_Click;
            cmdAddNgay.Click += cmdAddNgay_Click;
            grdPhieudieutrigoc.SelectionChanged+=grdPhieudieutrigoc_SelectionChanged;
            chkYlenh.CheckedChanged += chkYlenh_CheckedChanged;
            cboKhoanoitru.SelectedIndexChanged += cboKhoanoitru_SelectedIndexChanged;

        }

        void cboKhoanoitru_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!mv_blnHasLoaded) return;
            LaydanhsachPhieudieutri();
        }

        void chkchedotheodoi_CheckedChanged(object sender, EventArgs e)
        {
            if (!mv_blnHasLoaded) return;
            PropertyLib._NoitruProperties.SaochepDinhDuong= chkchedotheodoi.Checked;
            PropertyLib.SaveProperty(PropertyLib._NoitruProperties);  
        }

        void chkYlenh_CheckedChanged(object sender, EventArgs e)
        {
            if (!mv_blnHasLoaded) return;
            PropertyLib._NoitruProperties.SaochepYLenh = chkYlenh.Checked;
            PropertyLib.SaveProperty(PropertyLib._NoitruProperties);
        }
        private void CauHinh()
        {
            if (PropertyLib._NoitruProperties == null)
                PropertyLib._NoitruProperties = PropertyLib.GetNoitruProperties();
            chkViewAll.Checked = PropertyLib._NoitruProperties.Hienthitatcaphieudieutrikhisaochep;
            dtNgaylapphieu.Enabled = !chkViewAll.Checked;
            chkCLS.Checked = PropertyLib._NoitruProperties.SaochepCLS;
            chkThuoc.Checked = PropertyLib._NoitruProperties.SaochepThuoc;
            chkVatTu.Checked = PropertyLib._NoitruProperties.SaochepVTTH;
            chkYlenh.Checked = PropertyLib._NoitruProperties.SaochepYLenh;
            chkchedotheodoi.Checked = PropertyLib._NoitruProperties.SaochepDinhDuong;


        }
        private void frm_Saochep_phieudieutri_Load(object sender, EventArgs e)
        {
            dtSaoChepNgay.MinDate = objLuotkham.NgayNhapvien.Value;
            dtGioLapPhieu.Value = globalVariables.SysDate;
            DataBinding.BindDataCombox(cboKhoanoitru,
                                                THU_VIEN_CHUNG.LaydanhsachKhoanoitruTheoBacsi(globalVariables.UserName, Utility.Bool2byte(globalVariables.IsAdmin), (byte)1),
                                                DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong,
                                                "---Chọn khoa nội trú---", false);
            cboKhoanoitru.SelectedIndex = Utility.GetSelectedIndex(cboKhoanoitru, globalVariables.idKhoatheoMay.ToString());
            DataBinding.BindDataCombox(cboKhoaHientai,
                                                THU_VIEN_CHUNG.LaydanhsachKhoanoitruTheoBacsi(globalVariables.UserName, Utility.Bool2byte(globalVariables.IsAdmin), (byte)1),
                                                DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong,
                                                "---Chọn khoa nội trú---", false);
            //Khoa hiên tại để mặc định là khoa nội trú Bn đang điều trị
            cboKhoaHientai.SelectedIndex = Utility.GetSelectedIndex(cboKhoaHientai, Utility.sDbnull(objLuotkham.IdKhoanoitru, "0"));
            cboKhoaHientai.Enabled = globalVariables.IsAdmin || Utility.Int32Dbnull(cboKhoaHientai.SelectedValue, -1) <= 0;
            mv_blnHasLoaded = true;
            LaydanhsachPhieudieutri();
            Modifycommands();

           
           
        }

        private DataTable m_dNoitruPhieudieutri = new DataTable();
        private void cmdTimKiem_Click(object sender, EventArgs e)
        {
            LaydanhsachPhieudieutri();
        }
        private DataTable m_dtDataPhieuDT = new DataTable();
        /// <summary>
        /// hàm thực hiện viec khởi tạo thong tin dữ liệu khi load form
        /// </summary>
        /// <param name="objPhieudieutri"></param>
        private void LaythongtinDichvutheoPhieudieutri(NoitruPhieudieutri objPhieudieutri)
        {
            if (grdPhieudieutrigoc.CurrentRow != null)
            {

                m_dtDataPhieuDT = new noitru_phieudieutri().NoitruLaydulieuClsThuocVtthSaochep(Utility.Int32Dbnull(objPhieudieutri.IdPhieudieutri, -1),
                    Utility.Int32Dbnull(objPhieudieutri.IdBenhnhan, -1), Utility.sDbnull(objPhieudieutri.MaLuotkham, ""));
                Utility.SetDataSourceForDataGridEx(grdDichvuSaochep, m_dtDataPhieuDT, true, false, "", "");
                grdDichvuSaochep.CheckAllRecords();
            }
            else
            {
                m_dtDataPhieuDT.Clear();
                m_dtDataPhieuDT.AcceptChanges();

            }
        }
        private void LaydanhsachPhieudieutri()
        {
            try
            {
                string department_id = "-1";
                department_id = Utility.sDbnull(cboKhoanoitru.SelectedValue, globalVariables.idKhoatheoMay.ToString());
                bool IsAdmin = Utility.Coquyen("quyen_xemphieudieutricuabacsinoitrukhac");
                m_dtPhieudieutri = new KCB_THAMKHAM().NoitruTimkiemphieudieutriTheoluotkham(Utility.Bool2byte(IsAdmin), PropertyLib._NoitruProperties.Hienthitatcaphieudieutrikhisaochep ? "01/01/1900" : dtNgaylapphieu.Value.ToString("dd/MM/yyyy"), objLuotkham.MaLuotkham,
                    (int)objLuotkham.IdBenhnhan, department_id, 0);

                string _rowFilter = "1=1";
                Utility.SetDataSourceForDataGridEx_Basic(grdPhieudieutrigoc, m_dtPhieudieutri, false, true, _rowFilter, NoitruPhieudieutri.Columns.NgayDieutri + " desc");
                mv_blnHasLoaded = true;
                grdPhieudieutrigoc.MoveFirst();
                grdPhieudieutrigoc_SelectionChanged(grdPhieudieutrigoc, new EventArgs());
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                mv_blnHasLoaded = true;
            }
        }

        private void grdPhieudieutrigoc_SelectionChanged(object sender, EventArgs e)
        {
            if (!mv_blnHasLoaded ) return;
            if (Utility.isValidGrid( grdPhieudieutrigoc))
            {
                int v_Treat_ID = Utility.Int32Dbnull(grdPhieudieutrigoc.GetValue(NoitruPhieudieutri.Columns.IdPhieudieutri), -1);
                v_Treat_ID = Utility.Int32Dbnull(v_Treat_ID, -1);
                NoitruPhieudieutri objPhieudieutri = NoitruPhieudieutri.FetchByID(v_Treat_ID);
                LaythongtinDichvutheoPhieudieutri(objPhieudieutri);
                RowFilter();
            }
            
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void chkViewAll_CheckedChanged(object sender, EventArgs e)
        {
            if (!mv_blnHasLoaded) return;
            dtNgaylapphieu.Enabled = !chkViewAll.Checked;
            PropertyLib._NoitruProperties.Hienthitatcaphieudieutrikhisaochep = chkViewAll.Checked;
            PropertyLib.SaveProperty(PropertyLib._NoitruProperties);
            LaydanhsachPhieudieutri();
        }

        private void dtNgayLapYLenh_ValueChanged(object sender, EventArgs e)
        {
            if (!mv_blnHasLoaded) return;
            LaydanhsachPhieudieutri();
        }

        private void frm_Saochep_phieudieutri_FormClosing(object sender, FormClosingEventArgs e)
        {
            

        }

        private KcbDonthuocChitiet[] CreatePresDetail()
        {
            var query = from thuoc in grdDichvuSaochep.GetCheckedRows()
                        where Utility.Int32Dbnull(thuoc.Cells["id_loaithanhtoan"].Value)==3
                        select thuoc;
            var gridExRows = query as GridEXRow[] ?? query.ToArray();
            var arrDetail = new KcbDonthuocChitiet[gridExRows.Count()];
            int idx = 0;
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in gridExRows.AsEnumerable())
            {
                arrDetail[idx] = new KcbDonthuocChitiet();
                arrDetail[idx].IdChitietdonthuoc = Utility.Int32Dbnull(gridExRow.Cells["id_phieu_chitiet"].Value);
                arrDetail[idx].IdDonthuoc = Utility.Int32Dbnull(gridExRow.Cells["id_phieu"].Value);
                arrDetail[idx].SoLuong = Utility.DecimaltoDbnull(gridExRow.Cells["so_luong"].Value);
                arrDetail[idx].IdKho = Utility.Int16Dbnull(gridExRow.Cells["id_kho"].Value);
                  
                idx++;
            }
            return arrDetail;
        }
        private List< KcbDonthuocChitiet> CreatePresDetailVTTH()
        {
            var query = from thuoc in grdDichvuSaochep.GetCheckedRows()
                        where Utility.Int32Dbnull(thuoc.Cells["id_loaithanhtoan"].Value) ==5
                        select thuoc;
            var gridExRows = query as GridEXRow[] ?? query.ToArray();
            List<KcbDonthuocChitiet> lstVTTH = new List<KcbDonthuocChitiet>();
            int idx = 0;
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in gridExRows.AsEnumerable())
            {
                KcbDonthuocChitiet newItem = new KcbDonthuocChitiet();
                newItem.IdDonthuoc = Utility.Int32Dbnull(gridExRow.Cells["id_phieu"].Value);
                newItem.IdChitietdonthuoc = Utility.Int32Dbnull(gridExRow.Cells["id_phieu_chitiet"].Value);
                newItem.SoLuong = Utility.DecimaltoDbnull(gridExRow.Cells["so_luong"].Value);
                newItem.IdKho = Utility.Int16Dbnull(gridExRow.Cells["id_kho"].Value);
                lstVTTH.Add(newItem);
            }
            return lstVTTH;
        }
        private KcbChidinhclsChitiet[] CreateChiDinhCLS()
        {
            var query = from thuoc in grdDichvuSaochep.GetCheckedRows()
                        where Utility.Int32Dbnull(thuoc.Cells["id_loaithanhtoan"].Value) == 2
                        select thuoc;
            var gridExRows = query as GridEXRow[] ?? query.ToArray();
            var arrDetail = new KcbChidinhclsChitiet[gridExRows.Count()];
            int idx = 0;
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in gridExRows.AsEnumerable())
            {
                arrDetail[idx] = new KcbChidinhclsChitiet();
                arrDetail[idx].IdChitietchidinh = Utility.Int32Dbnull(gridExRow.Cells["id_phieu_chitiet"].Value);
                arrDetail[idx].SoLuong = Utility.DecimaltoDbnull(gridExRow.Cells["so_luong"].Value);
                idx++;
            }
            return arrDetail;
        }

        private NoitruPhieudieutri[] TaoPhieudieutriNoitru()
        {
            int idx = 0;
            NoitruPhieudieutri[] objPhieudieutri = new NoitruPhieudieutri[grdNgaysaochep.GetDataRows().Length];
            foreach (GridEXRow ngaythang in grdNgaysaochep.GetDataRows())
            {
                objPhieudieutri[idx]=new NoitruPhieudieutri();
                objPhieudieutri[idx].MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham);
                objPhieudieutri[idx].IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan);
                objPhieudieutri[idx].NgayDieutri = Convert.ToDateTime(ngaythang.Cells[NoitruPhieudieutri.Columns.NgayDieutri].Value);
                objPhieudieutri[idx].TenMaytao = globalVariables.gv_strComputerName;
                objPhieudieutri[idx].IpMaytao = globalVariables.gv_strIPAddress;
                objPhieudieutri[idx].IdKhoanoitru = Utility.Int16Dbnull(cboKhoaHientai.SelectedValue, globalVariables.idKhoatheoMay);
                objPhieudieutri[idx].GioDieutri = dtGioLapPhieu.Text;
               // objPhieudieutri[idx].GioDieutri = Utility.GetValueFromGridColumn(ngaythang,NoitruPhieudieutri.Columns.GioDieutri) ;//;
                objPhieudieutri[idx].TthaiIn = 0;
                objPhieudieutri[idx].IdBacsi = globalVariables.gv_intIDNhanvien;
                objPhieudieutri[idx].Thu = Utility.ConvertDayVietnamese(Convert.ToDateTime(ngaythang.Cells[NoitruPhieudieutri.Columns.NgayDieutri].Value).DayOfWeek.ToString());
                if (chkYlenh.Checked)
                {
                    objPhieudieutri[idx].ThongtinTheodoi = Utility.GetValueFromGridColumn(grdPhieudieutrigoc, NoitruPhieudieutri.Columns.ThongtinTheodoi);
                    objPhieudieutri[idx].ThongtinDieutri = Utility.GetValueFromGridColumn(grdPhieudieutrigoc, NoitruPhieudieutri.Columns.ThongtinDieutri);
                }
                idx++;
                // objPhieudieutri[idx]=new NoitruPhieudieutri();
            }
            return objPhieudieutri;
        }
        private void cmdSaoChepPhieu_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdPhieudieutrigoc))
            {
                Utility.ShowMsg("Bạn cần chọn phiếu điều trị gốc để thực hiện sao chép", "Thông báo", MessageBoxIcon.Error);
                grdPhieudieutrigoc.Focus();
                return;

            }
            if (grdDichvuSaochep.GetCheckedRows().Length <= 0)
            {
                Utility.ShowMsg("Bạn cần chọn ít nhất 1 dịch vụ cần sao chép", "Thông báo", MessageBoxIcon.Error);
                grdDichvuSaochep.Focus();
                return;

            }
            if (Utility.Int16Dbnull(cboKhoaHientai.SelectedValue, -1) < 0)
            {
                Utility.ShowMsg("Bạn cần chọn khoa nội trú hiện tại đang lập phiếu điều trị cho bệnh nhân", "Thông báo", MessageBoxIcon.Error);
                cboKhoaHientai.Focus();
                return;
            }
            if (grdNgaysaochep.GetDataRows().Count() <= 0)
            {
                Utility.ShowMsg("Bạn cần chọn ít nhất một ngày cần sao chép", "Thông báo", MessageBoxIcon.Error);
                cmdAddNgay.Focus();
                return;
            }
            bool moreAsk = true;
            if (Utility.Int16Dbnull(cboKhoaHientai.SelectedValue, -1) != Utility.Int16Dbnull(objLuotkham.IdKhoanoitru, -1))
            {
                if (!Utility.AcceptQuestion("Bệnh nhân đang nằm ở khoa nội trú khác với khoa Hiện tại bạn đang chọn. Bạn có chắc chắn đang tạo các phiếu điều trị cho khoa " + cboKhoaHientai.Text + " hay không?", "Thông báo", true))
                {
                    moreAsk = false;
                    cboKhoaHientai.Focus();
                    return;
                }
            }
            if(moreAsk)
                if (!Utility.AcceptQuestion("Bạn đã chắc chắn muốn sao chép phiếu điều trị cho các ngày bạn đang chọn không?", "Thông báo", true))
                {
                    return;
                }
            NoitruPhieudieutri[] objPhieudieutri = TaoPhieudieutriNoitru();
            ActionResult actionResult = new noitru_phieudieutri().SaoChepPhieuDieuTri(objPhieudieutri, objLuotkham, CreateChiDinhCLS(), CreatePresDetail(),CreatePresDetailVTTH());
            switch (actionResult)
            {
                case ActionResult.Success:
                    b_Cancel = true;
                    if (_OnCopyComplete != null)
                    {
                        _OnCopyComplete();
                    }
                    Utility.ShowMsg("Bạn sao chép thành công. Nhấn OK để kết thúc", "Thông báo");
                    break;
                case ActionResult.Error:
                    Utility.ShowMsg("Lỗi trong quá trình sao chép", "Thông báo");
                    break;
            }


        }
       

        private void RowFilter()
        {
            string _rowFilter = "1=1";
            string _rowFilterCLS = "";
            string _rowFilterThuoc = "";
            string _rowFilterVatTu = "";
            if (chkCLS.Checked)
            {
                _rowFilterCLS = string.Format("{0}={1}", "id_loaithanhtoan", 2);
            }
            if (chkThuoc.Checked)
            {
                _rowFilterThuoc = string.Format("{0}={1}", "id_loaithanhtoan", 3);
            }
            if (chkVatTu.Checked)
            {
                _rowFilterVatTu = string.Format("{0}={1}", "id_loaithanhtoan", 5);
            }
            _rowFilter = string.Format("{0} or {1} or {2}", string.IsNullOrEmpty(_rowFilterCLS) ? "1=2" : _rowFilterCLS,
                string.IsNullOrEmpty(_rowFilterThuoc) ? "1=2" : _rowFilterThuoc,
                string.IsNullOrEmpty(_rowFilterVatTu) ? "1=2" : _rowFilterVatTu);

            m_dtDataPhieuDT.DefaultView.RowFilter = _rowFilter;
            m_dtDataPhieuDT.AcceptChanges();
        }
        private void chkCLS_CheckedChanged(object sender, EventArgs e)
        {
            if (!mv_blnHasLoaded) return;
            PropertyLib._NoitruProperties.SaochepCLS = chkCLS.Checked;
            PropertyLib.SaveProperty(PropertyLib._NoitruProperties);
            RowFilter();
        }

        private void chkThuoc_CheckedChanged(object sender, EventArgs e)
        {
            if (!mv_blnHasLoaded) return;
            PropertyLib._NoitruProperties.SaochepThuoc = chkThuoc.Checked;
            PropertyLib.SaveProperty(PropertyLib._NoitruProperties);
            RowFilter();
        }

        private void chkVatTu_CheckedChanged(object sender, EventArgs e)
        {
            if (!mv_blnHasLoaded) return;
            PropertyLib._NoitruProperties.SaochepVTTH = chkVatTu.Checked;
            PropertyLib.SaveProperty(PropertyLib._NoitruProperties);
            RowFilter();
        }

        private DataTable m_dtDanhsachNgaysaochep;
        private void dtSaoChepNgay_DatesChanged(object sender, EventArgs e)
        {
          
        }

        private int getmaxPhieuDieuTri()
        {
            int sophieu = 0;
            var query = from phieu in m_dtDanhsachNgaysaochep.AsEnumerable()
                let y = Utility.Int32Dbnull(phieu["STT"])
                select y;
            if (query.Any())
                sophieu = Utility.Int32Dbnull(query.Max());
            else sophieu = 0;
            return sophieu + 1;
        }
        private void BindNgayDieutri()
        {
            //if (Utility.Int16Dbnull(cboKhoaHientai.SelectedValue, -1) < 0)
            //{
            //    Utility.ShowMsg("Bạn cần chọn khoa nội trú cần lập phiếu điều trị cho bệnh nhân", "Thông báo", MessageBoxIcon.Error);
            //    cboKhoaHientai.Focus();
            //    return;
            //}
            ////Kiểm tra có phải đang sao chép cho một khoa ở quá khứ hay không.Nếu đúng thì phải kiểm tra từng ngày tạo
            //if (Utility.Int32Dbnull(objLuotkham.IdKhoanoitru, -1) != Utility.Int16Dbnull(cboKhoaHientai.SelectedValue, -1))
            //{
            //    NoitruPhanbuonggiuongCollection lstPhanbuonggiuong=new Select().From(NoitruPhanbuonggiuong.Schema)
            //        .Where(NoitruPhanbuonggiuong.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
            //        .And(NoitruPhanbuonggiuong.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
            //        .And(NoitruPhanbuonggiuong.Columns.IdKhoanoitru).IsEqualTo(Utility.Int16Dbnull(cboKhoaHientai.SelectedValue, -1))
            //        .ExecuteAsCollection<NoitruPhanbuonggiuongCollection>();
            //}
            DateTime[] ngaythangRange = dtSaoChepNgay.SelectedDates;
            List<string> lstErrr = new List<string>();
          //  m_dtDanhsachNgaysaochep.Clone();
           List<int> lstHHmmss= (from p in dtGioLapPhieu.Text.Split(':').AsEnumerable()
                                     select Utility.Int32Dbnull(p,0)).ToList<int>();
            foreach (var ngaythang in ngaythangRange)
            {
                string err ="";
                DateTime ngay_lapphieu = new DateTime(ngaythang.Year, ngaythang.Month, ngaythang.Day, lstHHmmss[0], lstHHmmss[1], lstHHmmss[2]);
                if (ngay_lapphieu > dtpNgaynhapvien.Value)
                {
                    var query = from ngay in m_dtDanhsachNgaysaochep.AsEnumerable()
                                where Convert.ToDateTime(ngay[NoitruPhieudieutri.Columns.NgayDieutri]).Date == ngaythang.Date
                                select ngay;
                    NoitruPhieudieutri objPDT = new Select().From(NoitruPhieudieutri.Schema)
                        .Where(NoitruPhieudieutri.Columns.NgayDieutri).IsEqualTo(ngaythang.Date)
                        .And(NoitruPhieudieutri.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .And(NoitruPhieudieutri.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        .ExecuteSingle<NoitruPhieudieutri>();

                    if (!query.Any() && objPDT == null)
                    {
                        DataRow dr = m_dtDanhsachNgaysaochep.NewRow();
                        dr["STT"] = Utility.Int32Dbnull(getmaxPhieuDieuTri());
                        dr[NoitruPhieudieutri.Columns.NgayDieutri] = ngaythang;
                        dr["sNgay_Dieutri"] = ngaythang.Date;
                        dr[NoitruPhieudieutri.Columns.GioDieutri] = dtGioLapPhieu.Text;
                        dr[NoitruPhieudieutri.Columns.Thu] = Utility.ConvertDayVietnamese(ngaythang.DayOfWeek.ToString());
                        m_dtDanhsachNgaysaochep.Rows.Add(dr);
                    }
                    else
                    {
                         err = string.Format("Ngày điều trị {0} đã có phiếu nên hệ thống sẽ không tạo", ngaythang.Date.ToString("dd/MM/yyyy"));
                    }
                }
                else
                {
                     err = string.Format("Ngày điều trị {0}  <= ngày nhập viện {1} nên hệ thống sẽ không tạo",ngay_lapphieu.ToString("dd/MM/yyyy HH:mm:ss"), dtpNgaynhapvien.Text);
                }
                if (err.Length > 0)
                    lstErrr.Add(err);
            }
            Utility.SetDataSourceForDataGridEx(grdNgaysaochep, m_dtDanhsachNgaysaochep, false, true, "1=1", NoitruPhieudieutri.Columns.NgayDieutri);
            Utility.SetGridEXSortKey(grdNgaysaochep, NoitruPhieudieutri.Columns.NgayDieutri, Janus.Windows.GridEX.SortOrder.Ascending);
            Modifycommands();
            if (lstErrr.Count > 0)
                Utility.ShowMsg(string.Format(@"Cảnh báo:\n{0}",string.Join(@"\n",lstErrr.ToArray<string>())));
        }
        void InitTableStructure()
        {
            try
            {
                m_dtDanhsachNgaysaochep = new DataTable();
                m_dtDanhsachNgaysaochep.Columns.AddRange(new DataColumn[] { 
                new DataColumn("STT",typeof(Int32)),
                new DataColumn(NoitruPhieudieutri.Columns.NgayDieutri,typeof(DateTime)),
                new DataColumn("THU",typeof(string)),
                new DataColumn("sNgay_Dieutri",typeof(string)),
                new DataColumn(NoitruPhieudieutri.Columns.GioDieutri,typeof(string)),
                });
            }
            catch (Exception ex)
            {
                
                
            }
        }
        void Modifycommands()
        {
            var query = from phieu in m_dtDanhsachNgaysaochep.AsEnumerable()
                        select phieu;
            cmdPhieuDieuTri.Enabled = query.Any();
            cmdSaoChepPhieu.Enabled = query.Any();
        }
        private void dtSaoChepNgay_Click(object sender, EventArgs e)
        {
            
        }

        private void cmdPhieuDieuTri_Click(object sender, EventArgs e)
        {
            var query = from phieu in m_dtDanhsachNgaysaochep.AsEnumerable()
                where
                    Utility.Int32Dbnull(phieu["STT"]) ==
                    Utility.Int32Dbnull(grdNgaysaochep.GetValue("STT"))
                select phieu;
            if (query.Any())
            {
                var firstrow = query.FirstOrDefault();
                if(firstrow!=null)firstrow.Delete();
                m_dtDanhsachNgaysaochep.AcceptChanges();
            }
            Modifycommands();
           
        }

        private void PhieuDieuTri_Opening(object sender, CancelEventArgs e)
        {
            Modifycommands();
        }

        private void cmdAddNgay_Click(object sender, EventArgs e)
        {
            BindNgayDieutri();
        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void cmdUpdategiodieutri_Click(object sender, EventArgs e)
        {
            List<string> lstErrr = new List<string>();
            List<int> lstHHmmss = (from p in dtGioLapPhieu.Text.Split(':').AsEnumerable()
                                   select Utility.Int32Dbnull(p, 0)).ToList<int>();
            foreach (DataRow dr in m_dtDanhsachNgaysaochep.Rows)
            {
                string err = "";
                DateTime _date = Convert.ToDateTime(dr[NoitruPhieudieutri.Columns.NgayDieutri]);
                DateTime ngay_lapphieu = new DateTime(_date.Year, _date.Month, _date.Day, lstHHmmss[0], lstHHmmss[1], lstHHmmss[2]);
                if (ngay_lapphieu > dtpNgaynhapvien.Value)
                {
                    dr[NoitruPhieudieutri.Columns.GioDieutri] = dtGioLapPhieu.Text;
                }
                else
                {
                    err = string.Format("Ngày điều trị {0}  <= ngày nhập viện {1} nên hệ thống sẽ không tạo", ngay_lapphieu.ToString("dd/MM/yyyy HH:mm:ss"), dtpNgaynhapvien.Text);
                }
                if (err.Length > 0)
                    lstErrr.Add(err);
            }
             if (lstErrr.Count > 0)
                Utility.ShowMsg(string.Format(@"Cảnh báo:\n{0}",string.Join(@"\n",lstErrr.ToArray<string>())));
            m_dtDanhsachNgaysaochep.AcceptChanges();
        }
    }
}
