using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VMS.HIS.DAL;
using VNS.Libs;
using SubSonic;

namespace VNS.HIS.UI.Forms.Dungchung
{
    public partial class frm_CapnhatNguon_Doitac : Form
    {
        bool AllowNguonGTChanged = false;
        public KcbLuotkham _objLuotkham;
        public KcbDanhsachBenhnhan objBenhnhan;
        public DataRow drData = null;
        public frm_CapnhatNguon_Doitac()
        {
            InitializeComponent();
            cboNguongioithieu.SelectedIndexChanged += CboNguongioithieu_SelectedIndexChanged;
            this.KeyDown += Frm_CapnhatNguon_Doitac_KeyDown;
        }

        private void Frm_CapnhatNguon_Doitac_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdThoat.PerformClick();
            if (e.Control && e.KeyCode == Keys.S) cmdUpdate.PerformClick();
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                _objLuotkham.MaDoitac = Utility.sDbnull(cboDoitac.SelectedValue);
                _objLuotkham.NoiGioithieu = Utility.sDbnull(cboNguongioithieu.SelectedValue);

                string sql = string.Format("update kcb_luotkham set noi_gioithieu='{0}', ma_doitac='{1}' where id_benhnhan={2} and ma_luotkham='{3}'", _objLuotkham.NoiGioithieu, _objLuotkham.MaDoitac, _objLuotkham.IdBenhnhan, _objLuotkham.MaLuotkham);
                Utility.ExecuteNonQuery(sql, CommandType.Text);
                string newInfor = string.Format("Người bệnh với id={0}, mã lượt khám={1}, nguồn từ {2} thành {3}, đối tác từ {4} thành {5}",_objLuotkham.IdBenhnhan,_objLuotkham.MaLuotkham, txtNguonGTCu.Text,cboNguongioithieu.Text,txtDoitaccu.Text,cboDoitac.Text);
                Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật thông tin nguồn, đối tác với chi tiết như sau: {0} ", newInfor), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                drData[KcbLuotkham.Columns.MaDoitac] = Utility.sDbnull(cboDoitac.SelectedValue);
                drData["ten_doitac"] = Utility.sDbnull(cboDoitac.Text);
                drData[KcbLuotkham.Columns.NoiGioithieu] = Utility.sDbnull(cboNguongioithieu.SelectedValue);
                drData["ten_nguongioithieu"] = Utility.sDbnull(cboNguongioithieu.Text);
                Utility.ShowMsg("Cập nhật nguồn giới thiệu và đối tác thành công. Nhấn OK để kết thúc!");
                this.Close();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
           
        }


        private void cmdThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_CapnhatNguon_Doitac_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dtNguonGT = THU_VIEN_CHUNG.LayDulieuDanhmucChung("NGUONGTHIEU", true);
                DataBinding.BindDataCombobox(cboNguongioithieu, dtNguonGT, DmucChung.Columns.Ma, DmucChung.Columns.Ten);
                var q = from p in dtNguonGT.AsEnumerable() where Utility.Int32Dbnull(p[DmucChung.Columns.TrangthaiMacdinh]) == 1 select p;
                if (q.Any())
                {
                    cboNguongioithieu.SelectedValue = q.FirstOrDefault()["MA"];
                    CboNguongioithieu_SelectedIndexChanged(cboNguongioithieu, new EventArgs());
                }
                else
                    cboNguongioithieu.SelectedValue = "";

                AllowNguonGTChanged = true;

                if (objBenhnhan == null) objBenhnhan =
                       new Select().From(KcbDanhsachBenhnhan.Schema)
                           .Where(KcbDanhsachBenhnhan.Columns.IdBenhnhan)
                           .IsEqualTo(Utility.Int64Dbnull(txtidbenhnhancu.Text))
                           .ExecuteSingle<KcbDanhsachBenhnhan>();
                if (objBenhnhan != null && _objLuotkham != null)
                {
                    txtmalankham.Text = _objLuotkham.MaLuotkham;
                    txttenbenhnhancu.Text = Utility.sDbnull(objBenhnhan.TenBenhnhan);
                    txtnamsinhcu.Text = Utility.sDbnull(objBenhnhan.NamSinh);
                    if (Utility.DoTrim(_objLuotkham.NoiGioithieu).Length > 0)
                        cboNguongioithieu.SelectedValue = _objLuotkham.NoiGioithieu;
                    CboNguongioithieu_SelectedIndexChanged(cboNguongioithieu, new EventArgs());
                    cboDoitac.SelectedValue = _objLuotkham.MaDoitac;
                    txtNguonGTCu.Text = cboNguongioithieu.Text;
                    txtDoitaccu.Text = cboDoitac.Text;
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
           

        }
        private void CboNguongioithieu_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!AllowNguonGTChanged) return;
                DataTable dtDoitac = SPs.TiepdonDmucdoitacLaytheonguongioithieu(Utility.sDbnull(cboNguongioithieu.SelectedValue)).GetDataSet().Tables[0];
                DataBinding.BindDataCombobox(cboDoitac, dtDoitac, DmucDoitac.Columns.MaDoitac, DmucDoitac.Columns.TenDoitac);
                var q = from p in dtDoitac.AsEnumerable() where Utility.Int32Dbnull(p[DmucDoitac.Columns.TrangthaiMacdinh]) == 1 select p;
                if (q.Any())
                {
                    cboDoitac.SelectedValue = q.FirstOrDefault()[DmucDoitac.Columns.MaDoitac];
                }
                else
                    cboDoitac.SelectedValue = "";
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
    }
}
