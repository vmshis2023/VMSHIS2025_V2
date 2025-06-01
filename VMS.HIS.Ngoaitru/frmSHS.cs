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

namespace VMS.HIS.Ngoaitru
{
    public partial class frmSHS : Form
    {
        public frmSHS()
        {
            InitializeComponent();
            this.SizeChanged += FrmSHS_SizeChanged;
            this.KeyDown += FrmSHS_KeyDown;
        }

        private void FrmSHS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == Keys.I || e.KeyCode == Keys.H))
                cmdHide.PerformClick();
            else if (e.Control && (e.KeyCode == Keys.V || e.KeyCode == Keys.S))
                cmdShow.PerformClick();
            if (e.KeyCode == Keys.Escape)
                cmdExit.PerformClick();
        }

        private void FrmSHS_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                grdListShow.Width = Convert.ToInt32(Math.Ceiling((decimal)(this.Width / 2)));
            }
            catch (Exception)
            {

            }
        }

        DataTable dt_Show;
        DataTable dt_Hide;
        DataTable dtAll;
        private void frmSHS_Load(object sender, EventArgs e)
        {
            try
            {
                DataTable dtDichvuCls = new Select().From(DmucDichvuclsChitiet.Schema).ExecuteDataSet().Tables[0];
                DataBinding.BindDataCombobox(cboDichvuCLS, dtDichvuCls, DmucDichvuclsChitiet.Columns.IdChitietdichvu, DmucDichvuclsChitiet.Columns.TenChitietdichvu,"---Chọn dịch vụ cls---",true);

                SearchData();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        void SearchData()
        {
            try
            {
                dtAll = new KCB_DANGKY().ShsTimkiemBenhnhan("01/01/1900", "01/01/1900", -1, -1, "", -1, "", "", new DateTime(1900, 1, 1), 100, "", globalVariables.MA_KHOA_THIEN, 2, (byte)100, "ALL", 1, Utility.Int32Dbnull(cboDichvuCLS.SelectedValue));
                dt_Show = dtAll.Clone();
                dt_Hide = dtAll.Clone();
                DataRow[] arrDr = dtAll.Select("SHS=1 or SHS is null");
                if (arrDr.Length > 0)
                    dt_Show = arrDr.CopyToDataTable();
                arrDr = dtAll.Select("SHS=0");
                if (arrDr.Length > 0)
                    dt_Hide = arrDr.CopyToDataTable();
                Utility.SetDataSourceForDataGridEx(grdListShow, dt_Show, true, true, "1=1", KcbDanhsachBenhnhan.Columns.TenBenhnhan + " asc");
                Utility.SetDataSourceForDataGridEx(grdListHide, dt_Hide, true, true, "1=1", KcbDanhsachBenhnhan.Columns.TenBenhnhan + " asc");
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        private void cmdHide_Click(object sender, EventArgs e)
        {
            if(grdListShow.GetCheckedRows().Count()<=0)
            {
                Utility.ShowMsg("Bạn cần chọn ít nhất 1 người bệnh trên lưới danh sách để thực hiện ẩn");
                return;
            }    
            foreach (GridEXRow _row in grdListShow.GetCheckedRows())
            {
                try
                {
                    long id_benhnhan = Utility.Int64Dbnull(_row.Cells["id_benhnhan"].Value, -1);
                    string ma_luotkham = Utility.sDbnull(_row.Cells["ma_luotkham"].Value, "-1");
                    int num = SPs.TiepdonShs(id_benhnhan, ma_luotkham, 0).Execute();
                    DataRow dr = Utility.getCurrentDataRow(_row);
                    dr["SHS"] = 0;
                    dt_Hide.ImportRow(dr);
                    dt_Show.Rows.Remove(dr);
                    Utility.Log(Name, globalVariables.UserName, string.Format("HIDE người bệnh với thông tin id={0},ma_luotkham={1}", id_benhnhan, ma_luotkham), newaction.View, this.GetType().Assembly.ManifestModule.Name);
                }
                catch (Exception ex)
                {

                    Utility.CatchException(ex);
                }
            }
        }

        private void cmdShow_Click(object sender, EventArgs e)
        {
            if (grdListHide.GetCheckedRows().Count() <= 0)
            {
                Utility.ShowMsg("Bạn cần chọn ít nhất 1 người bệnh trên lưới danh sách để thực hiện ẩn");
                return;
            }
            foreach (GridEXRow _row in grdListHide.GetCheckedRows())
            {
                try
                {
                    long id_benhnhan = Utility.Int64Dbnull(_row.Cells["id_benhnhan"].Value, -1);
                    string ma_luotkham = Utility.sDbnull(_row.Cells["ma_luotkham"].Value, "-1");
                    int num = SPs.TiepdonShs(id_benhnhan, ma_luotkham, 1).Execute();
                    DataRow dr = Utility.getCurrentDataRow(_row);
                    dr["SHS"] = 1;
                    
                    dt_Show.ImportRow(dr);
                    dt_Hide.Rows.Remove(dr);
                    Utility.Log(Name, globalVariables.UserName, string.Format("SHOW người bệnh với thông tin id={0},ma_luotkham={1}", id_benhnhan, ma_luotkham), newaction.View, this.GetType().Assembly.ManifestModule.Name);
                }
                catch (Exception ex)
                {

                    Utility.CatchException(ex);
                }
            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            SearchData();
        }

        private void cboDichvuCLS_SelectedIndexChanged(object sender, EventArgs e)
        {
            SearchData();
        }
    }
}
