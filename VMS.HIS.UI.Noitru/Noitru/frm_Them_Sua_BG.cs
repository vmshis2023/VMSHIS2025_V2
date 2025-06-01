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
using VNS.HIS.NGHIEPVU;


namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_Them_Sua_BG : Form
    {
        public action em_Action = action.Insert;
        public delegate void OnSucess(long id_bg);
        public event OnSucess _OnSucess;
        public int IDBuonggiuong = -1;
        public bool b_CallParent;
        public bool b_Cancel=true;
        public bool b_HasValue;
        private bool m_blnHasLoaded;
        public GridEX grdList;
        private DataTable m_dtGiuong = new DataTable();
        private DataTable m_dtKhoaNoItru = new DataTable();
        private DataTable m_dtZoom = new DataTable();
        public NoitruPhanbuonggiuong objPhanbuonggiuong;
        private KcbLuotkham objLuotkham;
        DmucDoituongkcb objDoituongKcb;
        public DataTable p_DanhSachPhanBuongGiuong = new DataTable();

        public frm_Them_Sua_BG(KcbLuotkham objLuotkham,NoitruPhanbuonggiuong objPhanbuonggiuong)
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            grdGiuong.CellSelectionMode = Janus.Windows.GridEX.CellSelectionMode.SingleCell;
            this.objLuotkham = objLuotkham;
            this.objPhanbuonggiuong = objPhanbuonggiuong;
            dtNgayvao.Value = dtNgayChuyen.Value = globalVariables.SysDate;
            txtGio.Value = globalVariables.SysDate.Hour;
            txtPhut.Value = globalVariables.SysDate.AddMinutes(1).Minute;
            ClearControl();
            CauHinh();
            txtDepartment_ID.Visible = globalVariables.IsAdmin;
            txtPatientDept_ID.Visible = globalVariables.IsAdmin;
            chkAutoCal.CheckedChanged += new EventHandler(chkAutoCal_CheckedChanged);
            txtHour2Cal.Text = THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_SOGIO_LAMTRONNGAY", "1", false);
            txtMaLanKham.KeyDown += txtMaLanKham_KeyDown;
            txtKhoanoitru._OnEnterMe += txtKhoanoitru__OnEnterMe;
            txtKhoanoitru._OnSelectionChanged += txtKhoanoitru__OnSelectionChanged;
            nmrSoluong.LostFocus += txtSoluong_LostFocus;
            txtGia._OnEnterMe += txtGia__OnEnterMe;
            grdBuong.SelectionChanged+=grdBuong_SelectionChanged;
            grdGiuong.SelectionChanged+=grdGiuong_SelectionChanged;
            dtNgayvao.ValueChanged += _ValueChanged;

            txtRoom_code._OnEnterMe += txtRoom_code__OnEnterMe;
            txtBedCode._OnEnterMe += txtBedCode__OnEnterMe;
            nmrSoluong.ValueChanged += nmrSoluong_ValueChanged;
            cboGia.ValueChanged+=cboGia_ValueChanged;
        }
       
        void nmrSoluong_ValueChanged(object sender, EventArgs e)
        {
            if (!m_blnHasLoaded) return;
            dtNgayChuyen.Value = dtNgayvao.Value.AddDays(Utility.DoubletoDbnull(nmrSoluong.Value));
        }
        private void txtBedCode__OnEnterMe()
        {
            if (!m_blnHasLoaded) return;
            Utility.GotoNewRowJanus(grdGiuong, NoitruDmucGiuongbenh.Columns.IdGiuong,
                Utility.sDbnull(txtBedCode.MyID, ""));
        }

        private void txtRoom_code__OnEnterMe()
        {
            if (!m_blnHasLoaded) return;
            Utility.GotoNewRowJanus(grdBuong, NoitruDmucBuong.Columns.IdBuong, Utility.sDbnull(txtRoom_code.MyID, ""));
        }

        void txtSoluong_LostFocus(object sender, EventArgs e)
        {
            dtNgayChuyen.Value = dtNgayvao.Value.AddDays(Utility.Int32Dbnull(nmrSoluong.Value, 1));
        }
        void txtGia__OnEnterMe()
        {
            //cboGia.Text = txtGia.Text;
            cboGia.SelectedIndex = Utility.GetSelectedIndex(cboGia, txtGia.MyID.ToString());
        }

        void cboGia_ValueChanged(object sender, EventArgs e)
        {
            if (!m_blnHasLoaded) return;
            txtGia.SetId(cboGia.Value);
        }
        private void txtGia__OnSelectionChanged()
        {
            cboGia.Text = txtGia.Text;
            cboGia.SelectedIndex = Utility.GetSelectedIndex(cboGia, txtGia.MyID.ToString());
        }
        void txtKhoanoitru__OnSelectionChanged()
        {
            
        }

        void txtKhoanoitru__OnEnterMe()
        {
            m_blnHasLoaded = false;
            m_dtZoom = THU_VIEN_CHUNG.NoitruTimkiembuongTheokhoa(Utility.Int32Dbnull(txtKhoanoitru.MyID));

            Utility.SetDataSourceForDataGridEx_Basic(grdBuong, m_dtZoom, true, true, "1=1",
                "sluong_giuong_trong desc,ten_buong");
            txtRoom_code.Init(m_dtZoom,
                new List<string>
                        {
                            NoitruDmucBuong.Columns.IdBuong,
                            NoitruDmucBuong.Columns.MaBuong,
                            NoitruDmucBuong.Columns.TenBuong
                        });
            m_blnHasLoaded = true;
            ChonBuong();
        }
        private void grdBuong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBedCode.Focus();
                txtRoom_code.Text = Utility.sDbnull(grdBuong.GetValue(NoitruDmucBuong.Columns.TenBuong));
            }
        }

        private void grdBuong_SelectionChanged(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdBuong) || !m_blnHasLoaded) return;
            ChonBuong();
        }

        private void ChonBuong()
        {
            m_blnHasLoaded = false;
            string idBuong = Utility.sDbnull(grdBuong.GetValue(NoitruDmucBuong.Columns.IdBuong), -1);
            txtRoom_code.SetId(idBuong);
            m_dtGiuong = THU_VIEN_CHUNG.NoitruTimkiemgiuongTheobuong(Utility.Int32Dbnull(txtKhoanoitru.MyID),
                Utility.Int32Dbnull(idBuong), 0);
            Utility.SetDataSourceForDataGridEx_Basic(grdGiuong, m_dtGiuong, true, true, "1=1",
                "isFull asc,dang_nam ASC,ten_giuong");
            m_blnHasLoaded = true;
            if (m_dtGiuong.Rows.Count <= 0)
            {
                //xóa trắng phần giường và giá
                txtBedCode.Init(m_dtGiuong, new List<string>
                {
                    NoitruDmucGiuongbenh.Columns.IdGiuong,
                    NoitruDmucGiuongbenh.Columns.MaGiuong,
                    NoitruDmucGiuongbenh.Columns.TenGiuong
                });
                txtBedCode.SetId(-1);
                txtGia.SetId(-1);
                cboGia.DataSource = null;
                lblGiaBG.Text = "Chọn giá";
                txtGia.RaiseEnterEvents();
                return;
            }
            string oldBed = txtBedCode.MyCode;
            txtBedCode.Init(m_dtGiuong,
                new List<string>
                {
                    NoitruDmucGiuongbenh.Columns.IdGiuong,
                    NoitruDmucGiuongbenh.Columns.MaGiuong,
                    NoitruDmucGiuongbenh.Columns.TenGiuong
                });
            if (em_Action == action.Update && objPhanbuonggiuong != null)
            {
                txtBedCode.SetCode(objPhanbuonggiuong.IdGiuong);
                Utility.GotoNewRowJanus(grdGiuong, "id_giuong", objPhanbuonggiuong.IdGiuong.ToString());
            }
            if (txtBedCode.MyID == "-1")
            {
                if (grdGiuong.DataSource != null)
                {
                    grdGiuong.MoveFirst();
                    ChonGiuong();
                }
            }
            else
                ChonGiuong();
            
        }

       
        private void grdGiuong_SelectionChanged(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdGiuong) || !m_blnHasLoaded) return;
            ChonGiuong();
        }

        private void ChonGiuong()
        {
            Int16 idGiuong = Utility.Int16Dbnull(grdGiuong.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong), -1);
            txtBedCode.SetId(idGiuong);
            LoadGiaGiuong();
        }
        void LoadGiaGiuong()
        {
            DataTable dtGia = new dmucgiagiuong_busrule().dsGetLoaigiuong(Utility.Int16Dbnull(txtBedCode.MyID, -1)).Tables[0];
            dtGia.DefaultView.Sort = NoitruGiabuonggiuong.Columns.SttHthi + "," +
                                     NoitruGiabuonggiuong.Columns.TenGia;
            //Utility.CapnhatDongia(dtGia, NoitruGiabuonggiuong.Columns.GiaDichvu, objDoituongKcb);
            txtGia.Init(dtGia,
                new List<string>
                        {
                            NoitruGiabuonggiuong.Columns.IdGia,
                            NoitruGiabuonggiuong.Columns.MaGia,
                            NoitruGiabuonggiuong.Columns.TenGia
                        });
            cboGia.DataSource = dtGia;
            cboGia.DataMember = NoitruGiabuonggiuong.Columns.IdGia;
            cboGia.ValueMember = NoitruGiabuonggiuong.Columns.IdGia;
            cboGia.DisplayMember = NoitruGiabuonggiuong.Columns.TenGia;
            if (em_Action == action.Update && objPhanbuonggiuong != null) txtGia.SetId(objPhanbuonggiuong.IdGia);
            txtGia.RaiseEnterEvents();
            lblGiaBG.Text = string.Format("Chọn giá ({0}):", dtGia.Rows.Count.ToString());
            if (txtGia.MyID == "-1" && dtGia.Rows.Count > 1)
                cboGia.Text = "Mời bạn chọn giá buồng giường";
            if (dtGia.Rows.Count == 1)
                if (objPhanbuonggiuong!=null && Utility.sDbnull(objPhanbuonggiuong.IdGia, "-1") != "-1" && Utility.sDbnull(objPhanbuonggiuong.IdGiuong, "-1") == txtBedCode.MyID)
                    cboGia.SelectedIndex = Utility.GetSelectedIndex(cboGia, Utility.sDbnull(objPhanbuonggiuong.IdGia, "-1"));
                else
                    cboGia.SelectedIndex = 0;
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
        private void frm_Them_Sua_BG_Load(object sender, EventArgs e)
        {
           // LoadData();
            m_blnHasLoaded = true;
        }
        public void LoadData()
        {
            maluotkham = Utility.AutoFullPatientCode(txtMaLanKham.Text);
            txtMaLanKham.Text = maluotkham;
            m_dtKhoaNoItru = THU_VIEN_CHUNG.Laydanhmuckhoa("NOI", 0);//, Utility.Int32Dbnull(txtDepartment_ID.Text));
            txtKhoanoitru.Init(m_dtKhoaNoItru, new List<string>() { DmucKhoaphong.Columns.IdKhoaphong, DmucKhoaphong.Columns.MaKhoaphong, DmucKhoaphong.Columns.TenKhoaphong });
            txtKhoanoitru._TextAlign = HorizontalAlignment.Left;
            BindData();
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
                    objDoituongKcb = DmucDoituongkcb.FetchByID(objLuotkham.IdDoituongKcb);
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
                    NoitruPhanbuonggiuong obgBGHientai = NoitruPhanbuonggiuong.FetchByID(objLuotkham.IdRavien);
                   
                    if (obgBGHientai != null)
                    {
                        dtNgayvao.Value = obgBGHientai.NgayVaokhoa;
                        nmrSoluong.Value = Utility.DecimaltoDbnull(obgBGHientai.SoLuong, 0);
                        txtPatientDept_ID.Text = Utility.sDbnull(obgBGHientai.Id);
                        chkKhongtinh.Checked = !Utility.Byte2Bool(obgBGHientai.TinhChiphi);
                        NoitruDmucBuong objRoom = NoitruDmucBuong.FetchByID(obgBGHientai.IdBuong);
                        if (objRoom != null)
                        {
                            txtSoPhong.Text = Utility.sDbnull(objRoom.TenBuong);
                            txtSoPhong.Tag = Utility.sDbnull(obgBGHientai.IdBuong);
                        }
                        NoitruDmucGiuongbenh objNoitruDmucGiuongbenh = NoitruDmucGiuongbenh.FetchByID(obgBGHientai.IdGiuong);
                        if (objNoitruDmucGiuongbenh != null)
                        {
                            txtSoGiuong.Text = Utility.sDbnull(objNoitruDmucGiuongbenh.TenGiuong);
                            txtSoGiuong.Tag = Utility.sDbnull(obgBGHientai.IdGiuong);
                        }
                    }
                    if (em_Action == action.Update)
                    {
                        txtKhoanoitru.SetId(objPhanbuonggiuong.IdKhoanoitru);
                        txtKhoanoitru.RaiseEnterEvents();
                        Utility.GotoNewRowJanus(grdBuong, "id_buong", Utility.sDbnull(objPhanbuonggiuong.IdBuong, "-1"));
                        Utility.GotoNewRowJanus(grdGiuong, "id_giuong", Utility.sDbnull(objPhanbuonggiuong.IdGiuong, "-1"));
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
                Utility.ShowMsg("Bệnh nhân chưa nhập viện nội trú nên bạn không thể thêm hoặc sửa buồng giường. Đề nghị bạn kiểm tra lại");
                return false;
            }
            if (objLuotkham.TrangthaiNoitru == 3)
            {
                Utility.ShowMsg("Bệnh nhân đã được khoa nội trú cho ra viện nên bạn không thể thêm hoặc sửa buồng giường");
                return false;
            }
            if (objLuotkham.TrangthaiNoitru == 4)
            {
                Utility.ShowMsg("Bệnh nhân đã được khoa nội trú tổng hợp dữ liệu và xác nhận chuyển tài chính duyệt(hoặc thanh toán) nên bạn không thể thêm hoặc sửa buồng giường");
                return false;
            }
            if (objLuotkham.TrangthaiNoitru == 5)
            {
                Utility.ShowMsg("Bệnh nhân đã được duyệt chờ thanh toán nội trú nên bạn không thể thêm hoặc sửa buồng giường");
                return false;
            }
            if (Utility.Byte2Bool(objLuotkham.TthaiThanhtoannoitru) || objLuotkham.TrangthaiNoitru == 6)
            {
                Utility.ShowMsg("Bệnh nhân đã được thanh toán nội trú nên bạn không thể thêm hoặc sửa buồng giường");
                return false;
            }

            if (Utility.Int32Dbnull(txtKhoanoitru.MyID, -1) == -1)
            {
                Utility.ShowMsg("Bạn phải chọn khoa nội trú", "Thông báo", MessageBoxIcon.Warning);
                txtKhoanoitru.Focus();
                return false;
            }
            if (!Utility.isValidGrid(grdBuong))
            {
                Utility.ShowMsg("Bạn phải chọn Buồng cần chuyển");
                txtRoom_code.Focus();
                txtRoom_code.SelectAll();
                return false;
            }
            DataTable dtfull = SPs.NoitruBuongiuongKiemtrathuefullBuong(Utility.Int32Dbnull(grdBuong.GetValue("id_buong"))).GetDataSet().Tables[0];
            if (dtfull != null && dtfull.Rows.Count > 0)
            {
                Utility.ShowMsg("Buồng bạn chọn đã được bệnh nhân khác thuê nguyên buồng. Vui lòng chọn buồng-giường khác");
                txtRoom_code.Focus();
                txtRoom_code.SelectAll();
                return false;
            }
            if (!Utility.isValidGrid(grdGiuong))
            {
                Utility.ShowMsg("Bạn phải chọn giường cần chuyển");
                txtBedCode.Focus();
                txtBedCode.SelectAll();
                return false;
            }
            if (lblGiaBG.Visible && Utility.Int32Dbnull(txtGia.MyID, -1) == -1)
            {
                Utility.ShowMsg("Bạn phải chọn giá buồng giường");
                txtGia.Focus();
                txtGia.SelectAll();
                return false;
            }

            if (Utility.Int32Dbnull(Utility.DecimaltoDbnull(nmrSoluong.Value)) != THU_VIEN_CHUNG.Songay(dtNgayvao.Value, dtNgayChuyen.Value))
            {
                DateTime ngayvaokhoa=dtNgayvao.Value.AddDays(Utility.Int32Dbnull(Utility.DecimaltoDbnull(nmrSoluong.Value))) ;
                ngayvaokhoa=new DateTime(ngayvaokhoa.Year, ngayvaokhoa.Month, ngayvaokhoa.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                if (!Utility.AcceptQuestion(string.Format("Bạn có chắc chắn số ngày nằm buồng giường là: {0} thay vì {1}(Hệ thống tự động tính toán từ ngày bắt đầu {2} đến ngày kết thúc {3}) hay không?\nNhấn Yes để hệ thống sẽ tự động điều chỉnh ngày kết thúc từ {4} thành {5}\nNhấn No để chỉnh lại ngày kết thúc trước khi thực hiện ", nmrSoluong.Value, THU_VIEN_CHUNG.Songay(dtNgayvao.Value, dtNgayChuyen.Value).ToString(), dtNgayvao.Value.ToString("dd/MM/yyyy HH:mm:ss"), dtNgayChuyen.Value.ToString("dd/MM/yyyy HH:mm:ss"), dtNgayChuyen.Value.ToString("dd/MM/yyyy HH:mm:ss"), ngayvaokhoa.ToString("dd/MM/yyyy HH:mm:ss")), "Cảnh báo", true))
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
            if (em_Action == action.Insert)//Thêm buồng giường mới
            {
                NoitruPhanbuonggiuong objNew = new NoitruPhanbuonggiuong();

                if (objNew != null)
                {
                    objNew.IdBenhnhan = objLuotkham.IdBenhnhan;
                    objNew.MaLuotkham = objLuotkham.MaLuotkham;
                    objNew.IdKhoanoitru = Utility.Int16Dbnull(txtKhoanoitru.MyID, -1);
                    objNew.IdBuong = Utility.Int16Dbnull(grdBuong.GetValue("id_buong"), -1);
                    objNew.IdGiuong = Utility.Int16Dbnull(grdGiuong.GetValue("id_giuong"), -1);
                    objNew.NgayVaokhoa = dtNgayvao.Value;
                    objNew.SoLuong = Utility.DecimaltoDbnull(nmrSoluong.Value, 0);
                    objNew.IdLichsuDoituongKcb = objLuotkham.IdLichsuDoituongKcb;
                    objNew.TrangthaiChuyen = 0;
                    objNew.TrangthaiXacnhan = 0;
                    objNew.TinhChiphi = Utility.ByteDbnull(chkKhongtinh.Checked ? 0 : 1, 1);
                    objNew.NoiTru = 1;
                    objNew.KieuGiuong = 1;//Bổ sung xử lý tình huống xóa giường
                    objNew.TrangthaiThanhtoan = 0;
                    objNew.KieuThue = "GIUONG";
                    objNew.IdGia = Utility.Int32Dbnull(cboGia.Value, 0);
                    noitru_nhapvien.LayThongTinGia(objNew, objLuotkham);
                    //Cập nhật lại đơn giá theo đối tượng nước ngoài
                    objNew.DonGia = Utility.DecimaltoDbnull(objNew.DonGia) * (1 + Utility.DecimaltoDbnull(objDoituongKcb.MotaThem, 0) / 100);
                    objNew.GiaGoc = objNew.DonGia;
                    objNew.NgayPhangiuong = dtNgayvao.Value;
                    objNew.NgayKetthuc = dtNgayChuyen.Value;
                    objNew.TenHienthi = grdGiuong.GetValue("ten_giuong").ToString();
                    objNew.NguoiPhangiuong = globalVariables.UserName;
                    objNew.SoluongGio = (int)Math.Ceiling((objNew.NgayKetthuc.Value - objNew.NgayVaokhoa).TotalHours);
                    objNew.NguoiTao = globalVariables.UserName;
                    objNew.TrangThai = 1;
                    objNew.NgayTao = DateTime.Now;
                    objNew.Save();
                    if (_OnSucess != null) _OnSucess(objNew.Id);
                    Utility.ShowMsg(string.Format("Đã thêm thông tin buồng giường thành công"));
                }
            }
            else//Sửa buồng giường
            {
                if (objPhanbuonggiuong != null)
                {
                    objPhanbuonggiuong.MarkOld();
                    objPhanbuonggiuong.IsNew = false;
                    objPhanbuonggiuong.TinhChiphi = Utility.ByteDbnull(chkKhongtinh.Checked ? 0 : 1, 1);
                    objPhanbuonggiuong.IdKhoanoitru = Utility.Int16Dbnull(txtKhoanoitru.MyID, -1);
                    objPhanbuonggiuong.IdBuong = Utility.Int16Dbnull(grdBuong.GetValue("id_buong"), -1);
                    objPhanbuonggiuong.IdGiuong = Utility.Int16Dbnull(grdGiuong.GetValue("id_giuong"), -1);
                    objPhanbuonggiuong.TenHienthi = grdGiuong.GetValue("ten_giuong").ToString();
                    objPhanbuonggiuong.NgayVaokhoa = dtNgayvao.Value;

                    objPhanbuonggiuong.NgayPhangiuong = dtNgayvao.Value;
                    objPhanbuonggiuong.NgayKetthuc = dtNgayChuyen.Value;
                    objPhanbuonggiuong.SoluongGio = (int)Math.Ceiling((objPhanbuonggiuong.NgayKetthuc.Value - objPhanbuonggiuong.NgayVaokhoa).TotalHours);
                    objPhanbuonggiuong.SoLuong = Utility.DecimaltoDbnull(nmrSoluong.Value, 0);
                    //Tính lại phần giá nếu chọn sang đối tượng giá khác
                    objPhanbuonggiuong.IdGia = Utility.Int32Dbnull(cboGia.Value, 0);
                    noitru_nhapvien.LayThongTinGia(objPhanbuonggiuong, objLuotkham);
                    //Cập nhật lại đơn giá theo đối tượng nước ngoài
                    objPhanbuonggiuong.DonGia = Utility.DecimaltoDbnull(objPhanbuonggiuong.DonGia) * (1 + Utility.DecimaltoDbnull(objDoituongKcb.MotaThem, 0) / 100);
                    objPhanbuonggiuong.NguoiSua = globalVariables.UserName;
                    objPhanbuonggiuong.NgaySua = DateTime.Now;
                    objPhanbuonggiuong.Save();
                    if (_OnSucess != null) _OnSucess(objPhanbuonggiuong.Id);
                    Utility.ShowMsg(string.Format("Đã sửa thông tin buồng giường thành công"));
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
                m_dtZoom =
                    THU_VIEN_CHUNG.NoitruTimkiembuongTheokhoa(Utility.Int32Dbnull(txtKhoanoitru.MyID));
            }
        }

        
        /// <summary>
        /// hàm thực hiện việc phím tắt thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_Them_Sua_BG_KeyDown(object sender, KeyEventArgs e)
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

        private void _ValueChanged(object sender, EventArgs e)
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

        private void _TextChanged(object sender, EventArgs e)
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
                nmrSoluong.Value =Utility.DecimaltoDbnull(  THU_VIEN_CHUNG.Songay(dtNgayvao.Value, dtNgayChuyen.Value).ToString(),0);
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