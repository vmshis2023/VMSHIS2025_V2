using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;
using SubSonic;
using System.Data.OleDb;
using System.Transactions;
using Janus.Windows.GridEX;
using System.IO;
using System.Threading;
using VMS.HIS.DAL;
using VNS.Libs.AppType;


namespace VNS.HIS.UI.HinhAnh
{
    public partial class frm_themmoi_vungkhaosat : Form
    {
        #region "khai báo biêns "
        int IDDuplicate = -1;
        public action m_enAct  = action.Insert;
        public DmucVungkhaosat Obj;
        public DataTable Table;
        string args = "ALL";
        #endregion

        public frm_themmoi_vungkhaosat(string args)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            txtMoTa.Url = new Uri(Application.StartupPath.ToString() + @"\editor\ckeditor.html");
            Application.DoEvents();
            this.args = args;
            KeyDown += frm_themmoi_vungkhaosat_KeyDown;
            Utility.GetResizeForm(this);
        }

    

        /// <summary>
        /// hàm thực hiện việc thoát form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// hàm thực hiện việc phím tắt của đơn vị tính
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_themmoi_vungkhaosat_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S) cmdSave.PerformClick();
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.Enter)
            {
                if (this.ActiveControl.Name == txtMoTa.Name)
                    return;
                else
                    ProcessTabKey(true);
            }
        }

        /// <summary>
        /// hàm thực hiện việc load thông tin của form hiện tại lên
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_themmoi_vungkhaosat_Load(object sender, EventArgs e)
        {
            DataTable dtLoaiDV = null;
            //SysSystemParameter _objsys = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("ID_LOAIDVU_CDHA").ExecuteSingle<SysSystemParameter>();
            if (globalVariables.IsAdmin) args = "ALL";
            if (args != "ALL")
                    dtLoaiDV = new Select().From(DmucDichvucl.Schema).Where(DmucDichvucl.Columns.MaDichvu).In(args.Split(',').ToList<string>()).ExecuteDataSet().Tables[0];
            else
                    dtLoaiDV = new Select().From(DmucDichvucl.Schema).ExecuteDataSet().Tables[0];
            DataBinding.BindDataCombobox(cboLoaiDvu, dtLoaiDV, DmucDichvucl.Columns.MaDichvu, DmucDichvucl.Columns.TenDichvu, "---Chọn---", true); 
            BindData();
            txtMa.Focus();
        }
        private void BindData()
        {
            try
            {
                if (m_enAct == action.Update || m_enAct == action.Duplicate)
                {
                    txtIdVungKs.Text = Obj.Id.ToString();
                    txtTenVungKs.Text = Obj.TenVungkhaosat;
                    txtMa.Text = Obj.MaKhaosat;
                    txtKet_Luan.Text = Obj.KetLuan;
                    txtDenghi.Text = Obj.DeNghi;
                    txtFileMau.Text = Obj.TenfileKq;
                    txtKichthuocanh.Text = Obj.Kichthuocanh;
                    cboLoaiDvu.SelectedIndex = Utility.GetSelectedIndex(cboLoaiDvu,Utility.sDbnull( Obj.MaLoaidvu));
                    txtNoidung.Text = Obj != null ? Obj.MotaHtml : "";
                    txtMoTa.Document.InvokeScript("setValue", new string[] { txtNoidung.Text });
                    if (m_enAct == action.Duplicate)
                    {
                        IDDuplicate = Obj.Id;
                        txtIdVungKs.Text = "-1";
                        Obj = null;
                    }
                }
                else
                {
                    txtIdVungKs.Clear();
                    txtTenVungKs.Clear();
                    txtMoTa.ResetText();
                    txtKet_Luan.Clear();
                    txtDenghi.Clear();
                    txtNoidung.Clear();
                    txtKichthuocanh.Clear();

                }

            }
            catch (Exception)
            {


            }
            finally
            {
                //LoadHTML();
                timer1.Start();
                
            }
        }
        void LoadHTML()
        {
            string noidung = txtNoidung.Text;
            txtMoTa.Document.InvokeScript("setValue", new string[] { noidung });
        }
       
        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (!IsValidData()) return;
            PerformAction();
        }

        private bool IsValidData()
        {
            if (string.IsNullOrEmpty(txtMa.Text))
            {
                Utility.ShowMsg("Bạn phải nhập mã vùng khảo sát", "Thông báo", MessageBoxIcon.Warning);
                txtMa.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtTenVungKs.Text))
            {
                Utility.ShowMsg("Bạn phải nhập tên vùng khảo sát", "Thông báo", MessageBoxIcon.Warning);
                txtTenVungKs.Focus();
                return false;
            }

            //Kiểm tra nếu đã tồn tại vị trí và thiết bị
            //Nếu là Insert
            if (m_enAct == action.Insert || m_enAct == action.Duplicate)
            {
                int recordCount =
                   new Select().From(DmucVungkhaosat.Schema.Name).Where(DmucVungkhaosat.MaKhaosatColumn).
                       IsEqualTo(Utility.DoTrim( txtMa.Text)).GetRecordCount();
                if (recordCount > 0)
                {
                    Utility.ShowMsg("Mã vùng khảo sát đã tồn tại. Đề nghị nhập mã khác", "Thông báo", MessageBoxIcon.Warning);
                    txtMa.SelectAll();
                    txtMa.Focus();
                    return false;
                }

                 recordCount =
                    new Select().From(DmucVungkhaosat.Schema.Name).Where(DmucVungkhaosat.TenVungkhaosatColumn).
                        IsEqualTo(Utility.DoTrim(txtTenVungKs.Text)).GetRecordCount();
                if (recordCount > 0)
                {
                    Utility.ShowMsg("Tên vùng khảo sát đã tồn tại. Đề nghị nhập tên khác", "Thông báo", MessageBoxIcon.Warning);
                    txtTenVungKs.SelectAll();
                    txtTenVungKs.Focus();
                    return false;
                }
            }

            if (m_enAct.Equals(action.Update))
            {
                int recordCount =
                   new Select().From(DmucVungkhaosat.Schema.Name).Where(DmucVungkhaosat.MaKhaosatColumn).
                       IsEqualTo(Utility.DoTrim(txtMa.Text)).And(DmucVungkhaosat.IdColumn).IsNotEqualTo(Obj.Id)
                       .GetRecordCount();
                if (recordCount > 0)
                {
                    Utility.ShowMsg(string.Format("Mã vùng khảo sát {0} đã tồn tại. Đề nghị nhập mã khác", txtMa.Text), "Thông báo", MessageBoxIcon.Warning);
                    txtMa.SelectAll();
                    txtMa.Focus();
                    return false;
                }

                 recordCount =
                    new Select().From(DmucVungkhaosat.Schema.Name).Where(DmucVungkhaosat.TenVungkhaosatColumn).
                        IsEqualTo(Obj.TenVungkhaosat).And(DmucVungkhaosat.IdColumn).IsNotEqualTo(Obj.Id)
                        .GetRecordCount();
                if (recordCount > 0)
                {
                    Utility.ShowMsg("Tên vùng khảo sát đã tồn tại. Đề nghị nhập tên khác", "Thông báo", MessageBoxIcon.Warning);
                    txtTenVungKs.SelectAll();
                    txtTenVungKs.Focus();
                    return false;
                }
            }
            return true;
        }

        private void PerformAction()
        {
            try
            {

                if (Obj == null) Obj = new DmucVungkhaosat();
                Obj.MaKhaosat = Utility.DoTrim(txtMa.Text);
                Obj.TenVungkhaosat = Utility.DoTrim(txtTenVungKs.Text);
                Obj.MaLoaidvu = Utility.sDbnull(cboLoaiDvu.SelectedValue, "-1");
                Obj.Mota = txtMoTa.Document.InvokeScript("getData").ToString();
                Obj.MotaHtml = txtMoTa.Document.InvokeScript("getValue").ToString();
                Obj.KetLuan = txtKet_Luan.Text;
                Obj.DeNghi = txtDenghi.Text;
                Obj.Kichthuocanh = Utility.DoTrim( txtKichthuocanh.Text);
                Obj.TenfileKq = Utility.DoTrim(txtFileMau.Text);
                Obj.TrangThai = chkTrangthai.Checked;
                Obj.IsNew = m_enAct == action.Insert || m_enAct == action.Duplicate;
                if (m_enAct == action.Update)
                    Obj.MarkOld();
                Obj.Save();
                if (m_enAct == action.Duplicate)//Duplicate dynamic fields
                {
                    string Sql = "Insert into DynamicFields([Ma],[mota],[stt],[id_vungks],[Rtxt],[topLabel],[multiline],[W],[H],[lblW],[AllowEmpty],[Bold]) SELECT [Ma],[mota],[stt]," + Obj.Id.ToString() + ",[Rtxt],[topLabel],[multiline],[W],[H],[lblW],[AllowEmpty],[Bold] FROM [dbo].[DynamicFields] where [id_vungks]=" + IDDuplicate.ToString();
                    new InlineQuery().Execute(Sql);
                }
                DataRow dr = m_enAct == action.Insert || m_enAct == action.Duplicate
                                 ? Table.NewRow()
                                 : Table.Select(string.Format("{0}={1}", DmucVungkhaosat.Columns.Id, Obj.Id))[0];
                Utility.FromObjectToDatarow(Obj, ref dr);
                if (m_enAct == action.Insert || m_enAct == action.Duplicate)
                {
                    Table.Rows.Add(dr);
                    if (chkChoPhepNhapLienTuc.Checked)
                    {
                        Obj = new DmucVungkhaosat();
                        Obj.IsNew = true;
                        txtMa.Clear();
                        txtTenVungKs.Clear();
                        txtMoTa.ResetText();
                        txtKet_Luan.Clear();
                        txtDenghi.Clear();
                        txtFileMau.Clear();
                        txtTenVungKs.SelectAll();
                        BindData();
                    }
                    else
                    {
                        this.Close();
                    }

                }
                else this.Close();
            }
            catch
            {
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtNoidung.Text)) ReloadHTML();
        }
        private void ReloadHTML()
        {
            txtMoTa.Document.InvokeScript("setValue", new string[] { txtNoidung.Text });
            Application.DoEvents();
            Thread.Sleep(100);
            if (txtMoTa.Document.InvokeScript("getData").ToString().Length > 0)
                timer1.Stop();
        }

        private void lnkMore_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
       

    }
}