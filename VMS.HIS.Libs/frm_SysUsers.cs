using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using VMS.HIS.DAL;

namespace VNS.Libs
{
    public partial class frm_SysUsers : Form
    {
        public string TenBaoCao { get; set; }
        public string FontName = "";
        public string FontSize = "";
        public string FontStyle = "";
        public string NoiDung = "";
        public frm_SysUsers()
        {
            InitializeComponent();
            
            Utility.GetResizeForm(this);
        }

     
        private void cmdUpdateAllUser_Click(object sender, EventArgs e)
        {
            if(grdSysUser.GetCheckedRows().Length<=0)
            {
                Utility.ShowMsg("Bạn phải chọn thông tin trên lưới thực hiện ","Thông báo");
                return;
            }
            try
            {
                if(chkOverUser.Checked)
                {
                 foreach (Janus.Windows.GridEX.GridEXRow gridExRow  in grdSysUser.GetCheckedRows())
                 {

                     new Delete().From(SysTrinhky.Schema)
                         .Where(SysTrinhky.Columns.ReportName).IsEqualTo(TenBaoCao)
                         .And(SysTrinhky.Columns.ObjectName).IsEqualTo(Utility.sDbnull(gridExRow.Cells[SysUser.Columns.PkSuid].Value, "")).Execute();



                 }  
                }
                foreach (Janus.Windows.GridEX.GridEXRow gridExRow  in grdSysUser.GetCheckedRows())
                {
                    SqlQuery sqlQuery = new Select().From(SysTrinhky.Schema)
                        .Where(SysTrinhky.Columns.ReportName).IsEqualTo(TenBaoCao)
                        .And(SysTrinhky.Columns.ObjectName).IsEqualTo(
                            Utility.sDbnull(gridExRow.Cells[SysUser.Columns.PkSuid].Value, ""));
                       if(sqlQuery.GetRecordCount()<=0)
                        SysTrinhky.Insert(TenBaoCao, Utility.sDbnull(gridExRow.Cells[SysUser.Columns.PkSuid].Value, ""),NoiDung);
                }
                Utility.ShowMsg("Bạn thực hiện thành công","Thông báo thành công");

            }
            catch (Exception)
            {

                throw;
            }

        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_SysUsers_Load(object sender, EventArgs e)
        {
            SysUserCollection sysUserCollection = new Select().From(SysUser.Schema).ExecuteAsCollection<SysUserCollection>();
           // grdSysUser.DataSource = sysUserCollection.ToDataTable();
            Utility.SetDataSourceForDataGridEx(grdSysUser,sysUserCollection.ToDataTable(),true,true,"","");

            SqlQuery sqlQuery = new Select().From(SysTrinhky.Schema)
               .Where(SysTrinhky.Columns.ReportName).IsEqualTo(TenBaoCao)
               .And(SysTrinhky.Columns.ObjectName).IsEqualTo(globalVariables.UserName);
            SysTrinhky objSysTrinhky = sqlQuery.ExecuteSingle<SysTrinhky>();
            if(objSysTrinhky!=null)
            {
                NoiDung = Utility.sDbnull(objSysTrinhky.Trinhky);
            }


        }

        private void frm_SysUsers_ResizeEnd(object sender, EventArgs e)
        {
            Utility.SetResize(this);
        }
    }
}
