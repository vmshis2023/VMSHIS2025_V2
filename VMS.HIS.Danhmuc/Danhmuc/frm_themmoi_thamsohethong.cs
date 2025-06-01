using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using VMS.HIS.DAL;
using VNS.Libs;
using VNS.HIS.UI.DANHMUC;

namespace VMS.HIS.Danhmuc.UI
{
    public partial class frm_themmoi_thamsohethong : Form
    {
        public SysSystemParameter objSys;
        public string _sValue;
        public DataTable p_dtThamsohethong = new DataTable();
        public action em_Action = action.Insert;
        public Janus.Windows.GridEX.GridEX grd;
        public frm_themmoi_thamsohethong()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            txtnhomthamso._OnShowDataV1 += txtnhomthamso__OnShowDataV1;
           
        }

        void txtnhomthamso__OnShowDataV1(VNS.HIS.UCs.AutoCompleteTextbox_Danhmucchung obj)
        {
            DMUC_DCHUNG dmucDchung = new DMUC_DCHUNG(obj.LOAI_DANHMUC);
            dmucDchung.ShowDialog();
            if (!dmucDchung.m_blnCancel)
            {
                string oldCode = obj.myCode;
                obj.Init();
                obj.SetCode(oldCode);
                obj.Focus();
            } 
        }
       
        public bool Kiemtra()
        {
            if (string.IsNullOrEmpty(txtTenThamSo.Text))
            {
                Utility.ShowMsg("Tên tham số không được bỏ trống.", "Thông báo", MessageBoxIcon.Warning);
                txtGiaTri.Focus();
                return false;
            }

            if (string.IsNullOrEmpty(txtGiaTri.Text))
            {
                Utility.ShowMsg("Giá trị tham số không được bỏ trống.", "Thông báo", MessageBoxIcon.Warning);
                txtGiaTri.Focus();
                return false;
            }
            return true;
        }
        private SysSystemParameter CreateSystemParameter()
        {
            SysSystemParameter sysparameter = new SysSystemParameter();

            if (em_Action == action.Update)
            {
                sysparameter.IsLoaded = true;
                sysparameter.MarkOld();
            }

            sysparameter.SName = Utility.sDbnull(txtTenThamSo.Text);
            sysparameter.SValue = Utility.sDbnull(txtGiaTri.Text);
            sysparameter.FpSBranchID = Utility.sDbnull(txtSBranchID.Text);
            sysparameter.IMonth = Utility.Int16Dbnull(txtIMonth.Text);
            sysparameter.IYear = Utility.Int16Dbnull(txtIYear.Text);
            sysparameter.SDataType = Utility.sDbnull(txtnhomthamso.myCode);
            if (chkTrangThai.Checked) sysparameter.IStatus = 1;
            else sysparameter.IStatus = 0;
            sysparameter.SDesc = Utility.sDbnull(txtDienGiai.Text);
            sysparameter.FpSBranchID = "HIS";

            return sysparameter;

        }

