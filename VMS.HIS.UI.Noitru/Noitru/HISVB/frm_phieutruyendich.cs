using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using VMS.HIS.DAL;
using VNS.Libs;
using SubSonic;
namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_phieutruyendich : Form
    {
        #region "khởi tạo thông tin"
        private  DataTable m_dtDataPhieuDichTruyen=new DataTable();
        public frm_phieutruyendich()
        {
            InitializeComponent();
            txtPatientCode.TextChanged+=new EventHandler(txtPatientCode_TextChanged);
        }
        #endregion
        private void grb1_Enter(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// hàm thực hiện thoát Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// hàm thực hieenj load thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_phieutruyendich_Load(object sender, EventArgs e)
        {
           // InitalData();
            ModifyCommand();
        }
        private void ModifyCommand()
        {
            try
            {
                cmdUpdate.Enabled = grdList.RowCount > 0 && grdList.CurrentRow.RowType == RowType.Record;
                cmdDelete.Enabled = grdList.RowCount > 0 && grdList.CurrentRow.RowType == RowType.Record;
                cmdPrint.Enabled = grdList.RowCount > 0 && grdList.CurrentRow.RowType == RowType.Record;
            }
            catch (Exception)
            {
                
                //throw;
            }
        }
        private void GetData()
        {
            m_dtDataPhieuDichTruyen =
                SPs.DetmayLoadDataPhieuDichtruyen(Utility.Int32Dbnull(txtPatientID.Text, -1),
                                                  Utility.sDbnull(txtPatientCode.Text, ""), globalVariables.DepartmentID)
                    .GetDataSet().Tables[0];

            grdList.DataSource = m_dtDataPhieuDichTruyen;
            ModifyCommand();
        }

        private short ObjectType_ID = -1;
        /// <summary>
        /// hàm thực hiện việc thay đổi thông tin của khi load tên lên
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPatientCode_TextChanged(object sender, EventArgs e)
        {
            //LẤY VỀ MÃ BỆNH NHÂN 
            string Patient_Code = txtPatientCode.Text.Trim();
            bool v_blnFound = false;
            long Patient_ID;
            TPatientInfoCollection arrPatientInfo = null;
            TPatientExamCollection arrPatientExam =
                new TPatientExamController().FetchByQuery(
                    TPatientExam.CreateQuery().WHERE(TPatientExam.PatientCodeColumn.ColumnName, Comparison.Equals,
                                                     Patient_Code));
            if (arrPatientExam.Count > 0)
            {
                Patient_ID = arrPatientExam[0].PatientId;
                ObjectType_ID = arrPatientExam[0].ObjectTypeId;
                txtInsuranceNum.Text = arrPatientExam[0].InsuranceNum;
                LDepartment department = LDepartment.FetchByID(arrPatientExam[0].DepartmentId);
                if (department != null)
                {
                    txtDepartment_Name.Text = department.DepartmentName.Trim();
                    txtDepartment_ID.Text = department.DepartmentId.ToString().Trim();
                    TPatientDeptCollection patientDeptCollection =
                        new TPatientDeptController().FetchByQuery(
                            TPatientDept.CreateQuery().AddWhere(TPatientDept.Columns.DepartmentId, Comparison.Equals,
                                                                txtDepartment_ID.Text).AND(TPatientDept.Columns.Status,
                                                                                           Comparison.Equals, 0).AND(
                                                                                               TPatientDept.Columns.
                                                                                                   PatientCode,
                                                                                               Comparison.Equals,
                                                                                               arrPatientExam[0].
                                                                                                   PatientCode));
                    if (patientDeptCollection.Count > 0)
                    {
                        LRoom objRoom = LRoom.FetchByID(patientDeptCollection[0].RoomId);
                        if (objRoom != null)
                        {
                            txtRoom_Name.Text = objRoom.RoomName.Trim();
                            LBed objBed = LBed.FetchByID(patientDeptCollection[0].BedId);
                            if (objBed != null)
                            {
                                txtBed_Name.Text = objBed.BedName.Trim();
                            }
                        }
                      
                    }
                }
                LObjectTypeCollection arrObjectType =
                    new LObjectTypeController().FetchByQuery(
                        LObjectType.CreateQuery().WHERE(LObjectType.ObjectTypeIdColumn.ColumnName, Comparison.Equals,
                                                        ObjectType_ID));
                if (arrObjectType.Count > 0)
                {
                    txtObjectName.Text = arrObjectType[0].ObjectTypeName.Trim();
                    txtObjectType_ID.Text = arrObjectType[0].ObjectTypeId.ToString();
                }
                else
                {
                    txtObjectName.Text = "Chưa thuộc đối tượng BHYT nào.";
                    ObjectType_ID = -1;
                    txtObjectType_ID.Text = "-1";
                }
                v_blnFound = true;
            }
            else
            {
                ObjectType_ID = -1;
                Patient_ID = -1;
                v_blnFound = false;
            }
            if (v_blnFound)
                arrPatientInfo = new TPatientInfoController().FetchByID(Patient_ID);
            TPatientInfo v_objPatient = null;
            if (arrPatientInfo != null && arrPatientInfo.Count > 0)
            {
                v_objPatient = arrPatientInfo[0];
                v_blnFound = true;
            }
            else
            {
                v_objPatient = null;
                v_blnFound = false;
            }
            if (v_objPatient != null)
            {
                txtPatientID.Text = v_objPatient.PatientId.ToString().Trim();
                txtPatientName.Text = v_objPatient.PatientName;
                txtAddress.Text = v_objPatient.PatientAddr;
                txtSex.Text = v_objPatient.PatientSex == 0 ? "Nam" : "Nữ";
                txtYearBirth.Text = v_objPatient.YearOfBirth.ToString();
                txtIdentifyNum.Text = v_objPatient.IdentifyNum;
                if (v_objPatient.PatientBirth != null)
                    txtYearBirth.Text = v_objPatient.PatientBirth.Value.Year.ToString();
                // InitalData();

               
                GetData();

            }
            else
            {
                txtPatientID.ResetText();
                txtPatientName.ResetText();
                txtAddress.ResetText();
                txtSex.ResetText();
                txtObjectName.ResetText();
                txtInsuranceNum.ResetText();
                txtIdentifyNum.ResetText();
                txtYearBirth.ResetText();
                txtObjectType_ID.ResetText();
                txtRoom_Name.ResetText();
                txtDepartment_Name.ResetText();
                txtDepartment_ID.ResetText();
                txtBed_Name.ResetText();


            }

        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if(grdList.GetCheckedRows().Length<=0)
            {
                Utility.ShowMsg("Bạn phải chọn một bản ghi thực hiện xóa thông tin");
                grdList.Focus();
                return;
            }
            if(Utility.AcceptQuestion("Bạn có muốn thực hiện xóa thông tin đang chọn không","Thông báo",true))
            {
                foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdList.GetCheckedRows())
                {
                    long Id_Phieu = Utility.Int64Dbnull(gridExRow.Cells["Phieu_TD_ID"], -1);
                   if( new Delete().From(TPhieuDichTruyen.Schema)
                        .Where(TPhieuDichTruyen.Columns.PhieuTdId).IsEqualTo(Id_Phieu).Execute()>0)
                   {
                       gridExRow.Delete();
                       grdList.UpdateData();
                       grdList.Refresh();
                   }
                }
            }
            m_dtDataPhieuDichTruyen.AcceptChanges();
            ModifyCommand();
        }
        /// <summary>
        /// hàm thực hiện thêm mới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdCreateNew_Click(object sender, EventArgs e)
        {
            Form_DETMAY.frm_DETMAY_Add_PHIEU_DICHTRUYEN frm=new frm_DETMAY_Add_PHIEU_DICHTRUYEN();
            frm.em_Action = action.Insert;
            frm.p_DataPhieuDich = m_dtDataPhieuDichTruyen;
            frm.grdList = grdList;
            frm.ShowDialog();
           
        }
        /// <summary>
        /// hàm thực hiện việc update thông tin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            if(grdList.CurrentRow!=null)
            {
           Form_DETMAY.frm_DETMAY_Add_PHIEU_DICHTRUYEN frm = new frm_DETMAY_Add_PHIEU_DICHTRUYEN();
            frm.em_Action = action.Update;
            frm.p_DataPhieuDich = m_dtDataPhieuDichTruyen;
            frm.grdList = grdList;
            frm.txtID.Text = Utility.sDbnull(grdList.GetValue("Phieu_TD_ID"), -1);
            frm.ShowDialog();
            }
           
           
        }

        private void cmdGetPatient_Click(object sender, EventArgs e)
        {

        }
    }
}
