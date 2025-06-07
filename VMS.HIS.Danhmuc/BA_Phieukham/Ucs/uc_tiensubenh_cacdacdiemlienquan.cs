using Janus.Windows.GridEX.EditControls;
using SubSonic;
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

namespace VMS.EMR.PHIEUKHAM.Ucs
{
    public partial class uc_tiensubenh_cacdacdiemlienquan : UserControl
    {
        KcbLuotkham objLuotkham;
        EmrTiensubenhDacdiemlienquan objtsb = null;
        public uc_tiensubenh_cacdacdiemlienquan()
        {
            InitializeComponent();
            chkDiUng.CheckedChanged += chkDiUng_CheckedChanged;
            chkMaTuy.CheckedChanged += chkMaTuy_CheckedChanged;
            chkRuouBia.CheckedChanged += chkRuouBia_CheckedChanged;
            chkThuocLa.CheckedChanged += chkThuocLa_CheckedChanged;
            chkThuocLao.CheckedChanged += chkThuocLao_CheckedChanged;
            chkKhac.CheckedChanged += chkKhac_CheckedChanged;
        }
        public void HandleKeyEnter()
        {
            Control activeCtrl = Utility.getActiveControl(this);
            if (activeCtrl.GetType().Equals(typeof(EditBox)))
            {
                EditBox box = activeCtrl as EditBox;
                if (box.Multiline)
                {
                    return;
                }
                else
                    SendKeys.Send("{TAB}");
            }
            else if (activeCtrl.GetType().Equals(typeof(TextBox)))
            {
                TextBox box = activeCtrl as TextBox;
                if (box.Multiline)
                {
                    return;
                }
                else
                    SendKeys.Send("{TAB}");
            }
            else
                SendKeys.Send("{TAB}");
        }
        void chkKhac_CheckedChanged(object sender, EventArgs e)
        {
            txtkhac.Enabled = chkKhac.Checked;
            txtkhac.Focus();
        }

        void chkThuocLao_CheckedChanged(object sender, EventArgs e)
        {
            txtThuocLao.Enabled = chkThuocLao.Checked;
            txtThuocLao.Focus();
        }

        void chkThuocLa_CheckedChanged(object sender, EventArgs e)
        {
            txtThuocLa.Enabled = chkThuocLa.Checked;
            txtThuocLa.Focus();
        }

        void chkRuouBia_CheckedChanged(object sender, EventArgs e)
        {
            txtRuouBia.Enabled = chkRuouBia.Checked;
            txtRuouBia.Focus();
        }

        void chkMaTuy_CheckedChanged(object sender, EventArgs e)
        {
            txtMaTuy.Enabled = chkMaTuy.Checked;
            txtMaTuy.Focus();
        }

