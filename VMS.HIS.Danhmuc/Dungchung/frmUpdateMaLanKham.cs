using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Transactions;
using System.Windows.Forms;
using VMS.HIS.DAL;
using VNS.Libs;
using SubSonic;

namespace VNS.HIS.UI.Forms.Dungchung
{
    public partial class frmUpdateMaLanKham : Form
    {
        public frmUpdateMaLanKham()
        {
            InitializeComponent();
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                SqlQuery sqlkt = new Select().From(KcbLuotkham.Schema).Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(txtmabenhnhanmoi.Text);
                if(sqlkt.GetRecordCount()>0)
                {
                    Utility.ShowMsg("Mã lần khám này đang được sử dụng cho bệnh nhân khác! Bạn cần check lại mã lượt khám khác");
                    txtmabenhnhanmoi.Focus();
                    return;
                }
                ActionResult result = UpdateMaLanKham();
                if (result == ActionResult.Error)
                {
                    Utility.ShowMsg("Bạn không update lần khám thành công!");    
                }
                else
                {
                    Utility.Log(Name, globalVariables.UserName,
                              string.Format("Thực hiện update mã lần khám thành công! Từ mã lần khám  {0} sang mã lần khám {1}", Utility.sDbnull(txtmabenhnhanmoi.Text), Utility.sDbnull(txtmabenhnhancu.Text)), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                    Utility.ShowMsg("Bạn đã update lần khám thành công!");    
                }
                
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
           
        }

        private ActionResult UpdateMaLanKham()
        {
            try
            {
                using (var db = new  TransactionScope())
                {
                    using (var sb = new SharedDbConnectionScope())
                    {
                        var sp = SPs.SpUpdateMaLuotKham(Utility.sDbnull(txtmabenhnhanmoi.Text), Utility.sDbnull(txtmabenhnhancu.Text));
                        sp.Execute();                            
                    }
                    db.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception ex)
            {
                return ActionResult.Error;
            }
        }
        private void txtmalankhammoi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && Utility.DoTrim(txtmabenhnhanmoi.Text) != "")
            {
                string _maluotkham  = Utility.AutoFullPatientCode(txtmabenhnhanmoi.Text);
                txtmabenhnhanmoi.Text = _maluotkham;
            }
        }

        private void cmdThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void frmUpdateMaLanKham_Load(object sender, EventArgs e)
        {
            if (Utility.Int64Dbnull(txtidbenhnhancu.Text) > 0)
            {
                KcbDanhsachBenhnhan objBenhNhan =
                    new Select().From(KcbDanhsachBenhnhan.Schema)
                        .Where(KcbDanhsachBenhnhan.Columns.IdBenhnhan)
                        .IsEqualTo(Utility.Int64Dbnull(txtidbenhnhancu.Text))
                        .ExecuteSingle<KcbDanhsachBenhnhan>();
                if (objBenhNhan != null)
                {
                    txttenbenhnhancu.Text = Utility.sDbnull(objBenhNhan.TenBenhnhan);
                    txtnamsinhcu.Text = Utility.sDbnull(objBenhNhan.NamSinh);
                }
            }
        }
    }
}
