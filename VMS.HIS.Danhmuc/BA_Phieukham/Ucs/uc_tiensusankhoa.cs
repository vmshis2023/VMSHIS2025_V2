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
using VNS.HIS.UI.DANHMUC;
using VNS.HIS.UCs;
using Janus.Windows.GridEX.EditControls;
using VMS.HIS.Danhmuc.Dungchung;
using System;
using System.Transactions;

namespace VMS.EMR.PHIEUKHAM.Ucs
{
    public partial class uc_tiensusankhoa : UserControl
    {
        EmrTiensuSankhoa tssk;
        DataTable dt_tssk;
        KcbLuotkham objLuotkham;
        public int id_bacsikham = -1;
        bool isAllowSelectionChanged = false;
        action _act = action.Insert;
        public uc_tiensusankhoa()
        {
            InitializeComponent();
            grdTiensuSankhoa.SelectionChanged += GrdTiensuSankhoa_SelectionChanged;
            grdTiensuSankhoa.MouseDoubleClick += GrdTiensuSankhoa_MouseDoubleClick;
            grdTiensuSankhoa.ColumnButtonClick += GrdTiensuSankhoa_ColumnButtonClick;
        }

        private void GrdTiensuSankhoa_ColumnButtonClick(object sender, ColumnActionEventArgs e)
        {
           if(e.Column.Key=="")
            {
                int num=new Delete().From(EmrTiensuSankhoa.Schema).Where(EmrTiensuSankhoa.Columns.Id).IsEqualTo(Utility.Int32Dbnull(grdTiensuSankhoa.GetValue("id"))).Execute();
                if(num>0)
                {
                    grdTiensuSankhoa.CurrentRow.Delete();
                }    
            }    
        }

