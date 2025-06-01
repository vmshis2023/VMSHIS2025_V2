using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;
using VNS.HIS.BusRule.Goikham;
using VMS.HIS.DAL;
using SubSonic;
using VNS.HIS.UI.DANHMUC;
using Janus.Windows.GridEX;
using VNS.Properties;

namespace VNS.HIS.UI.GOIKHAM
{
    public partial class frm_DanhSachGoiKham : Form
    {
        private DataTable _dtGoiKham;
        private DataTable m_dtChitietgoi;
        private clsGoikham _goiKhamService;
        bool AllowSelectionChanged = false;
        byte loai_goi = 0;
        public frm_DanhSachGoiKham(string args)
        {
            InitializeComponent();
            loai_goi = Utility.ByteDbnull(args, 0);
            Utility.SetVisualStyle(this);
            _goiKhamService = new clsGoikham();
        }

        private void frm_DanhSachGoiKham_Load(object sender, EventArgs e)
        {
            lblLoaigoi.Visible = cboLoai.Visible = loai_goi > 0;
            cboTrangThai.SelectedIndex = 0;
            cboLoai.SelectedIndex = 0;
            TimkiemGoi();
        }

        void TimkiemGoi()
        {
            try
            {
                int id = Utility.Int32Dbnull(txtId.Text, -1);
                string ma = Utility.sDbnull(txtMa.Text, "");
                string ten = Utility.sDbnull(txtTen.Text, "");
                byte bytloaigoi = 0;
                if (ma == "") ma = "-1";
                if (lblLoaigoi.Visible) bytloaigoi = Utility.ByteDbnull(cboLoai.SelectedValue);
                _dtGoiKham = _goiKhamService.LayDanhSachGoiKham(id, ma, ten, Utility.ByteDbnull(cboTrangThai.SelectedValue), bytloaigoi, dtmFrom.Value, dtmTo.Value, Utility.ByteDbnull(cboKieugoi.SelectedValue));
                grdGoiKham.DataSource = _dtGoiKham;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
            finally
            {
                AllowSelectionChanged = true;
                grdGoiKham_SelectionChanged(grdGoiKham, new EventArgs());
            }
        }
        private void grdGoiKham_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (!AllowSelectionChanged) return;
                var idGoiDvu = Utility.Int32Dbnull(grdGoiKham.GetValue("id_goi"));
                m_dtChitietgoi = _goiKhamService.LayChiTietGoiKhamTheoIdGoi(idGoiDvu);
                grdChiTietGoiKham.DataSource = m_dtChitietgoi;
                LoadGiaQuanheDoituong();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        DataTable m_dtGiaDoituongGoi = new DataTable();
        void LoadGiaQuanheDoituong()
        {
            try
            {
                if (Utility.isValidGrid(grdGoiKham))
                {
                    int id_goi = Utility.Int32Dbnull(grdGoiKham.CurrentRow.Cells[GoiDanhsach.Columns.IdGoi].Value, -1);

                    m_dtGiaDoituongGoi = SPs.QheGiadoituongGoi(id_goi).GetDataSet().Tables[0];
                    if (!m_dtGiaDoituongGoi.Columns.Contains("IsNew"))
                        m_dtGiaDoituongGoi.Columns.Add("IsNew", typeof(int));
                    if (!m_dtGiaDoituongGoi.Columns.Contains("CHON"))
                        m_dtGiaDoituongGoi.Columns.Add("CHON", typeof(int));

                    Utility.SetDataSourceForDataGridEx(grdQhe, m_dtGiaDoituongGoi, true, true, "1=1", "");
                }
                ModifyCommand1();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }
        void ModifyCommand1()
        {
            cmdDelete.Enabled = cmdSave.Enabled = Utility.isValidGrid(grdQhe);
        }
        private void cmdThemMoi_Click(object sender, EventArgs e)
        {
            try
            {
                if (loai_goi == 0)
                {
                    frm_TaoGoiKham frm = new frm_TaoGoiKham();
                    frm.DtGoiKham = _dtGoiKham;
                    frm.CS_Action = action.Insert;
                    frm.ShowDialog();
                    if (!frm.BCancel)
                    {
                        grdGoiKham.MoveLast();
                    }
                }
                else
                {
                    frm_TaoChuongtrinhKhuyenmai_Voucher frm = new frm_TaoChuongtrinhKhuyenmai_Voucher();
                    frm.DtGoiKham = _dtGoiKham;
                    frm.CS_Action = action.Insert;
                    frm.ShowDialog();
                    if (!frm.BCancel)
                    {
                        grdGoiKham.MoveLast();
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void cmdSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (loai_goi == 0)
                {
                    var frm = new frm_TaoGoiKham();
                    frm.DtGoiKham = _dtGoiKham;
                    frm.CS_Action = action.Update;
                    frm.m_dtChitietgoi = m_dtChitietgoi.Copy();
                    frm.IdGoiDVu = Utility.Int32Dbnull(grdGoiKham.GetValue("id_goi"));
                    frm.ShowDialog();
                    if (frm.BCancel)
                    {
                        frm.m_dtChitietgoi.RejectChanges();
                    }
                    else
                        grdGoiKham_SelectionChanged(grdGoiKham, e);
                }
                else
                {
                    frm_TaoChuongtrinhKhuyenmai_Voucher frm = new frm_TaoChuongtrinhKhuyenmai_Voucher();
                    frm.DtGoiKham = _dtGoiKham;
                    frm.CS_Action = action.Update;
                    frm.m_dtChitietgoi = m_dtChitietgoi.Copy();
                    frm.IdGoiDVu = Utility.Int32Dbnull(grdGoiKham.GetValue("id_goi"));
                    frm.ShowDialog();
                    if (frm.BCancel)
                    {
                        frm.m_dtChitietgoi.RejectChanges();
                    }
                    else
                        grdGoiKham_SelectionChanged(grdGoiKham, e);
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void cmdXoa_Click(object sender, EventArgs e)
        {
            try
            {
                int idGoiKham = Utility.Int32Dbnull(grdGoiKham.CurrentRow.Cells[GoiDanhsach.Columns.IdGoi].Value, -1);
                if (idGoiKham > 0)
                {
                    if (!_goiKhamService.kiemtra_sudung_goikham(idGoiKham))
                    {
                        if (
                            Utility.AcceptQuestion(string.Format("Bạn có đồng ý xóa danh mục gói khám: {0} không? ",
                                Utility.sDbnull(grdGoiKham.CurrentRow.Cells[GoiDanhsach.Columns.TenGoi].Value, "")),"Thông báo", true))
                        {
                            _goiKhamService.XoaGoiKham(idGoiKham);
                            grdGoiKham.CurrentRow.Delete();
                        }
                       
                    }
                    else
                    {
                        Utility.ShowMsg("Gói đã được sử dụng nên không thể xóa", "Cảnh báo", MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void cmdThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_DanhSachGoiKham_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Control && ( e.KeyCode==Keys.T ||  e.KeyCode==Keys.N)) cmdThemMoi.PerformClick();
            else if(e.Control && (e.KeyCode==Keys.U || e.KeyCode==Keys.E )) cmdSua.PerformClick();
            else if(e.Control && e.KeyCode==Keys.D) cmdXoa.PerformClick();
            else if(e.KeyCode==Keys.Escape) cmdThoat.PerformClick();
                   
            
        }

        private void grdGoiKham_RowDoubleClick(object sender, Janus.Windows.GridEX.RowActionEventArgs e)
        {
            cmdSua.PerformClick();
        }

        private void cmdHieuluc_Click(object sender, EventArgs e)
        {
            int idGoiKham = Utility.Int32Dbnull(grdGoiKham.CurrentRow.Cells[GoiDanhsach.Columns.IdGoi].Value, -1);
            if (idGoiKham > 0)
            {
              int num=  new Update(GoiDanhsach.Schema).Set(GoiDanhsach.Columns.TrangThai).EqualTo(1).Where(GoiDanhsach.Columns.IdGoi).IsEqualTo(idGoiKham).Execute();
                if(num>0)
                {
                    grdGoiKham.CurrentRow.BeginEdit();
                    grdGoiKham.CurrentRow.Cells["trang_thai"].Value = 1;
                    grdGoiKham.CurrentRow.EndEdit();
                    grdGoiKham.Refetch();
                }    
            }
        }

        private void cmdHide_Click(object sender, EventArgs e)
        {
            int idGoiKham = Utility.Int32Dbnull(grdGoiKham.CurrentRow.Cells[GoiDanhsach.Columns.IdGoi].Value, -1);
            if (idGoiKham > 0)
            {
                int num = new Update(GoiDanhsach.Schema).Set(GoiDanhsach.Columns.TrangThai).EqualTo(0).Where(GoiDanhsach.Columns.IdGoi).IsEqualTo(idGoiKham).Execute();
                if (num > 0)
                {
                    grdGoiKham.CurrentRow.BeginEdit();
                    grdGoiKham.CurrentRow.Cells["trang_thai"].Value = 0;
                    grdGoiKham.CurrentRow.EndEdit();
                    grdGoiKham.Refetch();
                }
            }
        }

        private void cmdGenerateVoucher_Click(object sender, EventArgs e)
        {
            frm_TaoMaVoucher _TaoMaVoucher = new frm_TaoMaVoucher();
            _TaoMaVoucher.ShowDialog();
        }

        private void cmdTimKiem_Click(object sender, EventArgs e)
        {
            TimkiemGoi();
        }

        private void cmdAdd_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdQhe)) return;
            var frm = new frm_ChonDoituongKCB();
            frm._enObjectType = enObjectType.GoiDichVu;
            frm.m_dtObjectDataSource = m_dtGiaDoituongGoi;
            frm.Original_Price = Utility.DecimaltoDbnull(grdGoiKham.CurrentRow.Cells[GoiDanhsach.Columns.SoTien].Value, 0);
            frm.MaKhoaTHIEN = globalVariables.MA_KHOA_THIEN;
            frm.DetailService = Utility.Int32Dbnull(grdGoiKham.CurrentRow.Cells[GoiDanhsach.Columns.IdGoi].Value, 0);
            frm.ShowDialog();
            if (!frm.m_blnCancel)
                cmdSave_Click(cmdSave, e);
            ModifyCommand1();
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {

        }

        private void cmdSave_Click(object sender, EventArgs e)
        {

        }
        private decimal LayGiaDV()
        {
            foreach (GridEXRow gridExRow in grdQhe.GetRows())
            {
                if (gridExRow.Cells[DmucDoituongkcb.Columns.MaDoituongKcb].Value.ToString() == "DV")
                    return Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongGoi.Columns.DonGia].Value, 0);
            }
            return -1;
        }
        private decimal LayGiaBHYT()
        {
            foreach (GridEXRow gridExRow in grdQhe.GetRows())
            {
                if (gridExRow.Cells[DmucDoituongkcb.Columns.MaDoituongKcb].Value.ToString() == "BHYT")
                    return Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongGoi.Columns.DonGia].Value, 0);
            }
            return -1;
        }
        private void SaveAll()
        {
            try
            {
                Utility.SetMsg(lblMsg, "", false);
                decimal GiaDV = LayGiaDV();
                int id_goi = -1;
                decimal GiaPhuThu = 0;
                decimal GiaBHYT = LayGiaBHYT();
                string KTH = "ALL";
                decimal tyle_tt = 100;
                id_goi = Utility.Int32Dbnull(grdGoiKham.GetValue( QheDoituongGoi.Columns.IdGoi), -1);
                foreach (GridEXRow gridExRow in grdQhe.GetRows())
                {
                    if (gridExRow.Cells[QheDoituongDichvucl.Columns.MaDoituongKcb].Value.ToString() == "BHYT" && GiaDV > 0)
                    {
                        GiaBHYT = Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongDichvucl.Columns.DonGia].Value, 0);
                        if (PropertyLib._QheGiaCLSProperties.TudongDieuChinhGiaPTTT)
                            GiaPhuThu = GiaDV - GiaBHYT > 0 ? GiaDV - GiaBHYT : 0;
                    }
                    long id_quanhe= Utility.Int32Dbnull(gridExRow.Cells[QheDoituongGoi.Columns.IdQuanhe].Value, -1);
                    if (id_quanhe <= 0)
                    {
                        QheDoituongGoi _newItems = new QheDoituongGoi();
                        _newItems.IdDoituongKcb = Utility.Int16Dbnull(gridExRow.Cells[QheDoituongGoi.Columns.IdDoituongKcb].Value, -1);
                        _newItems.IdGoi = id_goi;
                        _newItems.TyleGiam = 0;
                        _newItems.KieuGiamgia = 0;
                        _newItems.DonGia = Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongGoi.Columns.DonGia].Value, 0);
                        _newItems.PhuthuDungtuyen = Utility.DecimaltoDbnull(gridExRow.Cells[QheDoituongGoi.Columns.PhuthuDungtuyen].Value, 0);
                        _newItems.PhuthuTraituyen = GiaPhuThu;
                        _newItems.IdLoaidoituongKcb = Utility.Int32Dbnull(gridExRow.Cells[QheDoituongGoi.Columns.IdLoaidoituongKcb].Value, -1);
                        _newItems.MaDoituongKcb = Utility.sDbnull(gridExRow.Cells[QheDoituongGoi.Columns.MaDoituongKcb].Value, "DV");
                        _newItems.NguoiTao = globalVariables.UserName;
                        _newItems.NgayTao = globalVariables.SysDate;
                        _newItems.MaKhoaThuchien = KTH;
                        _newItems.IsNew = true;
                        _newItems.Save();
                        gridExRow.BeginEdit();
                        gridExRow.Cells[QheDoituongGoi.Columns.IdQuanhe].Value = _newItems.IdQuanhe;
                        gridExRow.EndEdit();
                    } 
                   
                }
                Utility.SetMsg(lblMsg, "Bạn thực hiện cập nhập giá thành công", false);
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
    }
}
