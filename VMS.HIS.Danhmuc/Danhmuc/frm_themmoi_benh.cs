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
using Janus.Windows.GridEX.EditControls;

namespace VNS.HIS.UI.DANHMUC
{
    public partial class frm_themmoi_benh : Form
    {
        #region "Declare Variable public"
        public action em_Action = action.Insert;
        public DataTable m_dtDisease;
        public GridEX grdDiease;
        #endregion
        #region "Declare Variable private"
        private DataTable m_dtDiseaseType = new DataTable();
        private Query _Query = DmucBenh.CreateQuery();
        #endregion
        #region "Contructor"
        public frm_themmoi_benh()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            txtDieaseName.LostFocus+=new EventHandler(txtDieaseName_LostFocus);
            m_dtDiseaseType = new Select().From(DmucChung.Schema).Where(DmucChung.Columns.Loai).IsEqualTo("LOAIBENH").ExecuteDataSet().Tables[0];
            txtDieaseCode.CharacterCasing = CharacterCasing.Upper;
        }
        #endregion
        #region "Method Of Event Form"
        private void txtDieaseName_LostFocus(object sender, System.EventArgs e)
        {
            //txtDieaseName.Text = Utility.chuanhoachuoi(txtDieaseName.Text);
        }
        /// <summary>
        /// Dóng Form hiện tại lại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// Hàm thực hiện load toàn bộ thông tin của Form hiển thị lên
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_themmoi_benh_Load(object sender, EventArgs e)
        {
            InitData();
            MofifyCommand();
            if(em_Action==action.Update) GetData();
            
        }
        /// <summary>
        /// hàm thực hiện thay đổi thông tin của mã bệnh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDieaseCode_TextChanged(object sender, EventArgs e)
        {
            MofifyCommand();
        }
        /// <summary>
        /// hàm thực hiện thay đổi thông tin của tên bệnh
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDieaseName_TextChanged(object sender, EventArgs e)
        {
            MofifyCommand();
        }
        /// <summary>
        /// Lưu lại thông tin của nút
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            PerformAction();
           
        }
       
        /// <summary>
        /// bắt sự kiện khi thay đổi change của combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboDieaseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            MofifyCommand();
        }
        /// <summary>
        /// sự kiện dùng để thực hiện phím tắt của Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_themmoi_benh_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Control ctr = Utility.getActiveControl(this);
                if (ctr != null && (ctr.Name==cboDieaseType.Name || ctr.Name== txtsDesc.Name))
                    return;
                else if (ctr != null && ctr.GetType().Equals(typeof(EditBox)))
                {
                    EditBox box = ctr as EditBox;
                    if (box.Multiline)
                        return;
                    else
                        SendKeys.Send("{TAB}");
                }
                else
                        SendKeys.Send("{TAB}");
            }
            if(e.KeyCode==Keys.Escape)cmdExit.PerformClick();
            if(e.Control&&e.KeyCode==Keys.S)cmdSave.PerformClick();
        }
        /// <summary>
        /// bắt kiện khi thay đổi khóa chính
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDiease_ID_TextChanged(object sender, EventArgs e)
        {

        }

        #endregion

        #region "Method Of Common"


        /// <summary>
        /// thực hiện thông tin thêm mới hoặc update
        /// </summary>
        private void PerformAction()
        {
            switch (em_Action)
            {
                case action.Insert:
                    if (!InValiData()) return;
                    PerformActionInsert();
                    break;
                case action.Update:
                    if (!InValiUpdateData()) return;
                    PerformActionUpdate();
                    break;
            }

        }

        

        /// <summary>
        /// Bắt sự kiện khi thêm mới thông tin
        /// </summary>
        private void PerformActionInsert()
        {
            try
            {
                DmucBenh.Insert(txtDieaseCode.Text, Utility.sDbnull(cboDieaseType.SelectedValue, "-1"), Utility.sDbnull(txtDieaseName.Text, ""), Utility.sDbnull(txtTenTiengAnh.Text, ""), txtsDesc.Text, "");
                DataRow dr = m_dtDisease.NewRow();
                dr[DmucBenh.Columns.IdBenh] = Utility.Int16Dbnull(_Query.GetMax(DmucBenh.Columns.IdBenh), -1);
                dr[DmucBenh.Columns.MaBenh] = txtDieaseCode.Text;
                dr[DmucBenh.Columns.TenBenh] = Utility.sDbnull(txtDieaseName.Text, "");
                dr[DmucBenh.Columns.TenQuocte] = Utility.sDbnull(txtTenTiengAnh.Text, "");
                dr[DmucBenh.Columns.MaLoaibenh] = Utility.Int16Dbnull(cboDieaseType.SelectedValue, -1);
                dr[DmucBenh.Columns.MotaThem] = Utility.sDbnull(txtsDesc.Text, "");
                dr["ten_loaibenh"] = Utility.sDbnull(cboDieaseType.Text, "");
                m_dtDisease.Rows.Add(dr);
                globalVariables.gv_dtDmucBenh.ImportRow(dr);
                Utility.GonewRowJanus(grdDiease, DmucBenh.Columns.IdBenh, Utility.sDbnull(dr[DmucBenh.Columns.IdBenh], "-1"));
                this.Close();
            }
            catch
            {}
        }
        /// <summary>
        /// bắt sự kiện của Update thông tin 
        /// </summary>
        private void PerformActionUpdate()
        {
            //DmucBenh.Update(Utility.Int16Dbnull(txtDiease_ID.Text, -1), txtDieaseCode.Text, Utility.Int16Dbnull(cboDieaseType.SelectedValue, -1), Utility.sDbnull(txtDieaseName.Text, ""), txtsDesc.Text);
           int record= new Update(DmucBenh.Schema)
                .Set(DmucBenh.Columns.MotaThem).EqualTo(Utility.sDbnull(txtsDesc.Text))
                .Set(DmucBenh.Columns.MaLoaibenh).EqualTo(cboDieaseType.SelectedValue)
                .Set(DmucBenh.Columns.MaBenh).EqualTo(Utility.sDbnull(txtDieaseCode.Text))
                .Set(DmucBenh.Columns.TenBenh).EqualTo(Utility.sDbnull(txtDieaseName.Text))
                .Set(DmucBenh.Columns.TenQuocte).EqualTo(Utility.sDbnull(txtTenTiengAnh.Text))
                .Where(DmucBenh.Columns.IdBenh).IsEqualTo(Utility.Int32Dbnull(txtDiease_ID.Text, -1)).Execute();
            if(record>0)
            {
                DataRow[] dr = m_dtDisease.Select(DmucBenh.Columns.IdBenh+ "=" + Utility.Int32Dbnull(txtDiease_ID.Text, -1));
                if (dr.GetLength(0) > 0)
                {
                    dr[0][DmucBenh.Columns.MaBenh] = txtDieaseCode.Text;
                    dr[0][DmucBenh.Columns.TenBenh] = Utility.sDbnull(txtDieaseName.Text, "");
                    dr[0][DmucBenh.Columns.TenQuocte] = Utility.sDbnull(txtTenTiengAnh.Text, "");
                    dr[0][DmucBenh.Columns.MaLoaibenh] = Utility.sDbnull(cboDieaseType.SelectedValue, "-1");
                    dr[0][DmucBenh.Columns.MotaThem] = Utility.sDbnull(txtsDesc.Text, "");
                    dr[0]["ten_loaibenh"] = Utility.sDbnull(cboDieaseType.Text, "");

                }
                this.Close();
            }
            else
            {
                Utility.ShowMsg("Lỗi trong quá trình cập nhập thông tin");
            }
         

        }
        private void GetData()
        {
            DmucBenh objDisease = DmucBenh.FetchByID(Utility.Int32Dbnull(txtDiease_ID.Text, -1));
            if (objDisease != null)
            {
                txtsDesc.Text = objDisease.MotaThem;
                txtDieaseCode.Text = objDisease.MaBenh;
                txtDieaseName.Text = objDisease.TenBenh;
                txtTenTiengAnh.Text = objDisease.TenQuocte;
                cboDieaseType.SelectedValue = objDisease.MaLoaibenh;
            }
        }
        private void InitData()
        {
            DataBinding.BindDataCombox(cboDieaseType, m_dtDiseaseType, DmucChung.Columns.Ma, DmucChung.Columns.Ten);
        }
        private void MofifyCommand()
        {
            Utility.ResetMessageError(errorProvider1);
            Utility.ResetMessageError(errorProvider2);
            Utility.ResetMessageError(errorProvider3);
           
           

        }
        /// <summary>
        /// hàm thực hiển kiểm tra Validata dữ liệu
        /// </summary>
        /// <returns></returns>
        private bool InValiData()
        {

            if (string.IsNullOrEmpty(txtDieaseCode.Text))
            {
                Utility.SetMsgError(errorProvider1, txtDieaseCode, "Bạn phải nhập mã bệnh");
                txtDieaseCode.Focus();
                return false;

            }
            if (string.IsNullOrEmpty(txtDieaseName.Text))
            {
                Utility.SetMsgError(errorProvider2, txtDieaseName, "Bạn phải nhập tên bệnh");
                txtDieaseName.Focus();
                return false;

            }
            if (cboDieaseType.SelectedIndex <= 0)
            {
                Utility.SetMsgError(errorProvider3, cboDieaseType, "Bạn phải chọn kiểu bệnh");
                cboDieaseType.Focus();
                return false;
            }
            SqlQuery q = new Select().From(DmucBenh.Schema)
                .Where(DmucBenh.Columns.MaBenh).IsEqualTo(txtDieaseCode.Text).And(DmucBenh.Columns.MaLoaibenh).IsEqualTo(cboDieaseType.SelectedValue);
            if (q.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Đã tồn tại mã bệnh, Yêu cầu nhập lại");
                txtDieaseCode.Focus();
                return false;
            }
            SqlQuery q1 = new Select().From(DmucBenh.Schema)
                .Where(DmucBenh.Columns.TenBenh).IsEqualTo(txtDieaseName.Text).And(DmucBenh.Columns.MaLoaibenh).IsEqualTo(cboDieaseType.SelectedValue);
            if (q1.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Đã tồn tại tên bệnh, Yêu cầu nhập lại");
                txtDieaseName.Focus();
                return false;
            }
            return true;

        }
        private bool InValiUpdateData()
        {

            if (string.IsNullOrEmpty(txtDieaseCode.Text))
            {
                Utility.SetMsgError(errorProvider1, txtDieaseCode, "Bạn phải nhập mã bệnh");
                txtDieaseCode.Focus();
                return false;

            }
            if (string.IsNullOrEmpty(txtDieaseName.Text))
            {
                Utility.SetMsgError(errorProvider2, txtDieaseName, "Bạn phải nhập tên bệnh");
                txtDieaseCode.Focus();
                return false;

            }
            if (cboDieaseType.SelectedIndex <= 0)
            {
                Utility.SetMsgError(errorProvider3, cboDieaseType, "Bạn phải chọn kiểu bệnh");
                cboDieaseType.Focus();
                return false;
            }
            SqlQuery q = new Select().From(DmucBenh.Schema)
               .Where(DmucBenh.Columns.MaBenh).IsEqualTo(txtDieaseCode.Text)
               .And(DmucBenh.Columns.MaLoaibenh).IsEqualTo(cboDieaseType.SelectedValue)
               .And(DmucBenh.Columns.IdBenh).IsNotEqualTo(txtDiease_ID.Text);
            if (q.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Đã tồn tại mã bệnh, Yêu cầu nhập lại");
                txtDieaseCode.Focus();
                return false;
            }
            SqlQuery q1 = new Select().From(DmucBenh.Schema)
                .Where(DmucBenh.Columns.TenBenh).IsEqualTo(txtDieaseName.Text)
                .And(DmucBenh.Columns.MaLoaibenh).IsEqualTo(cboDieaseType.SelectedValue)
                .And(DmucBenh.Columns.IdBenh).IsNotEqualTo(txtDiease_ID.Text);
            if (q1.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Đã tồn tại tên bệnh, Yêu cầu nhập lại");
                txtDieaseName.Focus();
                return false;
            }
            return true;

        }
        #endregion
    }
}