        private void UpdateData()
        {
            try
            {
                SysSystemParameter objsys = new SysSystemParameter();
                
                if (em_Action == action.Update)
                {
                    objsys.MarkOld();
                    objsys.IsLoaded = true;
                    objsys.Id = Utility.Int64Dbnull(txtID.Text);
                }

                objsys.SName = Utility.sDbnull(txtTenThamSo.Text);
                objsys.SValue = Utility.sDbnull(txtGiaTri.Text);
                objSys.FpSBranchID = Utility.sDbnull(txtSBranchID.Text);
                objSys.IMonth = Utility.Int16Dbnull(txtIMonth.Text);
                objSys.IYear = Utility.Int16Dbnull(txtIYear.Text);
                objsys.SDataType = Utility.sDbnull(txtnhomthamso.myCode);
                if (chkTrangThai.Checked) objsys.IStatus = 1;
                else objsys.IStatus = 0;
                objsys.SDesc = Utility.sDbnull(txtDienGiai.Text);
                objsys.FpSBranchID = "HIS";
                objsys.Save();

                if (objsys != null)
                {
                    
                    Utility.GonewRowJanus(grd, SysSystemParameter.Columns.SName, Utility.sDbnull(txtTenThamSo.Text));
                    
                }

                lblMessage.Visible = true;
                lblMessage.Text = @"Bạn thực hiện sửa thông tin thành công!";
                objSys = SysSystemParameter.FetchByID(Utility.sDbnull(txtID.Text));
                if (objSys != null)
                {
                    Utility.Log(this.Name, globalVariables.UserName, string.Format("Thay đổi giá trị của tham số hệ thống: {0}; Giá trị cũ: {1}; Giá trị mới: {2}"
                      , objSys.SName, _sValue, txtGiaTri.Text), newaction.Update, "UI");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void InsertData()
        {
            try
            {

                SqlQuery Ma = new Select(SysSystemParameter.Columns.Id).From<SysSystemParameter>()
                        .Where(SysSystemParameter.Columns.SName)
                        .IsEqualTo(txtTenThamSo.Text);
                int id = Ma.ExecuteScalar<Int32>();


                if (id > 0)
                {
                    Utility.ShowMsg("Tham số đã được tạo trong cơ sở dữ liệu bạn không thể thêm.", "Thông báo", MessageBoxIcon.Warning);
                    txtTenThamSo.Focus();
                    //return false;
                }
                else {
                    SysSystemParameter objWeb = CreateSystemParameter();
                    objWeb.IsNew = true;
                    objWeb.Save();
                    txtID.Text = Utility.sDbnull(objWeb.Id);
                    string ten = Utility.sDbnull(objWeb.SName);
                    objWeb = SysSystemParameter.FetchByID(Utility.Int64Dbnull(txtID.Text));
                    if (objWeb != null)
                    {
                        DataRow dataRow = p_dtThamsohethong.NewRow();
                        Utility.FromObjectToDatarow(objWeb, ref dataRow);
                        p_dtThamsohethong.Rows.Add(dataRow);
                        Utility.GonewRowJanus(grd, SysSystemParameter.Columns.Id, Utility.sDbnull(txtID.Text));
                    }
                    lblMessage.Visible = true;
                    lblMessage.Text = "Bạn thực hiện thêm mới tham số thành công!";
                }

                

            }
            catch (Exception)
            {
                throw;
            }

        }
        private void GetData()
        {
            try
            {
                objSys = SysSystemParameter.FetchByID(Utility.Int64Dbnull(txtID.Text));
                if (objSys != null)
                {
                    txtTenThamSo.Text = Utility.sDbnull(objSys.SName);
                    txtGiaTri.Text = Utility.sDbnull(objSys.SValue);
                    txtSBranchID.Text = Utility.sDbnull(objSys.FpSBranchID);
                    txtIMonth.Text = Utility.sDbnull(objSys.IMonth);
                    txtIYear.Text = Utility.sDbnull(objSys.IYear);
                    txtnhomthamso.SetCode(Utility.sDbnull(objSys.SDataType));
                    chkTrangThai.Checked = Utility.Byte2Bool(objSys.IStatus);
                    txtDienGiai.Text = Utility.sDbnull(objSys.SDesc);

                }
                else
                {
                    txtTenThamSo.Clear();
                    txtGiaTri.Clear();
                    txtDienGiai.Clear();
                    txtnhomthamso.setDefaultValue();
                    chkTrangThai.Checked = false;
                }
            }
            catch (Exception ex) {
                Utility.ShowMsg(ex.Message);
            }
        }
        public bool b_Cancel = false;
        private void PerformAction()
        {
            switch (em_Action)
            {
                case action.Insert:
                    InsertData();
                    break;
                case action.Update:
                    UpdateData();
                    break;
            }
            b_Cancel = true;

        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Kiemtra()) return;
                PerformAction();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void frm_themmoi_thamsohethong_Load(object sender, EventArgs e)
        {
            txtnhomthamso.Init();
            lblMessage.Visible = false;
            GetData();
            chkTrangThai.Checked = true;
        }
    }
}
