using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using VMS.HIS.DAL;
using VNS.Libs;
using VNS.HIS.BusRule.Classes;
using VNS.HIS.UI.NGOAITRU;

namespace VNS.HIS.UI.Forms.Dungchung.UCs
{
    public partial class ucBHYT : UserControl
    {
        public delegate void OnFindPatientByMatheBHYT(string matheBHYT);
        public event OnFindPatientByMatheBHYT _OnFindPatientByMatheBHYT;
        public string SoBHYT = "";
        private bool hasjustpressBACKKey;
        private bool isAutoFinding;
        bool PreventEnabled = false;
        bool Chuyenvien = false;
        DmucDoituongkcb objDoituongKCB = null;
        KcbLuotkham objLuotkham = null;
        public ucBHYT()
        {
            InitializeComponent();
            InitEvents();
        }
        public bool IsBHYT
        {
            set { pnlBHYT.Enabled = value; }
            get { return pnlBHYT.Enabled; }
        }
        public void Init()
        {
            dtInsFromDate.Value = new DateTime(globalVariables.SysDate.Year, 1, 1);
            dtInsToDate.Value = new DateTime(globalVariables.SysDate.Year, 12, 31);
            Utility.SetColor(lblDiachiBHYT, THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_BATNHAP_DIACHI_BHYT", "1", false) == "1" ? lblMatheBHYT.ForeColor : lblDoituongSinhsong.ForeColor);
            chkTraiTuyen.Visible = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_CHOPHEPTIEPDON_TRAITUYEN", "1", false) == "1";
            txtMaDTsinhsong.Init();
        }
        public void DoAutoComplete()
        {
            try
            {
                txtDiachi_bhyt.dtData = globalVariables.dtAutocompleteAddress;
                this.txtDiachi_bhyt.AutoCompleteList = globalVariables.LstAutocompleteAddressSource;
                this.txtDiachi_bhyt.CaseSensitive = false;
                this.txtDiachi_bhyt.MinTypedCharacters = 1;

                DataTable dt_dataDoituongBHYT = new Select().From(DmucDoituongbhyt.Schema).ExecuteDataSet().Tables[0];
                txtMaDtuong_BHYT2.Init(dt_dataDoituongBHYT, new List<string>() { DmucDoituongbhyt.Columns.IdDoituongbhyt, DmucDoituongbhyt.Columns.MaDoituongbhyt, DmucDoituongbhyt.Columns.TenDoituongbhyt });
            }
            catch
            {
            }
            finally
            {
            }
        }
        public void SetValues(bool Chuyenvien, DmucDoituongkcb objDoituongKCB,KcbLuotkham objLuotkham)
        {
            this.Chuyenvien = Chuyenvien;
            this.objDoituongKCB = objDoituongKCB;
            this.objLuotkham = objLuotkham;
            //Gan gia tri
            chkCapCuu.Checked = Utility.Int32Dbnull(objLuotkham.TrangthaiCapcuu, 0) == 1;
            chkTraiTuyen.Checked = Utility.Int32Dbnull(objLuotkham.DungTuyen, 0) == 0;
            lblTuyenBHYT.Text = chkTraiTuyen.Checked ? "TRÁI TUYẾN" : "ĐÚNG TUYẾN";
            txtDiachi_bhyt._Text = Utility.sDbnull(objLuotkham.DiachiBhyt);
            if (!string.IsNullOrEmpty(Utility.sDbnull(objLuotkham.NgaybatdauBhyt)))
                dtInsFromDate.Value = Convert.ToDateTime(objLuotkham.NgaybatdauBhyt);
            if (!string.IsNullOrEmpty(Utility.sDbnull(objLuotkham.NgayketthucBhyt)))
                dtInsToDate.Value = Convert.ToDateTime(objLuotkham.NgayketthucBhyt);
            txtPtramBHYT.Text = Utility.sDbnull(objLuotkham.PtramBhyt, "0");
            txtptramDauthe.Text = Utility.sDbnull(objLuotkham.PtramBhytGoc, "0");
            txtMaDtuong_BHYT.Text = Utility.sDbnull(objLuotkham.MaDoituongBhyt);

            txtMaQuyenloi_BHYT.Text = Utility.sDbnull(objLuotkham.MaQuyenloi);
            txtNoiDongtrusoKCBBD.Text = Utility.sDbnull(objLuotkham.NoiDongtrusoKcbbd);
            txtOthu4.Text = Utility.sDbnull(objLuotkham.MatheBhyt).Substring(5, 2);
            txtOthu5.Text = Utility.sDbnull(objLuotkham.MatheBhyt).Substring(7, 3);
            txtOthu6.Text = Utility.sDbnull(objLuotkham.MatheBhyt).Substring(10, 5);

            txtMaDTsinhsong.SetCode(objLuotkham.MadtuongSinhsong);
            chkGiayBHYT.Checked = Utility.Byte2Bool(objLuotkham.GiayBhyt);

            txtNoiphattheBHYT.Text = Utility.sDbnull(objLuotkham.MaNoicapBhyt);
            txtNoiDKKCBBD.Text = Utility.sDbnull(objLuotkham.MaKcbbd);
            pnlBHYT.Enabled = true && !PreventEnabled;
        }
        public void SetValues(DataRow drData)
        {
            if (drData == null)
            {
                ClearMe();
                lblTuyenBHYT.Visible = false;
                pnlBHYT.Enabled = false;
                lblPtram.Text = "% giảm giá";
            }
            else
            {
                string _MaDoituongKcb = Utility.sDbnull(drData[KcbLichsuDoituongKcb.Columns.MaDoituongKcb]);
                objDoituongKCB = new Select().From(DmucDoituongkcb.Schema).Where(DmucDoituongkcb.MaDoituongKcbColumn).IsEqualTo(_MaDoituongKcb).ExecuteSingle<DmucDoituongkcb>();

                ChangeObjectRegion(objDoituongKCB);
                if (objDoituongKCB.IdLoaidoituongKcb == 0)//ĐỐi tượng BHYT
                {
                    chkCapCuu.Checked = Utility.Int32Dbnull(drData[KcbLichsuDoituongKcb.Columns.TrangthaiCapcuu], 0) == 1;
                    chkTraiTuyen.Checked = Utility.Int32Dbnull(drData[KcbLichsuDoituongKcb.Columns.DungTuyen], 0) == 0;
                    lblTuyenBHYT.Text = chkTraiTuyen.Checked ? "TRÁI TUYẾN" : "ĐÚNG TUYẾN";
                    txtDiachi_bhyt._Text = Utility.sDbnull(drData[KcbLichsuDoituongKcb.Columns.DiachiBhyt]);
                    if (!string.IsNullOrEmpty(Utility.sDbnull(drData[KcbLichsuDoituongKcb.Columns.NgaybatdauBhyt])))
                        dtInsFromDate.Value = Convert.ToDateTime(drData[KcbLichsuDoituongKcb.Columns.NgaybatdauBhyt]);
                    if (!string.IsNullOrEmpty(Utility.sDbnull(drData[KcbLichsuDoituongKcb.Columns.NgayketthucBhyt])))
                        dtInsToDate.Value = Convert.ToDateTime(drData[KcbLichsuDoituongKcb.Columns.NgayketthucBhyt]);
                    txtPtramBHYT.Text = Utility.sDbnull(drData[KcbLichsuDoituongKcb.Columns.PtramBhyt], "0");
                    txtptramDauthe.Text = Utility.sDbnull(drData[KcbLichsuDoituongKcb.Columns.PtramBhytGoc], "0");
                    txtMaDtuong_BHYT.Text = Utility.sDbnull(drData[KcbLichsuDoituongKcb.Columns.MaDoituongBhyt]);

                    txtMaQuyenloi_BHYT.Text = Utility.sDbnull(drData[KcbLichsuDoituongKcb.Columns.MaQuyenloi]);
                    txtNoiDongtrusoKCBBD.Text = Utility.sDbnull(drData[KcbLichsuDoituongKcb.Columns.NoiDongtrusoKcbbd]);
                    txtOthu4.Text = Utility.sDbnull(drData[KcbLichsuDoituongKcb.Columns.MatheBhyt]).Substring(5, 2);
                    txtOthu5.Text = Utility.sDbnull(drData[KcbLichsuDoituongKcb.Columns.MatheBhyt]).Substring(7, 3);
                    txtOthu6.Text = Utility.sDbnull(drData[KcbLichsuDoituongKcb.Columns.MatheBhyt]).Substring(10, 5);

                    txtMaDTsinhsong.SetCode(Utility.sDbnull(drData[KcbLichsuDoituongKcb.Columns.MadtuongSinhsong]));
                    chkGiayBHYT.Checked = Utility.Byte2Bool(drData[KcbLichsuDoituongKcb.Columns.GiayBhyt]);

                    txtNoiphattheBHYT.Text = Utility.sDbnull(drData[KcbLichsuDoituongKcb.Columns.MaNoicapBhyt]);
                    txtNoiDKKCBBD.Text = Utility.sDbnull(drData[KcbLichsuDoituongKcb.Columns.MaKcbbd]);

                   
                }
                else//Đối tượng khác BHYT
                {
                    ClearMe();
                }
               
               
            }
        }
        public void GetData(ref DataRow drData)
        {
            if (pnlBHYT.Enabled)
            {
                drData[KcbLichsuDoituongKcb.Columns.TrangthaiCapcuu] = Utility.Bool2byte(chkCapCuu.Checked);
                drData[KcbLichsuDoituongKcb.Columns.DungTuyen] = !chkTraiTuyen.Visible ? 1 : (((byte?)(chkTraiTuyen.Checked ? 0 : 1)));
                drData[KcbLichsuDoituongKcb.Columns.DiachiBhyt] = Utility.DoTrim(txtDiachi_bhyt.Text);
                drData[KcbLichsuDoituongKcb.Columns.NgaybatdauBhyt] = dtInsFromDate.Value;
                drData[KcbLichsuDoituongKcb.Columns.NgayketthucBhyt] = dtInsToDate.Value;

                drData[KcbLichsuDoituongKcb.Columns.PtramBhyt] = Utility.DecimaltoDbnull(txtPtramBHYT.Text, 0);
                drData[KcbLichsuDoituongKcb.Columns.PtramBhytGoc] = Utility.DecimaltoDbnull(txtptramDauthe.Text, 0);
                drData[KcbLichsuDoituongKcb.Columns.MaDoituongBhyt] = txtMaDtuong_BHYT.Text;

                drData[KcbLichsuDoituongKcb.Columns.MaQuyenloi] = Utility.Int32Dbnull(txtMaQuyenloi_BHYT.Text, -1);
                drData[KcbLichsuDoituongKcb.Columns.NoiDongtrusoKcbbd] = txtNoiDongtrusoKCBBD.Text;

                drData[KcbLichsuDoituongKcb.Columns.MatheBhyt] = Laymathe_BHYT();
                drData[KcbLichsuDoituongKcb.Columns.MadtuongSinhsong] = txtMaDTsinhsong.myCode;
                drData[KcbLichsuDoituongKcb.Columns.GiayBhyt] = Utility.Bool2byte(chkGiayBHYT.Checked);


                drData[KcbLichsuDoituongKcb.Columns.MaNoicapBhyt] = txtNoiphattheBHYT.Text;
                drData[KcbLichsuDoituongKcb.Columns.NoicapBhyt] = lblNoiCapThe.Text;
                drData[KcbLichsuDoituongKcb.Columns.MaKcbbd] = txtNoiDKKCBBD.Text;
            }
            else
            {
                drData[KcbLichsuDoituongKcb.Columns.TrangthaiCapcuu] = Utility.Bool2byte(chkCapCuu.Checked);
                drData[KcbLichsuDoituongKcb.Columns.DungTuyen] = 0;
                drData[KcbLichsuDoituongKcb.Columns.DiachiBhyt] = "";
                drData[KcbLichsuDoituongKcb.Columns.NgaybatdauBhyt] = DBNull.Value;
                drData[KcbLichsuDoituongKcb.Columns.NgayketthucBhyt] = DBNull.Value;

                drData[KcbLichsuDoituongKcb.Columns.PtramBhyt] = 0;
                drData[KcbLichsuDoituongKcb.Columns.PtramBhytGoc] =0;
                drData[KcbLichsuDoituongKcb.Columns.MaDoituongBhyt] = "";

                drData[KcbLichsuDoituongKcb.Columns.MaQuyenloi] = 1;
                drData[KcbLichsuDoituongKcb.Columns.NoiDongtrusoKcbbd] = "";

                drData[KcbLichsuDoituongKcb.Columns.MatheBhyt] = "";
                drData[KcbLichsuDoituongKcb.Columns.MadtuongSinhsong] = "";
                drData[KcbLichsuDoituongKcb.Columns.GiayBhyt] = 0;


                drData[KcbLichsuDoituongKcb.Columns.MaNoicapBhyt] = "";
                drData[KcbLichsuDoituongKcb.Columns.NoicapBhyt] = "";
                drData[KcbLichsuDoituongKcb.Columns.MaKcbbd] = "";
            }
        }
        public void ChangeObjectRegion(DmucDoituongkcb objDoituongKCB)
        {
            this.objDoituongKCB = objDoituongKCB;
            ResetMe(objDoituongKCB);
        }
        public void ClearNoiDKKCBBD()
        {
            txtNoiDKKCBBD.Clear();
            txtNoiphattheBHYT.Clear();
        }
        public void ResetMe(DmucDoituongkcb objDoituongKCB)
        {
            this.objDoituongKCB = objDoituongKCB;
            txtPtramBHYT.Text = objDoituongKCB.PhantramTraituyen.ToString();
            txtptramDauthe.Text = objDoituongKCB.PhantramTraituyen.ToString();
            if (objDoituongKCB.IdLoaidoituongKcb == 0)//Đối tượng BHYT
            {
                pnlBHYT.Enabled = true && !PreventEnabled;
                lblPtram.Text = "% BHYT ngoại trú";
                TinhPtramBHYT();
                lblTuyenBHYT.Visible = true;
                txtMaDtuong_BHYT.SelectAll();
                txtMaDtuong_BHYT.Focus();
            }
            else//Đối tượng khác BHYT
            {
                lblTuyenBHYT.Visible = false;
                pnlBHYT.Enabled = false;
                lblPtram.Text = "% giảm giá";
            }
            chkTraiTuyen.Checked = false;
            lblTuyenBHYT.Text = chkTraiTuyen.Checked ? "TRÁI TUYẾN" : "ĐÚNG TUYẾN";
            lblPtramdauthe.Visible = objDoituongKCB.IdLoaidoituongKcb == 0;
            txtptramDauthe.Visible = objDoituongKCB.IdLoaidoituongKcb == 0;
            chkCapCuu.Checked = false;
            txtPtramBHYT.Text = "0";
            txtptramDauthe.Text = "0";
            lnkThem.Visible = false;
            dtInsFromDate.Value = new DateTime(globalVariables.SysDate.Year, 1, 1);
            dtInsToDate.Value = new DateTime(globalVariables.SysDate.Year, 12, 31);
        }
        public void ClearMe()
        {
            dtInsFromDate.Value = new DateTime(globalVariables.SysDate.Year, 1, 1);
            dtInsToDate.Value = new DateTime(globalVariables.SysDate.Year, 12, 31);
            txtPtramBHYT.Text = "";
            txtptramDauthe.Text = "";
            lblNoiCapThe.Text = "";
            lblClinicName.Text = "";
            txtMaDtuong_BHYT.Clear();
            txtMaDTsinhsong.ResetText();
            chkGiayBHYT.Checked = false;
            txtMaQuyenloi_BHYT.Clear();
            txtNoiDongtrusoKCBBD.Clear();
            txtOthu4.Clear();
            txtOthu5.Clear();
            txtOthu6.Clear();
            chkTraiTuyen.Checked = false;
            lblTuyenBHYT.Text = chkTraiTuyen.Checked ? "TRÁI TUYẾN" : "ĐÚNG TUYẾN";
            txtNoiphattheBHYT.Clear();
            txtDiachi_bhyt.Clear();
            txtNoiDKKCBBD.Clear();
        }
        void InitEvents()
        {
            txtMaDtuong_BHYT.KeyDown += txtMaDtuong_BHYT_KeyDown;
            txtMaDtuong_BHYT.TextChanged += new EventHandler(txtMaDtuong_BHYT_TextChanged);

            txtMaQuyenloi_BHYT.KeyDown += txtMaQuyenloi_BHYT_KeyDown;
            txtMaQuyenloi_BHYT.TextChanged += new EventHandler(txtMaQuyenloi_BHYT_TextChanged);

            txtNoiphattheBHYT.TextChanged += new EventHandler(txtNoiphattheBHYT_TextChanged);
            txtNoiphattheBHYT.KeyDown += txtNoiphattheBHYT_KeyDown;
            txtOthu4.KeyDown += txtOthu4_KeyDown;
            txtOthu4.TextChanged += new EventHandler(txtOthu4_TextChanged);
            txtOthu5.KeyDown += txtOthu5_KeyDown;
            txtOthu5.TextChanged += new EventHandler(txtOthu5_TextChanged);
            txtOthu6.TextChanged += new EventHandler(txtOthu6_TextChanged);
            txtOthu6.KeyDown += txtOthu6_KeyDown;
            txtOthu6.LostFocus += _LostFocus;
            txtNoiDKKCBBD.KeyDown += txtNoiDKKCBBD_KeyDown;
            txtNoiDKKCBBD.TextChanged += new EventHandler(txtNoiDKKCBBD_TextChanged);

            txtNoiDongtrusoKCBBD.TextChanged += new EventHandler(txtNoiDongtrusoKCBBD_TextChanged);
            txtNoiDongtrusoKCBBD.KeyDown += txtNoiDongtrusoKCBBD_KeyDown;

            chkTraiTuyen.CheckedChanged += chkTraiTuyen_CheckedChanged;
            lnkThem.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(lnkThem_LinkClicked);

            txtMaDTsinhsong._OnEnterMe += txtMaDTsinhsong__OnEnterMe;
            chkGiayBHYT.CheckedChanged += chkGiayBHYT_CheckedChanged;
        }
        private void _LostFocus(object sender, EventArgs e)
        {
            if (isAutoFinding) return;
            string MA_BHYT = txtMaDtuong_BHYT.Text.Trim() + txtMaQuyenloi_BHYT.Text.Trim() + txtNoiphattheBHYT.Text.Trim() +
                             txtOthu4.Text.Trim() + txtOthu5.Text.Trim() + txtOthu6.Text.Trim();
            if (MA_BHYT.Length == 15 && _OnFindPatientByMatheBHYT!=null) _OnFindPatientByMatheBHYT(MA_BHYT);
        }
        void chkGiayBHYT_CheckedChanged(object sender, EventArgs e)
        {
            TinhPtramBHYT();
        }
        void txtMaDTsinhsong__OnEnterMe()
        {
            if (txtMaDTsinhsong.myCode != "-1")
            {
                if (chkTraiTuyen.Checked)
                {
                    chkTraiTuyen.Checked = false;
                    lblTuyenBHYT.Text = chkTraiTuyen.Checked ? "TRÁI TUYẾN" : "ĐÚNG TUYẾN";
                }
            }
            TinhPtramBHYT();
        }
        private void chkTraiTuyen_CheckedChanged(object sender, EventArgs e)
        {
            lblTuyenBHYT.Text = chkTraiTuyen.Checked ? "TRÁI TUYẾN" : "ĐÚNG TUYẾN";
            TinhPtramBHYT();
        }
        private void txtNoiDKKCBBD_KeyDown(object sender, KeyEventArgs e)
        {
            hasjustpressBACKKey = false;
            if (e.KeyCode == Keys.Enter)
            {
                //string MA_BHYT = txtMaDtuong_BHYT.Text.Trim() + txtMaQuyenloi_BHYT.Text.Trim() + txtNoiDongtrusoKCBBD.Text.Trim() + txtOthu4.Text.Trim() + txtOthu5.Text.Trim() + txtOthu6.Text.Trim();
                //if (MA_BHYT.Length == 15) FindPatientIDbyBHYT(MA_BHYT);
                return;
            }
            if (e.KeyCode == Keys.Back)
            {
                hasjustpressBACKKey = true;
                if (txtNoiDKKCBBD.Text.Length <= 0)
                {
                    txtNoiDongtrusoKCBBD.Focus();
                    txtNoiDongtrusoKCBBD.Select(txtNoiDongtrusoKCBBD.Text.Length, 0);
                }
            }
        }
        private void txtNoiphattheBHYT_KeyDown(object sender, KeyEventArgs e)
        {
            hasjustpressBACKKey = false;
            if (e.KeyCode == Keys.Enter)
            {
                return;
            }
            if (e.KeyCode == Keys.Back)
            {
                hasjustpressBACKKey = true;
                if (txtNoiphattheBHYT.Text.Length <= 0)
                {
                    txtMaQuyenloi_BHYT.Focus();
                    txtMaQuyenloi_BHYT.Select(txtMaQuyenloi_BHYT.Text.Length, 0);
                }
            }
        }
        /// <summary>
        /// hàm thực hiện việc đánh nhanh thông tin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMaDtuong_BHYT_TextChanged(object sender, EventArgs e)
        {
            if (objDoituongKCB == null || objDoituongKCB.MaDoituongKcb == "DV") return;
            if (txtMaDtuong_BHYT.Text.Length < 2) return;
            if (!IsValidTheBHYT()) return;
            TinhPtramBHYT();
            txtMaQuyenloi_BHYT.Focus();
            txtMaQuyenloi_BHYT.SelectAll();
        }

        public bool IsValidTheBHYT()
        {
            if (!string.IsNullOrEmpty(txtMaDtuong_BHYT.Text))
            {
                SqlQuery sqlQuery = new Select().From(DmucDoituongbhyt.Schema)
                    .Where(DmucDoituongbhyt.Columns.MaDoituongbhyt).IsEqualTo(txtMaDtuong_BHYT.Text);
                if (sqlQuery.GetRecordCount() <= 0)
                {
                    Utility.ShowMsg(
                        "Mã đối tượng BHYT không tồn tại trong hệ thống. Mời bạn kiểm tra lại",
                        "Thông báo", MessageBoxIcon.Information);
                    txtMaDtuong_BHYT.Focus();
                    txtMaDtuong_BHYT.SelectAll();
                    return false;
                }
            }
            if (Utility.DoTrim(txtMaDtuong_BHYT.Text) != "" && Utility.DoTrim(txtMaQuyenloi_BHYT.Text) != "")
            {
                QheDautheQloiBhyt objQheDautheQloiBhyt = new Select().From(QheDautheQloiBhyt.Schema).Where(QheDautheQloiBhyt.Columns.MaDoituongbhyt).IsEqualTo(Utility.DoTrim(txtMaDtuong_BHYT.Text))
                    .And(QheDautheQloiBhyt.Columns.MaQloi).IsEqualTo(Utility.Int32Dbnull(txtMaQuyenloi_BHYT.Text, 0)).ExecuteSingle<QheDautheQloiBhyt>();
                if (objQheDautheQloiBhyt == null)
                {
                    Utility.ShowMsg(string.Format("Đầu thẻ BHYT: {0} chưa được cấu hình gắn với mã quyền lợi: {1}. Mời bạn kiểm tra lại", txtMaDtuong_BHYT.Text, txtMaQuyenloi_BHYT.Text));
                    txtMaQuyenloi_BHYT.Focus();
                    txtMaQuyenloi_BHYT.SelectAll();
                    return false;
                }
            }
            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_KIEMTRAMATHE", "1", true) == "1")
            {
                if (!string.IsNullOrEmpty(txtMaQuyenloi_BHYT.Text))
                {
                    if (Utility.Int32Dbnull(txtMaQuyenloi_BHYT.Text, 0) < 1 || Utility.Int32Dbnull(txtMaQuyenloi_BHYT.Text, 0) > 9)
                    {
                        Utility.ShowMsg("Số thứ tự 2 của mã bảo hiểm nằm trong khoảng từ 1->9", "Thông báo",
                                        MessageBoxIcon.Information);
                        txtMaQuyenloi_BHYT.Focus();
                        txtMaQuyenloi_BHYT.SelectAll();
                        return false;
                    }

                    QheDautheQloiBhytCollection lstqhe = new Select().From(QheDautheQloiBhyt.Schema).Where(QheDautheQloiBhyt.Columns.MaDoituongbhyt).IsEqualTo(txtMaDtuong_BHYT.Text).ExecuteAsCollection<QheDautheQloiBhytCollection>();
                    if (lstqhe.Count > 0)
                    {
                        var q = from p in lstqhe
                                where p.MaQloi == Utility.ByteDbnull(txtMaQuyenloi_BHYT.Text, -1)
                                select objDoituongKCB;

                        if (q.Count() <= 0)
                        {

                            Utility.ShowMsg(
                                string.Format(
                                    "Đầu thẻ :{0} chưa được tạo quan hệ với mã quyền lợi {1}\n Đề nghị bạn kiểm tra lại danh mục đối tượng tham gia BHYT",
                                    txtMaDtuong_BHYT.Text, txtMaQuyenloi_BHYT.Text));
                            txtMaQuyenloi_BHYT.Focus();
                            txtMaQuyenloi_BHYT.SelectAll();
                            return false;
                        }
                    }
                    else
                    {
                        Utility.ShowMsg(
                            string.Format(
                                "Đầu thẻ :{0} chưa được tạo quan hệ với mã quyền lợi {1}\n Đề nghị bạn kiểm tra lại danh mục đối tượng tham gia BHYT",
                                txtMaDtuong_BHYT.Text, txtMaQuyenloi_BHYT.Text));
                        txtMaQuyenloi_BHYT.Focus();
                        txtMaQuyenloi_BHYT.SelectAll();
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty(txtNoiphattheBHYT.Text))
                {
                    if (txtNoiphattheBHYT.Text.Length <= 1)
                    {
                        Utility.ShowMsg("Mã nơi phát thẻ BHYT phải nằm trong khoảng từ 00->99", "Thông báo",
                                        MessageBoxIcon.Information);
                        txtNoiphattheBHYT.Focus();
                        txtNoiphattheBHYT.SelectAll();
                        return false;
                    }
                    if (Utility.Int32Dbnull(txtNoiphattheBHYT.Text, 0) <= 0)
                    {
                        Utility.ShowMsg("Mã nơi phát thẻ BHYT không được phép có chữ cái và phải nằm trong khoảng từ 00->99", "Thông báo",
                                        MessageBoxIcon.Information);
                        txtNoiphattheBHYT.Focus();
                        txtNoiphattheBHYT.SelectAll();
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty(txtOthu4.Text))
                {
                    if (txtOthu4.Text.Length <= 1)
                    {
                        Utility.ShowMsg("Hai kí tự ô số 4 của mã bảo hiểm nằm trong khoảng từ 01->99", "Thông báo",
                                        MessageBoxIcon.Information);
                        txtOthu4.Focus();
                        txtOthu4.SelectAll();
                        return false;
                    }

                    if (Utility.Int32Dbnull(txtOthu4.Text, 0) <= 0)
                    {
                        Utility.ShowMsg("Hai kí tự ô số 4 của mã bảo hiểm không được phép có chữ cái và phải nằm trong khoảng từ 01->99", "Thông báo",
                                        MessageBoxIcon.Information);
                        txtOthu4.Focus();
                        txtOthu4.SelectAll();
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty(txtOthu5.Text))
                {
                    if (txtOthu5.Text.Length <= 2)
                    {
                        Utility.ShowMsg("3 kí tự ô số 5 của mã bảo hiểm nằm trong khoảng từ 001->999", "Thông báo",
                                        MessageBoxIcon.Information);
                        txtOthu5.Focus();
                        txtOthu5.SelectAll();
                        return false;
                    }

                    if (Utility.Int32Dbnull(txtOthu5.Text, 0) <= 0)
                    {
                        Utility.ShowMsg("3 kí tự ô số 5 của mã bảo hiểm không được phép có chữ cái và phải nằm trong khoảng từ 001->999", "Thông báo",
                                        MessageBoxIcon.Information);
                        txtOthu5.Focus();
                        txtOthu5.SelectAll();
                        return false;
                    }
                }
                if (!string.IsNullOrEmpty(txtOthu6.Text))
                {
                    if (txtOthu6.Text.Length <= 4)
                    {
                        Utility.ShowMsg("5 kí tự ô số 6 của mã bảo hiểm nằm trong khoảng từ 00001->99999", "Thông báo",
                                        MessageBoxIcon.Information);
                        txtOthu6.Focus();
                        txtOthu6.SelectAll();
                        return false;
                    }

                    if (Utility.Int32Dbnull(txtOthu6.Text, 0) <= 0)
                    {
                        Utility.ShowMsg("5 kí tự ô số 6 của mã bảo hiểm không được phép có chữ cái và phải nằm trong khoảng từ 00001->99999", "Thông báo",
                                        MessageBoxIcon.Information);
                        txtOthu6.Focus();
                        txtOthu6.SelectAll();
                        return false;
                    }
                }
            }
            if (!string.IsNullOrEmpty(txtNoiDongtrusoKCBBD.Text))
            {
                if (txtNoiDongtrusoKCBBD.Text.Length <= 1)
                {
                    Utility.ShowMsg("2 kí tự nơi đóng trụ sợ KCBBD phải nhập từ 01->99", "Thông báo",
                                    MessageBoxIcon.Information);
                    txtNoiDongtrusoKCBBD.Focus();
                    txtNoiDongtrusoKCBBD.SelectAll();
                    return false;
                }

                if (Utility.Int32Dbnull(txtNoiDongtrusoKCBBD.Text, 0) <= 0)
                {
                    Utility.ShowMsg("2 kí tự nơi đóng trụ sợ KCBBD không được phép có chữ cái và phải nằm trong khoảng từ 01->99", "Thông báo",
                                    MessageBoxIcon.Information);
                    txtNoiDongtrusoKCBBD.Focus();
                    txtNoiDongtrusoKCBBD.SelectAll();
                    return false;
                }

                SqlQuery sqlQuery = new Select().From(DmucDiachinh.Schema)
                    .Where(DmucDiachinh.Columns.MaDiachinh).IsEqualTo(txtNoiDongtrusoKCBBD.Text);
                if (sqlQuery.GetRecordCount() <= 0)
                {
                    Utility.ShowMsg(
                        "Mã thành phố nơi đăng ký khám hiện không tồn tại trong CSDL\n Mời bạn liên hệ với quản trị mạng để nhập thêm",
                        "Thông báo", MessageBoxIcon.Information);
                    txtNoiDongtrusoKCBBD.Focus();
                    txtNoiDongtrusoKCBBD.SelectAll();
                    return false;
                }
            }
            if (!string.IsNullOrEmpty(txtNoiDKKCBBD.Text))
            {
                string ma_diachinh = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_DANGKY_CACHXACDINH_NOIDKKCBBD", true) == "0" ? txtNoiphattheBHYT.Text : txtNoiDongtrusoKCBBD.Text;
                SqlQuery sqlQuery = new Select().From(DmucNoiKCBBD.Schema)
                    .Where(DmucNoiKCBBD.Columns.MaKcbbd).IsEqualTo(txtNoiDKKCBBD.Text)
                    .And(DmucNoiKCBBD.Columns.MaDiachinh).IsEqualTo(ma_diachinh);
                if (sqlQuery.GetRecordCount() <= 0)
                {
                    Utility.ShowMsg(
                        "Mã  nơi đăng ký khám hiện không tồn tại trong CSDL\n Mời bạn liên hệ với quản trị mạng để nhập thêm",
                        "Thông báo", MessageBoxIcon.Information);
                    txtNoiDKKCBBD.Focus();
                    txtNoiDKKCBBD.SelectAll();
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// hàm thực hiện viecj kiểm tra thông tin cảu đối tượng bảo hiểm
        /// </summary>
        /// <returns></returns>
        public bool IsValidBHYT()
        {
            if (string.IsNullOrEmpty(txtMaDtuong_BHYT.Text))
            {
                Utility.ShowMsg("Bạn phải nhập đối tượng đầu thẻ cho bảo hiểm không bỏ trống", "Thông báo",
                                MessageBoxIcon.Information);
                txtMaDtuong_BHYT.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtMaQuyenloi_BHYT.Text))
            {
                Utility.ShowMsg("Bạn phải nhập mã quyền lợi cho bảo hiểm không bỏ trống", "Thông báo");
                txtMaQuyenloi_BHYT.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtNoiDongtrusoKCBBD.Text))
            {
                Utility.ShowMsg("Bạn phải nhập nơi đăng ký ô thứ 3  cho bảo hiểm không bỏ trống", "Thông báo");
                txtNoiDongtrusoKCBBD.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtOthu4.Text))
            {
                Utility.ShowMsg("Bạn phải nhập nơi đăng ký ô thứ 4  cho bảo hiểm không bỏ trống", "Thông báo");
                txtOthu4.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtOthu5.Text))
            {
                Utility.ShowMsg("Bạn phải nhập nơi đăng ký ô thứ 5  cho bảo hiểm không bỏ trống", "Thông báo");
                txtOthu5.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtOthu6.Text))
            {
                Utility.ShowMsg("Bạn phải nhập nơi đăng ký ô thứ 6  cho bảo hiểm không bỏ trống", "Thông báo");
                txtOthu6.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtNoiphattheBHYT.Text))
            {
                Utility.ShowMsg("Bạn phải nhập nơi cấp thẻ  cho bảo hiểm không bỏ trống", "Thông báo",
                                MessageBoxIcon.Information);
                txtNoiphattheBHYT.Focus();
                return false;
            }
            if (string.IsNullOrEmpty(txtNoiDKKCBBD.Text))
            {
                Utility.ShowMsg("Bạn phải nhập nơi đăng ký khám chữa bệnh ban đầu cho bảo hiểm không bỏ trống",
                                "Thông báo");
                txtNoiDKKCBBD.Focus();
                return false;
            }
            if (dtInsToDate.Value < dtInsFromDate.Value)
            {
                Utility.ShowMsg("Ngày hết hạn thẻ BHYT phải lớn hơn hoặc bằng ngày đăng ký thẻ BHYT", "Thông báo");
                dtInsToDate.Focus();
                return false;
            }
            if (dtInsToDate.Value < globalVariables.SysDate)
            {
                Utility.ShowMsg("Ngày hết hạn thẻ BHYT phải lớn hơn hoặc bằng ngày hiện tại", "Thông báo");
                dtInsToDate.Focus();
                return false;
            }
            return true;
        }
        /// <summary>
        /// hàm thực hiện việc số thứ tự của BHYT
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtMaQuyenloi_BHYT_TextChanged(object sender, EventArgs e)
        {
             if (objDoituongKCB == null || objDoituongKCB.MaDoituongKcb == "DV") return;
            if (hasjustpressBACKKey && txtMaQuyenloi_BHYT.Text.Length <= 0)
            {
                txtMaDtuong_BHYT.Focus();
                if (txtMaDtuong_BHYT.Text.Length > 0) txtMaDtuong_BHYT.Select(txtMaDtuong_BHYT.Text.Length, 0);
            }
            if (txtMaQuyenloi_BHYT.Text.Length < 1) return;
            if (!IsValidTheBHYT()) return;
            TinhPtramBHYT();
            txtNoiphattheBHYT.Focus();
            txtNoiphattheBHYT.SelectAll();
        }

        /// <summary>
        /// hàm thực hiện việc thay đổi thông tin của phần 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtNoiDongtrusoKCBBD_TextChanged(object sender, EventArgs e)
        {
             if (objDoituongKCB == null || objDoituongKCB.MaDoituongKcb == "DV") return;
            if (hasjustpressBACKKey && txtNoiDongtrusoKCBBD.Text.Length <= 0)
            {
                txtOthu6.Focus();
                if (txtOthu6.Text.Length > 0) txtOthu6.Select(txtOthu6.Text.Length, 0);
                return;
            }
            if (txtNoiDongtrusoKCBBD.Text.Length < 2) return;
            if (!IsValidTheBHYT()) return;
            LoadClinicCode();
            txtNoiDKKCBBD.Focus();
            txtNoiDKKCBBD.SelectAll();
        }

        private void txtOthu4_TextChanged(object sender, EventArgs e)
        {
             if (objDoituongKCB == null || objDoituongKCB.MaDoituongKcb == "DV") return;
            if (hasjustpressBACKKey && txtOthu4.Text.Length <= 0)
            {
                txtNoiphattheBHYT.Focus();
                if (txtNoiphattheBHYT.Text.Length > 0) txtNoiphattheBHYT.Select(txtNoiphattheBHYT.Text.Length, 0);
                return;
            }
            if (txtOthu4.Text.Length < 2) return;
            if (!IsValidTheBHYT()) return;
            txtOthu5.Focus();
            txtOthu5.SelectAll();
        }

        private void txtOthu5_TextChanged(object sender, EventArgs e)
        {
             if (objDoituongKCB == null || objDoituongKCB.MaDoituongKcb == "DV") return;
            if (hasjustpressBACKKey && txtOthu5.Text.Length <= 0)
            {
                txtOthu4.Focus();
                if (txtOthu4.Text.Length > 0) txtOthu4.Select(txtOthu4.Text.Length, 0);
                return;
            }
            if (txtOthu5.Text.Length < 3) return;
            if (!IsValidTheBHYT()) return;
            txtOthu6.Focus();
            txtOthu6.SelectAll();
        }

        private void txtOthu6_TextChanged(object sender, EventArgs e)
        {
             if (objDoituongKCB == null || objDoituongKCB.MaDoituongKcb == "DV") return;
            if (hasjustpressBACKKey && txtOthu6.Text.Length <= 0)
            {
                txtOthu5.Focus();
                if (txtOthu5.Text.Length > 0) txtOthu5.Select(txtOthu5.Text.Length, 0);
                return;
            }
            if (txtOthu6.Text.Length < 5) return;
            if (!IsValidTheBHYT()) return;
            txtNoiDongtrusoKCBBD.Focus();
            txtNoiDongtrusoKCBBD.SelectAll();
        }

        private void txtNoiphattheBHYT_TextChanged(object sender, EventArgs e)
        {
             if (objDoituongKCB == null || objDoituongKCB.MaDoituongKcb == "DV") return;
            if (txtNoiphattheBHYT.Text.Length < 2)
            {
                Utility.SetMsg(lblNoiCapThe, "", false);
                return;
            }
            else
                GetNoiDangKy();
            if (!IsValidTheBHYT()) return;
            txtOthu4.Focus();
            txtOthu4.SelectAll();

        }

        private void GetNoiDangKy()
        {
            SqlQuery sqlQuery = new Select().From(DmucDiachinh.Schema)
                .Where(DmucDiachinh.Columns.MaDiachinh).IsEqualTo(txtNoiphattheBHYT.Text);
            var objDiachinh = sqlQuery.ExecuteSingle<DmucDiachinh>();
            if (objDiachinh != null)
            {
                lblNoiCapThe.Visible = true;
                Utility.SetMsg(lblNoiCapThe, Utility.sDbnull(objDiachinh.TenDiachinh), true);
                //LoadClinicCode();
            }
            else
            {
                lblNoiCapThe.Visible = false;
            }
        }

        private void txtNoiDKKCBBD_TextChanged(object sender, EventArgs e)
        {
             if (objDoituongKCB == null || objDoituongKCB.MaDoituongKcb == "DV") return;
            if (txtNoiDKKCBBD.Text.Length < 3)
            {
                Utility.SetMsg(lblClinicName, "", false);
                return;
            }
            LoadClinicCode();
            if (lnkThem.Visible) lnkThem.Focus();
            else
                dtInsFromDate.Focus();
        }

        public void LaySoTheBHYT()
        {
            string SoBHYT = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", txtMaDtuong_BHYT.Text, txtMaQuyenloi_BHYT.Text,
                                          txtNoiphattheBHYT.Text, txtOthu4.Text, txtOthu5.Text, txtOthu6.Text,
                                          txtNoiDongtrusoKCBBD.Text, txtNoiDKKCBBD.Text);
            GetSoBHYT = SoBHYT;
        }
        public string mathe_bhyt_full()
        {
            return string.Format("{0}{1}{2}{3}{4}{5}{6}{7}", txtMaDtuong_BHYT.Text, txtMaQuyenloi_BHYT.Text,
                                          txtNoiphattheBHYT.Text, txtOthu4.Text, txtOthu5.Text, txtOthu6.Text,
                                          txtNoiDongtrusoKCBBD.Text, txtNoiDKKCBBD.Text);

        }

