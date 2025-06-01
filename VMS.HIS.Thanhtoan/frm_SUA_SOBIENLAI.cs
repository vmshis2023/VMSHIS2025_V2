using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;
using VNS.HIS.DAL;
using SubSonic;
using VNS.HIS.NGHIEPVU;
using VNS.HIS.BusRule.Classes;

namespace  VNS.HIS.UI.NGOAITRU
{
    public partial class frm_SUA_SOBIENLAI : Form
    {
        public HoadonLog _HoadonLog;
        public bool m_blnCancel = true;
        public frm_SUA_SOBIENLAI()
        {
            InitializeComponent();

        }
        private void frm_SUA_SOBIENLAI_Load(object sender, EventArgs e)
        {
            try
            {
                txtmaquyencu.Text = Utility.sDbnull(_HoadonLog.MaQuyen, "");
                txtMauSoCu.Text = Utility.sDbnull(_HoadonLog.MauHoadon, "");
                txtKyHieuCu.Text = Utility.sDbnull(_HoadonLog.KiHieu, "");
                txtSoBienLaiCu.Text = Utility.sDbnull(_HoadonLog.Serie, "");
                txtmaquyenmoi.Text = Utility.sDbnull(_HoadonLog.MaQuyen, "");
                txtMauSoMoi.Text = Utility.sDbnull(_HoadonLog.MauHoadon, "");
                txtKyHieuMoi.Text = Utility.sDbnull(_HoadonLog.KiHieu, "");
            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình lấy thông tin");
             
            }
        }

        bool KTRA_DULIEU()
        {
            try
            {

                if(string.IsNullOrEmpty(txtMauSoMoi.Text))
                {
                    Utility.ShowMsg("Mẫu số biên lai không được để trống");
                    txtMauSoMoi.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtKyHieuMoi.Text))
                {
                    Utility.ShowMsg("Ký hiệu biên lai không được để trống");
                    txtMauSoMoi.Focus();
                    return false;
                }
                if (string.IsNullOrEmpty(txtSoBienLaiMoi.Text))
                {
                    Utility.ShowMsg("Số biên lai không được để trống");
                    txtMauSoMoi.Focus();
                    return false;
                }
                if (Utility.DoTrim(txtSoBienLaiCu.Text).ToUpper() == Utility.DoTrim(txtSoBienLaiMoi.Text).ToUpper())
                {
                    Utility.ShowMsg("Số biên lai mới phải khác số biên lai cũ. Nếu bạn không muốn sửa biên lai, hãy nhấn nút thoát hoặc phím Escape");
                    txtSoBienLaiMoi.Focus();
                    return false;
                }

                QueryCommand cmd = HoadonLog.CreateQuery().BuildCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandSql = "SELECT * FROM hoadon_capphat lhm " +
                                 "WHERE lhm.mau_hoadon = '" + txtMauSoMoi.Text + "' AND lhm.MA_QUYEN = '" + txtmaquyenmoi.Text + "' AND lhm.KI_HIEU ='" + txtKyHieuMoi.Text + "' " +
                                 "AND (CONVERT(INT,lhm.SERIE_DAU) <= CONVERT(INT,'"+ txtSoBienLaiMoi.Text +"') " +
                                 "AND CONVERT(INT, lhm.SERIE_CUOI) >= CONVERT(INT,'" + txtSoBienLaiMoi.Text + "'))";
                DataTable temp = DataService.GetDataSet(cmd).Tables[0];
                if (temp.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tồn tại số biên lai trong danh sách. Mời bạn nhập lại hoặc khởi tạo danh mục biên lai mới");
                    return false;
                }
                SqlQuery sql2 = new Select().From(HoadonLog.Schema).Where(HoadonLog.Columns.MauHoadon).IsEqualTo(
                    txtMauSoMoi.Text)
                    .And(HoadonLog.Columns.MaQuyen).IsEqualTo(txtmaquyenmoi.Text)
                    .And(HoadonLog.Columns.KiHieu).IsEqualTo(txtKyHieuMoi.Text)
                    .And(HoadonLog.Columns.Serie).IsEqualTo(txtSoBienLaiMoi.Text).And(HoadonLog.Columns.TrangThai).IsEqualTo(0);
                if(sql2.GetRecordCount() > 0)
                {
                    Utility.ShowMsg(string.Format("Số serie mới {0} đã được sử dụng. Bạn không thể đổi sang số serie này. Mời bạn nhập số serie khác", Utility.DoTrim(txtSoBienLaiMoi.Text)));
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return false;               
            }
        }
        HoadonLog get_HoadonLog()
        {
            try
            {
                HoadonLog lHoadon = _HoadonLog;
                lHoadon.MaQuyen = Utility.sDbnull(txtmaquyenmoi.Text, "");
                lHoadon.MauHoadon = Utility.sDbnull(txtMauSoMoi.Text, "");
                lHoadon.KiHieu = Utility.sDbnull(txtKyHieuMoi.Text, "");
                lHoadon.Serie = Utility.sDbnull(txtSoBienLaiMoi.Text, "");
                return lHoadon;
            }
            catch (Exception)
            {

                Utility.ShowMsg("Có lỗi trong quá trình khởi tạo đối tượng HoadonLog");
                return null;
            }
        }
        private void cmdLuuThongTin_Click(object sender, EventArgs e)
        {
            try
            {
                if (!KTRA_DULIEU())
                {
                    return;
                }
                ActionResult actionResult = new KCB_THANHTOAN().UPDATE_SOBIENLAI(get_HoadonLog());
                switch (actionResult)
                {
                    case ActionResult.Success:
                        m_blnCancel = false;
                        Utility.ShowMsg("Đã sửa biên lai thành công");
                        Close();
                        break;
                    case ActionResult.Error:
                        Utility.ShowMsg("Sửa biên lai không thành công.");
                        break;
                }
            
            }
            catch (Exception ex)
            {
                Utility.CatchException("Có lỗi khi sửa biên lai", ex);
            }
        }

        private void frm_SUA_SOBIENLAI_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Escape)
            {
                Close();                
            }
        }

        private void cmdThoat_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
