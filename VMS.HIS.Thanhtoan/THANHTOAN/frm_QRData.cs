using Janus.Windows.GridEX;
using SubSonic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VMS.HIS.DAL;
using VNS.HIS.BusRule.Classes;
using VNS.Libs;
using VNS.Properties;

namespace VNS.HIS.UI.THANHTOAN
{
    public partial class frm_QRData : Form
    {
        KCB_THANHTOAN _THANHTOAN = new KCB_THANHTOAN();
        public KcbLuotkham objLuotkham;
        public long id_thanhtoan;
          KcbThanhtoan objThanhtoan;
        public frm_QRData(long id_thanhtoan)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.id_thanhtoan = id_thanhtoan;
            this.Text = string.Format("Dữ liệu sinh QR Code cho thanh toán có Id = {0}", id_thanhtoan);
        }

        private void frm_QRData_Load(object sender, EventArgs e)
        {
            try
            {
                objThanhtoan = KcbThanhtoan.FetchByID(id_thanhtoan);
                DataTable dtQRcode =
                new Select().From(QrPhieuThanhtoan.Schema)
                    .Where(QrPhieuThanhtoan.Columns.MaLuotkham).IsEqualTo(objThanhtoan.MaLuotkham)
                    .And(QrPhieuThanhtoan.Columns.IdBenhnhan).IsEqualTo(objThanhtoan.IdBenhnhan)
                    .And(QrPhieuThanhtoan.Columns.IdThanhtoan).IsEqualTo(objThanhtoan.IdThanhtoan)
                    .ExecuteDataSet()
                    .Tables[0];
                Utility.SetDataSourceForDataGridEx(grdListQrcode, dtQRcode, false, true, "1=1", "");

            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }
       
    }
}
