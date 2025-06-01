using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SubSonic;
using AggregateFunction = Janus.Windows.GridEX.AggregateFunction;
using VNS.Libs;
using VMS.HIS.DAL;
using VNS.HIS.BusRule.Classes;
using VNS.Properties;
using VNS.HIS.UI.Forms.NGOAITRU;
using Janus.Windows.GridEX;
using Janus.Windows.GridEX.EditControls;
using VNS.HIS.Classes;
using VNS.HIS.UI.Forms.Cauhinh;
using VMS.HIS.Bus.BHYT;

namespace VNS.HIS.UI.THANHTOAN
{
    public partial class frm_HuyThanhtoan : Form
    {
        public long v_Payment_Id = -1;
        public bool m_blnCancel = true;
        public KcbLuotkham objLuotkham;
        public int Id_HD_LOG = -1;
        public int TotalPayment = 0;
        public long id_donthuoc = -1;
        bool m_blnLoaded = false;
        string lst_IDLoaithanhtoan = "3";
        public frm_HuyThanhtoan(string lst_IDLoaithanhtoan)
        {
            InitializeComponent();
            this.lst_IDLoaithanhtoan = lst_IDLoaithanhtoan;
            Utility.SetVisualStyle(this);
            setProperties();
            this.KeyDown+=new KeyEventHandler(frm_HuyThanhtoan_KeyDown);
            chkNoview.CheckedChanged += new EventHandler(chkNoview_CheckedChanged);
            chkNoview.Checked = PropertyLib._ThanhtoanProperties.Hienthihuythanhtoan;

            cmdInhoadon.Click += new EventHandler(cmdInhoadon_Click);
            cmdInBienlai.Click += new EventHandler(cmdInBienlai_Click);
            cmdInBienlaiTonghop.Click += new EventHandler(cmdInBienlaiTonghop_Click);
            cmdInphieuDCT.Click += new EventHandler(cmdInphieuDCT_Click);
            cmdInphoiBHYT.Click += new EventHandler(cmdInphoiBHYT_Click);
            cmdClose1.Click += new EventHandler(cmdClose1_Click);
        }

        void cmdClose1_Click(object sender, EventArgs e)
        {
            cmdExit_Click(cmdExit, e);
        }

