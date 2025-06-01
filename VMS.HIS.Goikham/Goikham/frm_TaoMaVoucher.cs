using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VMS.HIS.DAL;
using VNS.HIS.UI.NGOAITRU;
using SubSonic;
using VNS.Libs;
using VNS.HIS.BusRule.Classes;
using VNS.Properties;
using VNS.HIS.UI.DANHMUC;
using Janus.Windows.GridEX.EditControls;
using Janus.Windows.EditControls;

namespace VNS.HIS.UI.GOIKHAM
{
    public partial class frm_TaoMaVoucher : Form
    {
        public DateTime ngay_thuchien = DateTime.Now;
        public int sluong=1;
        public int dodai = 6;
        int id_goi = 0;//id chương trình giảm giá voucher, id chương trình khuyến mại
        public bool m_blnCancel = true;
        public frm_TaoMaVoucher()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            InitEvents();
        }
        public frm_TaoMaVoucher(string loaidanhmuc,string title1,string title2,string _name,string ngaythuchien)
        {
            InitializeComponent();
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Text = title1;
            lblDate.Text = ngaythuchien;
            lblTitle1.Text = title1;
            lblTitle2.Text = title2;
            InitEvents();
        }
        void InitEvents()
        {
            this.FormClosing += new FormClosingEventHandler(frm_TaoMaVoucher_FormClosing);
            this.Load += new EventHandler(frm_TaoMaVoucher_Load);
            this.KeyDown += new KeyEventHandler(frm_TaoMaVoucher_KeyDown);
            cmdClose.Click+=new EventHandler(cmdClose_Click);
            cmdSave.Click+=new EventHandler(cmdSave_Click);
        }

      

        void cmdClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        void frm_TaoMaVoucher_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Escape)
                cmdClose_Click(cmdClose, new EventArgs());
            if (e.KeyCode == Keys.S && e.Control) cmdSave.PerformClick();
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
        }

        void frm_TaoMaVoucher_Load(object sender, EventArgs e)
        {
            try
            {

                nmrNumber.Focus();
            }
            catch
            {
            }
            finally
            {
            }
        }
     

        void frm_TaoMaVoucher_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
       

        void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.EnableButton(cmdSave, false);
                sluong =(int) nmrNumber.Value;
                dodai = (int)nmrLength.Value;
                prgb_update.Visible = true;
                prgb_update.Maximum = sluong;
                prgb_update.Minimum = 0;
                prgb_update.Step = 1;
                prgb_update.Value = 1;
                GoiMaVoucher newItem=null;
                for (int i = 1; i <= sluong; i++) 
                {
                    if (prgb_update.Value + 1 > prgb_update.Maximum)
                        prgb_update.Value = prgb_update.Maximum;
                    else
                        prgb_update.Value += 1;
                    //
                    if (newItem == null)
                    {
                        newItem = new GoiMaVoucher();
                        newItem.NgayTao = DateTime.Now;
                        newItem.NguoiTao = globalVariables.UserName;
                        newItem.IdVoucher = id_goi;

                        newItem.TrangThai = (byte)0;
                        newItem.IpMaytao = globalVariables.gv_strIPAddress;
                        newItem.TenMaytao = globalVariables.gv_strComputerName;
                    }
                    else
                    {
                        newItem.IsNew = true;
                    }
                    
                    newItem.MaVoucher = Security.HardWare.GetKey(Guid.NewGuid().ToString("N").Substring(0, 6));

                    newItem.Save();
                    Application.DoEvents();
                }
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                m_blnCancel = false;
                this.Close();
            }
            catch (Exception ex)
            {
                Utility.EnableButton(cmdSave, true);
                Utility.ShowMsg("Lỗi khi nhấn nút chấp nhận:\n" + ex.Message);
                throw;
            }
            finally
            {
                Utility.EnableButton(cmdSave, true);
            }
        }
     
        
    }
}
