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
using System.Transactions;
using VNS.HIS.UI.THANHTOAN;
using VNS.HIS.Classes;
namespace VNS.HIS.UCs.Noitru
{
    public partial class ucThuchikhac : UserControl
    {
        public delegate void OnChangedData();

        public event OnChangedData _OnChangedData;

        action m_enAct = action.FirstOrFinished;
        bool AllowedChanged = false;
        bool AllowedChanged_maskedEdit = false;
        KcbPhieuthu objPhieuthuchi = null;
        private DataTable m_dtTimKiembenhNhan = new DataTable();
        public TrangthaiNoitru _TrangthaiNoitru = TrangthaiNoitru.NoiTru;
        public bool callfromMenu = true;
        public KcbLuotkham objLuotkham = null;
        public byte v_bytNoitru = 0;
        public ucThuchikhac()
        {
            InitializeComponent();
            InitEvents();
            
        }
        bool hasLoadedDmuc = false;
        DataTable dtPttt = new DataTable();
        DataTable dtPttt_rieng = new DataTable();
        DataTable dtNganhang = new DataTable();
        void LoadPtttNganhang()
        {
            if (hasLoadedDmuc) return;
            DataTable dtData = THU_VIEN_CHUNG.LayDulieuDanhmucChung(new List<string>() { "PHUONGTHUCTHANHTOAN", "NGANHANG" }, true);
            dtPttt = THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, "PHUONGTHUCTHANHTOAN");
            dtNganhang = THU_VIEN_CHUNG.LayDulieuDanhmucChung(dtData, "NGANHANG");
            cboPttt.DataSource = dtPttt;
            cboPttt.ValueMember = DmucChung.Columns.Ma;
            cboPttt.DisplayMember = DmucChung.Columns.Ten;
            cboNganhang.DataSource = dtNganhang;
            cboNganhang.ValueMember = DmucChung.Columns.Ma;
            cboNganhang.DisplayMember = DmucChung.Columns.Ten;

            cboPttt.SelectedValue = THU_VIEN_CHUNG.Laygiatrimacdinh(dtPttt);
            cboNganhang.SelectedValue = THU_VIEN_CHUNG.Laygiatrimacdinh(dtNganhang);
            grdThuchi.ColumnButtonClick += grdThuchi_ColumnButtonClick;
        }

        void grdThuchi_ColumnButtonClick(object sender, Janus.Windows.GridEX.ColumnActionEventArgs e)
        {
            if (e.Column.Key == "PHANBO")
            {
                if (!Utility.Coquyen("THANHTOAN_QUYEN_PHANBOPTTT"))
                {
                    Utility.ShowMsg("Bạn không có quyền phân bổ hình thức thanh toán(THANHTOAN_QUYEN_PHANBOPTTT). Vui lòng liên hệ quản trị để được cấp quyền");
                    return;
                }
                Phanbo();
            }
        }

        public void ChangePatients(KcbLuotkham objLuotkham,string maloailydothu)
        {
            AllowedChanged = false;
            this.objLuotkham = objLuotkham;
            if (maloailydothu != string.Empty)
                txtLydo.LOAI_DANHMUC = maloailydothu;
            Init();
        }
       
        void InitEvents()
        {
            
            grdThuchi.SelectionChanged += new EventHandler(grdTamung_SelectionChanged);
            grdThuchi.KeyDown += new KeyEventHandler(grdTamung_KeyDown);
            txtSotien._OnTextChanged += new MaskedTextBox.MaskedTextBox.OnTextChanged(txtSotien__OnTextChanged);
            
            txtLydo._OnShowData+=txtLydo__OnShowData;
            txtLydo._OnSaveAs+=txtLydo__OnSaveAs;

            cmdthemmoi.Click += new EventHandler(cmdthemmoi_Click);
            cmdSua.Click += new EventHandler(cmdSua_Click);
            cmdxoa.Click += new EventHandler(cmdxoa_Click);
            cmdIn.Click += new EventHandler(cmdIn_Click);
            cmdHuy.Click += new EventHandler(cmdHuy_Click);
            cmdGhi.Click += new EventHandler(cmdGhi_Click);
            cmdConfig.Click += cmdConfig_Click;
            chkSaveAndPrint.CheckedChanged += chkSaveAndPrint_CheckedChanged;
            chkPrintPreview.CheckedChanged += chkPrintPreview_CheckedChanged;
            //foreach(Control ctrl in panel2.Controls)
            //    ctrl.KeyDown += ctrl_KeyDown;
            //cboPttt._OnEnterMe += cboPttt__OnEnterMe;
            autoNguonkiqui._OnEnterMe += autoNguonkiqui__OnEnterMe;
            //cboPttt._OnShowData += cboPttt__OnShowData;
            autoNguonkiqui._OnShowData += autoNguonkiqui__OnShowData;
            //cboNganhang._OnShowData += cboNganhang__OnShowData;
            optPhieuThu.CheckedChanged+=optPhieuThu_CheckedChanged;
            optPhieuChi.CheckedChanged += optPhieuThu_CheckedChanged;
        }