        void cmdInphoiBHYT_Click(object sender, EventArgs e)
        {
            if (objLuotkham.TrangthaiNoitru > 0)
            {

            }
            else
                InphoiBHYT_Ngoaitru();


        }
         private void InphoiBHYT_Ngoaitru()
        {
            if (!THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb.Value))
                return;
            if (objLuotkham == null)
            {
                Utility.ShowMsg("Bạn cần chọn Bệnh nhân cần thanh toán");
                return;
            }
            if (string.IsNullOrEmpty(objLuotkham.MabenhChinh))
            {
                Utility.ShowMsg("Chưa có mã bệnh ICD. Quay lại phòng khám của bác sĩ để nhập");
                return;
            }
            if (THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb)
              && Utility.Int16Dbnull(objLuotkham.TrangthaiNgoaitru, 0) < 1
              && THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_BHYT_ISKETTHUC", "0", false) == "1")
            {
                Utility.ShowMsg("Bệnh nhân cần được kết thúc trước khi in phôi BHYT");
                return;
            }
            SqlQuery sqlQuery = new Select().From(KcbDangkyKcb.Schema)
                                  .Where(KcbDangkyKcb.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                                  .And(KcbDangkyKcb.Columns.IdBenhnhan).IsEqualTo(objLuotkham.IdBenhnhan)
                                  .And(KcbDangkyKcb.Columns.TrangThai).IsEqualTo(0);
            // yêu cầu kết thúc khám tất cả các phòng khám trước khi in phôi BHYT
            if (THU_VIEN_CHUNG.IsBaoHiem(objLuotkham.IdLoaidoituongKcb) && sqlQuery.GetRecordCount() > 0)
            {
                KcbDangkyKcb objExam = sqlQuery.ExecuteSingle<KcbDangkyKcb>();
                if (objExam != null)
                {
                    DmucKhoaphong objKP = DmucKhoaphong.FetchByID(objExam.IdPhongkham);
                    Utility.ShowMsg(
                        string.Format(
                            "Bệnh nhân Bảo hiểm y tế chưa được kết thúc tại phòng khám ngoại trú {0}, không thể in phôi BHYT",
                            objKP.TenKhoaphong), "Thông báo", MessageBoxIcon.Warning);
                }
                return;
            }
            if (objLuotkham.MaDoituongKcbBhyt == "2")
            {
                sqlQuery = new Select().From<NoitruPhieuravien>()
                    .Where(NoitruPhieuravien.Columns.MaLuotkham).IsEqualTo(objLuotkham.MaLuotkham)
                    .And(NoitruPhieuravien.Columns.IdBenhnhan).IsEqualTo(Utility.Int32Dbnull(objLuotkham.IdBenhnhan))
                    //.And(NoitruPhieuravien.Columns.MaLoaiKcb).IsEqualTo("2")
                    ;
                if (sqlQuery.GetRecordCount() <= 0)
                {
                    Utility.ShowMsg("Bệnh nhân phải ra viện mới chấp nhận xét duyệt tổng hợp\n Mời bạn xem lại", "Thông báo", MessageBoxIcon.Warning);
                    return;
                }
            }
            if (objLuotkham.MaDoituongKcbBhyt == "3")
            {
                Utility.ShowMsg("Người bệnh đã được nhập viện điều trị nội trú nên không thể in phôi BHYT ở chức năng thanh toán ngoại trú", "Thông báo", MessageBoxIcon.Warning);
                return;
            }
            if (objLuotkham.MaDoituongKcbBhyt == "4")
            {
                Utility.ShowMsg("Người bệnh đã được nhập viện điều trị ban ngày nên không thể in phôi BHYT ở chức năng thanh toán ngoại trú", "Thông báo", MessageBoxIcon.Warning);
                return;
            }
            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("BHYT_INPHOI_THANHTOAN", "1", true) == "1")//Được in phôi trước khi thanh toán. Dữ liệu lấy xong sẽ được tính toán % BHYT và các thông tin tiền cùng chi trả
            {
                InPhoiBhyt();
            }
            else//Thanh toán xong mới được in phôi BHYT
            {
                //if (!THANHTOAN_BHYT_INPHIEU()) return;
                InPhoiBhyt();
            }

        }
        private void InPhoiBhyt()
        {
            try
            {
                if (new BHYT_InPhieu().InPhoiBHYT(objLuotkham,dtPaymentDate.Value, "BHYT_InPhoi_01"))
                {
                   
                }

            }
            catch (Exception ex)
            {
                Utility.ShowMsg("Lỗi:" + ex.Message);
            }
            finally
            {
                GC.Collect();
            }
        }
        void cmdInphieuDCT_Click(object sender, EventArgs e)
        {
            new INPHIEU_THANHTOAN_NGOAITRU().InphieuDCT_Benhnhan(objLuotkham, m_dtPaymentDetail);
        }

        void cmdInBienlaiTonghop_Click(object sender, EventArgs e)
        {
            new INPHIEU_THANHTOAN_NGOAITRU().InBienlai(true, v_Payment_Id, id_donthuoc, objLuotkham, noitru);
        }

        void cmdInBienlai_Click(object sender, EventArgs e)
        {
            if (chkIntonghop.Visible && chkIntonghop.Checked)
                new INPHIEU_THANHTOAN_NGOAITRU().InBienlai(true, v_Payment_Id, id_donthuoc, objLuotkham, noitru);
            else
                new INPHIEU_THANHTOAN_NGOAITRU().InBienlai(false, v_Payment_Id, id_donthuoc, objLuotkham, noitru);
        }

