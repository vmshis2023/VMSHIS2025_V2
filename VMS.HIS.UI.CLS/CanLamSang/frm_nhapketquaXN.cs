using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using Janus.Windows.CalendarCombo;
using Janus.Windows.EditControls;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using Janus.Windows.UI.Tab;
using Microsoft.VisualBasic;
using NLog;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;

using VNS.HIS.UI.NGOAITRU;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;

using VNS.HIS.UI.Forms.NGOAITRU;
using VNS.HIS.UI.DANHMUC;
using VNS.HIS.Classes;

namespace VNS.HIS.UI.Forms.CanLamSang
{
    public partial class frm_nhapketquaXN : Form
    {
        string ma_luotkham = "";
        Int64 id_benhnhan = -1;
        string MaBenhpham = "";
        string MaChidinh = "";
        int IdChitietdichvu = -1;
        int currRowIdx = -1;
        int id_chidinh = -1;
        int id_dichvu = -1;
        int co_chitiet = -1;
        public frm_nhapketquaXN()
        {
            InitializeComponent();
            InitEvents();
        }
        void InitEvents()
        {
            this.Load += frm_nhapketquaXN_Load;
            this.KeyDown += frm_nhapketquaXN_KeyDown;
            cmdSearch.Click += cmdSearch_Click;
            txtMaluotkham.KeyDown += txtMaluotkham_KeyDown;
            txtMabenhpham.KeyDown += txtMabenhpham_KeyDown;
            grdChidinh.CurrentCellChanged += grdChidinh_CurrentCellChanged;
            grdKetqua.UpdatingCell += grdKetqua_UpdatingCell;
            mnuCancelResult.Click += mnuCancelResult_Click;
            cmdConfirm.Click += cmdConfirm_Click;
        }