        private void GrdTiensuSankhoa_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!Utility.isValidGrid(grdTiensuSankhoa))
            {
                ClearControl();
                return;
            }
            else
            {
                tssk = EmrTiensuSankhoa.FetchByID(Utility.Int64Dbnull( grdTiensuSankhoa.GetValue("Id")));
                if(tssk!=null)
                {
                    _act = action.Insert;
                    cmdHuy.BringToFront();
                    cmdGhi.Enabled = true;
                    dtpNam.Text = tssk.Nam.ToString();
                    optDeduthang.Checked = Utility.Bool2Bool(tssk.Deduthang);
                    optDethieuthang.Checked = Utility.Bool2Bool(tssk.Dethieuthang);
                    optSay.Checked = Utility.Bool2Bool(tssk.Say);
                    optHut.Checked = Utility.Bool2Bool(tssk.Hut);
                    optNao.Checked = Utility.Bool2Bool(tssk.Nao);
                    chkCovac.Checked = Utility.Bool2Bool(tssk.Covac);

                    optChuangoaitucung.Checked = Utility.Bool2Bool(tssk.Chuangoaitucung);
                    optThaichetluu.Checked = Utility.Bool2Bool(tssk.Thaichetluu);
                    optConhiensong.Checked = Utility.Bool2Bool(tssk.Conhiensong);
                    optTaibienhausan.Checked = Utility.Bool2Bool(tssk.TaibienHausan);
                    txtNoiketthucthainghen.Text = tssk.Noiketthucthainghen;
                    txtTuoithai.Text = tssk.Tuoithai;
                    txtDienbienthai.Text = tssk.Dienbienthai;
                    txtPhuongphapdeCachthucsinh.Text = tssk.Phuongphapde;
                    txtThongtintre.Text = tssk.ThongtintreCannangBenhtat;
                    txtNoiketthucthainghen.Focus();
                }   
                else
                {
                    ClearControl();
                    return;
                }    
            }    
                
        }

        private void GrdTiensuSankhoa_SelectionChanged(object sender, EventArgs e)
        {
           
        }

        public bool VisibleSaveButton
        {
            get { return cmdGhi.Visible; }
            set { cmdGhi.Visible = value; }
        }
        public void Init(KcbLuotkham objLuotkham, EmrTiensuSankhoa tssk)
        {

            this.objLuotkham = objLuotkham;
            this.tssk = tssk;
           
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
        public void Init(KcbLuotkham objLuotkham)
        {
            isAllowSelectionChanged = false;
            this.objLuotkham = objLuotkham;
            dt_tssk = new Select().From(EmrTiensuSankhoa.Schema)
                        .Where(EmrTiensuSankhoa.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                        .And(EmrTiensuSankhoa.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                        .ExecuteDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdTiensuSankhoa, dt_tssk, true, true, "1=1", "nam desc");
            isAllowSelectionChanged = true;
            GrdTiensuSankhoa_SelectionChanged(grdTiensuSankhoa, new EventArgs());

        }
       
       void ClearControl()
        {
            foreach (Control ctr in this.Controls)
                if (ctr.GetType().Equals(autoTxt.GetType()))
                    ((AutoCompleteTextbox_Danhmucchung)ctr).SetDefaultItem();
                else if (ctr is EditBox)
                {
                    ((EditBox)(ctr)).Clear();
                }
                else if (ctr is CheckBox)
                {
                    ((CheckBox)(ctr)).Checked = false;
                }
        }
        public bool Save()
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {
                       
                        if (tssk == null || tssk.Id <= 0)
                        {
                            tssk.IsNew = true;
                            tssk.NgayTao = DateTime.Now;
                            tssk.NguoiTao = globalVariables.UserName;
                        }
                        else
                        {
                            tssk.IsNew = false;
                            tssk.MarkOld();
                            tssk.NgaySua = DateTime.Now;
                            tssk.NguoiSua = globalVariables.UserName;
                        }
                        tssk.IdBenhnhan = objLuotkham.IdBenhnhan;
                        tssk.MaLuotkham = objLuotkham.MaLuotkham;
                        tssk.Nam = Utility.Int32Dbnull(dtpNam.Value.Year);
                        tssk.Deduthang = optDeduthang.Checked;
                        tssk.Dethieuthang = optDethieuthang.Checked;
                        tssk.Say = optSay.Checked;
                        tssk.Hut = optHut.Checked;
                        tssk.Nao = optNao.Checked;
                        tssk.Covac = chkCovac.Checked;
                        tssk.Chuangoaitucung = optChuangoaitucung.Checked;
                        tssk.Thaichetluu = optThaichetluu.Checked;
                        tssk.Conhiensong = optConhiensong.Checked;
                        tssk.TaibienHausan = optTaibienhausan.Checked;
                        tssk.Noiketthucthainghen = Utility.sDbnull(txtNoiketthucthainghen.Text);
                        tssk.Tuoithai = Utility.sDbnull(txtTuoithai.Text);
                        tssk.Dienbienthai = Utility.sDbnull(txtDienbienthai.Text);
                        tssk.Phuongphapde = Utility.sDbnull(txtPhuongphapdeCachthucsinh.Text);
                        tssk.ThongtintreCannangBenhtat = Utility.sDbnull(txtThongtintre.Text);
                        tssk.Save();
                        if (dt_tssk != null && dt_tssk.Columns.Count > 0)
                        {
                            DataRow newdr = dt_tssk.NewRow();
                            Utility.FromObjectToDatarow(tssk, ref newdr);
                            dt_tssk.Rows.Add(newdr);
                        }


                    }
                    scope.Complete();
                }
                return true;
            }
            catch (System.Exception ex)
            {
                Utility.CatchException(ex);
                return false;
            }
        }

      

        private void cmdThem_Click(object sender, EventArgs e)
        {
            tssk = new EmrTiensuSankhoa();
            _act = action.Insert;
            cmdGhi.Enabled = true;
            isAllowSelectionChanged = false;
            ClearControl();
            optDeduthang.Checked = true;
            optConhiensong.Checked = true;
            dtpNam.Focus();
            cmdHuy.BringToFront();
        }

        private void cmdGhi_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void cmdHuy_Click(object sender, EventArgs e)
        {
            cmdHuy.SendToBack();
            cmdGhi.Enabled = false;
            _act = action.Normal;
            GrdTiensuSankhoa_SelectionChanged(grdTiensuSankhoa, e);
        }
    }
}
