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
namespace VNS.HIS.UI.THANHTOAN
{
    public partial class frm_HuyThanhtoan_Quaythuoc : Form
    {
        public int v_Payment_Id = -1;
        public bool m_blnCancel = true;
        public int Id_HD_LOG = -1;
        public int TotalPayment = 0;
        bool m_blnLoaded = false;
        public long id_donthuoc = -1;
        KcbDanhsachBenhnhan objBenhnhan = null;
        public frm_HuyThanhtoan_Quaythuoc()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            setProperties();
            this.KeyDown+=new KeyEventHandler(frm_HuyThanhtoan_Quaythuoc_KeyDown);
            chkNoview.CheckedChanged += new EventHandler(chkNoview_CheckedChanged);
            chkNoview.Checked = PropertyLib._ThanhtoanProperties.Hienthihuythanhtoan;

            cmdInhoadon.Click += new EventHandler(cmdInhoadon_Click);
            cmdInBienlai.Click += new EventHandler(cmdInBienlai_Click);
            cmdInBienlaiTonghop.Click += new EventHandler(cmdInBienlaiTonghop_Click);
            cmdClose1.Click += new EventHandler(cmdClose1_Click);
        }

        void cmdClose1_Click(object sender, EventArgs e)
        {
            cmdExit_Click(cmdExit, e);
        }

       


        void cmdInBienlaiTonghop_Click(object sender, EventArgs e)
        {
            new INPHIEU_THANHTOAN_NGOAITRU().InBienlaiQuaythuoc(true, v_Payment_Id, id_donthuoc);
        }

        void cmdInBienlai_Click(object sender, EventArgs e)
        {
            new INPHIEU_THANHTOAN_NGOAITRU().InBienlaiQuaythuoc(false, v_Payment_Id, id_donthuoc);
        }

