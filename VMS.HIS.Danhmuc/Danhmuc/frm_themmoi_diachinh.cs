using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;
using Janus.Windows.GridEX;

namespace VNS.HIS.UI.DANHMUC
{
    public partial class frm_themmoi_diachinh : Form
    {
        #region "Declare Variable (Level private)"
        bool m_blnLoaded = false;
        public action em_Action = action.Insert;
        public GridEX grdList;
        private Query _QuerySurvey = DmucDiachinh.CreateQuery();
        #endregion

        #region "Contructor"
        public frm_themmoi_diachinh()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            txtSurveyName.LostFocus+=new EventHandler(txtSurveyName_LostFocus);
            cboSurveyType.SelectedIndexChanged += new EventHandler(cboSurveyType_SelectedIndexChanged);
            
        }
        void cboSurveyType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!m_blnLoaded) return;

                DataRow[] arrDR = globalVariables.gv_dtDmucDiachinh.Select(DmucDiachinh.Columns.LoaiDiachinh + "=" + (cboSurveyType.SelectedIndex-1).ToString());
                if (cboSurveyType.SelectedIndex >0)
                {
                   cboDiachinhcaptren.Enabled= lblCaptren.Enabled = true;
                    if (arrDR.Length > 0)
                        DataBinding.BindDataCombox(cboDiachinhcaptren, arrDR.CopyToDataTable(), DmucDiachinh.Columns.MaDiachinh, DmucDiachinh.Columns.TenDiachinh, "Chọn", true);
                    else
                        DataBinding.BindDataCombox(cboDiachinhcaptren, null, DmucDiachinh.Columns.MaDiachinh, DmucDiachinh.Columns.TenDiachinh, "Chọn", true);
                }
                else
                {
                    DataBinding.BindDataCombox(cboDiachinhcaptren, null, DmucDiachinh.Columns.MaDiachinh, DmucDiachinh.Columns.TenDiachinh);
                    cboDiachinhcaptren.Enabled = lblCaptren.Enabled = false;
                }

            }
            catch
            {
            }
        }
        #endregion
        private void txtSurveyName_LostFocus(object sender, System.EventArgs e)
        {
            txtSurveyName.Text = Utility.chuanhoachuoi(txtSurveyName.Text);
        }
        private void IntialData()
        {
            cboSurveyType.SelectedIndex=0;
           

        }
        #region "Method Of Event Form"

        /// <summary>
        /// Form đóng khi nhấn nút
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// lưu lại thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            PerformAction();
        }
       /// <summary>
       /// Load thông tin khi load Form lên
       /// </summary>
       /// <param name="sender"></param>
       /// <param name="e"></param>
        private void frm_themmoi_diachinh_Load(object sender, EventArgs e)
        {
            m_blnLoaded = true;
            if (em_Action == action.Update) GetData();
            cboSurveyType_SelectedIndexChanged(cboSurveyType, new EventArgs());
            MofifyCommand();
            
           
        }
       
        /// <summary>
        /// Phím tắt của Form khi dùng Phím tứt
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_themmoi_diachinh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.Control && e.KeyCode == Keys.S) cmdSave.PerformClick();
            if (e.KeyCode == Keys.F5) ClearControl();
        }
        #endregion
        #region "Event Common Form"
       

        /// <summary>
        /// trạng thái khi bắt sự kiện thông tin Save thông tin
        /// </summary>
        private void PerformAction()
        {
            switch (em_Action)
            {
                case action.Insert:
                    if (!isValidData()) return;
                    PerformActionInsert();
                    break;
                case action.Update:
                    if (!InValiUpdateData()) return;
                    PerformActionUpdate();
                    break;
            }

        }
        /// <summary>
        /// Kiểm tra thông tin khi Insert thông tin 
        /// </summary>
        /// <returns></returns>
        private bool isValidData()
        {

            if (string.IsNullOrEmpty(txtSurveyCode.Text))
            {
                Utility.SetMsgError(errorProvider1, txtSurveyCode, "Bạn phải nhập mã địa chính");
                txtSurveyCode.Focus();
                return false;

            }
            if (string.IsNullOrEmpty(txtSurveyName.Text))
            {
                Utility.SetMsgError(errorProvider2, txtSurveyName, "Bạn phải nhập tên địa chính");
                txtSurveyName.Focus();
                return false;

            }
            if(cboDiachinhcaptren.Enabled)
            {
                if (cboDiachinhcaptren.SelectedIndex <= 0)
                {
                    Utility.SetMsgError(errorProvider3, cboDiachinhcaptren, "Bạn phải chọn kiểu địa chính");
                    cboDiachinhcaptren.Focus();
                    return false;
                }
            }
            SqlQuery q = new Select().From(DmucDiachinh.Schema)
               .Where(DmucDiachinh.Columns.MaDiachinh).IsEqualTo(txtSurveyCode.Text);
            if (q.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Đã tồn tại mã địa chính, Yêu cầu nhập lại");
                txtSurveyCode.Focus();
                return false;
            }
           
            return true;
        }
        /// <summary>
        /// kiểm tra thông tin khi Update thông tin 
        /// </summary>
        /// <returns></returns>
        private bool InValiUpdateData()
        {

            if (string.IsNullOrEmpty(txtSurveyCode.Text))
            {
                Utility.SetMsgError(errorProvider1, txtSurveyCode, "Bạn phải nhập mã địa chính");
                txtSurveyCode.Focus();
                return false;

            }
            if (string.IsNullOrEmpty(txtSurveyName.Text))
            {
                Utility.SetMsgError(errorProvider2, txtSurveyName, "Bạn phải nhập tên địa chính");
                txtSurveyName.Focus();
                return false;

            }
            if (cboDiachinhcaptren.Enabled)
            {
                if (cboDiachinhcaptren.SelectedIndex <= 0)
                {
                    Utility.SetMsgError(errorProvider3, cboDiachinhcaptren, "Bạn phải chọn kiểu địa chính");
                    cboDiachinhcaptren.Focus();
                    return false;
                }
            }
            
            SqlQuery q1 = new Select().From(DmucDiachinh.Schema)
               .Where(DmucDiachinh.Columns.TenDiachinh).IsEqualTo(txtSurveyName.Text).And(DmucDiachinh.Columns.MaDiachinh)
               .IsNotEqualTo(txtSurveyCode.Text);
            if (q1.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Đã tồn tại tên địa chính, Yêu cầu nhập lại");
                txtSurveyName.Focus();
                return false;
            }
            return true;
        }
        private void PerformActionInsert()
        {
            DmucDiachinh.Insert(txtSurveyCode.Text, txtSurveyName.Text,
                cboDiachinhcaptren.Enabled && cboDiachinhcaptren.SelectedIndex>0? Utility.sDbnull(cboDiachinhcaptren.SelectedValue, ""):"" ,
                           Utility.Int16Dbnull(txtIntOrder.Text, ""),
                            Convert.ToByte(cboSurveyType.SelectedIndex), txtsDesc.Text);

            DataRow dr = globalVariables.gv_dtDmucDiachinh.NewRow();
           // dr[DmucDiachinh.Columns.MaDiachinh] = Utility.Int16Dbnull(_Query.GetMax(LDisease.Columns.DiseaseId), -1);
            dr[DmucDiachinh.Columns.MaDiachinh] = txtSurveyCode.Text;
            dr[DmucDiachinh.Columns.TenDiachinh] = Utility.sDbnull(txtSurveyName.Text, "");
            dr[DmucDiachinh.Columns.MaCha] = cboDiachinhcaptren.Enabled && cboDiachinhcaptren.SelectedIndex > 0 ? Utility.sDbnull(cboDiachinhcaptren.SelectedValue, "") : "";
            dr[DmucDiachinh.Columns.MotaThem] = Utility.sDbnull(txtsDesc.Text, "");
            dr[DmucDiachinh.Columns.LoaiDiachinh] = Convert.ToByte(cboSurveyType.SelectedIndex);
            dr["ten_loaidiachinh"] = Utility.getTenloaiDiachinh(cboSurveyType.SelectedIndex);
            dr["ten_diachinh_captren"] = Utility.sDbnull(cboDiachinhcaptren.Text, "");
            dr[DmucDiachinh.Columns.SttHthi] = Utility.Int16Dbnull(txtIntOrder.Text, "0");
            globalVariables.gv_dtDmucDiachinh.Rows.Add(dr);
            if (grdList != null) Utility.GotoNewRowJanus(grdList, DmucDiachinh.Columns.MaDiachinh, txtSurveyCode.Text);
            this.Close();
        }
        private void PerformActionUpdate()
        {
            new Update(DmucDiachinh.Schema).Set(DmucDiachinh.TenDiachinhColumn).EqualTo(txtSurveyName.Text)
                .Set(DmucDiachinh.LoaiDiachinhColumn).EqualTo(Convert.ToByte(cboSurveyType.SelectedIndex))
                .Set(DmucDiachinh.MaChaColumn).EqualTo(cboDiachinhcaptren.Enabled && cboDiachinhcaptren.SelectedIndex > 0 ? Utility.sDbnull(cboDiachinhcaptren.SelectedValue, "") : "")
                .Set(DmucDiachinh.MotaThemColumn).EqualTo(txtsDesc.Text)
                .Where(DmucDiachinh.MaDiachinhColumn).IsEqualTo(Utility.sDbnull(txtSurveyCode.Text, "-1")).Execute();


            DataRow[] dr = globalVariables.gv_dtDmucDiachinh.Select(DmucDiachinh.Columns.MaDiachinh + "='" + Utility.sDbnull(txtSurveyCode.Text, -1) + "'");
            if (dr.GetLength(0) > 0)
            {
                //dr[0][LDisease.Columns.DiseaseId] = Utility.Int16Dbnull(_Query.GetMax(LDisease.Columns.DiseaseId), -1);
                dr[0][DmucDiachinh.Columns.MaDiachinh] = txtSurveyCode.Text;
                dr[0][DmucDiachinh.Columns.TenDiachinh] = Utility.sDbnull(txtSurveyName.Text, "");
                dr[0][DmucDiachinh.Columns.MaCha] = cboDiachinhcaptren.Enabled && cboDiachinhcaptren.SelectedIndex > 0 ? Utility.sDbnull(cboDiachinhcaptren.SelectedValue, "") : "";
                dr[0][DmucDiachinh.Columns.MotaThem] = Utility.sDbnull(txtsDesc.Text, "");
                dr[0][DmucDiachinh.Columns.LoaiDiachinh] = Convert.ToByte(cboSurveyType.SelectedIndex);
                dr[0]["ten_loaidiachinh"] =Utility.getTenloaiDiachinh(cboSurveyType.SelectedIndex);
                dr[0]["ten_diachinh_captren"] = Utility.sDbnull(cboDiachinhcaptren.Text, "");
                dr[0][DmucDiachinh.Columns.SttHthi] = Utility.Int16Dbnull(txtIntOrder.Text, "0");

            }
           //p_dtSurvey.AcceptChanges();
            this.Close();

        }

        private void GetData()
        {
            DmucDiachinh objDiachinh = DmucDiachinh.FetchByID(Utility.sDbnull(txtSurveyCode.Text,""));
            if (objDiachinh != null)
            {
                txtsDesc.Text = objDiachinh.MotaThem;
                txtSurveyCode.Text = objDiachinh.MaDiachinh;
                txtSurveyName.Text = objDiachinh.TenDiachinh;
                cboDiachinhcaptren.Enabled = lblCaptren.Enabled = objDiachinh.LoaiDiachinh <= 1;
                txtIntOrder.Value = Utility.DecimaltoDbnull(objDiachinh.SttHthi,0);
                cboSurveyType.SelectedIndex =(int) objDiachinh.LoaiDiachinh.Value;
                cboDiachinhcaptren.Enabled = lblCaptren.Enabled = cboSurveyType.SelectedIndex > 0;
                if (cboDiachinhcaptren.Enabled)
                    cboDiachinhcaptren.SelectedIndex = Utility.GetSelectedIndex(cboDiachinhcaptren,
                                                                      Utility.sDbnull( objDiachinh.MaCha,""));
            }
            txtSurveyCode.Enabled = false;
        }
        
        private void MofifyCommand()
        {

            if (string.IsNullOrEmpty(txtSurveyCode.Text))
            {
                Utility.SetMsgError(errorProvider1, txtSurveyCode, "Bạn phải nhập mã đia chính");
                txtSurveyCode.Focus();


            }
            if (string.IsNullOrEmpty(txtSurveyName.Text))
            {
                Utility.SetMsgError(errorProvider2, txtSurveyName, "Bạn phải nhập tên địa chính");
                //txtDieaseCode.Focus();


            }
            if (cboDiachinhcaptren.Enabled)
            {
                if (cboDiachinhcaptren.SelectedIndex <= 0)
                {
                    Utility.SetMsgError(errorProvider3, cboDiachinhcaptren, "Bạn phải chọn đơn vị địa chính cấp trên");
                    //cboDieaseType.Focus();

                }
            }
            if (em_Action == action.Insert)
            {
                if (cboDiachinhcaptren.Enabled)
                    txtIntOrder.Value = Utility.DecimaltoDbnull(new Select(Aggregate.Max(DmucDiachinh.Columns.SttHthi)).From(DmucDiachinh.Schema).Where(DmucDiachinh.Columns.LoaiDiachinh).IsEqualTo(0).ExecuteScalar()) + 1;
                else
                    txtIntOrder.Value = Utility.DecimaltoDbnull(new Select(Aggregate.Max(DmucDiachinh.Columns.SttHthi)).From(DmucDiachinh.Schema).Where(DmucDiachinh.Columns.LoaiDiachinh).IsEqualTo(1).ExecuteScalar()) + 1;

            }
        }
        #endregion

        private void cmdClear_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ClearControl();
        }
        void ClearControl()
        {
            foreach (Control control in grpControl.Controls)
            {
                if (control is Janus.Windows.GridEX.EditControls.EditBox)
                {
                    ((Janus.Windows.GridEX.EditControls.EditBox)(control)).Clear();
                }
                em_Action = action.Insert;
                txtSurveyCode.Focus();
            }
        }

      
       
    }
}
