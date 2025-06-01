using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Janus.Windows.GridEX.EditControls;
using SubSonic;
using VNS.HIS.BusRule.Classes;
using VMS.HIS.DAL;
using VNS.HIS.NGHIEPVU;
using VNS.Libs;
using VNS.HIS.UI.Forms.Noitru;

namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_phanbuonggiuong : Form
    {
        public bool BCancel = true;
        private DataTable m_dtDataRoom = new DataTable();
        private DataTable m_dtGiuong = new DataTable();
        private KcbLuotkham objLuotkham;
        public NoitruPhanbuonggiuong ObjPhanbuonggiuong;
        bool AllowValueChanged = false;
        public DataTable PDanhSachPhanBuongGiuong = new DataTable();

        public frm_phanbuonggiuong()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            grdGiuong.CellSelectionMode = Janus.Windows.GridEX.CellSelectionMode.SingleCell;
            InitEvents();
            dtNgayChuyen.Value = globalVariables.SysDate;
            lblGiaBG.Visible =
                txtGia.Visible =
                    cboGia.Visible =
                        THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_APGIABUONGGIUONG_THEODANHMUCGIA", "0", false) =="1";
        }

        private void InitEvents()
        {
            Load += frm_phanbuonggiuong_Load;
            KeyDown += frm_phanbuonggiuong_KeyDown;
            cmdExit.Click += cmdExit_Click;
            cmdSave.Click += cmdSave_Click;
            dtNgayChuyen.TextChanged += dtNgayChuyen_TextChanged;
            dtNgayChuyen.ValueChanged += dtNgayChuyen_ValueChanged;
            
            txtGia._OnSelectionChanged += txtGia__OnSelectionChanged;
            txtGia._OnEnterMe += txtGia__OnEnterMe;
            txtRoom_code._OnEnterMe += txtRoom_code__OnEnterMe;
            txtBedCode._OnEnterMe += txtBedCode__OnEnterMe;
            grdBuong.SelectionChanged += grdBuong_SelectionChanged;
            grdBuong.KeyDown += grdBuong_KeyDown;
            grdGiuong.KeyDown += grdGiuong_KeyDown;
            grdGiuong.SelectionChanged += grdGiuong_SelectionChanged;
            grdGiuong.MouseDoubleClick += grdGiuong_MouseDoubleClick;
            ucThongtinnguoibenh1._OnEnterMe += ucThongtinnguoibenh1__OnEnterMe;
            cboGia.ValueChanged += cboGia_ValueChanged;
            dtpGio.ValueChanged += dtpGio_ValueChanged;
            dtpPhut.ValueChanged += dtpPhut_ValueChanged;
        }

        void dtpPhut_ValueChanged(object sender, EventArgs e)
        {
            if (!AllowValueChanged) return;
            dtNgayChuyen.Value = dtNgayChuyen.Value.Date.AddHours(dtpGio.Value.Hour).AddMinutes(dtpPhut.Value.Minute);
        }

        void dtpGio_ValueChanged(object sender, EventArgs e)
        {
            if (!AllowValueChanged) return;
            dtNgayChuyen.Value = dtNgayChuyen.Value.Date.AddHours(dtpGio.Value.Hour).AddMinutes(dtpPhut.Value.Minute);
        }

        void ucThongtinnguoibenh1__OnEnterMe()
        {
            if (ucThongtinnguoibenh1.objLuotkham != null)
            {
                this.objLuotkham = ucThongtinnguoibenh1.objLuotkham;
                ObjPhanbuonggiuong = null;
                ClearControl();
                BindData();
            }
        }
        bool m_blnAllowSelectionChanged = false;
        void txtGia__OnEnterMe()
        {
            //cboGia.Text = txtGia.Text;
            cboGia.SelectedIndex = Utility.GetSelectedIndex(cboGia, txtGia.MyID.ToString());
        }

        void cboGia_ValueChanged(object sender, EventArgs e)
        {
            if (!m_blnAllowSelectionChanged) return;
            txtGia.SetId(cboGia.Value);
        }

        private void grdGiuong_SelectionChanged(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdGiuong) || !m_blnAllowSelectionChanged) return;
            ChonGiuong();
        }

        private void ChonGiuong()
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

        private void grdGiuong_MouseDoubleClick(object sender, MouseEventArgs e)
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
            if (!Utility.isValidGrid(grdBuong) || !m_blnAllowSelectionChanged) return;
            ChonBuong();
        }

        private void ChonBuong()
        {
            string idBuong = Utility.sDbnull(grdBuong.GetValue(NoitruDmucBuong.Columns.IdBuong), -1);
            txtRoom_code.SetId(idBuong);
            m_dtGiuong = THU_VIEN_CHUNG.NoitruTimkiemgiuongTheobuong(Utility.Int32Dbnull(txtDepartment_ID.Text),
                Utility.Int32Dbnull(idBuong), 0);
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
            txtBedCode.SetId(ObjPhanbuonggiuong.IdGiuong);
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

            Utility.GotoNewRowJanus(grdGiuong, "id_giuong", Utility.sDbnull(ObjPhanbuonggiuong.IdGiuong, "-1"));

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

        private void ClearControl()
        {
            foreach (Control ctrl in grpThongTinBN.Controls)
            {
                if (ctrl is EditBox)
                {
                    ((EditBox) ctrl).Clear();
                }
            }
        }


        private void dtNgayChuyen_ValueChanged(object sender, EventArgs e)
        {
            if (!AllowValueChanged) return;
            dtpPhut.Text = dtpGio.Text = dtNgayChuyen.Text;
        }

        private void dtNgayChuyen_TextChanged(object sender, EventArgs e)
        {
            if (!AllowValueChanged) return;
           dtpPhut.Value= dtpGio.Value = dtNgayChuyen.Value;
            
        }

        /// <summary>
        ///     hàm thực hiện thoát form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdSave_Click(object sender, EventArgs e)
        {
            //if (!InValBenhNhan.ExistBN(objPhanbuonggiuong.MaLuotkham)) return;
            if (!IsValidData()) return;
            PerformAction();
        }

        private void PerformAction()
        {
            //if (Utility.AcceptQuestion("Bạn có muốn chuyển bệnh nhân vào Buồng và giường đang chọn không", "Thông báo",
            //    true))
            //{
            DateTime ngaychuyenkhoa = dtNgayChuyen.Value; //new DateTime(dtNgayChuyen.Value.Year, dtNgayChuyen.Value.Month, dtNgayChuyen.Value.Day, Utility.Int32Dbnull(txtGio.Text), Utility.Int32Dbnull(txtPhut.Text), 00);
            objLuotkham =
                new Select().From<KcbLuotkham>()
                    .Where(KcbLuotkham.Columns.MaLuotkham)
                    .IsEqualTo(ObjPhanbuonggiuong.MaLuotkham)
                    .And(KcbLuotkham.Columns.IdBenhnhan)
                    .IsEqualTo(ObjPhanbuonggiuong.IdBenhnhan)
                    .ExecuteSingle<KcbLuotkham>();
            if (objLuotkham != null)
            {
                Int16 id_giuongcu = Utility.Int16Dbnull(ObjPhanbuonggiuong.IdGiuong, -1);
                Int16 id_buongcu = Utility.Int16Dbnull(ObjPhanbuonggiuong.IdBuong, -1);
                objLuotkham.IdBsDieutrinoitruChinh = Utility.Int32Dbnull(txtBacsi.MyID, -1);
                ObjPhanbuonggiuong.Id = Utility.Int32Dbnull(txtPatientDept_ID.Text);
                ObjPhanbuonggiuong.NgayVaokhoa = ngaychuyenkhoa;
                ObjPhanbuonggiuong.IdBuong = Utility.Int16Dbnull(grdBuong.GetValue(NoitruDmucBuong.Columns.IdBuong));
                ObjPhanbuonggiuong.IdGiuong = Utility.Int16Dbnull(grdGiuong.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong));
                ObjPhanbuonggiuong.TinhChiphi = Utility.ByteDbnull(chkKhongtinh.Checked ? 0 : 1, 1);
                ObjPhanbuonggiuong.IsGhepGiuong = Utility.ByteDbnull(chkGhepgiuong.Checked);
                if(chkGhepgiuong.Checked)  ObjPhanbuonggiuong.SoLuongGhep = Utility.Int16Dbnull(txtsoluongghep.Text,0);
                ObjPhanbuonggiuong.IdGia = Utility.Int32Dbnull(txtGia.MyID, -1);
                ActionResult actionResult = new noitru_nhapvien().PhanGiuongDieuTri(ObjPhanbuonggiuong, objLuotkham, ngaychuyenkhoa, 
                    Utility.Int16Dbnull(grdBuong.GetValue(NoitruDmucBuong.Columns.IdBuong)),
                    Utility.Int16Dbnull(grdGiuong.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong)), id_giuongcu, id_buongcu);
                switch (actionResult)
                {
                    case ActionResult.Success:
                        Utility.Log(this.Name, globalVariables.UserName, string.Format("Phân buồng giường cho người bệnh ID bệnh nhân={0}, PID={1}, id buồng={2}, id giường={3} thành công ", objLuotkham.IdBenhnhan.ToString(), objLuotkham.MaLuotkham, ObjPhanbuonggiuong.IdBuong, ObjPhanbuonggiuong.IdGiuong), newaction.Add, this.GetType().Assembly.ManifestModule.Name);
                        txtPatientDept_ID.Text = Utility.sDbnull(ObjPhanbuonggiuong.Id);
                        Utility.SetMsg(lblMsg, "Bạn chuyển bệnh nhân vào giường thành công", true);
                        Utility.ShowMsg(lblMsg.Text);
                        ProcessChuyenKhoa();
                        BCancel = false;
                        Close();

                        break;
                    case ActionResult.Error:
                        Utility.ShowMsg("Lỗi trong quá trình phân buồng giường", "Thông báo", MessageBoxIcon.Error);
                        break;
                }
            }
            //  }
        }

        private bool IsValidData()
        {
            errorProvider1.Clear();
            KcbLuotkham objKcbLuotkham = Utility.getKcbLuotkham(ObjPhanbuonggiuong.IdBenhnhan,
                ObjPhanbuonggiuong.MaLuotkham);
            if (objKcbLuotkham != null && objKcbLuotkham.TrangthaiNoitru < 1)
            {
                Utility.ShowMsg("Bệnh nhân này chưa nhập viện nên không được phép phân buồng giường", "Thông báo",
                    MessageBoxIcon.Warning);
                
                return false;
            }

            if (dtNgayChuyen.Value<dtpNgaynhapvien.Value) 
            {
                Utility.ShowMsg("Ngày phân buồng giường phải lớn hơn hoặc bằng ngày nhập viện. Mời bạn kiểm tra lại", "Thông báo",
                    MessageBoxIcon.Warning);
                dtNgayChuyen.Focus();
                return false;
            }

            if (!Utility.isValidGrid(grdBuong))
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn Buồng cần chuyển", true);
                errorProvider1.SetError(txtRoom_code, lblMsg.Text);
                txtRoom_code.Focus();
                txtRoom_code.SelectAll();
                return false;
            }
            DataTable dtfull = SPs.NoitruBuongiuongKiemtrathuefullBuong(Utility.Int32Dbnull(grdBuong.GetValue("id_buong"))).GetDataSet().Tables[0];
            if (dtfull != null && dtfull.Rows.Count > 0)
            {
                //Kiểm tra nếu vẫn người bệnh đó thì là đang muốn đổi giường khác
                if (ObjPhanbuonggiuong.IdBuong != Utility.Int32Dbnull(grdBuong.GetValue("id_buong")))
                {
                    Utility.SetMsg(lblMsg, "Buồng bạn chọn đã được bệnh nhân khác thuê nguyên buồng. Vui lòng chọn buồng-giường khác", true);
                    errorProvider1.SetError(txtRoom_code, lblMsg.Text);
                    txtRoom_code.Focus();
                    txtRoom_code.SelectAll();
                    return false;
                }
                else//Chọn vẫn buồng đó và đổi giường thì ko cần cảnh báo gì
                {
                }
            }
            if (!Utility.isValidGrid(grdGiuong))
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn giường cần chuyển", true);
                errorProvider1.SetError(txtBedCode, lblMsg.Text);
                txtBedCode.Focus();
                txtBedCode.SelectAll();
                return false;
            }
            if (lblGiaBG.Visible && Utility.Int32Dbnull(txtGia.MyID, -1) == -1)
            {
                Utility.SetMsg(lblMsg, "Bạn phải chọn giá buồng giường", true);
                errorProvider1.SetError(txtGia, lblMsg.Text);
                txtGia.Focus();
                txtGia.SelectAll();
                return false;
            }
            DataTable dt = new noitru_nhapvien().NoitruKiemtraBuongGiuong(ObjPhanbuonggiuong.IdKhoanoitru,
                Utility.Int16Dbnull(grdBuong.GetValue(NoitruDmucGiuongbenh.Columns.IdBuong)),
                Utility.Int16Dbnull(grdGiuong.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong)),objLuotkham.IdBenhnhan,objLuotkham.MaLuotkham);

            if (dt != null && dt.Rows.Count > 0)
            {
                Utility.SetMsg(lblMsg,
                    string.Format("Giường này đang được nằm bởi bệnh nhân: {0}. Mời bạn chọn giường khác",
                        Utility.sDbnull(dt.Rows[0][KcbDanhsachBenhnhan.Columns.TenBenhnhan])), true);
                errorProvider1.SetError(txtBedCode, lblMsg.Text);
                txtBedCode.Focus();
                txtBedCode.SelectAll();
                return false;
            }
            if (chkKhongtinh.Checked)
            {
                if (!Utility.AcceptQuestion("Hệ thống phát hiện bạn chọn không tính phí giường nằm cho giường đang chuyển.\nBạn có chắc chắn không muốn tính chi phí cho giường đang chuyển hay không?", "Xác nhận", true))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        ///     hàm thực hiện việc xử lý thông tin chuyển khoa
        /// </summary>
        private void ProcessChuyenKhoa()
        {
            DataRow query = (from khoa in PDanhSachPhanBuongGiuong.AsEnumerable()
                where
                    Utility.Int32Dbnull(khoa[NoitruPhanbuonggiuong.Columns.Id]) ==
                    Utility.Int32Dbnull(Utility.Int32Dbnull(txtPatientDept_ID.Text))
                select khoa).FirstOrDefault();
            if (query != null)
            {
                NoitruDmucBuong objRoom =
                    NoitruDmucBuong.FetchByID(Utility.Int32Dbnull(grdBuong.GetValue(NoitruDmucBuong.Columns.IdBuong)));
                if (objRoom != null)
                {
                    query[NoitruDmucBuong.Columns.IdBuong] = Utility.Int32Dbnull(objRoom.IdBuong, -1);
                    query[NoitruDmucBuong.Columns.TenBuong] = Utility.sDbnull(objRoom.TenBuong);
                }
                NoitruDmucGiuongbenh objBed =
                    NoitruDmucGiuongbenh.FetchByID(
                        Utility.Int32Dbnull(grdGiuong.GetValue(NoitruDmucGiuongbenh.Columns.IdGiuong)));
                if (objBed != null)
                {
                    query[NoitruDmucGiuongbenh.Columns.IdGiuong] = Utility.Int32Dbnull(objBed.IdGiuong, -1);
                    query[NoitruDmucGiuongbenh.Columns.TenGiuong] = Utility.sDbnull(objBed.TenGiuong);
                }
                query[NoitruPhanbuonggiuong.Columns.Id] = Utility.sDbnull(txtPatientDept_ID.Text);
                query.AcceptChanges();
            }
        }

        private void BindData()
        {
             
                if (objLuotkham != null)
                {
                    dtpNgaynhapvien.Value = objLuotkham.NgayNhapvien.Value;
                    if (ObjPhanbuonggiuong == null)//99,99% ko xảy ra
                        ObjPhanbuonggiuong = NoitruPhanbuonggiuong.FetchByID(Utility.Int32Dbnull(objLuotkham.IdRavien, 0));
                    if (ObjPhanbuonggiuong != null)
                    {
                        dtNgayChuyen.Value = ObjPhanbuonggiuong.NgayPhangiuong.HasValue ? ObjPhanbuonggiuong.NgayPhangiuong.Value : ObjPhanbuonggiuong.NgayVaokhoa;
                        dtpGio.Value = dtNgayChuyen.Value;
                        dtpPhut.Value = dtNgayChuyen.Value;
                        txtPatientDept_ID.Text = Utility.sDbnull(ObjPhanbuonggiuong.Id);
                        txtsoluongghep.Text = Utility.sDbnull(ObjPhanbuonggiuong.SoLuongGhep);
                        chkGhepgiuong.Checked = Utility.Byte2Bool(ObjPhanbuonggiuong.IsGhepGiuong);
                        txtDepartment_ID.Text = ObjPhanbuonggiuong.IdKhoanoitru.ToString();
                    }
                        Int16 id_buong = Utility.Int16Dbnull(ObjPhanbuonggiuong.IdBuong, -1);
                        Int16 id_giuong = Utility.Int16Dbnull(ObjPhanbuonggiuong.IdGiuong, -1);
                    
                    m_dtDataRoom = THU_VIEN_CHUNG.NoitruTimkiembuongTheokhoa(Utility.Int32Dbnull(txtDepartment_ID.Text));
                    Utility.SetDataSourceForDataGridEx_Basic(grdBuong, m_dtDataRoom, true, true, "1=1",
                        "sluong_giuong_trong desc,ten_buong");
                    txtRoom_code.Init(m_dtDataRoom,
                        new List<string>
                        {
                            NoitruDmucBuong.Columns.IdBuong,
                            NoitruDmucBuong.Columns.MaBuong,
                            NoitruDmucBuong.Columns.TenBuong
                        });
                    txtRoom_code.SetId(id_buong);
                    txtBedCode.SetId(id_giuong);
                    txtBacsi.SetId(objLuotkham.IdBsDieutrinoitruChinh);
                    //txtGia.RaiseEnterEvents();
                    if (grdBuong.DataSource != null)
                    {
                        grdBuong.MoveFirst();
                    }
                }
                else
                {
                    ClearControl();
                    if (m_dtDataRoom != null) m_dtDataRoom.Clear();
                    if (m_dtGiuong != null) m_dtGiuong.Clear();
                   
                }
           
        }


        private void frm_phanbuonggiuong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.S && e.Control) cmdSave.PerformClick();
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.F2) txtRoom_code.Focus();
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.F2)
            {
              ucThongtinnguoibenh1.txtMaluotkham.Focus();
              ucThongtinnguoibenh1.txtMaluotkham.SelectAll();
            }
            
            if (e.KeyCode == Keys.F5)
            {
                grdBuong_SelectionChanged(grdBuong, new EventArgs());
            }
        }

       
        DataTable dtBacsinoitru = new DataTable();
        private void frm_phanbuonggiuong_Load(object sender, EventArgs e)
        {
            try
            {
                m_blnAllowSelectionChanged = false;
                if (ObjPhanbuonggiuong != null)
                {
                   ucThongtinnguoibenh1.txtMaluotkham.Text= ObjPhanbuonggiuong.MaLuotkham;
                   ucThongtinnguoibenh1.Refresh_V1(false);
                   objLuotkham = ucThongtinnguoibenh1.objLuotkham;
                    txtPatientDept_ID.Text = Utility.sDbnull(ObjPhanbuonggiuong.Id);
                    txtDepartment_ID.Text = Utility.sDbnull(ObjPhanbuonggiuong.IdKhoanoitru);
                    DmucKhoaphong objDepartment = DmucKhoaphong.FetchByID(Utility.Int32Dbnull(txtDepartment_ID.Text));
                    if (objDepartment != null)
                    {
                        txtDepartmentName.Text = Utility.sDbnull(objDepartment.TenKhoaphong);
                        txtDepartmentName.Tag = Utility.sDbnull(objDepartment.IdKhoaphong);
                    }
                    dtNgayChuyen.Value = globalVariables.SysDate;
                    //dtpGio.Text = Utility.sDbnull(dtNgayChuyen.Value.Hour);
                    //dtpPhut.Text = Utility.sDbnull(dtNgayChuyen.Value.Minute);

                    dtBacsinoitru = THU_VIEN_CHUNG.LaydanhsachBacsi(-1, 1);
                    txtBacsi.Init(dtBacsinoitru, new List<string>() { DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.MaNhanvien, DmucNhanvien.Columns.TenNhanvien });
                    txtBacsi._OnSelectionChanged += txtBacsi__OnSelectionChanged;
                    txtBacsi._OnEnterMe += txtBacsi__OnEnterMe;

                    BindData();
                    //Đặt code đoạn load giá ở đây để tránh việc các sự kiện trên lưới buồng, giường thay đổi load sai giá giường đã chọn
                    LoadGiaGiuong();
                    txtRoom_code.Focus();
                    txtRoom_code.SelectAll();
                }
            }
            catch (Exception)
            {

            }
            finally
            {
                AllowValueChanged = true;
                m_blnAllowSelectionChanged = true;
                Utility.GotoNewRowJanus(grdBuong, "id_buong",Utility.sDbnull( ObjPhanbuonggiuong.IdBuong,"-1"));
                grdBuong_SelectionChanged(grdBuong, e);
            }
            
        }
        void LoadGiaGiuong()
        {
            DataTable dtGia = new dmucgiagiuong_busrule().dsGetLoaigiuong(Utility.Int16Dbnull( txtBedCode.MyID,-1)).Tables[0];
            dtGia.DefaultView.Sort = NoitruGiabuonggiuong.Columns.SttHthi + "," +
                                     NoitruGiabuonggiuong.Columns.TenGia;
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
            txtGia.SetId(ObjPhanbuonggiuong.IdGia);
            txtGia.RaiseEnterEvents();
            lblGiaBG.Text = string.Format("Chọn giá ({0}):", dtGia.Rows.Count.ToString());
            if (txtGia.MyID == "-1" && dtGia.Rows.Count > 1)
                cboGia.Text = "Mời bạn chọn giá buồng giường";
            if (dtGia.Rows.Count == 1)
                if (Utility.sDbnull(ObjPhanbuonggiuong.IdGia, "-1") != "-1" && Utility.sDbnull( ObjPhanbuonggiuong.IdGiuong,"-1") == txtBedCode.MyID)
                    cboGia.SelectedIndex = Utility.GetSelectedIndex(cboGia, Utility.sDbnull(ObjPhanbuonggiuong.IdGia, "-1"));
                else
                    cboGia.SelectedIndex = 0;
        }
        void txtBacsi__OnEnterMe()
        {
        }

        void txtBacsi__OnSelectionChanged()
        {
        }
        private void chkGhepgiuong_CheckedChanged(object sender, EventArgs e)
        {
            if (chkGhepgiuong.Checked)
            {
                lblsoluongghep.Visible = true;
                txtsoluongghep.Visible = true;
            }
            else
            {
                lblsoluongghep.Visible = false;
                txtsoluongghep.Visible = false;
            }
        }

        private void cmdSave_Click_1(object sender, EventArgs e)
        {

        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            frm_chonbacsidieutri _chonbacsidieutri = new frm_chonbacsidieutri(ObjPhanbuonggiuong.IdKhoanoitru,objLuotkham);
            _chonbacsidieutri._OnAccept += _chonbacsidieutri__OnAccept;
            _chonbacsidieutri.ShowDialog();
        }

        void _chonbacsidieutri__OnAccept(object ID)
        {
            txtBacsi.SetId(ID);
        }

        private void cmdSave_Click_2(object sender, EventArgs e)
        {

        }
    }
}