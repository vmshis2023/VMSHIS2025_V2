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
using VNS.HIS.NGHIEPVU;
using System.Collections.Generic;


namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_Chuyengiuong : Form
    {
        public int IDBuonggiuong = -1;
        public bool b_CallParent;
        public bool b_Cancel=true;
        public bool b_HasValue;
        public GridEX grdList;
        private DataTable m_dtBuong = new DataTable();
        private DataTable m_dtGiuong = new DataTable();
        public KcbLuotkham objLuotkham;
        DmucDoituongkcb objDoituongKcb;
        public DataTable p_DanhSachPhanBuongGiuong = new DataTable();
        private string rowFilter = "1=1";
        private readonly Logger log;
        private bool AllowGridSelecttionChanged = true;
        bool m_blnHasLoaded = false;
        public frm_Chuyengiuong()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            grdGiuong.CellSelectionMode = Janus.Windows.GridEX.CellSelectionMode.SingleCell;
            InitEvents();
            dtNgayChuyen.Value = globalVariables.SysDate;
            txtGio.Value = globalVariables.SysDate.Hour;
            txtPhut.Value = globalVariables.SysDate.Minute;
            
            //cmdConfig.Visible = globalVariables.IsAdmin;
           
            txtPatientDept_ID.Visible = globalVariables.IsAdmin;
            lblGiaBG.Visible = txtGia.Visible = cboGia.Visible = THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_APGIABUONGGIUONG_THEODANHMUCGIA", "0", false) == "1";
            txtHour2Cal.Text =THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_SOGIO_LAMTRONNGAY", "1", false);
            ucThongtinnguoibenh1.SetReadonly();
            CauHinh();
        }
        void InitEvents()
        {
            this.Load += new System.EventHandler(this.frm_Chuyengiuong_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frm_Chuyengiuong_KeyDown);
            this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
            this.cmdExit.Click += new System.EventHandler(this.cmdExit_Click);
            dtNgayChuyen.ValueChanged += new EventHandler(dtNgayChuyen_ValueChanged);
            dtNgayChuyen.TextChanged += new EventHandler(dtNgayChuyen_TextChanged);
            dtNgayvao.ValueChanged += new EventHandler(dtNgayvao_ValueChanged);
            chkAutoCal.CheckedChanged += new EventHandler(chkAutoCal_CheckedChanged);

            txtGia._OnSelectionChanged += txtGia__OnSelectionChanged;
            txtGia._OnEnterMe += txtGia__OnEnterMe;
            txtRoom_code._OnEnterMe += txtRoom_code__OnEnterMe;
            grdBuong.SelectionChanged += grdBuong_SelectionChanged;
            grdBuong.KeyDown += grdBuong_KeyDown;
            grdGiuong.KeyDown += grdGiuong_KeyDown;
            grdGiuong.SelectionChanged += grdGiuong_SelectionChanged;
            grdGiuong.MouseDoubleClick += grdGiuong_MouseDoubleClick;
            txtBedCode._OnEnterMe += txtBedCode__OnEnterMe;
            cboGia.ValueChanged += cboGia_ValueChanged;
            nmrSoluong.ValueChanged += nmrSoluong_ValueChanged;
            ucThongtinnguoibenh1._OnEnterMe += ucThongtinnguoibenh1__OnEnterMe;
        }

        void ucThongtinnguoibenh1__OnEnterMe()
        {
            if (ucThongtinnguoibenh1.objLuotkham != null)
            {
                this.objLuotkham = ucThongtinnguoibenh1.objLuotkham;
                objPhanbuonggiuong = null;
                ClearControl();
                BindData();
            }
        }

        void nmrSoluong_ValueChanged(object sender, EventArgs e)
        {
            if (!m_blnHasLoaded) return;
            dtNgayChuyen.Value = dtNgayvao.Value.AddDays(Utility.DoubletoDbnull( nmrSoluong.Value));
        }

        void nmrSoluong_TextChanged(object sender, EventArgs e)
        {
            //dtNgayChuyen.Value = dtNgayvao.Value.AddDays(Utility.Int32Dbnull(nmrSoluong.Text, 0));
        }

        void cboGia_ValueChanged(object sender, EventArgs e)
        {
            if (!m_blnAllowSelectionChanged) return;
            txtGia.SetId(cboGia.Value);
        }
        bool m_blnAllowSelectionChanged = false;
        void txtGia__OnEnterMe()
        {
            //cboGia.Text = txtGia.Text;
            cboGia.SelectedIndex = Utility.GetSelectedIndex(cboGia, txtGia.MyID.ToString());
        }
        private void grdGiuong_SelectionChanged(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdGiuong) || !m_blnAllowSelectionChanged) return;
            ChonGiuong();
        }
        void ChonGiuong()
        {
            Int16 idGiuong = Utility.Int16Dbnull(grdGiuong.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong), -1);
            //string oldID =Utility.sDbnull( txtGia.MyID,"-1");
            //DataTable dtGia = new dmucgiagiuong_busrule().dsGetLoaigiuong(idGiuong).Tables[0];
            //dtGia.DefaultView.Sort = NoitruGiabuonggiuong.Columns.SttHthi + "," +
            //                         NoitruGiabuonggiuong.Columns.TenGia;
            //txtGia.Init(dtGia,
            //    new List<string>
            //            {
            //                NoitruGiabuonggiuong.Columns.IdGia,
            //                NoitruGiabuonggiuong.Columns.MaGia,
            //                NoitruGiabuonggiuong.Columns.TenGia
            //            });
            //cboGia.DataSource = dtGia;
            //cboGia.DataMember = NoitruGiabuonggiuong.Columns.IdGia;
            //cboGia.ValueMember = NoitruGiabuonggiuong.Columns.IdGia;
            //cboGia.DisplayMember = NoitruGiabuonggiuong.Columns.TenGia;
            //cboGia.Value = oldID;
            //txtGia.SetId(oldID);
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
            txtGia.SetId(objPhanbuonggiuong.IdGia);
            txtGia.RaiseEnterEvents();
            lblGiaBG.Text = string.Format("Chọn giá ({0}):", dtGia.Rows.Count.ToString());
            if (txtGia.MyID == "-1" && dtGia.Rows.Count > 1)
                cboGia.Text = "Mời bạn chọn giá buồng giường";
            if (dtGia.Rows.Count == 1)
                if (Utility.sDbnull(objPhanbuonggiuong.IdGia, "-1") != "-1" && Utility.sDbnull(objPhanbuonggiuong.IdGiuong, "-1") == txtBedCode.MyID)
                    cboGia.SelectedIndex = Utility.GetSelectedIndex(cboGia, Utility.sDbnull(objPhanbuonggiuong.IdGia, "-1"));
                else
                    cboGia.SelectedIndex = 0;
        }
        private void txtBedCode__OnEnterMe()
        {
            if (!m_blnAllowSelectionChanged) return;
            Utility.GotoNewRowJanus(grdGiuong, NoitruDmucGiuongbenh.Columns.IdGiuong,
                Utility.sDbnull(txtBedCode.MyID, ""));
        }

        private void txtRoom_code__OnEnterMe()
        {
            if (!m_blnAllowSelectionChanged) return;
            Utility.GotoNewRowJanus(grdBuong, NoitruDmucBuong.Columns.IdBuong, Utility.sDbnull(txtRoom_code.MyID, ""));
        }
        void grdGiuong_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            grdGiuong_KeyDown(sender, new KeyEventArgs(Keys.Enter));
        }
        private void grdGiuong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && Utility.isValidGrid(grdGiuong))
            {
                cmdSave_Click(cmdSave, new EventArgs());
            }
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
            if (!AllowGridSelecttionChanged || !Utility.isValidGrid(grdBuong)) return;
            ChonBuong();
        }
        private void ChonBuong()
        {
            string idBuong = Utility.sDbnull(grdBuong.GetValue(NoitruDmucBuong.Columns.IdBuong), -1);
            txtRoom_code.SetId(idBuong);
            m_dtGiuong = THU_VIEN_CHUNG.NoitruTimkiemgiuongTheobuong(Utility.Int32Dbnull(objLuotkham.IdKhoanoitru),
                Utility.Int32Dbnull(idBuong),0);
            Utility.SetDataSourceForDataGridEx_Basic(grdGiuong, m_dtGiuong, true, true, "1=1",
                "isFull asc,dang_nam ASC,ten_giuong");
            if (m_dtGiuong.Rows.Count <= 0)
            {
                //xóa trắng phần giường và giá
                txtBedCode.SetId(-1);
                txtGia.SetId(-1);
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
            txtBedCode.SetId(objPhanbuonggiuong.IdGiuong);
            if (txtBedCode.MyID == "-1")
            {
                if (grdGiuong.DataSource != null)
                {
                    grdGiuong.MoveFirst();
                    grdGiuong_SelectionChanged(grdGiuong, new EventArgs());
                }
            }
            else
                txtBedCode.RaiseEnterEvents();
            Utility.GotoNewRowJanus(grdGiuong, "id_giuong", Utility.sDbnull(objPhanbuonggiuong.IdGiuong, "-1"));
            //if (txtBedCode.MyCode == "-1")
            //{
            //    string idGiuong = Utility.sDbnull(grdGiuong.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong), -1);
            //    txtBedCode.SetId(idGiuong);
            //}
        }
        private void txtGia__OnSelectionChanged()
        {
            cboGia.Text = txtGia.Text;
            cboGia.SelectedIndex = Utility.GetSelectedIndex(cboGia, txtGia.MyID.ToString());
        }

        void chkAutoCal_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAutoCal.Checked)
                nmrSoluong.Value =Utility.DecimaltoDbnull( THU_VIEN_CHUNG.Songay(dtNgayvao.Value, dtNgayChuyen.Value).ToString(),0);
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

        void dtNgayChuyen_TextChanged(object sender, EventArgs e)
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
                nmrSoluong.Text = THU_VIEN_CHUNG.Songay(dtNgayvao.Value, dtNgayChuyen.Value).ToString();
        }

        void dtNgayChuyen_ValueChanged(object sender, EventArgs e)
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
            //{
                nmrSoluong.Text = THU_VIEN_CHUNG.Songay(dtNgayvao.Value, dtNgayChuyen.Value).ToString();
            //}
        }

        private string maluotkham { get; set; }

        private void CauHinh()
        {
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// hàm thực hiên việc load thông tin của khoa Buồng hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_Chuyengiuong_Load(object sender, EventArgs e)
        {
            try
            {
                m_blnAllowSelectionChanged = false;
                if (b_CallParent)
                {
                    LoadData();

                    chkAutoCal_CheckedChanged(chkAutoCal, e);
                    txtRoom_code.SelectAll();
                    txtRoom_code.Focus();
                }
                txtTotalHour.Text = Math.Ceiling((dtNgayChuyen.Value - dtNgayvao.Value).TotalHours).ToString();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
            finally
            {
                m_blnHasLoaded = true;
                m_blnAllowSelectionChanged = true;
                Utility.GotoNewRowJanus(grdBuong, "id_buong", Utility.sDbnull(objPhanbuonggiuong.IdBuong, "-1"));
                grdBuong_SelectionChanged(grdBuong, e);
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

        public NoitruPhanbuonggiuong objPhanbuonggiuong = null;
        private void BindData()
        {
            if (objLuotkham == null)
                this.objLuotkham = ucThongtinnguoibenh1.objLuotkham;
            if (objLuotkham != null)
            {
                objDoituongKcb = DmucDoituongkcb.FetchByID(objLuotkham.IdDoituongKcb);
                if (objPhanbuonggiuong == null)//99,99% ko xảy ra
                    objPhanbuonggiuong = NoitruPhanbuonggiuong.FetchByID(Utility.Int32Dbnull(objLuotkham.IdRavien, 0));
                if (objPhanbuonggiuong != null)
                {
                    nmrSoluong.Value = Utility.DecimaltoDbnull(objPhanbuonggiuong.SoLuong, 0);
                    dtNgayvao.Value = objPhanbuonggiuong.NgayVaokhoa;
                    dtNgayChuyen.MinDate = dtNgayvao.Value;
                    txtPatientDept_ID.Text = Utility.sDbnull(objPhanbuonggiuong.Id);
                }
                Int16 id_buong = Utility.Int16Dbnull(objPhanbuonggiuong.IdBuong, -1);
                Int16 id_giuong = Utility.Int16Dbnull(objPhanbuonggiuong.IdGiuong, -1);
                m_dtBuong = THU_VIEN_CHUNG.NoitruTimkiembuongTheokhoa(Utility.Int32Dbnull(objPhanbuonggiuong.IdKhoanoitru));
                Utility.SetDataSourceForDataGridEx_Basic(grdBuong, m_dtBuong, true, true, "1=1", "sluong_giuong_trong desc,ten_buong");
                txtRoom_code.Init(m_dtBuong, new List<string>() { NoitruDmucBuong.Columns.IdBuong, NoitruDmucBuong.Columns.MaBuong, NoitruDmucBuong.Columns.TenBuong });
                txtRoom_code.SetId(id_buong);
                txtBedCode.SetId(id_giuong);

                //txtGia.RaiseEnterEvents();
                if (grdBuong.DataSource != null)
                {
                    grdBuong.MoveFirst();
                }
            }
            else
            {
                ClearControl();
                if (m_dtBuong != null) m_dtBuong.Clear();
                if (m_dtGiuong != null) m_dtGiuong.Clear();
            }
        }

        
        

        void LoadData()
        {
            ucThongtinnguoibenh1.Refresh_V1(false);
            BindData();
            ModifyCommand();
            chkAutoCal_CheckedChanged(chkAutoCal, new EventArgs());
            txtTotalHour.Text = Math.Ceiling((dtNgayChuyen.Value - dtNgayvao.Value).TotalHours).ToString();
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
        }

        private bool IsValidData()
        {
            errorProvider1.Clear();
            KcbLuotkham objKcbLuotkham = Utility.getKcbLuotkham(ucThongtinnguoibenh1.objLuotkham);
            if (objKcbLuotkham != null && objKcbLuotkham.TrangthaiNoitru < 1)
            {
                Utility.ShowMsg("Bệnh nhân này chưa nhập viện nên không được phép phân buồng giường", "Thông báo",
                                MessageBoxIcon.Warning);
                errorProvider1.SetError(ucThongtinnguoibenh1.txtMaluotkham, "Bệnh nhân này chưa nhập viện nên không được phép phân buồng giường");
                return false;
            }
            if (dtNgayChuyen.Value < dtNgayvao.Value)
            {
                Utility.ShowMsg("Ngày chuyển  giường phải lớn hơn hoặc bằng ngày vào giường. Mời bạn kiểm tra lại", "Thông báo",
                    MessageBoxIcon.Warning);
                errorProvider1.SetError(dtNgayChuyen, "Ngày chuyển  giường phải lớn hơn hoặc bằng ngày vào giường. Mời bạn kiểm tra lại");
                dtNgayChuyen.Focus();
                return false;
            }
            if (!Utility.isValidGrid(grdBuong))
            {
                Utility.ShowMsg("Bạn phải chọn Buồng cần chuyển", "Thông báo", MessageBoxIcon.Warning);
                errorProvider1.SetError(txtRoom_code, "Bạn phải chọn Buồng cần chuyển");
                // cboPhong.Focus();
                txtRoom_code.Focus();
                txtRoom_code.SelectAll();
                return false;
            }


            if (!Utility.isValidGrid(grdGiuong))
            {
                Utility.ShowMsg("Bạn phải chọn giường cần chuyển", "Thông báo", MessageBoxIcon.Warning);
                errorProvider1.SetError(txtBedCode, "Bạn phải chọn giường cần chuyển");
                txtBedCode.Focus();
                txtBedCode.SelectAll();
                // cboGiuong.Focus();
                return false;
            }
            if (lblGiaBG.Visible && Utility.Int32Dbnull(txtGia.MyID, -1) == -1)
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn giá buồng giường", true);
                errorProvider1.SetError(txtGia, "Bạn phải chọn giá buồng giường");
                txtGia.Focus();
                txtGia.SelectAll();
                return false;
            }
            if (objPhanbuonggiuong.IdGiuong == Utility.Int16Dbnull(grdGiuong.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong)))//Chưa chọn giường khác
            {
                Utility.ShowMsg(string.Format("Bạn phải chọn giường cần chuyển khác với {0} thuộc {1}", ucThongtinnguoibenh1.txtGiuong.Text, ucThongtinnguoibenh1.txtBuong.Text), "Thông báo", MessageBoxIcon.Warning);
                errorProvider1.SetError(txtRoom_code, string.Format("Bạn phải chọn giường cần chuyển khác với {0} thuộc {1}", ucThongtinnguoibenh1.txtGiuong.Text, ucThongtinnguoibenh1.txtBuong.Text));
                return false;
            }
            DataTable dt = new noitru_nhapvien().NoitruKiemtraBuongGiuong((int)objLuotkham.IdKhoanoitru, Utility.Int16Dbnull(grdBuong.GetValue(NoitruDmucGiuongbenh.Columns.IdBuong)), Utility.Int16Dbnull(grdGiuong.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong)),objLuotkham.IdBenhnhan,objLuotkham.MaLuotkham);
            if (dt != null && dt.Rows.Count > 0)
            {
                Utility.SetMsg(lblMsg, string.Format("Giường này đang được nằm bởi bệnh nhân\n{0}\nMời bạn chọn giường khác", Utility.sDbnull(dt.Rows[0][KcbDanhsachBenhnhan.Columns.TenBenhnhan])), true);
                errorProvider1.SetError(txtBedCode, string.Format("Giường này đang được nằm bởi bệnh nhân\n{0}\nMời bạn chọn giường khác", Utility.sDbnull(dt.Rows[0][KcbDanhsachBenhnhan.Columns.TenBenhnhan])));
                txtBedCode.Focus();
                txtBedCode.SelectAll();
                return false;
            }
            if (nmrSoluong.Value!=Utility.DecimaltoDbnull( THU_VIEN_CHUNG.Songay(dtNgayvao.Value, dtNgayChuyen.Value),0))
            {
                if (!Utility.AcceptQuestion(string.Format("Bạn có chắc chắn số ngày nằm viện trước khi chuyển là {0} ngày thay vì {1} ngày(Hệ thống tự động tính toán) hay không?", nmrSoluong.Text, THU_VIEN_CHUNG.Songay(dtNgayvao.Value, dtNgayChuyen.Value).ToString()), "Cảnh báo", true))
                {
                    nmrSoluong.Focus();
                   
                    return false;
                }
            }
            if (chkKhongtinh.Checked)
            {
                if (!Utility.AcceptQuestion("Hệ thống phát hiện bạn chọn không tính phí giường nằm cho giường đang chuyển.\nBạn có chắc chắn không muốn tính chi phí cho giường đang chuyển hay không?", "Xác nhận", true))
                {
                    return false;
                }
            }
            //if (!chkKhongtinh.Checked)
            //{
            //    if (Utility.DoubletoDbnull(nmrSoluong.Text) <= 0)
            //    {
            //        Utility.ShowMsg("Số ngày điều trị ở giường cũ phải lớn hơn >=0", "Thông báo", MessageBoxIcon.Warning);
            //        nmrSoluong.Focus();
            //        return false;
            //    }
            //}
            return true;
        }

        /// <summary>
        /// hàm thực hiện việc hoạt động lưu lại thông tin 
        /// </summary>
        private void PerformAction()
        {
            NoitruPhanbuonggiuong objPhanbuonggiuong = NoitruPhanbuonggiuong.FetchByID(Utility.Int32Dbnull(txtPatientDept_ID.Text));
            if (objPhanbuonggiuong != null)
            {
                string content = string.Format("Bạn có chắc chắn muốn chuyển từ {0} thuộc {1} sang {2} thuộc {3} hay không?", ucThongtinnguoibenh1.txtGiuong.Text, ucThongtinnguoibenh1.txtBuong.Text, txtBedCode.Text, txtRoom_code.Text);
                if (Utility.AcceptQuestion(content, "Thông báo", true))
                {
                    var ngaychuyenkhoa = new DateTime(dtNgayChuyen.Value.Year, dtNgayChuyen.Value.Month,
                                                      dtNgayChuyen.Value.Day, Utility.Int32Dbnull(txtGio.Text),
                                                      Utility.Int32Dbnull(txtPhut.Text), 00);
                    objPhanbuonggiuong.SoLuong = Utility.DecimaltoDbnull(nmrSoluong.Text);
                    objPhanbuonggiuong.SoluongGio = Utility.Int32Dbnull(txtTotalHour.Text);
                    objPhanbuonggiuong.CachtinhSoluong = (byte)(chkAutoCal.Checked ? 0 : 1);
                    string id_buong_cu =Utility.sDbnull( objPhanbuonggiuong.IdBuong,"NAN");
                    string id_giuong_cu = Utility.sDbnull(objPhanbuonggiuong.IdGiuong, "NAN");
                    //objPhanbuonggiuong.IsGhepGiuong = Utility.ByteDbnull(chkGhepgiuong.Checked);
                    decimal soluongghep = 0;
                    if (chkGhepgiuong.Checked)
                    {
                        soluongghep = nmrSoluongghep.Value;
                    }
                    else
                    {
                        soluongghep = 0;
                    }
                    ActionResult actionResult = new noitru_nhapvien().ChuyenGiuongDieuTri(objPhanbuonggiuong,
                        objLuotkham, ngaychuyenkhoa,
                        Utility.Int16Dbnull(grdBuong.GetValue(NoitruDmucBuong.Columns.IdBuong)),
                        Utility.Int16Dbnull(grdGiuong.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong)),
                        Utility.Int32Dbnull(txtGia.MyID, -1), Utility.ByteDbnull(chkGhepgiuong.Checked), soluongghep, chkKhongtinh.Checked);

                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Chuyển buồng giường cho người bệnh ID bệnh nhân={0}, PID={1}, từ id buồng={2}, id giường={3} sang id buồng={4}, id giường={5} thành công ", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham,id_buong_cu,id_giuong_cu, objPhanbuonggiuong.IdBuong, objPhanbuonggiuong.IdGiuong), newaction.Transfer, this.GetType().Assembly.ManifestModule.Name);
                            txtPatientDept_ID.Text = Utility.sDbnull(objPhanbuonggiuong.Id);
                            Utility.SetMsg(lblMsg, "Bạn chuyển Buồng thành công", true);
                            Utility.ShowMsg(lblMsg.Text);
                            // Utility.ShowMsg("Bạn chuyển Buồng thành công", "Thông báo", MessageBoxIcon.Information);
                            if (b_CallParent)
                            {
                                ProcessChuyenKhoa();
                                b_Cancel = false;
                                Close();
                            }
                            else
                            {
                                ClearControl();
                            }
                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Lỗi trong quá trình chuyển khoa", "Thông báo", MessageBoxIcon.Error);
                            break;
                    }
                }
            }
        }

        private void ProcessChuyenKhoa()
        {
            DataRow query = (from khoa in p_DanhSachPhanBuongGiuong.AsEnumerable()
                             where
                                 Utility.Int32Dbnull(khoa[NoitruPhanbuonggiuong.Columns.Id]) ==
                                 Utility.Int32Dbnull(IDBuonggiuong)
                             select khoa).FirstOrDefault();
            if (query != null)
            {
                NoitruDmucBuong objRoom = NoitruDmucBuong.FetchByID(Utility.Int32Dbnull(Utility.Int32Dbnull(grdBuong.GetValue(NoitruDmucBuong.Columns.IdBuong))));
                if (objRoom != null)
                {
                    query[NoitruDmucBuong.Columns.IdBuong] = Utility.Int32Dbnull(objRoom.IdBuong, -1);
                    query[NoitruDmucBuong.Columns.TenBuong] = Utility.sDbnull(objRoom.TenBuong);
                }
                NoitruDmucGiuongbenh objBed = NoitruDmucGiuongbenh.FetchByID(Utility.Int32Dbnull(Utility.Int32Dbnull(grdGiuong.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong))));
                if (objBed != null)
                {
                    query[NoitruDmucGiuongbenh.Columns.IdGiuong] = Utility.Int32Dbnull(objBed.IdGiuong, -1);
                    query[NoitruDmucGiuongbenh.Columns.TenGiuong] = Utility.sDbnull(objBed.TenGiuong);
                }
                query[NoitruPhanbuonggiuong.Columns.Id] = Utility.sDbnull(txtPatientDept_ID.Text);
                query.AcceptChanges();
            }
        }

        
        /// <summary>
        /// hàm thực hiện việc phím tắt thông tin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_Chuyengiuong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S && e.Control) cmdSave.PerformClick();
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.F2)
            {
                
            }
            if (e.KeyCode == Keys.F3)
            {
               
            }
            if (e.KeyCode == Keys.F5)
            {
                grdBuong_SelectionChanged(grdBuong, new EventArgs());
                return;
            }
        }

        private void ModifyCommand()
        {
            cmdSave.Enabled = objLuotkham!=null;
            grpThongTinChuyenKhoa.Enabled = objLuotkham != null;
        }

       

        private void cmdConfig_Click(object sender, EventArgs e)
        {
            //var frm = new frm_CauHinh_BuongGiuong();
            //frm.HisclsProperties = _hinhPhanBuongGiuongProperties;
            //frm.ShowDialog();
            //CauHinh();
        }

       

        private void lblMessage_Click(object sender, EventArgs e)
        {
        }

        private void txtDepartment_ID_TextChanged(object sender, EventArgs e)
        {
        }

        private void chkGhepgiuong_CheckedChanged(object sender, EventArgs e)
        {

            if (chkGhepgiuong.Checked)
            {
                lblsoluongghep.Visible = true;
                nmrSoluongghep.Visible = true;
            }
            else
            {
                lblsoluongghep.Visible = false;
                nmrSoluongghep.Visible = false;
            }
        }

        private void cmdSave_Click_1(object sender, EventArgs e)
        {

        }

       

       
    }
}