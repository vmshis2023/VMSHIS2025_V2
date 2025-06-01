using System;
using System.Data;
using System.Windows.Forms;
using SubSonic;
using VMS.HIS.DAL;
using VNS.Libs;
using System.Linq;
using System.Transactions;
using System.Collections.Generic;
namespace VNS.HIS.UI.DANHMUC
{
    public partial class frm_themmoi_phongban : Form
    {
        private readonly Query m_Query = DmucKhoaphong.CreateQuery();

        #region "THUOC TINH"

        public DataRow drKhoaphong;
        public DataTable dsKhoaphong = new DataTable();
        public DataGridView grdList = new DataGridView();
        public action m_enAction = action.Insert;
        private DataTable m_dtKhoThuoc = new DataTable();
        #endregion

        #region "THUOC TINH KHAI BAO PRIVATE"

        private DataTable dtobjKhoaphong = new DataTable();

        #endregion

        #region "Contructor"

        public frm_themmoi_phongban()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            txtDepartment_Name.LostFocus += txtDepartment_Name_LostFocus;
            KeyPreview = true;
            //cboDeptType.SelectedIndex = 0;
            KeyDown += frm_themmoi_phongban_KeyDown;
            //sysColor.BackColor = globalVariables.SystemColor;
            txtDepartment_Code.CharacterCasing = CharacterCasing.Upper;
            txtDonvitinh._OnShowData += txtDonvitinh__OnShowData;
            THU_VIEN_CHUNG.EnableBHYT(this);
        }

