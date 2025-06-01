using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using VNS.Libs;
using VMS.HIS.DAL;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;
using SubSonic;

namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_Add_PhongNoiTru : Form
    {
        private string sKieuDanhMuc = "LYDOTANG";
        public NoitruDmucBuong objRoom;
        public delegate void timkiem();

        public timkiem MyGetData;
        #region "khai báo biến"
        public DataTable p_dtDataPhong=new DataTable();
        public action m_enAct = action.Insert;
        public Janus.Windows.GridEX.GridEX grdList;
        public string Loai = ""; 


        #endregion
        public frm_Add_PhongNoiTru()
        {
            InitializeComponent();
            //this.sKieuDanhMuc = KieuDanhMuc;
            
            this.KeyDown+=new KeyEventHandler(frm_Add_DCHUNG_KeyDown);
            cmdSave.Click+=new EventHandler(cmdSave_Click);
            cmdExit.Click+=new EventHandler(cmdExit_Click);
            txtTEN.LostFocus +=new EventHandler(txtTEN_LostFocus);

        }
       //ry>
        /// hàm thực hiện việc phím tắt của đơn vị tính
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_Add_DCHUNG_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S) cmdSave.PerformClick();
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.F5) NhapLienTuc();
        }
          /// <summary>
        /// hàm thực hiện việc load thông tin của form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_Add_DCHUNG_Load(object sender, EventArgs e)
        {
            try
            {
                InitData();
                if (m_enAct == action.Insert)
                {
                    IntialDataControl();
                }
                bHasloaded = true;
                Getdata();
                txtMa.Focus();
                txtMa.SelectAll();
            }
            catch (Exception ex)
            {

            }

        }
        //private DataTable m_dtKieuDungChung=new DataTable();
        private DataTable m_dtKhoaNoiTru=new DataTable();
        private void InitData()
        {

            m_dtKhoaNoiTru =
                THU_VIEN_CHUNG.Laydanhmuckhoa("NOI", 0);
            DataBinding.BindDataCombobox(cboKhoaNoiTru, m_dtKhoaNoiTru, DmucKhoaphong.Columns.IdKhoaphong,
                                       DmucKhoaphong.Columns.TenKhoaphong, "", true);
           
        }

        
        /// <summary>
        /// hàm thưucj hiện lấy thông tin của dữ liệu
        /// </summary>
        private void Getdata()
        {
            if (m_enAct == action.Update)
            {
                chkcodefrom.Enabled = false;
                lblSample.Visible = lblSuffix.Visible = false;
                txtID.Text = objRoom.IdBuong.ToString();
                txtMa.Text = Utility.sDbnull(objRoom.MaBuong, "");
                txtTEN.Text = Utility.sDbnull(objRoom.TenBuong, "");
                txt_Mo_Ta.Text = Utility.sDbnull(objRoom.MotaThem, "");
                chkTrangThai.Checked = Utility.Byte2Bool(objRoom.TrangThai.Value);
                cboKhoaNoiTru.SelectedValue = objRoom.IdKhoanoitru.ToString();
                txt_STT_HTHI.Text = objRoom.SttHthi.ToString();
            }

        }
        /// <summary>
        /// hàm thực hiện việc thoát Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// hàm thực hiện bắt sự kiện của form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            if(!InValiData())return;
            PerformAction();
        }
        private bool InValiData()
        {
            if (string.IsNullOrEmpty(txtMa.Text))
            {
                Utility.SetMsg(lblMessage,"Bạn phải nhập thông tin mã phòng ",true);
                txtMa.Focus();
                return false;
            }
            if (chkcodefrom.Checked)
            {
                if (Utility.DoTrim(txtCodefrom.Text) == "")
                {
                    Utility.SetMsg(lblMessage, "Bạn phải nhập mã bắt đầu", true);
                    txtCodefrom.Focus();
                    txtCodefrom.SelectAll();
                    return false;
                }
                if (Utility.DecimaltoDbnull(txtCodefrom.Text, 0) <= 0)
                {
                    Utility.SetMsg(lblMessage, "Hậu tố mã phòng bắt đầu phải >0", true);
                    txtCodefrom.Focus();
                    txtCodefrom.SelectAll();
                    return false;
                }
                if (Utility.DoTrim(txtCode2.Text) == "")
                {
                    Utility.SetMsg(lblMessage, "Bạn phải nhập mã kết thúc", true);
                    txtCode2.Focus();
                    txtCode2.SelectAll();
                    return false;
                }
                if (Utility.DecimaltoDbnull(txtCodefrom.Text, 0) > Utility.DecimaltoDbnull(txtCode2.Text, 0))
                {
                    Utility.SetMsg(lblMessage, "Hậu tố mã phòng bắt đầu < mã phòng đến", true);
                    txtCode2.Focus();
                    txtCode2.SelectAll();
                    return false;
                }
            }
             if (string.IsNullOrEmpty(txtTEN.Text))
            {
                Utility.SetMsg(lblMessage,"Bạn phải nhập thông tin tên phòng ", true);
                txtTEN.Focus();
                return false;
            }
            if(cboKhoaNoiTru.SelectedIndex<0)
            {
                Utility.SetMsg(lblMessage,"Bạn phải chọn khoa nội trú ", true);
                cboKhoaNoiTru.Focus();
                return false;
            }
            DmucKhoaphong objDmucKhoaphong = DmucKhoaphong.FetchByID(Utility.Int32Dbnull(cboKhoaNoiTru.SelectedValue));
            if(objDmucKhoaphong==null)
            {
                Utility.SetMsg(lblMessage,"Bạn chọn khoa nội trú ", true);
                cboKhoaNoiTru.Focus();
                return false;
            }
            if (m_enAct == action.Insert)
            {
                if (!chkcodefrom.Checked)//Thêm đơn lẻ từng chiếc mới kiểm tra
                {
                    SqlQuery sqlQuery = new Select().From(NoitruDmucBuong.Schema)
                   .Where(NoitruDmucBuong.Columns.IdKhoanoitru).IsEqualTo(Utility.Int32Dbnull(cboKhoaNoiTru.SelectedValue, ""));
                    sqlQuery.And(NoitruDmucBuong.Columns.MaBuong).IsEqualTo(Utility.sDbnull(txtMa.Text));
                    if (sqlQuery.GetRecordCount() > 0)
                    {
                        Utility.ShowMsg("Mã buồng đã tồn tại. Đề nghị bạn nhập mã khác", "Thông báo", MessageBoxIcon.Warning);
                        txtMa.Focus();
                        return false;
                    }
                    sqlQuery = new Select().From(NoitruDmucBuong.Schema)
                   .Where(NoitruDmucBuong.Columns.IdKhoanoitru).IsEqualTo(Utility.Int32Dbnull(cboKhoaNoiTru.SelectedValue, ""));
                    sqlQuery.And(NoitruDmucBuong.Columns.TenBuong).IsEqualTo(Utility.sDbnull(txtTEN.Text));
                    if (sqlQuery.GetRecordCount() > 0)
                    {
                        Utility.ShowMsg("Tên buồng đã tồn tại. Đề nghị bạn nhập mã khác", "Thông báo", MessageBoxIcon.Warning);
                        txtTEN.Focus();
                        return false;
                    }
                }

            }
            else
            {
                SqlQuery sqlQuery = new Select().From(NoitruDmucBuong.Schema)
              .Where(NoitruDmucBuong.Columns.IdKhoanoitru).IsEqualTo(Utility.sDbnull(cboKhoaNoiTru.SelectedValue, ""));
                sqlQuery.And(NoitruDmucBuong.Columns.MaBuong).IsEqualTo(Utility.sDbnull(txtMa.Text))
                    .And(NoitruDmucBuong.Columns.IdBuong).IsNotEqualTo(Utility.Int32Dbnull(txtID.Text));
                if (sqlQuery.GetRecordCount() > 0)
                {
                    Utility.ShowMsg("Mã buồng đã tồn tại. Đề nghị bạn nhập mã khác", "Thông báo", MessageBoxIcon.Warning);
                    txtMa.Focus();
                    return false;
                }
                sqlQuery = new Select().From(NoitruDmucBuong.Schema)
               .Where(NoitruDmucBuong.Columns.IdKhoanoitru).IsEqualTo(Utility.sDbnull(cboKhoaNoiTru.SelectedValue, ""));
                sqlQuery.And(NoitruDmucBuong.Columns.TenBuong).IsEqualTo(Utility.sDbnull(txtTEN.Text))
                    .And(NoitruDmucBuong.Columns.IdBuong).IsNotEqualTo(Utility.Int32Dbnull(txtID.Text));
                if (sqlQuery.GetRecordCount() > 0)
                {
                    Utility.ShowMsg("Tên buồng đã tồn tại. Đề nghị bạn nhập mã khác", "Thông báo", MessageBoxIcon.Warning);
                    txtTEN.Focus();
                    return false;
                }
            }
            return true;
        }

        public bool b_Cancel = false;
        private void PerformAction()
        {
            switch (m_enAct)
            {
                case action.Insert:
                    if (chkcodefrom.Checked)
                        MultiInsert();
                    else
                        InsertData();
                    break;
                case action.Update:
                    UpdateData();
                    break;
            }
            b_Cancel = true;

        }

        private Query _Query = NoitruDmucBuong.CreateQuery();
        private void MultiInsert()
        {
            try
            {
                Int16 STTHthi=Utility.Int16Dbnull(txt_STT_HTHI.Text);
                for (int i = Utility.Int32Dbnull(Utility.DecimaltoDbnull(txtCodefrom.Text), 0); i <= Utility.Int32Dbnull(Utility.DecimaltoDbnull(txtCode2.Text), 0); i++)
                {
                    objRoom = new NoitruDmucBuong();
                    objRoom.IdBuong = -1;
                    objRoom.IdBuong = Utility.Int16Dbnull(txtID.Text);
                    objRoom.MaBuong =  Utility.DoTrim(txtMa.Text) + i.ToString();
                    objRoom.TenBuong =  Utility.DoTrim(txtTEN.Text) + " " + i.ToString();
                    objRoom.MotaThem = Utility.DoTrim(txt_Mo_Ta.Text);
                    objRoom.TrangThai = Utility.Bool2byte(chkTrangThai.Checked);
                    objRoom.IdKhoanoitru = Utility.Int16Dbnull(cboKhoaNoiTru.SelectedValue);
                    NoitruDmucBuong objcheck = new Select().From(NoitruDmucBuong.Schema).Where(NoitruDmucBuong.Columns.MaBuong).IsEqualTo(objRoom.MaBuong).ExecuteSingle<NoitruDmucBuong>();
                    if (objcheck != null)
                    {
                        if (chkAutoupdate.Checked)
                        {
                            objRoom.IdBuong = objcheck.IdBuong;
                            objRoom.MarkOld();
                            objRoom.IsNew = true;
                        }
                        else
                        {
                            continue;
                        }
                    }
                    {
                        STTHthi += 1;
                        objRoom.IsNew = true;
                    }
                   
                    objRoom.SttHthi = STTHthi;
                    objRoom.Save();
                    if (objRoom != null)
                    {
                        DataRow newDR = p_dtDataPhong.NewRow();
                        Utility.FromObjectToDatarow(objRoom, ref newDR);
                        newDR["ten_khoanoitru"] = cboKhoaNoiTru.Text;
                        p_dtDataPhong.Rows.Add(newDR);
                        if (MyGetData != null)
                        {
                            MyGetData();
                        }
                        Utility.GonewRowJanus(grdList, NoitruDmucBuong.Columns.IdBuong, Utility.sDbnull(txtID.Text));
                    }
                   
                }
               
                Utility.SetMsg(lblMessage, "Bạn thực hiện thêm mới thành công", true);
                if (chkthemmoilientuc.Checked) ClearControl();
                else
                    Close();
                
            }
            catch (Exception exception)
            {

                Utility.CatchException(exception);
            }

        }
        /// <summary>
        /// hàm thực hiện viẹc thêmm ới thông tin 
        /// </summary>
        private void InsertData()
        {
            try
            {
                objRoom = new NoitruDmucBuong();
                objRoom.IdBuong = -1;
                objRoom.IdBuong =Utility.Int16Dbnull( txtID.Text);
                objRoom.MaBuong =  Utility.DoTrim(txtMa.Text);
                objRoom.TenBuong =  Utility.DoTrim(txtTEN.Text);
                objRoom.MotaThem=Utility.DoTrim(txt_Mo_Ta.Text);
                objRoom.TrangThai =Utility.Bool2byte( chkTrangThai.Checked);
                objRoom.IdKhoanoitru =Utility.Int16Dbnull( cboKhoaNoiTru.SelectedValue);
                objRoom.SttHthi = Utility.Int16Dbnull(txt_STT_HTHI.Text);

                objRoom.IsNew = true;
                objRoom.Save();
                if (objRoom != null)
                {
                    DataRow newDR = p_dtDataPhong.NewRow();
                    Utility.FromObjectToDatarow(objRoom, ref newDR);
                    newDR["ten_khoanoitru"] = cboKhoaNoiTru.Text;
                    p_dtDataPhong.Rows.Add(newDR);
                    if (MyGetData != null)
                    {
                        MyGetData();
                    }
                    Utility.GonewRowJanus(grdList, NoitruDmucBuong.Columns.IdBuong, Utility.sDbnull(txtID.Text));
                }
                Utility.SetMsg(lblMessage, "Bạn thực hiện thêm mới thành công", true);
                if (chkthemmoilientuc.Checked) ClearControl();
                else
                    Close();
            }
            catch (Exception exception)
            {

                Utility.CatchException(exception);
            }
           
        }
        /// <summary>
        /// hàm thưucj hiện việc cập nhập lại thông tin thành công
        /// </summary>
        private void UpdateData()
        {
            try
            {
                objRoom = new NoitruDmucBuongController().FetchByID(txtID.Text)[0];
                objRoom.IdBuong = Utility.Int16Dbnull(txtID.Text);
                objRoom.MaBuong = txtMa.Text;
                objRoom.TenBuong = txtTEN.Text;
                //txt_Mo_Ta.Text = objRoom.MotaThem;
                objRoom.TrangThai = Utility.Bool2byte(chkTrangThai.Checked);
                objRoom.IdKhoanoitru = Utility.Int16Dbnull(cboKhoaNoiTru.SelectedValue);
                objRoom.SttHthi = Utility.Int16Dbnull(txt_STT_HTHI.Text);
                objRoom.MotaThem = Utility.DoTrim(txt_Mo_Ta.Text);
                objRoom.IsNew = false;
                objRoom.MarkOld();
                objRoom.Save();
                if (MyGetData != null)
                {
                    DataRow newDR = p_dtDataPhong.Select(NoitruDmucBuong.Columns.IdBuong + "=" + objRoom.IdBuong.ToString())[0];
                    Utility.FromObjectToDatarow(objRoom, ref newDR);
                    newDR["ten_khoanoitru"] = cboKhoaNoiTru.Text;
                    p_dtDataPhong.AcceptChanges();
                    MyGetData();
                }
                Utility.GonewRowJanus(grdList, NoitruDmucBuong.Columns.IdBuong, Utility.sDbnull(txtID.Text));
                if (chkthemmoilientuc.Checked) ClearControl();
                else
                    Close();
                Utility.SetMsg(lblMessage, "Bạn thực hiện sửa thông tin  thành công", true);
            }
            catch(Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        private void txtTEN_LostFocus(object sender, EventArgs e)
        {
           
        }
       

        private void txtTEN_TextChanged(object sender, EventArgs e)
        {
          
        }

        private void cboLoaiChung_SelectedIndexChanged(object sender, EventArgs e)
        {
    
        }

        private void txtMa_TextChanged(object sender, EventArgs e)
        {
             
        }
        /// <summary>
        /// hàm thực hiện cho phép nhập liên tục thông tin 
        /// </summary>
        private void NhapLienTuc()
        {

            m_enAct = action.Insert;
            ClearControl();
            IntialDataControl();

        }
        private void IntialDataControl()
        {
            if (m_enAct == action.Insert)
            {
                int MaxSTT =
                    Utility.Int32Dbnull(
                        _Query.AddWhere(NoitruDmucBuong.Columns.IdKhoanoitru, Comparison.Equals, Utility.Int32Dbnull(cboKhoaNoiTru.SelectedValue)).GetMax(
                            NoitruDmucBuong.Columns.SttHthi), 0);

                txt_STT_HTHI.Value = Utility.Int32Dbnull(MaxSTT+ 1);
                txtMa.Focus();
            }

        }
        private void ClearControl()
        {
          
            m_enAct = action.Insert;
            foreach (Control control in this.grpControl.Controls)
            {
                if (control is Janus.Windows.GridEX.EditControls.EditBox) control.Text = string.Empty;

            }
            txtMa.Enabled = true;
            cboKhoaNoiTru.SelectedIndex = 0;
            txtMa.Focus();

        }

        private bool bHasloaded = false;
        private void cboKhoaNoiTru_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bHasloaded)
            IntialDataControl();
        }

        private void chkcodefrom_CheckedChanged(object sender, EventArgs e)
        {
            txtCodefrom.Enabled = txtCode2.Enabled = chkcodefrom.Checked;
            lblmatiento.Text = chkcodefrom.Checked ?"Mã chung": "Mã buồng"  ;
            lblSuffix.Visible=lblSample.Visible = chkcodefrom.Checked;
            
        }
    }
}
