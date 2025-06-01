using System;
using System.Data;
using System.Windows.Forms;
using SubSonic;
using VMS.HIS.DAL;
using VNS.Libs;

namespace VNS.HIS.UI.DANHMUC
{
    public partial class frm_themmoi_dichvu_kcb : Form
    {
        #region "Declare Variable Form"

        public DataTable m_dtDataRelation = new DataTable();
        private DataTable m_dtGroupInsurance = new DataTable();
        private DataTable m_dtObjectType = new DataTable();
        public action m_enAction = action.Insert;

        #endregion

        #region "Contructor"

        public frm_themmoi_dichvu_kcb()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            cmdSaveAll.Visible = globalVariables.IsAdmin;
            cboRoomDept.SelectedIndexChanged += cboRoomDept_SelectedIndexChanged;
            //cboRoomDept_ValueChanged
        }

        public TextBox _txtInsObject_ID
        {
            get { return txtInsObject_ID; }
        }

        #endregion

        #region "Method of Event Form"

        /// <summary>
        /// hàm thực hiện laod thông tin cảu Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_themmoi_dichvu_kcb_Load(object sender, EventArgs e)
        {
            InitData();
            if (m_enAction == action.Update) GetData();
            else
            {
                nmrSTT.Value = Utility.DecimaltoDbnull(
                    DmucDichvucl.CreateQuery().GetMax(DmucDichvukcb.Columns.SttHthi), 0);
            }
            m_blnLoaded = true;
            //cboDepartment1_SelectedIndexChanged(cboDepartment1, e);
        }