        void cmdConfirm_Click(object sender, EventArgs e)
        {
            
        }
        long id_chidinhchitiet = -1;
        public void SetParams(string ma_luotkham,string ma_benhpham, long id_chidinhchitiet )
        {
            txtMabenhpham.Text = ma_benhpham;
            txtMaluotkham.Text = ma_luotkham;
            this.id_chidinhchitiet = id_chidinhchitiet;
            grpThongTinBN.Height = 0;
            //pnlChidinh.Width = 0;
            this.Size = new Size(1024, 768);
            grdChidinh.RootTable.Groups.Clear();
            SearchData();
            grdChidinh_CurrentCellChanged(grdChidinh, new EventArgs());
        }
        void mnuCancelResult_Click(object sender, EventArgs e)
        {
            if (grdKetqua.SelectedItems.Count > 1)
                if (!Utility.AcceptQuestion("Bạn có chắc chắn muốn hủy kết quả các xét nghiệm đang chọn", "Hủy kết quả", true))
                    return;
            List< KcbKetquaCl> lstResult =new List<KcbKetquaCl>();
            List<KcbChidinhclsChitiet> lstDetails = new List<KcbChidinhclsChitiet>();
            foreach (GridEXRow row in grdKetqua.SelectedItems)
            {
                KcbKetquaCl _item = null;
                KcbChidinhclsChitiet _itemchitiet = null;
                try
                {
                    int id_kq = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(row, KcbKetquaCl.Columns.IdKq), -1);
                    int IdChitietchidinh = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(row, KcbChidinhclsChitiet.Columns.IdChitietchidinh), -1);
                    int IdChitietdichvu = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(row, DmucDichvuclsChitiet.Columns.IdChitietdichvu), -1);
                    _itemchitiet = KcbChidinhclsChitiet.FetchByID(IdChitietchidinh);
                    _itemchitiet.IsNew = false;
                    _itemchitiet.MarkOld();
                    if (id_kq > 0)
                    {
                        _item = KcbKetquaCl.FetchByID(id_kq);
                        _item.IsNew = false;
                        _item.NguoiSua = globalVariables.UserName;
                        _item.NgaySua = globalVariables.SysDate;
                        _item.IpMaysua = globalVariables.gv_strIPAddress;
                        _item.TenMaysua = globalVariables.gv_strComputerName;
                        _item.MarkOld();
                    }
                    else
                    {
                        _item = new KcbKetquaCl();
                        _item.IsNew = true;
                        _item.NguoiTao = globalVariables.UserName;
                        _item.NgayTao = globalVariables.SysDate;
                        _item.IpMaytao = globalVariables.gv_strIPAddress;
                        _item.TenMaytao = globalVariables.gv_strComputerName;
                    }
                    DmucDichvuclsChitiet objcls = DmucDichvuclsChitiet.FetchByID(IdChitietdichvu);
                    if (objcls != null)
                    {
                        _item.MaChidinh = Utility.GetValueFromGridColumn(grdChidinh, KcbChidinhcl.Columns.MaChidinh);
                        _item.MaBenhpham = Utility.GetValueFromGridColumn(grdChidinh, KcbChidinhcl.Columns.MaBenhpham);
                        _item.Barcode = Utility.GetValueFromGridColumn(grdChidinh, KcbChidinhcl.Columns.Barcode);
                        _item.IdBenhnhan = id_benhnhan;
                        _item.MaLuotkham = ma_luotkham;
                        _item.IdChidinh = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(row, KcbChidinhclsChitiet.Columns.IdChidinh), -1);
                        _item.IdChitietchidinh = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(row, KcbChidinhclsChitiet.Columns.IdChitietchidinh), -1);
                        _item.IdDichvu = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(row, KcbChidinhclsChitiet.Columns.IdDichvu), -1);
                        _item.IdDichvuchitiet = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(row, KcbChidinhclsChitiet.Columns.IdChitietdichvu), -1);
                        _item.Barcode = Utility.GetValueFromGridColumn(grdChidinh, KcbChidinhcl.Columns.Barcode);
                        _item.SttIn = objcls.SttHthi;
                        _item.BtNam = objcls.BinhthuongNam;
                        _item.BtNu = objcls.BinhthuongNu;
                        _item.KetQua = Utility.sDbnull(Utility.GetValueFromGridColumn(row, KcbChidinhclsChitiet.Columns.KetQua), -1);
                        if (_item.TrangThai < 3)
                            _item.TrangThai = 3;
                        if (chkSaveAndConfirm.Checked)
                            _item.TrangThai = 4;//Duyệt luôn để hiển thị trên form thăm khám của bác sĩ

                        if (Utility.DoTrim(_item.KetQua) == "")
                            _item.TrangThai = 2;//Quay ve trang thai chuyen đang thực hiện
                        //_item.TenDonvitinh = objcls.TenDonvitinh;
                        _itemchitiet.KetQua = Utility.sDbnull(Utility.GetValueFromGridColumn(row, KcbChidinhclsChitiet.Columns.KetQua), -1);
                        if (_itemchitiet.TrangThai < 3)
                            _itemchitiet.TrangThai = 3;
                        if (chkSaveAndConfirm.Checked)
                            _itemchitiet.TrangThai = 4;//Duyệt luôn để hiển thị trên form thăm khám của bác sĩ
                        if (Utility.DoTrim(_itemchitiet.KetQua) == "")
                            _itemchitiet.TrangThai = 1;//Quay ve trang thai chuyen can
                        _item.TenThongso = "";
                        _item.TenKq = "";
                        _item.LoaiKq = 0;
                        _item.ChophepHienthi = 1;
                        _item.ChophepIn = 1;
                        _item.MotaThem = objcls.MotaThem;
                        
                    }
                    lstResult.Add(_item);
                    lstDetails.Add(_itemchitiet);
                }
                catch (Exception)
                {


                }
            }
            if (clsXN.UpdateResult(lstResult, lstDetails) == ActionResult.Success)
                Utility.ShowMsg("Đã hủy kết quả các xét nghiệm đang chọn thành công");
            else
                Utility.ShowMsg("Lỗi khi thực hiện hủy kết quả xét nghiệm");
        }
        
        //KcbChidinhclsChitiet.Trang_thai:0=Mới chỉ định;1=Đã chuyển CLS;2=Đang thực hiện;3= Đã có kết quả CLS;4=Đã xác nhận kết quả
        void grdKetqua_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                List<KcbKetquaCl> lstResult = new List<KcbKetquaCl>();
                List<KcbChidinhclsChitiet> lstDetails = new List<KcbChidinhclsChitiet>();
                int id_kq = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdKetqua,KcbKetquaCl.Columns.IdKq) ,-1);
                int IdChitietchidinh = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdKetqua, KcbChidinhclsChitiet.Columns.IdChitietchidinh), -1);
                int IdChitietchidinhcha = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdChidinh, KcbChidinhclsChitiet.Columns.IdChitietchidinh), -1);
                int CoChitiet = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdChidinh, DmucDichvuclsChitiet.Columns.CoChitiet), -1);

                int IdChitietdichvu = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdKetqua, DmucDichvuclsChitiet.Columns.IdChitietdichvu), -1);
                KcbKetquaCl _item = null;
                KcbChidinhclsChitiet _itemchitiet = KcbChidinhclsChitiet.FetchByID(IdChitietchidinh);
                KcbChidinhclsChitiet _itemchitietcha = null;
                if (CoChitiet==1)
                {
                    _itemchitietcha = KcbChidinhclsChitiet.FetchByID(IdChitietchidinhcha);
                    if (_itemchitietcha != null)
                    {
                        _itemchitietcha.IsNew = false;
                        _itemchitietcha.MarkOld();
                    }
                }
                _itemchitiet.IsNew = false;
                _itemchitiet.MarkOld();
                if (id_kq >0)
                {
                    _item = KcbKetquaCl.FetchByID(id_kq);
                    _item.IsNew = false;
                    _item.NguoiSua = globalVariables.UserName;
                    _item.NgaySua = globalVariables.SysDate;
                    _item.IpMaysua = globalVariables.gv_strIPAddress;
                    _item.TenMaysua = globalVariables.gv_strComputerName;
                    _item.MarkOld();
                }
                else
                {
                    _item = new KcbKetquaCl();
                    _item.IsNew = true;
                    _item.NguoiTao = globalVariables.UserName;
                    _item.NgayTao = globalVariables.SysDate;
                    _item.IpMaytao = globalVariables.gv_strIPAddress;
                    _item.TenMaytao = globalVariables.gv_strComputerName;
                }
                DmucDichvuclsChitiet objcls = DmucDichvuclsChitiet.FetchByID(IdChitietdichvu);
                if (objcls != null)
                {
                    _item.MaChidinh = Utility.GetValueFromGridColumn(grdChidinh, KcbChidinhcl.Columns.MaChidinh);
                    _item.MaBenhpham = Utility.GetValueFromGridColumn(grdChidinh, KcbChidinhcl.Columns.MaBenhpham);
                    _item.Barcode = Utility.GetValueFromGridColumn(grdChidinh, KcbChidinhcl.Columns.Barcode);
                    _item.IdBenhnhan = id_benhnhan;
                    _item.MaLuotkham = ma_luotkham;
                    _item.IdChidinh = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdKetqua, KcbChidinhclsChitiet.Columns.IdChidinh), -1);
                    _item.IdChitietchidinh = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdKetqua, KcbChidinhclsChitiet.Columns.IdChitietchidinh), -1);
                    _item.IdDichvu = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdKetqua, KcbChidinhclsChitiet.Columns.IdDichvu), -1);
                    _item.IdDichvuchitiet = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdKetqua, KcbChidinhclsChitiet.Columns.IdChitietdichvu), -1);
                    _item.Barcode = Utility.GetValueFromGridColumn(grdChidinh, KcbChidinhcl.Columns.Barcode);
                    _item.SttIn = objcls.SttHthi;
                    _item.BtNam = objcls.BinhthuongNam;
                    _item.BtNu = objcls.BinhthuongNu;
                    _item.KetQua = Utility.sDbnull(e.Value, "");
                    if (_item.TrangThai < 3)
                        _item.TrangThai = 3;
                    if (chkSaveAndConfirm.Checked)
                        _item.TrangThai = 4;//Duyệt luôn để hiển thị trên form thăm khám của bác sĩ

                    if (Utility.DoTrim(_item.KetQua) == "")
                        _item.TrangThai = 2;//Quay ve trang thai chuyen đang thực hiện
                    //_item.TenDonvitinh = objcls.TenDonvitinh;
                    _itemchitiet.KetQua = Utility.sDbnull(e.Value, "");
                    if (_itemchitiet.TrangThai < 3)
                        _itemchitiet.TrangThai = 3;
                    if (chkSaveAndConfirm.Checked)
                        _itemchitiet.TrangThai = 4;//Duyệt luôn để hiển thị trên form thăm khám của bác sĩ
                    if (Utility.DoTrim(_itemchitiet.KetQua) == "")
                        _itemchitiet.TrangThai = 1;//Quay ve trang thai chuyen can

                    if (_itemchitietcha!=null && _itemchitietcha.TrangThai < 3)
                        _itemchitietcha.TrangThai = 3;
                    if (_itemchitietcha != null && chkSaveAndConfirm.Checked)
                        _itemchitietcha.TrangThai = 4;//Duyệt luôn để hiển thị trên form thăm khám của bác sĩ
                    if (_itemchitietcha != null && Utility.DoTrim(Utility.sDbnull(e.Value, "")) == "")
                        _itemchitietcha.TrangThai = 1;//Quay ve trang thai chuyen can

                    _item.TenThongso = "";
                    _item.TenKq = "";
                    _item.LoaiKq = 0;
                    _item.ChophepHienthi = 1;
                    _item.ChophepIn = 1;
                    _item.MotaThem = objcls.MotaThem;
                    lstResult.Add(_item);
                    lstDetails.Add(_itemchitiet);
                    if(_itemchitietcha!=null)
                        lstDetails.Add(_itemchitietcha);
                    if (clsXN.UpdateResult(lstResult, lstDetails) != ActionResult.Success)
                        e.Cancel = true;
                }
            }
            catch (Exception)
            {
                
                
            }
        }

        void grdChidinh_CurrentCellChanged(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdChidinh)) return;
            int tempRowIdx=grdChidinh.CurrentRow.RowIndex;
            if (currRowIdx == -1 || currRowIdx != tempRowIdx)
            {
                currRowIdx = tempRowIdx;
                id_dichvu = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdChidinh, VKcbChidinhcl.Columns.IdDichvu), 0);
                IdChitietdichvu = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdChidinh, VKcbChidinhcl.Columns.IdChitietdichvu), 0);
                co_chitiet = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdChidinh, VKcbChidinhcl.Columns.CoChitiet), 0);
                id_chidinh = Utility.Int32Dbnull(Utility.GetValueFromGridColumn(grdChidinh, VKcbChidinhcl.Columns.IdChidinh), 0);
                ma_luotkham = Utility.GetValueFromGridColumn(grdChidinh, VKcbChidinhcl.Columns.MaLuotkham);
                MaChidinh = Utility.GetValueFromGridColumn(grdChidinh, VKcbChidinhcl.Columns.MaChidinh);
                MaBenhpham = Utility.GetValueFromGridColumn(grdChidinh, VKcbChidinhcl.Columns.MaBenhpham);
                HienthiNhapketqua(id_dichvu, co_chitiet);
            }
        }
        void HienthiNhapketqua(int id_dichvu,int co_chitiet)
        {
            try
            {
                DataTable dt = SPs.ClsTimkiemthongsoXNNhapketqua(ma_luotkham, MaChidinh, MaBenhpham, id_chidinh, co_chitiet, id_dichvu, IdChitietdichvu).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx_Basic(grdKetqua, dt, true, true, "1=1", DmucDichvuclsChitiet.Columns.SttHthi );

                Utility.focusCell(grdKetqua, KcbKetquaCl.Columns.KetQua);
            }
            catch (Exception ex)
            {
                
                
            }
        }
        void txtMabenhpham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SearchData();
        }

        void txtMaluotkham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SearchData();
        }

        

        void frm_nhapketquaXN_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.F2 && grdKetqua.GetDataRows().Length>0)
                Utility.focusCell(grdKetqua, KcbKetquaCl.Columns.KetQua);
        }

        void frm_nhapketquaXN_Load(object sender, EventArgs e)
        {
            
        }

        void cmdSearch_Click(object sender, EventArgs e)
        {
            SearchData();
        }
        void FillPatientData(DataRow dr)
        {
            if (dr == null)
            {
                 txtKhoaDieuTri.Clear();
                txtBuong.Clear();
                txtGiuong.Clear();

                txtTrangthaiNgoaitru.Clear();
                txtTrangthaiNoitru.Clear();

                txtGioitinh.Clear();
                txtPatient_Name.Clear();
                txtDiaChi.Clear();

                txtSoBHYT.Clear();
                txtTuoi.Clear();
            }
            else
            {
                txtKhoaDieuTri.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.TenKhoanoitru]);
                txtBuong.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.TenBuong]);
                txtGiuong.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.TenGiuong]);

                txtTrangthaiNgoaitru.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.TrangthaiNgoaitru]) == "0" ? "Đang khám" : "Đã khám xong";
                txtTrangthaiNoitru.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.TenTrangthaiNoitru]);

                txtGioitinh.Text =
                    Utility.sDbnull(dr[VKcbLuotkham.Columns.GioiTinh], "");
                txtPatient_Name.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.TenBenhnhan], "");
                txtDiaChi.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.DiaChi], "");
                txtNamSinh.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.NamSinh], "");
                txtSoBHYT.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.MatheBhyt], "");
                txtTuoi.Text = Utility.sDbnull(dr[VKcbLuotkham.Columns.Tuoi]);
            }
        }
       public void SearchData()
        {
            try
            {

                ma_luotkham = "ALL";
                if (Utility.DoTrim(txtMaluotkham.Text) != "")
                    ma_luotkham = Utility.AutoFullPatientCode(txtMaluotkham.Text);

                string ma_benhpham = "ALL";
                if (Utility.DoTrim(txtMabenhpham.Text) != "")
                    ma_benhpham = Utility.DoTrim(txtMabenhpham.Text);
                if (ma_luotkham == "ALL" && ma_benhpham == "ALL")
                {
                    Utility.ShowMsg("Bạn cần nhập ít nhất 1 trong 2 thông tin: Mã lượt khám hoặc mã bệnh phẩm");
                    return;
                }
                if (ma_luotkham != "ALL")
                    txtMaluotkham.Text = ma_luotkham;
                if (ma_benhpham != "ALL")
                    txtMabenhpham.Text = ma_benhpham;
                DataTable dt = SPs.ClsTimkiemClsthuocXetnghiem(ma_luotkham, ma_benhpham, id_chidinhchitiet).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx_Basic(grdChidinh, dt, true, true,"1=1", VKcbChidinhcl.Columns.SttHthiDichvu + " desc");
                grdChidinh.MoveFirst();
                if (dt.Rows.Count > 0)
                {
                    id_benhnhan = Utility.Int64Dbnull(dt.Rows[0][KcbLuotkham.Columns.IdBenhnhan], -1);
                    ma_luotkham = Utility.sDbnull(dt.Rows[0][KcbLuotkham.Columns.MaLuotkham], "");
                    DataTable dtPatientInfor = SPs.CommonLaythongtinbenhnhantheoIdMaluotkham(id_benhnhan, ma_luotkham).GetDataSet().Tables[0];
                    if (dtPatientInfor.Rows.Count > 0)
                        FillPatientData(dtPatientInfor.Rows[0]);
                    else
                        FillPatientData(null);
                }
            }
            catch (Exception)
            {
                ma_luotkham = "";

            }
        }
    }
}
