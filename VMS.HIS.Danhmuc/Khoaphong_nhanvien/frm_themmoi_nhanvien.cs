using System;
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
using SubSonic;

using SortOrder = Janus.Windows.GridEX.SortOrder;
using VNS.HIS.NGHIEPVU;
namespace VNS.HIS.UI.DANHMUC
{
    public partial class frm_themmoi_nhanvien : Form
    {
        #region "Public Variables(Class Level)
        DataTable m_dtDepartmentList = new DataTable();
        DataTable m_dtDepartmentListUp = new DataTable();
        public bool m_blnCancel = true;
        public string UserName = "";
        public DataTable p_dtStaffList=new DataTable();
        public action em_Action = action.Insert;
        /// <summary>
        /// Biến xác định xem form được gọi từ đâu
        /// true: Gọi từ Menu
        /// false: Gọi từ một Form khác và thường trả về đối tượng khi chọn trên lưới hoặc nhấn nút chọn
        /// </summary>
        public bool m_blnCallFromMenu = true;
        /// <summary>
        /// Biến trả về khi thực hiện DoubleClick trên lưới với điều kiện blnCallFromMenu=false
        /// </summary>
        public DmucNhanvien m_objObjectReturn = null;

       

        #endregion

        List<string> lstckhaucong = new List<string>();
        public frm_themmoi_nhanvien()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            InitEvents();
            txtName.LostFocus+=new EventHandler(txtName_LostFocus);
        }
        void InitEvents()
        {
            grdKhoa.SelectionChanged += new System.EventHandler(grdKhoa_SelectionChanged);
           // cbockhau.SelectedIndexChanged += cbockhau_SelectedIndexChanged;
        }

        //void cbockhau_SelectedIndexChanged(object sender, System.EventArgs e)
        //{
        //    if (cbockhau.SelectedIndex == 2)
        //    {
        //        txtCkhau.Enabled = true;
        //        txtCkhau.Focus();
        //    }
        //    else
        //        txtCkhau.Enabled = false;
        //}