        void cmdInhoadon_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdPaymentDetail))
                return;
            new INPHIEU_THANHTOAN_NGOAITRU().IN_HOADON(v_Payment_Id); 
        }
        bool isCancel = false;
        public bool ShowCancel
        {
            set {
                isCancel = value;
                lblLydohuy.Visible = txtLydohuy.Visible = isCancel;
                if (value) pnlHuyThanhtoan.BringToFront();
                else pnlHuyThanhtoan.SendToBack();
            }
            get
            {
                return isCancel;
            }
        }
        void chkNoview_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_blnLoaded) return;
            PropertyLib._ThanhtoanProperties.Hienthihuythanhtoan = chkNoview.Checked;
            PropertyLib.SaveProperty(PropertyLib._ThanhtoanProperties);
        }
        KCB_THANHTOAN _THANHTOAN = new KCB_THANHTOAN();
        /// <summary>
        /// /hàm thực hiện load thông tin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_HuyThanhtoan_Load(object sender, EventArgs e)
        {
           List<Control> lstCtrls=new List<Control>();
           txtLydohuy.Init();
            GetData();
            //pnlInfor.Height = 209;
            //if (objLuotkham.IdLoaidoituongKcb == 1)
            //{
            //    pnlInfor.Height = 100;
            //    lstCtrls.AddRange(new Control[] {pnlActions, lblNgaythanhtoan, dtPaymentDate, lblTongtien, txtTongChiPhi });
            //}
            //Visible(lstCtrls);
            ModifyComamd();
            m_blnLoaded = true;
        }
        byte noitru = 0;
        private  DataTable m_dtPaymentDetail=new DataTable();
        /// <summary>
        /// lấy thông tin dữ liệu
        /// </summary>
        private void GetData()
        {
            m_dtPaymentDetail = _THANHTOAN.Laychitietthanhtoan(v_Payment_Id,(byte)0);
            if (m_dtPaymentDetail.Rows.Count > 0)
                noitru = Utility.ByteDbnull(m_dtPaymentDetail.Rows[0]["noi_tru"], 0);
            m_dtPaymentDetail.AcceptChanges();
            grdPaymentDetail.DataSource = m_dtPaymentDetail;
            KcbThanhtoan objTT = KcbThanhtoan.FetchByID(v_Payment_Id);
            dtPaymentDate.Value = objTT.NgayThanhtoan;
            SetSumTotalProperties();
        }
       public decimal Chuathanhtoan = 0m;
        private void SetSumTotalProperties()
        {
            try
            {
                GridEXColumn gridExColumntrangthaithanhtoan = getGridExColumn(grdPaymentDetail, "trangthai_thanhtoan");
                GridEXColumn gridExColumn = getGridExColumn(grdPaymentDetail, "TT_KHONG_PHUTHU");
                GridEXColumn gridExColumn_tutuc = getGridExColumn(grdPaymentDetail, "TT_BN_KHONG_TUTUC");
                GridEXColumn gridExColumnTT = getGridExColumn(grdPaymentDetail, "TT");
                GridEXColumn gridExColumnTT_chietkhau = getGridExColumn(grdPaymentDetail, KcbThanhtoanChitiet.Columns.TienChietkhau);
                GridEXColumn gridExColumnBHYT = getGridExColumn(grdPaymentDetail, "TT_BHYT");
                GridEXColumn gridExColumnTTBN = getGridExColumn(grdPaymentDetail, "TT_BN");
                GridEXColumn gridExColumntutuc = getGridExColumn(grdPaymentDetail, "tu_tuc");
                GridEXColumn gridExColumntrangthai_huy = getGridExColumn(grdPaymentDetail, "trangthai_huy");
                GridEXColumn gridExColumnPhuThu = getGridExColumn(grdPaymentDetail,
                    "TT_PHUTHU");
                var gridExFilterCondition_khong_Tutuc =
                    new GridEXFilterCondition(gridExColumntutuc, ConditionOperator.Equal, 0);
                var gridExFilterConditionTutuc =
                    new GridEXFilterCondition(gridExColumntutuc, ConditionOperator.Equal, 1);
                var gridExFilterChuathanhtoan =
                    new GridEXFilterCondition(gridExColumntrangthaithanhtoan, ConditionOperator.Equal, 0);
                var gridExFilterDathanhtoan =
                  new GridEXFilterCondition(gridExColumntrangthaithanhtoan, ConditionOperator.Equal, 1);
                var gridExFilterCondition_TuTuc =
                   new GridEXFilterCondition(gridExColumntutuc, ConditionOperator.Equal, 1);

                var gridExFilterConditionKhongTuTuc =
                    new GridEXFilterCondition(gridExColumntutuc, ConditionOperator.Equal, 0);
                var gridExFilterConditiontrangthai_huy =
                    new GridEXFilterCondition(gridExColumntrangthai_huy, ConditionOperator.Equal, 0);
                var gridExFilterConditiontrangthai_huy_va_khongtutuc =
                   new GridEXFilterCondition(gridExColumntrangthai_huy, ConditionOperator.Equal, 0);
                gridExFilterConditiontrangthai_huy_va_khongtutuc.AddCondition(gridExFilterConditionKhongTuTuc);
                GridEXColumn gridExColumnBNCT = getGridExColumn(grdPaymentDetail,
                    "bnhan_chitra");
                // Janus.Windows.GridEX.GridEXColumn gridExColumnTuTuc = getGridExColumn(grdPaymentDetail, "bnhan_chitra");
                decimal BN_KHONGTUTUC =
                  Utility.DecimaltoDbnull(grdPaymentDetail.GetTotal(gridExColumn_tutuc, AggregateFunction.Sum),
                      gridExFilterCondition_khong_Tutuc);
                decimal TT =
                    Utility.DecimaltoDbnull(grdPaymentDetail.GetTotal(gridExColumnTT, AggregateFunction.Sum));//, gridExFilterConditiontrangthai_huy);
                decimal TT_Chietkhau =
                   Utility.DecimaltoDbnull(grdPaymentDetail.GetTotal(gridExColumnTT_chietkhau, AggregateFunction.Sum));//, gridExFilterConditiontrangthai_huy);

                decimal TT_KHONG_PHUTHU =
                   Utility.DecimaltoDbnull(grdPaymentDetail.GetTotal(gridExColumn, AggregateFunction.Sum));//, gridExFilterConditiontrangthai_huy);
                decimal TT_BHYT =
                    Utility.DecimaltoDbnull(grdPaymentDetail.GetTotal(gridExColumnBHYT, AggregateFunction.Sum));//, gridExFilterConditiontrangthai_huy));
                decimal TT_BN =
                    Utility.DecimaltoDbnull(grdPaymentDetail.GetTotal(gridExColumnTTBN, AggregateFunction.Sum));//, gridExFilterConditiontrangthai_huy));
               
                Chuathanhtoan =
                   Utility.DecimaltoDbnull(grdPaymentDetail.GetTotal(gridExColumnTTBN, AggregateFunction.Sum,
                       gridExFilterChuathanhtoan));
                //Tạm bỏ
                //decimal PtramBHYT = 0;
                //_THANHTOAN.LayThongPtramBHYT(TongChiphiBHYT, objLuotkham, ref PtramBHYT);
                decimal PhuThu =
                    Utility.DecimaltoDbnull(grdPaymentDetail.GetTotal(gridExColumnPhuThu, AggregateFunction.Sum));
                decimal TuTuc =
                    Utility.DecimaltoDbnull(grdPaymentDetail.GetTotal(gridExColumnBNCT, AggregateFunction.Sum,
                        gridExFilterConditionTutuc));
                //txtPtramBHChiTra.Text = Utility.sDbnull(PtramBHYT);
                txtTongChiPhi.Text = Utility.sDbnull(TT);
                TT_KHONG_PHUTHU -= TuTuc;
                txtTongtienDCT.Text = objLuotkham.MaDoituongKcb == "DV" ? "0" : Utility.sDbnull(TT_BHYT + BN_KHONGTUTUC);// objLuotkham.MaDoituongKcb == "DV" ? "0" : Utility.sDbnull(TT_KHONG_PHUTHU);
                txtPhuThu.Text = Utility.sDbnull(PhuThu);
                txtTuTuc.Text = Utility.sDbnull(TuTuc);
                //decimal BHCT = TongChiphiBHYT*PtramBHYT/100;
                txtBHCT.Text = Utility.sDbnull(TT_BHYT, "0");
                decimal BNCT = BN_KHONGTUTUC;
                txtBNCT.Text = Utility.sDbnull(BNCT);
                decimal BNPhaiTra = BNCT + Utility.DecimaltoDbnull(txtTuTuc.Text, 0) +
                                    Utility.DecimaltoDbnull(txtPhuThu.Text);
                txtBNPhaiTra.Text = Utility.sDbnull(TT_BN);
                SysSystemParameter _objLabel = new Select().From(SysSystemParameter.Schema).Where(SysSystemParameter.Columns.SName).IsEqualTo("THANHTOAN_THUATHIEU").ExecuteSingle<SysSystemParameter>();
                KcbThanhtoanCollection _item = new KcbThanhtoanController().FetchByID(v_Payment_Id);
                decimal tamung = Utility.DecimaltoDbnull(SPs.ThanhtoanLaythongtinhoanungtheothanhtoan(v_Payment_Id).GetDataSet().Tables[0].Rows[0][0], 0);
                txtTamung.Text = Utility.sDbnull(tamung);
                if (!_item.Any())
                {
                    txtsotiendathu.Text = "0";
                    txtDachietkhau.Text = "0";
                    txtTamung.Text = "0";
                    txtMGHD.Text = "0";
                }
                else
                {
                    txtDachietkhau.Text = _item.FirstOrDefault().TongtienChietkhau.ToString();
                    decimal sotienthu= Utility.DecimaltoDbnull(_item.FirstOrDefault().BnhanChitra,0)
                        + Utility.DecimaltoDbnull(_item.FirstOrDefault().PhuThu,0)
                        + Utility.DecimaltoDbnull(_item.FirstOrDefault().TuTuc,0)
                        - Utility.DecimaltoDbnull(txtDachietkhau.Text, 0)
                        - Utility.DecimaltoDbnull(txtTamung.Text, 0);
                   
                    txtsotiendathu.Text =Math.Abs(sotienthu).ToString();
                    if (sotienthu > 0)
                    {
                        lblDathu.Text = _objLabel == null ? @"Thu tiền: " : _objLabel.SValue.Split(';')[0];
                        lblDathu.ForeColor = Color.DarkBlue;
                    }
                    else
                    {
                        lblDathu.ForeColor = Color.DarkRed;
                        lblDathu.Text = _objLabel == null ? @"Trả lại: " : _objLabel.SValue.Split(';')[1];
                    }
                    txtMGHD.Text = _item.FirstOrDefault().TongtienChietkhauHoadon.ToString();
                }
                ModifyCommand();
            }
            catch
            { }
        }
        private void setProperties()
        {
            try
            {
                foreach (Control control in pnlInfor.Controls)
                {
                    if (control is EditBox)
                    {
                        var txtFormantTongTien = new EditBox();
                        txtFormantTongTien = ((EditBox)(control));
                        txtFormantTongTien.Clear();
                        txtFormantTongTien.ReadOnly = true;
                        txtFormantTongTien.TextAlignment = TextAlignment.Far;
                        txtFormantTongTien.TextChanged += txtEventTongTien_TextChanged;
                    }
                }
               
            }
            catch (Exception exception)
            {
            }
        }
        private void txtEventTongTien_TextChanged(Object sender, EventArgs e)
        {
            var txt = ((EditBox)(sender));
            Utility.FormatCurrencyHIS(txt);
        }
        private void ModifyCommand()
        {
            try
            {
                cmdInhoadon.Enabled = grdPaymentDetail.GetDataRows().Length > 0 && grdPaymentDetail.CurrentRow != null && grdPaymentDetail.CurrentRow.RowType == RowType.Record && objLuotkham != null;
                cmdInBienlai.Visible = grdPaymentDetail.GetDataRows().Length > 0 && grdPaymentDetail.CurrentRow != null && grdPaymentDetail.CurrentRow.RowType == RowType.Record && objLuotkham != null;
                cmdInphoiBHYT.Visible = Chuathanhtoan <= 0 && Utility.DecimaltoDbnull(txtsotiendathu.Text) > 0 && objLuotkham.MaDoituongKcb == "BHYT" && grdPaymentDetail.GetDataRows().Length > 0 && objLuotkham != null;
                cmdInphieuDCT.Visible = objLuotkham.MaDoituongKcb == "BHYT" && grdPaymentDetail.GetDataRows().Length > 0 && objLuotkham != null;
                if (TotalPayment > 1 && objLuotkham != null)
                {
                    string _value = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_KIEUHIEUTHI_INBIENLAITONGHOP", "0", false);
                    if (_value == "0")
                    {
                        cmdInBienlaiTonghop.Visible = true;
                        chkIntonghop.Visible = false;
                    }
                    else
                    {
                        cmdInBienlaiTonghop.Visible = false;
                        chkIntonghop.Visible = true;
                    }

                }
                else
                {
                    cmdInBienlaiTonghop.Visible = false;
                    chkIntonghop.Visible = false;
                    chkIntonghop.Checked = false;
                }
            }
            catch (Exception)
            {
                //throw;
            }
        }
        void Visible(List<Control> lstCtrls)
        {
            bool _visible = lstCtrls.Count <= 0;
            foreach (Control ctrl in pnlInfor.Controls)
            {
                ctrl.Visible =_visible || lstCtrls.Contains(ctrl);
            }
        }
        private GridEXColumn getGridExColumn(GridEX gridEx, string colName)
        {
            return gridEx.RootTable.Columns[colName];
        }
        /// <summary>
        /// hàm thực hiện thoát khỏi form hienj tại
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// hàm thự hiện việc hủy thanh toán
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_HuyThanhtoan_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.Control && e.KeyCode == Keys.Delete) cmdHuythanhtoan.PerformClick();
           // if (e.KeyCode == Keys.F4) cmdInPhieu.PerformClick();
        }
        private void ModifyComamd()
        {
            cmdHuythanhtoan.Enabled = m_dtPaymentDetail.DefaultView.Count > 0;
        }
        string ma_lydohuy = "";
        string lydo_huy = "";
        private ActionResult actionResult = ActionResult.Error;
        /// <summary>
        /// hàm thực hiện việc hủy thanh toán 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdPrint_Click(object sender, EventArgs e)
        {
             KcbThanhtoan objPayment = KcbThanhtoan.FetchByID(v_Payment_Id);
             if (objPayment.KieuThanhtoan == 0)
                 Huythanhtoan(objPayment);
             else
                 Huytamthu(objPayment);

        }
        void Huytamthu(KcbThanhtoan objPayment)
        {
            if (!Utility.Coquyen("thanhtoan_huytamthu"))
            {
                Utility.ShowMsg("Bạn không có quyền hủy tạm thu (thanhtoan_huytamthu). Vui lòng liên hệ quản trị hệ thống để được cấp quyền");
                return;
            }
            if (objLuotkham.Noitru == 0 && objLuotkham.TrangthaiNoitru >= Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_CHAN_THANHTOANNGOAITRU", "2", false), 2))//Chỉ ngoại trú mới thông báo
            {
                Utility.ShowMsg("Bệnh nhân này đã ở trạng thái nội trú nên hệ thống không cho phép hủy phiếu thu-chi ngoại trú nữa");
                return;
            }
            //Kiểm tra ngày hủy
            int KCB_THANHTOAN_SONGAY_HUYTHANHTOAN = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_SONGAY_HUYTHANHTOAN", "0", true), 0);
            int Chenhlech = (int)Math.Ceiling((globalVariables.SysDate.Date - objPayment.NgayThanhtoan.Date).TotalDays);
            if (Chenhlech > KCB_THANHTOAN_SONGAY_HUYTHANHTOAN)
            {
                Utility.ShowMsg(string.Format("Ngày tạm thu {0} - Ngày hủy tạm thu {1}. Hệ thống không cho phép bạn hủy tạm thu đã quá {2} ngày. Cần liên hệ quản trị hệ thống để được trợ giúp", objPayment.NgayThanhtoan.ToString("dd/MM/yyyy"), globalVariables.SysDate.ToString("dd/MM/yyyy"), KCB_THANHTOAN_SONGAY_HUYTHANHTOAN.ToString()));
                return;
            }
            if (PropertyLib._ThanhtoanProperties.Hoitruockhihuythanhtoan)
                if (!Utility.AcceptQuestion(string.Format("Bạn có muốn hủy lần tạm thu với Mã tạm thu {0}", v_Payment_Id.ToString()), "Thông báo", true))
                {
                    return;
                }
            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_BATNHAPLYDO_HUYTHANHTOAN", "1", false) == "1")
            {
                if (ShowCancel && txtLydohuy.myCode == "-1")
                {
                    Utility.ShowMsg("Bạn phải chọn lý do hủy", "Thông báo");
                    txtLydohuy.Focus();
                    return;
                }
                ma_lydohuy = txtLydohuy.myCode;
                //frm_Chondanhmucdungchung _Nhaplydohuythanhtoan = new frm_Chondanhmucdungchung("LYDOHUYTHANHTOAN", "Hủy tạm thu tiền Bệnh nhân", "Nhập lý do hủy tạm thu trước khi thực hiện...", "Lý do hủy tạm thu");
                //_Nhaplydohuythanhtoan.ShowDialog();
                //m_blnCancel = _Nhaplydohuythanhtoan.m_blnCancel;
                //if(m_blnCancel) return;
                //ma_lydohuy = _Nhaplydohuythanhtoan.ma;
            }
            bool HUYTHANHTOAN_HUYBIENLAI = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_HUYTHANHTOAN_HUYBIENLAI", "1", true) == "1";
            actionResult = _THANHTOAN.HuyTamthu(objPayment, objLuotkham, ma_lydohuy, Utility.Int32Dbnull(grdPaymentDetail.CurrentRow.Cells[KcbThanhtoan.Columns.IdHdonLog], -1), HUYTHANHTOAN_HUYBIENLAI);
            int record = -1;
            switch (actionResult)
            {
                case ActionResult.Success:
                    ModifyComamd();
                    Utility.ShowMsg("Bạn hủy thông tin tạm thu thành công", "Thông báo");
                    m_blnCancel = false;
                    cmdExit.PerformClick();
                    break;
                case ActionResult.DataUsed:
                    //Utility.ShowMsg("Thuốc đã cấp phát cho Bệnh nhân nên cần trả lại thuốc bên Dược mới có thể thực hiện hủy tạm thu", "Thông báo", MessageBoxIcon.Warning);
                    break;
                case ActionResult.ExistedRecord:
                    //Utility.ShowMsg("Thuốc đã cấp phát cho Bệnh nhân nên cần trả lại thuốc bên Dược mới có thể thực hiện hủy tạm thu", "Thông báo", MessageBoxIcon.Warning);
                    break;
                case ActionResult.Error:
                    Utility.ShowMsg("Lỗi trong quá trình hủy thông tin tạm thu", "Thông báo", MessageBoxIcon.Error);
                    break;
            }
        }
        void Huythanhtoan( KcbThanhtoan objPayment)
        {
            if (!Utility.Coquyen("thanhtoan_huythanhtoan"))
            {
                Utility.ShowMsg("Bạn không có quyền hủy thanh toán (thanhtoan_huythanhtoan). Vui lòng liên hệ quản trị hệ thống để được cấp quyền");
                return;
            }
            if (objLuotkham.Noitru == 0 && objLuotkham.TrangthaiNoitru >= Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_CHAN_THANHTOANNGOAITRU", "2", false), 2))//Chỉ ngoại trú mới thông báo
            {
                Utility.ShowMsg("Bệnh nhân này đã ở trạng thái nội trú nên hệ thống không cho phép hủy phiếu thu-chi ngoại trú nữa");
                return;
            }
            //Kiểm tra ngày hủy
            int KCB_THANHTOAN_SONGAY_HUYTHANHTOAN = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_SONGAY_HUYTHANHTOAN", "0", true), 0);
            int Chenhlech = (int)Math.Ceiling((globalVariables.SysDate.Date - objPayment.NgayThanhtoan.Date).TotalDays);
            if (Chenhlech > KCB_THANHTOAN_SONGAY_HUYTHANHTOAN)
            {
                Utility.ShowMsg(string.Format("Ngày thanh toán {0} - Ngày hủy thanh toán {1}. Hệ thống không cho phép bạn hủy thanh toán đã quá {2} ngày. Cần liên hệ quản trị hệ thống để được trợ giúp", objPayment.NgayThanhtoan.ToString("dd/MM/yyyy"), globalVariables.SysDate.ToString("dd/MM/yyyy"), KCB_THANHTOAN_SONGAY_HUYTHANHTOAN.ToString()));
                return;
            }
            if (PropertyLib._ThanhtoanProperties.Hoitruockhihuythanhtoan)
                if (!Utility.AcceptQuestion(string.Format("Bạn có muốn hủy lần thanh toán với Mã thanh toán {0}", v_Payment_Id.ToString()), "Thông báo", true))
                {
                    return;
                }
            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_BATNHAPLYDO_HUYTHANHTOAN", "1", false) == "1")
            {
                if (ShowCancel && txtLydohuy.myCode == "-1")
                {
                    Utility.ShowMsg("Bạn phải chọn lý do hủy", "Thông báo");
                    txtLydohuy.Focus();
                    return;
                }
                ma_lydohuy = txtLydohuy.myCode;
                //frm_Chondanhmucdungchung _Nhaplydohuythanhtoan = new frm_Chondanhmucdungchung("LYDOHUYTHANHTOAN", "Hủy thanh toán tiền Bệnh nhân", "Nhập lý do hủy thanh toán trước khi thực hiện...", "Lý do hủy thanh toán");
                //_Nhaplydohuythanhtoan.ShowDialog();
                //m_blnCancel = _Nhaplydohuythanhtoan.m_blnCancel;
                //if(m_blnCancel) return;
                //ma_lydohuy = _Nhaplydohuythanhtoan.ma;
            }
            bool HUYTHANHTOAN_HUYBIENLAI = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_HUYTHANHTOAN_HUYBIENLAI", "1", true) == "1";
            actionResult = _THANHTOAN.HuyThanhtoan(objPayment, objLuotkham, ma_lydohuy, Utility.Int32Dbnull(grdPaymentDetail.CurrentRow.Cells[KcbThanhtoan.Columns.IdHdonLog], -1), HUYTHANHTOAN_HUYBIENLAI);
            int record = -1;
            switch (actionResult)
            {
                case ActionResult.Success:
                    ModifyComamd();
                    Utility.ShowMsg("Bạn hủy thông tin thanh toán thành công", "Thông báo");
                    m_blnCancel = false;
                    cmdExit.PerformClick();
                    break;
                case ActionResult.DataUsed:
                    //Utility.ShowMsg("Thuốc đã cấp phát cho Bệnh nhân nên cần trả lại thuốc bên Dược mới có thể thực hiện hủy thanh toán", "Thông báo", MessageBoxIcon.Warning);
                    break;
                case ActionResult.ExistedRecord:
                    //Utility.ShowMsg("Thuốc đã cấp phát cho Bệnh nhân nên cần trả lại thuốc bên Dược mới có thể thực hiện hủy thanh toán", "Thông báo", MessageBoxIcon.Warning);
                    break;
                case ActionResult.Error:
                    Utility.ShowMsg("Lỗi trong quá trình hủy thông tin thanh toán", "Thông báo", MessageBoxIcon.Error);
                    break;
            }
        }
        
        private void cmdInBangkeCPKCB_Click(object sender, EventArgs e)
        {
            new INPHIEU_THANHTOAN_NGOAITRU().In_Bangke_CPKCB(v_Payment_Id, true, objLuotkham.Noitru.Value);
        }

        private void cmdInBienlai_Click_1(object sender, EventArgs e)
        {

        }
       
    }
}
