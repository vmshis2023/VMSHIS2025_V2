using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using VMS.HIS.DAL;
using VNS.Libs;
namespace CIS2008
{
    public partial class frm_Sysparams : Form
    {
        public frm_Sysparams()
        {
            InitializeComponent();
            this.KeyDown += frm_Sysparams_KeyDown;
            this.Load += frm_Sysparams_Load;
            grdList.UpdatingCell += grdList_UpdatingCell;
            this.FormClosing += frm_Sysparams_FormClosing;
        }

        void frm_Sysparams_FormClosing(object sender, FormClosingEventArgs e)
        {
            globalVariables.gv_dtSysparams = new Select().From(SysSystemParameter.Schema).ExecuteDataSet().Tables[0];
            globalVariables.gv_dtSysTieude = new Select().From(SysTieude.Schema).ExecuteDataSet().Tables[0];
            THU_VIEN_CHUNG.LoadThamSoHeThong();
        }

        void frm_Sysparams_Load(object sender, EventArgs e)
        {
            Utility.SetDataSourceForDataGridEx_Basic(grdList, globalVariables.gv_dtSysparams, true, true, "1=1", SysSystemParameter.Columns.SName);
        }

        void grdList_UpdatingCell(object sender, Janus.Windows.GridEX.UpdatingCellEventArgs e)
        {
            
            try
            {
                if (e.Column.Key == SysSystemParameter.Columns.SValue)
                {
                    int id = Utility.Int32Dbnull(grdList.CurrentRow.Cells[SysSystemParameter.Columns.Id].Value);
                    new Update(SysSystemParameter.Schema)
                        .Set(SysSystemParameter.Columns.SValue).EqualTo(e.Value)
                        .Where(SysSystemParameter.Columns.Id).IsEqualTo(id)
                        .Execute();

                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }

        void frm_Sysparams_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();

        }
    }
}
