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
    public partial class ucMiengiamkhac : UserControl
    {
        public delegate void OnChangedData();

        public event OnChangedData _OnChangedData;
        byte noitru = 0;
        action m_enAct = action.FirstOrFinished;
        bool AllowedChanged = false;
        bool AllowedChanged_maskedEdit = false;
        KcbChietkhau objMG = null;
        private DataTable m_dtTimKiembenhNhan = new DataTable();
        public TrangthaiNoitru _TrangthaiNoitru = TrangthaiNoitru.NoiTru;
        public bool callfromMenu = true;
        public KcbLuotkham objLuotkham = null;
        public ucMiengiamkhac()
        {
            InitializeComponent();
            InitEvents();
            
        }
        public void ChangePatients(KcbLuotkham objLuotkham, string malydochietkhau, string Tongtien, string TongMG,byte noitru)
        {
            lblMsg.Text = "";
            lblTongtien.Text = "";
            this.noitru = noitru;
            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_MIENGIAM_HIENTHITATCA", "0", true) == "1") this.noitru = 10;
            AllowedChanged = false;
            this.objLuotkham = objLuotkham;
            if (malydochietkhau != string.Empty)
                txtLydo.LOAI_DANHMUC = malydochietkhau;
            txtTongtien.Text = Tongtien;
            txtDaMG.Text = TongMG;
            Init();
        }
       
        void InitEvents()
        {
            
            grdMG.SelectionChanged += new EventHandler(grdTamung_SelectionChanged);
            grdMG.KeyDown += new KeyEventHandler(grdTamung_KeyDown);
            
            txtLydo._OnShowData+=txtLydo__OnShowData;
            txtLydo._OnSaveAs+=txtLydo__OnSaveAs;

            cmdthemmoi.Click += new EventHandler(cmdthemmoi_Click);
            cmdSua.Click += new EventHandler(cmdSua_Click);
            cmdxoa.Click += new EventHandler(cmdxoa_Click);
            cmdIn.Click += new EventHandler(cmdIn_Click);
            cmdHuy.Click += new EventHandler(cmdHuy_Click);
            cmdGhi.Click += new EventHandler(cmdGhi_Click);
            chkSaveAndPrint.CheckedChanged += chkSaveAndPrint_CheckedChanged;
            chkPrintPreview.CheckedChanged += chkPrintPreview_CheckedChanged;
            //foreach (Control ctrl in panel2.Controls)
            //    ctrl.KeyDown += ctrl_KeyDown;
            txtPttt._OnEnterMe += txtPttt__OnEnterMe;
            autoNguonkiqui._OnEnterMe += autoNguonkiqui__OnEnterMe;
            txtPttt._OnShowData += txtPttt__OnShowData;
            autoNguonkiqui._OnShowData += autoNguonkiqui__OnShowData;
            autoNganhang._OnShowData += autoNganhang__OnShowData;
        }

       

        void autoNganhang__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(autoNganhang.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = autoNganhang.myCode;
                autoNganhang.Init();
                autoNganhang.SetCode(oldCode);
                autoNganhang.Focus();
            } 
        }

        void autoNguonkiqui__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(autoNguonkiqui.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = autoNguonkiqui.myCode;
                autoNguonkiqui.Init();
                autoNguonkiqui.SetCode(oldCode);
                autoNguonkiqui.Focus();
            } 
        }

        void txtPttt__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtPttt.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtPttt.myCode;
                txtPttt.Init();
                txtPttt.SetCode(oldCode);
                txtPttt.Focus();
            } 
        }

        void autoNguonkiqui__OnEnterMe()
        {
            try
            {
                List<string> lstqhe = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_TAMUNG_CAP_NGUON_PTTT", true).Split('-').ToList<string>();
                if (lstqhe.Count == 2)
                {
                    List<string> lstNguon = lstqhe[0].Split(',').ToList<string>();
                    List<string> lstPttt = lstqhe[1].Split(',').ToList<string>();
                    int idx = lstNguon.IndexOf(autoNguonkiqui.MyCode);
                    if (idx < 0) return;
                    string ma_pttt = lstPttt[idx];
                    txtPttt.SetCode(ma_pttt);
                    txtPttt.RaiseEnterEvents();
                    if (!autoNganhang.Enabled) autoNganhang.SetCode("-1");
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException("THANHTOAN_TAMUNG_CAP_NGUON_PTTT",ex);
            }
            
        }
        void txtPttt__OnEnterMe()
        {
            List<string> lstPTTT = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_CHONNGANHANG", false).Split(',').ToList<string>();
            autoNganhang.Enabled = lstPTTT.Contains(txtPttt.MyCode);
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

       

        void chkPrintPreview_CheckedChanged(object sender, EventArgs e)
        {
            PropertyLib._MayInProperties.PreviewPhieuTamung = chkPrintPreview.Checked;
            PropertyLib.SaveProperty(PropertyLib._MayInProperties);
        }

        void chkSaveAndPrint_CheckedChanged(object sender, EventArgs e)
        {
            PropertyLib._NoitruProperties.InsaukhiLuu = chkSaveAndPrint.Checked;
            PropertyLib.SaveProperty(PropertyLib._NoitruProperties);
        }

        void txtLydo__OnSaveAs()
        {
            if (Utility.DoTrim(txtLydo.Text) == "") return;
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtLydo.LOAI_DANHMUC);
            _DMUC_DCHUNG.SetStatus(true, txtLydo.Text);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtLydo.myCode;
                txtLydo.Init();
                txtLydo.SetCode(oldCode);
                txtLydo.Focus();
            }
        }

        void txtLydo__OnShowData()
        {
            DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(txtLydo.LOAI_DANHMUC);
            _DMUC_DCHUNG.ShowDialog();
            if (!_DMUC_DCHUNG.m_blnCancel)
            {
                string oldCode = txtLydo.myCode;
                txtLydo.Init();
                txtLydo.SetCode(oldCode);
                txtLydo.Focus();
            }
        }

        void grdTamung_KeyDown(object sender, KeyEventArgs e)
        {
            if (Utility.isValidGrid(grdMG) && e.KeyCode == Keys.Delete) cmdxoa.PerformClick();
        }

        void txtSotien__OnTextChanged(string text)
        {
            if (AllowedChanged_maskedEdit)
                Utility.SetMsg(lblMsg, text, false);
        }
        void cmdGhi_Click(object sender, EventArgs e)
        {
            if (!isValidData()) return;
            try
            {
                if (m_enAct == action.Insert)
                {
                    KcbChietkhau newck = new KcbChietkhau();
                    newck.IdBenhnhan = objLuotkham.IdBenhnhan;
                    newck.MaLuotkham = objLuotkham.MaLuotkham;
                    newck.IdThanhtoan = -1;
                    newck.SoTien = Utility.DecimaltoDbnull(txtTienChietkhau.Text, 0);
                    newck.NoiTru = noitru;
                    newck.TrangThai = true;
                    newck.IdChitietThanhtoan = -1;
                    newck.MaLydoChietkhau = txtLydo.myCode;
                    newck.KieuChietkhau = "T";
                    newck.TileChietkhau = 0;
                    newck.MaUudai = "-1";
                    newck.BoChitiet = true;
                    newck.NguoiKi = txtNguoiki.MyCode;
                    newck.NgayMiengiam = dtpNgaythu.Value;
                    newck.NguoiTao = globalVariables.UserName;
                    newck.NgayTao = DateTime.Now;
                    newck.IsNew = true;
                    newck.MaCoso = objLuotkham.MaCoso;
                    newck.Save();


                    DataRow newDr = m_dtMG.NewRow();
                    Utility.FromObjectToDatarow(objMG, ref newDr);
                    newDr["ngay_miengiam"] = dtpNgaythu.Value;
                    newDr["sngay_miengiam"] = dtpNgaythu.Value.ToString("dd/MM/yyyy");
                    newDr["ten_nguoithuchien"] = txtNguoiki.Text;
                    newDr["so_tien"] = newck.SoTien;
                    newDr[KcbChietkhau.Columns.MaLydoChietkhau] = txtLydo.myCode;
                    newDr["nguoi_ki"] = txtNguoiki.MyCode;
                    newDr["ten_lydochietkhau"] = txtLydo.Text;
                    newDr["ten_nguoitao"] = globalVariables.gv_strTenNhanvien;
                    newDr["ngay_tao"] = DateTime.Now;
                    newDr["nguoi_tao"] = globalVariables.UserName;
                    m_dtMG.Rows.Add(newDr);
                    m_dtMG.AcceptChanges();
                    Utility.GotoNewRowJanus(grdMG, KcbChietkhau.Columns.IdChietkhau, newck.IdChietkhau.ToString());
                    if (chkSaveAndPrint.Checked)
                        cmdIn_Click(cmdIn, e);
                    m_enAct = action.FirstOrFinished;

                }
                else
                {
                    objMG.SoTien = Utility.DecimaltoDbnull(txtTienChietkhau.Text, 0);
                    objMG.NguoiKi = txtNguoiki.MyCode;
                    objMG.MaLydoChietkhau = txtLydo.myCode;
                    objMG.NgayMiengiam = dtpNgaythu.Value;
                    objMG.NguoiSua = globalVariables.UserName;
                    objMG.NgaySua = DateTime.Now;
                    objMG.IsNew = false;
                    objMG.MarkOld();
                    objMG.Save();


                    DataRow _myDr = ((DataRowView)grdMG.CurrentRow.DataRow).Row;
                    _myDr["so_tien"] = objMG.SoTien;
                    _myDr["ngay_miengiam"] = dtpNgaythu.Value;
                    _myDr["sngay_miengiam"] = dtpNgaythu.Value.ToString("dd/MM/yyyy");
                    _myDr["ten_nguoithuchien"] = txtNguoiki.Text;
                    _myDr["nguoi_ki"] = objMG.NguoiKi;
                    _myDr[KcbChietkhau.Columns.MaLydoChietkhau] = txtLydo.myCode;
                    _myDr["ten_lydochietkhau"] = txtLydo.Text;
                    _myDr["ten_nguoisua"] = globalVariables.gv_strTenNhanvien;
                    _myDr["ngay_sua"] = DateTime.Now;
                    _myDr["nguoi_sua"] = globalVariables.UserName;

                    m_dtMG.AcceptChanges();
                    m_enAct = action.FirstOrFinished;
                }                
               
                
                if (_OnChangedData != null) _OnChangedData();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                setTongtienStatus();
                SetControlStatus();
               
            }


        }
        bool isValidData()
        {
            Utility.SetMsg(lblMsg, "", true);
            objLuotkham = Utility.getKcbLuotkham(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham);
            if (objLuotkham == null)
            {
                Utility.SetMsg(lblMsg, "Bạn cần chọn Bệnh nhân cần miễn giảm", true);
                return false;
            }
            //Kiểm tra miễn giảm ngoại trú
            //if (objLuotkham.TrangthaiNoitru<=0 && THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_KICHHOAT_TAMUNG_NGOAITRU",true)=="1")
            //{
            //    return true;
            //}
            if (objLuotkham.TrangthaiNoitru >=6)
            {
                Utility.ShowMsg("Bệnh nhân đã thanh toán ra viện nên bạn không thể nộp thêm tiền miễn giảm");
                return false;
            }
            //if (!THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb) && !new noitru_TamungHoanung().DathanhtoanhetNgoaitru(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham))
            //{
            //    Utility.SetMsg(lblMsg, "Bệnh nhân Dịch vụ chưa thanh toán hết tiền ngoại trú", true);
            //    return false;
            //}
            if (Utility.DecimaltoDbnull(txtTienChietkhau.Text) <= 0)
            {
                Utility.SetMsg(lblMsg, "Bạn cần nhập số tiền miễn giảm >0 ", true);
                txtTienChietkhau.Focus();
                return false;
            }
            if (Utility.DoTrim(txtLydo.Text) == "")
            {
                Utility.SetMsg(lblMsg, "Bạn cần nhập lý do thu tiền miễn giảm ", true);
                txtLydo.SelectAll();
                txtLydo.Focus();
                return false;
            }
            if (txtNguoiki.MyID.ToString() == "-1")
            {
                Utility.SetMsg(lblMsg, "Bạn cần nhập tên người kí quyết định miễn giảm(Có thể xóa trắng và nhập phím cách để ra tất cả các nhân viên trong hệ thống)", true);
                txtNguoiki.SelectAll();
                txtNguoiki.Focus();
                return false;
            }
            
            return true;
        }
        void cmdHuy_Click(object sender, EventArgs e)
        {
            m_enAct = action.FirstOrFinished;
            SetControlStatus();
        }

        void cmdIn_Click(object sender, EventArgs e)
        {
            try
            {
                if (objLuotkham==null)
                {
                    Utility.ShowMsg("Bạn cần chọn bệnh nhân in phiếu miễn giảm");
                    return;
                }
                if (grdMG.GetDataRows().Count() <= 0)
                {
                    Utility.ShowMsg("Bạn cần chọn phiếu miễn giảm muốn in");
                    return;
                }
                if (!Utility.isValidGrid(grdMG))
                {

                    grdMG.MoveFirst();

                }
                DataTable m_dtReport = mien_giam.Inphieumiengiam(Utility.Int64Dbnull(Utility.GetValueFromGridColumn(grdMG, KcbChietkhau.Columns.IdChietkhau), -1));
                THU_VIEN_CHUNG.CreateXML(m_dtReport, "kcb_phieumiengiam.xml");
                if (m_dtReport.Rows.Count <= 0)
                {
                    Utility.ShowMsg("Không tìm thấy dữ liệu", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
                string tieude = "", reportname = "";
                var crpt = Utility.GetReport("kcb_phieumiengiam", ref tieude, ref reportname);
                if (crpt == null) return;

                MoneyByLetter _moneyByLetter = new MoneyByLetter();
                var objForm = new frmPrintPreview(tieude, crpt, true, m_dtReport.Rows.Count <= 0 ? false : true);
                Utility.UpdateLogotoDatatable(ref m_dtReport);

                crpt.SetDataSource(m_dtReport);


                objForm.mv_sReportFileName = Path.GetFileName(reportname);
                objForm.mv_sReportCode = "kcb_phieumiengiam";
                Utility.SetParameterValue(crpt, "ParentBranchName", globalVariables.ParentBranch_Name);
                Utility.SetParameterValue(crpt, "BranchName", globalVariables.Branch_Name);
                Utility.SetParameterValue(crpt, "Address", globalVariables.Branch_Address);
                Utility.SetParameterValue(crpt, "TelePhone", globalVariables.Branch_Phone);
                Utility.SetParameterValue(crpt, "sMoneyLetter", _moneyByLetter.sMoneyToLetter(Utility.Int32Dbnull(m_dtReport.Compute("SUM(so_tien)", "1=1"), 0).ToString()));
                Utility.SetParameterValue(crpt, "sCurrentDate", Utility.FormatDateTimeWithThanhPho(DateTime.Now));
                Utility.SetParameterValue(crpt, "sTitleReport", tieude);
                Utility.SetParameterValue(crpt, "BottomCondition", THU_VIEN_CHUNG.BottomCondition());
                objForm.crptViewer.ReportSource = crpt;

                if (Utility.isPrintPreview(PropertyLib._MayInProperties.TenMayInBienlai, PropertyLib._MayInProperties.PreviewPhieuTamung))
                {
                    objForm.SetDefaultPrinter(PropertyLib._MayInProperties.TenMayInBienlai, 0);
                    objForm.ShowDialog();
                }
                else
                {
                    objForm.addTrinhKy_OnFormLoad();
                    crpt.PrintOptions.PrinterName = PropertyLib._MayInProperties.TenMayInBienlai;
                    crpt.PrintToPrinter(objForm.getPrintNumber, false, 0, 0);
                }


            }
            catch (Exception)
            {
            }
        }

        void cmdxoa_Click(object sender, EventArgs e)
        {
            Xoatamung();
            m_enAct = action.FirstOrFinished;
            SetControlStatus();
        }
        void Xoatamung()
        {
            if (!isValidXoatamung()) return;
            try
            {
                if (Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa dòng miễn giảm đang chọn này hay không?", "Xác nhận", true))
                {
                    if (objMG != null)
                    {
                        frm_Chondanhmucdungchung _Chondanhmucdungchung = new frm_Chondanhmucdungchung(txtLydo.LOAI_DANHMUC, "Hủy tiền miễn giảm", "Chọn lý do hủy tiền miễn giảm...", "Lý do hủy",false);
                        _Chondanhmucdungchung.ShowDialog();
                        if (!_Chondanhmucdungchung.m_blnCancel)
                        {
                            if (noitru_TamungHoanung.XoaTienMG(objMG, _Chondanhmucdungchung.ten))
                            {
                                Utility.SetMsg(lblMsg, string.Format("Xóa miễn giảm {0} thành công", txtTienChietkhau.Text), false);
                                DataRow drDelete = Utility.getCurrentDataRow(grdMG);
                                if (drDelete != null)
                                {
                                    m_dtMG.Rows.Remove(drDelete);
                                    m_dtMG.AcceptChanges();
                                }
                            }
                        }
                        SetControlStatus();
                    }
                    else
                    {
                        Utility.SetMsg(lblMsg, string.Format("Bạn cần chọn dòng miễn giảm cần xóa"), true);
                    }
                }
                if (_OnChangedData != null) _OnChangedData();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                setTongtienStatus();
            }
        }
        bool isValidXoatamung()
        {
            if (!Utility.isValidGrid(grdMG))
            {
                Utility.SetMsg(lblMsg, "Bạn cần chọn một dòng miễn giảm trên lưới để thực hiện xóa", true);
                return false;
            }
            objLuotkham = Utility.getKcbLuotkham(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham);
            if (objLuotkham == null)
            {
                Utility.SetMsg(lblMsg, "Bạn cần chọn Bệnh nhân cần miễn giảm", true);
                return false;
            }
            if (Utility.Int64Dbnull(grdMG.GetValue("id_thanhtoan"), -1) > 0 || Utility.Int64Dbnull(grdMG.GetValue("id_chitiet_thanhtoan"), -1) > 0)
            {
                Utility.ShowMsg("Miễn giảm bạn chọn gắn với thanh toán nên cần phải hủy bản ghi thanh toán trước khi hủy miễn giảm");
                return false;
            }
            if (Utility.Byte2Bool( objLuotkham.Noitru) && objLuotkham.TrangthaiNoitru == 6)
            {
                Utility.ShowMsg("Bệnh nhân đã được thanh toán ra viện nên bạn không thể hủy tiền miễn giảm được");
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
                Utility.ShowMsg("Bạn cần chọn bệnh nhân cần nộp tiền miễn giảm");
                return;
            }
            if (Utility.Byte2Bool(objLuotkham.TthaiThopNoitru) && objLuotkham.TrangthaiNoitru == 6)
            {
                Utility.ShowMsg("Bệnh nhân đã hoàn ứng và ra viện nên bạn không thể nộp thêm tiền miễn giảm");
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
                grdMG.Enabled = false;
                AllowedChanged = false;
                switch (m_enAct)
                {
                    case action.Insert:
                        //Cho phép nhập liệu mã loại đối tượng,vị trí, tên loại đối tượng và mô tả thêm
                        Utility.DisabledTextBox(txtID);
                        dtpNgaythu.Enabled = true;
                        txtPtramCK.Enabled = true;
                        txtTienChietkhau.Enabled = true;
                        txtLydo.Enabled = true;
                        txtNguoiki.Enabled = true;
                        objMG = null;
                        dtpNgaythu.Value = globalVariables.SysDate;
                        txtTienChietkhau.Text = "0";
                        txtPtramCK.Text = "0";
                        txtLydo.SetCode("-1");
                        txtNguoiki.SetCode("-1");

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
                        txtID.Text = "Tự sinh";
                        cmdHuy.Text = "Hủy";
                        txtPtramCK.Focus();
                        break;
                    case action.Update:
                        //Không cho phép cập nhật lại mã loại đối tượng
                        Utility.DisabledTextBox(txtID);
                        //Cho phép cập nhật lại vị trí, tên loại đối tượng và mô tả thêm
                        dtpNgaythu.Enabled = true;
                        txtLydo.Enabled = true;
                        txtNguoiki.Enabled = true;
                        txtPtramCK.Enabled = true;
                        txtTienChietkhau.Enabled = true;
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
                        txtPtramCK.Focus();
                        break;
                    case action.FirstOrFinished://Hủy hoặc trạng thái ban đầu khi mới hiển thị Form
                        AllowedChanged_maskedEdit = false;
                        grdMG.Enabled = true;
                        AllowedChanged = true;
                        //Không cho phép nhập liệu mã loại đối tượng, tên loại đối tượng và mô tả thêm
                        Utility.DisabledTextBox(txtID);
                        dtpNgaythu.Enabled = false;
                        txtLydo.Enabled = false;
                        txtNguoiki.Enabled = false;
                        txtPtramCK.Enabled = false;
                        txtTienChietkhau.Enabled = false;
                        autoNguonkiqui.Enabled = false;
                        txtPttt.Enabled = false;
                        autoNganhang.Enabled = false;

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
                        grdTamung_SelectionChanged(grdMG, new EventArgs());
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
                grdMG.Enabled = true;
                setTongtienStatus();
            }

        }
        void grdTamung_SelectionChanged(object sender, EventArgs e)
        {
            if (!AllowedChanged) return;
            FillData();
        }
        void FillData()
        {
            try
            {
                if (!Utility.isValidGrid(grdMG))
                {

                    objMG = null;
                    dtpNgaythu.Value = globalVariables.SysDate;
                    txtTienChietkhau.Text = "0";
                    txtPtramCK.Text = "0";
                    txtLydo.SetCode("-1");
                    txtNguoiki.SetCode("-1");
                    autoNganhang.SetCode("-1");
                    txtPttt.SetCode("-1");
                    autoNguonkiqui.SetCode("-1");
                }
                else
                {

                    objMG = KcbChietkhau.FetchByID(Utility.Int32Dbnull(grdMG.GetValue(KcbChietkhau.Columns.IdChietkhau)));

                    if (objMG == null)
                    {
                        dtpNgaythu.Value = globalVariables.SysDate;
                        txtTienChietkhau.Text = "0";
                        txtPtramCK.Text = "0";
                        txtLydo.SetCode("-1");
                        txtNguoiki.SetCode("-1");
                        autoNganhang.SetCode("-1");
                        txtPttt.SetCode("-1");
                        autoNguonkiqui.SetCode("-1");
                    }
                    else
                    {
                        objMG.IsNew = false;
                        objMG.MarkOld();
                        dtpNgaythu.Value = objMG.NgayMiengiam;
                        txtTienChietkhau.Text = objMG.SoTien.ToString();
                        txtLydo.SetCode(objMG.MaLydoChietkhau);
                        txtNguoiki.SetCode(objMG.NguoiKi);

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
            LoadConfig();
            LaydanhsachMG();
            AutoCompleteTextBox();
            
            m_enAct = action.FirstOrFinished;
            SetControlStatus();
            ModifyCommand();

        }
        
        private DataTable m_dtKhoaNoiTru = new DataTable();
      
        void AutoCompleteTextBox()
        {
            txtLydo.Init();
            AutoCompleteTextBox_nguoithu();
            //txtNguoiki.SetId(globalVariables.gv_intIDNhanvien);

        }
        void AutoCompleteTextBox_nguoithu()
        {
            DataTable m_dtNhanvien = SPs.DmucLaydanhsachNhanvien("LANHDAO").GetDataSet().Tables[0];
            txtNguoiki.Init(m_dtNhanvien, new List<string>() { DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.MaNhanvien, DmucNhanvien.Columns.TenNhanvien });
        }
       
        private void ModifyCommand()
        {
            bool isValid = objLuotkham != null;
            bool isValid2 = Utility.isValidGrid(grdMG);
            cmdSua.Enabled = isValid && isValid2;
            cmdxoa.Enabled = isValid && isValid2;
            cmdIn.Enabled = isValid && isValid2;
            cmdthemmoi.Enabled = isValid;
            cmdGhi.Enabled = m_enAct != action.FirstOrFinished;
            cmdHuy.Enabled = cmdGhi.Enabled;
        }
        public void Refresh()
        {
            LaydanhsachMG();
            setTongtienStatus();
        }
        public DataTable m_dtMG = null;
         void setTongtienStatus()
        {
            try
            {
                if (m_dtMG == null) return;
                lblTongtien.Text = "Tổng tiền: " + new MoneyByLetter().sMoneyToLetter(Utility.sDbnull(m_dtMG.Compute("SUM(so_tien)", "1=1"), "0"));
                txtDaMG.Text = m_dtMG.Compute("sum(so_tien)", "1=1").ToString();
            }
            catch (Exception ex)
            {

            }

        }
         void LaydanhsachMG()
        {
            try
            {
                if (objLuotkham == null)
                {
                    grdMG.DataSource = null;
                    return;
                }
                m_dtMG = new KCB_THAMKHAM().KcbTimkiemthongtinMiengiam(objLuotkham.MaLuotkham,
                  Utility.Int32Dbnull(objLuotkham.IdBenhnhan, 0),noitru);
                txtDaMG.Text = m_dtMG.Compute("sum(so_tien)", "1=1").ToString();
                Utility.SetDataSourceForDataGridEx_Basic(grdMG, m_dtMG, false, true, "1=1", KcbChietkhau.Columns.NgayMiengiam + " desc");
                grdMG.MoveFirst();
                if (grdMG.GetDataRows().Length <= 0)
                {
                    objMG = null;
                    dtpNgaythu.Value = globalVariables.SysDate;
                    txtLydo.SetCode("-1");
                    txtNguoiki.SetId(-1);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                AllowedChanged = true;
                setTongtienStatus();
                ShowLSuTamung();
            }
        }
        void ShowLSuTamung()
        {
            if (objLuotkham != null)
            {
                grdMG.Width = 0;
            }
            else
            {
                grdMG.Width = 425;
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

        void LoadConfig()
        {
            chkPrintPreview.Checked = PropertyLib._MayInProperties.PreviewPhieuTamung;
            chkSaveAndPrint.Checked = PropertyLib._NoitruProperties.InsaukhiLuu;
        }

        private void cmdConfig_Click(object sender, EventArgs e)
        {
            frm_Properties _Properties = new frm_Properties(PropertyLib._NoitruProperties);
            _Properties.ShowDialog();
            LoadConfig();
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

        private void txtPtramCK_ValueChanged(object sender, EventArgs e)
        {
            txtTienChietkhau.Text = Utility.sDbnull(Utility.DecimaltoDbnull(txtTongtien.Text) *
                                                    Utility.DecimaltoDbnull(txtPtramCK.Text) / 100);

            
        }

        private void cmdRefresh_Click(object sender, EventArgs e)
        {
            string oldcode = txtLydo.myCode;
            string oldcode2 = txtNguoiki.MyCode;
            AutoCompleteTextBox();
            txtLydo.SetCode(oldcode);
            txtNguoiki.SetCode(oldcode);
        }

        private void cmdxoa_Click_1(object sender, EventArgs e)
        {

        }

        private void chkMiengiamkhac_CheckedChanged(object sender, EventArgs e)
        {
            m_dtMG.DefaultView.RowFilter=!chkMiengiamkhac.Checked?"1=1":"id_thanhtoan=-1 ";
            m_dtMG.AcceptChanges();
        }
    }
}