        void cmdInhoadon_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdPaymentDetail))
                return;
            new INPHIEU_THANHTOAN_NGOAITRU().IN_HOADON(v_Payment_Id); 
        }
      
        public bool ShowCancel
        {
            set {
                if (value) pnlHuyThanhtoan.BringToFront();
                else pnlHuyThanhtoan.SendToBack();
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
        private void frm_HuyThanhtoan_Quaythuoc_Load(object sender, EventArgs e)
        {
           List<Control> lstCtrls=new List<Control>();
            GetData();
            pnlInfor.Height = 209;
            if (true)
            {
                pnlInfor.Height = 91;
                lstCtrls.AddRange(new Control[] {pnlActions, lblNgaythanhtoan, dtPaymentDate, lblTongtien, txtTongChiPhi });
            }
            Visible(lstCtrls);
            ModifyComamd();
            m_blnLoaded = true;
        }
        private  DataTable m_dtPaymentDetail=new DataTable();
        /// <summary>
        /// lấy thông tin dữ liệu
        /// </summary>
        private void GetData()
        {
            m_dtPaymentDetail = _THANHTOAN.Laychitietthanhtoan(v_Payment_Id,(byte)0);
            m_dtPaymentDetail.AcceptChanges();
            grdPaymentDetail.DataSource = m_dtPaymentDetail;
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
                    Utility.DecimaltoDbnull(grdPaymentDetail.GetTotal(gridExColumnTT, AggregateFunction.Sum),
                        gridExFilterConditiontrangthai_huy);
                decimal TT_Chietkhau =
                   Utility.DecimaltoDbnull(grdPaymentDetail.GetTotal(gridExColumnTT_chietkhau, AggregateFunction.Sum),
                       gridExFilterConditiontrangthai_huy);

                decimal TT_KHONG_PHUTHU =
                   Utility.DecimaltoDbnull(grdPaymentDetail.GetTotal(gridExColumn, AggregateFunction.Sum),
                       gridExFilterConditiontrangthai_huy);
                decimal TT_BHYT =
                    Utility.DecimaltoDbnull(grdPaymentDetail.GetTotal(gridExColumnBHYT, AggregateFunction.Sum,
                        gridExFilterConditiontrangthai_huy));
                decimal TT_BN =
                    Utility.DecimaltoDbnull(grdPaymentDetail.GetTotal(gridExColumnTTBN, AggregateFunction.Sum,
                        gridExFilterConditiontrangthai_huy));
               
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
                txtTongtienDCT.Text = "0";
                txtPhuThu.Text = Utility.sDbnull(PhuThu);
                txtTuTuc.Text = Utility.sDbnull(TuTuc);
                //decimal BHCT = TongChiphiBHYT*PtramBHYT/100;
                txtBHCT.Text = Utility.sDbnull(TT_BHYT, "0");
                decimal BNCT = BN_KHONGTUTUC;
                txtBNCT.Text = Utility.sDbnull(BNCT);
                decimal BNPhaiTra = BNCT + Utility.DecimaltoDbnull(txtTuTuc.Text, 0) +
                                    Utility.DecimaltoDbnull(txtPhuThu.Text);
                txtBNPhaiTra.Text = Utility.sDbnull(TT_BN);

                KcbThanhtoanCollection _item = new KcbThanhtoanController().FetchByID(v_Payment_Id);
                if (!_item.Any())
                {
                    txtsotiendathu.Text = "0";
                    txtDachietkhau.Text = "0";
                }
                else
                {
                    txtDachietkhau.Text = _item.FirstOrDefault().TongtienChietkhau.ToString();
                    txtsotiendathu.Text = (Utility.DecimaltoDbnull(_item.FirstOrDefault().BnhanChitra, 0)
                        + Utility.DecimaltoDbnull(_item.FirstOrDefault().PhuThu, 0)
                        + Utility.DecimaltoDbnull(_item.FirstOrDefault().TuTuc, 0)
                        - Utility.DecimaltoDbnull(txtDachietkhau.Text, 0)).ToString();
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
                cmdInhoadon.Enabled = grdPaymentDetail.GetDataRows().Length > 0 && grdPaymentDetail.CurrentRow != null && grdPaymentDetail.CurrentRow.RowType == RowType.Record && objBenhnhan != null;
                cmdInBienlai.Visible = grdPaymentDetail.GetDataRows().Length > 0 && grdPaymentDetail.CurrentRow != null && grdPaymentDetail.CurrentRow.RowType == RowType.Record && objBenhnhan != null;
                cmdInBienlaiTonghop.Visible = TotalPayment > 1 && objBenhnhan != null;
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
        private void frm_HuyThanhtoan_Quaythuoc_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.F5) cmdPrint.PerformClick();
           // if (e.KeyCode == Keys.F4) cmdInPhieu.PerformClick();
        }
        private void ModifyComamd()
        {
            cmdPrint.Enabled = m_dtPaymentDetail.DefaultView.Count > 0;
        }
        string ma_lydohuy = "";
        private ActionResult actionResult = ActionResult.Error;
        /// <summary>
        /// hàm thực hiện việc hủy thanh toán 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdPrint_Click(object sender, EventArgs e)
        {
            if (THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_BATNHAPLYDO_HUYTHANHTOAN", "1", false) == "1")
            {
                frm_Chondanhmucdungchung _Nhaplydohuythanhtoan = new frm_Chondanhmucdungchung("LYDOHUYTHANHTOAN", "Hủy thanh toán tiền Bệnh nhân", "Nhập lý do hủy thanh toán trước khi thực hiện...", "Lý do hủy thanh toán",false);
                _Nhaplydohuythanhtoan.ShowDialog();
                m_blnCancel = _Nhaplydohuythanhtoan.m_blnCancel;
                if(m_blnCancel) return;
                ma_lydohuy = _Nhaplydohuythanhtoan.ma;
            }
            bool HUYTHANHTOAN_HUYBIENLAI = THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_HUYTHANHTOAN_HUYBIENLAI", "1", true) == "1";
            actionResult = _THANHTOAN.HuyThanhtoanDonthuoctaiquay(v_Payment_Id, null, ma_lydohuy, Utility.Int32Dbnull(grdPaymentDetail.CurrentRow.Cells[KcbThanhtoan.Columns.IdHdonLog], -1), HUYTHANHTOAN_HUYBIENLAI);
            int record = -1;
            switch (actionResult)
            {
                case ActionResult.Success:
                    ModifyComamd();
                    Utility.ShowMsg("Bạn hủy thông tin thanh toán thành công", "Thông báo");
                    m_blnCancel = false;
                    cmdExit.PerformClick();
                    break;
                case ActionResult.ExistedRecord:
                    Utility.ShowMsg("Thuốc đã cấp phát cho Bệnh nhân nên cần trả lại thuốc bên Dược mới có thể thực hiện hủy thanh toán", "Thông báo", MessageBoxIcon.Warning);
                    break;
                case ActionResult.Error:
                    Utility.ShowMsg("Lỗi trong quá trình hủy thông tin thanh toán", "Thông báo", MessageBoxIcon.Error);
                    break;
            }
            //Rem lại
            //if (record <= 0 && Id_HD_LOG > 0)
            //{
            //   Utility.ShowMsg("Có lỗi trong quá trình hủy hóa đơn thanh toán.");
            //}

        }
       
    }
}
