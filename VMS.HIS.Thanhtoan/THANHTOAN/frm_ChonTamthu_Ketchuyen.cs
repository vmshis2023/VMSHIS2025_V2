using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VMS.HIS.DAL;
using VNS.HIS.UI.THANHTOAN;
using VNS.Libs;

namespace VMS.HIS.Thanhtoan.THANHTOAN
{
    public partial class frm_ChonTamthu_Ketchuyen : Form
    {
        KcbLuotkham objLuotkham;
        string lst_IDLoaithanhtoan = "ALL";
        public List<long> lstIdTamthu = new List<long>();
        public frm_ChonTamthu_Ketchuyen(KcbLuotkham objLuotkham, string lst_IDLoaithanhtoan)
        {
            InitializeComponent();
            this.objLuotkham = objLuotkham;
            this.lst_IDLoaithanhtoan = lst_IDLoaithanhtoan;
            uc_tamthu1._OnAction += Uc_tamthu1__OnAction;
        }

        private void Uc_tamthu1__OnAction(long id_thanhtoan, string ActType)
        {
            if(ActType=="IN")
            { 
                CallPhieuThu(id_thanhtoan);
            }
            else
            { 
            }    
        }
        private void CallPhieuThu(long id_thanhtoan)
        {



            KcbThanhtoan objThanhtoan = KcbThanhtoan.FetchByID(id_thanhtoan);
            if (objThanhtoan != null)
            {
                if (objLuotkham != null)
                {
                    frm_HuyThanhtoan frm = new frm_HuyThanhtoan(lst_IDLoaithanhtoan);
                    frm.objLuotkham = objLuotkham;
                    frm.v_Payment_Id = Utility.Int32Dbnull(objThanhtoan.IdThanhtoan, -1);
                    frm.Chuathanhtoan = 1;
                    frm.TotalPayment = 0;//Không cho phép in gộp các biên lai
                    frm.txtSoTienCanNop.Text = "0";
                    frm.ShowCancel = false;
                    frm.ShowDialog();
                }
            }

        }

        private void cmdAccept_Click(object sender, EventArgs e)
        {
            if(uc_tamthu1.grdPhieutamthu.GetCheckedRows().Length<=0)
            {
                Utility.ShowMsg("Bạn cần chọn ít nhất một phiếu tạm thu để thực hiện kết chuyển thanh toán");
                return;
            }
            var q = from p in uc_tamthu1.grdPhieutamthu.GetCheckedRows() select Utility.Int64Dbnull(p.Cells["id_thanhtoan"].Value);
            if (q.Any())
                lstIdTamthu = q.ToList<long>();
            this.DialogResult = DialogResult.OK;
            Close();
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
