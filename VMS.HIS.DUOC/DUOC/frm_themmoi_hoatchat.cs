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

namespace VNS.HISLink.UI.Duoc.Form_DanhMuc
{
    public partial class frm_themmoi_hoatchat : Form
    {

        #region Khai bao bien

        public DmucHoatchat objReport;
        public action em_action = action.Insert;
        public Janus.Windows.GridEX.GridEX grdList;
        public bool _bAcceptClose = false;
        public delegate void LayThongTin();
        public frm_themmoi_hoatchat.LayThongTin MyGetData;

        #endregion
        public frm_themmoi_hoatchat()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            Checkcontrol();
        }
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void GetData()
        {
            try
            {



                // Utility.TryToSetBindData(chkhienthi, "Checked", objReport, QlddCheDo.HienThiColumn.PropertyName);
                objReport = DmucHoatchat.FetchByID(Utility.Int32Dbnull(txtIdHoatChat.Text));
                if (objReport != null)
                {
                    txtMaHoatChat.Text = Utility.sDbnull(objReport.MaHoatchat);
                    txtTenHoatChat.Text = Utility.sDbnull(objReport.TenHoatchat);
                    txtThuTu.Text = Utility.sDbnull(objReport.SttHthi);
                    chkhienthi.Checked = Utility.Int32Dbnull(objReport.TrangThai) == 1; 
                    txtduongdung.SetCode( Utility.sDbnull(objReport.MaDuongdung));
                    txtmota.Text = Utility.sDbnull(objReport.MoTa);

                }


            }
            catch (Exception ex) { }
        }


        private Query query = DmucHoatchat.CreateQuery();

        void getSTT()
        {
            txtThuTu.Text = (Utility.Int32Dbnull(query.GetMax("stt_hthi"), 0) + 1).ToString();
        }


        private bool CheckValidData()
        {

            if (string.IsNullOrEmpty(txtMaHoatChat.Text))
            {
                Utility.ShowMsg("Chưa nhập mã hoạt chất", "Thông báo", MessageBoxIcon.Warning);
                txtMaHoatChat.Focus();
                txtMaHoatChat.SelectAll();
                return false;
            }
            if (string.IsNullOrEmpty(txtTenHoatChat.Text))
            {
                Utility.ShowMsg("Bạn phải nhập tiêu đề đường dùng", "Thông báo", MessageBoxIcon.Warning);
                txtTenHoatChat.Focus();
                txtTenHoatChat.SelectAll();
                return false;
            }


            //SqlQuery sqlQuery = new Select().From<DmucHoatchat>().Where(DmucHoatchat.Columns.MaHoatChat).IsEqualTo(txtMaHoatChat.Text);
            //if (em_action == action.Update)
            //{
            //    sqlQuery.And(DmucHoatchat.Columns.IDmucHoatchat).IsNotEqualTo(txtIDmucHoatchat.Text);
            //}
            //if (sqlQuery.GetRecordCount() > 0)
            //{
            //    Utility.ShowMsg("Đã tồn tại mã đương dùng\n Mời bạn xem lại", "Thông báo", MessageBoxIcon.Error);
            //    txtMaHoatChat.Focus();
            //    txtMaHoatChat.SelectAll();
            //    return false;
            //}
            return true;

        }

        public DataTable p_dtHoatChat;
        private void InsertData()
        {
            try
            {
                // objReport.ThuMuc = Utility.sDbnull(txtThuMuc.Text);
                DmucHoatchat objReport = new DmucHoatchat();// obj
                objReport.TenHoatchat = txtTenHoatChat.Text;
                objReport.MaHoatchat = txtMaHoatChat.Text;
                objReport.SttHthi = Utility.Int16Dbnull(txtThuTu.Value);
                objReport.TrangThai = chkhienthi.Checked;
                objReport.MaDuongdung = Utility.sDbnull(txtduongdung.MyCode);
                objReport.MoTa = Utility.sDbnull(txtmota.Text);
                objReport.IsNew = true;
                objReport.Save();
                em_action = action.Update;
                txtMaHoatChat.Enabled = false;
                txtIdHoatChat.Text = Utility.sDbnull(objReport.IdHoatchat);
                DataRow dr = p_dtHoatChat.NewRow();
                Utility.FromObjectToDatarow(objReport, ref dr);
                p_dtHoatChat.Rows.Add(dr);

                Utility.SetMsg(lblMessage, "Bạn thêm thành công", true);
                if (grdList != null)
                {
                    Utility.GonewRowJanus(grdList, DmucHoatchat.Columns.IdHoatchat, txtIdHoatChat.Text);
                }
                if (!_bAcceptClose) Clear();
            }
            catch (Exception ex) { }
        }

