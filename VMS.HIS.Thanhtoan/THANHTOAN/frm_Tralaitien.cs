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
    public partial class frm_Tralaitien : Form
    {
        public int v_Payment_Id = -1;
        public bool m_blnCancel = true;
        public KcbLuotkham objLuotkham;
        public KcbDanhsachBenhnhan objBenhnhan;
        public KcbThanhtoan objKcbThanhtoan;
        public int Id_HD_LOG = -1;
        public int TotalPayment = 0;
        bool m_blnLoaded = false;
        public frm_Tralaitien()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            setProperties();
            this.KeyDown+=new KeyEventHandler(frm_Tralaitien_KeyDown);
            chkNoview.CheckedChanged += new EventHandler(chkNoview_CheckedChanged);
            chkNoview.Checked = PropertyLib._ThanhtoanProperties.Hienthihuythanhtoan;

            cmdInhoadon.Click += new EventHandler(cmdInhoadon_Click);
            cmdInBienlai.Click += new EventHandler(cmdInBienlai_Click);
            cmdClose1.Click += new EventHandler(cmdClose1_Click);
        }

        void cmdClose1_Click(object sender, EventArgs e)
        {
            cmdExit_Click(cmdExit, e);
        }

      
      
        void cmdInBienlaiTonghop_Click(object sender, EventArgs e)
        {
            new INPHIEU_THANHTOAN_NGOAITRU().InBienlai(true, v_Payment_Id, -1, objLuotkham,Utility.ByteDbnull( objLuotkham.Noitru,0));
        }

        void cmdInBienlai_Click(object sender, EventArgs e)
        {
            if (objKcbThanhtoan == null) objKcbThanhtoan = KcbThanhtoan.FetchByID(v_Payment_Id);
            if (objKcbThanhtoan == null)
            {
                Utility.ShowMsg("");
                return;
            }

            new INPHIEU_THANHTOAN_NGOAITRU().InBienlaiPhieuChi(chkIntonghop.Visible && chkIntonghop.Checked, v_Payment_Id, objLuotkham, objKcbThanhtoan.NoiTru);
           

            //new INPHIEU_THANHTOAN_NGOAITRU().InBienlai(false, v_Payment_Id, objLuotkham, Utility.ByteDbnull(objLuotkham.Noitru, 0));
        }

        void cmdInhoadon_Click(object sender, EventArgs e)
        {
            if (!Utility.isValidGrid(grdPaymentDetail))
                return;
            new Update(KcbPhieuthu.Schema).Set(KcbPhieuthu.Columns.TaikhoanCo).EqualTo(Utility.DoTrim(txtCo.Text))
                .Set(KcbPhieuthu.Columns.TaikhoanNo).EqualTo(Utility.DoTrim(txtNo.Text))
                .Where(KcbPhieuthu.Columns.IdPhieuthu).IsEqualTo(Utility.Int64Dbnull(txtIDPhieuthu.Text,-1)).Execute();
            new INPHIEU_THANHTOAN_NGOAITRU().InPhieuchi(v_Payment_Id); 
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
        private void frm_Tralaitien_Load(object sender, EventArgs e)
        {
           List<Control> lstCtrls=new List<Control>();
           txtLydohuy.Init();
            GetData();
            Modifycommands();
            m_blnLoaded = true;
        }
        private  DataTable m_dtPaymentDetail=new DataTable();
        /// <summary>
        /// lấy thông tin dữ liệu
        /// </summary>
        private void GetData()
        {
            try
            {
                if (objBenhnhan == null && objLuotkham != null)
                    objBenhnhan = Utility.getKcbDanhsachBenhnhan(objLuotkham);
                m_dtPaymentDetail = _THANHTOAN.Laychitietthanhtoan(v_Payment_Id, (byte)1);
                m_dtPaymentDetail.AcceptChanges();
                grdPaymentDetail.DataSource = m_dtPaymentDetail;
                KcbThanhtoan objTT = KcbThanhtoan.FetchByID(v_Payment_Id);
                dtpngaythanhtoan.Value = objTT.NgayThanhtoan;
                SetSumTotalProperties();
            }
            catch (Exception ex)
            {
                Utility.CatchException(ex);
            }
           
        }
       public decimal Chuathanhtoan = 0m;
        private void SetSumTotalProperties()
        {
            try
            {
                DataTable dtPhieuchi = SPs.KcbThanhtoanLaythongtinphieuchi(v_Payment_Id).GetDataSet().Tables[0];
                dtpngaythanhtoan.Text = Utility.sDbnull(dtPhieuchi.Rows[0]["sngay_thanhtoan"], DateTime.Now.ToString("dd/MM/yyyy"));
                txtSotien.Text = Utility.sDbnull(dtPhieuchi.Rows[0]["thuc_chi"], DateTime.Now.ToString("dd/MM/yyyy"));
                txtSoCTgoc.Text = Utility.sDbnull(dtPhieuchi.Rows[0][KcbPhieuthu.Columns.SoluongChungtugoc],"");
                txtNo.Text = Utility.sDbnull(dtPhieuchi.Rows[0][KcbPhieuthu.Columns.TaikhoanNo], "");
                txtCo.Text = Utility.sDbnull(dtPhieuchi.Rows[0][KcbPhieuthu.Columns.TaikhoanCo], "");
                txtMaphieu.Text = Utility.sDbnull(dtPhieuchi.Rows[0][KcbPhieuthu.Columns.MaPhieuthu], "");
                txtIDthanhtoan.Text = Utility.sDbnull(dtPhieuchi.Rows[0][KcbPhieuthu.Columns.IdThanhtoan], "");
                txtIDPhieuthu.Text = Utility.sDbnull(dtPhieuchi.Rows[0][KcbPhieuthu.Columns.IdPhieuthu], "");
                txtMathanhtoan.Text = Utility.sDbnull(dtPhieuchi.Rows[0][KcbThanhtoan.Columns.MaThanhtoan], "");
                txtNguoinop.Text = Utility.sDbnull(dtPhieuchi.Rows[0][KcbPhieuthu.Columns.NguoiNop], "");
                txtLydohuy._Text = Utility.sDbnull(dtPhieuchi.Rows[0][KcbPhieuthu.Columns.LydoNop], "");
                txtDichvutralai.Text = Utility.sDbnull(dtPhieuchi.Rows[0][KcbPhieuthu.Columns.NoiDung], "");
                ModifyCommands();
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
                        EditBox txt = control as EditBox;
                        txt.TextChanged += txtEventTongTien_TextChanged;
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
            if (txt.Name != txtIDPhieuthu.Name)
                Utility.FormatCurrencyHIS(txt);
        }
        private void ModifyCommands()
        {
            try
            {
                cmdInhoadon.Enabled = grdPaymentDetail.GetDataRows().Length > 0 && grdPaymentDetail.CurrentRow != null && grdPaymentDetail.CurrentRow.RowType == RowType.Record && objLuotkham != null;
                cmdInBienlai.Visible = grdPaymentDetail.GetDataRows().Length > 0 && grdPaymentDetail.CurrentRow != null && grdPaymentDetail.CurrentRow.RowType == RowType.Record && objLuotkham != null;
                chkIntonghop.Visible = TotalPayment > 1 && objLuotkham != null;
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
        private void frm_Tralaitien_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.F5) cmdPrint.PerformClick();
           // if (e.KeyCode == Keys.F4) cmdInPhieu.PerformClick();
        }
        private void Modifycommands()
        {
            cmdPrint.Enabled = m_dtPaymentDetail.DefaultView.Count > 0;
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
            if (!Utility.Coquyen("thanhtoan_huyphieuchi"))
            {
                Utility.ShowMsg("Bạn không có quyền hủy phiếu chi (thanhtoan_huyphieuchi). Vui lòng liên hệ quản trị hệ thống để được cấp quyền");
                return;
            }
            if (objLuotkham.TrangthaiNoitru >= Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("THANHTOAN_CHAN_THANHTOANNGOAITRU", "2", false), 2))
            {
                Utility.ShowMsg("Bệnh nhân này đã ở trạng thái nội trú nên hệ thống không cho phép hủy phiếu chi ngoại trú nữa");
                return;
            }
            KcbThanhtoan objPayment = KcbThanhtoan.FetchByID(v_Payment_Id);

            if (objPayment != null)
            {
                //Kiểm tra ngày hủy
                int KCB_THANHTOAN_SONGAY_HUYPHIEUCHI = Utility.Int32Dbnull(THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_SONGAY_HUYPHIEUCHI", "0", true), 0);
                int Chenhlech = (int)Math.Ceiling((globalVariables.SysDate.Date - objPayment.NgayThanhtoan.Date).TotalDays);
                if (Chenhlech > KCB_THANHTOAN_SONGAY_HUYPHIEUCHI)
                {
                    Utility.ShowMsg(string.Format("Ngày lập phiếu chi {0} - Ngày hủy phiếu chi {1}. Hệ thống không cho phép bạn hủy phiếu chi đã quá {2} ngày. Cần liên hệ quản trị hệ thống để được trợ giúp", objPayment.NgayThanhtoan.ToString("dd/MM/yyyy"), globalVariables.SysDate.ToString("dd/MM/yyyy"), KCB_THANHTOAN_SONGAY_HUYPHIEUCHI.ToString()));
                    return;
                }
                if (THU_VIEN_CHUNG.Laygiatrithamsohethong("KCB_THANHTOAN_BATNHAPLYDO_HUYTRALAITIEN", "1", false) == "1")
                {
                    frm_Chondanhmucdungchung _Chondanhmucdungchung = new frm_Chondanhmucdungchung("LYDOHUYTRALAITIEN", "Hủy trả lại tiền Bệnh nhân", "Nhập lý do hủy trả lại tiền trước khi thực hiện...", "Lý do hủy trả lại tiền",false);
                    _Chondanhmucdungchung.ShowDialog();
                    if (_Chondanhmucdungchung.m_blnCancel) return;
                    lydo_huy = _Chondanhmucdungchung.ma;
                }
                ActionResult actionResult = _THANHTOAN.HuyPhieuchi(objPayment,objLuotkham, ma_lydohuy);
                switch (actionResult)
                {
                    case ActionResult.Success:
                        Utility.Log(Name, globalVariables.UserName,
                                    string.Format(
                                        "Bán thuốc tại quầy: Hủy phiếu chi của bệnh nhân {0} có mã lần khám {1} và id bệnh nhân: {2} ", objBenhnhan.TenBenhnhan,
                                        objLuotkham.MaLuotkham, objLuotkham.IdBenhnhan),
                                    newaction.CancelData, this.GetType().Assembly.ManifestModule.Name);
                        Utility.ShowMsg("Bạn xóa phiếu chi thành công", "Thông báo");
                        m_blnCancel = false;
                        cmdExit.PerformClick();
                        break;
                    case ActionResult.Error:
                        Utility.ShowMsg("Lỗi trong quá trình hủy phiếu chi", "Thông báo lỗi", MessageBoxIcon.Error);
                        break;
                }
            }


        }

        private void cmdInBienlai_Click_1(object sender, EventArgs e)
        {

        }
       
    }
}
