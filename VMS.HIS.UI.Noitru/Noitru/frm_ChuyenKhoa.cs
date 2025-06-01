using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Janus.Windows.EditControls;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using NLog;
using VNS.Libs;
using VMS.HIS.DAL;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;
using SubSonic;
using System.Collections.Generic;


namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_ChuyenKhoa : Form
    {
        public int IDBuonggiuong = -1;
        public bool b_CallParent;
        public bool b_Cancel=true;
        public bool b_HasValue;
        private bool m_blnHasLoaded;
        public GridEX grdList;
        private DataTable m_dtGiuong = new DataTable();
        private DataTable m_dtKhoaNoItru = new DataTable();
        private DataTable m_dtPhong = new DataTable();
        private NoitruPhanbuonggiuong objPhanbuonggiuong;
        private KcbLuotkham objLuotkham;
        public DataTable p_DanhSachPhanBuongGiuong = new DataTable();

        public frm_ChuyenKhoa()
        {
            InitializeComponent();
            dtNgayvao.Value = globalVariables.SysDate;
            dtNgayChuyen.Value = globalVariables.SysDate;
            txtGio.Value = globalVariables.SysDate.Hour;
            txtPhut.Value = globalVariables.SysDate.AddMinutes(1).Minute;
            ClearControl();
            dtNgayvao.ValueChanged += new EventHandler(dtNgayvao_ValueChanged);
            CauHinh();
            txtDepartment_ID.Visible = globalVariables.IsAdmin;
            txtPatientDept_ID.Visible = globalVariables.IsAdmin;
            chkAutoCal.CheckedChanged += new EventHandler(chkAutoCal_CheckedChanged);
            txtHour2Cal.Text = THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_SOGIO_LAMTRONNGAY", "1", false);
            txtMaLanKham.KeyDown += txtMaLanKham_KeyDown;
            nmrSoluong.ValueChanged += nmrSoluong_ValueChanged;
        }

        void nmrSoluong_ValueChanged(object sender, EventArgs e)
        {
            if (!m_blnHasLoaded) return;
            dtNgayChuyen.Value = dtNgayvao.Value.AddDays(Utility.DoubletoDbnull(nmrSoluong.Value));
        }

        void txtMaLanKham_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && Utility.DoTrim(txtMaLanKham.Text)!="")
            {
                string _patient_Code = Utility.AutoFullPatientCode(txtMaLanKham.Text);
                ClearControl();
                txtMaLanKham.Text = _patient_Code;
                LoadData();
            }
        }

        void chkAutoCal_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoCal.Checked)
                nmrSoluong.Value =Utility.DecimaltoDbnull( THU_VIEN_CHUNG.Songay(dtNgayvao.Value, dtNgayChuyen.Value).ToString(),0);
            else
                nmrSoluong.Focus();
        }

        void dtNgayvao_ValueChanged(object sender, EventArgs e)
        {
            if (!m_blnHasLoaded) return;
            if (string.IsNullOrEmpty(txtGio.Text))
            {
                txtGiovao.Text = Utility.sDbnull(dtNgayvao.Value.Hour);
            }
            if (string.IsNullOrEmpty(txtPhut.Text))
            {
                txtPhutvao.Text = Utility.sDbnull(dtNgayvao.Value.Minute);
            }
          
        }

        private string maluotkham { get; set; }

        private void CauHinh()
        {
            //_hinhPhanBuongGiuongProperties = Utility.GetPhanBuongGiuongPropertiesConfig();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// hàm thực hiên việc load thông tin của khoa phòng hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_ChuyenKhoa_Load(object sender, EventArgs e)
        {
            LoadData();
            m_blnHasLoaded = true;
        }
        void LoadData()
        {
            maluotkham = Utility.AutoFullPatientCode(txtMaLanKham.Text);
            txtMaLanKham.Text = maluotkham;
            BindData();
            m_dtKhoaNoItru =
               THU_VIEN_CHUNG.Laydanhmuckhoa("NOI", 0, Utility.Int32Dbnull(txtDepartment_ID.Text));
            txtKhoanoitru.Init(m_dtKhoaNoItru, new List<string>() { DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong });
            txtKhoanoitru._TextAlign = HorizontalAlignment.Left;
            ModifyCommand();
            chkAutoCal_CheckedChanged(chkAutoCal, new EventArgs());
            txtTotalHour.Text = Math.Ceiling((dtNgayChuyen.Value - dtNgayvao.Value).TotalHours).ToString();
            //dtNgayChuyen.Focus();
        }
     
        private void ShowPatientList()
        {
            var frm = new frm_TimKiem_BN();
            frm.SearchByDate = false;
            frm.txtPatientCode.Text = txtMaLanKham.Text;
            frm.ShowDialog();
            if (!frm.m_blnCancel)
            {
                txtMaLanKham.Text = Utility.sDbnull(frm.MaLuotkham);
                BindData();
            }
        }

        

        private void ClearControl()
        {
            foreach (Control ctrl in grpThongTinBN.Controls)
            {
                if (ctrl is EditBox)
                {
                    ((EditBox)ctrl).Clear();
                }
               
            }
        }

        private void BindData()
        {
            SqlQuery sqlQuery = new Select().From(KcbLuotkham.Schema)
                .Where(KcbLuotkham.Columns.MaLuotkham).IsEqualTo(txtMaLanKham.Text);
            if (sqlQuery.GetRecordCount() > 0)
            {
                objLuotkham = sqlQuery.ExecuteSingle<KcbLuotkham>();
                if (objLuotkham != null)
                {
                    txtMaLanKham.Text = Utility.sDbnull(objLuotkham.MaLuotkham);
                    txtSoBHYT.Text = Utility.sDbnull(objLuotkham.MatheBhyt);
                    DmucKhoaphong objDmucKhoaphong = DmucKhoaphong.FetchByID(objLuotkham.IdKhoanoitru);
                    if (objDmucKhoaphong != null)
                    {
                        txtDepartmentName.Tag = Utility.sDbnull(objDmucKhoaphong.IdKhoaphong);
                        txtDepartment_ID.Text = Utility.sDbnull(objDmucKhoaphong.IdKhoaphong);
                        txtDepartmentName.Text = Utility.sDbnull(objDmucKhoaphong.TenKhoaphong);
                    }

                    KcbDanhsachBenhnhan objPatientInfo = KcbDanhsachBenhnhan.FetchByID(objLuotkham.IdBenhnhan);
                    if (objPatientInfo != null)
                    {
                        txtPatient_Name.Text = Utility.sDbnull(objPatientInfo.TenBenhnhan);
                        txtPatient_ID.Text = Utility.sDbnull(objLuotkham.IdBenhnhan);
                        txtNamSinh.Text = Utility.sDbnull(objPatientInfo.NamSinh);
                        txtTuoi.Text = Utility.sDbnull(DateTime.Now.Year - objPatientInfo.NamSinh);
                        txtPatientSex.Text = objPatientInfo.GioiTinh;// Utility.Int32Dbnull(objPatientInfo.PatientSex) == 0 ? "Nam" : "Nữ";
                    }
                    objPhanbuonggiuong = NoitruPhanbuonggiuong.FetchByID(objLuotkham.IdRavien);
                    dtNgayvao.Value = objPhanbuonggiuong.NgayVaokhoa;
                    if (objPhanbuonggiuong != null)
                    {
                        txtPatientDept_ID.Text = Utility.sDbnull(objPhanbuonggiuong.Id);
                        NoitruDmucBuong objRoom = NoitruDmucBuong.FetchByID(objPhanbuonggiuong.IdBuong);
                        if (objRoom != null)
                        {
                            txtSoPhong.Text = Utility.sDbnull(objRoom.TenBuong);
                            txtSoPhong.Tag = Utility.sDbnull(objPhanbuonggiuong.IdBuong);
                        }
                        NoitruDmucGiuongbenh objNoitruDmucGiuongbenh = NoitruDmucGiuongbenh.FetchByID(objPhanbuonggiuong.IdGiuong);
                        if (objNoitruDmucGiuongbenh != null)
                        {
                            txtSoGiuong.Text = Utility.sDbnull(objNoitruDmucGiuongbenh.TenGiuong);
                            txtSoGiuong.Tag = Utility.sDbnull(objPhanbuonggiuong.IdGiuong);
                        }
                    }
                }
               
            }
            ModifyCommand();
        }

        /// <summary>
        /// hàm thực hiện việc lưu lại thông tin chuyển viện
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            //if (!InValBenhNhan.ExistBN(txtMaLanKham.Text)) return;
            if (!IsValidData()) return;
            PerformAction();
            ModifyCommand();
        }

        private bool IsValidData()
        {
            objLuotkham = Utility.getKcbLuotkham(objLuotkham.MaLuotkham);
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Không lấy được dữ liệu lượt khám của bệnh nhân. Đề nghị bạn kiểm tra lại");
                return false;
            }
            if (objLuotkham.TrangthaiNoitru == 0)
            {
                Utility.ShowMsg("Bệnh nhân chưa nhập viện nội trú nên bạn không thể chuyển khoa. Đề nghị bạn kiểm tra lại");
                return false;
            }
            if (dtNgayChuyen.Value < dtNgayvao.Value)
            {
                Utility.ShowMsg("Ngày chuyển  khoa  phải lớn hơn hoặc bằng ngày vào khoa. Mời bạn kiểm tra lại", "Thông báo",
                    MessageBoxIcon.Warning);
                //errorProvider1.SetError(dtNgayChuyen, "Ngày chuyển  giường phải lớn hơn hoặc bằng ngày vào giường. Mời bạn kiểm tra lại");
                dtNgayChuyen.Focus();
                return false;
            }
            if (objLuotkham.TrangthaiNoitru == 3)
            {
                Utility.ShowMsg("Bệnh nhân đã được khoa nội trú cho ra viện nên bạn không thể chuyển khoa");
                return false;
            }
            if (objLuotkham.TrangthaiNoitru == 4)
            {
                Utility.ShowMsg("Bệnh nhân đã được khoa nội trú tổng hợp dữ liệu và xác nhận chuyển tài chính duyệt(hoặc thanh toán) nên bạn không thể chuyển khoa");
                return false;
            }
            if (objLuotkham.TrangthaiNoitru == 5)
            {
                Utility.ShowMsg("Bệnh nhân đã được duyệt chờ thanh toán nội trú nên bạn không thể chuyển khoa");
                return false;
            }
            if (Utility.Byte2Bool(objLuotkham.TthaiThanhtoannoitru) || objLuotkham.TrangthaiNoitru == 6)
            {
                Utility.ShowMsg("Bệnh nhân đã được thanh toán nội trú nên bạn không thể  chuyển khoa");
                return false;
            }

            if (Utility.Int32Dbnull(txtKhoanoitru.MyID, -1) == -1)
            {
                Utility.ShowMsg("Bạn phải chọn khoa nội trú chuyển đến", "Thông báo", MessageBoxIcon.Warning);
                txtKhoanoitru.Focus();
                return false;
            }
            if (nmrSoluong.Value !=Utility.DecimaltoDbnull( THU_VIEN_CHUNG.Songay(dtNgayvao.Value, dtNgayChuyen.Value),0))
            {
                DateTime ngayvaokhoa = dtNgayvao.Value.AddDays(Utility.Int32Dbnull(nmrSoluong.Value));
                ngayvaokhoa=new DateTime(ngayvaokhoa.Year, ngayvaokhoa.Month, ngayvaokhoa.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                if (!Utility.AcceptQuestion(string.Format("Bạn có chắc chắn số ngày nằm viện trước khi chuyển khoa là: {0} thay vì {1}(Hệ thống tự động tính toán từ ngày vào khoa {2} đến ngày chuyển khoa {3}) hay không?\nNhấn Yes để hệ thống sẽ tự động điều chỉnh ngày chuyển khoa từ {4} thành {5}\nNhấn No để chỉnh lại ngày chuyển khoa trước khi thực hiện ", nmrSoluong.Value.ToString(), THU_VIEN_CHUNG.Songay(dtNgayvao.Value, dtNgayChuyen.Value).ToString(), dtNgayvao.Value.ToString("dd/MM/yyyy HH:mm:ss"), dtNgayChuyen.Value.ToString("dd/MM/yyyy HH:mm:ss"), dtNgayChuyen.Value.ToString("dd/MM/yyyy HH:mm:ss"), ngayvaokhoa.ToString("dd/MM/yyyy HH:mm:ss")), "Cảnh báo", true))
                {
                    nmrSoluong.Focus();
                    return false;
                }
                dtNgayChuyen.Value = ngayvaokhoa;
            }
            return true;
        }

        private void PerformAction()
        {
            NoitruPhanbuonggiuong objPhanbuonggiuong = NoitruPhanbuonggiuong.FetchByID(Utility.Int32Dbnull(txtPatientDept_ID.Text));

            if (objPhanbuonggiuong != null)
            {
                if (Utility.AcceptQuestion(string.Format("Bạn có chắc chắn muốn chuyển khoa điều trị từ {0} đến {1} hay không?",txtDepartmentName.Text,txtKhoanoitru.Text), "Thông báo", true))
                {
                    var ngaychuyenkhoa = new DateTime(dtNgayChuyen.Value.Year, dtNgayChuyen.Value.Month,
                                                      dtNgayChuyen.Value.Day, Utility.Int32Dbnull(txtGio.Text),
                                                      Utility.Int32Dbnull(txtPhut.Text), 00);
                    objPhanbuonggiuong.SoLuong = nmrSoluong.Value;
                    objPhanbuonggiuong.SoluongGio = Utility.Int32Dbnull(Utility.DecimaltoDbnull(txtTotalHour.Text)); 
                    objPhanbuonggiuong.CachtinhSoluong = (byte)(chkAutoCal.Checked ? 0 : 1);
                    
                    ActionResult actionResult = new noitru_nhapvien().ChuyenKhoaDieuTri(objPhanbuonggiuong,
                                                                                            objLuotkham,
                                                                                            ngaychuyenkhoa,
                                                                                            Utility.Int16Dbnull(txtKhoanoitru.MyID, -1), -1,
                                                                                            -1);
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Chuyển khoa nội trú cho người bệnh ID bệnh nhân={0}, PID={1}, từ id khoa cũ={2} sang id khoa mới={3} thành công ", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham,  objPhanbuonggiuong.IdKhoachuyen, objPhanbuonggiuong.IdKhoanoitru), newaction.Transfer, this.GetType().Assembly.ManifestModule.Name);
                            txtPatientDept_ID.Text = Utility.sDbnull(objPhanbuonggiuong.Id);
                            Utility.SetMsg(lblMsg, "Bạn chuyển khoa nội trú thành công", true);
                            if (b_CallParent)
                            {
                                b_Cancel = false;
                                Close();
                            }
                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Lỗi trong quá trình chuyển khoa nội trú", "Thông báo", MessageBoxIcon.Error);
                            break;
                    }
                }
            }
        }


        /// <summary>
        /// hàm thực hiện việc load thông tin của phòng hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cboKhoaChuyenDen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_blnHasLoaded)
            {
                m_dtPhong =
                    THU_VIEN_CHUNG.NoitruTimkiembuongTheokhoa(Utility.Int32Dbnull(txtKhoanoitru.MyID));
            }
        }

        
        /// <summary>
        /// hàm thực hiện việc phím tắt thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_ChuyenKhoa_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S && e.Control) cmdSave.PerformClick();
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.F2)
            {
                txtMaLanKham.Focus();
                txtMaLanKham.SelectAll();
            }
            if (e.KeyCode == Keys.F3)
            {
                ShowPatientList();
            }
        }

        private void ModifyCommand()
        {
            cmdSave.Enabled = !string.IsNullOrEmpty(txtMaLanKham.Text);
            
        }
        private void txtMaLanKham_TextChanged(object sender, EventArgs e)
        {
            ModifyCommand();
        }

        private void dtNgayChuyen_ValueChanged(object sender, EventArgs e)
        {
            if (!m_blnHasLoaded) return;
            if (string.IsNullOrEmpty(txtGio.Text))
            {
                txtGio.Text = Utility.sDbnull(dtNgayChuyen.Value.Hour);
            }
            if (string.IsNullOrEmpty(txtPhut.Text))
            {
                txtPhut.Text = Utility.sDbnull(dtNgayChuyen.Value.Minute);
            }
            txtTotalHour.Text =Math.Ceiling( (dtNgayChuyen.Value - dtNgayvao.Value).TotalHours).ToString();
            //if (chkAutoCal.Checked)
                nmrSoluong.Value =Utility.DecimaltoDbnull( THU_VIEN_CHUNG.Songay(dtNgayvao.Value, dtNgayChuyen.Value).ToString(),0);
        }

        private void dtNgayChuyen_TextChanged(object sender, EventArgs e)
        {
            if (!m_blnHasLoaded) return;
            if (string.IsNullOrEmpty(txtGio.Text))
            {
                txtGio.Text = Utility.sDbnull(dtNgayChuyen.Value.Hour);
            }
            if (string.IsNullOrEmpty(txtPhut.Text))
            {
                txtPhut.Text = Utility.sDbnull(dtNgayChuyen.Value.Minute);
            }
            txtTotalHour.Text =Math.Ceiling( (dtNgayChuyen.Value - dtNgayvao.Value).TotalHours).ToString();
            //if (chkAutoCal.Checked)
                nmrSoluong.Value  =Utility.DecimaltoDbnull(THU_VIEN_CHUNG.Songay(dtNgayvao.Value, dtNgayChuyen.Value).ToString());
        }

        private void cmdConfig_Click(object sender, EventArgs e)
        {
            //var frm = new frm_CauHinh_BuongGiuong();
            //frm.HisclsProperties = _hinhPhanBuongGiuongProperties;
            //frm.ShowDialog();
            //CauHinh();
        }

       
     

       
    }
}