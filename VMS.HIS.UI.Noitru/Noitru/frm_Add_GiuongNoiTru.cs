using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using VNS.Libs;
using VMS.HIS.DAL;
using SubSonic;
using VNS.HIS.UI.DANHMUC;
using System.Transactions;

namespace VNS.HIS.UI.NOITRU
{
    public partial class frm_Add_GiuongNoiTru : Form
    {
        private string sKieuDanhMuc = "LYDOTANG";
        public NoitruDmucGiuongbenh objBed;

        public delegate void timkiem();

        public timkiem MyGetData;
        #region "khai báo biến"
        public DataTable p_dtDataGiuong = new DataTable();
        public action m_enAct = action.Insert;
        public Janus.Windows.GridEX.GridEX grdList;
        public string Loai = ""; 


        #endregion
        public frm_Add_GiuongNoiTru()
        {
            InitializeComponent();
            txtGiaBHYT.TabStop = txtGiakhac.TabStop = !chkChungGia.Checked;
            InitEvents();
            THU_VIEN_CHUNG.EnableBHYT(this);
        }
        void InitEvents()
        {
            this.chkcodefrom.CheckedChanged += new System.EventHandler(this.chkcodefrom_CheckedChanged);
            this.cmdSaoChep.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.cmdSaoChep_LinkClicked);
            txtBuong._OnSelectionChanged += txtBuong__OnSelectionChanged;
            this.cboKhoaNoiTru.SelectedIndexChanged += new System.EventHandler(this.cboKhoaNoiTru_SelectedIndexChanged);
            this.txtTEN.TextChanged += new System.EventHandler(this.txtTEN_TextChanged);
            this.txtMa.TextChanged += new System.EventHandler(this.txtMa_TextChanged);
            this.Load += new System.EventHandler(this.frm_Add_DCHUNG_Load);
            this.cmdThemMoi.Click += new System.EventHandler(this.cmdThemMoi_Click);
            this.KeyDown += new KeyEventHandler(frm_Add_DCHUNG_KeyDown);
            cmdSave.Click += new EventHandler(cmdSave_Click);
            cmdExit.Click += new EventHandler(cmdExit_Click);
            chkChungGia.CheckedChanged += chkChungGia_CheckedChanged;
            txtDongia.TextChanged += txtDongia_TextChanged;
            txtDonvitinh._OnShowData += txtDonvitinh__OnShowData;
            chkApDungGia.CheckedChanged += chkApDungGia_CheckedChanged;
        }

        void chkApDungGia_CheckedChanged(object sender, EventArgs e)
        {
            tabpage_GiaDoituongKCB.TabVisible = chkApDungGia.Checked;
        }