        void cboNganhang__OnShowData()
        {
            //DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(cboNganhang.LOAI_DANHMUC);
            //_DMUC_DCHUNG.ShowDialog();
            //if (!_DMUC_DCHUNG.m_blnCancel)
            //{
            //    string oldCode = Utility.sDbnull(cboNganhang.SelectedValue, "-1");
            //    cboNganhang.Init();
            //    cboNganhang.SetCode(oldCode);
            //    cboNganhang.Focus();
            //} 
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

        void cboPttt__OnShowData()
        {
            //DMUC_DCHUNG _DMUC_DCHUNG = new DMUC_DCHUNG(cboPttt.LOAI_DANHMUC);
            //_DMUC_DCHUNG.ShowDialog();
            //if (!_DMUC_DCHUNG.m_blnCancel)
            //{
            //    string oldCode = Utility.sDbnull(cboPttt.SelectedValue, "-1");
            //    cboPttt.Init();
            //    cboPttt.SetCode(oldCode);
            //    cboPttt.Focus();
            //} 
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
                    cboPttt.SelectedValue = ma_pttt;// SetCode(ma_pttt);
                    cboPttt_SelectedIndexChanged(cboPttt, new EventArgs());
                    if (!cboNganhang.Enabled) cboNganhang.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {

                Utility.CatchException("THANHTOAN_TAMUNG_CAP_NGUON_PTTT",ex);
            }
            
        }
        void cboPttt__OnEnterMe()
        {
            //List<string> lstPTTT = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_CHONNGANHANG", false).Split(',').ToList<string>();
            //cboNganhang.Enabled = lstPTTT.Contains(Utility.sDbnull(cboPttt.SelectedValue, "-1"));
        }
        public void EnterNextControl(Control currentCtr)
        {
            SelectNextControl(currentCtr, true, true, true, true);
        }
        void ctrl_KeyDown(object sender, KeyEventArgs e)
        {
            Control _ctrl = sender as Control;
            if (e.KeyCode == Keys.Enter)
            {
                if ((this.ActiveControl != null && this.ActiveControl.Name == this.cboPttt.Name && !this.cboNganhang.Enabled))
                {
                    this.txtMotathem.Focus();
                }
                else
                    SendKeys.Send("{TAB}");
                //SelectNextControl(_ctrl, true, true, true, true);
            }
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
            if (Utility.isValidGrid(grdThuchi) && e.KeyCode == Keys.Delete) cmdxoa.PerformClick();
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

                    objPhieuthuchi = new KcbPhieuthu();
                    objPhieuthuchi.IdBenhnhan = objLuotkham.IdBenhnhan;
                    objPhieuthuchi.MaLuotkham = objLuotkham.MaLuotkham;
                    objPhieuthuchi.IdKhoanoitru = objLuotkham.IdKhoanoitru;
                    objPhieuthuchi.IdBuonggiuong = objLuotkham.IdRavien;
                    objPhieuthuchi.IdBuong = objLuotkham.IdBuong;
                    objPhieuthuchi.IdGiuong = objLuotkham.IdGiuong;
                    objPhieuthuchi.NoiTru = (byte)(objLuotkham.TrangthaiNoitru <= 0 ? 0 : 1);
                    objPhieuthuchi.LoaiPhieuthu = (byte)(optPhieuThu.Checked ? 0 : 1);
                    objPhieuthuchi.MaPhieuthu = THU_VIEN_CHUNG.GetMaPhieuThu(globalVariables.SysDate,Convert.ToInt32( objPhieuthuchi.LoaiPhieuthu));
                    objPhieuthuchi.LydoNop = txtLydo.Text;
                    objPhieuthuchi.NguoiNop = Utility.DoTrim(txtNguoithu.MyCode);
                    objPhieuthuchi.SoTien = Utility.DecimaltoDbnull(txtSotien.Text);
                    objPhieuthuchi.SotienGoc = objPhieuthuchi.SoTien;
                    objPhieuthuchi.NgayThuchien = dtpNgaythu.Value;
                    objPhieuthuchi.MaPttt = Utility.sDbnull(cboPttt.SelectedValue, "-1");
                    objPhieuthuchi.MaNganhang = cboNganhang.Enabled ? Utility.sDbnull(cboNganhang.SelectedValue, "-1"): "-1";
                    objPhieuthuchi.MaNguonTt = autoNguonkiqui.myCode;
                    objPhieuthuchi.IdKhoanoitru = objLuotkham.IdKhoanoitru;
                    objPhieuthuchi.IdBuonggiuong = objLuotkham.IdRavien;
                    objPhieuthuchi.IdBuong = objLuotkham.IdBuong;
                    objPhieuthuchi.IdGiuong = objLuotkham.IdGiuong;
                    objPhieuthuchi.IdNhanvien = Utility.Int16Dbnull(txtNguoithu.MyID);
                    objPhieuthuchi.NoiDung = txtMotathem.Text;
                    objPhieuthuchi.MaCoso = objLuotkham.MaCoso;
                    objPhieuthuchi.IsNew = true;
                    objPhieuthuchi.NguoiTao = globalVariables.UserName;
                    objPhieuthuchi.NgayTao = DateTime.Now;
                    using (var scope = new TransactionScope())
                    {
                        using (var dbscope = new SharedDbConnectionScope())
                        {
                            objPhieuthuchi.Save();
                            SPs.SpKcbThanhtoanPhanbotheoPTTTInsert(-1l, objPhieuthuchi.IdPhieuthu, -1l, objPhieuthuchi.MaPttt, objPhieuthuchi.MaNganhang,
                                    objPhieuthuchi.IdBenhnhan, objPhieuthuchi.MaLuotkham,
                                    objPhieuthuchi.NoiTru, objPhieuthuchi.SoTien, objPhieuthuchi.SotienGoc,
                                    objPhieuthuchi.NguoiTao, objPhieuthuchi.NgayTao, "", objPhieuthuchi.NgayTao,-1l,0,1).Execute();
                        }
                        scope.Complete();
                    }
                    DataRow newDr = m_dtPhieuthuchi.NewRow();
                    Utility.FromObjectToDatarow(objPhieuthuchi, ref newDr);
                    newDr["sngay_thuchien"] = dtpNgaythu.Value.ToString("dd/MM/yyyy");
                    newDr["ten_khoanoitru"] = "";
                    newDr["ten_nhanvien"] = txtNguoithu.Text;
                    newDr["ten_loaiphieu"] = optPhieuThu.Checked ? "Phiếu thu" : "Phiếu chi";
                    newDr[KcbPhieuthu.Columns.MaNganhang] = cboNganhang.Enabled ? Utility.sDbnull(cboNganhang.SelectedValue, "-1"): "-1";
                    newDr[KcbPhieuthu.Columns.MaNguonTt] = autoNguonkiqui.myCode;
                    newDr[KcbPhieuthu.Columns.MaPttt] = Utility.sDbnull(cboPttt.SelectedValue, "-1");
                    newDr["ten_nganhang"] = cboNganhang.Enabled ? cboNganhang.Text : "";
                    newDr["ten_nguon_tt"] = autoNguonkiqui.Text;
                    newDr["ten_pttt"] = cboPttt.Text;
                    newDr["noi_dung"] = txtMotathem.Text;
                    m_dtPhieuthuchi.Rows.Add(newDr);
                    m_dtPhieuthuchi.AcceptChanges();
                    Utility.GotoNewRowJanus(grdThuchi, KcbPhieuthu.Columns.IdPhieuthu, objPhieuthuchi.IdPhieuthu.ToString());
                    if (chkSaveAndPrint.Checked)
                        cmdIn_Click(cmdIn, e);
                    m_enAct = action.FirstOrFinished;

                }
                else
                {
                    string thongtincu = string.Format("LoaiPhieuthu={0}, số tiền ={1},Lý do nộp ={2}, Thu ngân viên ={3}", objPhieuthuchi.LoaiPhieuthu, objPhieuthuchi.SoTien, objPhieuthuchi.LydoNop, objPhieuthuchi.NguoiNop);
                    objPhieuthuchi.LoaiPhieuthu = (byte)(optPhieuThu.Checked ? 0 : 1);
                    string thongtinmoi = string.Format("LoaiPhieuthu={0}, số tiền ={1},Lý do nộp ={2}, Thu ngân viên ={3}", objPhieuthuchi.LoaiPhieuthu, objPhieuthuchi.SoTien, objPhieuthuchi.LydoNop, objPhieuthuchi.NguoiNop);
                    objPhieuthuchi.SoTien = Utility.DecimaltoDbnull(txtSotien.Text);
                    objPhieuthuchi.SotienGoc = objPhieuthuchi.SoTien;
                    objPhieuthuchi.NgayThuchien = dtpNgaythu.Value;
                    objPhieuthuchi.LydoNop = txtLydo.Text;
                    objPhieuthuchi.NguoiNop = Utility.DoTrim(txtNguoithu.MyCode);
                    objPhieuthuchi.IdNhanvien = Utility.Int16Dbnull(txtNguoithu.MyID);
                    objPhieuthuchi.MaPttt = Utility.sDbnull(cboPttt.SelectedValue, "-1");
                    objPhieuthuchi.MaNganhang = cboNganhang.Enabled ? Utility.sDbnull(cboNganhang.SelectedValue, "-1"): "-1";
                    objPhieuthuchi.MaNguonTt = autoNguonkiqui.myCode;
                    objPhieuthuchi.NoiDung = txtMotathem.Text;
                    objPhieuthuchi.NguoiSua = globalVariables.UserName;
                    objPhieuthuchi.NgaySua = DateTime.Now;
                    objPhieuthuchi.IsNew = false;
                    objPhieuthuchi.MarkOld();
                    using (var scope = new TransactionScope())
                    {
                        using (var dbscope = new SharedDbConnectionScope())
                        {
                            objPhieuthuchi.Save();
                            SPs.SpKcbThanhtoanPhanbotheoPTTTInsert(-1l, objPhieuthuchi.IdPhieuthu, -1l, objPhieuthuchi.MaPttt, objPhieuthuchi.MaNganhang,
                                    objPhieuthuchi.IdBenhnhan, objPhieuthuchi.MaLuotkham,
                                    objPhieuthuchi.NoiTru, objPhieuthuchi.SoTien, objPhieuthuchi.SotienGoc,
                                    objPhieuthuchi.NguoiTao, objPhieuthuchi.NgayTao, "", objPhieuthuchi.NgayTao,-1,0,1).Execute();
                            Utility.Log(Name, globalVariables.UserName, string.Format("Cập nhật thông tin phiếu thu chi khác. Thông tin cũ: {0} Thông tin mới {1}", thongtincu,thongtinmoi), newaction.Update, this.GetType().Assembly.ManifestModule.Name);

                        }
                        scope.Complete();
                    }

                    DataRow _myDr = ((DataRowView)grdThuchi.CurrentRow.DataRow).Row;
                    _myDr[KcbPhieuthu.Columns.SoTien] = Utility.DecimaltoDbnull(txtSotien.Text);
                    _myDr[KcbPhieuthu.Columns.NgayThuchien] = dtpNgaythu.Value;
                    _myDr[KcbPhieuthu.Columns.LydoNop] = txtLydo.Text;
                    _myDr[KcbPhieuthu.Columns.NguoiNop] = txtNguoithu.MyCode;
                    _myDr["ten_loaiphieu"] = optPhieuThu.Checked ? "Phiếu thu" : "Phiếu chi";
                    _myDr["sngay_thuchien"] = dtpNgaythu.Value.ToString("dd/MM/yyyy");
                    _myDr["ten_khoanoitru"] = "";
                    _myDr["ten_nhanvien"] = txtNguoithu.Text;
                    _myDr["noi_dung"] = txtMotathem.Text;
                    _myDr[KcbPhieuthu.Columns.MaNganhang] = Utility.sDbnull(cboNganhang.SelectedValue, "-1");
                    _myDr[KcbPhieuthu.Columns.MaNguonTt] = autoNguonkiqui.myCode;
                    _myDr[KcbPhieuthu.Columns.MaPttt] = Utility.sDbnull(cboPttt.SelectedValue, "-1");
                    _myDr["ten_nganhang"] = cboNganhang.Text;
                    _myDr["ten_nguon_tt"] = autoNguonkiqui.Text;
                    _myDr["ten_nguon_tt"] = autoNguonkiqui.Text;
                    _myDr["ten_loaiphieu"] = cboPttt.Text;

                    m_dtPhieuthuchi.AcceptChanges();
                    m_enAct = action.FirstOrFinished;

                }
                setTongtienStatus();
                SetControlStatus();
                if (_OnChangedData != null) _OnChangedData();
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }


        }
        bool isValidData()
        {
            Utility.SetMsg(lblMsg, "", true);
            string thuchi = optPhieuThu.Checked ? "thu" : "chi";
            objLuotkham = Utility.getKcbLuotkham(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham);
            if (objLuotkham == null)
            {
                Utility.SetMsg(lblMsg,string.Format( "Bạn cần chọn Bệnh nhân cần {0}",thuchi), true);
                return false;
            }
            //Kiểm tra tạm ứng ngoại trú
            //if (objLuotkham.TrangthaiNoitru<=0 && THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_KICHHOAT_TAMUNG_NGOAITRU",true)=="1")
            //{
            //    return true;
            //}
            if (Utility.Byte2Bool(objLuotkham.TthaiThopNoitru) && objLuotkham.TrangthaiNoitru == 5)
            {
                Utility.ShowMsg(string.Format("Bệnh nhân đã hoàn ứng và ra viện nên bạn không thể thực hiên lập phiếu {0}",thuchi));
                return false;
            }
            //if (!THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb) && !new noitru_TamungHoanung().DathanhtoanhetNgoaitru(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham))
            //{
            //    Utility.SetMsg(lblMsg, string.Format("Bệnh nhân Dịch vụ chưa thanh toán hết tiền ngoại trú"), true);
            //    return false;
            //}
            if (Utility.DecimaltoDbnull(txtSotien.Text) <= 0)
            {
                Utility.SetMsg(lblMsg, string.Format("Bạn cần nhập số tiền {0} >0 ",thuchi), true);
                txtSotien.SelectAll();
                txtSotien.Focus();
                return false;
            }
            if (Utility.DoTrim(txtLydo.Text) == "")
            {
                Utility.SetMsg(lblMsg, string.Format("Bạn cần nhập lý do {0}",thuchi), true);
                txtLydo.SelectAll();
                txtLydo.Focus();
                return false;
            }
            if (txtNguoithu.MyID.ToString() == "-1")
            {
                Utility.SetMsg(lblMsg, string.Format("Bạn cần nhập tên người {0} tiền (Có thể xóa trắng và nhập phím cách để ra tất cả các nhân viên trong hệ thống)",thuchi), true);
                txtNguoithu.SelectAll();
                txtNguoithu.Focus();
                return false;
            }
            if (Utility.sDbnull(cboPttt.SelectedValue, "-1") == "-1")
            {
                Utility.SetMsg(lblMsg, string.Format("Bạn phải chọn hình thức thanh toán"), true);
                cboPttt.Focus();
                return false;
            }
            if (cboNganhang.Enabled && Utility.sDbnull(cboNganhang.SelectedValue, "-1")== "-1")
            {
                Utility.SetMsg(lblMsg,string.Format( "Phương thức thanh toán {0} bắt buộc phải nhập thông tin ngân hàng. Mời bạn chọn ngân hàng",cboPttt.Text), true);
                cboNganhang.Focus();
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
                    Utility.ShowMsg("Bạn cần chọn bệnh nhân in phiếu ");
                    return;
                }
                if (grdThuchi.GetDataRows().Count() <= 0)
                {
                    Utility.ShowMsg("Bạn cần chọn phiếu muốn in");
                    return;
                }
                if (!Utility.isValidGrid(grdThuchi))
                {

                    grdThuchi.MoveFirst();

                }
                string loai_phieuthu = grdThuchi.GetValue("loai_phieuthu").ToString();
                long id_thanhtoan = Utility.Int64Dbnull(grdThuchi.GetValue("id_thanhtoan"),-1);
                if (id_thanhtoan > 0)
                {
                    if(loai_phieuthu=="1")
                        new INPHIEU_THANHTOAN_NGOAITRU().InPhieuchi(id_thanhtoan);
                    else
                    new INPHIEU_THANHTOAN_NGOAITRU().IN_HOADON(id_thanhtoan);
                }
                else
                    new INPHIEU_THANHTOAN_NGOAITRU().InPhieuthhuchikhac(Utility.Int32Dbnull(grdThuchi.CurrentRow.Cells[KcbPhieuthu.Columns.IdPhieuthu].Value, -1), loai_phieuthu == "0" ? "thanhtoan_phieuthukhac" : "thanhtoan_phieuchikhac");


            }
            catch (Exception)
            {
            }
        }

        void cmdxoa_Click(object sender, EventArgs e)
        {
            Xoaphieuthuchi();
            m_enAct = action.FirstOrFinished;
            SetControlStatus();
        }
        void Xoaphieuthuchi()
        {
            if (!isValidThuchi()) return;
            try
            {
                if (Utility.AcceptQuestion("Bạn có chắc chắn muốn xóa phiếu đang chọn này hay không?", "Xác nhận", true))
                {
                    if (objPhieuthuchi != null)
                    {
                        frm_Chondanhmucdungchung _Chondanhmucdungchung = new frm_Chondanhmucdungchung(grdThuchi.GetValue(KcbPhieuthu.Columns.LoaiPhieuthu).ToString() == "0" ? "LYDOHUYPHIEUTHUKHAC" : "LYDOHUYPHIEUCHIKHAC", "Hủy phiếu", "Chọn lý do hủy phiếu", "Lý do hủy",false);
                        _Chondanhmucdungchung.ShowDialog();
                        if (!_Chondanhmucdungchung.m_blnCancel)
                        {
                            if (noitru_TamungHoanung.XoaPhieuthuchikhac(objPhieuthuchi, grdThuchi.GetDataRows().Length - 1 > 0, _Chondanhmucdungchung.ten))
                            {
                                Utility.SetMsg(lblMsg, string.Format("Xóa phiếu thu {0} thành công", txtSotien.Text), false);
                                Utility.ShowMsg(lblMsg.Text);
                                DataRow drDelete = Utility.getCurrentDataRow(grdThuchi);
                                if (drDelete != null)
                                {
                                    m_dtPhieuthuchi.Rows.Remove(drDelete);
                                    m_dtPhieuthuchi.AcceptChanges();
                                }
                            }
                        }
                        SetControlStatus();
                    }
                    else
                    {
                        Utility.SetMsg(lblMsg, string.Format("Bạn cần chọn dòng tạm ứng cần xóa"), true);
                    }
                }
                if (_OnChangedData != null) _OnChangedData();
            }
            catch (Exception ex)
            {

            }
        }
        bool isValidThuchi()
        {
            if (!Utility.isValidGrid(grdThuchi))
            {
                Utility.SetMsg(lblMsg, "Bạn cần chọn phiếu cần xóa", true);
                return false;
            }
            if (Utility.Int64Dbnull(grdThuchi.GetValue("id_thanhtoan"), -1) >= 0)
            {
                Utility.ShowMsg("Phiếu bạn chọn là phiếu tạo từ quá trình thanh toán nên không thể sửa/xóa. Vui lòng chọn các phiếu thu - chi khác");
                return false;
            }
            if (Utility.Coquyen("quyen_suaphieuthuchikhac") ||
               Utility.sDbnull(grdThuchi.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.NguoiTao].Value, "") ==
               globalVariables.UserName)
            {
            }
            else
            {
                Utility.ShowMsg(string.Format("Phiếu thu/chi đang chọn sửa/xóa được tạo bởi {0} nên bạn không được phép xóa", Utility.sDbnull(grdThuchi.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.NguoiTao].Value, "")));
                return false;
            }
            objLuotkham = Utility.getKcbLuotkham(objLuotkham.IdBenhnhan, objLuotkham.MaLuotkham);
            if (objLuotkham == null)
            {
                Utility.SetMsg(lblMsg, "Bạn cần chọn Bệnh nhân cần hủy phiếu", true);
                return false;
            }
            
            if (objLuotkham.TrangthaiNoitru == 6)
            {
                Utility.ShowMsg("Bệnh nhân đã xuất viện nên bạn không thể hủy phiếu");
                return false;
            }
            if (objLuotkham.TrangthaiNoitru == 5)
            {
                Utility.ShowMsg("Bệnh nhân đã được tài chính duyệt nên bạn không thể hủy phiếu");
                return false;
            }
            //if (objLuotkham.TrangthaiNoitru == 4)
            //{
            //    Utility.ShowMsg("Bệnh nhân đã được tổng hợp xuất viện nên bạn không thể hủy phiếu");
            //    return false;
            //}
            if (objLuotkham.TthaiThanhtoannoitru >0)
            {
                Utility.ShowMsg("Bệnh nhân đã được thanh toán bạn không thể hủy phiếu được");
                return false;
            }
            return true;
        }
        void cmdSua_Click(object sender, EventArgs e)
        {
            if (Utility.Int64Dbnull(grdThuchi.GetValue("id_thanhtoan"), -1) >= 0)
            {
                Utility.ShowMsg("Phiếu bạn chọn là phiếu tạo từ quá trình thanh toán nên không thể sửa. Vui lòng chọn các phiếu thu - chi khác");
                return;
            }
            if (Utility.Coquyen("quyen_suaphieuthuchikhac") ||
               Utility.sDbnull(grdThuchi.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.NguoiTao].Value, "") ==
               globalVariables.UserName)
            {
            }
            else
            {
                Utility.ShowMsg(string.Format("Phiếu thu/chi đang chọn sửa/xóa được tạo bởi {0} nên bạn không được phép xóa", Utility.sDbnull(grdThuchi.CurrentRow.Cells[KcbChidinhclsChitiet.Columns.NguoiTao].Value, "")));
                return ;
            }
            m_enAct = action.Update;
            AllowedChanged_maskedEdit = true;
            SetControlStatus();
        }
        public void Themmoi()
        {
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Bạn cần chọn bệnh nhân thực hiện thu-chi");
                return;
            }
            if (Utility.Byte2Bool(objLuotkham.TthaiThopNoitru) && objLuotkham.TrangthaiNoitru == 5)
            {
                Utility.ShowMsg("Bệnh nhân đã hoàn ứng và ra viện nên bạn không thể lập phiếu thu - chi");
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
                grdThuchi.Enabled = false;
                AllowedChanged = false;
                switch (m_enAct)
                {
                    case action.Insert:
                        //Cho phép nhập liệu mã loại đối tượng,vị trí, tên loại đối tượng và mô tả thêm
                        Utility.DisabledTextBox(txtID);
                        dtpNgaythu.Enabled = true;
                        txtSotien.Enabled = true;
                        txtLydo.Enabled = true;
                        txtNguoithu.Enabled = true;
                        objPhieuthuchi = null;
                        dtpNgaythu.Value = globalVariables.SysDate;
                        txtSotien.Text = "0";
                        txtLydo.SetCode("-1");
                        txtNguoithu.SetCode("-1");
                        txtMotathem.Clear();
                        autoNguonkiqui.Enabled = true;
                        cboPttt.Enabled = true;
                        cboPttt_SelectedIndexChanged(cboPttt, new EventArgs());

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
                        txtSotien.Focus();
                        break;
                    case action.Update:
                        //Không cho phép cập nhật lại mã loại đối tượng
                        Utility.DisabledTextBox(txtID);
                        //Cho phép cập nhật lại vị trí, tên loại đối tượng và mô tả thêm
                        dtpNgaythu.Enabled = true;
                        txtLydo.Enabled = true;
                        txtNguoithu.Enabled = true;
                        txtSotien.Enabled = true;
                        autoNguonkiqui.Enabled = true;
                        cboPttt.Enabled = true;
                       cboPttt_SelectedIndexChanged(cboPttt, new EventArgs());
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
                        txtSotien.Focus();
                        break;
                    case action.FirstOrFinished://Hủy hoặc trạng thái ban đầu khi mới hiển thị Form
                        AllowedChanged_maskedEdit = false;
                        grdThuchi.Enabled = true;
                        AllowedChanged = true;
                        //Không cho phép nhập liệu mã loại đối tượng, tên loại đối tượng và mô tả thêm
                        Utility.DisabledTextBox(txtID);
                        dtpNgaythu.Enabled = false;
                        txtLydo.Enabled = false;
                        txtNguoithu.Enabled = false;
                        txtSotien.Enabled = false;
                        autoNguonkiqui.Enabled = false;
                        cboPttt.Enabled = false;
                        cboNganhang.Enabled = false;

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
                        grdTamung_SelectionChanged(grdThuchi, new EventArgs());
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
                grdThuchi.Enabled = true;
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
                if (!Utility.isValidGrid(grdThuchi))
                {

                    objPhieuthuchi = null;
                    //optPhieuThu.Checked = true;
                    dtpNgaythu.Value = globalVariables.SysDate;
                    txtSotien.Text = "0";
                    txtLydo.SetCode("-1");
                    txtNguoithu.SetCode("-1");
                    cboNganhang.SelectedIndex = -1;
                    cboPttt.SelectedIndex = -1;
                    txtMotathem.Clear();
                    autoNguonkiqui.SetCode("-1");
                }
                else
                {

                    objPhieuthuchi = KcbPhieuthu.FetchByID(Utility.Int32Dbnull(grdThuchi.GetValue(KcbPhieuthu.Columns.IdPhieuthu)));

                    if (objPhieuthuchi == null)
                    {
                        dtpNgaythu.Value = globalVariables.SysDate;
                        txtSotien.Text = "0";
                        txtLydo.SetCode("-1");
                        txtNguoithu.SetCode("-1");
                        cboNganhang.SelectedIndex = -1;
                        cboPttt.SelectedIndex = -1;
                        txtMotathem.Clear();
                        autoNguonkiqui.SetCode("-1");
                    }
                    else
                    {
                        objPhieuthuchi.IsNew = false;
                        objPhieuthuchi.MarkOld();
                        //optPhieuThu.Checked =!Utility.Byte2Bool( objPhieuthuchi.LoaiPhieuthu );
                        //optPhieuChi.Checked = Utility.Byte2Bool(objPhieuthuchi.LoaiPhieuthu);
                        dtpNgaythu.Value = objPhieuthuchi.NgayThuchien;
                        txtSotien.Text = objPhieuthuchi.SoTien.ToString();
                        txtLydo._Text = objPhieuthuchi.LydoNop;
                        txtNguoithu.SetId(objPhieuthuchi.IdNhanvien);
                        txtMotathem.Text = objPhieuthuchi.NoiDung;
                        cboNganhang.SelectedValue = objPhieuthuchi.MaNganhang;
                        cboPttt.SelectedValue = objPhieuthuchi.MaPttt;
                       
                        autoNguonkiqui.SetCode(objPhieuthuchi.MaNguonTt);

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
            NaplichsuThuChi();
            AutoCompleteTextBox();
            LoadPtttNganhang();
            m_enAct = action.FirstOrFinished;
            SetControlStatus();
            ModifyCommand();
            hasLoadedDmuc = true;
        }
        
        private DataTable m_dtKhoaNoiTru = new DataTable();
      
        void AutoCompleteTextBox()
        {
            if (hasLoadedDmuc) return;
            txtLydo.Init();
            AutoCompleteTextBox_nguoithu();
            txtNguoithu.SetId(globalVariables.gv_intIDNhanvien);
            autoNguonkiqui.Init();

        }
        void AutoCompleteTextBox_nguoithu()
        {
            DataTable m_dtNhanvien = SPs.DmucLaydanhsachTnv().GetDataSet().Tables[0];
            txtNguoithu.Init(m_dtNhanvien, new List<string>() { DmucNhanvien.Columns.IdNhanvien, DmucNhanvien.Columns.MaNhanvien, DmucNhanvien.Columns.TenNhanvien });
        }
       
        private void ModifyCommand()
        {
            bool isValid = objLuotkham != null;
            bool isValid2 = Utility.isValidGrid(grdThuchi);
            //bool isValid3 = Utility.Int32Dbnull(grdThuchi.GetValue("id_thanhtoan"),0)<=0;
            //bool isValid4 = Utility.isValidGrid(grdThuchi);
            cmdSua.Enabled = isValid && isValid2;
            cmdxoa.Enabled = isValid && isValid2;
            cmdIn.Enabled = isValid && isValid2;
            cmdthemmoi.Enabled = isValid;
            cmdGhi.Enabled = m_enAct != action.FirstOrFinished;
            cmdHuy.Enabled = cmdGhi.Enabled;
        }

        public DataTable m_dtPhieuthuchi = null;
        void setTongtienStatus()
        {
            try
            {
                if (m_dtPhieuthuchi == null) return;
                lblTongtien.Text = "Tổng tiền: " + new MoneyByLetter().sMoneyToLetter(Utility.sDbnull(m_dtPhieuthuchi.Compute("SUM(so_tien)", "1=1"), "0"));
            }
            catch (Exception ex)
            {

            }

        }
        void NaplichsuThuChi()
        {
            try
            {
                if (objLuotkham == null)
                {
                    grdThuchi.DataSource = null;
                    return;
                }
                m_dtPhieuthuchi = new KCB_THAMKHAM().KcbTimkiemphieuThuchi(objLuotkham.MaLuotkham,
                  Utility.Int32Dbnull(objLuotkham.IdBenhnhan, 0), -1, -1, v_bytNoitru);//objLuotkham.TrangthaiNoitru > 0 ? Utility.Int32Dbnull(objLuotkham.IdKhoanoitru, 0) : -1,(byte)(objLuotkham.TrangthaiNoitru > 0 ?1:0));
                Utility.SetDataSourceForDataGridEx_Basic(grdThuchi, m_dtPhieuthuchi, false, true, "1=1", KcbPhieuthu.Columns.NgayThuchien + " desc");
                grdThuchi.MoveFirst();
                if (grdThuchi.GetDataRows().Length <= 0)
                {
                    objPhieuthuchi = null;
                    dtpNgaythu.Value = globalVariables.SysDate;
                    txtSotien.Text = "0";
                    txtLydo.SetCode("-1");
                    txtNguoithu.SetId(globalVariables.gv_intIDNhanvien);
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
                grdThuchi.Width = 0;
            }
            else
            {
                grdThuchi.Width = 425;
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
            if (hasLoadedDmuc) return;
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

        private void optPhieuThu_CheckedChanged(object sender, EventArgs e)
        {
            lblNguoithuchi.Text = optPhieuThu.Checked ? "Người thu" : "Người chi";
            txtLydo.LOAI_DANHMUC = optPhieuThu.Checked ? "LYDOTHU" : "LYDOCHI";
            txtLydo.Init();
            grdTamung_SelectionChanged(grdThuchi, e);
        }

        private void cmdPhanbo_Click(object sender, EventArgs e)
        {
        
         Phanbo();
        }
        void Phanbo()
        {
            if (!Utility.isValidGrid(grdThuchi)) return;
            long id_phieu = Utility.Int64Dbnull(grdThuchi.CurrentRow.Cells[KcbPhieuthu.Columns.IdPhieuthu].Value, -1);
            long id_thanhtoan = Utility.Int64Dbnull(grdThuchi.CurrentRow.Cells[KcbPhieuthu.Columns.IdThanhtoan].Value, -1);
            string ma_pttt = Utility.sDbnull(grdThuchi.CurrentRow.Cells[KcbPhieuthu.Columns.MaPttt].Value, "TM");
            string ma_nganhang = Utility.sDbnull(grdThuchi.CurrentRow.Cells[KcbThanhtoan.Columns.MaNganhang].Value, "TM");
            frm_PhanbotientheoPTTT _PhanbotientheoPTTT = new frm_PhanbotientheoPTTT(id_thanhtoan, id_phieu, -1, ma_pttt, ma_nganhang);
            _PhanbotientheoPTTT.loai_phanbo = Utility.ByteDbnull(grdThuchi.CurrentRow.Cells[KcbPhieuthu.Columns.LoaiPhieuthu].Value, 0);
            _PhanbotientheoPTTT._OnChangePTTT += _PhanbotientheoPTTT__OnChangePTTT;
            _PhanbotientheoPTTT.objLuotkham = this.objLuotkham;
            _PhanbotientheoPTTT.ShowDialog();
        }

        void _PhanbotientheoPTTT__OnChangePTTT(long id_thanhtoan, string ma_pttt, string ten_pttt, string ma_nganhang, string ten_nganhang)
        {
            try
            {
                DataRow dr = ((DataRowView)grdThuchi.CurrentRow.DataRow).Row;
                dr["ma_pttt"] = ma_pttt;
                dr["ten_pttt"] = ten_pttt;
                dr["ma_nganhang"] = ma_nganhang;
                dr["ten_nganhang"] = ten_nganhang;
                m_dtPhieuthuchi.AcceptChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void cboPttt_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> lstPTTT = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_PTTT_CHONNGANHANG", false).Split(',').ToList<string>();
            cboNganhang.Enabled = lstPTTT.Contains(Utility.sDbnull(cboPttt.SelectedValue, "-1"));
            if (!cboNganhang.Enabled) cboNganhang.SelectedIndex = -1;
        }
    }
}