        public string Laymathe_BHYT()
        {
            string SoBHYT = string.Format("{0}{1}{2}{3}{4}{5}", txtMaDtuong_BHYT.Text, txtMaQuyenloi_BHYT.Text,
                                          txtNoiphattheBHYT.Text, txtOthu4.Text, txtOthu5.Text, txtOthu6.Text);
            return SoBHYT;
        }
        public string GetSoBHYT
        {
            get { return SoBHYT; }
            set { SoBHYT = value; }
        }
        /// <summary>
        /// hàm thực hiện việc tính phàn trăm bảo hiểm
        /// </summary>
        public void TinhPtramBHYT()
        {
            try
            {
                LaySoTheBHYT();
                if (!string.IsNullOrEmpty(Laymathe_BHYT()) && Laymathe_BHYT().Length >= 15)
                {
                    if ((!string.IsNullOrEmpty(GetSoBHYT)) && (!string.IsNullOrEmpty(txtNoiDKKCBBD.Text)))
                    {
                        var objLuotkham = new KcbLuotkham();
                        objLuotkham.MaNoicapBhyt = Utility.sDbnull(txtNoiphattheBHYT.Text);
                        objLuotkham.NoiDongtrusoKcbbd = Utility.sDbnull(txtNoiDongtrusoKCBBD.Text);
                        objLuotkham.MatheBhyt = Laymathe_BHYT();
                        objLuotkham.MaDoituongBhyt = txtMaDtuong_BHYT.Text;
                        objLuotkham.DungTuyen = !chkTraiTuyen.Visible ? 1 : (((byte?)(chkTraiTuyen.Checked ? 0 : 1)));
                        objLuotkham.MadtuongSinhsong = txtMaDTsinhsong.myCode;
                        objLuotkham.GiayBhyt = Utility.Bool2byte(chkGiayBHYT.Checked);
                        objLuotkham.MaKcbbd = Utility.sDbnull(txtNoiDKKCBBD.Text);
                        objLuotkham.IdDoituongKcb = objDoituongKCB.IdDoituongKcb;
                        objLuotkham.MaQuyenloi = Utility.Int32Dbnull(txtMaQuyenloi_BHYT.Text);
                        THU_VIEN_CHUNG.TinhPtramBhyt(objLuotkham);
                        txtPtramBHYT.Text = objLuotkham.PtramBhyt.ToString();
                        txtptramDauthe.Text = objLuotkham.PtramBhytGoc.ToString();
                    }
                    else
                    {
                        txtPtramBHYT.Text = "0";
                        txtptramDauthe.Text = "0";
                    }
                }
                else
                {
                    txtPtramBHYT.Text = "0";
                    txtptramDauthe.Text = "0";
                }
            }
            catch (Exception)
            {
                txtPtramBHYT.Text = "0";
                txtptramDauthe.Text = "0";
            }
            finally
            {

            }
        }

