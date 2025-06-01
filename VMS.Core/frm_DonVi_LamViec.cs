using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;

using SubSonic;

using VMS.HIS.DAL;
using VNS.Libs;

namespace CIS.CoreApp
{
    public partial class frm_DonVi_LamViec : Form
    {
        public frm_DonVi_LamViec()
        {
            InitializeComponent();
            
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// hàm thực hiện việc cập nhập đơn vị làm việc
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_DonVi_LamViec_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ProcessTabKey(true);
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.S && e.Control) cmdSave.PerformClick();
        }
        
        private void cmdSave_Click(object sender, EventArgs e)
        {
            SqlQuery sqlQuery = new Select().From(SysManagementUnit.Schema)
                .Where(SysManagementUnit.Columns.PkSBranchID).IsEqualTo("LIS");
           
            if (sqlQuery.GetRecordCount() <= 0)
            {
                //LDonVi.Insert("LIS", txtDonViCapTren.Text, txtDonVi.Text, txtDiaChi.Text, txtDienThoai.Text);
                SysManagementUnit objSysManagementUnit = new SysManagementUnit();
                objSysManagementUnit.PkSBranchID = "LIS";
                objSysManagementUnit.SName = txtDonVi.Text;
                objSysManagementUnit.SParentBranchName = txtDonViCapTren.Text;
                objSysManagementUnit.SAddress = txtDiaChi.Text;
                objSysManagementUnit.SPhone = txtDienThoai.Text;
                objSysManagementUnit.IsNew = true;
                objSysManagementUnit.Save();
            }
            else
            {

                SysManagementUnit objSysManagementUnit = new SysManagementUnit();
                objSysManagementUnit.MarkOld();
                objSysManagementUnit.IsLoaded = true;
                objSysManagementUnit.PkSBranchID = "LIS";
                objSysManagementUnit.SName = txtDonVi.Text;
                objSysManagementUnit.SParentBranchName = txtDonViCapTren.Text;
                objSysManagementUnit.SAddress = txtDiaChi.Text;
                objSysManagementUnit.SPhone = txtDienThoai.Text;
               // objSysManagementUnit.IsNew = true;
                objSysManagementUnit.Save();
            }
            Utility.ShowMsg("Bạn thực hiện lưu thành công","Thông báo");
        }
        private void GetData()
        {
            SqlQuery sqlQuery = new Select().From(SysManagementUnit.Schema)
               .Where(SysManagementUnit.Columns.PkSBranchID).IsEqualTo("LIS");
            SysManagementUnit objSysManagementUnit = sqlQuery.ExecuteSingle<SysManagementUnit>();
           // LDonVi objDonVi = LDonVi.FetchByID("LIS");
            if (objSysManagementUnit != null)
            {
                txtDiaChi.Text = objSysManagementUnit.SAddress;
                txtDienThoai.Text = objSysManagementUnit.SPhone;
                txtDonViCapTren.Text = objSysManagementUnit.SParentBranchName;
                txtDonVi.Text = objSysManagementUnit.SName;
            }
        }
        /// <summary>
        /// hàm thực hiện viecj load thông tin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_DonVi_LamViec_Load(object sender, EventArgs e)
        {
            GetData();
        }
    }
}
