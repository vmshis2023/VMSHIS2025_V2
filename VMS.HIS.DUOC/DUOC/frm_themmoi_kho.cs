using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Janus.Windows.EditControls;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using SubSonic;
using VMS.HIS.DAL;
using VNS.HIS.NGHIEPVU.THUOC;
using VNS.HIS.UI.DANHMUC;
using VNS.Libs;

namespace VNS.HIS.UI.THUOC
{
    public partial class frm_themmoi_kho : Form
    {
        #region "khai báo biến"

        private readonly string strFromThemKho = Application.StartupPath + @"\CAUHINH\FromThemKho.txt";
        private bool AllowTextChanged;
        public string Loai = "";
        public action em_Action = action.Insert;
        public GridEX grdList;
        private DataTable m_Doituong = new DataTable();
        public bool m_blnCancel = true;
        public DataTable p_dtDataChung = new DataTable();

        #endregion

        private readonly Query _Query = TDmucKho.CreateQuery();
        public bool b_Cancel = false;

        public frm_themmoi_kho()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            KeyDown += frm_Add_DCHUNG_KeyDown;
            cmdExit.Click += cmdExit_Click;
            txtTEN.LostFocus += txtTEN_LostFocus;
            txtMa.TextChanged += txtMa_TextChanged;
            txtTEN.LostFocus += txtTEN_LostFocus;
            txtTEN.TextChanged += txtTEN_TextChanged;
            optThuoc.Checked = true;

            txtKieubiendong._OnShowData += txtKieubiendong__OnShowData;
        }

        private void txtKieubiendong__OnShowData()
        {
            var _DMUC_DCHUNG = new DMUC_DCHUNG(txtKieubiendong.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtKieubiendong.myCode;
                txtKieubiendong.Init();
                txtKieubiendong.SetCode(oldCode);
                txtKieubiendong.Focus();
            }
        }

        /// <summary>
        ///     hàm thực hiện việc cho phép nhập thông tin của đơn vị tính trạng thía bắt buộc nhập
        /// </summary>
        private void SetCOntrolStatus()
        {
            Utility.ResetMessageError(errorProvider3);
            if (string.IsNullOrEmpty(txtMa.Text))
            {
                Utility.SetMsgError(errorProvider3, txtMa, "Nhập mã kho thuốc");
            }

            if (string.IsNullOrEmpty(txtTEN.Text))
            {
                Utility.SetMsgError(errorProvider3, txtTEN, "Nhập tên kho thuốc");
            }
        }

