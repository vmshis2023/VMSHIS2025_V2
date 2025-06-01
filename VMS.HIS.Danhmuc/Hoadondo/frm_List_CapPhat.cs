using System;
using System.Data;
using System.Windows.Forms;
using VNS.Libs;
using VMS.HIS.DAL;
using SubSonic;
namespace VNS.HIS.UI.HOADONDO
{
    public partial class frm_List_CapPhat : Form
    {
        private DataTable dtCapPhat;

        public frm_List_CapPhat()
        {
            InitializeComponent();
        }

        private void frm_Log_RedInvoice_Load(object sender, EventArgs e)
        {
            try
            {
                LoadTrang_Thai();
                Load_NVien();

                cmdSearch.PerformClick();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
                Close();
            }
        }

        private void LoadTrang_Thai()
        {
            DataTable dt =
                new Select("*").From(HoadonTrangthai.Schema).Where(HoadonTrangthai.Columns.MessageType).IsEqualTo("CapPhatHD").
                    OrderAsc(HoadonTrangthai.Columns.MessageOrder).ExecuteDataSet().Tables[0];
            DataBinding.BindDataCombobox(cboTrang_Thai, dt, HoadonTrangthai.Columns.MessageId, HoadonTrangthai.Columns.MessageName,"Chọn",true);
        }

        private void Load_NVien()
        {
            DataTable dtNVien =
                new Select(SysUser.Columns.PkSuid, "(PK_sUID + isnull(' - ' + sFullName,'')) as User_Info").
                    From(SysUser.Schema).ExecuteDataSet().Tables[0];
            DataBinding.BindDataCombobox(cboStaff, dtNVien, SysUser.Columns.PkSuid, "User_Info", "Chọn", true);
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            try
            {
                dtCapPhat =
                            SPs.HoadonLaydanhsachHoadonCapphat(Utility.sDbnull(txtSerie_Dau.Text, "NONE"),
                                                   Utility.sDbnull(txtSerie_Cuoi.Text, "NONE"),
                                                   chkFromDate.Checked,
                                                   dtpFromDate.Value.Date,
                                                   dtpToDate.Value.Date.AddDays(1).AddMilliseconds(-1),
                                                   Utility.sDbnull(cboStaff.SelectedValue),
                                                   Utility.Int16Dbnull(cboTrang_Thai.SelectedValue)).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdCapPhat, dtCapPhat, true, true, "", "", true);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void frm_Log_RedInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
                switch (e.KeyCode)
                {
                    case Keys.P:
                        break;
                }
            else
                switch (e.KeyCode)
                {
                    case Keys.Escape:
                        cmdThoat.PerformClick();
                        break;
                }
        }

        private void cmdThoat_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void grdCapPhat_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if ( grdCapPhat.CurrentRow == null) return;
                DataTable dt =
                    new Select(
                        "*, (Select top 1 lm.Message_Name from L_Message lm where lm.Message_Type = 'TrangThaiInHD' and  lm.Message_ID = TRANG_THAI) as Trang_Thai_Message," +
                        "(Select top 1 lm.Message_Name from L_Message lm where lm.Message_Type = 'LyDoInHD' and  lm.Message_ID = MA_LDO) as Lydo_Message," +
                        "(Select top 1 tpi.ten_benhnhan from kcb_danhsach_benhnhan tpi where tpi.id_benhnhan = [HOADON_LOG].id_benhnhan) as ten_benhnhan")
                        .From(HoadonLog.Schema).Where(HoadonLog.Columns.IdCapphat).
                        IsEqualTo(Utility.Int32Dbnull(grdCapPhat.GetValue(HoadonLog.Columns.IdCapphat))).
                        OrderDesc(HoadonLog.Columns.IdHdonLog).ExecuteDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdLog,dt,true,true,"","",true);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void tsmTachHDon_Click(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {
                Utility.ShowMsg("Có lỗi trong quá trình tách hóa đơn");              
            }
        }
    }
}
