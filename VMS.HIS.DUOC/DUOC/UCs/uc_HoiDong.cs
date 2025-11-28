using SubSonic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Windows.Forms;
using VMS.HIS.DAL;
using VNS.HIS.UI.DANHMUC;
using VNS.Libs;

namespace VMS.HIS.Duoc.DUOC.UCs
{
    public partial class uc_HoiDong : UserControl
    {
        public uc_HoiDong()
        {
            InitializeComponent();
        }
      
            long id_phieu = -1;
        byte loai_phieu = 1;
        TBienbanKiemnhap objBienban = null;
        DataTable dtChitiet = new DataTable();
        public void Init(long id_phieu, byte loai_phieu)
        {
            InitializeComponent();
            this.id_phieu = id_phieu;
            this.loai_phieu = loai_phieu;
            grd_thanhvien.ColumnButtonClick += Grd_thanhvien_ColumnButtonClick;
            grd_thanhvien.MouseDoubleClick += Grd_hoidong_MouseDoubleClick;
            InitData();

        }
        private void Grd_thanhvien_ColumnButtonClick(object sender, Janus.Windows.GridEX.ColumnActionEventArgs e)
        {
            if (e.Column.Key == "XOA")
            {
                if (Utility.AcceptQuestion(string.Format("Bạn có muốn xóa thành viên {0} khỏi hội đồng?", grd_thanhvien.GetValue("ten_nhanvien")), "Xác nhận xóa", true))
                {
                    grd_thanhvien.CurrentRow.Delete();
                    dtChitiet.AcceptChanges();
                }
            }
        }
        void InitData()
        {
            try
            {
                DataBinding.BindDataCombobox(cbo_thanhvien, globalVariables.gv_dtDmucNhanvien, DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.TenNhanvien, "", true);
                DataBinding.BindDataCombobox(cbo_chucvu, THU_VIEN_CHUNG.LayDulieuDanhmucChung("NVIEN_CHUCVU", true), DmucChung.Columns.Ma, DmucChung.Columns.Ten, "", true);
                dtChitiet = new Select().From(TDsachHoidong.Schema)
                    .Where(TDsachHoidong.Columns.IdPhieu).IsEqualTo(id_phieu)
                    .And(TDsachHoidong.Columns.LoaiPhieu).IsEqualTo(loai_phieu)
                    .ExecuteDataSet().Tables[0];
                dtChitiet.Columns.Add(new DataColumn("uuid", typeof(string)));
                Utility.SetDataSourceForDataGridEx_Basic(grd_thanhvien, dtChitiet, true, true, "1=1", "stt,ten_nhanvien");

            }
            catch (Exception ex)
            {


            }
        }
        private void Grd_hoidong_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Capnhat();
        }
        void Capnhat()
        {
            try
            {
                if (!Utility.isValidGrid(grd_thanhvien)) return;
                Int16 id_nhanvien = Utility.Int16Dbnull(grd_thanhvien.GetValue("id_nhanvien"));
                txt_Id.Text = Utility.sDbnull(grd_thanhvien.GetValue("id_hoidong"));
                cbo_thanhvien.SelectedValue = id_nhanvien;
                cbo_chucvu.Text = Utility.sDbnull(grd_thanhvien.GetValue("chuc_vu"));
                nmr_stt.Value = Utility.DecimaltoDbnull(grd_thanhvien.GetValue("stt"));
            }
            catch (Exception)
            {


            }
        }
        private void cmd_quanly_chucvu_Click(object sender, EventArgs e)
        {
            ThemDanhMuc("NVIEN_CHUCVU");
        }
        private void ThemDanhMuc(string LOAI_DANHMUC)
        {
            DMUC_DCHUNG dmucDchung = new DMUC_DCHUNG(LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                DataBinding.BindDataCombobox(cbo_chucvu, THU_VIEN_CHUNG.LayDulieuDanhmucChung("NVIEN_CHUCVU", true), DmucChung.Columns.Ma, DmucChung.Columns.Ten, "", true);
            }
        }
        private void ModifyCommand()
        {
            cmdSave.Enabled = dtChitiet.Rows.Count > 0;
        }
        bool isValidData()
        {
            errorProvider1.Clear();

            if (Utility.Int32Dbnull(cbo_thanhvien.SelectedValue) <= 0)
            {
                errorProvider1.SetError(cbo_thanhvien, "Bạn cần chọn thông tin thành viên hội đồng từ danh mục nhân viên");
                cbo_thanhvien.Focus();
                cbo_thanhvien.SelectAll();
                return false;
            }

            if (Utility.DoTrim(cbo_chucvu.Text) == "")
            {
                errorProvider1.SetError(cbo_chucvu, "Bạn cần chọn chức vụ");
                cbo_chucvu.Focus();
                cbo_chucvu.SelectAll();
                return false;
            }
            return true;
        }
        private void cmd_them_Click(object sender, EventArgs e)
        {
            try
            {
                if (!isValidData()) return;
                if (Utility.Int64Dbnull(txt_Id.Text) <= 0)
                {
                    DataRow newItem = dtChitiet.NewRow();
                    newItem["id_hoidong"] = -1;
                    newItem["id_nhanvien"] = Utility.Int16Dbnull(cbo_thanhvien.SelectedValue);
                    newItem["ten_nhanvien"] = Utility.sDbnull(cbo_thanhvien.Text);
                    newItem["chuc_vu"] = cbo_chucvu.Text;
                    newItem["stt"] = Utility.ByteDbnull(nmr_stt.Value);
                    newItem["uuid"] = Guid.NewGuid().ToString();
                    dtChitiet.Rows.Add(newItem);
                }
                else
                {
                    DataRow item = dtChitiet.Select(string.Format("id_hoidong={0} or uuid='{1}'", txt_Id.Text, txt_uuid.Text)).FirstOrDefault();
                    if (item != null)
                    {
                        item["id_nhanvien"] = Utility.Int16Dbnull(cbo_thanhvien.SelectedValue);
                        item["ten_nhanvien"] = Utility.sDbnull(cbo_thanhvien.Text);
                        item["chuc_vu"] = cbo_chucvu.Text;
                        item["stt"] = Utility.ByteDbnull(nmr_stt.Value);
                    }
                }
                Reset();
            }
            catch (Exception ex)
            {

            }
        }
        void Reset()
        {
            cbo_thanhvien.SelectedIndex = 0;
            cbo_chucvu.SelectedIndex = 0;
            cbo_thanhvien.Focus();
        }
        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {

                        new Delete().From(TDsachHoidong.Schema)
                            .Where(TDsachHoidong.Columns.IdPhieu).IsEqualTo(id_phieu)
                            .And(TDsachHoidong.Columns.LoaiPhieu).IsEqualTo(loai_phieu)
                            .Execute();
                        foreach (DataRow dr in dtChitiet.Rows)//Insert lại chi tiết thay vì kiểm tra thêm mới or cập nhật
                        {
                            TDsachHoidong newItem = new TDsachHoidong();
                            newItem.IdPhieu = id_phieu;
                            newItem.LoaiPhieu = loai_phieu;
                            newItem.IdNhanvien = Utility.Int16Dbnull(dr["id_nhanvien"]);
                            newItem.TenNhanvien = Utility.sDbnull(dr["ten_nhanvien"]);
                            newItem.ChucVu = Utility.sDbnull(dr["chuc_vu"]);
                            newItem.Stt = Utility.ByteDbnull(dr["stt"]);
                            newItem.NguoiTao = globalVariables.UserName;
                            newItem.NgayTao = globalVariables.SysDate;
                            newItem.Save();

                        }
                    }
                    scope.Complete();
                }
                Utility.ShowMsg("Lưu thông tin hội đồng thành công");
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
    }
}
