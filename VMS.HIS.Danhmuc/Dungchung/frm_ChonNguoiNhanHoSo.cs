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
using VNS.Libs;
namespace VNS.HIS.UI.Forms.Cauhinh
{
    public partial class frm_ChonNguoiNhanHoSo : Form
    {
        public string v_Patient_Code = "";
        public DateTime pdt_InputDate = globalVariables.SysDate;
        public bool b_Cancel = true;
        public bool _hienthinhanvien = false;
        EmrBa objBA;
        public frm_ChonNguoiNhanHoSo(EmrBa objBA)
        {
            InitializeComponent();
            this.objBA = objBA;
            this.KeyDown+=frm_ChonNguoiNhanHoSo_KeyDown;
         
            dtCreateDate.Value = globalVariables.SysDate;
        }
        
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        int num = 0;
        private void cmdAccept_Click(object sender, EventArgs e)
        {
            Utility.SetMsg(lblMsg, "", true);
          
            if (Utility.Int32Dbnull(cbo_nguoinhanhoso.SelectedValue, -1) <= 0)
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn người nhận hồ sơ", true);
                cbo_nguoinhanhoso.SelectAll();
                cbo_nguoinhanhoso.Focus();
                return;
            }
            DmucNhanvien objNV = DmucNhanvien.FetchByID(Utility.Int32Dbnull(cbo_nguoinhanhoso.SelectedValue, -1));
            if (objNV != null)
            {
                num= new Update(EmrBa.Schema)
                    .Set(EmrBa.Columns.IdNguoinhanHoso).EqualTo(objNV.IdNhanvien)
                    .Set(EmrBa.Columns.MaNguoinhanHoso).EqualTo(objNV.MaNhanvien)
                    .Set(EmrBa.Columns.TrangThai).EqualTo(4)
                    .Where(EmrBa.Columns.IdBa).IsEqualTo(objBA.IdBa).Execute();
            }
            if (num > 0)
                Utility.ShowMsg("Đã nhận Hồ sơ người bệnh thành công");
            b_Cancel = false;
            pdt_InputDate = dtCreateDate.Value;
            this.Close();
        }

        private void frm_ChonNguoiNhanHoSo_Load(object sender, EventArgs e)
        {
            DataBinding.BindDataCombobox(cbo_nguoinhanhoso, globalVariables.gv_dtDmucNhanvien, DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.TenNhanvien);
        }

        
        private void frm_ChonNguoiNhanHoSo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && _hienthinhanvien) this.ProcessTabKey(true);
            if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
            if(e.Control&&(e.KeyCode==Keys.A || e.KeyCode==Keys.S))cmdAccept.PerformClick();
        }

        
    }
}