        /// <summary>
        ///     hàm thực hiện việc thêm mới thông tin
        /// </summary>
        private void InsertData()
        {
            try
            {
                TDmucKho objDmuckho = CreateKhoThuoc();
                objDmuckho.IsNew = true;
                objDmuckho.Save();
                m_blnCancel = false;
                txtIDKHO.Text = Utility.sDbnull(objDmuckho.IdKho);
                objDmuckho = TDmucKho.FetchByID(Utility.Int32Dbnull(txtIDKHO.Text, -1));
                if (objDmuckho != null)
                {
                    DataRow dataRow = p_dtDataChung.NewRow();
                    Utility.FromObjectToDatarow(objDmuckho, ref dataRow);
                    dataRow["ten_khoaphong"] = txtKhoanoitru.Text;
                    dataRow["ten_cosokcb"] = txtCosoKCB.Text;
                    p_dtDataChung.Rows.Add(dataRow);
                    Utility.GonewRowJanus(grdList, TDmucKho.Columns.IdKho, Utility.sDbnull(txtIDKHO.Text));
                }
                foreach (QheDoituongKho objdoituongkho in CreateDoiTuongKhoThem())
                {
                    QuanHeDoiTuongKho.THEM_DOITUONG_KHO(objdoituongkho);
                }
                Utility.ShowMsg("Bạn thực hiện thêm mới thành công", "Thông báo", MessageBoxIcon.Information);
                if (chkTrangThaiForm.Checked) Close();
                else
                {
                    NhapLienTuc();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
        string getInfor(TDmucKho objKho)
        {
            try
            {
                return string.Format("id_kho={0}, ten_kho={1}, noitru_ngoaitru={2}, chan_le={3}, thuoc_vt={4}, songay_nhaton={5}", objKho.IdKho, objKho.TenKho, objKho.LoaiBnhan, objKho.KieuKho, objKho.KhoThuocVt, objKho.SongayNhaton);
            }
            catch (Exception)
            {

                return string.Format("id_kho={0}, ten_kho={1}", objKho.IdKho, objKho.TenKho);
            }
           
        }
        /// <summary>
        ///     hàm thực hiện việc cập nhập lại thông tin thành công
        /// </summary>
        private void UpdateData()
        {
            try
            {
                TDmucKho objDmuckho = CreateKhoThuoc();
                objDmuckho.Save();
                string newInfor = getInfor(objDmuckho);
                Utility.Log(this.Name, globalVariables.UserName, string.Format("Thay đổi thông tin kho từ {0} sang {1} thành công ", oldInfor, newInfor), newaction.Update, this.GetType().Assembly.ManifestModule.Name);
                m_blnCancel = false;
                objDmuckho = TDmucKho.FetchByID(Utility.Int32Dbnull(txtIDKHO.Text, -1));
                DataRow[] arrDr =
                    p_dtDataChung.Select(string.Format("{0}={1}", TDmucKho.Columns.IdKho,
                        Utility.Int32Dbnull(txtIDKHO.Text)));
                if (arrDr.GetLength(0) > 0)
                {
                    arrDr[0].Delete();
                }
                p_dtDataChung.AcceptChanges();
                if (objDmuckho != null)
                {
                    DataRow dataRow = p_dtDataChung.NewRow();
                    Utility.FromObjectToDatarow(objDmuckho, ref dataRow);
                    dataRow["ten_cosokcb"] = txtCosoKCB.Text;
                    dataRow["ten_khoaphong"] = txtKhoanoitru.Text;
                    p_dtDataChung.Rows.Add(dataRow);
                    Utility.GonewRowJanus(grdList, TDmucKho.Columns.IdKho, Utility.sDbnull(txtIDKHO.Text));
                }
                new Delete().From(QheDoituongKho.Schema)
                    .Where(QheDoituongKho.Columns.IdKho).IsEqualTo(Utility.Int32Dbnull(txtIDKHO.Text, -1))
                    .Execute();

                foreach (QheDoituongKho objdoituongkho in CreateDoiTuongKhoThem())
                {
                    QuanHeDoiTuongKho.THEM_DOITUONG_KHO(objdoituongkho);
                }
                Utility.ShowMsg("Bạn đã cập nhật dữ liệu thành công", "Thông báo", MessageBoxIcon.Information);
                if (chkTrangThaiForm.Checked) Close();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private QheDoituongKho[] CreateDoiTuongKhoThem()
        {
            var arrDoituongKho = new QheDoituongKho[grdDoiTuong.RowCount];
            int idx = 0;
            foreach (GridEXRow rowSelect in grdDoiTuong.GetCheckedRows())
            {
                arrDoituongKho[idx] = new QheDoituongKho();
                arrDoituongKho[idx].IdKho = Utility.Int32Dbnull(txtIDKHO.Text);
                arrDoituongKho[idx].MaDoituongKcb =
                    Utility.sDbnull(rowSelect.Cells[QheDoituongKho.Columns.MaDoituongKcb].Value);
                arrDoituongKho[idx].SttHthi = Utility.Int32Dbnull(rowSelect.Cells[QheDoituongKho.Columns.SttHthi].Value);
                idx++;
            }
            return arrDoituongKho;
        }

        private TDmucKho CreateKhoThuoc()
        {
            var objDmuckho = new TDmucKho();
            if (em_Action == action.Update)
            {
                objDmuckho.IsLoaded = true;
                objDmuckho.MarkOld();
                objDmuckho.IdKho = Utility.Int32Dbnull(txtIDKHO.Text, -1);
                objDmuckho.NgaySua = globalVariables.SysDate;
                objDmuckho.NguoiSua = globalVariables.UserName;
            }
            objDmuckho.MaKho = Utility.sDbnull(txtMa.Text.ToUpper());
            objDmuckho.ChophepChongia = Utility.Bool2byte(chkChongiakhikedon.Checked);
            objDmuckho.TenKho = txtTEN.Text;
            objDmuckho.SttHthi = Utility.Int32Dbnull(txt_STT_HTHI.Value);
            objDmuckho.IdKhoaphong = Utility.Int16Dbnull(txtKhoanoitru.MyID, -1);
            objDmuckho.KhoThuocVt = optThuoc.Checked ? "THUOC" : (optVT.Checked ? "VT" : "THUOCVT");
            objDmuckho.KtraTon = Utility.ByteDbnull(chkSoLuongTon.Checked, 0);
            objDmuckho.LaTuthuoc = Utility.ByteDbnull(chkTuThuoc.Checked, 0);
            objDmuckho.MotaThem = Utility.sDbnull(txtGhiChu.Text);
            objDmuckho.LaQuaythuoc = (byte?) (chkKhoBan.Checked ? 1 : 0);
            objDmuckho.TrangThai = (byte?) (chkHienThi.Checked ? 1 : 0);
            objDmuckho.KieuBiendong = txtKieubiendong.myCode;
            objDmuckho.SongayNhaton = Utility.DoubletoDbnull(txtSongaynhaton.Text);
            objDmuckho.LoaibothuocHethan =chkCamthuochethan.Checked;
            objDmuckho.KhongTinhgia = chkKhongtinhgia.Checked;
            objDmuckho.CapCuu = Utility.Bool2byte(chkTuthuoccapcuu.Checked);
            objDmuckho.MaCoso = txtCosoKCB.myCode;
            objDmuckho.NgayTao = globalVariables.SysDate;
            objDmuckho.NguoiTao = globalVariables.UserName;
            objDmuckho.LoaiKho = Utility.Bool2byte(optKhoAo.Checked);
            byte kieukho = 0;
            //if(radKhoTong.Checked) kieukho = 0;
            if (radKhoLe.Checked) kieukho = 1;
            if (radKhoChan.Checked) kieukho = 2;
            if (optChanle.Checked) kieukho = 3;
            objDmuckho.KieuKho = kieukho == 1 ? "LE" : (kieukho == 2 ? "CHAN" : "CHANLE");
            string loaiBn = "TATCA";
            if (radTatCa.Checked) loaiBn = "TATCA";
            if (radNgoaiTru.Checked) loaiBn = "NGOAITRU";
            if (radNoiTru.Checked) loaiBn = "NOITRU";
            objDmuckho.LoaiBnhan = loaiBn;
            return objDmuckho;
        }


        private void txtTEN_LostFocus(object sender, EventArgs e)
        {
            txtTEN.Text = Utility.chuanhoachuoi(txtTEN.Text);
        }


        private void txtTEN_TextChanged(object sender, EventArgs e)
        {
            SetCOntrolStatus();
        }

        /// <summary>
        ///     hàm thực hiện bắt sự kiện của form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdSave_Click(object sender, EventArgs e)
        {
            if (!IsValidData()) return;
            PerformAction();
        }

        /// <summary>
        ///     Kiểm tra dữ liệu
        /// </summary>
        /// <returns></returns>
        private bool IsValidData()
        {
            if (string.IsNullOrEmpty(txtMa.Text))
            {
                Utility.ShowMsg("Bạn phải nhập thông tin mã ", "Thông báo", MessageBoxIcon.Warning);
                txtMa.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtTEN.Text))
            {
                Utility.ShowMsg("Bạn phải nhập thông tin tên ", "Thông báo", MessageBoxIcon.Warning);
                txtTEN.Focus();
                return false;
            }

            if (em_Action == action.Insert)
            {
                SqlQuery sqlQuery = new Select().From(TDmucKho.Schema)
                    .Where(TDmucKho.Columns.MaKho).IsEqualTo(txtMa.Text);
                sqlQuery.Or(TDmucKho.Columns.TenKho).IsEqualTo(txtTEN.Text);
                //sqlQuery.And(TDmucKho.Columns.IdKho).IsEqualTo(Utility.Int32Dbnull(txtIDKHO.Text));
                if (sqlQuery.GetRecordCount() > 0)
                {
                    Utility.ShowMsg("Mã kho hoặc tên Tên kho đã tồn tại, Bạn xem lại, hoặc nhập thông tin khác",
                        "Thông báo", MessageBoxIcon.Warning);
                    txtMa.Focus();
                    return false;
                }
            }
            else
            {
                SqlQuery sqlQuery_All = new Select().From(TDmucKho.Schema)
                    .Where(TDmucKho.Columns.IdKho).IsNotEqualTo(Utility.Int32Dbnull(txtIDKHO.Text));
                sqlQuery_All.And(TDmucKho.Columns.TenKho)
                    .IsEqualTo(txtTEN.Text)
                    .And(TDmucKho.Columns.MaKho)
                    .IsEqualTo(txtMa.Text);


                if (sqlQuery_All.GetRecordCount() > 0)
                {
                    Utility.ShowMsg("Mã kho và Tên kho đã tồn tại, Bạn xem lại, hoặc nhập thông tin khác",
                        "Thông báo", MessageBoxIcon.Warning);
                    txtMa.Focus();
                    return false;
                }

                SqlQuery sqlQuery_Name = new Select().From(TDmucKho.Schema)
                    .Where(TDmucKho.Columns.IdKho).IsNotEqualTo(Utility.Int32Dbnull(txtIDKHO.Text));
                sqlQuery_Name.And(TDmucKho.Columns.TenKho).IsEqualTo(txtTEN.Text);
                //.And(TDmucKho.Columns.MaKho).IsEqualTo(txtMa.Text);


                if (sqlQuery_Name.GetRecordCount() > 0)
                {
                    Utility.ShowMsg("Tên kho đã tồn tại, Bạn xem lại, hoặc nhập Tên kho khác",
                        "Thông báo", MessageBoxIcon.Warning);
                    txtMa.Focus();
                    return false;
                }
                SqlQuery sqlQuery_Code = new Select().From(TDmucKho.Schema)
                    .Where(TDmucKho.Columns.IdKho).IsNotEqualTo(Utility.Int32Dbnull(txtIDKHO.Text));
                sqlQuery_Code.And(TDmucKho.Columns.MaKho).IsEqualTo(txtMa.Text);


                if (sqlQuery_Code.GetRecordCount() > 0)
                {
                    Utility.ShowMsg("Mã kho đã tồn tại, Bạn xem lại, hoặc nhập mã kho khác",
                        "Thông báo", MessageBoxIcon.Warning);
                    txtMa.Focus();
                    return false;
                }
            }

            return true;
        }

        private void PerformAction()
        {
            switch (em_Action)
            {
                case action.Insert:
                    InsertData();
                    break;
                case action.Update:
                    UpdateData();
                    break;
            }
            b_Cancel = true;
        }

        private void frm_Add_DCHUNG_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S) cmdSave.PerformClick();
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.F5) NhapLienTuc();
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Getdata()
        {
            TDmucKho objDmucKho = TDmucKho.FetchByID(Utility.sDbnull(txtIDKHO.Text, -1));
            if (objDmucKho != null)
            {
                oldInfor = getInfor(objDmucKho);
                txt_STT_HTHI.Text = Utility.sDbnull(objDmucKho.SttHthi, 1);
                txtMa.Text = Utility.sDbnull(objDmucKho.MaKho, "");
                txtTEN.Text = Utility.sDbnull(objDmucKho.TenKho, "");
                txtCosoKCB.SetCode(objDmucKho.MaCoso);
                radKhoChan.Checked = objDmucKho.KieuKho == "CHAN";
                radKhoLe.Checked = objDmucKho.KieuKho == "LE";
                optChanle.Checked = objDmucKho.KieuKho == "CHANLE";
                chkChongiakhikedon.Checked = Utility.Byte2Bool(objDmucKho.ChophepChongia);
                txtKieubiendong.SetCode(objDmucKho.KieuBiendong);
                if (Utility.sDbnull(objDmucKho.KhoThuocVt) == "VT")
                {
                    optVT.Checked = true;
                }
                else if (Utility.sDbnull(objDmucKho.KhoThuocVt) == "THUOC")
                    optThuoc.Checked = true;
                else
                    optThuocVT.Checked = true;

                optKhothuong.Checked = Utility.ByteDbnull(objDmucKho.LoaiKho, 0) == 0;
                optKhoAo.Checked = Utility.ByteDbnull(objDmucKho.LoaiKho, 0) == 1;
                txtKhoanoitru.SetId(Utility.sDbnull(objDmucKho.IdKhoaphong, "-1"));
                chkSoLuongTon.Checked = Utility.Int32Dbnull(objDmucKho.KtraTon) == 1;
                chkTuThuoc.Checked = Utility.Int32Dbnull(objDmucKho.LaTuthuoc) == 1;
                chkHienThi.Checked = Utility.Int32Dbnull(objDmucKho.TrangThai) == 1;
                chkKhongtinhgia.Checked = Utility.Byte2Bool(objDmucKho.KhongTinhgia);
                chkCamthuochethan.Checked = Utility.Byte2Bool(objDmucKho.LoaibothuocHethan);
                chkTuthuoccapcuu.Checked = Utility.Byte2Bool(objDmucKho.CapCuu);
                txtSongaynhaton.Text =Utility.DoubletoDbnull( objDmucKho.SongayNhaton,1f).ToString();
                int loaiBN = 0;
                if (Utility.sDbnull(objDmucKho.LoaiBnhan, "") == "TATCA") loaiBN = 0;

                else if (Utility.sDbnull(objDmucKho.LoaiBnhan, "") == "NGOAITRU")
                {
                    loaiBN = 1;
                }
                else if (Utility.sDbnull(objDmucKho.LoaiBnhan, "") == "NOITRU")
                {
                    loaiBN = 2;
                }


                radTatCa.Checked = loaiBN == 0;
                radNgoaiTru.Checked = loaiBN == 1;
                radNoiTru.Checked = loaiBN == 2;
                txtGhiChu.Text = Utility.sDbnull(objDmucKho.MotaThem);
                chkKhoBan.Checked = Utility.Int32Dbnull(objDmucKho.LaQuaythuoc) == 1;
            }
        }

        private void txtMa_TextChanged(object sender, EventArgs e)
        {
            SetCOntrolStatus();
        }

        /// <summary>
        ///     hàm thực hiện cho phép nhập liên tục thông tin
        /// </summary>
        private void NhapLienTuc()
        {
            em_Action = action.Insert;
            ClearControl();
            InitData();
        }

        private void InitData()
        {
            if (em_Action == action.Insert)
            {
                int MaxSTT =
                    Utility.Int32Dbnull(
                        _Query.GetMax(
                            TDmucKho.Columns.SttHthi), 0);

                txt_STT_HTHI.Value = Utility.Int32Dbnull(MaxSTT + 1);

                //txtID_DICHVU.Text = Utility.sDbnull(Utility.Int32Dbnull(_Query.GetMax(DDichVu.Columns.IdDvu)) + 1, "1");
                txtMa.Focus();
            }
        }

        /// <summary>
        ///     xóa trắng các control trên Form.
        /// </summary>
        private void ClearControl()
        {
            foreach (Control control in grpControl.Controls)
            {
                if (control is EditBox) control.Text = string.Empty;
                if (control is UIComboBox)
                {
                    var txtControl = new UIComboBox();
                    txtControl = ((UIComboBox) control);
                    txtControl.SelectedIndex = 0;
                }
                if (control is UICheckBox)
                {
                    var txtControl = new UICheckBox();
                    txtControl = ((UICheckBox) control);
                    txtControl.Checked = false;
                }
                if (control is UIRadioButton)
                {
                    var txtControl = new UIRadioButton();
                    txtControl = ((UIRadioButton) control);
                    txtControl.Checked = false;
                }
            }
            foreach (GridEXRow grRow in grdDoiTuong.GetRows())
            {
                grRow.IsChecked = false;
            }
            txtMa.Enabled = true;
            txtMa.Focus();
        }

        private int MaxStt()
        {
            int stt = 100;
            stt = new Select(Aggregate.Max(TDmucKho.Columns.SttHthi)).From(TDmucKho.Schema).ExecuteScalar<int>() + 1;
            return stt;
        }
        string oldInfor = "";
        private void frm_themmoi_kho_Load(object sender, EventArgs e)
        {
            try
            {
                optChanle.Enabled = optThuocVT.Enabled = THU_VIEN_CHUNG.Laygiatrithamsohethong("THUOC_CHOPHEPCHUNGKHO_THUOCVT", "0", true) == "1";
                txtKieubiendong.Init();
                txtCosoKCB.Init();
                AutoFromThemKho();
                SetCOntrolStatus();
                Laydanhsachkhoa();
                if (em_Action == action.Update) Getdata();
                else
                {
                    InitData();
                }
                txtTEN.Focus();
                m_Doituong =
                    new Select().From(DmucDoituongkcb.Schema)
                        .OrderAsc(DmucDoituongkcb.Columns.SttHthi)
                        .ExecuteDataSet()
                        .Tables[0];
                Utility.SetDataSourceForDataGridEx(grdDoiTuong, m_Doituong, false, false, "", "");
                if (em_Action == action.Update)
                {
                    LoadMoiQuanHe();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void LoadMoiQuanHe()
        {
            SqlQuery sql =
                new Select().From(QheDoituongKho.Schema).Where(QheDoituongKho.Columns.IdKho).IsEqualTo(
                    Utility.Int32Dbnull(txtIDKHO.Text));
            DataTable m_MQH = sql.ExecuteAsCollection<QheDoituongKhoCollection>().ToDataTable();
            foreach (DataRow dr in m_MQH.Rows)
            {
                foreach (GridEXRow grRow in grdDoiTuong.GetRows())
                {
                    if (dr[QheDoituongKho.Columns.MaDoituongKcb].ToString() ==
                        grRow.Cells[QheDoituongKho.Columns.MaDoituongKcb].Value.ToString())
                    {
                        grRow.BeginEdit();
                        grRow.IsChecked = true;
                        grRow.Cells[QheDoituongKho.Columns.SttHthi].Value =
                            dr[QheDoituongKho.Columns.SttHthi].ToString();
                        grRow.EndEdit();
                    }
                }
            }
        }

        private void Laydanhsachkhoa()
        {
            DataTable dtDepartment = THU_VIEN_CHUNG.Laydanhmuckhoa("ALL","ALL", -1);
            txtKhoanoitru.Init(dtDepartment,
                new List<string>
                {
                    DmucKhoaphong.Columns.IdKhoaphong,
                    DmucKhoaphong.Columns.MaKhoaphong,
                    DmucKhoaphong.Columns.TenKhoaphong
                });
        }

        private void AutoFromThemKho()
        {
            try
            {
                Try2CreateFolder();
                AllowTextChanged = false;
                if (!File.Exists(strFromThemKho)) return;
                using (var reader = new StreamReader(strFromThemKho))
                {
                    var readLine = reader.ReadLine();
                    if (readLine != null) chkTrangThaiForm.Checked = readLine.Trim() == "1";
                    reader.BaseStream.Flush();
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
            finally
            {
                AllowTextChanged = true;
            }
        }

        private void Try2CreateFolder()
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(strFromThemKho)))
                    Directory.CreateDirectory(Path.GetDirectoryName(strFromThemKho));
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void chkTrangThaiForm_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (!AllowTextChanged) return;
                using (var _writer = new StreamWriter(strFromThemKho))
                {
                    _writer.WriteLine(chkTrangThaiForm.Checked ? "1" : "0");
                    _writer.Flush();
                    _writer.Close();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi khi lưu trạng thái-->" + ex.Message);
            }
        }

        private void grpControl_Click(object sender, EventArgs e)
        {
        }

        private void cmdComboDown_Click(object sender, EventArgs e)
        {
            txtKhoanoitru.ShowMe();
        }
    }
}