        void txtDonvitinh__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtDonvitinh.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtDonvitinh.myCode;
                txtDonvitinh.Init();
                txtDonvitinh.SetCode(oldCode);
                txtDonvitinh.Focus();
            } 
        }

        void txtBuong__OnSelectionChanged()
        {
            SinhSTT();
        }

        void txtDongia_TextChanged(object sender, EventArgs e)
        {
            if (chkChungGia.Checked)
                txtGiaBHYT.Text = txtGiakhac.Text = txtDongia.Text;
        }
        void chkChungGia_CheckedChanged(object sender, EventArgs e)
        {
            txtGiaBHYT.TabStop = txtGiakhac.TabStop = !chkChungGia.Checked;
            if (chkChungGia.Checked) txtGiaBHYT.Text = txtGiakhac.Text = txtDongia.Text;
        }
       //ry>
        /// hàm thực hiện việc phím tắt của đơn vị tính
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_Add_DCHUNG_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S) cmdSave.PerformClick();
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.F5) NhapLienTuc();
        }
          /// <summary>
        /// hàm thực hiện việc load thông tin của form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_Add_DCHUNG_Load(object sender, EventArgs e)
          {
             InitData();
              
            if(m_enAct==action.Insert)
            {
                SinhSTT();
            }
            
            Getdata();
            txtMa.Focus();
            txtMa.SelectAll();
            bHasloaded = true;
        }
        //private DataTable m_dtKieuDungChung=new DataTable();
        private DataTable m_dtQuanHe=new DataTable();
        private DataTable m_dtKhoaNoiTru=new DataTable();
        private void InitData()
        {

            m_dtKhoaNoiTru =
                THU_VIEN_CHUNG.Laydanhmuckhoa("NOI", 0);
            DataBinding.BindDataCombobox(cboKhoaNoiTru, m_dtKhoaNoiTru, DmucKhoaphong.Columns.IdKhoaphong,
                                       DmucKhoaphong.Columns.TenKhoaphong, "", true);
            txtDonvitinh.Init();
            DataTable dtBuong = SPs.NoitruTimkiembuongTheokhoa(-1).GetDataSet().Tables[0];
            txtBuong.Init(dtBuong, new List<string>() { NoitruDmucBuong.Columns.IdBuong, NoitruDmucBuong.Columns.MaBuong, NoitruDmucBuong.Columns.TenBuong });
            DataTable dtLoaiGiuong = SPs.DmucLaydanhmucGiabuonggiuong("-1").GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdGiaGiuong, dtLoaiGiuong, true, true, "1=1", NoitruGiabuonggiuong.Columns.SttHthi + "," + NoitruGiabuonggiuong.Columns.TenGia);
            chkApDungGia.Visible = THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_HIENTHI_APGIAGIUONG_DOITUONGKCB", "0", true) == "1";
        }

       
        /// <summary>
        /// hàm thưucj hiện lấy thông tin của dữ liệu
        /// </summary>
        private void Getdata()
        {
            // objRoom = NoitruDmucBuong.FetchByID(Utility.Int32Dbnull(txtID.Text, -1));
            //if (objRoom != null)
            //{
            //    txt_Mo_Ta.Text = Utility.sDbnull(objRoom.MotaThem, "");
            //    txt_STT_HTHI.Text = Utility.sDbnull(objRoom.SttHthi, 1);
            //    txtTEN.Text = Utility.sDbnull(objRoom.Ten, "")b;
            //}
            if (m_enAct == action.Update)
            {
                chkcodefrom.Enabled = false;
                chkAutoupdate.Visible=lblSample.Visible = lblSuffix.Visible = false;
                
                if (objBed != null)
                {
                    txtID.Text = Utility.sDbnull(objBed.IdGiuong);
                    txtMa.Text = objBed.MaGiuong;
                    txtMaBHYT.Text = objBed.MaBhyt;
                    txtTen_BHYT.Text = Utility.sDbnull(objBed.TenBhyt);
                    txt_Mo_Ta.Text = Utility.sDbnull(objBed.MotaThem);
                    txt_STT_HTHI.Text = Utility.sDbnull(objBed.SttHthi);
                    txtTEN.Text = Utility.sDbnull(objBed.TenGiuong);
                    txtSuChua.Text = Utility.sDbnull(objBed.SonguoiToida);
                    txtDongia.Text = Utility.sDbnull(objBed.GiaDichvu);
                    txtGiaBHYT.Text = Utility.sDbnull(objBed.GiaBhyt);
                    txtGiakhac.Text = Utility.sDbnull(objBed.GiaKhac);
                    txtPTDT.Text = Utility.sDbnull(objBed.PhuthuDungtuyen);
                    txtPTTT.Text = Utility.sDbnull(objBed.PhuthuTraituyen);
                    chkFull.Checked = Utility.Bool2Bool(objBed.ThueNguyenbuong);
                    chktutuc.Checked = Utility.Byte2Bool(objBed.TthaiTunguyen);
                    chkTrangThai.Checked = Utility.Int32Dbnull(objBed.TrangThai) == 1;
                    if (Utility.Int32Dbnull(objBed.IdKhoanoitru) > 0)
                        cboKhoaNoiTru.SelectedIndex = Utility.GetSelectedIndex(cboKhoaNoiTru, objBed.IdKhoanoitru.ToString());
                    txtBuong.SetId(Utility.Int32Dbnull(objBed.IdBuong,0));
                    txtDonvitinh.SetCode(objBed.MaDonvitinh);
                    LoadQuangeGiuong_Loaigiuong();
                }
            }

            m_dtQuanHe = SPs.NoitruLaydulieuqheDoituongBuonggiuong(Utility.Int32Dbnull(txtID.Text)).GetDataSet().Tables[0];
            Utility.SetDataSourceForDataGridEx(grdQuanheGiuong, m_dtQuanHe, false, true, "1=1", "");
            NoitruQheDoituongBuonggiuongCollection roomBedObjectTypeCollection = new Select().From(NoitruQheDoituongBuonggiuong.Schema)
                .Where(NoitruQheDoituongBuonggiuong.Columns.IdGiuong).IsEqualTo(Utility.Int32Dbnull(objBed.IdGiuong)).ExecuteAsCollection
                <NoitruQheDoituongBuonggiuongCollection>();
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdQuanheGiuong.GetDataRows())
            {
                var query = from giuong in roomBedObjectTypeCollection.AsEnumerable()
                            where
                                Utility.sDbnull(giuong.MaDoituongKcb) ==
                                Utility.sDbnull(gridExRow.Cells[NoitruQheDoituongBuonggiuong.Columns.MaDoituongKcb].Value)
                            select giuong;
                if (query.Any())
                {
                    gridExRow.IsChecked = true;
                }
                else
                {
                    gridExRow.IsChecked = false;
                }
            }
            if (roomBedObjectTypeCollection.Count() <= 0) grdQuanheGiuong.GetCheckedRows();
            chkApDungGia.Checked = roomBedObjectTypeCollection.Count() > 0;
            LoadQuangeGiuong_Loaigiuong();
            //  Utility.TryToSetBindData(dtNgayLapPhieu, "Text", objTreatment,
        }
        /// <summary>
        /// hàm thực hiện việc thoát Form hiện tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// hàm thực hiện bắt sự kiện của form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
          
            if(!IsValidData())return;
            PerformAction();
        }
        private bool IsValidData()
        {
            Utility.SetMsg(lblMsg, "", true);
            if (string.IsNullOrEmpty(txtMa.Text))
            {
                Utility.SetMsg(lblMsg, "Bạn phải nhập thông tin mã giường ", true);
                txtMa.Focus();
                return false;
            }
            if (chkcodefrom.Checked)
            {
                if (Utility.DoTrim(txtCodefrom.Text) == "")
                {
                    Utility.SetMsg(lblMsg, "Bạn phải nhập mã bắt đầu", true);
                    txtCodefrom.Focus();
                    txtCodefrom.SelectAll();
                    return false;
                }
                if (Utility.DecimaltoDbnull(txtCodefrom.Text, 0) <= 0)
                {
                    Utility.SetMsg(lblMsg, "Hậu tố mã phòng bắt đầu phải >0", true);
                    txtCodefrom.Focus();
                    txtCodefrom.SelectAll();
                    return false;
                }
                if (Utility.DoTrim(txtCode2.Text) == "")
                {
                    Utility.SetMsg(lblMsg, "Bạn phải nhập mã kết thúc", true);
                    txtCode2.Focus();
                    txtCode2.SelectAll();
                    return false;
                }
                if (Utility.DecimaltoDbnull(txtCodefrom.Text, 0) > Utility.DecimaltoDbnull(txtCode2.Text, 0))
                {
                    Utility.SetMsg(lblMsg, "Hậu tố mã phòng bắt đầu < mã phòng đến", true);
                    txtCode2.Focus();
                    txtCode2.SelectAll();
                    return false;
                }
            }
            if (string.IsNullOrEmpty(txtTEN.Text))
            {
                 Utility.SetMsg(lblMsg,"Bạn phải nhập thông tin tên giường ", true);
                txtTEN.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(txtSuChua.Text) <= 0)
            {
                Utility.SetMsg(lblMsg, "Sức chứa không được <=0 ", true);
                txtSuChua.Focus();
                return false;
            }
            DmucKhoaphong objDmucKhoaphong = DmucKhoaphong.FetchByID(Utility.Int32Dbnull(cboKhoaNoiTru.SelectedValue));
            if (objDmucKhoaphong == null)
            {
                Utility.SetMsg(lblMsg, "Bạn chọn khoa nội trú ", true);
                cboKhoaNoiTru.Focus();
                return false;
            }
            if (Utility.Int32Dbnull(txtBuong.MyID)<=0)
            {
                Utility.SetMsg(lblMsg, "Bạn cần chọn phòng nội trú(Có thể nhấn phím cách để hiện thị các buồng thuộc phòng nội trú đang chọn)", true);
                txtBuong.Focus();
                return false;
            }
            
            if (Utility.DecimaltoDbnull(txtDongia.Text, 0) < 0)
            {
                Utility.SetMsg(lblMsg, "Giá áp dụng cho đối tượng Dịch vụ phải >=0.", true);
                txtDongia.Focus();
                return false;
            }
            if (Utility.DecimaltoDbnull(txtGiaBHYT.Text, 0) < 0)
            {
                Utility.SetMsg(lblMsg, "Giá áp dụng cho đối tượng BHYT phải >=0.", true);
                txtGiaBHYT.Focus();
                return false;
            }
            if (Utility.DecimaltoDbnull(txtGiakhac.Text, 0) < 0)
            {
                Utility.SetMsg(lblMsg, "Giá áp dụng cho đối tượng khác phải >=0.", true);
                txtGiakhac.Focus();
                return false;
            }
            if (Utility.DecimaltoDbnull(txtPTDT.Text, 0) < 0)
            {
                Utility.SetMsg(lblMsg, "Giá phụ thu đúng tuyến áp dụng cho đối tượng BHYT phải >=0.", true);
                txtPTDT.Focus();
                return false;
            }
            if (Utility.DecimaltoDbnull(txtPTTT.Text, 0) < 0)
            {
                Utility.SetMsg(lblMsg, "Giá phụ thu trái tuyến áp dụng cho đối tượng BHYT phải >=0.", true);
                txtPTTT.Focus();
                return false;
            }
            if (m_enAct == action.Insert)
            {
                if (!chkcodefrom.Checked)//Thêm đơn lẻ từng chiếc mới kiểm tra
                {
                    SqlQuery sqlQuery = new Select().From(NoitruDmucGiuongbenh.Schema)
                   .Where(NoitruDmucGiuongbenh.Columns.IdBuong).IsEqualTo(Utility.sDbnull(txtBuong.MyID, ""));
                    sqlQuery.And(NoitruDmucGiuongbenh.Columns.MaGiuong).IsEqualTo(Utility.sDbnull(txtMa.Text));
                    if (sqlQuery.GetRecordCount() > 0)
                    {
                        Utility.ShowMsg("Mã giường đã tồn tại. Đề nghị bạn nhập mã khác", "Thông báo", MessageBoxIcon.Warning);
                        txtMa.Focus();
                        return false;
                    }
                    sqlQuery = new Select().From(NoitruDmucGiuongbenh.Schema)
                   .Where(NoitruDmucGiuongbenh.Columns.IdBuong).IsEqualTo(Utility.sDbnull(txtBuong.MyID, ""));
                    sqlQuery.And(NoitruDmucGiuongbenh.Columns.TenGiuong).IsEqualTo(Utility.sDbnull(txtTEN.Text));
                    if (sqlQuery.GetRecordCount() > 0)
                    {
                        Utility.ShowMsg("Tên giường đã tồn tại. Đề nghị bạn nhập tên khác", "Thông báo", MessageBoxIcon.Warning);
                        txtTEN.Focus();
                        return false;
                    }
                }

            }
            else
            {
                SqlQuery sqlQuery = new Select().From(NoitruDmucGiuongbenh.Schema)
              .Where(NoitruDmucGiuongbenh.Columns.IdBuong).IsEqualTo(Utility.sDbnull(txtBuong.MyID, ""))
                .And(NoitruDmucGiuongbenh.Columns.MaGiuong).IsEqualTo(Utility.sDbnull(txtMa.Text))
                    .And(NoitruDmucGiuongbenh.Columns.IdGiuong).IsNotEqualTo(Utility.Int32Dbnull(txtID.Text));
                if (sqlQuery.GetRecordCount() > 0)
                {
                    Utility.ShowMsg("Mã giường đã tồn tại. Đề nghị bạn nhập mã khác", "Thông báo", MessageBoxIcon.Warning);
                    txtMa.Focus();
                    return false;
                }
                sqlQuery = new Select().From(NoitruDmucGiuongbenh.Schema)
                  .Where(NoitruDmucGiuongbenh.Columns.IdBuong).IsEqualTo(Utility.sDbnull(txtBuong.MyID, ""))
                .And(NoitruDmucGiuongbenh.Columns.TenGiuong).IsEqualTo(Utility.sDbnull(txtTEN.Text))
                    .And(NoitruDmucGiuongbenh.Columns.IdGiuong).IsNotEqualTo(Utility.Int32Dbnull(txtID.Text));
                if (sqlQuery.GetRecordCount() > 0)
                {
                    Utility.ShowMsg("Tên giường đã tồn tại. Đề nghị bạn nhập tên khác", "Thông báo", MessageBoxIcon.Warning);
                    txtTEN.Focus();
                    return false;
                }
            }

            return true;
        }

        public bool b_Cancel = false;
        private void PerformAction()
        {
            switch (m_enAct)
            {
                case action.Insert:
                    if (chkcodefrom.Checked)
                        MultiInsertData();
                    else
                        InsertData();
                    break;
                case action.Update:
                    UpdateData();
                    break;
            }
            b_Cancel = true;

        }
        private NoitruQheGiuongGiumCollection GetQuanheGiuongLoaiGiuong(short id_giuong)
        {
            NoitruQheGiuongGiumCollection lst = new NoitruQheGiuongGiumCollection();

            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdGiaGiuong.GetCheckedRows())
            {
                NoitruQheGiuongGium objQhe = new NoitruQheGiuongGium();
                objQhe.IdGia = Utility.Int16Dbnull(gridExRow.Cells[NoitruGiabuonggiuong.Columns.IdGia].Value);
                objQhe.IdGiuong = id_giuong;
                objQhe.IsNew = true;
                lst.Add(objQhe);
            }
            return lst;
        }
        private void LoadQuangeGiuong_Loaigiuong()
        {
            NoitruQheGiuongGiumCollection lstQhe = new Select().From(NoitruQheGiuongGium.Schema)
                .Where(NoitruQheGiuongGium.Columns.IdGiuong).IsEqualTo(Utility.Int32Dbnull(objBed.IdGiuong)).
                ExecuteAsCollection<NoitruQheGiuongGiumCollection>();
            foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdGiaGiuong.GetDataRows())
            {
                gridExRow.BeginEdit();
                var query = from kho in lstQhe.AsEnumerable()
                            where kho.IdGia == Utility.Int32Dbnull(gridExRow.Cells[NoitruGiabuonggiuong.Columns.IdGia].Value)
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

        private Query _Query = NoitruDmucGiuongbenh.CreateQuery();

        private NoitruDmucGiuongbenh createBed()
        {
            NoitruDmucGiuongbenh objBed=new NoitruDmucGiuongbenh();
            objBed.IsNew = true;
            if (m_enAct == action.Update)
            {
                objBed = NoitruDmucGiuongbenh.FetchByID(Utility.Int16Dbnull(txtID.Text));
                objBed.MarkOld();
                objBed.IsNew = false;
            }
            objBed.IdKhoanoitru = Utility.Int16Dbnull(cboKhoaNoiTru.SelectedValue);
            objBed.IdBuong = Utility.Int16Dbnull(txtBuong.MyID);
            objBed.MotaThem = Utility.sDbnull(txt_Mo_Ta.Text);
            objBed.SonguoiToida = Utility.Int16Dbnull(txtSuChua.Value);
            objBed.SttHthi = Utility.Int16Dbnull(txt_STT_HTHI.Value);
            objBed.MaGiuong = Utility.sDbnull(txtMa.Text);
            objBed.MaBhyt = Utility.sDbnull(txtMaBHYT.Text);
            objBed.TenGiuong = Utility.sDbnull(txtTEN.Text);
            objBed.TenBhyt = Utility.sDbnull(txtTen_BHYT.Text);
            objBed.MaDonvitinh = txtDonvitinh.myCode;
            objBed.TthaiTunguyen = Utility.Bool2byte(chktutuc.Checked);
            objBed.GiaDichvu = Utility.DecimaltoDbnull( txtDongia.Text,0);
            objBed.GiaBhyt = Utility.DecimaltoDbnull(txtGiaBHYT.Text, 0);
            objBed.GiaKhac = Utility.DecimaltoDbnull(txtGiakhac.Text, 0);
            objBed.PhuthuDungtuyen = Utility.DecimaltoDbnull(txtPTDT.Text, 0);
            objBed.PhuthuTraituyen = Utility.DecimaltoDbnull(txtPTTT.Text, 0);
            objBed.TrangThai = (byte?) (chkTrangThai.Checked ? 1 : 0);
            objBed.ThueNguyenbuong = chkFull.Checked;
            return objBed;
        }
        private void MultiInsertData()
        {
            try
            {
                NoitruQheGiuongGiumCollection lstQhe = GetQuanheGiuongLoaiGiuong(-1);
                 using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        Int16 STTHthi = Utility.Int16Dbnull(txt_STT_HTHI.Text);
                        for (int i = Utility.Int32Dbnull(Utility.DecimaltoDbnull(txtCodefrom.Text), 0); i <= Utility.Int32Dbnull(Utility.DecimaltoDbnull(txtCode2.Text), 0); i++)
                        {
                            NoitruDmucGiuongbenh objBed = new NoitruDmucGiuongbenh();
                            objBed.IdKhoanoitru = Utility.Int16Dbnull(cboKhoaNoiTru.SelectedValue);
                            objBed.IdBuong = Utility.Int16Dbnull(txtBuong.MyID);
                            objBed.MotaThem = Utility.sDbnull(txt_Mo_Ta.Text);
                            objBed.SonguoiToida = Utility.Int16Dbnull(txtSuChua.Value);
                            objBed.SttHthi = Utility.Int16Dbnull(txt_STT_HTHI.Value);
                            objBed.MaGiuong = Utility.sDbnull(txtMa.Text) + i.ToString();
                            objBed.MaBhyt = Utility.sDbnull(txtMaBHYT.Text) + i.ToString();
                            objBed.TenGiuong = Utility.sDbnull(txtTEN.Text) + " " + i.ToString();
                            objBed.TenBhyt = Utility.sDbnull(txtTEN.Text) + " " + i.ToString();
                            objBed.MaDonvitinh = txtDonvitinh.myCode;
                            objBed.TthaiTunguyen = Utility.Bool2byte(chktutuc.Checked);
                            objBed.GiaDichvu = Utility.DecimaltoDbnull(txtDongia.Text, 0);
                            objBed.GiaBhyt = Utility.DecimaltoDbnull(txtGiaBHYT.Text, 0);
                            objBed.GiaKhac = Utility.DecimaltoDbnull(txtGiakhac.Text, 0);
                            objBed.PhuthuDungtuyen = Utility.DecimaltoDbnull(txtPTDT.Text, 0);
                            objBed.PhuthuTraituyen = Utility.DecimaltoDbnull(txtPTTT.Text, 0);
                            objBed.TrangThai = (byte?)(chkTrangThai.Checked ? 1 : 0);
                            NoitruDmucGiuongbenh objcheck = new Select().From(NoitruDmucGiuongbenh.Schema)
                                .Where(NoitruDmucGiuongbenh.Columns.MaGiuong).IsEqualTo(objBed.MaGiuong)
                                .And(NoitruDmucGiuongbenh.Columns.IdKhoanoitru).IsEqualTo(objBed.IdKhoanoitru)
                                .And(NoitruDmucGiuongbenh.Columns.IdBuong).IsEqualTo(objBed.IdBuong)
                                .ExecuteSingle<NoitruDmucGiuongbenh>();
                            if (objcheck != null)
                            {
                                if (chkAutoupdate.Checked)
                                {
                                    objBed.IdGiuong = objcheck.IdGiuong;
                                    objBed.MarkOld();
                                    objBed.IsNew = false;
                                }
                                else
                                {
                                    continue;
                                }
                            }
                            else
                            {
                                STTHthi += 1;
                                objBed.IsNew = true;
                            }
                            objBed.SttHthi = STTHthi;
                            objBed.Save();
                            new Delete().From(NoitruQheGiuongGium.Schema).Where(NoitruQheGiuongGium.Columns.IdGiuong).IsEqualTo(objBed.IdGiuong).Execute();

                            foreach (NoitruQheGiuongGium obj in lstQhe)
                            {
                                obj.IdGiuong = objBed.IdGiuong;
                            }
                            lstQhe.SaveAll();
                            if (objBed != null)
                            {


                                if (chkApDungGia.Checked)
                                {
                                    new Delete().From(NoitruQheDoituongBuonggiuong.Schema)
                             .Where(NoitruQheDoituongBuonggiuong.Columns.IdGiuong).IsEqualTo(Utility.Int32Dbnull(txtID.Text)).Execute();
                                    foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdQuanheGiuong.GetCheckedRows())
                                    {
                                        NoitruQheDoituongBuonggiuong objectType = new NoitruQheDoituongBuonggiuong();
                                        objectType.DonGia = (objectType.MaDoituongKcb == "DV" ?
                                            Utility.DecimaltoDbnull(gridExRow.Cells[NoitruQheDoituongBuonggiuong.Columns.DonGia].Value, objBed.GiaDichvu)
                                            : (objectType.MaDoituongKcb == "BHYT" ?
                                            Utility.DecimaltoDbnull(gridExRow.Cells[NoitruQheDoituongBuonggiuong.Columns.DonGia].Value, objBed.GiaBhyt) :
                                            Utility.DecimaltoDbnull(gridExRow.Cells[NoitruQheDoituongBuonggiuong.Columns.DonGia].Value, objBed.GiaKhac))
                                            )
                                            ;
                                        objectType.PhuthuDungtuyen = Utility.DecimaltoDbnull(gridExRow.Cells[NoitruQheDoituongBuonggiuong.Columns.PhuthuDungtuyen].Value, 0);
                                        objectType.PhuthuTraituyen = Utility.DecimaltoDbnull(gridExRow.Cells[NoitruQheDoituongBuonggiuong.Columns.PhuthuTraituyen].Value, 0);
                                        objectType.MaDoituongKcb = Utility.sDbnull(gridExRow.Cells[NoitruQheDoituongBuonggiuong.Columns.MaDoituongKcb].Value);
                                        // DmucDoituongkcb objectType1=DmucDoituongkcb.FetchByID()
                                        SqlQuery sqlQuery =
                                           new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.Columns.MaDoituongKcb).IsEqualTo(
                                               objectType.MaDoituongKcb);
                                        DmucDoituongkcb objectType1 = sqlQuery.ExecuteSingle<DmucDoituongkcb>();
                                        if (objectType1 != null)
                                        {
                                            objectType.IdLoaidoituongKcb = Utility.Int32Dbnull(objectType1.IdLoaidoituongKcb);
                                        }
                                        objectType.KieuThue = "GIUONG";
                                        objectType.IdBuong = Utility.Int16Dbnull(txtBuong.MyID);
                                        objectType.IdGiuong = Utility.Int16Dbnull(objBed.IdGiuong, -1);

                                        objectType.IsNew = true;
                                        objectType.Save();
                                    }
                                }

                            }
                        }
                        scope.Complete();
                    }//end of SharedDbConnectionScope
                }//end of TransactionScope
                 if (MyGetData != null)
                 {
                     MyGetData();
                     Utility.GonewRowJanus(grdList, NoitruDmucGiuongbenh.Columns.IdGiuong, Utility.sDbnull(txtID.Text));
                 }
                Utility.SetMsg(lblMsg, "Bạn thực hiện thêm mới thành công", true);
                if (chkthemmoilientuc.Checked) ClearControl();
                else
                    Close();

            }
            catch (Exception)
            {

               
            }

        }

        /// <summary>
        /// hàm thực hiện viẹc thêmm ới thông tin 
        /// </summary>
        private void InsertData()
        {
            try
            {

                NoitruDmucGiuongbenh objBed = createBed();
                NoitruDmucGiuongbenh objcheck = new Select().From(NoitruDmucGiuongbenh.Schema)
                         .Where(NoitruDmucGiuongbenh.Columns.MaGiuong).IsEqualTo(objBed.MaGiuong)
                         .And(NoitruDmucGiuongbenh.Columns.IdKhoanoitru).IsEqualTo(objBed.IdKhoanoitru)
                         .And(NoitruDmucGiuongbenh.Columns.IdBuong).IsEqualTo(objBed.IdBuong)
                         .ExecuteSingle<NoitruDmucGiuongbenh>();
                if (objcheck != null)
                {
                    if (chkAutoupdate.Checked)
                    {
                        objBed.IdGiuong = objcheck.IdGiuong;
                        objBed.MarkOld();
                        objBed.IsNew = false;
                    }
                }
                else
                {
                    objBed.IsNew = true;
                }
                NoitruQheGiuongGiumCollection lstQhe = GetQuanheGiuongLoaiGiuong(-1);
                using (var scope = new TransactionScope())
                {
                    using (var sh = new SharedDbConnectionScope())
                    {
                        objBed.Save();
                        new Delete().From(NoitruQheGiuongGium.Schema).Where(NoitruQheGiuongGium.Columns.IdGiuong).IsEqualTo(objBed.IdGiuong).Execute();

                        foreach (NoitruQheGiuongGium obj in lstQhe)
                        {
                            obj.IdGiuong = objBed.IdGiuong;
                        }
                        lstQhe.SaveAll();
                    }
                    scope.Complete();
                }
                if (objBed != null)
                {
                    if (MyGetData != null)
                    {
                        MyGetData();
                        Utility.GonewRowJanus(grdList, NoitruDmucGiuongbenh.Columns.IdGiuong, Utility.sDbnull(txtID.Text));
                    }

                    #region tạm thời ko dùng
                    if (chkApDungGia.Checked)
                    {
                        new Delete().From(NoitruQheDoituongBuonggiuong.Schema)
                 .Where(NoitruQheDoituongBuonggiuong.Columns.IdGiuong).IsEqualTo(Utility.Int32Dbnull(txtID.Text)).Execute();
                        foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdQuanheGiuong.GetCheckedRows())
                        {
                            NoitruQheDoituongBuonggiuong objectType = new NoitruQheDoituongBuonggiuong();
                            objectType.DonGia = (objectType.MaDoituongKcb == "DV" ?
                                     Utility.DecimaltoDbnull(gridExRow.Cells[NoitruQheDoituongBuonggiuong.Columns.DonGia].Value, objBed.GiaDichvu)
                                     : (objectType.MaDoituongKcb == "BHYT" ?
                                     Utility.DecimaltoDbnull(gridExRow.Cells[NoitruQheDoituongBuonggiuong.Columns.DonGia].Value, objBed.GiaBhyt) :
                                     Utility.DecimaltoDbnull(gridExRow.Cells[NoitruQheDoituongBuonggiuong.Columns.DonGia].Value, objBed.GiaKhac))
                                     )
                                     ;
                            objectType.PhuthuDungtuyen = Utility.DecimaltoDbnull(gridExRow.Cells[NoitruQheDoituongBuonggiuong.Columns.PhuthuDungtuyen].Value,0);
                            objectType.PhuthuTraituyen = Utility.DecimaltoDbnull(gridExRow.Cells[NoitruQheDoituongBuonggiuong.Columns.PhuthuTraituyen].Value,0);
                            objectType.MaDoituongKcb = Utility.sDbnull(gridExRow.Cells[NoitruQheDoituongBuonggiuong.Columns.MaDoituongKcb].Value);
                            // DmucDoituongkcb objectType1=DmucDoituongkcb.FetchByID()
                            SqlQuery sqlQuery =
                               new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.Columns.MaDoituongKcb).IsEqualTo(
                                   objectType.MaDoituongKcb);
                            DmucDoituongkcb objectType1 = sqlQuery.ExecuteSingle<DmucDoituongkcb>();
                            if (objectType1 != null)
                            {
                                objectType.IdLoaidoituongKcb = Utility.Int32Dbnull(objectType1.IdLoaidoituongKcb);
                            }
                            objectType.KieuThue = "GIUONG";
                            objectType.IdBuong = Utility.Int16Dbnull(txtBuong.MyID);
                            objectType.IdGiuong = Utility.Int16Dbnull(objBed.IdGiuong, -1);

                            objectType.IsNew = true;
                            objectType.Save();
                        }
                    }
                    #endregion

                }
                Utility.SetMsg(lblMsg, "Bạn thực hiện thêm mới thành công", true);
                if (chkthemmoilientuc.Checked) ClearControl();
                else
                    Close();
            }
            catch (Exception)
            {
                
                throw;
            }
           
        }
        /// <summary>
        /// hàm thưucj hiện việc cập nhập lại thông tin thành công
        /// </summary>
        private void UpdateData()
        {
          objBed=  createBed();
          objBed.MarkOld();
          objBed.IsNew = false;
             NoitruQheGiuongGiumCollection lstQhe = GetQuanheGiuongLoaiGiuong(-1);
             using (var scope = new TransactionScope())
             {
                 using (var sh = new SharedDbConnectionScope())
                 {
                     objBed.Save();
                     new Delete().From(NoitruQheGiuongGium.Schema).Where(NoitruQheGiuongGium.Columns.IdGiuong).IsEqualTo(objBed.IdGiuong).Execute();

                     foreach (NoitruQheGiuongGium obj in lstQhe)
                     {
                         obj.IdGiuong = objBed.IdGiuong;
                     }
                     lstQhe.SaveAll();
                 }
                 scope.Complete();
             }
            if (MyGetData != null)
            {
                MyGetData();
                Utility.GonewRowJanus(grdList, NoitruDmucGiuongbenh.Columns.IdGiuong, Utility.sDbnull(txtID.Text));
            }

            #region Tạm thời ko dùng
            if (chkApDungGia.Checked)
            {
                new Delete().From(NoitruQheDoituongBuonggiuong.Schema)
             .Where(NoitruQheDoituongBuonggiuong.Columns.IdGiuong).IsEqualTo(Utility.Int32Dbnull(txtID.Text)).Execute();
                foreach (Janus.Windows.GridEX.GridEXRow gridExRow in grdQuanheGiuong.GetCheckedRows())
                {
                    NoitruQheDoituongBuonggiuong objectType = new NoitruQheDoituongBuonggiuong();
                    objectType.DonGia = (objectType.MaDoituongKcb == "DV" ?
                                     Utility.DecimaltoDbnull(gridExRow.Cells[NoitruQheDoituongBuonggiuong.Columns.DonGia].Value, objBed.GiaDichvu)
                                     : (objectType.MaDoituongKcb == "BHYT" ?
                                     Utility.DecimaltoDbnull(gridExRow.Cells[NoitruQheDoituongBuonggiuong.Columns.DonGia].Value, objBed.GiaBhyt) :
                                     Utility.DecimaltoDbnull(gridExRow.Cells[NoitruQheDoituongBuonggiuong.Columns.DonGia].Value, objBed.GiaKhac))
                                     )
                                     ;
                    objectType.PhuthuDungtuyen = Utility.DecimaltoDbnull(gridExRow.Cells[NoitruQheDoituongBuonggiuong.Columns.PhuthuDungtuyen].Value,0);
                    objectType.PhuthuTraituyen = Utility.DecimaltoDbnull(gridExRow.Cells[NoitruQheDoituongBuonggiuong.Columns.PhuthuTraituyen].Value,0);
                    objectType.MaDoituongKcb = Utility.sDbnull(gridExRow.Cells[NoitruQheDoituongBuonggiuong.Columns.MaDoituongKcb].Value);
                    SqlQuery sqlQuery =
                        new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.Columns.MaDoituongKcb).IsEqualTo(
                            objectType.MaDoituongKcb);
                    DmucDoituongkcb objectType1 = sqlQuery.ExecuteSingle<DmucDoituongkcb>();
                    if (objectType1 != null)
                    {
                        objectType.IdLoaidoituongKcb = Utility.Int32Dbnull(objectType1.IdLoaidoituongKcb);
                    }
                    objectType.KieuThue = "GIUONG";
                    // DmucDoituongkcb objectType1=DmucDoituongkcb.FetchByID()
                    objectType.IdBuong = Utility.Int16Dbnull(txtBuong.MyID);
                    objectType.IdGiuong = Utility.Int16Dbnull(objBed.IdGiuong, -1);
                    objectType.IsNew = true;
                    objectType.Save();
                }

            }
            #endregion

            Utility.SetMsg(lblMsg, "Bạn thực hiện sửa thông tin  thành công", true);
            if (chkthemmoilientuc.Checked)
            {
                ClearControl();
            }
            else
                this.Close();
        }

      

        private void txtTEN_TextChanged(object sender, EventArgs e)
        {
            txtTen_BHYT.Text = txtTEN.Text;
        }

        private void txtMa_TextChanged(object sender, EventArgs e)
        {
        }
        /// <summary>
        /// hàm thực hiện cho phép nhập liên tục thông tin 
        /// </summary>
        private void NhapLienTuc()
        {

            m_enAct = action.Insert;
            ClearControl();
            SinhSTT();

        }
        private void SinhSTT()
        {
            if (m_enAct == action.Insert)
            {
                int MaxSTT =
                    Utility.Int32Dbnull(
                        _Query.AddWhere(NoitruDmucGiuongbenh.Columns.IdBuong, Comparison.Equals, Utility.Int32Dbnull(txtBuong.MyID)).GetMax(
                            NoitruDmucBuong.Columns.SttHthi), 0);

                txt_STT_HTHI.Value = Utility.Int32Dbnull(MaxSTT+ 1);
            }

        }
        private void ClearControl()
        {
         
           // Getdata();
            m_enAct = action.Insert;
            foreach (Control control in this.grpControl.Controls)
            {
                if (control is Janus.Windows.GridEX.EditControls.EditBox) control.Text = string.Empty;

            }
            if (chkcodefrom.Checked)
            {
                txtCodefrom.Text = (Utility.Int32Dbnull(Utility.DecimaltoDbnull(txtCode2.Text)) + 1).ToString();
                txtCode2.Text = txtCodefrom.Text;
            }
            chkcodefrom.Checked = false;
            txtMa.Enabled = true;
            //cboKhoaNoiTru.SelectedIndex = 0;
            txtMa.Focus();
            txtID.Text = "Tự sinh";

        }

        private bool bHasloaded = false;
        private void cboKhoaNoiTru_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bHasloaded)
            {

                DataTable dtBuong = SPs.NoitruTimkiembuongTheokhoa(Utility.Int32Dbnull(cboKhoaNoiTru.SelectedValue)).GetDataSet().Tables[0];
                txtBuong.Init(dtBuong, new List<string>() { NoitruDmucBuong.Columns.IdBuong, NoitruDmucBuong.Columns.MaBuong, NoitruDmucBuong.Columns.TenBuong });
               

                SinhSTT();
            }
            
        }

        private void cboPhongNoiTru_SelectedIndexChanged(object sender, EventArgs e)
        {
            SinhSTT();
        }

       
       
       

        private void cmdThemMoi_Click(object sender, EventArgs e)
        {
            ClearControl();
            chkcodefrom.Enabled = true;
            chkAutoupdate.Visible = lblSample.Visible = lblSuffix.Visible = chkcodefrom.Enabled;
        }

        private void cmdSaoChep_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtTen_BHYT.Text = txtTEN.Text;
        }

       

        private void chkcodefrom_CheckedChanged(object sender, EventArgs e)
        {
            txtCodefrom.Enabled = txtCode2.Enabled = chkcodefrom.Checked;
            lblmatiento.Text = chkcodefrom.Checked ?"Mã chung": "Mã giường" ;
            lblSuffix.Visible = lblSample.Visible = chkcodefrom.Checked;
        }

        private void grdQuanheGiuong_FormattingRow(object sender, Janus.Windows.GridEX.RowLoadEventArgs e)
        {

        }
    }
}
