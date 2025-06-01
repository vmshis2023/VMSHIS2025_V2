using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NLog;
using VNS.Libs;
using VMS.HIS.DAL;
using VNS.Properties;
using VNS.HIS.BusRule.Classes;
using SubSonic;
using VNS.HIS.UI.DANHMUC;
using System.IO;
using VNS.HIS.UI.Forms.Cauhinh;
namespace VNS.HIS.UCs.Noitru
{
    public partial class ucChandoanICD : UserControl
    {
        public delegate void OnChangedData();

        public event OnChangedData _OnChangedData;
        byte noitru = 0;
        action m_enAct = action.FirstOrFinished;
        bool AllowedChanged = false;
        string tenkhoadieutri = "";
        bool AllowedChanged_maskedEdit = false;
        KcbChandoanKetluan objICD = null;
        private DataTable m_dtTimKiembenhNhan = new DataTable();
        public TrangthaiNoitru _TrangthaiNoitru = TrangthaiNoitru.NoiTru;
        public bool callfromMenu = true;
        public KcbLuotkham objLuotkham = null;
        public KcbDanhsachBenhnhan objBenhnhan = null;
        public ucChandoanICD()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            dtpNgaytao.Value = globalVariables.SysDate;
            InitEvents();
            
        }
        public void ChangePatients(KcbLuotkham objLuotkham,string tenkhoadieutri)
        {
            this.noitru = noitru;
            this.tenkhoadieutri = tenkhoadieutri;
            dtpNgaytao.Value = globalVariables.SysDate;
            AllowedChanged = false;
            this.objLuotkham = objLuotkham;
            Init();
        }
       
        void InitEvents()
        {
            
            grdICD.SelectionChanged += new EventHandler(grdICD_SelectionChanged);
            grdICD.KeyDown += new KeyEventHandler(grdICD_KeyDown);
          

            cmdthemmoi.Click += new EventHandler(cmdthemmoi_Click);
            cmdSua.Click += new EventHandler(cmdSua_Click);
            cmdxoa.Click += new EventHandler(cmdxoa_Click);
            
            cmdHuy.Click += new EventHandler(cmdHuy_Click);
            cmdGhi.Click += new EventHandler(cmdGhi_Click);
            txtChandoan._OnShowData += txtLoidanBS__OnShowData;
            //foreach(Control ctrl in panel2.Controls)
            //    ctrl.KeyDown += ctrl_KeyDown;

            optTatca.CheckedChanged += optTatca_CheckedChanged;
            optThuockhoa.CheckedChanged += optTatca_CheckedChanged;
            optKhoakhac.CheckedChanged += optTatca_CheckedChanged;
            
        }

        void optTatca_CheckedChanged(object sender, EventArgs e)
        {
           string Rowfilter="1=1";
            if(optKhoakhac.Checked)
                Rowfilter = "thuoc_khoa=0";
            else if (optThuockhoa.Checked)
                Rowfilter = "thuoc_khoa=1";
           m_dtICD.DefaultView.RowFilter = Rowfilter;
           m_dtICD.AcceptChanges();
        }

