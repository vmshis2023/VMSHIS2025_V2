using System;
using System.Transactions;
using System.Windows.Forms;
using SubSonic;
using VMS.HIS.DAL;
using VNS.Libs;

namespace VNS.HIS.UI.Forms.Dungchung
{
    public partial class frmUpdateMaBenhNhan : Form
    {
        public frmUpdateMaBenhNhan()
        {
            InitializeComponent();
        }

        private bool Kiemtra()
        {
            if (Utility.sDbnull(txttenbenhnhancu.Text.Trim()) != Utility.sDbnull(txttenbenhnhanmoi.Text.Trim()))
            {
                Utility.ShowMsg("Tên bệnh nhân không giống nhau! Bạn không thể update mã bệnh nhân được");
                return false;
            }
            if (Utility.Int32Dbnull(txtnamsinhcu.Text.Trim())!= Utility.Int32Dbnull(txtnamsinhmoi.Text.Trim()))
            {
                Utility.ShowMsg("Năm sinh không giống nhau! Bạn không thể update mã bệnh nhân được");
                return false;
            }
            return true;
        }
        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                //SqlQuery sqlkt = new Select().From(KcbLuotkham.Schema).Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(txtmabenhnhanmoi.Text);
                //if(sqlkt.GetRecordCount()>0)
                //{
                //    Utility.ShowMsg("Mã bệnh nhân này đang được sử dụng cho bệnh nhân khác! Bạn cần check lại mã mã bệnh nhân khác");
                //    txtmabenhnhanmoi.Focus();
                //    return;
                //}
                if(!Kiemtra()) return;
                ActionResult result = UpdateMabenhnhan();
                if (result == ActionResult.Error)
                {
                    Utility.ShowMsg("Lỗi không thực hiện ghép mã bệnh nhân ");
                }
                else
                {
                    Utility.Log(Name, globalVariables.UserName,
                          string.Format("Thực hiện update mã bệnh nhân thành công! Từ mã {0} sang mã {1}", txtmabenhnhancu.Text, txtmabenhnhanmoi.Text), newaction.Update,  this.GetType().Assembly.ManifestModule.Name);
                    Utility.ShowMsg("Bạn đã update mã bệnh nhân thành công!");
                }
               
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:"+ ex.Message);
            }
           
        }

        private ActionResult UpdateMabenhnhan()
        {
            try
            {
                using (var Scope = new TransactionScope())
                {
                    using (var dbScope = new SharedDbConnectionScope())
                    {
                        var sp = SPs.SpUpdateMaBenhNhan(Utility.sDbnull(txtmabenhnhanmoi.Text),
                            Utility.sDbnull(txtmabenhnhancu.Text));
                        sp.Execute();
                    }
                    Scope.Complete();
                    return ActionResult.Success;
                }
            }
            catch (Exception)
            {
                return ActionResult.Error;
            }
            

        }

        private void txtmalankhammoi_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && Utility.DoTrim(txtmabenhnhanmoi.Text) != "")
            {
                KcbDanhsachBenhnhan objBenhNhan =
                    new Select().From(KcbDanhsachBenhnhan.Schema)
                        .Where(KcbDanhsachBenhnhan.Columns.IdBenhnhan)
                        .IsEqualTo(Utility.Int64Dbnull(txtmabenhnhanmoi.Text))
                        .ExecuteSingle<KcbDanhsachBenhnhan>();
                if (objBenhNhan != null)
                {
                    txttenbenhnhanmoi.Text = Utility.sDbnull(objBenhNhan.TenBenhnhan);
                    txtnamsinhmoi.Text = Utility.sDbnull(objBenhNhan.NamSinh);
                }
            }
        }

        private void cmdThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void frmUpdateMaBenhNhan_Load(object sender, EventArgs e)
        {
            if (Utility.Int64Dbnull(txtmabenhnhancu.Text) > 0)
            {
                KcbDanhsachBenhnhan objBenhNhan =
                    new Select().From(KcbDanhsachBenhnhan.Schema)
                        .Where(KcbDanhsachBenhnhan.Columns.IdBenhnhan)
                        .IsEqualTo(Utility.Int64Dbnull(txtmabenhnhancu.Text))
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
