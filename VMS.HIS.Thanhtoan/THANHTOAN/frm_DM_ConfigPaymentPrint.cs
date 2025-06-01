using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;
namespace VNS.HIS.UI.THANHTOAN
{
    public partial class frm_DM_ConfigPaymentPrint : Form
    {
        public frm_DM_ConfigPaymentPrint()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_DM_ConfigPaymentPrint_Load(object sender, EventArgs e)
        {
            LoadListPrint();
            IntitalData();
        }
        private void IntitalData()
        {
            SysUserPrinter objSysUserPrinter = SysUserPrinter.FetchByID(globalVariables.UserName);
            if(objSysUserPrinter!=null)
            {
                txtQuantity.Value = Utility.Int32Dbnull(objSysUserPrinter.PagerCopy, 1);
                txtLapBangKe.Text = objSysUserPrinter.CreatedBy;
                txtGiamdinhbaohiem.Text = objSysUserPrinter.InsuranceBy;
                txtDaidienkhamchuabenh.Text = objSysUserPrinter.HospitalBy;
                txtKetoan.Text = objSysUserPrinter.InsuranceBy;

                cboListPrint.SelectedIndex = Utility.GetSelectedIndex(cboListPrint, objSysUserPrinter.PrinterName);


            }

        }
        private List<string> ListPrinter = new List<string>();

        private void LoadListPrint()
        {
            
            foreach (string printerName in PrinterSettings.InstalledPrinters)
            {
                ListPrinter.Add(printerName);
            }
            cboListPrint.DataSource = ListPrinter;
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            SqlQuery q = new Select().From(SysUserPrinter.Schema)
                .Where(SysUserPrinter.Columns.SysUserName).IsEqualTo(globalVariables.UserName);
            if(q.GetRecordCount()<=0)
            {
                new SysUserPrinterController().Insert(globalVariables.UserName, txtQuantity.Value,
                                                      Utility.sDbnull(cboListPrint.Text, ""), txtKetoan.Text,
                                                      txtLapBangKe.Text, txtGiamdinhbaohiem.Text,
                                                      txtDaidienkhamchuabenh.Text);

            }else
            {
                new Update(SysUserPrinter.Schema)
                    .Set(SysUserPrinter.Columns.PrinterName).EqualTo(cboListPrint.Text)
                    .Set(SysUserPrinter.Columns.CreatedBy).EqualTo(txtLapBangKe.Text)
                    .Set(SysUserPrinter.Columns.AccountName).EqualTo(txtKetoan.Text)
                    .Set(SysUserPrinter.Columns.InsuranceBy).EqualTo(txtGiamdinhbaohiem.Text)
                    .Set(SysUserPrinter.Columns.HospitalBy).EqualTo(txtDaidienkhamchuabenh.Text)
                    .Set(SysUserPrinter.Columns.PagerCopy).EqualTo(Utility.Int32Dbnull(txtQuantity.Text, 1))
                    .Where(SysUserPrinter.Columns.SysUserName).IsEqualTo(globalVariables.UserName).Execute();
            }
            Utility.ShowMsg("Bạn cập nhập thông tin thành công","Thông báo");
            this.Close();
        }
    }
}