        private void UpdateData()
        {
            try
            {
                //objReport.ThuMuc = Utility.sDbnull(txtThuMuc.Text);
                DmucHoatchat objReport = new DmucHoatchat();// obj
                if (em_action == action.Update)
                {
                    objReport.MarkOld();
                    objReport.IsNew = false;
                    objReport.IsLoaded = true;
                    objReport.IdHoatchat = Utility.Int16Dbnull(txtIdHoatChat.Text);
                    objReport.MaHoatchat = Utility.sDbnull(txtMaHoatChat.Text);
                }

                objReport.TenHoatchat = txtTenHoatChat.Text;
                objReport.MaHoatchat = txtMaHoatChat.Text;
                objReport.SttHthi = Utility.Int16Dbnull(txtThuTu.Value);
                objReport.TrangThai = chkhienthi.Checked;
                objReport.MaDuongdung = Utility.sDbnull(txtduongdung.MyCode);
                objReport.MoTa = Utility.sDbnull(txtmota.Text);
                objReport.MarkOld();
                objReport.IsNew = false;
                objReport.Save();
                //new Update(LDrug.Schema)
                //.Set(LDrug.Columns.MaBoYTe).EqualTo(txtMaHoatChat.Text)
                //.Where(LDrug.Columns.DrugId).IsEqualTo(Utility.Int32Dbnull(txt))
                if (p_dtHoatChat != null)
                {
                    var query = from p in p_dtHoatChat.AsEnumerable()
                                where Utility.sDbnull(p[DmucHoatchat.Columns.IdHoatchat]) == Utility.sDbnull(objReport.IdHoatchat)
                                select p;
                    if (query.Any())
                    {
                        var firstrow = query.FirstOrDefault();
                        if (firstrow != null) firstrow.Delete();
                        p_dtHoatChat.AcceptChanges();
                    }
                    DataRow dr = p_dtHoatChat.NewRow();
                    Utility.FromObjectToDatarow(objReport, ref dr);

                    p_dtHoatChat.Rows.Add(dr);

                    if (grdList != null)
                    {
                        Utility.GonewRowJanus(grdList, DmucHoatchat.Columns.IdHoatchat, txtIdHoatChat.Text);
                    }
                    Utility.ShowMsg("Bạn thực hiện sửa thông tin thành công", "Thông báo", MessageBoxIcon.Information);
                    this.Close();
                }

            }
            catch (Exception ex) { }
        }

        private void Clear()
        {
            foreach (Control ctr in grpControl.Controls)
            {
                if (ctr is Janus.Windows.GridEX.EditControls.EditBox)
                {
                    ctr.Text = "";
                }
            }
            lblMessage.Visible = false;
            em_action = action.Insert;
            objReport = new DmucHoatchat();
            objReport = null;
            getSTT();
            GetData();
            txtMaHoatChat.Enabled = true;
            txtMaHoatChat.Focus();

        }


        private void PerformAction()
        {
            switch (em_action)
            {
                case action.Insert:
                    InsertData();
                    break;
                case action.Update:
                    UpdateData();
                    break;
            }
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (CheckValidData())
                PerformAction();
        }



        private void frm_Add_SysReport_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.Enter) ProcessTabKey(true);
            if (e.KeyCode == Keys.S && e.Control) cmdSave.PerformClick();

        }

        private void frm_themmoi_hoatchat_Load(object sender, EventArgs e)
        {
            txtduongdung.Init();
            getSTT();
            GetData();
            txtMaHoatChat.Focus();
            //Utility.SetMessageError(errorProvider1, txtMaHoatChat, "Nhập mã hoạt chất");
            //Utility.SetMessageError(errorProvider2, txtTenHoatChat, "Nhập tên hoạt chất");

        }
        private void Checkcontrol()
        {
            Utility.ResetMessageError(errorProvider1);
            Utility.ResetMessageError(errorProvider2);
            Utility.ResetMessageError(errorProvider3);
            //if (string.IsNullOrEmpty(txtMaHoatChat.Text))
            //{
            //    Utility.SetMessageError(errorProvider1, txtMaHoatChat, "Nhập mã hoạt chất");
            //}
            //if (string.IsNullOrEmpty(txtTenHoatChat.Text))
            //{
            //    Utility.SetMessageError(errorProvider2, txtTenHoatChat, "Nhập tên chất");
            //}

        }

        private void txtMaHoatChat_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Checkcontrol();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void txtTenHoatChat_TextChanged(object sender, EventArgs e)
        {
            try
            {
                Checkcontrol();
            }
            catch (Exception)
            {

                throw;
            }
        }



    }

}
