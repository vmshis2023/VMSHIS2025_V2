using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Janus.Windows.GridEX;
using SubSonic;
using VNS.Libs;
using VMS.HIS.DAL;

namespace VNS.HIS.UI.DANHMUC
{
    public partial class frm_dmuc_doituongBHYT : Form
    {
        #region "Declare Variable private"

        private DataTable m_dtInsurance = new DataTable();
        private DataTable m_dtGroupInsurance = new DataTable();
        private DataTable m_dtObjectType = new DataTable();
        private action m_enAction = action.FirstOrFinished;
        #endregion

        #region "Contructor"

        public bool b_Cancel = false;
        public string InsObjectCode = "";
        DataTable dtQhe = new DataTable();
        public frm_dmuc_doituongBHYT()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(frm_ListDisease_KeyDown);
            grd_Insurance_Objects.SelectionChanged += new EventHandler(grd_Insurance_Objects_SelectionChanged);
            grdQhe.UpdatingCell += grdQhe_UpdatingCell;
            InitEvents();
        }

        void grdQhe_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grd_Insurance_Objects)) return;
                string madoituongBHYT = Utility.sDbnull(grd_Insurance_Objects.CurrentRow.Cells[DmucDoituongbhyt.Columns.MaDoituongbhyt].Value, "");
                if (e.Column.Key == QheDautheQloiBhyt.Columns.MaQloi)
                {
                    string maqloicu = e.InitialValue.ToString();
                    string maqloimoi = e.Value.ToString();
                    int ptramBHYT = Utility.Int32Dbnull(e.Value);
                    //Kiểm tra
                    if (dtQhe.Select(QheDautheQloiBhyt.Columns.MaDoituongbhyt+"='" + madoituongBHYT + "' AND "+QheDautheQloiBhyt.Columns.MaQloi+"=" + maqloimoi).Length > 0)
                    {
                        Utility.ShowMsg(string.Format("Mã đầu thẻ BHYT: {0} với mã quyền lợi: {1} đã có. Bạn cần nhập mã quyền lợi khác", madoituongBHYT, maqloimoi));
                        e.Value = e.InitialValue;
                        return;
                    }
                    new Update(QheDautheQloiBhyt.Schema)
                        .Set(QheDautheQloiBhyt.Columns.MaQloi).EqualTo(maqloimoi)
                        .Where(QheDautheQloiBhyt.Columns.MaDoituongbhyt).IsEqualTo(madoituongBHYT)
                        .And(QheDautheQloiBhyt.Columns.MaQloi).IsEqualTo(maqloicu)
                        .Execute();
                    dtQhe.AcceptChanges();
                    grdQhe.Refetch();
                }
                if (e.Column.Key == QheDautheQloiBhyt.Columns.PhantramBhyt)
                {
                    string maqloi = Utility.sDbnull(grdQhe.CurrentRow.Cells[QheDautheQloiBhyt.Columns.MaQloi].Value, "");
                    int ptramBHYT = Utility.Int32Dbnull(e.Value);
                    new Update(QheDautheQloiBhyt.Schema)
                        .Set(QheDautheQloiBhyt.Columns.PhantramBhyt).EqualTo(ptramBHYT)
                        .Where(QheDautheQloiBhyt.Columns.MaDoituongbhyt).IsEqualTo(madoituongBHYT)
                        .And(QheDautheQloiBhyt.Columns.MaQloi).IsEqualTo(maqloi)
                        .Execute();
                    dtQhe.AcceptChanges();
                    grdQhe.Refetch();
                }

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }

        void InitEvents()
        {
            cmdInsert.Click += cmdInsert_Click;
            cmdSave.Click += cmdSave_Click;
            cmdXoaquyenloi.Click += cmdXoaquyenloi_Click;
        }
        string getCheckedvalue(GridEX grd, string fieldName)
        {
            string reval = "";
            try
            {
                foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grd.GetCheckedRows())
                {
                    reval += Utility.sDbnull(gridExRow.Cells[fieldName].Value, "") + ",";
                }
                if (reval != "") reval= reval.Substring(0, reval.Length - 1);
                return reval;
            }
            catch (Exception)
            {

                return "";
            }
        }
        void cmdXoaquyenloi_Click(object sender, EventArgs e)
        {
            if (Utility.isValidGrid(grd_Insurance_Objects) && Utility.isValidGrid(grdQhe) && grdQhe.GetCheckedRows().Length > 0)
            {
                if (Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa danh mục quyền lợi đang chọn?", "Xác nhận xóa", true))
                {
                    string ma_qloi = getCheckedvalue(grdQhe,QheDautheQloiBhyt.Columns.MaQloi);
                    SPs.QheXoaqhedoituongbhytMaquyenloi(Utility.GetValueFromGridColumn(grd_Insurance_Objects, DmucDoituongbhyt.Columns.MaDoituongbhyt), ma_qloi).Execute();
                     grd_Insurance_Objects_SelectionChanged(grd_Insurance_Objects, e);
                }
            }
        }

        void cmdUpdate_Click(object sender, EventArgs e)
        {
            
        }

        void cmdSave_Click(object sender, EventArgs e)
        {
            
        }

        void cmdInsert_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grd_Insurance_Objects)) return;
            frm_ChonQloiBHYT _ChonQloiBHYT = new frm_ChonQloiBHYT(Utility.GetValueFromGridColumn(grd_Insurance_Objects, DmucDoituongbhyt.Columns.MaDoituongbhyt));
            _ChonQloiBHYT.m_dtqhe = dtQhe;
            if (_ChonQloiBHYT.ShowDialog() == System.Windows.Forms.DialogResult.OK) 
            {
                grd_Insurance_Objects_SelectionChanged(grd_Insurance_Objects, e);
            }
        }
      
        #endregion

        #region "Method of Common"

        /// <summary>
        /// hàm thực hiện khởi tạo thông tin của Form
        /// </summary>
        private void InitData()
        {
            m_dtGroupInsurance = THU_VIEN_CHUNG.LayDulieuDanhmucChung("NHOMBHYT", true);
            m_dtObjectType =
                new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.Columns.IdLoaidoituongKcb).IsEqualTo(0).
                    ExecuteDataSet().Tables[0];

            DataBinding.BindDataCombox(cboGroupInsurance, m_dtGroupInsurance, DmucChung.Columns.Ma,
                                       DmucChung.Columns.Ten);
            DataBinding.BindDataCombox(cboObjectType_ID, m_dtObjectType, DmucDoituongkcb.Columns.IdDoituongKcb,
                                       DmucDoituongkcb.Columns.TenDoituongKcb);
        }

        /// <summary>
        /// hàm thực hiện trạng thais của các nút trong form
        /// </summary>
        private void ModifyCommand()
        {
            try
            {
                cmdEdit.Enabled =Utility.isValidGrid(grd_Insurance_Objects);
                cmdDelete.Enabled = Utility.isValidGrid(grd_Insurance_Objects);
                cmdSaveAll.Enabled = Utility.isValidGrid(grd_Insurance_Objects) && Utility.isValidGrid(grdQhe);
                cmdInsert.Enabled = Utility.isValidGrid(grd_Insurance_Objects);
                cmdXoaquyenloi.Enabled = Utility.isValidGrid(grd_Insurance_Objects) && Utility.isValidGrid(grdQhe);
                cmdSave.Enabled = Utility.isValidGrid(grd_Insurance_Objects) && Utility.isValidGrid(grdQhe);
            }
            catch (Exception)
            {


            }


        }

        /// <summary>
        /// hàm tìm kiếm thông tin của Form
        /// </summary>
        private void Search()
        {
            try
            {
                SqlQuery q = new Select().From(VDmucDoituongbhyt.Schema);
                q.Where("1=1");
                if (!string.IsNullOrEmpty(txtInsObject_Code.Text))
                {
                    q.And(VDmucDoituongbhyt.Columns.MaDoituongbhyt).Like("%" + txtInsObject_Code.Text + "%");
                }
                if (!string.IsNullOrEmpty(txtDieaseName.Text))
                {
                    q.And(VDmucDoituongbhyt.Columns.TenDoituongbhyt).Like("%" + txtDieaseName.Text + "%");
                }
                if (cboObjectType_ID.SelectedIndex > 0)
                {
                    q.And(VDmucDoituongbhyt.Columns.IdDoituongKcb).IsEqualTo(
                        Utility.Int32Dbnull(cboObjectType_ID.SelectedValue, -1));
                }
                if (cboGroupInsurance.SelectedIndex > 0)
                {
                    q.And(VDmucDoituongbhyt.Columns.MaNhombhyt).IsEqualTo(
                        Utility.sDbnull(cboGroupInsurance.SelectedValue, "-1"));
                }
                q.OrderAsc(VDmucDoituongbhyt.Columns.SttHthi);
                m_dtInsurance = q.ExecuteDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx(grd_Insurance_Objects, m_dtInsurance, true, true, "", "");
            }
            catch (Exception)
            {


            }

            // grd_Insurance_Objects.DataSource = m_dtInsurance;
        }

        #endregion

        #region "Method Of Form(Handler Form)"

        private void frm_ListDisease_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) toolStripButton1.PerformClick();
            if (e.KeyCode == Keys.F3) cmdSearch.PerformClick();
            if (e.Control && e.KeyCode == Keys.N) cmdNew.PerformClick();
            if (e.Control && e.KeyCode == Keys.E) cmdEdit.PerformClick();
            if (e.Control && e.KeyCode == Keys.D) cmdDelete.PerformClick();
        }

        /// <summary>
        /// hàm thực hiện đóng Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// hàm thực hiện sự kiện tìm kiếm thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            Search();
            ModifyCommand();
        }

        /// <summary>
        /// hàm thực hiện load thông tin của Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_dmuc_doituongBHYT_Load(object sender, EventArgs e)
        {
            InitData();
            Search();
            ModifyCommand();
        }

        /// <summary>
        /// hàm thực hiện xóa thông tin bản ghi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (grd_Insurance_Objects.CurrentRow != null)
            {
                if (Utility.AcceptQuestion("Bạn có muốn xóa thông tin bản ghi đang chọn hay không", "Thông báo", true))
                {
                    int InsObjectId = Utility.Int32Dbnull(grd_Insurance_Objects.GetValue(DmucDoituongbhyt.Columns.IdDoituongbhyt), -1);
                    int record = new Delete().From(DmucDoituongbhyt.Schema)
                        .Where(DmucDoituongbhyt.Columns.IdDoituongbhyt).IsEqualTo(InsObjectId).Execute();
                    if (record > 0)
                    {
                        //Utility.ShowMsg("Bạn xóa bản ghi thành công", "Thông báo");
                        grd_Insurance_Objects.CurrentRow.Delete();
                        grd_Insurance_Objects.UpdateData();
                        grd_Insurance_Objects.Refresh();
                        m_dtInsurance.AcceptChanges();
                    }
                    else
                    {
                        Utility.ShowMsg("Lỗi trong quá trình xóa thông tin");
                    }
                }
            }
        }

        /// <summary>
        /// hàm thực hiện thêm mới đối tượng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdNew_Click(object sender, EventArgs e)
        {
            try
            {
                frm_themmoi_doituongBHYT frm = new frm_themmoi_doituongBHYT();
                frm.grdList = grd_Insurance_Objects;
                frm.em_Action = action.Insert;
                frm.p_dtDataInsuranceObjects = m_dtInsurance;
                frm.ShowDialog();
                if (frm.b_Cancel)
                {
                    b_Cancel = frm.b_Cancel;
                    InsObjectCode = Utility.sDbnull(frm.txtInsObjectCode.Text);
                }

            }
            catch (Exception exception)
            {

            }

        }

        /// <summary>
        /// hàm thực hiện sửa thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdEdit_Click(object sender, EventArgs e)
        {
            try
            {
                frm_themmoi_doituongBHYT frm = new frm_themmoi_doituongBHYT();
                frm.grdList = grd_Insurance_Objects;
                frm.em_Action = action.Update;
                frm.p_dtDataInsuranceObjects = m_dtInsurance;
                frm.txtInsObject_ID.Text =
                    Utility.Int32Dbnull(grd_Insurance_Objects.CurrentRow.Cells[DmucDoituongbhyt.Columns.IdDoituongbhyt].Value, -1).ToString();
                frm.ShowDialog();
                if (frm.b_Cancel)
                {
                    b_Cancel = frm.b_Cancel;
                    InsObjectCode = Utility.sDbnull(frm.txtInsObjectCode.Text);
                }
            }
            catch (Exception exception)
            {

            }

        }

        #endregion

        /// <summary>
        /// Lưu tất cả các thông tin được sửa trên lưới 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSaveAll_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRow drv in m_dtInsurance.Rows)
                {
                    new Update(DmucDoituongbhyt.Schema)
                        .Set(DmucDoituongbhyt.Columns.PhantramBhyt).EqualTo(
                            Utility.DecimaltoDbnull(drv[DmucDoituongbhyt.Columns.PhantramBhyt]))
                        .Set(DmucDoituongbhyt.Columns.MaDoituongbhyt).EqualTo(
                            Utility.sDbnull(drv[DmucDoituongbhyt.Columns.MaDoituongbhyt]))
                        .Set(DmucDoituongbhyt.Columns.TenDoituongbhyt).EqualTo(
                            Utility.sDbnull(drv[DmucDoituongbhyt.Columns.TenDoituongbhyt]))
                        .Set(DmucDoituongbhyt.Columns.MotaThem).EqualTo(
                            Utility.sDbnull(drv[DmucDoituongbhyt.Columns.MotaThem]))
                        .Where(DmucDoituongbhyt.Columns.IdDoituongbhyt).IsEqualTo(
                            Utility.Int32Dbnull(drv[DmucDoituongbhyt.Columns.IdDoituongbhyt])).Execute();

                }
                Utility.ShowMsg("Cập nhập thành công");
            }
            catch (Exception exception)
            {
                Utility.ShowMsg("Lỗi trong quá trình cập nhập thông tin");
                return;
            }

        }
        
        private void grd_Insurance_Objects_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                LoadDataQhe();
                ModifyCommand();
            }
            catch (Exception)
            {

                //  throw;
            }

        }
        void LoadDataQhe()
        {
            try
            {
                if (Utility.isValidGrid(grd_Insurance_Objects))
                {
                    string madoituongbhyt = Utility.sDbnull(grd_Insurance_Objects.CurrentRow.Cells[DmucDoituongbhyt.Columns.MaDoituongbhyt].Value, "");
                    dtQhe = new Select().From(QheDautheQloiBhyt.Schema).Where(QheDautheQloiBhyt.Columns.MaDoituongbhyt).IsEqualTo(madoituongbhyt).ExecuteDataSet().Tables[0];
                    grdQhe.DataSource = dtQhe;
                }
                else
                {
                    grdQhe.DataSource = null;
                }
            }
            catch (Exception ex)
            {
                
                
            }
        }
        /// <summary>
        /// sửa dữ liệu trực tiếp trên lưới
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grd_Insurance_Objects_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            try
            {
                int record = new Update(DmucDoituongbhyt.Schema)
                    .Set(DmucDoituongbhyt.Columns.MaDoituongbhyt).EqualTo(
                        Utility.sDbnull(grd_Insurance_Objects.CurrentRow.Cells[DmucDoituongbhyt.Columns.MaDoituongbhyt].Value, ""))
                    .Set(DmucDoituongbhyt.Columns.TenDoituongbhyt).EqualTo(
                        grd_Insurance_Objects.CurrentRow.Cells[DmucDoituongbhyt.Columns.TenDoituongbhyt].Value)
                    .Set(DmucDoituongbhyt.Columns.SttHthi).EqualTo(
                        Utility.Int16Dbnull(grd_Insurance_Objects.CurrentRow.Cells[DmucDoituongbhyt.Columns.SttHthi].Value, 1))
                    .Set(DmucDoituongbhyt.Columns.MotaThem).EqualTo(grd_Insurance_Objects.CurrentRow.Cells[KcbDonthuocChitiet.Columns.MotaThem].Value)
                    .Set(DmucDoituongbhyt.Columns.PhantramBhyt).EqualTo(
                        Utility.DecimaltoDbnull(grd_Insurance_Objects.CurrentRow.Cells[DmucDoituongbhyt.Columns.PhantramBhyt].Value, 0))
                    .Where(DmucDoituongbhyt.Columns.IdDoituongbhyt).IsEqualTo(
                        Utility.DecimaltoDbnull(grd_Insurance_Objects.CurrentRow.Cells[DmucDoituongbhyt.Columns.IdDoituongbhyt].Value, -1)).
                    Execute();

                if (record > 0)
                {

                    grd_Insurance_Objects.UpdateData();
                    grd_Insurance_Objects.Refresh();
                    m_dtInsurance.AcceptChanges();
                    Utility.ShowMsg("Bạn thực hiện cập nhập thông tin thành công");
                }
                else
                {
                    Utility.ShowMsg("Lỗi trong quá trình cập nhập dữ liệu");
                    return;
                }
            }
            catch (Exception exception)
            {

            }
        }
        /// <summary>
        /// Kiểm tra dữ liệu được sửa
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void grd_Insurance_Objects_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            var objValue = new object();
            if (e.Column.Key == DmucDoituongbhyt.Columns.PhantramBhyt)
            {
                objValue = e.Value;
                if (!SubSonic.Sugar.Numbers.IsNumber(Utility.sDbnull(objValue)))
                {
                    e.Cancel = true;
                    Utility.ShowMsg("Chỉ được phép nhập số");
                }
                else if (Utility.Int32Dbnull(objValue) > 100 || Utility.Int32Dbnull(objValue) < 0)
                {
                    e.Cancel = true;
                    Utility.ShowMsg("Giá trị nhập vào phải nằm trong khoảng 0-100");
                }
            }
            else if(e.Column.Key == DmucDoituongbhyt.Columns.MaDoituongbhyt)
            {
                objValue = e.Value;
                SqlQuery q = new Select().From(DmucDoituongbhyt.Schema)
                .Where(DmucDoituongbhyt.Columns.MaDoituongbhyt).IsEqualTo(Utility.sDbnull(objValue)).And(DmucDoituongbhyt.Columns.IdDoituongbhyt).IsNotEqualTo(Utility.Int32Dbnull(grd_Insurance_Objects.CurrentRow.Cells[DmucDoituongbhyt.Columns.IdDoituongbhyt].Value, -1));
                if (string.IsNullOrEmpty(Utility.sDbnull(objValue)))
                {
                    e.Cancel = true;
                    Utility.ShowMsg("Mã tham gia bảo hiểm không được để trống", "Thông báo tồn tại", MessageBoxIcon.Warning);
                }
                else if (q.GetRecordCount() > 0)
                {
                    e.Cancel = true;
                    Utility.ShowMsg("Đã tồn tại mã tham gia bảo hiểm", "Thông báo tồn tại", MessageBoxIcon.Warning);
                }
            }
            else if(e.Column.Key == DmucDoituongbhyt.Columns.TenDoituongbhyt)
            {
                objValue = e.Value;
                SqlQuery q = new Select().From(DmucDoituongbhyt.Schema)
                .Where(DmucDoituongbhyt.Columns.TenDoituongbhyt).IsEqualTo(Utility.sDbnull(objValue)).And(DmucDoituongbhyt.Columns.IdDoituongbhyt).IsNotEqualTo(Utility.Int32Dbnull(grd_Insurance_Objects.CurrentRow.Cells[DmucDoituongbhyt.Columns.IdDoituongbhyt].Value, -1));
                if (string.IsNullOrEmpty(Utility.sDbnull(objValue)))
                {
                    e.Cancel = true;
                    Utility.ShowMsg("Tên đối tượng tham gia bảo hiểm không được để trống", "Thông báo tồn tại", MessageBoxIcon.Warning);
                }
                else if (q.GetRecordCount() > 0)
                {
                    e.Cancel = true;
                    Utility.ShowMsg("Đã tồn tại tên đối tượng tham gia bảo hiểm này", "Thông báo tồn tại", MessageBoxIcon.Warning);
                }
            }
        }
    }
}
