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
namespace VNS.HIS.UI.DANHMUC
{
    public partial class frm_themmoi_doituongBHYT : Form
    {
        #region "Declare Variable Form"
        public DataTable p_dtDataInsuranceObjects=new DataTable();
        public action em_Action = action.Insert;
        private DataTable m_dtGroupInsurance = new DataTable();
        private DataTable m_dtObjectType = new DataTable();
        #endregion

        #region "Contructor"
        public frm_themmoi_doituongBHYT()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
        }
        #endregion
        #region "Method of Event Form"
        /// <summary>
        /// hàm thực hiện laod thông tin cảu Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_themmoi_doituongBHYT_Load(object sender, EventArgs e)
        {
            InitalData();
            if (em_Action == action.Update) GetData();
            if (em_Action == action.Insert)
            {
                txtintOrder.Value = Utility.DecimaltoDbnull(_Query.GetMax(DmucDoituongbhyt.Columns.SttHthi), 0) + 1;
            }
            SetStatusMessage();

        }
        /// <summary>
        /// hàm thực hiện dùng phím tắt trên Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_themmoi_doituongBHYT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.Control && e.KeyCode == Keys.S) cmdSave.PerformClick();
        }
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            PerformAction();
        }
        #endregion

        #region "Method of common"

        public bool b_Cancel = false;

        private void PerformAction()
        {
            switch (em_Action)
            {
                case action.Insert:
                    if (!InValiData()) return;
                    PerformActionInsert();
                    break;
                case action.Update:
                    if (!InValiData()) return;
                    PerformActionUpdate();
                    break;
            }

        }
        /// <summary>
        /// hàm thực hiện khởi tạo thông tin 
        /// </summary>
        private void InitalData()
        {
            m_dtGroupInsurance = THU_VIEN_CHUNG.LayDulieuDanhmucChung("NHOMBHYT",true);
               
            m_dtObjectType =
                new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.Columns.IdLoaidoituongKcb).IsEqualTo(0).
                    ExecuteDataSet().Tables[0];

            DataBinding.BindData(cboInsuranceGroupID, m_dtGroupInsurance, DmucChung.Columns.Ma, DmucChung.Columns.Ten);
            DataBinding.BindData(cboObjectTypeID, m_dtObjectType, DmucDoituongkcb.Columns.IdDoituongKcb, DmucDoituongkcb.Columns.TenDoituongKcb);
            DataTable mdtMaQuyenLoi = THU_VIEN_CHUNG.LayDulieuDanhmucChung("MAQUYENLOIBHYT", true);
           Utility.SetDataSourceForDataGridEx(chkMaQuyenLoi,mdtMaQuyenLoi,false,true,"1=1","");
            
        }
        private Query _Query = DmucDoituongbhyt.CreateQuery();
        public Janus.Windows.GridEX.GridEX grdList;
        private void PerformActionInsert()
        {

            try
            {

                var query = (from lox in chkMaQuyenLoi.GetCheckedRows().AsEnumerable()
                             let y = Utility.sDbnull(lox.Cells[DmucChung.Columns.Ma].Value)
                            select y).ToArray();
                string MaQuyenLoi = string.Join(";", query);


                DmucDoituongbhyt objDmucDoituongbhyt=new DmucDoituongbhyt();
                objDmucDoituongbhyt.MaDoituongbhyt = Utility.sDbnull(txtInsObjectCode.Text, "");
                objDmucDoituongbhyt.TenDoituongbhyt = txtInsObjectName.Text;
                objDmucDoituongbhyt.MaNhombhyt = Utility.sDbnull(cboInsuranceGroupID.SelectedValue, -1);
                objDmucDoituongbhyt.IdDoituongKcb = Utility.Int16Dbnull(cboObjectTypeID.SelectedValue, -1);
                objDmucDoituongbhyt.SttHthi = Utility.Int16Dbnull(txtintOrder.Text);
                objDmucDoituongbhyt.PhantramBhyt = Utility.DecimaltoDbnull(nmPercent.Value, 0);
                objDmucDoituongbhyt.MotaThem = txtsDesc.Text;
                objDmucDoituongbhyt.LaygiaChung = Utility.Bool2byte(chkTinhgiachung.Checked);
                objDmucDoituongbhyt.DanhsachQuyenloi = MaQuyenLoi;
                objDmucDoituongbhyt.IsNew = true;
                objDmucDoituongbhyt.Save();
                objDmucDoituongbhyt.IdDoituongbhyt =Utility.Int16Dbnull(_Query.GetMax(DmucDoituongbhyt.Columns.IdDoituongbhyt), -1);
                txtInsObject_ID.Text = Utility.sDbnull(objDmucDoituongbhyt.IdDoituongbhyt);
                DmucDoituongbhyt objInsuranceObject1 =
                    DmucDoituongbhyt.FetchByID(Utility.Int32Dbnull(txtInsObject_ID.Text, -1));
                if(objInsuranceObject1!=null)
                {
                    DataRow dr = p_dtDataInsuranceObjects.NewRow();
                    dr[DmucDoituongbhyt.Columns.IdDoituongbhyt] = Utility.Int32Dbnull(_Query.GetMax(DmucDoituongbhyt.Columns.IdDoituongbhyt), -1);
                    dr[DmucDoituongbhyt.Columns.MaDoituongbhyt] = txtInsObjectCode.Text;
                    dr[DmucDoituongbhyt.Columns.TenDoituongbhyt] = Utility.sDbnull(txtInsObjectName.Text, "");
                    dr[DmucDoituongbhyt.Columns.LaygiaChung] = Utility.Bool2byte(chkTinhgiachung.Checked);
                    dr[DmucDoituongbhyt.Columns.IdDoituongKcb] = Utility.Int32Dbnull(cboObjectTypeID.SelectedValue, -1);
                    dr[DmucDoituongbhyt.Columns.MotaThem] = Utility.sDbnull(txtsDesc.Text, "");
                    dr[DmucDoituongbhyt.Columns.PhantramBhyt] = Utility.DecimaltoDbnull(nmPercent.Value, 0);
                    dr[DmucDoituongbhyt.Columns.DanhsachQuyenloi] = MaQuyenLoi;
                    dr["ten_nhombhyt"] = Utility.sDbnull(cboInsuranceGroupID.Text, "");
                    dr[DmucDoituongkcb.Columns.TenDoituongKcb] = cboObjectTypeID.Text;
                    dr[DmucDoituongbhyt.Columns.SttHthi] = txtintOrder.Text;
                    p_dtDataInsuranceObjects.Rows.InsertAt(dr, 0);
                    Utility.GonewRowJanus(grdList, DmucDoituongbhyt.Columns.IdDoituongbhyt, txtInsObject_ID.Text);
                   
                   // Utility.ShowMsg(string.Format("Bạn thêm mã đối tượng {0} thành công", txtInsObjectCode.Text));
                    em_Action = action.Insert;

                   
                    b_Cancel = true;
                    this.Close();
                }
            
            }
            catch (Exception exception)
            {
                
                
            }
           
        }
        void ResetControl()
        {
            try
            {
                txtInsObject_ID.Clear();
                txtInsObjectCode.Clear();
            }
            catch
            {
            }
        }
        /// <summary>
        /// /hàm thực heien thông tin update thông tin lại
        /// </summary>
        private void PerformActionUpdate()
        {
            try
            {


                var query = (from lox in chkMaQuyenLoi.GetCheckedRows().AsEnumerable()
                             let y = Utility.sDbnull(lox.Cells[DmucChung.Columns.Ma].Value)
                             select y).ToArray();
                string MaQuyenLoi = string.Join(";", query);
                int record = new Update(DmucDoituongbhyt.Schema)
                .Set(DmucDoituongbhyt.Columns.DanhsachQuyenloi).EqualTo(MaQuyenLoi)
                .Set(DmucDoituongbhyt.Columns.MaDoituongbhyt).EqualTo(Utility.sDbnull(txtInsObjectCode.Text, ""))
                .Set(DmucDoituongbhyt.Columns.TenDoituongbhyt).EqualTo(txtInsObjectName.Text)
                .Set(DmucDoituongbhyt.Columns.SttHthi).EqualTo(Utility.Int16Dbnull(txtintOrder.Value, 1))
                .Set(DmucDoituongbhyt.Columns.IdDoituongKcb).EqualTo(Utility.Int16Dbnull(cboObjectTypeID.SelectedValue, -1))
                .Set(DmucDoituongbhyt.Columns.MotaThem).EqualTo(txtsDesc.Text)
                .Set(DmucDoituongbhyt.Columns.LaygiaChung).EqualTo(Utility.Bool2byte(chkTinhgiachung.Checked))
                .Set(DmucDoituongbhyt.Columns.PhantramBhyt).EqualTo(Utility.DecimaltoDbnull(nmPercent.Value, 0))
                 .Set(DmucDoituongbhyt.Columns.MaNhombhyt).EqualTo(Utility.sDbnull(cboInsuranceGroupID.SelectedValue, "-1"))
                .Where(DmucDoituongbhyt.Columns.IdDoituongbhyt).IsEqualTo(Utility.DecimaltoDbnull(txtInsObject_ID.Text, -1)).Execute();

                if (record > 0)
                {
                    DataRow[] dr = p_dtDataInsuranceObjects.Select(DmucDoituongbhyt.Columns.IdDoituongbhyt+ "=" + Utility.Int32Dbnull(txtInsObject_ID.Text, -1));
                    if (dr.GetLength(0) > 0)
                    {
                        dr[0][DmucDoituongbhyt.Columns.MaNhombhyt] = Utility.sDbnull(cboInsuranceGroupID.SelectedValue, "-1");
                        dr[0][DmucDoituongbhyt.Columns.MaDoituongbhyt] = txtInsObjectCode.Text;
                        dr[0][DmucDoituongkcb.Columns.TenDoituongKcb] = cboObjectTypeID.Text;
                        dr[0][DmucDoituongbhyt.Columns.LaygiaChung] = Utility.Bool2byte(chkTinhgiachung.Checked);
                        dr[0][DmucDoituongbhyt.Columns.PhantramBhyt] = Utility.DecimaltoDbnull(nmPercent.Value, 0);
                        dr[0][DmucDoituongbhyt.Columns.TenDoituongbhyt] = Utility.sDbnull(txtInsObjectName.Text, "");
                        dr[0][DmucDoituongbhyt.Columns.SttHthi] = Utility.Int16Dbnull(txtintOrder.Text, 0);
                        dr[0][DmucDoituongbhyt.Columns.MotaThem] = Utility.sDbnull(txtsDesc.Text, "");
                        dr[0][DmucDoituongbhyt.Columns.IdDoituongKcb] = Utility.Int16Dbnull(cboObjectTypeID.SelectedValue, -1);
                        dr[0]["ten_nhombhyt"] = Utility.sDbnull(cboInsuranceGroupID.Text, "");
                        dr[0][DmucDoituongbhyt.Columns.DanhsachQuyenloi] = MaQuyenLoi;
                    }
                    p_dtDataInsuranceObjects.AcceptChanges();
                    b_Cancel = true;
                    //Utility.ShowMsg(string.Format("Bạn sửa mã đối tượng {0} thành công", txtInsObjectCode.Text));
                    this.Close();
                }
                else
                {
                    Utility.ShowMsg("Lỗi trong quá trình cập nhập dữ liệu");
                    return;
                }
            }catch(Exception exception)
            {
                
            }
        }
        /// <summary>
        /// hàm thực hiện kiểm tra thông tin của phần mã tham gia đối tượng bảo hiểm
        /// </summary>
        /// <returns></returns>
        private bool InValiData()
        {

            if (string.IsNullOrEmpty(txtInsObjectCode.Text))
            {
                Utility.SetMsgError(errorProvider1, txtInsObjectCode, "Bạn phải nhập mã đối tượng tham gia bảo hiểm");
                txtInsObjectCode.Focus();
                return false;

            }
            if (string.IsNullOrEmpty(txtInsObjectName.Text))
            {
                Utility.SetMsgError(errorProvider2, txtInsObjectName, "Bạn phải nhập tên KCBBĐ");
                txtInsObjectName.Focus();
                return false;

            }
            if (cboObjectTypeID.SelectedIndex <= -1)
            {
                Utility.SetMsgError(errorProvider4, cboObjectTypeID, "Bạn phải chọn nhóm đối tượng ");
                cboObjectTypeID.Focus();
                return false;
            }
            if (cboInsuranceGroupID.SelectedIndex <= -1)
            {
                Utility.SetMsgError(errorProvider3, cboInsuranceGroupID, "Bạn phải chọn nhóm đối tượng bảo hiểm");
                cboInsuranceGroupID.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(nmPercent.Value)<=0)
            {
                Utility.ShowMsg("Phần trăm (%) BH phải > 0", "Thông báo tồn tại", MessageBoxIcon.Warning);
                nmPercent.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(nmPercent.Value) >100)
            {
                Utility.ShowMsg("Phần trăm (%) BH phải <= 100", "Thông báo tồn tại", MessageBoxIcon.Warning);
                nmPercent.Focus();
                return false;
            }
            SqlQuery q = new Select().From(DmucDoituongbhyt.Schema)
                .Where(DmucDoituongbhyt.Columns.MaDoituongbhyt).IsEqualTo(txtInsObjectCode.Text);
            if (em_Action == action.Update)
            {
                q.And(DmucDoituongbhyt.Columns.IdDoituongbhyt).IsNotEqualTo(Utility.Int32Dbnull(txtInsObject_ID.Text, -1));
            }
            if (q.GetRecordCount()>0)
            {
                Utility.ShowMsg("Đã tồn tại mã tham gia bảo hiểm","Thông báo tồn tại",MessageBoxIcon.Warning);
                txtInsObjectCode.Focus();
                return false;
            }
            SqlQuery q1 = new Select().From(DmucDoituongbhyt.Schema)
              .Where(DmucDoituongbhyt.Columns.TenDoituongbhyt).IsEqualTo(txtInsObjectName.Text);
            if(em_Action==action.Update)
            {
                q1.And(DmucDoituongbhyt.Columns.IdDoituongbhyt).IsNotEqualTo(Utility.Int32Dbnull(txtInsObject_ID.Text, -1));
            }
            if (q1.GetRecordCount() > 0)
            {
                Utility.ShowMsg("Đã tồn tại tên đối tượng tham gia bảo hiểm này", "Thông báo tồn tại", MessageBoxIcon.Warning);
                txtInsObjectName.Focus();
                //return false;
            }
            return true;
        }
      
        /// <summary>
        /// xét thông tin của các điều kiện cần nhập
        /// </summary>
        private void SetStatusMessage()
        {
          
            if(string.IsNullOrEmpty(txtInsObjectCode.Text))
            {
                Utility.SetMsgError(errorProvider1,txtInsObjectCode,"Bạn nhập mã tham gia bảo hiểm");
                txtInsObjectCode.Focus();
            }
            if (string.IsNullOrEmpty(txtInsObjectName.Text))
            {
                Utility.SetMsgError(errorProvider2, txtInsObjectName, "Bạn nhập tên tham gia bảo hiểm");
                txtInsObjectName.Focus();
            }
            if (cboObjectTypeID.SelectedIndex<=-1)
            {
                Utility.SetMsgError(errorProvider3, cboObjectTypeID, "Bạn chọn đối tượng");
                cboObjectTypeID.Focus();
            }
            if (cboInsuranceGroupID.SelectedIndex <= -1)
            {
                Utility.SetMsgError(errorProvider3, cboInsuranceGroupID, "Bạn chọn nhóm đối tượng tham gia bảo hiểm");
                cboInsuranceGroupID.Focus();
            }
            
        }
        /// <summary>
        /// lấy thông tin của khi load sửa thông tin của Form
        /// </summary>
        private void GetData()
        {
            DmucDoituongbhyt ObjInsuranceObject = DmucDoituongbhyt.FetchByID(Utility.Int32Dbnull(txtInsObject_ID.Text, -1));
            
            if (ObjInsuranceObject != null)
            {
                txtInsObjectCode.Text = ObjInsuranceObject.MaDoituongbhyt;
                txtInsObjectName.Text = ObjInsuranceObject.TenDoituongbhyt;
                chkTinhgiachung.Checked = Utility.Byte2Bool(ObjInsuranceObject.LaygiaChung);
                txtsDesc.Text = ObjInsuranceObject.MotaThem;
                txtintOrder.Text = ObjInsuranceObject.SttHthi.ToString();
                cboObjectTypeID.SelectedIndex = Utility.GetSelectedIndex(cboObjectTypeID,
                                                                         ObjInsuranceObject.IdDoituongKcb.ToString());
                cboInsuranceGroupID.SelectedIndex = Utility.GetSelectedIndex(cboInsuranceGroupID,
                                                                             ObjInsuranceObject.MaNhombhyt.
                                                                                 ToString());
                nmPercent.Value = Utility.Int32Dbnull(ObjInsuranceObject.PhantramBhyt,0);
                string maQUyenLoi = Utility.sDbnull(ObjInsuranceObject.DanhsachQuyenloi);
                string []arrMaQuyenLoi = maQUyenLoi.Split(';');
                foreach (Janus.Windows.GridEX.GridEXRow gridExRow in chkMaQuyenLoi.GetDataRows())
                {
                    var query = from quyen in arrMaQuyenLoi.AsEnumerable()
                                where quyen == Utility.sDbnull(gridExRow.Cells[DmucChung.Columns.Ma].Value)
                                select quyen;
                    if(query.Count()>0)
                        gridExRow.IsChecked = true;
                    else
                    {
                        gridExRow.IsChecked = false;
                    }

                }
            }
        }
        #endregion
      
    }
}
