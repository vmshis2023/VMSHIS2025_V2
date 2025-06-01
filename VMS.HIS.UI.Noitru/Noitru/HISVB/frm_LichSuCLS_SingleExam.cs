using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using VietBaIT.CommonLibrary;
using VietBaIT.HISLink.DataAccessLayer;

namespace VietBaIT.HISLink.UI.ControlUtility.LichSuCLS
{
    public partial class frm_LichSuCLS_SingleExam : Form
    {
        public int _nPatientID = -1;
        public string _sPatientCode = string.Empty;

        public int _nServiceTypeID = -1;
        public string _sServiceCode = string.Empty;

        public string _sReturn = string.Empty;

        string _sPatient_Name = string.Empty;

        TPatientInfo _oPatientInfo = null;

        private DataTable m_dtKetQua;
        private DataSet ds;
        public frm_LichSuCLS_SingleExam()
        {
            InitializeComponent();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_LichSuCLS_SingleExam_Load(object sender, EventArgs e)
        {
            GetPatientInfo();
            LoadCLS();
            grpKetQuaXN.Visible = _nServiceTypeID == 1;
            grpKetQuaCDHA.Visible = _nServiceTypeID == 2;
        }
        void GetPatientInfo()
        {
            try
            {
                _oPatientInfo = TPatientInfo.FetchByID(Utility.Int32Dbnull(_nPatientID, -1));

                if (_oPatientInfo != null && Utility.Int32Dbnull(_oPatientInfo.PatientId, -1) > 0)
                {
                    _sPatient_Name = Utility.sDbnull(_oPatientInfo.PatientName, string.Empty);
                    txtPatientID.Text = Utility.Int32Dbnull(_oPatientInfo.PatientId, -1).ToString();
                    txtPatientCode.Text = _sPatientCode;
                    txtPatientName.Text = _sPatient_Name;
                    txtSex.Text = Utility.Int32Dbnull(_oPatientInfo.PatientSex, 2) == 0 ? "Nam" : Utility.Int32Dbnull(_oPatientInfo.PatientSex, 2) == 1 ? "Nữ" : "Khác";
                    txtYearBirth.Text = Utility.Int32Dbnull(_oPatientInfo.YearOfBirth).ToString();
                }
                else
                    Utility.ShowMsg("Không tìm thấy thông tin bệnh nhân", "Lỗi khi lấy thông tin bệnh nhân.", MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message, "Lỗi khi lấy thông tin bệnh nhân.", MessageBoxIcon.Error);
            }
        }
        private void LoadCLS()
        {
            try
            {
                int _nNoiNgoaitru = radTatCa.Checked ? -1 : radNgoaiTru.Checked ? 0 : 1;
                int _nDaCoKetQua = radTatCaKQ.Checked ? -1 : radChuaCoKetQua.Checked ? 0 : 1;

                DataTable m_dtAssignDetail = SPs.ClsGetChiDinhCanLamSangByPatient(_nPatientID, _sPatientCode, _nServiceTypeID, _sServiceCode, _nNoiNgoaitru, _nDaCoKetQua).GetDataSet().Tables[0];

                Utility.AddColumToDataTable(ref m_dtAssignDetail, "AssignDetailStatus_", typeof(int));
                Utility.SetDataSourceForDataGridEx(grdAssignDetail, m_dtAssignDetail, false, true, "", "");
                if (m_dtAssignDetail.Rows.Count > 0)
                {
                    grdAssignDetail_SelectionChanged(grdAssignDetail, new EventArgs());
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message, "Lỗi khi lấy danh sách chỉ định CLS.", MessageBoxIcon.Error);
            }
        }

        private void getKetQuaXN(bool _withCurrentRow)
        {
            try
            {
                string _sAssignDetail_ID = string.Empty;
                if (grdAssignDetail.GetCheckedRows().Count() > 0)
                {
                    var query = (from chk in grdAssignDetail.GetCheckedRows().AsEnumerable()
                                 let x = Utility.sDbnull(chk.Cells["AssignDetail_ID"].Value)
                                 select x).ToArray();
                    if (query.Count() > 0)
                    {
                        _sAssignDetail_ID = string.Join(",", query);
                    }
                }
                //if (_withCurrentRow && !string.Format(",{0}", _sAssignDetail_ID).Contains(string.Format(",{0}", grdAssignDetail.CurrentRow.Cells["AssignDetail_ID"].Value)))
                //{
                //    _sAssignDetail_ID += string.Format(",{0},", grdAssignDetail.CurrentRow.Cells["AssignDetail_ID"].Value);
                //}

                DataTable temdt = SPs.ClsKetQuaXetNghiemByAssignDetail(_sAssignDetail_ID).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdKetQuaXN, temdt, true, true, "", "");
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message, "Lỗi khi lấy Kết quả xét nghiệm.", MessageBoxIcon.Error);
            }
        }
        private void getKetQuaCDHA(bool _withCurrentRow)
        {
            try
            {
                string _sAssignDetail_ID = string.Empty;
                if (grdAssignDetail.GetCheckedRows().Count() > 0)
                {
                    var query = (from chk in grdAssignDetail.GetCheckedRows().AsEnumerable()
                                 let x = Utility.sDbnull(chk.Cells["AssignDetail_ID"].Value)
                                 select x).ToArray();
                    if (query.Count() > 0)
                    {
                        _sAssignDetail_ID = string.Join(",", query);
                    }
                }
                //if (_withCurrentRow && !string.Format(",{0}", _sAssignDetail_ID).Contains(string.Format(",{0}", grdAssignDetail.CurrentRow.Cells["AssignDetail_ID"].Value)))
                //{
                //    _sAssignDetail_ID += string.Format(",{0},", grdAssignDetail.CurrentRow.Cells["AssignDetail_ID"].Value);
                //}

                DataTable temdt = SPs.ClsKetQuaCDHAByAssignDetail(_sAssignDetail_ID).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grdKetQuaCDHA, temdt, true, true, "", "");
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message, "Lỗi khi lấy Kết quả CDHA.", MessageBoxIcon.Error);
            }
        }
        private void cmdAccept_Click(object sender, EventArgs e)
        {
            try
            {
                _sReturn = string.Empty;
                switch (_nServiceTypeID)
                {
                    case 1:
                        //getKetQuaXN(false);
                        foreach (var item in grdKetQuaXN.GetCheckedRows())
                        {
                            _sReturn += Utility.sDbnull(item.Cells["Ten_KQ"].Value) + ": " + Utility.sDbnull(item.Cells["Ket_Qua"].Value) + Utility.sDbnull(item.Cells["Don_vi"].Value) + ";\t";
                        }
                        break;
                    case 2:
                        //getKetQuaCDHA(false);
                        foreach (var item in grdKetQuaCDHA.GetDataRows())
                        {
                            _sReturn += Utility.sDbnull(item.Cells["ServiceDetail_Name"].Value) + ": " + Utility.sDbnull(item.Cells["Ket_Luan"].Value) + ";\t";
                        }
                        break;
                    default:
                        break;
                }
                this.Close();
            }
            catch (Exception exception)
            {
                Utility.ShowMsg(exception.Message);
            }
        }

        private void cmdGetData_Click(object sender, EventArgs e)
        {
            LoadCLS();
        }

        private void grdAssignDetail_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                switch (_nServiceTypeID)
                {
                    case 1:
                        getKetQuaXN(true);
                        break;
                    case 2:
                        getKetQuaCDHA(true);
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message, "Lỗi khi lấy chi tiết kết quả.", MessageBoxIcon.Error);
            }
        }
    }
}

