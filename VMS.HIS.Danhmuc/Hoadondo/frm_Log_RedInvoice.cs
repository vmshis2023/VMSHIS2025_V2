using System;
using System.Data;
using System.Windows.Forms;
using VNS.Libs;
using VMS.HIS.DAL;
using SubSonic;
namespace VNS.HIS.UI.HOADONDO
{
    public partial class frm_Log_RedInvoice : Form
    {
        private DataTable dtList;

        public frm_Log_RedInvoice()
        {
            InitializeComponent();
        }

        private void frm_Log_RedInvoice_Load(object sender, EventArgs e)
        {
            try
            {
                LoadTrang_Thai();
                Load_Lydo();
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
                new Select("*").From(HoadonTrangthai.Schema).Where(HoadonTrangthai.Columns.MessageType).IsEqualTo("TrangThaiInHD").
                    OrderAsc(HoadonTrangthai.Columns.MessageOrder).ExecuteDataSet().Tables[0];
            DataBinding.BindDataCombobox(cboTrang_Thai, dt, HoadonTrangthai.Columns.MessageId, HoadonTrangthai.Columns.MessageName,"Chọn",true);
        }

        private void Load_NVien()
        {
            DataTable dtNVien =
                new Select(SysUser.Columns.PkSuid, "(PK_sUID + isnull(' - ' + sFullName,'')) as User_Info").
                    From(SysUser.Schema).ExecuteDataSet().Tables[0];
            DataBinding.BindDataCombobox(cboStaff, dtNVien, SysUser.Columns.PkSuid, "User_Info","Chọn",true);
        }

        private void Load_Lydo()
        {
            DataTable dtLyDo =
                new Select("*").From(HoadonTrangthai.Schema).Where(HoadonTrangthai.Columns.MessageType).IsEqualTo("LyDoInHD").
                    OrderAsc(HoadonTrangthai.Columns.MessageOrder).ExecuteDataSet().Tables[0];
            DataBinding.BindDataCombobox(cbo_LyDo, dtLyDo, HoadonTrangthai.Columns.MessageId, HoadonTrangthai.Columns.MessageName,"Chọn",true);
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            try
            {

                dtList =
                    SPs.HoadonLaythongtinlogs(Utility.sDbnull(txtSerie_Dau.Text, "NONE"),
                                           Utility.sDbnull(txtSerie_Cuoi.Text, "NONE"),
                                           chkFromDate.Checked,
                                           dtpFromDate.Value.Date,
                                           dtpToDate.Value.Date.AddDays(1).AddMilliseconds(-1),
                                           Utility.sDbnull(cboStaff.SelectedValue),
                                           Utility.sDbnull(cbo_LyDo.SelectedValue),
                                           Utility.Int16Dbnull(cboTrang_Thai.SelectedValue)).GetDataSet().Tables[0];
                        Utility.SetDataSourceForDataGridEx(grdList, dtList, true, true, "", "", true);
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
                        cmdPrintInvoice.PerformClick();
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

        private void cmdPrintInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                frm_Print_RedInvoice oForm = new frm_Print_RedInvoice();
                oForm.payment_ID = 61;
                oForm.dtList = dtList;
                oForm.ShowDialog();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }

        }

    }
}