        void chkDiUng_CheckedChanged(object sender, EventArgs e)
        {
            txtDiUng.Enabled = chkDiUng.Checked;
            txtDiUng.Focus();
        }
        public void InitData(KcbLuotkham objLuotkham)
        {
            this.objLuotkham = objLuotkham;
            objtsb = new Select().From(EmrTiensubenhDacdiemlienquan.Schema)
               .Where(EmrTiensubenhDacdiemlienquan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
               .And(EmrTiensubenhDacdiemlienquan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
               .ExecuteSingle<EmrTiensubenhDacdiemlienquan>();
            FillData();
        }
        private void FillData()
        {
            try
            {
                if (objtsb != null)
                {
                    //Thông tin dị ứng
                    chkDiUng.Checked = Utility.Int16Dbnull(objtsb.TsbDiung) == 1;
                    chkMaTuy.Checked = Utility.Int16Dbnull(objtsb.TsbMatuy) == 1;
                    chkRuouBia.Checked = Utility.Int16Dbnull(objtsb.TsbRuoubia) == 1;
                    chkThuocLa.Checked = Utility.Int16Dbnull(objtsb.TsbThuocla) == 1;
                    chkThuocLao.Checked = Utility.Int16Dbnull(objtsb.TsbThuoclao) == 1;
                    chkKhac.Checked = Utility.Int16Dbnull(objtsb.TsbKhac) == 1;
                    txtDiUng.Text = Utility.sDbnull(objtsb.TsbThoigianDiung);
                    txtMaTuy.Text = Utility.sDbnull(objtsb.TsbThoigianMatuy);
                    txtRuouBia.Text = Utility.sDbnull(objtsb.TsbThoigianRuoubia);
                    txtThuocLa.Text = Utility.sDbnull(objtsb.TsbThoigianThuocla);
                    txtThuocLao.Text = Utility.sDbnull(objtsb.TsbThoigianThuoclao);
                    txtkhac.Text = Utility.sDbnull(objtsb.TsbThoigianKhac);
                }
                else
                    ClearControls();
            }
            catch (Exception)
            {


            }
            finally
            {

            }
        }
        public bool SaveData()
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                        objtsb = new Select().From(EmrTiensubenhDacdiemlienquan.Schema)
               .Where(EmrTiensubenhDacdiemlienquan.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
               .And(EmrTiensubenhDacdiemlienquan.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
               .ExecuteSingle<EmrTiensubenhDacdiemlienquan>();
                        if (objtsb != null && objtsb.IdTsb > 0)
                        {
                            objtsb.MarkOld();
                            objtsb.NguoiSua = globalVariables.UserName;
                            objtsb.NgaySua = THU_VIEN_CHUNG.GetSysDateTime();
                        }
                        else
                        {
                            objtsb = new EmrTiensubenhDacdiemlienquan();
                            objtsb.IsNew = true;
                            objtsb.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham);
                            objtsb.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan);
                            objtsb.NguoiTao = globalVariables.UserName;
                            objtsb.NgayTao = THU_VIEN_CHUNG.GetSysDateTime();
                        }
                        objtsb.TsbDiung = chkDiUng.Checked;
                        objtsb.TsbMatuy = chkMaTuy.Checked;
                        objtsb.TsbRuoubia = chkRuouBia.Checked;
                        objtsb.TsbThuocla = chkThuocLa.Checked;
                        objtsb.TsbThuoclao = chkThuocLao.Checked;
                        objtsb.TsbKhac = chkKhac.Checked;
                        if (chkDiUng.Checked) objtsb.TsbThoigianDiung = txtDiUng.Text;
                        else objtsb.TsbThoigianDiung = "";
                        if (chkMaTuy.Checked) objtsb.TsbThoigianMatuy = txtMaTuy.Text;
                        else objtsb.TsbThoigianMatuy = "";
                        if (chkRuouBia.Checked) objtsb.TsbThoigianRuoubia = txtRuouBia.Text;
                        else objtsb.TsbThoigianRuoubia = "";
                        if (chkThuocLa.Checked) objtsb.TsbThoigianThuocla = txtThuocLa.Text;
                        else objtsb.TsbThoigianThuocla = "";
                        if (chkThuocLao.Checked) objtsb.TsbThoigianThuoclao = txtThuocLao.Text;
                        else objtsb.TsbThoigianThuoclao = "";
                        if (chkKhac.Checked) objtsb.TsbThoigianKhac = txtkhac.Text;
                        else objtsb.TsbThoigianKhac = "";
                        objtsb.Save();
                    }
                    scope.Complete();
                }
                return true;
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
                return false;
            }
        }
        void ClearControls()
        {
            chkDiUng.Checked = false;
            chkMaTuy.Checked = false;
            chkRuouBia.Checked = false;
            chkThuocLa.Checked = false;
            chkThuocLao.Checked = false;
            chkKhac.Checked = false;
            txtDiUng.Clear();
            txtMaTuy.Clear();
            txtRuouBia.Clear();
            txtThuocLa.Clear();
            txtThuocLao.Clear();
            txtkhac.Clear();


        }
    }
}
