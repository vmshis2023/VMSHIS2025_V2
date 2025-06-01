using System;
using System.Linq;
using Janus.Windows.EditControls;
using Janus.Windows.GridEX.EditControls;
using Microsoft.VisualBasic;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using CrystalDecisions.CrystalReports.Engine;
using SubSonic;

using VNS.Libs;
using VMS.HIS.DAL;


using NLog;
using TextAlignment = Janus.Windows.GridEX.TextAlignment;
using VNS.HIS.Classes;
using VNS.HIS.BusRule.Classes;

namespace VNS.HIS.UI.THANHTOAN
{
    public partial class frm_KiemTra_BHYT : Form
    {
        private NLog.Logger log;
        public KcbLuotkham objLuotkham;
        public string tieude { get; set; }
        private DataTable m_dtPaymentDetail = new DataTable();
        public frm_KiemTra_BHYT()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            setProperties();
            //BusinessHelper.GetTieuDeBaoCao(this.Name, txtTieuDe.Text);
            dtNgayIn.Value = globalVariables.SysDate;
            log = LogManager.GetCurrentClassLogger();
            
            this.KeyDown+=new KeyEventHandler(frm_KiemTra_BHYT_KeyDown);
           // txtTieuDe.LostFocus+=new EventHandler(txtTieuDe_LostFocus);
        }
        /// <summary>
        /// phím tắt trong form thực hiện khi su dụng phím tắt cho thao tác đơn giản hơn
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_KiemTra_BHYT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) SendKeys.Send("{TAB}");
            if (e.KeyCode == Keys.Escape) cmdExit.PerformClick();
            if (e.KeyCode == Keys.F5) cmdPrintAll.PerformClick();
            if (e.KeyCode == Keys.F4) cmdInPhieu_DongChiTra.PerformClick();
          //  if (e.KeyCode == Keys.F6) cmdINPHIEU_CHOBENHNHAN.PerformClick();
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
        private void txtEventTongTien_KeyPress(Object sender, KeyPressEventArgs e)
        {
            var txtTongTien =
                ((EditBox)(sender));
            Utility.OnlyDigit(e);
        }

        private void txtEventTongTien_TextChanged(Object sender, EventArgs e)
        {
            var txtTongTien =
                ((EditBox)(sender));
            Utility.FormatCurrencyHIS(txtTongTien);
        }
       
        private void setProperties()
        {
            foreach (Control control in grpTongTien.Controls)
            {
                if (control is Janus.Windows.GridEX.EditControls.EditBox)
                {
                    
                    Janus.Windows.GridEX.EditControls.EditBox txtTien = new EditBox();
                    txtTien = (Janus.Windows.GridEX.EditControls.EditBox)control;
                    if (!txtTien.Name.Equals(txtTieuDe.Name))
                    {
                        txtTien.Clear();
                        txtTien.ReadOnly = true;
                        txtTien.TextChanged += new EventHandler(txtEventTongTien_TextChanged);
                        txtTien.KeyPress += new KeyPressEventHandler(txtEventTongTien_KeyPress);
                        // txtTien.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        txtTien.TextAlignment = TextAlignment.Far;
                    }
                  
                }
            }
        }
        /// <summary>
        /// hàm thưc hiện việc load thông tin của kiểm tra thông tin của bảo hiểm y tế khi laod form heienj tại
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frm_KiemTra_BHYT_Load(object sender, EventArgs e)
        {
            txtTieuDe.Text = Utility.sDbnull(tieude);
            if(objLuotkham!=null)
            {
                LayThongTinPaymentDetail();
                SqlQuery sqlQuery = new Select().From(KcbThanhtoanChitiet.Schema)
                    .Where(KcbThanhtoanChitiet.Columns.IdThanhtoan)
                    .In(
                        new Select(KcbThanhtoan.Columns.IdThanhtoan).From(KcbThanhtoan.Schema).Where(KcbThanhtoan.Columns.MaLuotkham).
                            IsEqualTo(
                                objLuotkham.MaLuotkham).And(KcbThanhtoan.Columns.IdBenhnhan).IsEqualTo(
                                    Utility.Int32Dbnull(objLuotkham.IdBenhnhan)).And(KcbThanhtoan.Columns.KieuThanhtoan).IsEqualTo(0))
                    .And(KcbThanhtoanChitiet.Columns.TrangthaiHuy).IsEqualTo(0)
                    .And(KcbThanhtoanChitiet.Columns.TuTuc).IsEqualTo(0);

                KcbThanhtoanChitietCollection objPaymentDetailCollection = sqlQuery.ExecuteAsCollection<KcbThanhtoanChitietCollection>();
                if (objPaymentDetailCollection.Count() > 0)
                {
                    txtSoTienGoc.Text = Utility.sDbnull(objPaymentDetailCollection.Sum(c => c.SoLuong * c.DonGia));
                    txtTienBHCT.Text = Utility.sDbnull(objPaymentDetailCollection.Sum(c => c.SoLuong * c.BhytChitra));
                    txtTienBNCT.Text = Utility.sDbnull(objPaymentDetailCollection.Sum(c => c.SoLuong * c.BnhanChitra));
                    txtTienPhuThu.Text = Utility.sDbnull(objPaymentDetailCollection.Sum(c => c.SoLuong * c.PhuThu));
                    txtTienBNPhaiTra.Text =
                        Utility.sDbnull(Utility.DecimaltoDbnull(txtTienPhuThu.Text) + Utility.DecimaltoDbnull(txtTienBNCT.Text));
                    Utility.SetMsg(lblMoneyLetter, new MoneyByLetter().sMoneyToLetter(txtTienBNPhaiTra.Text), true);
                }
            }
            
           
        }
        private void LayThongTinPaymentDetail()
        {
            m_dtPaymentDetail =
                SPs.KcbThanhtoanLaydulieuthanhtoanChitiet(-1,Utility.sDbnull(objLuotkham.MaLuotkham),Utility.Int32Dbnull(objLuotkham.IdBenhnhan),0).GetDataSet().Tables[0];
           
            Utility.SetDataSourceForDataGridEx(grdPaymentDetail, m_dtPaymentDetail, false, true, "1=1", "");
        }
        /// <summary>
        /// hà thực hiện vieecin in phiếu đống chi trả cho bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdInPhieu_DongChiTra_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (objLuotkham != null)
                {

                    KcbPhieuDct objPhieuDct = CreatePhieuDongChiTra();
                    ActionResult actionResult = new KCB_THANHTOAN().UpdatePhieuDCT(objPhieuDct, objLuotkham);
                    switch (actionResult)
                    {
                        case ActionResult.Success:
                            InPhieuDCT(objLuotkham);
                            break;
                        case ActionResult.Error:
                            Utility.ShowMsg("Lỗi trong quá trình cập nhập thông tin đồng  chi trả", "Thông báo lỗi", MessageBoxIcon.Error);
                            break;
                    }


                }

            }
            catch (Exception exception)
            {

            }
        }
        private DataTable m_dtReportPhieuThu=new DataTable();
        private void InPhieuDCT(KcbLuotkham objPatientExam)
        {
            m_dtReportPhieuThu =
                SPs.BhytLaydulieuinphieudct(objPatientExam.MaLuotkham,
                                                 Utility.Int32Dbnull(objPatientExam.IdBenhnhan)).GetDataSet().Tables[
                                                     0];
            if (m_dtReportPhieuThu.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy bản ghi nào", "Thông báo", MessageBoxIcon.Warning);
                return;
            }
            new VNS.HIS.Classes.INPHIEU_THANHTOAN_NGOAITRU().
                INPHIEU_DONGCHITRA(m_dtReportPhieuThu, dtNgayIn.Value, "PHIẾU THU ĐỒNG CHI TRẢ");
        }
        private KcbPhieuDct CreatePhieuDongChiTra()
        {
            KcbPhieuDct objPhieuDct = new KcbPhieuDct();
            objPhieuDct.MaLuotkham = Utility.sDbnull(objLuotkham.MaLuotkham);
            objPhieuDct.IdBenhnhan = Utility.Int32Dbnull(objLuotkham.IdBenhnhan);
            objPhieuDct.KieuThanhtoan = 0;
            objPhieuDct.NguoiTao = globalVariables.UserName;
            objPhieuDct.NgayTao = globalVariables.SysDate;
            objPhieuDct.TongTien = Utility.DecimaltoDbnull(txtSoTienGoc.Text);
            objPhieuDct.BnhanChitra = Utility.DecimaltoDbnull(txtTienBNCT.Text);
            objPhieuDct.BhytChitra = Utility.DecimaltoDbnull(txtTienBHCT.Text);
            return objPhieuDct;
        }
        /// <summary>
        /// hàm thực hiện việc in phiếu bảo hiểm cho bệnh nhân
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdPrintAll_Click(object sender, EventArgs e)
        {
            KcbPhieuDct objPhieuDct = CreatePhieuDongChiTra();
            ActionResult actionResult = new KCB_THANHTOAN().UpdatePhieuDCT(objPhieuDct, objLuotkham);
            LAOKHO_INPHIEU_BAOHIEM();
        }
        private void LAOKHO_INPHIEU_BAOHIEM()
        {
           
            m_dtReportPhieuThu =
                SPs.BhytLaythongtinInphoi(Utility.Int32Dbnull(-1), Utility.sDbnull(objLuotkham.MaLuotkham, ""),
                                         Utility.Int32Dbnull(objLuotkham.IdBenhnhan, -1), 0).GetDataSet().Tables[0];

            ///load thông tin của việc lấy dữ liệu vào datatable trong phiếu thanh toán)

            if (m_dtReportPhieuThu.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy bản ghi ", "Thông báo");
                return;
            }
            if (m_dtReportPhieuThu.Rows.Count <= 0)
            {
                Utility.ShowMsg("Không tìm thấy thông tin của bản ghi nào");
                return;
            }
            if (!m_dtReportPhieuThu.Columns.Contains("Tyle")) m_dtReportPhieuThu.Columns.Add("Tyle", typeof(string));
            if (!m_dtReportPhieuThu.Columns.Contains("BHYT")) m_dtReportPhieuThu.Columns.Add("BHYT", typeof(string));
            if (!m_dtReportPhieuThu.Columns.Contains("BNTT")) m_dtReportPhieuThu.Columns.Add("BNTT", typeof(string));
            if (!m_dtReportPhieuThu.Columns.Contains("TyLe_BH")) m_dtReportPhieuThu.Columns.Add("TyLe_BH", typeof(string));
            if (!m_dtReportPhieuThu.Columns.Contains("Tyle_BN")) m_dtReportPhieuThu.Columns.Add("Tyle_BN", typeof(string));
            foreach (DataRow drv in m_dtReportPhieuThu.Rows)
            {
                drv["Tyle"] = (100 - objLuotkham.PtramBhyt) + " %";
                drv["BHYT"] = objLuotkham.PtramBhyt + " %";
                drv["BNTT"] = (100 - objLuotkham.PtramBhyt) + " %";
                drv["TyLe_BH"] = objLuotkham.PtramBhyt + " %";
                drv["Tyle_BN"] = (100 - objLuotkham.PtramBhyt) + " %";
            }
            m_dtReportPhieuThu.AcceptChanges();
            foreach (DataRow drv in m_dtReportPhieuThu.Rows)
            {
                if (drv[KcbThanhtoanChitiet.Columns.IdLoaithanhtoan].ToString() == "1")
                {
                    int Id_Loai_dvu = Utility.Int32Dbnull(drv["ID_LOAI_DVU"], -1);
                    
                    drv["TEN_LOAI_DVU"] = string.Empty;
                    drv["ID_LOAI_DVU"] = Id_Loai_dvu;
                    drv["STT"] = 1;
                }
                if (drv[KcbThanhtoanChitiet.Columns.IdLoaithanhtoan].ToString() == "2")
                {
                    int Id_Loai_dvu = Utility.Int32Dbnull(drv["ID_LOAI_DVU"], -1);
                    DmucDichvucl objService = DmucDichvucl.FetchByID(Id_Loai_dvu);
                    if (objService != null)
                    {
                        drv["TEN_LOAI_DVU"] = Utility.sDbnull(objService.TenDichvu);
                        drv["STT"] = Utility.sDbnull(objService.SttHthi);
                    }
                }
                
                if (drv[KcbThanhtoanChitiet.Columns.IdLoaithanhtoan].ToString() == "3")
                {
                    int Drug_ID = Utility.Int32Dbnull(drv["ID_DVU"], -1);
                    DmucThuoc objDrug = DmucThuoc.FetchByID(Drug_ID);
                    if (objDrug != null)
                    {
                        
                                if (objDrug.KieuThuocvattu == "THUOC")
                                {
                                    drv["ID_LOAI_DVU"] = 1;
                                    drv["STT"] = 1;
                                    drv["TEN_LOAI_DVU"] = "Thuốc ";
                                }
                                else 
                                {
                                    drv["ID_LOAI_DVU"] = 2;
                                    drv["STT"] = 2;
                                    drv["TEN_LOAI_DVU"] = "Vật tư y tế ";
                                }
                            
                    }

                }
                if (drv[KcbThanhtoanChitiet.Columns.IdLoaithanhtoan].ToString() == "5")
                {
                    drv["ID_LOAI_DVU"] = 1;
                    drv["STT"] = 1;
                    drv["TEN_LOAI_DVU"] = "Chi phí thêm  ";

                }
            }
            m_dtReportPhieuThu.AcceptChanges();
            // KyDong_PrintBaoHiem_DungTuyen(txtTieuDe.Text);
            new INPHIEU_THANHTOAN_NGOAITRU().LAOKHOA_INPHIEU_BAOHIEM_NGOAITRU(m_dtReportPhieuThu, txtTieuDe.Text, dtNgayIn.Value);
        }
    }
}