        void grdKhoa_SelectionChanged(object sender, System.EventArgs e)
        {
            try
            {
                if (!Utility.isValidGrid(grdKhoa))
                {
                    m_dtPhongkham.DefaultView.RowFilter = "1=2";
                    m_dtPhongkham.AcceptChanges();
                    return;
                }
                //m_dtPhongkham.DefaultView.RowFilter = "1=1";
                m_dtPhongkham.DefaultView.RowFilter = "ma_cha=" + grdKhoa.GetValue("id_khoaphong").ToString();
                m_dtPhongkham.AcceptChanges();
            }
            catch (Exception ex)
            {


            }
        }
        private void uiGroupBox1_Click(object sender, EventArgs e)
        {
        }
        private void txtName_LostFocus(object sender, System.EventArgs e)
        {
           // txtName.Text = Utility.chuanhoachuoi(txtName.Text);
        }
        private void txtSpecMoney_KeyPress(object sender, KeyPressEventArgs e)
        {
            Utility.OnlyDigit(e);
        }
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private DataTable m_dtPhongkham=new DataTable();
        private DataTable m_dtKhoangoaitru = new DataTable();
        private DataTable m_dtKhoanoitru = new DataTable();
        private DataTable m_dtPhongkhamNoi = new DataTable();
        QueryCommand cmd = null;
        private void InitData()
        {
            try
            {
                cboGioitinh.SelectedIndex = 0;
                //Khởi tạo danh mục loại nhân viên
                lstckhaucong = THU_VIEN_CHUNG.Laygiatrithamsohethong_off("CKHAU_CONG", "10,40", false).Split(',').ToList<string>();
                cbockhau.Items.AddRange(lstckhaucong.ToArray<string>());
                DataTable v_dtStaffTypeList = THU_VIEN_CHUNG.LayDulieuDanhmucChung("LOAINHANVIEN",true);
                //  v_dtStaffTypeList =
            //   DataBinding.BindData(cboUserName,v_dtStaffTypeList.Select(""));
                cboStaffType.DataSource = v_dtStaffTypeList.DefaultView;
                v_dtStaffTypeList.DefaultView.Sort = DmucChung.Columns.SttHthi;
                cboStaffType.ValueMember = DmucChung.Columns.Ma;
                cboStaffType.DisplayMember = DmucChung.Columns.Ten;
               
                DataTable dtLoaiKCB = THU_VIEN_CHUNG.LayDulieuDanhmucChung("MA_LOAI_KCB",true);
                DataBinding.BindDataCombobox(cboMaloaiKCB, dtLoaiKCB, DmucChung.Columns.Ma, DmucChung.Columns.Ten);

                DataTable dtChucdanh = THU_VIEN_CHUNG.LayDulieuDanhmucChung("NVIEN_CHUCDANH_NN", true);
                DataBinding.BindDataCombobox(cboChucdanh, dtChucdanh, DmucChung.Columns.Ma, DmucChung.Columns.Ten);

                DataTable dtChucvu = THU_VIEN_CHUNG.LayDulieuDanhmucChung("NVIEN_CHUCVU", true);
                DataBinding.BindDataCombobox(cboChucvu, dtChucvu, DmucChung.Columns.Ma, DmucChung.Columns.Ten);

                DataTable dtVitri = THU_VIEN_CHUNG.LayDulieuDanhmucChung("NVIEN_VITRI", true);
                DataBinding.BindDataCombobox(cboVitri, dtVitri, DmucChung.Columns.Ma, DmucChung.Columns.Ten);

                //Khởi tạo danh mục phòng ban
                m_dtDepartmentList = THU_VIEN_CHUNG.Laydanhmuckhoa("ALL", -1);
                DataBinding.BindData(cboUpLevel, m_dtDepartmentList, DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong);
                cboUpLevel_SelectedIndexChanged(cboUpLevel,new EventArgs());
                //Khởi tạo danh mục User
                DataTable v_dtUserList = new DataTable();
                if(em_Action == action.Insert)
                {
                     v_dtUserList =
                        new Select().From(SysUser.Schema).Where(SysUser.Columns.PkSuid).NotIn(
                            new Select(DmucNhanvien.Columns.UserName).From(DmucNhanvien.Schema)).ExecuteDataSet().Tables
                            [0];
                    DataBinding.BindData(cboUserName, v_dtUserList, SysUser.Columns.PkSuid, SysUser.Columns.PkSuid);
                }
                else
                {
                     v_dtUserList =
                        new Select().From(SysUser.Schema).ExecuteDataSet().Tables[0];
                    DataBinding.BindData(cboUserName, v_dtUserList, SysUser.Columns.PkSuid, SysUser.Columns.PkSuid);
                }
               
                //Utility.AddColumnAlltoUserDataTable(ref v_dtUserList, SysUser.Columns.PkSuid, "");
                //v_dtUserList.DefaultView.Sort = SysUser.Columns.PkSuid+" ASC";
                txtUID.Init(v_dtUserList, new List<string>() { SysUser.Columns.PkSuid, SysUser.Columns.PkSuid, SysUser.Columns.PkSuid });


                m_dtKhoThuoc = CommonLoadDuoc.LAYDANHMUCKHO(-1,"ALL", "ALL", "ALL", 100, 100, 1);//  CommonLoadDuoc.LAYTHONGTIN_KHOTHUOCVaTuThuoc();
                Utility.SetDataSourceForDataGridEx(grdKhoThuoc, m_dtKhoThuoc, true, true, "1=1", TDmucKho.Columns.SttHthi);
                m_dtPhongkham = THU_VIEN_CHUNG.LaydanhmucPhong(0,"NGOAI","PHONG");
                m_dtPhongkhamNoi = THU_VIEN_CHUNG.LaydanhmucPhong(0, "NOI", "PHONG");
                Utility.SetDataSourceForDataGridEx(grdPhongkham, m_dtPhongkham, true, true, "1=1", VDmucKhoaphong.Columns.SttHthi);
                Utility.SetDataSourceForDataGridEx(grdPhongkhamNoitru, m_dtPhongkhamNoi, true, true, "1=1", VDmucKhoaphong.Columns.SttHthi);
                m_dtKhoangoaitru = THU_VIEN_CHUNG.Laydanhmuckhoa("NGOAI",0);
                Utility.SetDataSourceForDataGridEx(grdKhoa, m_dtKhoangoaitru, true, true, "1=1", VDmucKhoaphong.Columns.SttHthi);

                m_dtKhoanoitru = THU_VIEN_CHUNG.Laydanhmuckhoa("NOI",0);
                Utility.SetDataSourceForDataGridEx(grdKhoanoitru, m_dtKhoanoitru, true, true, "1=1", VDmucKhoaphong.Columns.SttHthi);

               
                QueryCommand cmd = SysMultiReport.CreateQuery().BuildCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandSql = "select MultiReport_ID as ma, MultiReport_Name as ten,MultiReport_Type as Loai,MultiReport_Sequence as stt_hthi,(select TOP 1 TEN from dmuc_chung where LOAI='LOAI_BAOCAOMULTI' AND MA=p.MultiReport_Type) as ten_loai from sys_multi_report p where p.trang_thai=1";
                DataTable dtbaocaomulti = DataService.GetDataSet(cmd).Tables[0];
                Utility.SetDataSourceForDataGridEx_Basic(grdbaocaomulti, dtbaocaomulti, true, true, "1=1", DmucChung.Columns.SttHthi + "," + DmucChung.Columns.Ten);
                DataTable dtQuyen = SPs.DmucQuyennguoidungGetdata(globalVariables.UserName).GetDataSet().Tables[0];// new Select().From(DmucChung.Schema).Where(DmucChung.Columns.Loai).IsEqualTo("QUYENNHANVIEN").ExecuteDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx_Basic(grdQuyen, dtQuyen, true, true, "1=1", DmucChung.Columns.SttHthi + "," + DmucChung.Columns.Ten);

                Utility.SetDataSourceForDataGridEx_Basic(grdLoaiThuoc, SPs.DmucLaydanhsachLoaithuoc("-1").GetDataSet().Tables[0], true, true, "1=1", "stt_nhomthuoc," + DmucLoaithuoc.Columns.SttHthi + "," + DmucLoaithuoc.Columns.TenLoaithuoc);
                Utility.SetDataSourceForDataGridEx_Basic(grdDichvuCls, new Select().From(DmucChung.Schema).Where(DmucChung.Columns.Loai).IsEqualTo("LOAIDICHVUCLS").ExecuteDataSet().Tables[0], true, true, "1=1", DmucChung.Columns.SttHthi + "," + DmucChung.Columns.Ten);
                DataTable dtDmucchung = new Select().From(DmucKieudmuc.Schema).ExecuteDataSet().Tables[0];

                Utility.SetDataSourceForDataGridEx_Basic(grdDmucchung, dtDmucchung, true, true, "1=1", DmucKieudmuc.Columns.TenLoai);
                DataTable dtcosokcb = THU_VIEN_CHUNG.LayDulieuDanhmucChung("COSOKCB", true);

                Utility.SetDataSourceForDataGridEx_Basic(grdCosoKCB, dtcosokcb, true, true, "1=1", DmucChung.Columns.SttHthi);
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        private DataTable m_dtKhoThuoc=new DataTable();
       
        /// <summary>
        /// hàm thực hiện load thông tin của Form khi load thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_themmoi_nhanvien_Load(object sender, EventArgs e)
        {
            InitData();
            if (em_Action == action.Update) GetData();
            SetStatusAlter();
        }

        private void frm_themmoi_nhanvien_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                Control actCtrl = Utility.getActiveControl(this);
                if (actCtrl != null && actCtrl.GetType().Equals(cboVitri.GetType()))
                {
                }
                else
                {
                    SendKeys.Send("{TAB}");
                }
            }
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.Control && e.KeyCode == Keys.S) cmdSave.PerformClick();
        }

        #region "Method of common"
        /// <summary>
        /// hàm thực hiện validate trạng thái cần nhập
        /// </summary>
        private void SetStatusAlter()
        {
            Utility.SetMsg(lblMsg, "", true);
            if(string.IsNullOrEmpty(txtStaffCode.Text.Trim()))
            {
                Utility.SetMsg(lblMsg,"Bạn nhập phải nhập mã nhân viên",true);
                txtStaffCode.Focus();


            }
            if (string.IsNullOrEmpty(txtName.Text))
            {
                Utility.SetMsg(lblMsg, "Bạn nhập tên nhân viên",true);
                txtName.Focus();


            }
            if (cboStaffType.SelectedIndex<=-1)
            {
                Utility.SetMsg(lblMsg, "Bạn nhập loại nhân viên",true);
                cboStaffType.Focus();


            }
           
        }
        private void PerformAction()
        {
            switch (em_Action)
            {
                case action.Insert:
                    if (!isValidData()) return;
                    PerformActionInsert();
                    break;
                case action.Update:
                    if (!isValidData()) return;
                    PerformActionUpdate();
                    break;
            }

        }
        private bool isValidData()
        {
            Utility.SetMsg(lblMsg, "", true);
            if (string.IsNullOrEmpty(txtStaffCode.Text))
            {
                Utility.SetMsg(lblMsg,  "Bạn phải nhập mã nhân viên",true);
                txtStaffCode.Focus();
                return false;

            }
            if (string.IsNullOrEmpty(txtName.Text))
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập tên nhân viên",true);
                txtName.Focus();
                return false;

            }
            if (cboGioitinh.SelectedIndex<0)
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập giới tính nhân viên", true);
                cboGioitinh.Focus();
                return false;

            }
            SqlQuery q = new Select().From(DmucNhanvien.Schema)
                .Where(DmucNhanvien.Columns.MaNhanvien).IsEqualTo(txtStaffCode.Text);
            if (em_Action == action.Update)
                q.And(DmucNhanvien.Columns.IdNhanvien).IsNotEqualTo(Utility.Int32Dbnull(txtID.Text, -1));
            if(q.GetRecordCount()>0)
            {
                Utility.SetMsg(lblMsg, "Tồn tại mã nhân viên",true);
                txtStaffCode.Focus();
                return false;
            }
            
            if (txtUID.MyID!="-1")
            {
                SqlQuery q2 = new Select().From(DmucNhanvien.Schema)
                    .Where(DmucNhanvien.Columns.UserName).IsEqualTo(txtUID.MyID);
                if (em_Action == action.Update)
                    q2.And(DmucNhanvien.Columns.IdNhanvien).IsNotEqualTo(Utility.Int32Dbnull(txtID.Text, -1));
                if (q2.GetRecordCount() > 0)
                {
                    Utility.SetMsg(lblMsg, "Tên đăng nhập đã gán cho một nhân viên khác",true);
                    txtUID.Focus();
                    return false;
                }
            }
            if (cboUserName.SelectedValue != "-1")
            {
                SqlQuery q2 = new Select().From(DmucNhanvien.Schema)
                    .Where(DmucNhanvien.Columns.UserName).IsEqualTo(cboUserName.SelectedValue);
                if (em_Action == action.Update)
                    q2.And(DmucNhanvien.Columns.IdNhanvien).IsNotEqualTo(Utility.Int32Dbnull(txtID.Text, -1));
                if (q2.GetRecordCount() > 0)
                {
                    Utility.SetMsg(lblMsg, "Tên đăng nhập đã gán cho một nhân viên khác", true);
                    cboUserName.Focus();
                    return false;
                }
            }
            //if (!lstckhaucong.Contains(Utility.Int16Dbnull(Utility.DecimaltoDbnull(txtCkhau.Text), 0)))
            //{
            //    if (Utility.AcceptQuestion(string.Format( "Mức chiết khấu công nên là các giá trị: {0}. Bạn có chắc chắn muốn áp dụng mức chiết khấu mới cho bác sĩ "), "Cảnh báo chiết khấu", true))
            //    {
            //        return;
            //    }
            //}
            return true;
        }

        private Query _Query = DmucNhanvien.CreateQuery();
        /// <summary>
        /// hamdf thực hiện thêm thông tin 
        /// </summary>
        private void PerformActionInsert()
        {
            DmucNhanvien objDmucNhanvien = TaoDoituongNhanvien();
            QheNhanvienDanhmucCollection lstQheDmuc = GetQheNhanvienDanhmuc(objDmucNhanvien);
            QheNhanvienKhoCollection lstQhekho = GetQuanheNhanVienKho(objDmucNhanvien);
            QheBacsiKhoaphongCollection lstQhekhoa = GetQuanheBsi_khoaphong(objDmucNhanvien);
            QheNhanvienQuyensudungCollection lstQheQuyensudung = GetQuanheNhanVienQuyen(objDmucNhanvien);
            QheNhanvienBaocaomultiCollection lstQheBaocaomulti = GetQuanheNhanVienBaocaomulti(objDmucNhanvien);
            QheNhanvienDmucchungCollection lstQheDmchung = GetQuanheNhanVienDmucchung(objDmucNhanvien);
            QheNhanvienCosoCollection lstQheCoso = GetQheNhanvienCoso(objDmucNhanvien);
            string ErrMsg = dmucnhanvien_busrule.Insert(objDmucNhanvien, lstQhekho, lstQhekhoa, lstQheQuyensudung, lstQheDmuc, lstQheBaocaomulti, lstQheDmchung, lstQheCoso,true);
            if (ErrMsg == string.Empty)
            {

                DataRow dr = p_dtStaffList.NewRow();
                dr[DmucNhanvien.Columns.NguoiTao] = globalVariables.UserName;
                dr[DmucNhanvien.Columns.NgayTao] = globalVariables.SysDate;
                dr[DmucNhanvien.Columns.IdNhanvien] = Utility.Int32Dbnull(_Query.GetMax(DmucNhanvien.Columns.IdNhanvien), -1);
                dr[DmucNhanvien.Columns.MaNhanvien] = txtStaffCode.Text;
                dr[DmucNhanvien.Columns.TrangThai] = chkHienThi.Checked ? 1 : 0;
                dr[DmucNhanvien.Columns.TenNhanvien] = Utility.sDbnull(txtName.Text, "");
                dr[DmucNhanvien.Columns.MaChungchi] = Utility.sDbnull(txtMachungchi.Text, "");
                dr[DmucNhanvien.Columns.GioiTinh] = cboGioitinh.Text;
                dr[DmucNhanvien.Columns.IdGioitinh] = (byte)cboGioitinh.SelectedIndex;
                dr[DmucNhanvien.Columns.MotaThem] = Utility.DoTrim(txtmotathem.Text);
                dr[DmucNhanvien.Columns.IdKhoa] = Utility.Int16Dbnull(cboUpLevel.SelectedValue, 1);
                dr[DmucNhanvien.Columns.Cungthuchien] = chkCungthuchien.Checked ? 1 : 0;
                dr[DmucNhanvien.Columns.ChiphiCungthuchien] = Utility.DecimaltoDbnull(txtDongia.Text, 0);
                dr["ten_khoa"] = Utility.sDbnull(cboUpLevel.Text, "");
                dr[DmucNhanvien.Columns.IdPhong] = Utility.Int16Dbnull(cboDepart.SelectedValue, 1);
                dr["ten_phong"] = Utility.sDbnull(cboDepart.Text, "");
                dr["ten_loainhanvien"] = Utility.sDbnull(cboStaffType.Text, "");
                dr[DmucNhanvien.Columns.UserName] = Utility.sDbnull(cboUserName.SelectedValue, "");
                dr[DmucNhanvien.Columns.MaLoainhanvien] = Utility.sDbnull(cboStaffType.SelectedValue, "");

                p_dtStaffList.Rows.InsertAt(dr, 0);
                Utility.SetMsg(lblMsg, "Bạn thực hiện cập nhập thông tin thành công. Mời bạn tiếp tục thêm mới", false);
                if (chkClose.Checked)
                    ResetNew();
                else
                    this.Close();

            }
            else
            {
                Utility.ShowMsg(ErrMsg);
            }
        }
        void ResetNew()
        {
            em_Action = action.Insert;
            txtID.Clear();
            txtMachungchi.Clear();
            txtStaffCode.Clear();
            txtName.Clear();
            txtmotathem.Clear();
            txtUID._Text = "";
            cboUserName.SelectedIndex = -1;
            cbockhau.SelectedIndex = -1;
            if (!chkKhoaPhong.Checked)
            {
                cboStaffType.SelectedIndex = -1;
                cboDepart.SelectedIndex = -1;
                cboUpLevel.SelectedIndex = -1;
            }
            if (!chkQuyen.Checked)
            {
                grdDichvuCls.UnCheckAllRecords();
                grdKhoa.UnCheckAllRecords();
                grdKhoanoitru.UnCheckAllRecords();
                grdKhoThuoc.UnCheckAllRecords();
                grdLoaiThuoc.UnCheckAllRecords();
                grdPhongkham.UnCheckAllRecords();
                grdPhongkhamNoitru.UnCheckAllRecords();
                grdQuyen.UnCheckAllRecords();
            }
        }
        private QheNhanvienKhoCollection GetQuanheNhanVienKho(DmucNhanvien objDmucNhanvien)
        {
            QheNhanvienKhoCollection lst = new QheNhanvienKhoCollection();
           
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdKhoThuoc.GetCheckedRows())
            {
                QheNhanvienKho objDNhanvienKho = new QheNhanvienKho();
                objDNhanvienKho.IdKho = Utility.Int16Dbnull(gridExRow.Cells[TDmucKho.Columns.IdKho].Value);
                objDNhanvienKho.IdNhanvien = objDmucNhanvien.IdNhanvien;
                objDNhanvienKho.IsNew = true;
                lst.Add(objDNhanvienKho);
            }
            return lst;
        }
        private QheNhanvienDmucchungCollection GetQuanheNhanVienDmucchung(DmucNhanvien objDmucNhanvien)
        {
            QheNhanvienDmucchungCollection lst = new QheNhanvienDmucchungCollection();

            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdDmucchung.GetCheckedRows())
            {
                QheNhanvienDmucchung objQheNhanvienDmucchung = new QheNhanvienDmucchung();
                objQheNhanvienDmucchung.IdNhanvien = objDmucNhanvien.IdNhanvien;
                objQheNhanvienDmucchung.Loai = Utility.sDbnull(gridExRow.Cells["ma_loai"].Value, "ERR");
                objQheNhanvienDmucchung.IsNew = true;
                lst.Add(objQheNhanvienDmucchung);
            }
            return lst;
        }
        private QheNhanvienBaocaomultiCollection GetQuanheNhanVienBaocaomulti(DmucNhanvien objDmucNhanvien)
        {
            QheNhanvienBaocaomultiCollection lst = new QheNhanvienBaocaomultiCollection();

            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdbaocaomulti.GetCheckedRows())
            {
                QheNhanvienBaocaomulti objQheNhanvienBaocaomulti = new QheNhanvienBaocaomulti();
                objQheNhanvienBaocaomulti.IdNhanvien = objDmucNhanvien.IdNhanvien;
                objQheNhanvienBaocaomulti.IdBaocao = Utility.Int32Dbnull(gridExRow.Cells["ma"].Value, -1);
                objQheNhanvienBaocaomulti.IsNew = true;
                lst.Add(objQheNhanvienBaocaomulti);
            }
            return lst;
        }
        private QheNhanvienQuyensudungCollection GetQuanheNhanVienQuyen(DmucNhanvien objDmucNhanvien)
        {
            QheNhanvienQuyensudungCollection lst=new QheNhanvienQuyensudungCollection();
          
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdQuyen.GetCheckedRows())
            {
                QheNhanvienQuyensudung objQheNhanvienQuyensudung = new QheNhanvienQuyensudung();
                objQheNhanvienQuyensudung.Ma = Utility.sDbnull(gridExRow.Cells[QheNhanvienQuyensudung.Columns.Ma].Value);
                objQheNhanvienQuyensudung.IdNhanvien = objDmucNhanvien.IdNhanvien;
                objQheNhanvienQuyensudung.Loai = Utility.sDbnull(gridExRow.Cells[QheNhanvienQuyensudung.Columns.Loai].Value);
                objQheNhanvienQuyensudung.IsNew = true;
                lst.Add(objQheNhanvienQuyensudung);
            }
            return lst;
        }
        private QheBacsiKhoaphongCollection GetQuanheBsi_khoaphong(DmucNhanvien objDmucNhanvien)
        {
            QheBacsiKhoaphongCollection lst = new QheBacsiKhoaphongCollection();
          
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdKhoanoitru.GetCheckedRows())
            {
                QheBacsiKhoaphong objQheBacsiKhoaphong = new QheBacsiKhoaphong();
                objQheBacsiKhoaphong.IdKhoa = Utility.Int16Dbnull(gridExRow.Cells[DmucKhoaphong.Columns.IdKhoaphong].Value);
                objQheBacsiKhoaphong.IdBacsi = objDmucNhanvien.IdNhanvien;
                objQheBacsiKhoaphong.Noitru = 1;
                objQheBacsiKhoaphong.IdPhong = -1;
                objQheBacsiKhoaphong.IsNew = true;
                lst.Add(objQheBacsiKhoaphong);
            }
           
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPhongkham.GetCheckedRows())
            {
                QheBacsiKhoaphong objQheBacsiKhoaphong = new QheBacsiKhoaphong();
                objQheBacsiKhoaphong.IdKhoa = Utility.Int16Dbnull(gridExRow.Cells[DmucKhoaphong.Columns.MaCha].Value);
                objQheBacsiKhoaphong.IdBacsi = objDmucNhanvien.IdNhanvien;
                objQheBacsiKhoaphong.Noitru = 0;
                objQheBacsiKhoaphong.IdPhong = Utility.Int16Dbnull(gridExRow.Cells[DmucKhoaphong.Columns.IdKhoaphong].Value);
                objQheBacsiKhoaphong.IsNew = true;
                lst.Add(objQheBacsiKhoaphong);
            }
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPhongkhamNoitru.GetCheckedRows())
            {
                QheBacsiKhoaphong objQheBacsiKhoaphong = new QheBacsiKhoaphong();
                objQheBacsiKhoaphong.IdKhoa = Utility.Int16Dbnull(gridExRow.Cells[DmucKhoaphong.Columns.MaCha].Value);
                objQheBacsiKhoaphong.IdBacsi = objDmucNhanvien.IdNhanvien;
                objQheBacsiKhoaphong.Noitru = 1;
                objQheBacsiKhoaphong.IdPhong = Utility.Int16Dbnull(gridExRow.Cells[DmucKhoaphong.Columns.IdKhoaphong].Value);
                objQheBacsiKhoaphong.IsNew = true;
                lst.Add(objQheBacsiKhoaphong);
            }
            return lst;
        }
        private QheNhanvienCosoCollection GetQheNhanvienCoso(DmucNhanvien objDmucNhanvien)
        {
            QheNhanvienCosoCollection lst = new QheNhanvienCosoCollection();

            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdCosoKCB.GetCheckedRows())
            {
                QheNhanvienCoso objQheNhanvienCoso = new QheNhanvienCoso();
                objQheNhanvienCoso.IdNhanvien = objDmucNhanvien.IdNhanvien;
                objQheNhanvienCoso.MaCoso = Utility.sDbnull(gridExRow.Cells["ma"].Value, "ERR");
                objQheNhanvienCoso.IsNew = true;
                lst.Add(objQheNhanvienCoso);
            }
            return lst;
        }
        private QheNhanvienDanhmucCollection GetQheNhanvienDanhmuc(DmucNhanvien objDmucNhanvien)
        {
            QheNhanvienDanhmucCollection lst = new QheNhanvienDanhmucCollection();
           
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdLoaiThuoc.GetCheckedRows())
            {
                QheNhanvienDanhmuc objQheNhanvienDanhmuc = new QheNhanvienDanhmuc();
                objQheNhanvienDanhmuc.IdDichvu = Utility.sDbnull(gridExRow.Cells[DmucLoaithuoc.Columns.IdLoaithuoc].Value);
                objQheNhanvienDanhmuc.IdNhanvien = objDmucNhanvien.IdNhanvien;
                objQheNhanvienDanhmuc.Loai = 1;
                objQheNhanvienDanhmuc.IsNew = true;
                lst.Add(objQheNhanvienDanhmuc);
            }

            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdDichvuCls.GetCheckedRows())
            {
                QheNhanvienDanhmuc objQheNhanvienDanhmuc = new QheNhanvienDanhmuc();
                objQheNhanvienDanhmuc.IdDichvu = Utility.sDbnull(gridExRow.Cells[DmucChung.Columns.Ma].Value);
                objQheNhanvienDanhmuc.IdNhanvien = objDmucNhanvien.IdNhanvien;
                objQheNhanvienDanhmuc.Loai = 0;
                objQheNhanvienDanhmuc.IsNew = true;
                lst.Add(objQheNhanvienDanhmuc);
            }
            return lst;
        }
     

        private DmucNhanvien TaoDoituongNhanvien()
        {
            DmucNhanvien objDmucNhanvien = null;
            if (em_Action == action.Update)
            {
                objDmucNhanvien = DmucNhanvien.FetchByID(Utility.Int16Dbnull(txtID.Text, -1));
                objDmucNhanvien.MarkOld();
                objDmucNhanvien.IsNew = false;
                objDmucNhanvien.IsLoaded = true;
                objDmucNhanvien.NgaySua = globalVariables.SysDate;
                objDmucNhanvien.NguoiSua = globalVariables.UserName;
            }
            else
            {
                objDmucNhanvien = new DmucNhanvien();
                objDmucNhanvien.IsNew = true;
                objDmucNhanvien.NgayTao = globalVariables.SysDate;
                objDmucNhanvien.NguoiTao = globalVariables.UserName;
            }
            objDmucNhanvien.MaNhanvien = Utility.sDbnull(txtStaffCode.Text);
            objDmucNhanvien.MaChungchi = Utility.sDbnull(txtMachungchi.Text, "");
            objDmucNhanvien.TenNhanvien = Utility.sDbnull(txtName.Text);
            objDmucNhanvien.IdPhong = Utility.Int16Dbnull(cboDepart.SelectedValue, -1);
            objDmucNhanvien.IdKhoa = Utility.Int32Dbnull(cboUpLevel.SelectedValue, -1);
            objDmucNhanvien.MaLoainhanvien = Utility.sDbnull(cboStaffType.SelectedValue, "-1");
            objDmucNhanvien.UserName = Utility.sDbnull(cboUserName.SelectedValue, "");
            objDmucNhanvien.TrangThai = Convert.ToByte(chkHienThi.Checked?1:0);
            objDmucNhanvien.MotaThem = Utility.DoTrim(txtmotathem.Text);
            objDmucNhanvien.GioiTinh = cboGioitinh.Text;
            objDmucNhanvien.IdGioitinh = (byte)cboGioitinh.SelectedIndex;
            objDmucNhanvien.Cungthuchien = Convert.ToByte(chkCungthuchien.Checked ? 1 : 0);
            objDmucNhanvien.ChiphiCungthuchien = Utility.DecimaltoDbnull(txtDongia.Text, 0);
            objDmucNhanvien.PtramCkhau = Utility.ByteDbnull(cbockhau.Text, 0);
            
            objDmucNhanvien.MaLoaiKcb =Utility.sDbnull( cboMaloaiKCB.SelectedValue);
            objDmucNhanvien.DienThoai = Utility.sDbnull(txtDienThoai.Text, "");
            objDmucNhanvien.MaBhxh = Utility.sDbnull(txtMa_bhxh.Text, "");
            objDmucNhanvien.MaKhoa = Utility.sDbnull(txtMaKhoa.Text, "");
            objDmucNhanvien.TenKhoa = Utility.sDbnull(txtTenKhoa.Text, "");
            objDmucNhanvien.MaChucdanh = Utility.sDbnull(cboChucdanh.SelectedValue, "");
            objDmucNhanvien.ViTri = Utility.sDbnull(cboVitri.SelectedValue, "");
            objDmucNhanvien.NgaycapCchn = dtpNgaycapCCHN.Value.Date;
            objDmucNhanvien.NoicapCchn = Utility.sDbnull(txtNOICAP_CCHN.Text, "");
            objDmucNhanvien.PhamviChuyenmo = Utility.sDbnull(txtPHAMVI_CM.Text, "");
            objDmucNhanvien.PhamviChuyenmonBacsi = Utility.sDbnull(txtPHAMVI_CMBS.Text, "");
            objDmucNhanvien.VbanPhancong = Utility.sDbnull(txtVB_PHANCONG.Text, "");
            objDmucNhanvien.DvktKhac = Utility.sDbnull(txtDVKT_KHAC.Text, "");
            objDmucNhanvien.ThoigianDky = Utility.ByteDbnull(cbothoigiandk.SelectedValue, 0);
            objDmucNhanvien.ThoigianTuan = Utility.sDbnull(txtTHOIGIAN_TUAN.Text, "");
            objDmucNhanvien.ThoigianNgay = Utility.sDbnull(txtTHOIGIAN_NGAY.Text, "");
            objDmucNhanvien.CskcbCgkt = Utility.sDbnull(txtCSKCB_CGKT.Text, "");
            objDmucNhanvien.CskcbKhac = Utility.sDbnull(txtCSKCB_KHAC.Text, "");
            objDmucNhanvien.QdinhCgkt = Utility.sDbnull(txtQD_CGKT.Text, "");
            objDmucNhanvien.ChucVu = Utility.sDbnull(cboChucvu.SelectedValue, "");
            objDmucNhanvien.TranKedon = Utility.DecimaltoDbnull(txtTrankedon.Text, 0);
            return objDmucNhanvien;
        }

        private void PerformActionUpdate()
        {

            try
            {

           DmucNhanvien objDmucNhanvien = TaoDoituongNhanvien();
           QheNhanvienDanhmucCollection lstQheDmuc = GetQheNhanvienDanhmuc(objDmucNhanvien);
           QheNhanvienKhoCollection lstQhekho= GetQuanheNhanVienKho(objDmucNhanvien);
           QheBacsiKhoaphongCollection lstQhekhoa= GetQuanheBsi_khoaphong(objDmucNhanvien);
           QheNhanvienQuyensudungCollection lstQheQuyensudung = GetQuanheNhanVienQuyen(objDmucNhanvien);
           QheNhanvienBaocaomultiCollection lstQheBaocaomulti = GetQuanheNhanVienBaocaomulti(objDmucNhanvien);
           QheNhanvienDmucchungCollection lstQheDmchung = GetQuanheNhanVienDmucchung(objDmucNhanvien);
           QheNhanvienCosoCollection lstQheCoso = GetQheNhanvienCoso(objDmucNhanvien);
           string ErrMsg = dmucnhanvien_busrule.Insert(objDmucNhanvien, lstQhekho, lstQhekhoa, lstQheQuyensudung, lstQheDmuc, lstQheBaocaomulti, lstQheDmchung, lstQheCoso,true);
            if (ErrMsg == string.Empty)
            {
                string noidung = string.Format("cập nhật nhân viên với mã={0}, tên={1}, UID={2} thành công", objDmucNhanvien.MaNhanvien, objDmucNhanvien.TenNhanvien, objDmucNhanvien.UserName);
                Utility.Log(this.Name, globalVariables.UserName, noidung, newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                DataRow[] dr = p_dtStaffList.Select(DmucNhanvien.Columns.IdNhanvien + "=" + Utility.Int32Dbnull(txtID.Text, -1));
                if (dr.GetLength(0) > 0)
                {
                    dr[0][DmucNhanvien.Columns.UserName] = Utility.sDbnull(cboUserName.SelectedValue);
                    dr[0][DmucNhanvien.Columns.MaLoainhanvien] = Utility.sDbnull(cboStaffType.SelectedValue, -1);
                    dr[0][DmucNhanvien.Columns.MaChungchi] = Utility.sDbnull(txtMachungchi.Text,"");
                    dr[0]["ten_loainhanvien"] = Utility.sDbnull(cboStaffType.Text, "");
                    dr[0]["ten_phong"] = Utility.sDbnull(cboDepart.Text, -1);
                    dr[0][DmucNhanvien.Columns.IdPhong] = Utility.Int32Dbnull(cboDepart.SelectedValue, -1);
                    dr[0]["ten_khoa"] = Utility.sDbnull(cboUpLevel.Text, -1);
                    dr[0][DmucNhanvien.Columns.IdKhoa] = Utility.Int32Dbnull(cboUpLevel.SelectedValue, -1);
                    dr[0][DmucNhanvien.Columns.MaNhanvien] = txtStaffCode.Text;
                    dr[0][DmucNhanvien.Columns.TenNhanvien] = Utility.sDbnull(txtName.Text, "");
                    dr[0][DmucNhanvien.Columns.MotaThem] = Utility.DoTrim(txtmotathem.Text);
                    dr[0][DmucNhanvien.Columns.GioiTinh] = cboGioitinh.Text;
                    dr[0][DmucNhanvien.Columns.IdGioitinh] =(byte) cboGioitinh.SelectedIndex;
                    dr[0][DmucNhanvien.Columns.TrangThai] = chkHienThi.Checked ? 1 : 0;
                    dr[0][DmucNhanvien.Columns.Cungthuchien] = chkCungthuchien.Checked ? 1 : 0;
                    dr[0][DmucNhanvien.Columns.ChiphiCungthuchien] = Utility.DecimaltoDbnull(txtDongia.Text, 0);
                    dr[0][DmucNhanvien.Columns.NguoiSua] = globalVariables.UserName;
                }

                p_dtStaffList.AcceptChanges();
                this.Close();
            }
            else
            {
                Utility.ShowMsg(ErrMsg);
            }
            }
            catch
            {
            }
        }
        void AutocheckPhongkham(string danhmucphongkham)
        {
            try
            {
                if (!string.IsNullOrEmpty(danhmucphongkham))
                {
                    string[] rows = danhmucphongkham.Split(',');
                    foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPhongkham.GetDataRows())
                    {
                        foreach (string row in rows)
                        {
                            if (!string.IsNullOrEmpty(row))
                            {

                                if (Utility.sDbnull(gridExRow.Cells[DmucKhoaphong.Columns.IdKhoaphong].Value, "-1") == Utility.sDbnull(row))
                                {
                                    gridExRow.IsChecked = true;
                                    break;
                                }
                                else
                                {
                                    gridExRow.IsChecked = false;
                                }
                            }
                        }
                    }
                }
            }
            catch
            { }
        }
        private void GetData()
        {
            DmucNhanvien objDmucNhanvien = DmucNhanvien.FetchByID(Utility.Int32Dbnull(txtID.Text, -1));
            if (objDmucNhanvien != null)
            {
                txtName.Text = Utility.sDbnull(objDmucNhanvien.TenNhanvien);
                txtStaffCode.Text = Utility.sDbnull(objDmucNhanvien.MaNhanvien);
                txtMachungchi.Text = Utility.sDbnull(objDmucNhanvien.MaChungchi, "");
                cboStaffType.SelectedIndex = Utility.GetSelectedIndex(cboStaffType,
                                                                       objDmucNhanvien.MaLoainhanvien.ToString());
                txtUID.SetCode(objDmucNhanvien.UserName);
                cboUserName.SelectedIndex = Utility.GetSelectedIndex(cboUserName, objDmucNhanvien.UserName);
              
                cboUpLevel.SelectedIndex = Utility.GetSelectedIndex(cboUpLevel,
                                                                       objDmucNhanvien.IdKhoa.ToString());
                cboDepart.SelectedIndex = Utility.GetSelectedIndex(cboDepart,
                                                                    objDmucNhanvien.IdPhong.ToString());
                chkHienThi.Checked = Utility.Byte2Bool(objDmucNhanvien.TrangThai);
                chkCungthuchien.Checked = Utility.Byte2Bool(objDmucNhanvien.Cungthuchien);
                txtDongia.Text = Utility.sDbnull(objDmucNhanvien.ChiphiCungthuchien, "");
                cboGioitinh.SelectedIndex = Utility.Int32Dbnull(objDmucNhanvien.IdGioitinh, 0) > 3 ? -1 : objDmucNhanvien.IdGioitinh.Value;
                cbockhau.SelectedIndex = Utility.GetSelectedIndex(cbockhau,Utility.sDbnull( objDmucNhanvien.PtramCkhau,"0"),-1);

                cboMaloaiKCB.SelectedValue = Utility.sDbnull(objDmucNhanvien.MaLoaiKcb);
                txtDienThoai.Text = objDmucNhanvien.DienThoai;
                txtMa_bhxh.Text = objDmucNhanvien.MaBhxh;
                txtMaKhoa.Text = objDmucNhanvien.MaKhoa;
                txtTenKhoa.Text = objDmucNhanvien.TenKhoa;
                cboChucdanh.SelectedValue=Utility.sDbnull( objDmucNhanvien.MaChucdanh);
                cboVitri.SelectedValue=Utility.sDbnull( objDmucNhanvien.ViTri);
                if (objDmucNhanvien.NgaycapCchn.HasValue)
                    dtpNgaycapCCHN.Value = objDmucNhanvien.NgaycapCchn.Value;
                txtNOICAP_CCHN.Text = objDmucNhanvien.NoicapCchn;
                txtPHAMVI_CM.Text = objDmucNhanvien.PhamviChuyenmo;
                txtPHAMVI_CMBS.Text = objDmucNhanvien.PhamviChuyenmonBacsi;
                txtVB_PHANCONG.Text = objDmucNhanvien.VbanPhancong;
                txtDVKT_KHAC.Text = objDmucNhanvien.DvktKhac;
                cbothoigiandk.SelectedIndex = Utility.GetSelectedIndex(cbothoigiandk,Utility.sDbnull( objDmucNhanvien.ThoigianDky));
                txtTHOIGIAN_TUAN.Text = objDmucNhanvien.ThoigianTuan;
                txtTHOIGIAN_NGAY.Text = objDmucNhanvien.ThoigianNgay;
                txtCSKCB_CGKT.Text = objDmucNhanvien.CskcbCgkt;
                txtCSKCB_KHAC.Text = objDmucNhanvien.CskcbKhac;
                txtQD_CGKT.Text = objDmucNhanvien.QdinhCgkt;
                cboChucvu.SelectedValue=Utility.sDbnull(objDmucNhanvien.ChucVu);
                txtTrankedon.Text = Utility.sDbnull(objDmucNhanvien.TranKedon, "0");

                LoadQuanHeNhanVienKho();
                LoadQuanHeNhanVienQuyen();
                LoadQuanHeNhanVienBaocaoMulti();
                LoadQheBS_khoanoitru();
                LoadQheBS_khoangoaitru();
                LoadQheLoaithuoc();
                LoadQheDichvuCLS();
                LoadQuanHeNhanVienDmucchung();
                LoadQuanHeNhanVienCosoKCB();
                
            }
        }
        private void LoadQuanHeNhanVienCosoKCB()
        {
            try
            {
                QheNhanvienCosoCollection LstQheNhanvienCoso = new Select().From(QheNhanvienCoso.Schema)
                .Where(QheNhanvienCoso.Columns.IdNhanvien).IsEqualTo(Utility.Int32Dbnull(txtID.Text)).
                ExecuteAsCollection<QheNhanvienCosoCollection>();
                foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdCosoKCB.GetDataRows())
                {
                    gridExRow.BeginEdit();
                    var query = from kho in LstQheNhanvienCoso.AsEnumerable()
                                where kho.MaCoso == Utility.sDbnull(gridExRow.Cells["ma"].Value)
                                select kho;
                    if (query.Count() > 0)
                    {
                        gridExRow.IsChecked = true;
                    }

                    else
                    {
                        gridExRow.IsChecked = false;
                    }
                    gridExRow.EndEdit();

                }
            }
            catch (System.Exception ex)
            {
            }
            
        }
        private void LoadQheLoaithuoc()
        {
            QheNhanvienDanhmucCollection lstQhenhanviendanhmucthuoc = new Select().From(QheNhanvienDanhmuc.Schema)
                .Where(QheNhanvienDanhmuc.Columns.IdNhanvien).IsEqualTo(Utility.Int32Dbnull(txtID.Text))
                .And(QheNhanvienDanhmuc.Columns.Loai).IsEqualTo(1)
                .ExecuteAsCollection<QheNhanvienDanhmucCollection>();
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdLoaiThuoc.GetDataRows())
            {
                gridExRow.BeginEdit();
                var query = from kho in lstQhenhanviendanhmucthuoc.AsEnumerable()
                            where kho.IdDichvu == Utility.sDbnull(gridExRow.Cells[DmucLoaithuoc.Columns.IdLoaithuoc].Value)
                            select kho;
                if (query.Count() > 0)
                {
                    gridExRow.IsChecked = true;
                }

                else
                {
                    gridExRow.IsChecked = false;
                }
                gridExRow.EndEdit();

            }
        }
        private void LoadQheDichvuCLS()
        {
            QheNhanvienDanhmucCollection lstQhenhanviendanhmucdichvucls = new Select().From(QheNhanvienDanhmuc.Schema)
               .Where(QheNhanvienDanhmuc.Columns.IdNhanvien).IsEqualTo(Utility.Int32Dbnull(txtID.Text))
               .And(QheNhanvienDanhmuc.Columns.Loai).IsEqualTo(0)
               .ExecuteAsCollection<QheNhanvienDanhmucCollection>();
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdDichvuCls.GetDataRows())
            {
                gridExRow.BeginEdit();
                var query = from kho in lstQhenhanviendanhmucdichvucls.AsEnumerable()
                            where kho.IdDichvu == Utility.sDbnull(gridExRow.Cells[DmucChung.Columns.Ma].Value)
                            select kho;
                if (query.Count() > 0)
                {
                    gridExRow.IsChecked = true;
                }

                else
                {
                    gridExRow.IsChecked = false;
                }
                gridExRow.EndEdit();

            }
        }
        private void LoadQheBS_khoanoitru()
        {
            QheBacsiKhoaphongCollection lstQheBacsiKhoaphong = new Select().From(QheBacsiKhoaphong.Schema)
                .Where(QheBacsiKhoaphong.Columns.IdBacsi).IsEqualTo(Utility.Int32Dbnull(txtID.Text))
                .And(QheBacsiKhoaphong.Columns.Noitru).IsEqualTo(1)
                .ExecuteAsCollection<QheBacsiKhoaphongCollection>();
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdKhoanoitru.GetDataRows())
            {
                gridExRow.BeginEdit();
                var query = from kho in lstQheBacsiKhoaphong.AsEnumerable()
                            where kho.IdKhoa == Utility.Int32Dbnull(gridExRow.Cells[DmucKhoaphong.Columns.IdKhoaphong].Value)
                            select kho;
                if (query.Count() > 0)
                {
                    gridExRow.IsChecked = true;
                }

                else
                {
                    gridExRow.IsChecked = false;
                }
                gridExRow.EndEdit();

            }
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPhongkhamNoitru.GetDataRows())
            {
                gridExRow.BeginEdit();
                var query = from kho in lstQheBacsiKhoaphong.AsEnumerable()
                            where kho.IdPhong == Utility.Int32Dbnull(gridExRow.Cells[DmucKhoaphong.Columns.IdKhoaphong].Value)
                            select kho;
                if (query.Count() > 0)
                {
                    gridExRow.IsChecked = true;
                }

                else
                {
                    gridExRow.IsChecked = false;
                }
                gridExRow.EndEdit();

            }
        }
        private void LoadQheBS_khoangoaitru()
        {
            QheBacsiKhoaphongCollection lstQheBacsiKhoaphongCollection = new Select().From(QheBacsiKhoaphong.Schema)
                .Where(QheBacsiKhoaphong.Columns.IdBacsi).IsEqualTo(Utility.Int32Dbnull(txtID.Text))
                .And(QheBacsiKhoaphong.Columns.Noitru).IsEqualTo(0)
                .ExecuteAsCollection<QheBacsiKhoaphongCollection>();
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdKhoa.GetDataRows())
            {
                gridExRow.BeginEdit();
                var query = from kho in lstQheBacsiKhoaphongCollection.AsEnumerable()
                            where kho.IdKhoa == Utility.Int32Dbnull(gridExRow.Cells[DmucKhoaphong.Columns.IdKhoaphong].Value)
                            select kho;
                if (query.Count() > 0)
                {
                    gridExRow.IsChecked = true;
                }

                else
                {
                    gridExRow.IsChecked = false;
                }
                gridExRow.EndEdit();

            }
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdPhongkham.GetDataRows())
            {
                gridExRow.BeginEdit();
                var query = from kho in lstQheBacsiKhoaphongCollection.AsEnumerable()
                            where kho.IdPhong == Utility.Int32Dbnull(gridExRow.Cells[DmucKhoaphong.Columns.IdKhoaphong].Value)
                            select kho;
                if (query.Count() > 0)
                {
                    gridExRow.IsChecked = true;
                }

                else
                {
                    gridExRow.IsChecked = false;
                }
                gridExRow.EndEdit();

            }
        }
        private void LoadQuanHeNhanVienBaocaoMulti()
        {
            QheNhanvienBaocaomultiCollection LstQheNhanvienBaocaomulti = new Select().From(QheNhanvienBaocaomulti.Schema)
                .Where(QheNhanvienBaocaomulti.Columns.IdNhanvien).IsEqualTo(Utility.Int32Dbnull(txtID.Text)).
                ExecuteAsCollection<QheNhanvienBaocaomultiCollection>();
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdbaocaomulti.GetDataRows())
            {
                gridExRow.BeginEdit();
                var query = from kho in LstQheNhanvienBaocaomulti.AsEnumerable()
                            where kho.IdBaocao == Utility.Int32Dbnull(gridExRow.Cells["ma"].Value)
                            select kho;
                if (query.Count() > 0)
                {
                    gridExRow.IsChecked = true;
                }

                else
                {
                    gridExRow.IsChecked = false;
                }
                gridExRow.EndEdit();

            }
        }
        private void LoadQuanHeNhanVienDmucchung()
        {
            QheNhanvienDmucchungCollection LstQheNhanvienDmucchung = new Select().From(QheNhanvienDmucchung.Schema)
                .Where(QheNhanvienDmucchung.Columns.IdNhanvien).IsEqualTo(Utility.Int32Dbnull(txtID.Text)).
                ExecuteAsCollection<QheNhanvienDmucchungCollection>();
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdDmucchung.GetDataRows())
            {
                gridExRow.BeginEdit();
                var query = from kho in LstQheNhanvienDmucchung.AsEnumerable()
                            where kho.Loai == Utility.sDbnull(gridExRow.Cells["ma_loai"].Value)
                            select kho;
                if (query.Count() > 0)
                {
                    gridExRow.IsChecked = true;
                }

                else
                {
                    gridExRow.IsChecked = false;
                }
                gridExRow.EndEdit();

            }
        }
        private void LoadQuanHeNhanVienQuyen()
        {
            QheNhanvienQuyensudungCollection LstQheNhanvienQuyensudung = new Select().From(QheNhanvienQuyensudung.Schema)
                .Where(QheNhanvienQuyensudung.Columns.IdNhanvien).IsEqualTo(Utility.Int32Dbnull(txtID.Text)).
                ExecuteAsCollection<QheNhanvienQuyensudungCollection>();
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdQuyen.GetDataRows())
            {
                gridExRow.BeginEdit();
                var query = from kho in LstQheNhanvienQuyensudung.AsEnumerable()
                            where kho.Ma == Utility.sDbnull(gridExRow.Cells[QheNhanvienQuyensudung.Columns.Ma].Value)
                            select kho;
                if (query.Count() > 0)
                {
                    gridExRow.IsChecked = true;
                }

                else
                {
                    gridExRow.IsChecked = false;
                }
                gridExRow.EndEdit();

            }
        }
        private void LoadQuanHeNhanVienKho()
        {
            QheNhanvienKhoCollection objNhanvienKhoCollection = new Select().From(QheNhanvienKho.Schema)
                .Where(QheNhanvienKho.Columns.IdNhanvien).IsEqualTo(Utility.Int32Dbnull(txtID.Text)).
                ExecuteAsCollection<QheNhanvienKhoCollection>();
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdKhoThuoc.GetDataRows())
            {
                gridExRow.BeginEdit();
                var query = from kho in objNhanvienKhoCollection.AsEnumerable()
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
        
        private void cmdSave_Click(object sender, EventArgs e)
        {
            PerformAction();
        }

        private void cboUpLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            dataTable = THU_VIEN_CHUNG.Laydanhsachphongthuockhoa(Utility.Int32Dbnull(cboUpLevel.SelectedValue, -1),-1);
            DataBinding.BindData(cboDepart, dataTable, DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.TenKhoaphong);
        }

        private void cboUserName_SelectedValueChanged(object sender, System.EventArgs e)
        {
            DataTable objNhanvien =
                new Select("*").From(SysUser.Schema).Where(SysUser.Columns.PkSuid).IsEqualTo(
                    Utility.sDbnull(cboUserName.SelectedValue)).ExecuteDataSet().Tables[0];
            if (objNhanvien != null && em_Action == action.Insert)
            {
                foreach (DataRow row in objNhanvien.AsEnumerable())
                {
                    txtName.Text = row["sFullName"].ToString();
                    txtStaffCode.Text = row["PK_sUID"].ToString();
                    txtKhoa.Text = row["sDepart"].ToString();
                }
            }

        }

        private void chkCungthuchien_CheckedChanged(object sender, System.EventArgs e)
        {
            txtDongia.Enabled = chkCungthuchien.Checked;
        }

    }
}