        /// <summary>
        /// hàm thực hiện việc load thông tin của nơi khám chữa bệnh ban đầu
        /// </summary>
        private void LoadClinicCode()
        {
            try
            {
                string ma_diachinh = THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_DANGKY_CACHXACDINH_NOIDKKCBBD", true) == "0" ? txtNoiphattheBHYT.Text : txtNoiDongtrusoKCBBD.Text;
                //Lấy mã Cơ sở KCBBD
                string v_CliniCode = ma_diachinh + txtNoiDKKCBBD.Text.Trim();
                string strClinicName = "";
                DataTable dataTable = new KCB_DANGKY().GetClinicCode(v_CliniCode);
                if (dataTable.Rows.Count > 0)
                {
                    strClinicName = dataTable.Rows[0][DmucNoiKCBBD.Columns.TenKcbbd].ToString();
                    Utility.SetMsg(lblClinicName, strClinicName, !string.IsNullOrEmpty(txtNoiDKKCBBD.Text));
                }
                else
                {
                    Utility.SetMsg(lblClinicName, strClinicName, false);
                }
                lblClinicName.Visible = dataTable.Rows.Count > 0;
                lnkThem.Visible = dataTable.Rows.Count <= 0;
                //txtNamePresent.Text = strClinicName;
                //Check đúng tuyến cần lấy mã nơi cấp BHYT+mã kcbbd thay vì mã cơ sở kcbbd
                if (!chkCapCuu.Checked) //Nếu không phải trường hợp cấp cứu
                {
                    if (globalVariables.gv_intBHYT_TUDONGCHECKTRAITUYEN == 1)
                        //Nếu có chế độ tự động kiểm tra trái tuyến đúng tuyến
                        chkTraiTuyen.Checked =
                            !(THU_VIEN_CHUNG.KiemtraDungtuyenTraituyen(txtNoiDongtrusoKCBBD.Text.Trim() +
                                                                    txtNoiDKKCBBD.Text.Trim()) ||
                              (!THU_VIEN_CHUNG.KiemtraDungtuyenTraituyen(txtNoiDongtrusoKCBBD.Text.Trim() +
                                                                      txtNoiDKKCBBD.Text.Trim()) &&
                               Chuyenvien));
                }
                else //Nếu là BN cấp cứu
                {
                    if (globalVariables.gv_intBHYT_TUDONGCHECKTRAITUYEN == 1)
                        //Nếu có chế độ tự động kiểm tra trái tuyến đúng tuyến
                        chkTraiTuyen.Checked =
                            (!(THU_VIEN_CHUNG.KiemtraDungtuyenTraituyen(txtNoiDongtrusoKCBBD.Text.Trim() +
                                                                     txtNoiDKKCBBD.Text.Trim()) ||
                               (!THU_VIEN_CHUNG.KiemtraDungtuyenTraituyen(txtNoiDongtrusoKCBBD.Text.Trim() +
                                                                       txtNoiDKKCBBD.Text.Trim()) &&
                                Chuyenvien))) && (!chkCapCuu.Checked);
                }

                if (txtMaDTsinhsong.myCode != "-1")
                {
                    if (chkTraiTuyen.Checked)
                        chkTraiTuyen.Checked = false;
                }
                TinhPtramBHYT();
            }
            catch (Exception exception)
            {
            }
            finally
            {
                lblTuyenBHYT.Text = chkTraiTuyen.Checked ? "TRÁI TUYẾN" : "ĐÚNG TUYẾN";
            }
        }
        private void txtMaDtuong_BHYT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string MA_BHYT = txtMaDtuong_BHYT.Text.Trim() + txtMaQuyenloi_BHYT.Text.Trim() + txtNoiDongtrusoKCBBD.Text.Trim() +
                                 txtOthu4.Text.Trim() + txtOthu5.Text.Trim() + txtOthu6.Text.Trim();
                if (MA_BHYT.Length == 15 && _OnFindPatientByMatheBHYT != null) _OnFindPatientByMatheBHYT(MA_BHYT);
            }
        }

        private void txtMaQuyenloi_BHYT_KeyDown(object sender, KeyEventArgs e)
        {
            hasjustpressBACKKey = false;
            if (e.KeyCode == Keys.Enter)
            {
                string MA_BHYT = txtMaDtuong_BHYT.Text.Trim() + txtMaQuyenloi_BHYT.Text.Trim() + txtNoiphattheBHYT.Text.Trim() +
                                 txtOthu4.Text.Trim() + txtOthu5.Text.Trim() + txtOthu6.Text.Trim();
                if (MA_BHYT.Length == 15 && _OnFindPatientByMatheBHYT != null) _OnFindPatientByMatheBHYT(MA_BHYT);
                return;
            }
            if (e.KeyCode == Keys.Back)
            {
                hasjustpressBACKKey = true;
                if (txtMaQuyenloi_BHYT.Text.Length <= 0)
                {
                    txtMaDtuong_BHYT.Focus();
                    txtMaDtuong_BHYT.Select(txtMaDtuong_BHYT.Text.Length, 0);
                }
                return;
            }
            if (txtMaQuyenloi_BHYT.Text.Length == 1 && (Char.IsDigit((char)e.KeyCode) || Char.IsLetter((char)e.KeyCode)))
            {
                if (txtNoiphattheBHYT.Text.Length > 0)
                {
                    // txtNoiDongtrusoKCBBD.Text = ((char)e.KeyCode).ToString() + txtNoiDongtrusoKCBBD.Text.Substring(1);
                    txtNoiphattheBHYT.Focus();
                    txtNoiphattheBHYT.SelectAll();
                }
                return;
            }

        }

        private void txtNoiDongtrusoKCBBD_KeyDown(object sender, KeyEventArgs e)
        {
            hasjustpressBACKKey = false;
            if (e.KeyCode == Keys.Enter)
            {
                //Không cần tìm
                //string MA_BHYT = txtMaDtuong_BHYT.Text.Trim() + txtMaQuyenloi_BHYT.Text.Trim() + txtNoiphattheBHYT.Text.Trim() +
                //                 txtOthu4.Text.Trim() + txtOthu5.Text.Trim() + txtOthu6.Text.Trim();
                //if (MA_BHYT.Length == 15) FindPatientIDbyBHYT(MA_BHYT);
                //return;
            }
            else if (e.KeyCode == Keys.Back)
            {
                hasjustpressBACKKey = true;
                if (txtNoiDongtrusoKCBBD.Text.Length <= 0)
                {
                    txtOthu6.Focus();
                    txtOthu6.Select(txtOthu6.Text.Length, 0);
                }
            }
        }

        private void txtOthu4_KeyDown(object sender, KeyEventArgs e)
        {
            hasjustpressBACKKey = false;
            if (e.KeyCode == Keys.Enter)
            {
                string MA_BHYT = txtMaDtuong_BHYT.Text.Trim() + txtMaQuyenloi_BHYT.Text.Trim() + txtNoiphattheBHYT.Text.Trim() +
                                 txtOthu4.Text.Trim() + txtOthu5.Text.Trim() + txtOthu6.Text.Trim();
                if (MA_BHYT.Length == 15 && _OnFindPatientByMatheBHYT!=null) _OnFindPatientByMatheBHYT(MA_BHYT);
                return;
            }
            else if (e.KeyCode == Keys.Back)
            {
                hasjustpressBACKKey = true;
                if (txtOthu4.Text.Length <= 0)
                {
                    txtNoiphattheBHYT.Focus();
                    txtNoiphattheBHYT.Select(txtNoiphattheBHYT.Text.Length, 0);
                }
            }
        }

        private void txtOthu5_KeyDown(object sender, KeyEventArgs e)
        {
            hasjustpressBACKKey = false;
            if (e.KeyCode == Keys.Enter)
            {
                string MA_BHYT = txtMaDtuong_BHYT.Text.Trim() + txtMaQuyenloi_BHYT.Text.Trim() + txtNoiphattheBHYT.Text.Trim() +
                                 txtOthu4.Text.Trim() + txtOthu5.Text.Trim() + txtOthu6.Text.Trim();
                if (MA_BHYT.Length == 15 && _OnFindPatientByMatheBHYT != null) _OnFindPatientByMatheBHYT(MA_BHYT);
            }
            else if (e.KeyCode == Keys.Back)
            {
                hasjustpressBACKKey = true;
                if (txtOthu5.Text.Length <= 0)
                {
                    txtOthu4.Focus();
                    txtOthu4.Select(txtOthu4.Text.Length, 0);
                }
            }
        }

        private void txtOthu6_KeyDown(object sender, KeyEventArgs e)
        {
            hasjustpressBACKKey = false;
            if (e.KeyCode == Keys.Enter)
            {
                string MA_BHYT = txtMaDtuong_BHYT.Text.Trim() + txtMaQuyenloi_BHYT.Text.Trim() + txtNoiphattheBHYT.Text.Trim() +
                                 txtOthu4.Text.Trim() + txtOthu5.Text.Trim() + txtOthu6.Text.Trim();
                if (MA_BHYT.Length == 15 && _OnFindPatientByMatheBHYT != null) _OnFindPatientByMatheBHYT(MA_BHYT);
                return;
            }
            else if (e.KeyCode == Keys.Back)
            {
                hasjustpressBACKKey = true;
                if (txtOthu6.Text.Length <= 0)
                {
                    txtOthu5.Focus();
                    txtOthu5.Select(txtOthu5.Text.Length, 0);
                }
            }
        }

        private void lnkThem_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var newItem = new frm_ThemnoiKCBBD();
            newItem.m_dtDataThanhPho = globalVariables.gv_dtDmucDiachinh;
            newItem.SetInfor(txtNoiDKKCBBD.Text, txtNoiphattheBHYT.Text);
            if (newItem.ShowDialog() == DialogResult.OK)
            {
                txtNoiDKKCBBD.Text = "";
                txtNoiphattheBHYT.Text = "";
                txtNoiDKKCBBD.Text = newItem.txtMa.Text.Trim();
                txtNoiphattheBHYT.Text = newItem.txtMaThanhPho.Text.Trim();
                dtInsFromDate.Focus();
            }
        }
    }
}