        /// <summary>
        /// hàm thực hiện dùng phím tắt trên Form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_themmoi_dichvu_kcb_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.Control && e.KeyCode == Keys.S) cmdSave.PerformClick();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            PerformAction();
        }

        #endregion

        private decimal DeptFee;
        private bool m_blnLoaded;

        #region "Method of common"

        private readonly Query _Query = DmucDichvukcb.CreateQuery();
        private int Id = -1;
        private DataTable dtDept = new DataTable();
        private DataTable v_ObjectTypeList = new DataTable();

        private void PerformAction()
        {
            switch (m_enAction)
            {
                case action.Insert:
                    if (!IsValidData()) return;
                    PerformActionInsert();
                    break;
                case action.Update:
                    if (!IsValidData()) return;
                    PerformActionUpdate();
                    break;
            }
        }
        DataTable dtKieukham = null;
        /// <summary>
        /// hàm thực hiện khởi tạo thông tin 
        /// </summary>
        private void InitData()
        {
            try
            {
                dtDept = THU_VIEN_CHUNG.Laydanhmuckhoa("NGOAI", 0);
                //Khởi tạo danh mục Loại khám
                 dtKieukham = new DmucKieukhamCollection().Load().ToDataTable();

                 cboLoaiKham.DataSource = dtKieukham.DefaultView;
                cboLoaiKham.ValueMember = DmucDichvukcb.Columns.IdKieukham;
                cboLoaiKham.DisplayMember = DmucKieukham.Columns.TenKieukham;
                //Khởi tạo danh mục Loại khám
                v_ObjectTypeList = new Select().From(DmucDoituongkcb.Schema).ExecuteDataSet().Tables[0];
                //Utility.AddColumnAlltoDataTable(ref v_ObjectTypeList, DmucDoituongkcb.Columns.IdDoituongKcb, DmucDoituongkcb.Columns.TenDoituongKcb, "====Chọn====");
                //v_ObjectTypeList.DefaultView.Sort = DmucDoituongkcb.Columns.SttHthi;
                DataBinding.BindDataCombox(cboDoituong, v_ObjectTypeList, DmucDoituongkcb.Columns.IdDoituongKcb,
                                           DmucDoituongkcb.Columns.TenDoituongKcb);
                //cboDoituong.DataSource = v_ObjectTypeList.DefaultView;
                //cboDoituong.ValueMember = DmucDoituongkcb.Columns.IdDoituongKcb;
                //cboDoituong.DisplayMember = DmucDoituongkcb.Columns.TenDoituongKcb;
                //Phòng ban
                BindDepartment();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }

        private void BindStaffList(int departmentID)
        {
            try
            {
                DataTable _dsStaffList = THU_VIEN_CHUNG.Laydanhsachnhanvienthuockhoa(departmentID);
                Utility.AddColumnAlltoDataTable(ref _dsStaffList, DmucNhanvien.Columns.IdNhanvien,
                                                DmucNhanvien.Columns.TenNhanvien, "====Chọn====");
                _dsStaffList.DefaultView.Sort = DmucNhanvien.Columns.IdNhanvien + " ASC";
                cboBacSy.DataSource = _dsStaffList.DefaultView;
                cboBacSy.ValueMember = DmucNhanvien.Columns.IdNhanvien;
                cboBacSy.DisplayMember = DmucNhanvien.Columns.TenNhanvien;
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }

        private void BindDepartment()
        {
            //Utility.AddColumnAlltoDataTable(ref dtDept, DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong, "====Chọn====");
            DataBinding.BindDataCombox(cboDepartment1, dtDept, DmucKhoaphong.Columns.IdKhoaphong,
                                       DmucKhoaphong.Columns.TenKhoaphong);
            //dtDept.DefaultView.Sort = DmucKhoaphong.Columns.SttHthi;
            //cboDepartment1.DataSource = dtDept.DefaultView;
            //cboDepartment1.ValueMember = DmucDichvukcb.Columns.IdKhoaphong;
            //cboDepartment1.DisplayMember = DmucKhoaphong.Columns.TenKhoaphong;
            ////dtDept.DefaultView.RowFilter = DmucKhoaphong.Columns.PhongChucnang + "=0 and " + DmucKhoaphong.Columns.KieuKhoaphong + " =KHOA and ma_cha=0";
            //dtDept.DefaultView.Sort = DmucKhoaphong.Columns.SttHthi+ " ASC";
            if (cboDepartment1.Items.Count >= 0)
                BindRoomDept(Utility.Int32Dbnull(cboDepartment1.SelectedValue, -1));
        }
        DataTable dtPK = new DataTable();
        private void BindRoomDept(int Dept_Id)
        {
            dtPK = THU_VIEN_CHUNG.Laydanhsachphongthuockhoa(Dept_Id, 0);
            Utility.AddColumnAlltoDataTable(ref dtPK, DmucDichvukcb.Columns.IdKhoaphong,
                                            DmucKhoaphong.Columns.TenKhoaphong, "====Chọn====");
            cmdSave.Enabled = dtPK.Rows.Count > 0;
            dtPK.DefaultView.Sort = DmucKhoaphong.Columns.TenKhoaphong + " ASC";
            cboRoomDept.DataSource = dtPK.DefaultView;
            cboRoomDept.ValueMember = DmucKhoaphong.Columns.IdKhoaphong;
            cboRoomDept.DisplayMember = DmucKhoaphong.Columns.TenKhoaphong;
        }

        private void PerformActionInsert()
        {
            try
            {
                var objDmucDichvukcb = new DmucDichvukcb();
                objDmucDichvukcb.MaDichvukcb = Utility.sDbnull(txtCode.Text.Trim(), "");
                objDmucDichvukcb.MaBhyt = Utility.sDbnull(txtMaBhyt.Text.Trim(), "");
                objDmucDichvukcb.MaGia = Utility.sDbnull(txtmagiabhyt.Text.Trim(), "");
                objDmucDichvukcb.HoatDong = chkhoatdong.Checked;
                objDmucDichvukcb.TenDichvukcb = Utility.sDbnull(txtName.Text, "");
                objDmucDichvukcb.IdKieukham = Utility.Int16Dbnull(cboLoaiKham.SelectedValue, -1);
                objDmucDichvukcb.IdKhoaphong = Utility.Int16Dbnull(cboDepartment1.SelectedValue, -1);
                objDmucDichvukcb.IdBacsy = Convert.ToInt16(cboBacSy.Items.Count > 0
                                                               ? Utility.Int16Dbnull(cboBacSy.SelectedValue, -1)
                                                               : -1);
                objDmucDichvukcb.MotaThem = Utility.DoTrim(txtDesc.Text);
                objDmucDichvukcb.SttHthi = Utility.Int16Dbnull(nmrSTT.Value);
                objDmucDichvukcb.IdDoituongKcb = Utility.Int16Dbnull(cboDoituong.SelectedValue, -1);
                DmucDoituongkcb objectType = DmucDoituongkcb.FetchByID(objDmucDichvukcb.IdDoituongKcb);
                if (objectType != null)
                {
                    objDmucDichvukcb.MaDoituongKcb = Utility.sDbnull(objectType.MaDoituongKcb, "");
                    objDmucDichvukcb.IdLoaidoituongKcb = objectType.IdLoaidoituongKcb;
                }
                else
                {
                    objDmucDichvukcb.MaDoituongKcb = "ALL";
                    objDmucDichvukcb.IdLoaidoituongKcb = -1;
                }

                DmucKieukham objKieukham = DmucKieukham.FetchByID(Utility.Int16Dbnull(cboLoaiKham.SelectedValue, -1));
                if (objKieukham != null)
                    objDmucDichvukcb.NhomBaocao = Utility.sDbnull(objKieukham.NhomBaocao, "");
                else
                    objDmucDichvukcb.NhomBaocao = "-1";


                objDmucDichvukcb.IdPhongkham = Utility.Int16Dbnull(cboRoomDept.SelectedValue);
                objDmucDichvukcb.PhuthuDungtuyen = Utility.DecimaltoDbnull(txtPTDT.Text, 0);
                objDmucDichvukcb.PhuthuTraituyen = Utility.DecimaltoDbnull(txtPTTT.Text, 0);
                objDmucDichvukcb.DonGia = Utility.DecimaltoDbnull(txtDongia.Text, 0);
                objDmucDichvukcb.DongiaNgoaigio = Utility.DecimaltoDbnull(txtGiangoaigio.Text, 0);
                objDmucDichvukcb.PhuthuNgoaigio = Utility.DecimaltoDbnull(txtPhuthungoaigio.Text, 0);
                objDmucDichvukcb.TuTuc = Utility.Bool2byte(chkTutuc.Checked);
                objDmucDichvukcb.KhamThiluc = Utility.Bool2byte(chkKhamthiluc.Checked);
                objDmucDichvukcb.CapKinh =chkCapkinh.Checked;
                objDmucDichvukcb.IsNew = true;
                objDmucDichvukcb.Save();
                DataRow dr = m_dtDataRelation.NewRow();
                dr[DmucDichvukcb.Columns.IdDichvukcb] = Utility.Int32Dbnull(
                    _Query.GetMax(DmucDichvukcb.Columns.IdDichvukcb), -1);
                dr[DmucDichvukcb.Columns.IdDoituongKcb] = Utility.DecimaltoDbnull(cboDoituong.SelectedValue);
                dr[DmucDichvukcb.Columns.IdKhoaphong] = Utility.Int16Dbnull(cboDepartment1.SelectedValue, -1);
                dr[DmucDichvukcb.Columns.IdPhongkham] = Utility.Int16Dbnull(cboRoomDept.SelectedValue, -1);
                dr[DmucDichvukcb.Columns.IdBacsy] = cboBacSy.Items.Count > 0
                                                        ? Utility.Int16Dbnull(cboBacSy.SelectedValue, -1)
                                                        : -1;
                dr[DmucDichvukcb.Columns.DonGia] = Utility.DecimaltoDbnull(txtDongia.Text, 0);
                dr[DmucDichvukcb.Columns.PhuthuDungtuyen] = Utility.DecimaltoDbnull(txtPTDT.Text, 0);
                dr[DmucDichvukcb.Columns.PhuthuTraituyen] = Utility.DecimaltoDbnull(txtPTTT.Text, 0);
                dr[DmucDichvukcb.Columns.DongiaNgoaigio] = Utility.DecimaltoDbnull(txtGiangoaigio.Text, 0);
                dr[DmucDichvukcb.Columns.PhuthuNgoaigio] = Utility.DecimaltoDbnull(txtPhuthungoaigio.Text, 0);

                dr[DmucDichvukcb.Columns.MotaThem] = Utility.DoTrim(txtDesc.Text);
                dr[DmucDichvukcb.Columns.SttHthi] = Utility.Int16Dbnull(nmrSTT.Value, 1);


                dr[DmucDichvukcb.Columns.TuTuc] = Utility.Bool2byte(chkTutuc.Checked);
                dr[DmucDichvukcb.Columns.HoatDong] = Utility.Bool2byte(chkhoatdong.Checked);
                dr["ten_phong"] = cboRoomDept.Text;
                dr[DmucDichvukcb.Columns.IdKieukham] = Utility.Int16Dbnull(cboLoaiKham.SelectedValue, -1);
                dr[DmucKieukham.Columns.TenKieukham] = cboLoaiKham.Text;
                dr[DmucDichvukcb.Columns.MaDichvukcb] = txtCode.Text.Trim();
                dr[DmucDichvukcb.Columns.MaBhyt] = txtMaBhyt.Text.Trim();
                dr[DmucDichvukcb.Columns.MaGia] = txtmagiabhyt.Text.Trim();
                dr[DmucDichvukcb.Columns.TenDichvukcb] = txtName.Text.Trim();
                dr["ten_khoa"] = cboDepartment1.SelectedIndex <= -1 ? "Tất cả các khoa KCB" : cboDepartment1.Text;
                dr["ten_bacsi"] = cboBacSy.SelectedIndex > 0 ? cboBacSy.Text : "";
                dr[DmucDoituongkcb.Columns.TenDoituongKcb] = objectType != null ? cboDoituong.Text : "Tất cả các đối tượng";

                m_dtDataRelation.Rows.Add(dr);
                if (chkThemlientuc.Checked)
                {
                    txtCode.Clear();
                    txtMaBhyt.Clear();
                    txtName.Clear();
                    txtCode.Focus();
                    txtCode.SelectAll();
                }
                else
                    Close();
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
          
            //this.Close();
        }

        /// <summary>
        /// /hàm thực heien thông tin update thông tin lại
        /// </summary>
        private void PerformActionUpdate()
        {
            try
            {
                string MaDoituongKcb = "ALL";
                string NhomBaocao = "-1";
                Int16 idLoaidoituongkcb = -1;
                DmucDoituongkcb objectType = DmucDoituongkcb.FetchByID(Utility.Int16Dbnull(cboDoituong.SelectedValue, -1));
                if (objectType != null)
                {
                    idLoaidoituongkcb = objectType.IdLoaidoituongKcb;
                    MaDoituongKcb = objectType.MaDoituongKcb;
                }
                else
                {
                    MaDoituongKcb = "ALL";
                    idLoaidoituongkcb = -1;
                }
                DmucKieukham objKieukham = DmucKieukham.FetchByID(Utility.Int16Dbnull(cboLoaiKham.SelectedValue, -1));
                if (objKieukham != null)
                    NhomBaocao = Utility.sDbnull(objKieukham.NhomBaocao, "");
                else
                    NhomBaocao = "-1";
                int record = new Update(DmucDichvukcb.Schema)
                    .Set(DmucDichvukcb.Columns.IdKhoaphong).EqualTo(Utility.Int16Dbnull(cboDepartment1.SelectedValue, 1))
                    .Set(DmucDichvukcb.Columns.MaDichvukcb).EqualTo(txtCode.Text.Trim())
                    .Set(DmucDichvukcb.Columns.HoatDong).EqualTo(chkhoatdong.Checked)
                     .Set(DmucDichvukcb.Columns.KhamThiluc).EqualTo(Utility.Bool2byte(chkKhamthiluc.Checked))
                    .Set(DmucDichvukcb.Columns.MaGia).EqualTo(txtmagiabhyt.Text.Trim())
                    .Set(DmucDichvukcb.Columns.MaBhyt).EqualTo(txtMaBhyt.Text.Trim())
                    .Set(DmucDichvukcb.Columns.TenDichvukcb).EqualTo(txtName.Text.Trim())
                    .Set(DmucDichvukcb.Columns.MotaThem).EqualTo(txtDesc.Text.Trim())
                    .Set(DmucDichvukcb.Columns.SttHthi).EqualTo(Utility.Int16Dbnull(nmrSTT.Value))
                    .Set(DmucDichvukcb.Columns.NhomBaocao).EqualTo(NhomBaocao)
                    .Set(DmucDichvukcb.Columns.MaDoituongKcb).EqualTo(MaDoituongKcb)
                    .Set(DmucDichvukcb.Columns.IdPhongkham).EqualTo(Utility.Int16Dbnull(cboRoomDept.SelectedValue, 1))
                    .Set(DmucDichvukcb.Columns.IdBacsy).EqualTo(
                        Convert.ToInt16(cboBacSy.Items.Count > 0 ? Utility.Int16Dbnull(cboBacSy.SelectedValue, 1) : -1))
                    .Set(DmucDichvukcb.Columns.IdKieukham).EqualTo(Utility.Int16Dbnull(cboLoaiKham.SelectedValue, 1))
                    .Set(DmucDichvukcb.Columns.IdDoituongKcb).EqualTo(Utility.Int16Dbnull(cboDoituong.SelectedValue, -1))
                     .Set(DmucDichvukcb.Columns.IdLoaidoituongKcb).EqualTo(idLoaidoituongkcb)
                    .Set(DmucDichvukcb.Columns.DonGia).EqualTo(Utility.DecimaltoDbnull(txtDongia.Text, 0))
                    .Set(DmucDichvukcb.Columns.DongiaNgoaigio).EqualTo(Utility.DecimaltoDbnull(txtGiangoaigio.Text, 0))
                    .Set(DmucDichvukcb.Columns.PhuthuNgoaigio).EqualTo(Utility.DecimaltoDbnull(txtPhuthungoaigio.Text, 0))
                    .Set(DmucDichvukcb.Columns.TuTuc).EqualTo(Utility.Bool2byte(chkTutuc.Checked))
                    .Set(DmucDichvukcb.Columns.PhuthuDungtuyen).EqualTo(Utility.DecimaltoDbnull(txtPTDT.Text, 0))
                    .Set(DmucDichvukcb.Columns.PhuthuTraituyen).EqualTo(Utility.DecimaltoDbnull(txtPTTT.Text, 0))
                    .Where(DmucDichvukcb.Columns.IdDichvukcb).IsEqualTo(Utility.DecimaltoDbnull(txtInsObject_ID.Text, -1)).
                    Execute();
                if (record > 0)
                {
                    DataRow[] dr =
                        m_dtDataRelation.Select(DmucDichvukcb.Columns.IdDichvukcb + "=" +
                                                Utility.Int32Dbnull(txtInsObject_ID.Text, -1));
                    if (dr.GetLength(0) > 0)
                    {
                        dr[0][DmucDichvukcb.Columns.IdDoituongKcb] = Utility.DecimaltoDbnull(cboDoituong.SelectedValue);
                        dr[0][DmucDichvukcb.Columns.IdKhoaphong] = Utility.Int16Dbnull(cboDepartment1.SelectedValue, -1);
                        dr[0][DmucDichvukcb.Columns.IdPhongkham] = Utility.Int16Dbnull(cboRoomDept.SelectedValue, -1);
                        dr[0][DmucDichvukcb.Columns.IdBacsy] = cboBacSy.Items.Count > 0
                                                                   ? Utility.Int16Dbnull(cboBacSy.SelectedValue, -1)
                                                                   : -1;
                        dr[0][DmucDichvukcb.Columns.IdKieukham] = Utility.Int16Dbnull(cboLoaiKham.SelectedValue, -1);
                        dr[0][DmucKieukham.Columns.TenKieukham] = cboLoaiKham.Text;
                        dr[0]["ten_khoa"] = cboDepartment1.SelectedIndex <= -1 ? "Tất cả các khoa KCB" : cboDepartment1.Text;
                        dr[0]["ten_phong"] = cboRoomDept.Text;
                        dr[0][DmucDichvukcb.Columns.MaDichvukcb] = txtCode.Text.Trim();
                        dr[0][DmucDichvukcb.Columns.MaBhyt] = txtMaBhyt.Text.Trim();
                        dr[0][DmucDichvukcb.Columns.MaGia] = txtmagiabhyt.Text.Trim();
                        dr[0][DmucDichvukcb.Columns.TenDichvukcb] = txtName.Text.Trim();
                        dr[0]["ten_bacsi"] = cboBacSy.SelectedIndex > 0 ? cboBacSy.Text : "";
                        dr[0][DmucDichvukcb.Columns.MaDoituongKcb] = objectType != null ? objectType.MaDoituongKcb : "ALL";
                        dr[0][DmucDoituongkcb.Columns.TenDoituongKcb] = objectType != null
                                                                            ? cboDoituong.Text
                                                                            : "Tất cả các đối tượng";
                        dr[0][DmucDichvukcb.Columns.MotaThem] = Utility.DoTrim(txtDesc.Text);
                        dr[0][DmucDichvukcb.Columns.SttHthi] = Utility.Int16Dbnull(nmrSTT.Value, 1);

                        dr[0][DmucDichvukcb.Columns.DonGia] = Utility.DecimaltoDbnull(txtDongia.Text, 0);
                        dr[0][DmucDichvukcb.Columns.DongiaNgoaigio] = Utility.DecimaltoDbnull(txtGiangoaigio.Text, 0);
                        dr[0][DmucDichvukcb.Columns.PhuthuNgoaigio] = Utility.DecimaltoDbnull(txtPhuthungoaigio.Text, 0);
                        dr[0][DmucDichvukcb.Columns.TuTuc] = Utility.Bool2byte(chkTutuc.Checked);
                        dr[0][DmucDichvukcb.Columns.HoatDong] = Utility.Bool2byte(chkhoatdong.Checked);
                        dr[0][DmucDichvukcb.Columns.PhuthuDungtuyen] = Utility.DecimaltoDbnull(txtPTDT.Text, 0);
                        dr[0][DmucDichvukcb.Columns.PhuthuTraituyen] = Utility.DecimaltoDbnull(txtPTTT.Text, 0);
                    }
                    m_dtDataRelation.AcceptChanges();
                    Close();
                }
                else
                {
                    Utility.ShowMsg("Lỗi trong quá trình cập nhập dữ liệu");
                    return;
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException(ex);
            }
         
        }

        /// <summary>
        /// hàm thực hiện kiểm tra thông tin của phần mã tham gia đối tượng bảo hiểm
        /// </summary>
        /// <returns></returns>
        private bool IsValidData()
        {
            Utility.SetMsg(lblMsg, "", false);
            if (cboLoaiKham.Items.Count <= 0)
            {
                Utility.SetMsg(lblMsg, "Phải khởi tạo danh mục loại khám trước khi thực hiện chức năng này", true);
                cboLoaiKham.Focus();
                return false;
            }
            if (cboDoituong.Items.Count <= 0)
            {
                Utility.SetMsg(lblMsg, "Phải khởi tạo danh mục Đối tượng trước khi thực hiện chức năng này", true);
                cboDoituong.Focus();
                return false;
            }
            if (cboDepartment1.Items.Count <= 0)
            {
                Utility.SetMsg(lblMsg, "Phải khởi tạo danh mục Khoa trước khi thực hiện chức năng này", true);
                cboDepartment1.Focus();
                return false;
            }
            if (cboRoomDept.Items.Count <= 0)
            {
                Utility.SetMsg(lblMsg,
                               "Phải khởi tạo danh mục Phòng ban thuộc Khoa " + cboDepartment1.Text +
                               " trước khi thực hiện chức năng này", true);
                cboRoomDept.Focus();
                return false;
            }
            SqlQuery q =
                new Select().From(DmucDichvukcb.Schema).Where(DmucDichvukcb.Columns.MaDichvukcb).IsEqualTo(
                    Utility.DoTrim(txtCode.Text));
            if (m_enAction == action.Update)
                q.And(DmucDichvukcb.Columns.IdDichvukcb).IsNotEqualTo(Utility.Int32Dbnull(txtInsObject_ID.Text, -1));

            if (q.GetRecordCount() > 0)
            {
                Utility.SetMsg(lblMsg,
                               "Đã tồn tại dịch vụ KCB có mã " + Utility.DoTrim(txtCode.Text) + ". Mời bạn nhập mã khác",
                               true);
                txtCode.Focus();
                return false;
            }
            //Kiểm tra xem quan hệ này đã có hay chưa
            if (blnIsExisted(Id, txtCode.Text.Trim(), Convert.ToInt32(cboDoituong.SelectedValue),
                             Convert.ToInt32(cboRoomDept.SelectedValue), Convert.ToInt32(cboLoaiKham.SelectedValue),
                             Convert.ToInt32(cboBacSy.SelectedValue)))
            {
                Utility.SetMsg(lblMsg,
                               "Đã tồn tại quan hệ này. Đề nghị bạn chọn các cặp đối tượng khác! " + cboDepartment1.Text +
                               " trước khi thực hiện chức năng này", true);
                cboRoomDept.Focus();
                return false;
            }
            return true;
        }

        public bool blnIsExisted(int ID, string code, int objectType_Id, int Department_Id, int ExamType_ID,
                                 int Doctor_ID)
        {
            try
            {
                DmucDichvukcbCollection v_DmucDichvukcb =
                    new DmucDichvukcbController().FetchByQuery(
                        DmucDichvukcb.CreateQuery().AddWhere(DmucDichvukcb.Columns.MaDichvukcb,
                                                             Comparison.Equals, txtCode.Text.Trim()).AND(
                                                                 DmucDichvukcb.Columns.IdPhongkham,
                                                                 Comparison.Equals, Department_Id).AND(
                                                                     DmucDichvukcb.Columns.IdDoituongKcb,
                                                                     Comparison.Equals, objectType_Id).AND(
                                                                         DmucDichvukcb.Columns.IdKieukham,
                                                                         Comparison.Equals, ExamType_ID).AND(
                                                                             DmucDichvukcb.Columns.IdBacsy,
                                                                             Comparison.Equals, Doctor_ID).AND(
                                                                                 DmucDichvukcb.Columns.IdDichvukcb,
                                                                                 Comparison.NotEquals, ID));
                return v_DmucDichvukcb.Count > 0;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// lấy thông tin của khi load sửa thông tin của Form
        /// </summary>
        private void GetData()
        {
            try
            {
                DmucDichvukcb Obj = DmucDichvukcb.FetchByID(Utility.Int32Dbnull(txtInsObject_ID.Text, -1));
                if (Obj != null)
                {
                    Id = Obj.IdDichvukcb;
                    cboDepartment1.SelectedIndex = Utility.GetSelectedIndex(cboDepartment1,
                                                                            Obj.IdKhoaphong.ToString());
                    //cboDepartment1_SelectedIndexChanged(cboDepartment1, new EventArgs());
                    BindRoomDept(Obj.IdKhoaphong);
                    cboRoomDept.SelectedValue = Utility.Int32Dbnull(Obj.IdPhongkham);
                    //cboRoomDept.SelectedIndex = Utility.GetSelectedIndex(cboRoomDept,
                    //                                                        Obj.IdPhongkham.ToString());
                    //cboRoomDept_SelectedIndexChanged(cboRoomDept, new EventArgs());
                    BindStaffList(Obj.IdKhoaphong);
                    cboBacSy.SelectedValue = Utility.Int32Dbnull(Obj.IdBacsy);
                    //cboBacSy.SelectedIndex = Utility.GetSelectedIndex(cboBacSy,
                    //                                                  Obj.IdBacsy.
                    //                                                      ToString());

                    cboLoaiKham.SelectedIndex = Utility.GetSelectedIndex(cboLoaiKham,
                                                                         Obj.IdKieukham.
                                                                             ToString());
                    cboDoituong.SelectedIndex = Utility.GetSelectedIndex(cboDoituong,
                                                                         Obj.IdDoituongKcb.
                                                                             ToString());
                    //  cboRoomDept.SelectedIndex = Utility.GetSelectedIndex(cboRoomDept,Obj.IdPhongkham)
                    txtDongia.Text = Utility.DecimaltoDbnull(Obj.DonGia, 0).ToString();
                    txtGiangoaigio.Text = Utility.DecimaltoDbnull(Obj.DongiaNgoaigio, 0).ToString();
                    txtPhuthungoaigio.Text = Utility.DecimaltoDbnull(Obj.PhuthuNgoaigio, 0).ToString();
                    txtPTDT.Text = Utility.DecimaltoDbnull(Obj.PhuthuDungtuyen, 0).ToString();
                    txtPTTT.Text = Utility.DecimaltoDbnull(Obj.PhuthuTraituyen, 0).ToString();
                    chkTutuc.Checked = Utility.Byte2Bool(Obj.TuTuc);
                    chkKhamthiluc.Checked = Utility.Byte2Bool(Obj.KhamThiluc);
                    chkCapkinh.Checked = Utility.Bool2Bool(Obj.CapKinh);
                    chkhoatdong.Checked = Utility.Byte2Bool(Obj.HoatDong);
                    txtmagiabhyt.Text = Utility.sDbnull(Obj.MaGia);
                    txtMaBhyt.Text = Utility.sDbnull(Obj.MaBhyt);
                    txtDesc.Text = Obj.MotaThem;
                    nmrSTT.Value = Utility.DecimaltoDbnull(Obj.SttHthi, 1);
                    txtCode.Text = Obj.MaDichvukcb;
                    txtName.Text = Obj.TenDichvukcb;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }

        #endregion

        private void cboDepartment1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!m_blnLoaded) return;
                BindRoomDept(Utility.Int32Dbnull(cboDepartment1.SelectedValue, -1));
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }

        private void cboRoomDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!m_blnLoaded) return;
                BindStaffList(Utility.Int32Dbnull(cboDepartment1.SelectedValue));
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
        }
        DmucKieukham objExamTypeList = null;
        private void cboLoaiKham_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (!m_blnLoaded) return;
                string _RowFilter = "1=1";
                 objExamTypeList = DmucKieukham.FetchByID(Utility.Int32Dbnull(cboLoaiKham.SelectedValue));
                if (objExamTypeList != null)
                {
                    if (objExamTypeList.MaDoituongkcb == "ALL")
                    {
                        _RowFilter = string.Format("{0}", "1=1");
                    }
                    else
                    {
                        _RowFilter = string.Format("{0}='{1}'", DmucDoituongkcb.Columns.MaDoituongKcb,
                                                   objExamTypeList.MaDoituongkcb);
                    }
                    txtDongia.Text = Utility.sDbnull(objExamTypeList.DonGia, "0");
                }
                v_ObjectTypeList.DefaultView.RowFilter = _RowFilter;
                v_ObjectTypeList.AcceptChanges();
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
                // throw;
            }
        }

        private void cmdSaveAll_Click(object sender, EventArgs e)
        {
            SaveAll();
        }
        private void SaveAll()
        {
            if (Utility.Int32Dbnull(cboLoaiKham.SelectedValue, 0) > 0)
            {
                if (Utility.Int32Dbnull(cboRoomDept.SelectedValue, 0) < 0)//Chọn loại khám, không chọn phòng khám
                {
                    foreach (DataRow drpk in dtPK.Rows)
                    {
                        DataTable dtCheck = SPs.DmucDichvukcbKiemtratontai(Utility.Int32Dbnull(cboLoaiKham.SelectedValue, 0), Utility.Int32Dbnull(drpk[DmucKhoaphong.Columns.IdKhoaphong])).GetDataSet().Tables[0];
                        if (dtCheck.Rows.Count <= 0)
                        {
                            if (Utility.Int32Dbnull(drpk[DmucKhoaphong.Columns.IdKhoaphong]) > 0)
                            {
                                DmucKieukham objKieukham = DmucKieukham.FetchByID(Utility.Int32Dbnull(cboLoaiKham.SelectedValue, 0));
                                if (objKieukham != null)
                                {
                                    var objDmucDichvukcb = new DmucDichvukcb();
                                    objDmucDichvukcb.NhomBaocao = Utility.sDbnull(objKieukham.NhomBaocao, "");


                                    objDmucDichvukcb.MaDichvukcb = string.Format("{0}-{1}", objKieukham.MaKieukham, Utility.sDbnull(drpk[DmucKhoaphong.Columns.IdKhoaphong]));
                                    objDmucDichvukcb.MaBhyt = Utility.sDbnull(txtMaBhyt.Text.Trim(), "");
                                    objDmucDichvukcb.MaGia = Utility.sDbnull(txtmagiabhyt.Text.Trim(), "");
                                    objDmucDichvukcb.HoatDong = chkhoatdong.Checked;
                                    objDmucDichvukcb.TenDichvukcb = objKieukham.TenKieukham;
                                    objDmucDichvukcb.IdKieukham = Utility.Int16Dbnull(cboLoaiKham.SelectedValue, 0);
                                    objDmucDichvukcb.IdKhoaphong = Utility.Int16Dbnull(cboDepartment1.SelectedValue, -1);
                                    objDmucDichvukcb.IdBacsy = Convert.ToInt16(cboBacSy.Items.Count > 0
                                                                                   ? Utility.Int16Dbnull(cboBacSy.SelectedValue, -1)
                                                                                   : -1);
                                    objDmucDichvukcb.MotaThem = Utility.DoTrim(txtDesc.Text);
                                    objDmucDichvukcb.SttHthi = Utility.Int16Dbnull(nmrSTT.Value);
                                    objDmucDichvukcb.IdDoituongKcb = Utility.Int16Dbnull(cboDoituong.SelectedValue, -1);
                                    DmucDoituongkcb objectType = DmucDoituongkcb.FetchByID(objDmucDichvukcb.IdDoituongKcb);
                                    if (objectType != null)
                                    {
                                        objDmucDichvukcb.MaDoituongKcb = Utility.sDbnull(objectType.MaDoituongKcb, "");
                                        objDmucDichvukcb.IdLoaidoituongKcb = objectType.IdLoaidoituongKcb;
                                    }
                                    else
                                    {
                                        objDmucDichvukcb.MaDoituongKcb = "ALL";
                                        objDmucDichvukcb.IdLoaidoituongKcb = -1;
                                    }




                                    objDmucDichvukcb.IdPhongkham = Utility.Int16Dbnull(drpk[DmucKhoaphong.Columns.IdKhoaphong]);
                                    objDmucDichvukcb.PhuthuDungtuyen = Utility.DecimaltoDbnull(txtPTDT.Text, 0);
                                    objDmucDichvukcb.PhuthuTraituyen = Utility.DecimaltoDbnull(txtPTTT.Text, 0);
                                    objDmucDichvukcb.DonGia = Utility.DecimaltoDbnull(objKieukham.DonGia, 0);
                                    objDmucDichvukcb.DongiaNgoaigio = 0;
                                    objDmucDichvukcb.PhuthuNgoaigio = 0;
                                    objDmucDichvukcb.TuTuc = Utility.Bool2byte(chkTutuc.Checked);
                                    objDmucDichvukcb.IsNew = true;
                                    objDmucDichvukcb.Save();

                                }
                            }
                        }
                    }
                }
                else//Chọn PK và chọn Loại khám
                {
                    Utility.ShowMsg("Mời bạn dùng tính năng lưu bình thường cho trường hợp chọn cả kiểu khám lẫn phòng khám");
                    return;
                }
            }
            else//Chưa chọn kiểu khám
            {
                if (Utility.Int32Dbnull(cboRoomDept.SelectedValue, 0) >0)//Chọn phòng khám
                {
                    foreach (DataRow drkk in dtKieukham.Rows)
                    {
                        DataTable dtCheck = SPs.DmucDichvukcbKiemtratontai(Utility.Int32Dbnull(drkk["id_kieukham"], -1), Utility.Int32Dbnull(cboRoomDept.SelectedValue, 0)).GetDataSet().Tables[0];
                        if (dtCheck.Rows.Count <= 0)
                        {
                            
                                DmucKieukham objKieukham = DmucKieukham.FetchByID(Utility.Int32Dbnull(cboLoaiKham.SelectedValue, 0));
                                if (objKieukham != null)
                                {
                                    var objDmucDichvukcb = new DmucDichvukcb();
                                    objDmucDichvukcb.NhomBaocao = Utility.sDbnull(objKieukham.NhomBaocao, "");


                                    objDmucDichvukcb.MaDichvukcb = string.Format("{0}-{1}", objKieukham.MaKieukham, Utility.Int32Dbnull(cboRoomDept.SelectedValue, 0));
                                    objDmucDichvukcb.MaBhyt = Utility.sDbnull(txtMaBhyt.Text.Trim(), "");
                                    objDmucDichvukcb.MaGia = Utility.sDbnull(txtmagiabhyt.Text.Trim(), "");
                                    objDmucDichvukcb.HoatDong = chkhoatdong.Checked;
                                    objDmucDichvukcb.TenDichvukcb = objKieukham.TenKieukham;
                                    objDmucDichvukcb.IdKieukham = Utility.Int16Dbnull(cboLoaiKham.SelectedValue, 0);
                                    objDmucDichvukcb.IdKhoaphong = Utility.Int16Dbnull(cboDepartment1.SelectedValue, -1);
                                    objDmucDichvukcb.IdBacsy = Convert.ToInt16(cboBacSy.Items.Count > 0
                                                                                   ? Utility.Int16Dbnull(cboBacSy.SelectedValue, -1)
                                                                                   : -1);
                                    objDmucDichvukcb.MotaThem = Utility.DoTrim(txtDesc.Text);
                                    objDmucDichvukcb.SttHthi = Utility.Int16Dbnull(nmrSTT.Value);
                                    objDmucDichvukcb.IdDoituongKcb = Utility.Int16Dbnull(cboDoituong.SelectedValue, -1);
                                    DmucDoituongkcb objectType = DmucDoituongkcb.FetchByID(objDmucDichvukcb.IdDoituongKcb);
                                    if (objectType != null)
                                    {
                                        objDmucDichvukcb.MaDoituongKcb = Utility.sDbnull(objectType.MaDoituongKcb, "");
                                        objDmucDichvukcb.IdLoaidoituongKcb = objectType.IdLoaidoituongKcb;
                                    }
                                    else
                                    {
                                        objDmucDichvukcb.MaDoituongKcb = "ALL";
                                        objDmucDichvukcb.IdLoaidoituongKcb = -1;
                                    }




                                    objDmucDichvukcb.IdPhongkham = Utility.Int16Dbnull(cboRoomDept.SelectedValue, 0);
                                    objDmucDichvukcb.PhuthuDungtuyen = Utility.DecimaltoDbnull(txtPTDT.Text, 0);
                                    objDmucDichvukcb.PhuthuTraituyen = Utility.DecimaltoDbnull(txtPTTT.Text, 0);
                                    objDmucDichvukcb.DonGia = Utility.DecimaltoDbnull(objKieukham.DonGia, 0);
                                    objDmucDichvukcb.DongiaNgoaigio = 0;
                                    objDmucDichvukcb.PhuthuNgoaigio = 0;
                                    objDmucDichvukcb.TuTuc = Utility.Bool2byte(chkTutuc.Checked);
                                    objDmucDichvukcb.IsNew = true;
                                    objDmucDichvukcb.Save();

                                }
                            
                        }
                    }
                }
                else//Thêm tất cả các phòng khám x kiểu khám trừ các cặp đã tồn tại
                {
                    foreach (DataRow drkk in dtKieukham.Rows)
                    {
                        foreach (DataRow drpk in dtPK.Rows)
                        {
                            DataTable dtCheck = SPs.DmucDichvukcbKiemtratontai(Utility.Int32Dbnull(drkk["id_kieukham"], -1), Utility.Int32Dbnull(drpk[DmucKhoaphong.Columns.IdKhoaphong])).GetDataSet().Tables[0];
                            if (dtCheck.Rows.Count <= 0)
                            {
                                if (Utility.Int32Dbnull(drpk[DmucKhoaphong.Columns.IdKhoaphong]) > 0)
                                {
                                    DmucKieukham objKieukham = DmucKieukham.FetchByID(Utility.Int32Dbnull(drkk["id_kieukham"], -1));
                                    if (objKieukham != null)
                                    {
                                        var objDmucDichvukcb = new DmucDichvukcb();
                                        objDmucDichvukcb.NhomBaocao = Utility.sDbnull(objKieukham.NhomBaocao, "");


                                        objDmucDichvukcb.MaDichvukcb = string.Format("{0}-{1}", objKieukham.MaKieukham, Utility.sDbnull(drpk[DmucKhoaphong.Columns.IdKhoaphong]));
                                        objDmucDichvukcb.MaBhyt = Utility.sDbnull(txtMaBhyt.Text.Trim(), "");
                                        objDmucDichvukcb.MaGia = Utility.sDbnull(txtmagiabhyt.Text.Trim(), "");
                                        objDmucDichvukcb.HoatDong = chkhoatdong.Checked;
                                        objDmucDichvukcb.TenDichvukcb = objKieukham.TenKieukham;
                                        objDmucDichvukcb.IdKieukham = Utility.Int16Dbnull(drkk["id_kieukham"], -1);
                                        objDmucDichvukcb.IdKhoaphong = Utility.Int16Dbnull(cboDepartment1.SelectedValue, -1);
                                        objDmucDichvukcb.IdBacsy = Convert.ToInt16(cboBacSy.Items.Count > 0
                                                                                       ? Utility.Int16Dbnull(cboBacSy.SelectedValue, -1)
                                                                                       : -1);
                                        objDmucDichvukcb.MotaThem = Utility.DoTrim(txtDesc.Text);
                                        objDmucDichvukcb.SttHthi = Utility.Int16Dbnull(nmrSTT.Value);
                                        objDmucDichvukcb.IdDoituongKcb = Utility.Int16Dbnull(cboDoituong.SelectedValue, -1);
                                        DmucDoituongkcb objectType = DmucDoituongkcb.FetchByID(objDmucDichvukcb.IdDoituongKcb);
                                        if (objectType != null)
                                        {
                                            objDmucDichvukcb.MaDoituongKcb = Utility.sDbnull(objectType.MaDoituongKcb, "");
                                            objDmucDichvukcb.IdLoaidoituongKcb = objectType.IdLoaidoituongKcb;
                                        }
                                        else
                                        {
                                            objDmucDichvukcb.MaDoituongKcb = "ALL";
                                            objDmucDichvukcb.IdLoaidoituongKcb = -1;
                                        }




                                        objDmucDichvukcb.IdPhongkham = Utility.Int16Dbnull(drpk[DmucKhoaphong.Columns.IdKhoaphong]);
                                        objDmucDichvukcb.PhuthuDungtuyen = Utility.DecimaltoDbnull(txtPTDT.Text, 0);
                                        objDmucDichvukcb.PhuthuTraituyen = Utility.DecimaltoDbnull(txtPTTT.Text, 0);
                                        objDmucDichvukcb.DonGia = Utility.DecimaltoDbnull(objKieukham.DonGia, 0);
                                        objDmucDichvukcb.DongiaNgoaigio = 0;
                                        objDmucDichvukcb.PhuthuNgoaigio = 0;
                                        objDmucDichvukcb.TuTuc = Utility.Bool2byte(chkTutuc.Checked);
                                        objDmucDichvukcb.IsNew = true;
                                        objDmucDichvukcb.Save();
                                        //DataRow dr = m_dtDataRelation.NewRow();
                                        //dr[DmucDichvukcb.Columns.IdDichvukcb] = Utility.Int32Dbnull(
                                        //    _Query.GetMax(DmucDichvukcb.Columns.IdDichvukcb), -1);
                                        //dr[DmucDichvukcb.Columns.IdDoituongKcb] = Utility.DecimaltoDbnull(cboDoituong.SelectedValue);
                                        //dr[DmucDichvukcb.Columns.IdKhoaphong] = Utility.Int16Dbnull(cboDepartment1.SelectedValue, -1);
                                        //dr[DmucDichvukcb.Columns.IdPhongkham] = Utility.Int16Dbnull(cboRoomDept.SelectedValue, -1);
                                        //dr[DmucDichvukcb.Columns.IdBacsy] = cboBacSy.Items.Count > 0
                                        //                                        ? Utility.Int16Dbnull(cboBacSy.SelectedValue, -1)
                                        //                                        : -1;
                                        //dr[DmucDichvukcb.Columns.DonGia] = Utility.DecimaltoDbnull(txtDongia.Text, 0);
                                        //dr[DmucDichvukcb.Columns.PhuthuDungtuyen] = Utility.DecimaltoDbnull(txtPTDT.Text, 0);
                                        //dr[DmucDichvukcb.Columns.PhuthuTraituyen] = Utility.DecimaltoDbnull(txtPTTT.Text, 0);
                                        //dr[DmucDichvukcb.Columns.DongiaNgoaigio] = Utility.DecimaltoDbnull(txtGiangoaigio.Text, 0);
                                        //dr[DmucDichvukcb.Columns.PhuthuNgoaigio] = Utility.DecimaltoDbnull(txtPhuthungoaigio.Text, 0);

                                        //dr[DmucDichvukcb.Columns.MotaThem] = Utility.DoTrim(txtDesc.Text);
                                        //dr[DmucDichvukcb.Columns.SttHthi] = Utility.Int16Dbnull(nmrSTT.Value, 1);


                                        //dr[DmucDichvukcb.Columns.TuTuc] = Utility.Bool2byte(chkTutuc.Checked);
                                        //dr[DmucDichvukcb.Columns.HoatDong] = Utility.Bool2byte(chkhoatdong.Checked);
                                        //dr["ten_phong"] = cboRoomDept.Text;
                                        //dr[DmucDichvukcb.Columns.IdKieukham] = Utility.Int16Dbnull(cboLoaiKham.SelectedValue, -1);
                                        //dr[DmucKieukham.Columns.TenKieukham] = cboLoaiKham.Text;
                                        //dr[DmucDichvukcb.Columns.MaDichvukcb] = txtCode.Text.Trim();
                                        //dr[DmucDichvukcb.Columns.MaBhyt] = txtMaBhyt.Text.Trim();
                                        //dr[DmucDichvukcb.Columns.MaGia] = txtmagiabhyt.Text.Trim();
                                        //dr[DmucDichvukcb.Columns.TenDichvukcb] = txtName.Text.Trim();
                                        //dr["ten_khoa"] = cboDepartment1.SelectedIndex <= -1 ? "Tất cả các khoa KCB" : cboDepartment1.Text;
                                        //dr["ten_bacsi"] = cboBacSy.SelectedIndex > 0 ? cboBacSy.Text : "";
                                        //dr[DmucDoituongkcb.Columns.TenDoituongKcb] = objectType != null ? cboDoituong.Text : "Tất cả các đối tượng";

                                        //m_dtDataRelation.Rows.Add(dr);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            
            
            Utility.ShowMsg("Bạn đã thêm toàn bộ công khám trong hệ thống");
            //this.Close();
        }
    }
}