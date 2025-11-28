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
using System.Transactions;

namespace VMS.HIS.UI.EMR
{
    public partial class frm_NhaplydoTuchoi_BA : Form
    {
        public DateTime ngay_thuchien = DateTime.Now;
        public string ma="";
        public string ten = "";
        public bool m_blnCancel = true;
        bool ly_do_trong_danh_muc = false;
        EmrBa objBA = null;
        string _name = "";
        public frm_NhaplydoTuchoi_BA(EmrBa objBA)
        {
            InitializeComponent();
            this.objBA = objBA;
            Utility.SetVisualStyle(this);
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            dtp_ngaytuchoi.Value = DateTime.Now;
            txt_lantuchoi.Text =( Utility.Int32Dbnull(objBA.SolanBiTuchoiHsba,0)+1).ToString();
            this._name = lblName.Text;
            InitEvents();
        }
       
        void InitEvents()
        {
            this.FormClosing += new FormClosingEventHandler(frm_NhaplydoHuy_FormClosing);
            this.Load += new EventHandler(frm_NhaplydoHuy_Load);
            this.KeyDown += new KeyEventHandler(frm_NhaplydoHuy_KeyDown);
           
            cmdClose.Click+=new EventHandler(cmdClose_Click);
            cmdSave.Click+=new EventHandler(cmdSave_Click);
        }


        void cmdClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
        void frm_NhaplydoHuy_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Escape)
                cmdClose_Click(cmdClose, new EventArgs());
            if (e.KeyCode == Keys.S && e.Control) cmdSave.PerformClick();
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
        }

        void frm_NhaplydoHuy_Load(object sender, EventArgs e)
        {
            txt_lydotu_choi.Focus();
        }
     

        void frm_NhaplydoHuy_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
       

        void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                Utility.EnableButton(cmdSave, false);
                if (!isValidData()) return;
               
                ten = Utility.DoTrim(txt_lydotu_choi.Text);
                ngay_thuchien = dtp_ngaytuchoi.Value;
                EmrLogTuchoiHosoBA objtuchoi = new EmrLogTuchoiHosoBA();
                objtuchoi.NguoiTao = globalVariables.UserName;
                objtuchoi.NgayTao = globalVariables.SysDate;
                objtuchoi.NguoiTuchoi = globalVariables.UserName;
                objtuchoi.NgayTuchoi = globalVariables.SysDate;
                objtuchoi.IdBa = objBA.IdBa;
                objtuchoi.LanTuchoi = Utility.ByteDbnull(txt_lantuchoi.Text,1);
                objtuchoi.LydoTuchoi = Utility.DoTrim(txt_lydotu_choi.Text);
                using (var scope = new TransactionScope())
                {
                    using (var sp = new SharedDbConnectionScope())
                    {
                        objtuchoi.Save();
                        new Update(EmrBa.Schema)
                            .Set(EmrBa.Columns.SolanBiTuchoiHsba).EqualTo(objtuchoi.LanTuchoi)
                            .Set(EmrBa.Columns.TrangThai).EqualTo(2)//Đưa về trạng thái vừa hoàn tất BA
                            .Where(EmrBa.Columns.IdBa).IsEqualTo(objtuchoi.IdBa)
                            .And(EmrBa.Columns.TrangThai).IsEqualTo(3)//đang ở trạng thái chờ KHTT phê duyệt
                            .Execute();
                    }
                    scope.Complete();
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
     
        private bool isValidData()
        {
            Utility.SetMsg(lblMsg, "", true);
           
            if ( Utility.sDbnull(txt_lydotu_choi.Text) == "") 
            {
                Utility.ShowMsg("Bạn cần nhập lý do từ chối hồ sơ bệnh án");
                txt_lydotu_choi.Focus();
                txt_lydotu_choi.SelectAll();
                return false;
            }
            return true;
        }
       
    }
}