        void txtLoidanBS__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtChandoan.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtChandoan.myCode;
                txtChandoan.Init();
                txtChandoan.SetCode(oldCode);
                txtChandoan.Focus();
            } 
        }
        public void EnterNextControl(Control currentCtr)
        {
            SelectNextControl(currentCtr, true, true, true, true);
        }
        void ctrl_KeyDown(object sender, KeyEventArgs e)
        {
            Control _ctrl = sender as Control;
            if (e.KeyCode == Keys.Enter)
                SelectNextControl(_ctrl, true, true, true, true);
        }


        void grdICD_KeyDown(object sender, KeyEventArgs e)
        {
            if (Utility.isValidGrid(grdICD) && e.KeyCode == Keys.Delete) cmdxoa.PerformClick();
        }

      
        void cmdGhi_Click(object sender, EventArgs e)
        {
            if (!isValidData()) return;
            try
            {
                
                if (m_enAct == action.Insert)
                {
                    KcbChandoanKetluan newICD = new KcbChandoanKetluan();
                    newICD.IdBenhnhan = objLuotkham.IdBenhnhan;
                    newICD.MaLuotkham = objLuotkham.MaLuotkham;
                    newICD.IdKhoanoitru = objLuotkham.IdKhoanoitru;
                    newICD.IdBuong = objLuotkham.IdBuong;
                    newICD.IdBuonggiuong = objLuotkham.IdRavien;
                    newICD.IdGiuong = objLuotkham.IdGiuong;
                    newICD.KieuChandoan = 2;//Chẩn đoán nội trú hằng ngày
                    newICD.MabenhChinh = txtBenhchinh.MyCode;
                    newICD.Chandoan = txtChandoan.Text;
                    newICD.NgayChandoan = dtpNgaytao.Value;
                    newICD.IdKham = -1;
                    newICD.IdBacsikham = -1;
                    newICD.Noitru = objLuotkham.Noitru.Value;
                    newICD.NguoiTao = globalVariables.UserName;
                    newICD.NgayTao = DateTime.Now;
                    newICD.IsNew = true;
                    newICD.Save();


                    DataRow newDr = m_dtICD.NewRow();
                    Utility.FromObjectToDatarow(newICD, ref newDr);
                    newDr["ten_khoanoitru"] = tenkhoadieutri;
                    newDr["chandoan"] = txtChandoan.Text;
                    newDr["ten_nguoitao"] = globalVariables.gv_strTenNhanvien;
                    m_dtICD.Rows.Add(newDr);
                    m_dtICD.AcceptChanges();
                    Utility.GotoNewRowJanus(grdICD, KcbChandoanKetluan.Columns.IdChandoan, newICD.IdChandoan.ToString());
                    m_enAct = action.FirstOrFinished;

                }
                else
                {
                    objICD.MabenhChinh = txtBenhchinh.MyCode;
                    objICD.Chandoan = txtChandoan.Text;
                    objICD.NgayChandoan = dtpNgaytao.Value;
                    objICD.NguoiSua = globalVariables.UserName;
                    objICD.NgaySua = DateTime.Now;
                    objICD.IsNew = false;
                    objICD.MarkOld();
                    objICD.Save();
                    DataRow _myDr = ((DataRowView)grdICD.CurrentRow.DataRow).Row;
                    _myDr["mabenh_chinh"] = txtBenhchinh.MyCode;
                    _myDr["chandoan"] = txtChandoan.Text;
                    _myDr["ngay_chandoan"] = dtpNgaytao.Value;
                    _myDr["ten_nguoisua"] = globalVariables.gv_strTenNhanvien;
                    m_dtICD.AcceptChanges();
                    m_enAct = action.FirstOrFinished;
                }                
                if (_OnChangedData != null) _OnChangedData();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                SetControlStatus();
            }
        }
        bool isValidData()
        {
            Utility.SetMsg(lblMsg, "", true);
            if (objICD != null && objICD.Noitru == 0)
            {
                Utility.SetMsg(lblMsg, "Bạn không được phép sửa các chẩn đoán từ ngoại trú. Vui lòng chọn lại các chẩn đoán nội trú để sửa", true);
                Utility.ShowMsg(lblMsg.Text);
                return false;
            }
            objLuotkham = Utility.getKcbLuotkham(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham);
            
            if (objLuotkham == null)
            {
                Utility.SetMsg(lblMsg, "Bạn cần chọn Bệnh nhân cần nhập chẩn đoán", true);
                return false;
            }
            //Kiểm tra nhập chẩn đoán ngoại trú
            //if (objLuotkham.TrangthaiNoitru<=0 && THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_KICHHOAT_TAMUNG_NGOAITRU",true)=="1")
            //{
            //    return true;
            //}
            if (objLuotkham.TrangthaiNoitru >=6)
            {
                Utility.ShowMsg("Bệnh nhân đã thanh toán ra viện nên bạn không thể nộp thêm nhập chẩn đoán");
                return false;
            }
            //if (!THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb) && !new noitru_TamungHoanung().DathanhtoanhetNgoaitru(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham))
            //{
            //    Utility.SetMsg(lblMsg, "Bệnh nhân Dịch vụ chưa thanh toán hết tiền ngoại trú", true);
            //    return false;
            //}
            //if (txtBenhchinh.MyCode=="-1")
            //{
            //    Utility.SetMsg(lblMsg, "Bạn cần nhập mã bệnh ICD ", true);
            //    txtBenhchinh.Focus();
            //    return false;
            //}
            if (Utility.DoTrim(txtChandoan.Text) == "")
            {
                Utility.SetMsg(lblMsg, "Bạn cần nhập thông tin chẩn đoán ", true);
                txtChandoan.Focus();
                return false;
            }
            
            return true;
        }
        void cmdHuy_Click(object sender, EventArgs e)
        {
            m_enAct = action.FirstOrFinished;
            SetControlStatus();
        }

       

        void cmdxoa_Click(object sender, EventArgs e)
        {
            XoaChandoan();
            m_enAct = action.FirstOrFinished;
            SetControlStatus();
        }
        void XoaChandoan()
        {
            if (!isValidXoaChandoan()) return;
            try
            {
                if (Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa dòng chẩn đoán đang chọn này hay không? (Chú ý: Khi xóa chẩn đoán có thể ảnh hưởng đến thông tin chẩn đoán đã in trên một số phiếu trước đó như phiếu công khai, phiếu chỉ định, đơn thuốc...)", "Xác nhận", true))
                {
                    if (objICD != null)
                    {
                        int num = new Delete().From(KcbChandoanKetluan.Schema).Where(KcbChandoanKetluan.Columns.IdChandoan).IsEqualTo(Utility.Int64Dbnull(grdICD.GetValue("id_chandoan"))).Execute();
                        if (num > 0)
                        {
                            Utility.Log(this.Name, globalVariables.UserName, string.Format("Xóa chẩn đoán với thông tin ngày CĐ = {0}, CĐ chính = {1}, CĐ phụ ={2} của bệnh nhân ID={3}, PID={4}, Tên={5}", Utility.sDbnull(grdICD.GetValue("ngay_chandoan"), ""), Utility.sDbnull(grdICD.GetValue("ten_benhchinh"), ""), Utility.sDbnull(grdICD.GetValue("chandoan"), ""), objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham, objBenhnhan.TenBenhnhan), newaction.Delete, this.GetType().Assembly.ManifestModule.Name);
                            Utility.ShowMsg(string.Format("Xóa chẩn đoán thành công", ""));
                        }
                        DataRow drDelete = Utility.getCurrentDataRow(grdICD);
                        if (drDelete != null)
                        {
                            m_dtICD.Rows.Remove(drDelete);
                            m_dtICD.AcceptChanges();
                        }
                        SetControlStatus();
                    }
                    else
                    {
                        Utility.SetMsg(lblMsg, string.Format("Bạn cần chọn dòng chẩn đoán cần xóa"), true);
                    }
                }
                if (_OnChangedData != null) _OnChangedData();
            }
            catch (Exception ex)
            {

            }
            finally
            {
            }
        }
        bool isValidXoaChandoan()
        {
            if (!Utility.isValidGrid(grdICD))
            {
                Utility.SetMsg(lblMsg, "Bạn cần chọn một dòng nhập chẩn đoán trên lưới để thực hiện xóa", true);
                Utility.ShowMsg(lblMsg.Text);
                return false;
            }
            if (grdICD.GetValue("noitru").ToString() == "0")
            {
                Utility.SetMsg(lblMsg, "Chẩn đoán bạn chọn là chẩn đoán từ ngoại trú nên bạn không thể xóa", true);
                Utility.ShowMsg(lblMsg.Text);
                return false;
            }
            objLuotkham = Utility.getKcbLuotkham(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham);
            if (objLuotkham == null)
            {
                Utility.SetMsg(lblMsg, "Bạn cần chọn Bệnh nhân cần nhập chẩn đoán", true);
                Utility.ShowMsg(lblMsg.Text);
                return false;
            }
            int Dieukienxoachandoannoitru =Utility.Int32Dbnull( THU_VIEN_CHUNG.Laygiatrithamsohethong("NOITRU_CHANDOAN_TRANGTHAICHOPHEPXOA", "4", true),4);
            if (Utility.Byte2Bool(objLuotkham.Noitru) && objLuotkham.TrangthaiNoitru >= Dieukienxoachandoannoitru)
            {
                Utility.ShowMsg("Bệnh nhân đã được làm thủ tục ra viện nên bạn không thể xóa chẩn đoán được");
                return false;
            }
            return true;
        }
        void cmdSua_Click(object sender, EventArgs e)
        {
            m_enAct = action.Update;
            AllowedChanged_maskedEdit = true;
            SetControlStatus();
        }
        public void Themmoi()
        {
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Bạn cần chọn bệnh nhân cần nhập chẩn đoán");
                return;
            }
            if (objLuotkham.TrangthaiNoitru >= 6)
            {
                Utility.ShowMsg("Bệnh nhân đã thanh toán ra viện nên bạn không thể nhập chẩn đoán");
                return;
            }
            m_enAct = action.Insert;
            AllowedChanged_maskedEdit = true;
            SetControlStatus();
        }
         void cmdthemmoi_Click(object sender, EventArgs e)
        {
            Themmoi();
        }
         private void SetControlStatus()
         {
             try
             {
                 grdICD.Enabled = false;
                 AllowedChanged = false;
                 switch (m_enAct)
                 {
                     case action.Insert:
                         dtpNgaytao.Enabled = true;
                         txtBenhchinh.Enabled = true;
                         txtChandoan.Enabled = true;
                         objICD = null;
                         txtBenhchinh.SetId("-1");
                         txtChandoan._Text = "";

                         //--------------------------------------------------------------
                         //Thiết lập trạng thái các nút Insert, Update, Delete...
                         //Không cho phép nhấn Insert, Update,Delete
                         cmdthemmoi.Enabled = false;
                         cmdSua.Enabled = false;
                         cmdxoa.Enabled = false;
                         cmdGhi.Enabled = true;
                         cmdHuy.Enabled = true;
                         cmdGhi.BringToFront();
                         cmdHuy.BringToFront();

                         //--------------------------------------------------------------
                         //Không cho phép chọn trên lưới dữ liệu được fill vào các Control
                         AllowedChanged = false;
                         //Tự động Focus đến mục ID để người dùng nhập liệu
                         cmdHuy.Text = "Hủy";
                         txtBenhchinh.Focus();
                         break;
                     case action.Update:
                         dtpNgaytao.Enabled = true;
                         txtBenhchinh.Enabled = true;
                         txtChandoan.Enabled = true;
                         //--------------------------------------------------------------
                         //Thiết lập trạng thái các nút Insert, Update, Delete...
                         //Không cho phép nhấn Insert, Update,Delete
                         cmdthemmoi.Enabled = false;
                         cmdSua.Enabled = false;
                         cmdxoa.Enabled = false;
                         cmdGhi.Enabled = true;
                         cmdHuy.Enabled = true;
                         cmdGhi.BringToFront();
                         cmdHuy.BringToFront();
                         cmdHuy.Text = "Hủy";
                         //--------------------------------------------------------------
                         //Không cho phép chọn trên lưới dữ liệu được fill vào các Control
                         AllowedChanged = false;
                         //Tự động Focus đến mục Position để người dùng nhập liệu
                         txtBenhchinh.Focus();
                         break;
                     case action.FirstOrFinished://Hủy hoặc trạng thái ban đầu khi mới hiển thị Form

                         grdICD.Enabled = true;
                         AllowedChanged = true;

                         dtpNgaytao.Enabled = false;
                         txtBenhchinh.Enabled = false;
                         txtChandoan.Enabled = false;

                         //--------------------------------------------------------------
                         //Thiết lập trạng thái các nút Insert, Update, Delete...
                         //Sau khi nhấn Ghi thành công hoặc Hủy thao tác thì quay về trạng thái ban đầu
                         //Cho phép thêm mới
                         cmdGhi.Enabled = false;
                         cmdHuy.Enabled = false;
                         cmdGhi.SendToBack();
                         cmdHuy.SendToBack();
                         //Nút Hủy biến thành nút thoát
                         cmdHuy.Text = "Thoát";
                         //--------------------------------------------------------------
                         //Cho phép chọn trên lưới dữ liệu được fill vào các Control
                         AllowedChanged = true;
                         //Tự động chọn dòng hiện tại trên lưới để hiển thị lại trên Control
                         grdICD_SelectionChanged(grdICD, new EventArgs());
                         //Tự động Focus đến nút thêm mới? 
                         cmdthemmoi.Focus();
                         break;
                     default:
                         break;
                 }
             }
             catch (Exception ex)
             {

             }
             finally
             {
                 grdICD.Enabled = true;
             }

         }
        void grdICD_SelectionChanged(object sender, EventArgs e)
        {
            if (!AllowedChanged) return;
            FillData();
        }
        void FillData()
        {
            try
            {
                if (!Utility.isValidGrid(grdICD))
                {

                    objICD = null;
                    txtBenhchinh.SetCode("-1");
                    txtChandoan.SetCode("-1");
                }
                else
                {

                    objICD = KcbChandoanKetluan.FetchByID(Utility.Int32Dbnull(grdICD.GetValue(KcbChandoanKetluan.Columns.IdChandoan)));

                    if (objICD == null)
                    {
                        txtBenhchinh.SetCode("-1");
                        txtChandoan.SetCode("-1");
                    }
                    else
                    {
                        objICD.IsNew = false;
                        objICD.MarkOld();
                        dtpNgaytao.Value = objICD.NgayChandoan;
                        txtBenhchinh.SetCode(objICD.MabenhChinh);
                        if (objICD.Noitru == 0)
                            txtChandoan.SetCode(objICD.Chandoan);
                        else
                            txtChandoan._Text = objICD.Chandoan;

                    }
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                ModifyCommand();
                //SetControlStatus();
            }
        }
        

        public void Init()
        {
           
            LaydanhsachICD();
            if (objLuotkham != null && objLuotkham.TrangthaiNoitru > 0 && objLuotkham.NgayNhapvien.HasValue)
                dtpNgaytao.MinDate = objLuotkham.NgayNhapvien.Value;
            
            m_enAct = action.FirstOrFinished;
            SetControlStatus();
            ModifyCommand();

        }
        
        private DataTable m_dtKhoaNoiTru = new DataTable();
      
        public void AutoCompleteTextBox()
        {
            txtChandoan.Init();
            txtBenhchinh.Init(globalVariables.gv_dtDmucBenh, new List<string> { DmucBenh.Columns.IdBenh, DmucBenh.Columns.MaBenh, DmucBenh.Columns.TenBenh });

        }
      
       
        private void ModifyCommand()
        {
            bool isValid = objLuotkham != null;
            bool isValid2 = Utility.isValidGrid(grdICD);
            cmdSua.Enabled = isValid && isValid2;
            cmdxoa.Enabled = isValid && isValid2;
            cmdthemmoi.Enabled = isValid;
            cmdGhi.Enabled = m_enAct != action.FirstOrFinished;
            cmdHuy.Enabled = cmdGhi.Enabled;
            optTatca.Enabled = optThuockhoa.Enabled = optKhoakhac.Enabled = isValid && isValid2;
        }
        public void Refresh()
        {
            LaydanhsachICD();
        }
        public DataTable m_dtICD = null;
        
         void LaydanhsachICD()
        {
            try
            {
                if (objLuotkham == null)
                {
                    grdICD.DataSource = null;
                    return;
                }
                m_dtICD = SPs.NoitruLaythongtinchandoan(objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan, objLuotkham.IdKhoanoitru, Utility.ByteDbnull(optTatca.Checked ? 100 : (optThuockhoa.Checked ? 0 : 1)),new DateTime(1900,1,1)).GetDataSet().Tables[0];
                Utility.SetDataSourceForDataGridEx_Basic(grdICD, m_dtICD, false, true, "1=1", KcbChandoanKetluan.Columns.NgayChandoan + " desc");
                grdICD.MoveFirst();
                if (grdICD.GetDataRows().Length <= 0)
                {
                    objICD = null;
                    dtpNgaytao.Value = globalVariables.SysDate;
                    txtBenhchinh.SetCode("-1");
                    txtChandoan.SetCode("-1");
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                AllowedChanged = true;
                ShowLSuTamung();
            }
        }
        void ShowLSuTamung()
        {
            if (objLuotkham != null)
            {
                grdICD.Width = 0;
            }
            else
            {
                grdICD.Width = 425;
            }
        }

        
        /// <summary>
        /// hàm thực hiện việc phím tắt thông tin 
        /// </summary>
        private string MaLuotkham { get; set; }
        private void frm_Quanlytamung_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ProcessTabKey(true);
                return;
            }

            if (e.KeyCode == Keys.N && e.Control)
            {
                cmdthemmoi.PerformClick();
                return;
            }
            if (e.KeyCode == Keys.S && e.Control)
            {
                cmdGhi.PerformClick();
                return;
            }
            if (e.KeyCode == Keys.U && e.Control)
            {
                cmdSua.PerformClick();
                return;
            }
        }

       
        private void cmdIn_Click_1(object sender, EventArgs e)
        {

        }

        private void ctxMore_Opening(object sender, CancelEventArgs e)
        {
            
        }
        void Phanbo()
        {
           
        }

        void _PhanbotientheoPTTT__OnChangePTTT(long id_thanhtoan, string ma_pttt, string ten_pttt, string ma_nganhang, string ten_nganhang)
        {
           
        }

        private void cmdxoa_Click_1(object sender, EventArgs e)
        {

        }

       
    }
}
