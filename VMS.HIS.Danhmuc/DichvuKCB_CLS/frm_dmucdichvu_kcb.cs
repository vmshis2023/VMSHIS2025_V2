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
using VNS.HIS.NGHIEPVU;
using VMS.HIS.DAL;
using System.IO;



namespace VNS.HIS.UI.DANHMUC
{
    public partial class frm_dmucdichvu_kcb : Form
    {
        #region "Declare Variable private"
        private DataTable m_dtDmucDichvu_kcb=new DataTable();
        private DataTable m_dtGroupInsurance = new DataTable();
        private DataTable m_dtObjectType = new DataTable();
        bool m_blnLoaded = false;
        #endregion

        #region "Contructor"
        public frm_dmucdichvu_kcb()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            this.KeyPreview = true;
            this.KeyDown += new KeyEventHandler(frm_ListDisease_KeyDown);
            grdList.SelectionChanged+=new EventHandler(grd_Insurance_Objects_SelectionChanged);
            cboPhongBan.SelectedIndexChanged += new EventHandler(cboPhongBan_SelectedIndexChanged);
        }

        void cboPhongBan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_blnLoaded) return;
            BindRoomDept(Utility.Int32Dbnull(cboPhongBan.SelectedValue, -1));
        }
        #endregion
        #region "Method of Common"
        
        private DataTable dtDeptRoom = new DataTable();
        private DataTable dtDept = new DataTable();
       /// <summary>
       /// hà thực hiện khởi tạo thông tin của Form
       /// </summary>
        private void InitalData()
        {

            try
            {
                dtDept = THU_VIEN_CHUNG.Laydanhmuckhoa("NGOAI",0);
                //Khởi tạo danh mục Loại khám
                DataTable v_ExamTypeList = new DmucKieukhamCollection().Load().ToDataTable();
                Utility.AddColumnAlltoDataTable(ref v_ExamTypeList, DmucDichvukcb.Columns.IdKieukham, DmucKieukham.Columns.TenKieukham, "====Chọn====");
                v_ExamTypeList.DefaultView.Sort =DmucKieukham.Columns.SttHthi+ " ASC";
                cboLoaiKham.DataSource = v_ExamTypeList.DefaultView;
                cboLoaiKham.ValueMember = DmucDichvukcb.Columns.IdKieukham;
                cboLoaiKham.DisplayMember = DmucKieukham.Columns.TenKieukham;
                cboLoaiKham.SelectedValue = -1;
                //Khởi tạo danh mục Loại khám
                DataTable v_ObjectTypeList = new DmucDoituongkcbCollection().Load().ToDataTable();
                Utility.AddColumnAlltoDataTable(ref v_ObjectTypeList, DmucDichvukcb.Columns.IdDoituongKcb, DmucDoituongkcb.Columns.TenDoituongKcb, "====Chọn====");
                v_ObjectTypeList.DefaultView.Sort =DmucDoituongkcb.Columns.SttHthi+ " ASC";
                cboDoituong.DataSource = v_ObjectTypeList.DefaultView;
                cboDoituong.ValueMember = DmucDichvukcb.Columns.IdDoituongKcb;
                cboDoituong.DisplayMember = DmucDoituongkcb.Columns.TenDoituongKcb;
                //Phòng ban
                BindDepartment();

            }
            catch
            {
            }
        }
        private void BindStaffList(int departmentID)
        {
            try
            {
                
                DataTable _dsStaffList = THU_VIEN_CHUNG.Laydanhsachnhanvienthuockhoa(departmentID);
                Utility.AddColumnAlltoDataTable(ref _dsStaffList,DmucNhanvien.Columns.IdNhanvien , DmucNhanvien.Columns.TenNhanvien, "====Chọn====");
                _dsStaffList.DefaultView.Sort = DmucNhanvien.Columns.IdNhanvien+" ASC";
                cboBacsi.DataSource = _dsStaffList.DefaultView;
                cboBacsi.ValueMember = DmucNhanvien.Columns.IdNhanvien;
                cboBacsi.DisplayMember = DmucNhanvien.Columns.TenNhanvien;
                //DataBinding.BindData(cboBacsi, _dsStaffList, LStaff.Columns.StaffId, LStaff.Columns.StaffName);
            }
            catch
            {
            }

        }
        private void BindDepartment()
        {
            try
            {
               
                dtDeptRoom = dtDept.Copy();

                Utility.AddColumnAlltoDataTable(ref dtDept, DmucDichvukcb.Columns.IdKhoaphong,DmucKhoaphong.Columns.TenKhoaphong, "====Chọn====");

                cboPhongBan.DataSource = dtDept.DefaultView;
                cboPhongBan.ValueMember = DmucDichvukcb.Columns.IdKhoaphong;
                cboPhongBan.DisplayMember = DmucKhoaphong.Columns.TenKhoaphong;
                //dtDept.DefaultView.RowFilter = DmucKhoaphong.Columns.PhongChucnang + "=0 and " + DmucKhoaphong.Columns.KieuKhoaphong + " =KHOA and ma_cha=0";
                dtDept.DefaultView.Sort = DmucKhoaphong.Columns.SttHthi + " ASC";
                if (cboPhongBan.Items.Count >= 0)
                    BindRoomDept(Utility.Int32Dbnull(cboPhongBan.SelectedValue, -1));


            }
            catch (Exception)
            {
                
              
            }
           



        }
        private void BindRoomDept(int Dept_Id)
        {
            try
            {
                
                DataTable dataTable = THU_VIEN_CHUNG.Laydanhsachphongthuockhoa(Dept_Id,0);
                Utility.AddColumnAlltoDataTable(ref dataTable, DmucDichvukcb.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong, "====Chọn====");
                
                dataTable.DefaultView.Sort = DmucKhoaphong.Columns.TenKhoaphong + " ASC";
                cboRoomDept.DataSource = dataTable.DefaultView;
                cboRoomDept.ValueMember = DmucKhoaphong.Columns.IdKhoaphong;
                cboRoomDept.DisplayMember = DmucKhoaphong.Columns.TenKhoaphong;
            }
            catch
            {
            }
        }
        /// <summary>
        /// hàm thực hiện trạng thais của các nút trong form
        /// </summary>
        private void ModifyCommand()
        {
            if (!Utility.isValidGrid(grdList))
            {
                cmdEdit.Enabled = cmdDelete.Enabled = cmdSaveAll.Enabled = false;
            }
            else
            {
                cmdEdit.Enabled = cmdDelete.Enabled = cmdSaveAll.Enabled = true;
            }
        }
        /// <summary>
        /// hàm tìm kiếm thông tin của Form
        /// </summary>
        private void Search()
        {
            try
            {
                SqlQuery query = new Select().From(VDmucDichvukcb.Schema);
                
                    if (cboLoaiKham.SelectedIndex > 0)
                        if (!query.HasWhere)
                        query.Where(VDmucDichvukcb.Columns.IdKieukham).IsEqualTo(Utility.Int32Dbnull(cboLoaiKham.SelectedValue, 0));
                        else
                            query.And(VDmucDichvukcb.Columns.IdKieukham).IsEqualTo(Utility.Int32Dbnull(cboLoaiKham.SelectedValue, 0));

                    if (cboDoituong.SelectedIndex > 0)
                        if (!query.HasWhere)
                            query.Where(VDmucDichvukcb.Columns.IdDoituongKcb).IsEqualTo(Utility.Int32Dbnull(cboDoituong.SelectedValue, 0));
                        else
                            query.And(VDmucDichvukcb.Columns.IdDoituongKcb).IsEqualTo(Utility.Int32Dbnull(cboDoituong.SelectedValue, 0));

                    if (cboPhongBan.SelectedIndex > 0)
                        if (!query.HasWhere)
                            query.Where(VDmucDichvukcb.Columns.IdKhoaphong).IsEqualTo(Utility.Int32Dbnull(cboPhongBan.SelectedValue, 0));
                        else
                            query.And(VDmucDichvukcb.Columns.IdKhoaphong).IsEqualTo(Utility.Int32Dbnull(cboPhongBan.SelectedValue, 0));

                    if (cboRoomDept.SelectedIndex > 0)
                        if (!query.HasWhere)
                            query.Where(VDmucDichvukcb.Columns.IdPhongkham).IsEqualTo(Utility.Int32Dbnull(cboRoomDept.SelectedValue, 0));
                        else
                            query.And(VDmucDichvukcb.Columns.IdPhongkham).IsEqualTo(Utility.Int32Dbnull(cboRoomDept.SelectedValue, 0));

                    if (cboBacsi.SelectedIndex > 0)
                        if (!query.HasWhere)
                            query.Where(VDmucDichvukcb.Columns.IdBacsy).IsEqualTo(Utility.Int32Dbnull(cboBacsi.SelectedValue, 0));
                        else
                            query.And(VDmucDichvukcb.Columns.IdBacsy).IsEqualTo(Utility.Int32Dbnull(cboBacsi.SelectedValue, 0));

                    
                m_dtDmucDichvu_kcb=query.ExecuteDataSet().Tables[0];
               // grd_Insurance_Objects.DataSource = m_dtDmucDichvu_kcb;
                Utility.SetDataSourceForDataGridEx(grdList, m_dtDmucDichvu_kcb,true,true,"1=1","");
                ModifyCommand();
            }
            catch(Exception ex)
            {
                ModifyCommand();
                Utility.ShowMsg(ex.Message);
            }
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
        private void frm_dmucdichvu_kcb_Load(object sender, EventArgs e)
        {
            InitalData();
            Search();
            ModifyCommand();
            m_blnLoaded = true;
           
        }
        /// <summary>
        /// hàm thực hiện xóa thông tin bản ghi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if (grdList.GetCheckedRows().Count() <= 0)
            {
                if (grdList.CurrentRow != null)
                {
                    if (Utility.AcceptQuestion("Bạn có muốn xóa thông tin Dịch vụ khám đang chọn hay không", "Thông báo", true))
                    {
                        int _IdDichvukcb = Utility.Int32Dbnull(grdList.CurrentRow.Cells[DmucDichvukcb.Columns.IdDichvukcb].Value,
                                                              -1);
                        KcbDangkyKcb item = new Select().From(KcbDangkyKcb.Schema).Where(KcbDangkyKcb.Columns.IdDichvuKcb).IsEqualTo(_IdDichvukcb).ExecuteSingle<KcbDangkyKcb>();
                        if (item != null)
                        {
                            Utility.ShowMsg("Dịch vụ KCB bạn đang chọn xóa đã được áp dụng cho một số Bệnh nhân nên bạn không thể xóa");
                            return;
                        }
                        int record = new Delete().From(DmucDichvukcb.Schema)
                            .Where(DmucDichvukcb.Columns.IdDichvukcb).IsEqualTo(_IdDichvukcb).Execute();
                        if (record > 0)
                        {
                            DataRow[] arrDr = m_dtDmucDichvu_kcb.Select(DmucDichvukcb.Columns.IdDichvukcb + "=" + _IdDichvukcb);
                            if (arrDr.GetLength(0) > 0)
                            {
                                arrDr[0].Delete();
                            }
                            m_dtDmucDichvu_kcb.AcceptChanges();
                        }
                        else
                        {
                            Utility.ShowMsg("Lỗi trong quá trình xóa thông tin");
                        }
                    }
                }
            }
            else
            {
                List<string> lstCongkham = new List<string>();
                foreach (GridEXRow row in grdList.GetCheckedRows())
                {
                    var keyId = (int)row.Cells[DmucDichvukcb.Columns.IdDichvukcb].Value;
                    KcbDangkyKcb item = new Select().From(KcbDangkyKcb.Schema).Where(KcbDangkyKcb.Columns.IdDichvuKcb).IsEqualTo(keyId).ExecuteSingle<KcbDangkyKcb>();
                    if (item != null)
                    {
                        lstCongkham.Add(string.Format("Mã: {0}- Tên: {1}", row.Cells["ma_dichvukcb"].Value, row.Cells["ten_dichvukcb"].Value));
                    }
                    else
                    {
                        DataRow dr = (from datarow in m_dtDmucDichvu_kcb.AsEnumerable()
                                      where datarow.Field<int>(DmucDichvukcb.Columns.IdDichvukcb) == keyId
                                      select datarow).First();
                        DmucDichvukcb.Delete(keyId);
                        if (dr != null) m_dtDmucDichvu_kcb.Rows.Remove(dr);
                    }
                }
                if (lstCongkham.Count > 0)
                {
                    Utility.ShowMsg(string.Format("Một số dịch vụ KCB bạn chọn đã được đăng ký cho người bệnh nên bạn không thể xóa. Vui lòng kiểm tra lại\n{0}", string.Join(",\n", lstCongkham.ToArray<string>())));
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
            frm_themmoi_dichvu_kcb frm = new frm_themmoi_dichvu_kcb();
            frm.m_enAction = action.Insert;
            frm.m_dtDataRelation = m_dtDmucDichvu_kcb;
            frm.ShowDialog();
        }
        /// <summary>
        /// hàm thực hiện sửa thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdEdit_Click(object sender, EventArgs e)
        {
            frm_themmoi_dichvu_kcb frm = new frm_themmoi_dichvu_kcb();
            frm.m_enAction = action.Update;
            frm.m_dtDataRelation = m_dtDmucDichvu_kcb;
            frm._txtInsObject_ID.Text =
                Utility.Int32Dbnull(grdList.CurrentRow.Cells[DmucDichvukcb.Columns.IdDichvukcb].Value, -1).ToString();

            frm.ShowDialog();
        }
        #endregion
        
        private void uiCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
         

        }

        private void cmdSaveAll_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataRow drv in m_dtDmucDichvu_kcb.Rows)
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
            }catch(Exception exception)
            {
                Utility.ShowMsg("Lỗi trong quá trình cập nhập thông tin");
                return;
            }
           
        }

        private void grd_Insurance_Objects_SelectionChanged(object sender, EventArgs e)
        {
            ModifyCommand();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindRoomDept(Utility.Int32Dbnull(cboPhongBan.SelectedValue, -1));
        }

        private void cboRoomDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                if (!m_blnLoaded) return;

                BindStaffList(Utility.Int32Dbnull(cboRoomDept.SelectedValue));

            }
            catch (Exception)
            {

            }
        }

        private void grd_Insurance_Objects_ApplyingFilter(object sender, CancelEventArgs e)
        {
            ModifyCommand();
        }

        private void grd_Insurance_Objects_CellUpdated(object sender, ColumnActionEventArgs e)
        {
            if(e.Column.Key==DmucDichvukcb.Columns.DonGia)
            {
                new Update(DmucDichvukcb.Schema)
                    .Set(DmucDichvukcb.Columns.DonGia).EqualTo(
                        Utility.DecimaltoDbnull(
                            grdList.GetValue(DmucDichvukcb.Columns.DonGia)))
                    .Where(DmucDichvukcb.Columns.IdDichvukcb).IsEqualTo(
                        Utility.Int32Dbnull(grdList.GetValue(DmucDichvukcb.Columns.IdDichvukcb)))
                    .Execute();

            }
            if (e.Column.Key == DmucDichvukcb.Columns.TenDichvukcb)
            {
                new Update(DmucDichvukcb.Schema)
                    .Set(DmucDichvukcb.Columns.TenDichvukcb).EqualTo(
                        Utility.sDbnull(
                            grdList.GetValue(DmucDichvukcb.Columns.TenDichvukcb)))
                    .Where(DmucDichvukcb.Columns.IdDichvukcb).IsEqualTo(
                        Utility.Int32Dbnull(grdList.GetValue(DmucDichvukcb.Columns.IdDichvukcb)))
                    .Execute();

            }
            if (e.Column.Key == DmucDichvukcb.Columns.PhuthuDungtuyen)
            {
                new Update(DmucDichvukcb.Schema)
                    .Set(DmucDichvukcb.Columns.DonGia).EqualTo(
                        Utility.DecimaltoDbnull(
                            grdList.GetValue(DmucDichvukcb.Columns.PhuthuDungtuyen)))
                    .Where(DmucDichvukcb.Columns.IdDichvukcb).IsEqualTo(
                        Utility.Int32Dbnull(grdList.GetValue(DmucDichvukcb.Columns.IdDichvukcb)))
                    .Execute();

            }
        }

        private void grd_Insurance_Objects_UpdatingCell(object sender, UpdatingCellEventArgs e)
        {
            if(e.Column.Key==DmucDichvukcb.Columns.DonGia)
            {
                
            }
        }

        private void cmdExport_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*";
                saveFileDialog1.FileName = "dmuc_congkham";
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    using (Stream s = File.Open(saveFileDialog1.FileName, FileMode.Create))
                    {
                        gridEXExporter.GridEX = grdList;
                        gridEXExporter.Export(s);

                    }
                    Utility.ShowMsg("Xuất Excel thành công. Nhấn OK để mở file");
                    System.Diagnostics.Process.Start(saveFileDialog1.FileName);
                }

            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
       
    }
}
