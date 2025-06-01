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

namespace VMS.EMR.PHIEUKHAM
{
    public partial class frm_ThemtiensuSankhoa : Form
    {
        public EmrTiensuSankhoa tssk;
        public DataTable dt_tssk;
        KcbLuotkham objLuotkham;
        public frm_ThemtiensuSankhoa(KcbLuotkham objLuotkham, EmrTiensuSankhoa tssk)
        {
            InitializeComponent();
            this.objLuotkham = objLuotkham;
            this.tssk = tssk;
            this.Shown += Frm_TiensuSanphukhoa_Shown;
            this.KeyDown += frm_ThemtiensuSankhoa_KeyDown;
        }

        private void frm_ThemtiensuSankhoa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                ProcessTabKey(true);
        }

        private void Frm_TiensuSanphukhoa_Shown(object sender, EventArgs e)
        {
            DisplayData();
        }
        void DisplayData()
        {
            if (tssk != null)
            {
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
                dtpNam.Focus();
        }
        private void Frm_TiensuSanphukhoa_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveUserConfigs();
        }

        void LoadUserConfigs()
        {
            try
            {
                

            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
        void SaveUserConfigs()
        {
            try
            {
            

            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
        }
       

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            try
            {
                bool isUpdate = false;
                using (var scope = new TransactionScope())
                {
                    using (var dbscope = new SharedDbConnectionScope())
                    {

                        if (tssk == null || tssk.Id <= 0)
                        {
                            tssk = new EmrTiensuSankhoa();
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
                        isUpdate = tssk.Id <= 0;
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
                        if (!isUpdate)
                        {
                            if (dt_tssk != null && dt_tssk.Columns.Count > 0)
                            {
                                DataRow newdr = dt_tssk.NewRow();
                                Utility.FromObjectToDatarow(tssk, ref newdr);
                                dt_tssk.Rows.Add(newdr);
                            }
                        }
                        else
                        {
                            if (dt_tssk != null && dt_tssk.Columns.Count > 0)
                            {
                                (from p in dt_tssk.AsEnumerable()
                                 where tssk.Id == Utility.Int64Dbnull(p["id"], "-1")
                                 select p).ToList()
                             .ForEach(x =>
                             {
                                 x[EmrTiensuSankhoa.NamColumn.ColumnName] = tssk.Nam;
                                 x[EmrTiensuSankhoa.DeduthangColumn.ColumnName] = tssk.Deduthang;
                                 x[EmrTiensuSankhoa.DethieuthangColumn.ColumnName] = tssk.Dethieuthang;
                                 x[EmrTiensuSankhoa.SayColumn.ColumnName] = tssk.Say;
                                 x[EmrTiensuSankhoa.HutColumn.ColumnName] = tssk.Hut;
                                 x[EmrTiensuSankhoa.NaoColumn.ColumnName] = tssk.Nao;
                                 x[EmrTiensuSankhoa.CovacColumn.ColumnName] = tssk.Covac;
                                 x[EmrTiensuSankhoa.ChuangoaitucungColumn.ColumnName] = tssk.Chuangoaitucung;
                                 x[EmrTiensuSankhoa.ThaichetluuColumn.ColumnName] = tssk.Thaichetluu;
                                 x[EmrTiensuSankhoa.ConhiensongColumn.ColumnName] = tssk.Conhiensong;
                                 x[EmrTiensuSankhoa.TaibienHausanColumn.ColumnName] = tssk.TaibienHausan;
                                 x[EmrTiensuSankhoa.NoiketthucthainghenColumn.ColumnName] = tssk.Noiketthucthainghen;
                                 x[EmrTiensuSankhoa.TuoithaiColumn.ColumnName] = tssk.Tuoithai;
                                 x[EmrTiensuSankhoa.DienbienthaiColumn.ColumnName] = tssk.Dienbienthai;
                                 x[EmrTiensuSankhoa.PhuongphapdeColumn.ColumnName] = tssk.Phuongphapde;
                                 x[EmrTiensuSankhoa.ThongtintreCannangBenhtatColumn.ColumnName] = tssk.ThongtintreCannangBenhtat;
                             }
                             );
                            }
                        }
                    }
                    scope.Complete();
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (System.Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void frm_ThemtiensuSankhoa_Load(object sender, EventArgs e)
        {
            dtpNam.Focus();
        }
    }
}