        private void txtDonvitinh__OnShowData()
        {
            var _DMUC_DCHUNG = new DMUC_DCHUNG("DONVITINH");
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtDonvitinh.myCode;
                txtDonvitinh.Init();
                txtDonvitinh.SetCode(oldCode);
                txtDonvitinh.Focus();
            }
        }

        #endregion

        /// <summary>
        /// hàm thực hiện thoát Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// hàm thực hiện dùng phím tắt của Form thực hiện
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_themmoi_phongban_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.Escape) cmdThoat.PerformClick();
            if (e.Control && e.KeyCode == Keys.S) cmdGhi.PerformClick();
        }

        /// <summary>
        /// hàm thực hiện load thông tin khi load Form lên
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_themmoi_phongban_Load(object sender, EventArgs e)
        {
            BindParent_ID();
            LaydanhsachBacsi();
            txtDonvitinh.Init();
            txtCosoKCB.Init();
            cboParent_ID.SelectedIndex = 0;
            m_dtKhoThuoc = CommonLoadDuoc.LAYDANHMUCKHO(-1,"ALL", "ALL", "ALL", 100, 100, 1);// CommonLoadDuoc.LAYTHONGTIN_KHOTHUOCVaTuThuoc();
            Utility.SetDataSourceForDataGridEx(grdKhoThuoc, m_dtKhoThuoc, false, true, "1=1", TDmucKho.Columns.SttHthi);
            cbomaloaikcb.SelectedIndex = 0;
            if (m_enAction == action.Update) GetData();
            if (string.IsNullOrEmpty(txtID.Text))
                txtintOrder.Text =
                    Utility.sDbnull(
                        new Select(Aggregate.Max(DmucKhoaphong.Columns.SttHthi)).From(DmucKhoaphong.Schema).
                            ExecuteScalar<int>() + 1);
        }
        private void LaydanhsachBacsi()
        {
            try
            {
                DataTable dtBacsi = THU_VIEN_CHUNG.LaydanhsachBacsi(-1, -1);
                txtTruongkhoa.Init(dtBacsi, new List<string>() { DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.MaNhanvien, DmucNhanvien.Columns.TenNhanvien });
            }
            catch (Exception exception)
            {
                // throw;
            }
        }
        /// <summary>
        /// hàm thực hiện Ghi thông tin khoa phòng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdGhi_Click(object sender, EventArgs e)
        {
            if (!isValidData()) return;
            switch (m_enAction)
            {
                case action.Insert:
                    Insert();
                    break;
                case action.Update:
                    Update();
                    Close();
                    break;
            }
           
        }

        /// <summary>
        /// hàm thực hiện thêm thông tin 
        /// </summary>
        private void Insert()
        {
            try
            {
                Int16 result = 0;
                result = CreateobjKhoaphong();
                if (result > 0)
                {
                    ProcessData(chkParent.Checked ? Utility.Int32Dbnull(cboParent_ID.SelectedValue, 0) : 0);
                    Utility.SetMsg(lblMsg, "Bạn thực hiện cập nhập thông tin thành công. Mời bạn tiếp tục thêm mới", false);
                    if (chkThemmoi.Checked)
                        ResetNew();
                    else
                        this.Close();
                    //
                }
                else
                {
                    Utility.ShowMsg("Lỗi trong quá trình cập nhập thông tin");
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
        }
        void ResetNew()
        {
            m_enAction = action.Insert;
            txtDepartment_Code.Clear();
            txtID.Clear();
            txtDepartment_Name.Clear();
            txtDeptFee.Text = "0";
            txtDesc.Clear();
            txtDonvitinh._Text = "";
            txtintOrder.Value += 1;
            txtMaphongXepStt.Clear();
            txtTIEN_TAM_UNG.Text = "0";
            grdKhoThuoc.UnCheckAllRecords();
            txtDepartment_Code.Focus();
        }
        /// <summary>
        /// hàm thực hiện xử lý thông tin khoa phòng
        /// </summary>
        /// <param name="Parent_ID"></param>
        private void ProcessData(int Parent_ID)
        {
            int departId = Utility.Int32Dbnull(m_Query.GetMax(DmucKhoaphong.Columns.IdKhoaphong));

            DataRow dr = dsKhoaphong.NewRow();
            dr[DmucKhoaphong.Columns.IdKhoaphong] = departId;
            dr[DmucKhoaphong.Columns.MaPhongStt] = Utility.sDbnull(txtMaphongXepStt.Text);
            dr[DmucKhoaphong.Columns.MaXml] = Utility.sDbnull(txtMaBoYte.Text);
            dr[DmucKhoaphong.Columns.ChiDan] = Utility.sDbnull(txtPhong_Thien.Text);
            dr["ten_khoaphong_captren"] = chkParent.Checked ? cboParent_ID.Text : "CÁC KHOA";
            // dr["DeptType_Name"] = chkNoitru.Checked?"Nội trú":"Ngoại trú";
            dr[DmucKhoaphong.Columns.TenKhoaphong] = txtDepartment_Name.Text;
            dr[DmucKhoaphong.Columns.MaKhoaphong] = txtDepartment_Code.Text;
            dr[DmucKhoaphong.Columns.NoitruNgoaitru] = optNgoaitru.Checked
                                                           ? "NGOAI"
                                                           : (optNoitru.Checked ? "NOI" : "CAHAI");
            dr[DmucKhoaphong.Columns.KieuKhoaphong] = optPhong.Checked ? "PHONG" : "KHOA";
            dr["ten_kieukhoaphong"] = optPhong.Checked ? "Phòng" : "Khoa";
            dr[DmucKhoaphong.Columns.MotaThem] = txtDesc.Text;
            dr[DmucKhoaphong.Columns.SttHthi] = txtintOrder.Value;
            dr[DmucKhoaphong.Columns.MaCha] = Parent_ID;
            dr[DmucKhoaphong.Columns.DonGia] = Utility.DecimaltoDbnull(txtDeptFee.Text, 0);
            dr[DmucKhoaphong.Columns.TamUng] = Utility.DecimaltoDbnull(txtTIEN_TAM_UNG.Text, 0);
            dr[DmucKhoaphong.Columns.NgayTao] = globalVariables.SysDate;
            dr[DmucKhoaphong.Columns.NguoiTao] = globalVariables.UserName;
            dr[DmucKhoaphong.Columns.LaKhoacapcuu] = chkKhoaCapCuu.Checked ? 1 : 0;
            dr[DmucKhoaphong.Columns.MaDonvitinh] = txtDonvitinh.myCode;
            dr[DmucKhoaphong.Columns.PhongChucnang] = (byte) (optChucnang.Checked ? 1 : 0);
            dr[DmucKhoaphong.Columns.IdTruongkhoa] = Utility.Int32Dbnull(txtTruongkhoa.MyID, 0);
            dr["ten_donvitinh"] = txtDonvitinh.Text;
            dr["ten_cosokcb"] = txtCosoKCB.Text;
            dr["ten_truongkhoa"] = txtTruongkhoa.Text;
            dsKhoaphong.Rows.Add(dr);
            dsKhoaphong.AcceptChanges();
        }

        /// <summary>
        /// Hàm thực hiện khởi tạo thôngtin của khoa phòng
        /// </summary>
        /// <returns></returns>
        private Int16 CreateobjKhoaphong()
        {
            try
            {
                short Parent_ID = 0;

                var objKhoaphong = new DmucKhoaphong();
                objKhoaphong.MaKhoaphong = txtDepartment_Code.Text;
                objKhoaphong.SttHthi = Utility.Int16Dbnull(txtintOrder.Value, 0);
                objKhoaphong.TenKhoaphong = txtDepartment_Name.Text;
                objKhoaphong.DonGia = Utility.DecimaltoDbnull(txtDeptFee.Text, 0);
                objKhoaphong.MotaThem = Utility.sDbnull(txtDesc.Text, "");
                objKhoaphong.NoitruNgoaitru = optNgoaitru.Checked ? "NGOAI" : (optNoitru.Checked ? "NOI" : "CAHAI");
                objKhoaphong.MaCha =
                    (short?) (chkParent.Checked ? Utility.Int16Dbnull(cboParent_ID.SelectedValue, -1) : 0);
                objKhoaphong.TamUng = Utility.DecimaltoDbnull(txtTIEN_TAM_UNG.Text, 0);
                objKhoaphong.MaDonvitinh = txtDonvitinh.myCode;
                objKhoaphong.NgayTao = globalVariables.SysDate;
                objKhoaphong.NguoiTao = globalVariables.UserName;
                objKhoaphong.MaPhongStt = Utility.sDbnull(txtMaphongXepStt.Text);
                objKhoaphong.ChiDan = Utility.sDbnull(txtPhong_Thien.Text);
                objKhoaphong.IdTruongkhoa = Utility.Int32Dbnull(txtTruongkhoa.MyID,-1);
                objKhoaphong.KieuKhoaphong = optPhong.Checked ? "PHONG" : "KHOA";
                objKhoaphong.TrangThai = chkTrangthai.Checked;
                objKhoaphong.PhongChucnang = (byte) (optChucnang.Checked ? 1 : 0);
                objKhoaphong.HoatdongTheotang = Utility.Bool2byte(chkHoatdongtheotang.Checked);
                objKhoaphong.LaKhoacapcuu = (byte) (chkKhoaCapCuu.Checked ? 1 : 0);
                objKhoaphong.MaCoso = txtCosoKCB.myCode;
                objKhoaphong.MaXml = Utility.sDbnull(txtMaBoYte.Text);
                objKhoaphong.MaLoaikcb = cbomaloaikcb.SelectedValue.ToString();
                objKhoaphong.AnhxaBhyt = chkAnhxa.Checked;
                objKhoaphong.Ldlk =Utility.Int32Dbnull( nmrLDLK.Value,0);
                objKhoaphong.IsNew = true;
                QheKhoaKhoCollection lstQhe = GetQuanheKhoaKho(-1);
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        objKhoaphong.Save();
                        new Delete().From(QheKhoaKho.Schema).Where(QheKhoaKho.Columns.IdKhoa).IsEqualTo(objKhoaphong.IdKhoaphong).Execute();

                        foreach (QheKhoaKho obj in lstQhe)
                        {
                            obj.IdKhoa = objKhoaphong.IdKhoaphong;
                        }
                        lstQhe.SaveAll();
                    }
                    scope.Complete();
                }
                return objKhoaphong.IdKhoaphong;
            }
            catch(Exception ex)
            {
                Utility.ShowMsg(ex.ToString());
                return -1;
            }
        }


        /// <summary>
        /// hàm thực hiện Update thông tin
        /// </summary>
        private void Update()
        {
            try
            {
                int Parent_ID = 0;
                int record = 0;
                QheKhoaKhoCollection lstQhe = GetQuanheKhoaKho(Utility.Int16Dbnull(txtID.Text, -1));
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        record = new Update(DmucKhoaphong.Schema)
                            .Set(DmucKhoaphong.Columns.MaPhongStt).EqualTo(Utility.sDbnull(txtMaphongXepStt.Text))
                             .Set(DmucKhoaphong.Columns.MaXml).EqualTo(Utility.sDbnull(txtMaBoYte.Text))
                            .Set(DmucKhoaphong.Columns.ChiDan).EqualTo(txtPhong_Thien.Text)
                            .Set(DmucKhoaphong.Columns.NguoiSua).EqualTo(globalVariables.UserName)
                            .Set(DmucKhoaphong.Columns.NgaySua).EqualTo(globalVariables.SysDate)
                            .Set(DmucKhoaphong.Columns.TenKhoaphong).EqualTo(txtDepartment_Name.Text)
                            .Set(DmucKhoaphong.Columns.LaKhoacapcuu).EqualTo(chkKhoaCapCuu.Checked ? 1 : 0)
                            .Set(DmucKhoaphong.Columns.MaKhoaphong).EqualTo(txtDepartment_Code.Text)
                            .Set(DmucKhoaphong.Columns.PhongChucnang).EqualTo(optChucnang.Checked ? 1 : 0)
                            .Set(DmucKhoaphong.Columns.KieuKhoaphong).EqualTo(optPhong.Checked ? "PHONG" : "KHOA")
                            .Set(DmucKhoaphong.Columns.NoitruNgoaitru).EqualTo(optNgoaitru.Checked ? "NGOAI" : (optNoitru.Checked ? "NOI" : "CAHAI"))
                            .Set(DmucKhoaphong.Columns.SttHthi).EqualTo(txtintOrder.Value)
                            .Set(DmucKhoaphong.Columns.MotaThem).EqualTo(txtDesc.Text)
                            .Set(DmucKhoaphong.Columns.MaCoso).EqualTo(txtCosoKCB.myCode)
                            .Set(DmucKhoaphong.Columns.MaCha).EqualTo(chkParent.Checked ? Utility.Int32Dbnull(cboParent_ID.SelectedValue, -1) : 0)
                            .Set(DmucKhoaphong.Columns.DonGia).EqualTo(Utility.DecimaltoDbnull(txtDeptFee.Text, 0))
                            .Set(DmucKhoaphong.Columns.TamUng).EqualTo(Utility.DecimaltoDbnull(txtTIEN_TAM_UNG.Text, 0))
                            .Set(DmucKhoaphong.Columns.MaDonvitinh).EqualTo(txtDonvitinh.myCode)
                            .Set(DmucKhoaphong.Columns.TrangThai).EqualTo(chkTrangthai.Checked)
                            .Set(DmucKhoaphong.Columns.AnhxaBhyt).EqualTo(chkAnhxa.Checked)
                             .Set(DmucKhoaphong.Columns.Ldlk).EqualTo(Utility.Int32Dbnull(nmrLDLK.Value, 0))
                             .Set(DmucKhoaphong.Columns.HoatdongTheotang).EqualTo(Utility.Bool2byte(chkHoatdongtheotang.Checked))
                            .Set(DmucKhoaphong.Columns.IdTruongkhoa).EqualTo(Utility.Int32Dbnull(txtTruongkhoa.MyID))
                            .Where(DmucKhoaphong.Columns.IdKhoaphong).IsEqualTo(Utility.Int32Dbnull(txtID.Text, -1))
                            .Execute();
                        Utility.Log(this.Name, globalVariables.UserName, string.Format("Cập nhật thông tin khoa phòng Id={0} thành công", txtID.Text), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                        new Delete().From(QheKhoaKho.Schema).Where(QheKhoaKho.Columns.IdKhoa).IsEqualTo(Utility.Int16Dbnull(txtID.Text, -1)).Execute();
                        foreach (QheKhoaKho obj in lstQhe)
                        {
                            obj.IdKhoa = Utility.Int16Dbnull(txtID.Text, -1);
                        }
                        lstQhe.SaveAll();
                    }
                    scope.Complete();
                }
                if (record > 0)
                {
                    //Utility.ShowMsg("Bạn thực hiện cập nhập thông tin thành công");
                    foreach (DataRow dr in dsKhoaphong.Rows)
                    {
                        if (dr[DmucKhoaphong.Columns.IdKhoaphong].ToString() == Utility.sDbnull(txtID.Text, -1))
                        {
                            dr[DmucKhoaphong.Columns.MaPhongStt] = Utility.sDbnull(txtMaphongXepStt.Text);
                            dr[DmucKhoaphong.Columns.MaXml] = Utility.sDbnull(txtMaBoYte.Text);
                            dr[DmucKhoaphong.Columns.ChiDan] = Utility.sDbnull(txtPhong_Thien.Text);
                            dr[DmucKhoaphong.Columns.NgaySua] = globalVariables.SysDate;
                            dr[DmucKhoaphong.Columns.NguoiSua] = globalVariables.UserName;
                            dr[DmucKhoaphong.Columns.TenKhoaphong] = Utility.sDbnull(txtDepartment_Name.Text, "");
                            dr[DmucKhoaphong.Columns.MaKhoaphong] = Utility.sDbnull(txtDepartment_Code.Text, "");
                            dr[DmucKhoaphong.Columns.LaKhoacapcuu] = chkKhoaCapCuu.Checked ? 1 : 0;
                            dr[DmucKhoaphong.Columns.NoitruNgoaitru] = optNgoaitru.Checked
                                                                           ? "NGOAI"
                                                                           : (optNoitru.Checked ? "NOI" : "CAHAI");
                            dr[DmucKhoaphong.Columns.SttHthi] = Utility.Int32Dbnull(txtintOrder.Text, 0);
                            dr[DmucKhoaphong.Columns.IdTruongkhoa] = Utility.Int32Dbnull(txtTruongkhoa.MyID, 0);
                            dr[DmucKhoaphong.Columns.MaCha] = chkParent.Checked
                                                                  ? Utility.Int32Dbnull(cboParent_ID.SelectedValue, -1)
                                                                  : 0;
                            dr[DmucKhoaphong.Columns.DonGia] = Utility.DecimaltoDbnull(txtDeptFee.Text, 0);
                            dr[DmucKhoaphong.Columns.MotaThem] = Utility.sDbnull(txtDesc.Text, "");
                            dr[DmucKhoaphong.Columns.TamUng] = Utility.DecimaltoDbnull(txtTIEN_TAM_UNG.Text, 0);
                            dr[DmucKhoaphong.Columns.KieuKhoaphong] = optPhong.Checked ? "PHONG" : "KHOA";
                            dr[DmucKhoaphong.Columns.MaDonvitinh] = txtDonvitinh.myCode;
                            dr[DmucKhoaphong.Columns.PhongChucnang] = (byte) (optChucnang.Checked ? 1 : 0);
                            dr["ten_donvitinh"] = txtDonvitinh.Text;
                            dr["ten_cosokcb"] = txtCosoKCB.Text;
                            dr["ten_truongkhoa"] = txtTruongkhoa.Text;
                            dr["ten_kieukhoaphong"] = optPhong.Checked ? "Phòng" : "Khoa";
                            dr["ten_khoaphong_captren"] = chkParent.Checked ? cboParent_ID.Text : "CÁC KHOA";
                            break;
                        }
                    }
                    dsKhoaphong.AcceptChanges();
                }
                else
                {
                    Utility.ShowMsg("Lỗi trong quá trình cập nhập thông tin ");
                }
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            //Utility.GotoNewRow(grdList,"coDmucKhoaphong_ID",txtID.Text);
        }

        /// <summary>
        /// chỉ cho  nhập số
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDeptFee_TextChanged(object sender, EventArgs e)
        {
            // Utility.FormatCurrencyHIS(txtDeptFee);
        }

        private void txtDeptFee_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Utility.OnlyDigit(e);
        }

        private void chkParent_CheckedChanged(object sender, EventArgs e)
        {
            cboParent_ID.Enabled = chkParent.Checked;
        }

        private void txtDepartment_Name_LostFocus(object sender, EventArgs e)
        {
            //txtDepartment_Name.Text = Utility.chuanhoachuoi(txtDepartment_Name.Text);
        }//

        /// <summary>
        /// chỉ cho nhập số
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTIEN_TAM_UNG_TextChanged(object sender, EventArgs e)
        {
            // Utility.FormatCurrencyHIS(txtTIEN_TAM_UNG);
        }

        private void txtTIEN_TAM_UNG_KeyPress(object sender, KeyPressEventArgs e)
        {
            //Utility.OnlyDigit(e);
        }

        /// <summary>
        /// HÀM THỰC HIỆN VIỆC CHO PHÉP BẠN THỰC HIỆN VIỆC KIỂM TRA THÔNG TIN CỦA PHẦN NỘI TRÚ HOẶC NGOẠI TRÚ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboParent_ID_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        #region "HAM DUNG CHUNG"

        /// <summary>
        /// hàm thực hiện set validate các trường phải nhập chỉ số
        /// </summary>
        /// <summary>
        /// hàm thực hiện lấy thông tin đơn vị cấp trên
        /// </summary>
        private void BindParent_ID()
        {
            dtobjKhoaphong =
                new Select().From(DmucKhoaphong.Schema).Where(DmucKhoaphong.Columns.MaCha).IsEqualTo(0).And(DmucKhoaphong.Columns.KieuKhoaphong).IsEqualTo("KHOA")
                    .OrderDesc(DmucKhoaphong.Columns.SttHthi).ExecuteDataSet().Tables[0];
            DataBinding.BindDataCombox(cboParent_ID, dtobjKhoaphong, DmucKhoaphong.Columns.IdKhoaphong,
                                       DmucKhoaphong.Columns.TenKhoaphong);
        }

        /// <summary>
        /// hàm thực hiện validate thông tin
        /// </summary>
        /// <returns></returns>
        private bool isValidData()
        {
            Utility.SetMsg(lblMsg, "", true);


            if (string.IsNullOrEmpty(txtDepartment_Code.Text.Trim()))
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập mã khoa phòng", true);
                txtDepartment_Code.Focus();
                return false;
            }
            if (Utility.sDbnull(txtCosoKCB.myCode)=="-1")
            {
                Utility.SetMsg(lblMsg, "Bạn phải Chọn cơ sở KCB", true);
                txtCosoKCB.Focus();
                return false;
            }
            SqlQuery q =
                new Select().From(DmucKhoaphong.Schema).Where(DmucKhoaphong.Columns.MaKhoaphong).IsEqualTo(
                    Utility.DoTrim(txtDepartment_Code.Text));
            if (m_enAction == action.Update)
                q.And(DmucKhoaphong.Columns.IdKhoaphong).IsNotEqualTo(Utility.Int32Dbnull(txtID.Text, -1));

            if (q.GetRecordCount() > 0)
            {
                Utility.SetMsg(lblMsg, "Đã tồn tại khoa phòng có mã " + Utility.DoTrim(txtDepartment_Code.Text) + ". Mời bạn nhập mã khác", true);
                txtDepartment_Code.Focus();
                return false;
            }
            q =
                new Select().From(DmucKhoaphong.Schema).Where(DmucKhoaphong.Columns.MaXml).IsEqualTo(
                    Utility.DoTrim(txtMaBoYte.Text));
            if (m_enAction == action.Update)
                q.And(DmucKhoaphong.Columns.IdKhoaphong).IsNotEqualTo(Utility.Int32Dbnull(txtID.Text, -1));

            if (q.GetRecordCount() > 0)
            {
                Utility.SetMsg(lblMsg, "Đã tồn tại khoa phòng có mã XML" + Utility.DoTrim(txtMaBoYte.Text) + ". Mời bạn nhập mã khác", true);
                txtMaBoYte.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtDepartment_Name.Text.Trim()))
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập tên khoa phòng", true);
                txtDepartment_Name.Focus();
                return false;
            }
            if (chkParent.Checked)
            {
                if (cboParent_ID.SelectedIndex <= 0)
                {
                    Utility.SetMsg(lblMsg, "Bạn phải chọn khoa/phòng cấp trên", true);
                    cboParent_ID.Focus();
                    return false;
                }
            }
            if (txtDonvitinh.myCode == "-1")
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn đơn vị tính", true);
                txtDonvitinh.Focus();
                return false;
            }
            return true;
        }
        private void GetData()
        {
            DmucKhoaphong objKhoaphong = DmucKhoaphong.FetchByID(Utility.Int32Dbnull(txtID.Text, -1));
            if (objKhoaphong != null)
            {
                txtDepartment_Code.Text = objKhoaphong.MaKhoaphong;
                txtMaBoYte.Text = objKhoaphong.MaXml;
                txtDepartment_Name.Text = objKhoaphong.TenKhoaphong;
                txtintOrder.Text = Utility.sDbnull(objKhoaphong.SttHthi);
                txtTruongkhoa.SetId(objKhoaphong.IdKhoaphong);
                chkParent.Checked = objKhoaphong.MaCha.ToString() == "0" ? false : true;
                chkHoatdongtheotang.Checked = Utility.Byte2Bool(objKhoaphong.HoatdongTheotang);
                cboParent_ID.SelectedIndex =
                    Utility.GetSelectedIndex(cboParent_ID, Utility.sDbnull(objKhoaphong.MaCha, "-1"));
                txtDesc.Text = Utility.sDbnull(objKhoaphong.MotaThem, "");
                optPhong.Checked = objKhoaphong.KieuKhoaphong == "PHONG";
                if (objKhoaphong.NoitruNgoaitru == "NOI") optNoitru.Checked = true;
                else if (objKhoaphong.NoitruNgoaitru == "NGOAI") optNgoaitru.Checked = true;
                else optKhac.Checked = true;
                if (objKhoaphong.PhongChucnang == 1) optChucnang.Checked = true;
                else optChuyenmon.Checked = true;
                // cboParent_ID.Enabled=Utility.sDbnull(drKhoaphong[DmucKhoaphong.Columns.MaCha], "-1") == "0" ? true : false;
                // cboParent_ID.SelectedIndex = Utility.GetSelectedIndex(cboParent_ID,Utility.sDbnull(objKhoaphong.MaCha,-1));
                txtTIEN_TAM_UNG.Text = Utility.sDbnull(objKhoaphong.TamUng, 0);
                txtDeptFee.Text = Utility.sDbnull(objKhoaphong.DonGia, "0");
                chkKhoaCapCuu.Checked = Utility.Int32Dbnull(objKhoaphong.LaKhoacapcuu, 0) == 1;
                txtDonvitinh.SetCode(objKhoaphong.MaDonvitinh);
                txtMaphongXepStt.Text = Utility.sDbnull(objKhoaphong.MaPhongStt);
                txtPhong_Thien.Text = Utility.sDbnull(objKhoaphong.ChiDan);
                chkTrangthai.Checked = Utility.Bool2Bool(objKhoaphong.TrangThai);
                chkAnhxa.Checked = Utility.Bool2Bool(objKhoaphong.AnhxaBhyt);
                nmrLDLK.Value =Utility.DecimaltoDbnull( objKhoaphong.Ldlk,0);
                txtCosoKCB.SetCode(objKhoaphong.MaCoso);
                cbomaloaikcb.SelectedIndex = Utility.GetSelectedIndex(cbomaloaikcb, objKhoaphong.MaLoaikcb);
                LoadQuanHeKhoaKho();
            }
        }

        private QheKhoaKhoCollection GetQuanheKhoaKho(short IdKhoaphong)
        {
            QheKhoaKhoCollection lst = new QheKhoaKhoCollection();

            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdKhoThuoc.GetCheckedRows())
            {
                QheKhoaKho objQheKhoaKho = new QheKhoaKho();
                objQheKhoaKho.IdKho = Utility.Int16Dbnull(gridExRow.Cells[TDmucKho.Columns.IdKho].Value);
                objQheKhoaKho.IdKhoa = IdKhoaphong;
                objQheKhoaKho.IsNew = true;
                lst.Add(objQheKhoaKho);
            }
            return lst;
        }
        private void LoadQuanHeKhoaKho()
        {
            QheKhoaKhoCollection lstQheKhoaKhoCollection = new Select().From(QheKhoaKho.Schema)
                .Where(QheKhoaKho.Columns.IdKhoa).IsEqualTo(Utility.Int32Dbnull(txtID.Text)).
                ExecuteAsCollection<QheKhoaKhoCollection>();
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdKhoThuoc.GetDataRows())
            {
                gridExRow.BeginEdit();
                var query = from kho in lstQheKhoaKhoCollection.AsEnumerable()
                            where kho.IdKho == Utility.Int32Dbnull(gridExRow.Cells[TDmucKho.Columns.IdKho].Value)
                            select kho;
                if (query.Count() > 0)
                {
                    gridExRow.Cells["IsChon"].Value = 1;
                    gridExRow.IsChecked = true;
                }

                else
                {
                    gridExRow.Cells["IsChon"].Value = 0;
                    gridExRow.IsChecked = false;
                }
                gridExRow.EndEdit();

            }
        }

        #endregion
    }
}