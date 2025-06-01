using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using SubSonic;
using VMS.HIS.DAL;
using VNS.Libs;

namespace VMS.Invoice
{
    public partial class FrmThongtinDieuchinh : Form
    {
        public LoadDataCommon.ServicesVacom.InvoicesClient _invoicesConnectionDieuchinh;
        public string InvInvoiceAuthId = "";
        public bool isCancel = false;
        public FrmThongtinDieuchinh()
        {
            InitializeComponent();
            Utility.SetVisualStyle(this);
            txtinv_InvoiceAuth_id_new.Visible =  txtinv_InvoiceAuth_id.Visible = globalVariables.IsAdmin;
            txtinv_InvoiceAuth_id.Text = InvInvoiceAuthId;
            dtngaydieuchinh.Value = globalVariables.SysDate;
        }
        DataTable _dtListPayment = new DataTable();
        private string _thongtinhoadon = "";
        private void frm_thongtin_dieuchinh_Load(object sender, EventArgs e)
        {
            try
            {
                if(string.IsNullOrEmpty(InvInvoiceAuthId)) return;
                DataKetqua objKetqua =
                    new Select().From(DataKetqua.Schema)
                        .Where(DataKetqua.Columns.InvInvoiceAuthId)
                        .IsEqualTo(InvInvoiceAuthId)
                        .ExecuteSingle<DataKetqua>();
                _dtListPayment =
                    new Select().From(HoadonLog.Schema)
                        .Where(HoadonLog.Columns.InvInvoiceAuthId)
                        .IsEqualTo(InvInvoiceAuthId)
                        .ExecuteDataSet()
                        .Tables[0]; 
                if (objKetqua != null && _dtListPayment.Rows.Count >0)
                {
                    txtinvoiceType.Text = Utility.sDbnull(objKetqua.InvInvoiceType, "");
                    txtInvoiceSeries.Text = Utility.sDbnull(objKetqua.InvInvoiceSeries, "");
                    txtInvoiceNumber.Text = Utility.sDbnull(objKetqua.InvInvoiceNumber, "");
                    dtngayhoadon.Value = Convert.ToDateTime(objKetqua.InvInvoiceIssuedDate);
                    txtInvoiceBuyerName.Text = Utility.sDbnull(objKetqua.InvBuyerDisplayName, "");
                    txtInvoiceBuyerAddress.Text = Utility.sDbnull(objKetqua.InvBuyerAddressLine, "");
                    _thongtinhoadon = string.Format("{0}/{1}/{2}", txtinvoiceType.Text.Trim(), txtInvoiceSeries.Text.Trim(), txtInvoiceNumber.Text.Trim());
                    txtsovanban.Text = _thongtinhoadon;
                    txtinv_InvoiceAuth_id.Text = InvInvoiceAuthId;
                    cmdChangeDinhDanh.Enabled = true;
                    cmdDownloadDinhDanh.Enabled = true;
                }
                else
                {
                    Utility.ShowMsg("Không có dữ liệu");
                    cmdChangeDinhDanh.Enabled = false;
                    cmdDownloadDinhDanh.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        private HoadonLog CreateHoadonLog(string inv_InvoiceAuth_id)
        {
            HoadonLog objHoadonLog = new Select().From(HoadonLog.Schema)
                .Where(HoadonLog.Columns.InvInvoiceAuthId).IsEqualTo(inv_InvoiceAuth_id)                
                .ExecuteSingle<HoadonLog>();
            return objHoadonLog;
        }
        private void cmdChangeDinhDanh_Click(object sender, EventArgs e)
        {
            try
            {
                string eMessage = "";
                var cultures = new CultureInfo("en-US");
                string sDataRequest = "";
                string result = string.Empty;
                HoadonLog objHoadonLog_old = CreateHoadonLog(txtinv_InvoiceAuth_id.Text);
                ThongTinDieuChinh objDieuChinh = new ThongTinDieuChinh();
                objDieuChinh.sovb = txtsovanban.Text.Trim();
                objDieuChinh.inv_InvoiceAuth_id = InvInvoiceAuthId;
                objDieuChinh.inv_invoiceIssuedDate = Convert.ToDateTime(dtngaydieuchinh.Value, cultures).ToString("yyyy-MM-dd");
                objDieuChinh.ngayvb = Convert.ToDateTime(dtngaydieuchinh.Value, cultures).ToString("yyyy-MM-dd");
                objDieuChinh.inv_buyerDisplayName = txthovaten.Text.Trim();
                objDieuChinh.inv_buyerAddressLine = txtdiachithaythe.Text.Trim();
                objDieuChinh.ghi_chu = txtghichu.Text.Trim();
                objDieuChinh.user_new = globalVariables.Tennguoidung;
                objDieuChinh.date_new = Convert.ToDateTime(THU_VIEN_CHUNG.GetSysDateTime(), cultures).ToString("yyyy-MM-dd");
                string sohoadon = "";
                string sThamso = "";
                sDataRequest = JsonConvert.SerializeObject(objDieuChinh); 
                result = _invoicesConnectionDieuchinh.ChangeDinhDanh(sDataRequest, ref eMessage);
                if (result != null)
                {
                    ketqua_dieuchinh objKetquaHoadon = JsonConvert.DeserializeObject<ketqua_dieuchinh>(result);
                    if (objKetquaHoadon != null )
                    {
                        if( objKetquaHoadon.ok != null )
                        {

                            Utility.Log(this.Name, globalVariables.UserName,
                           string.Format("Thực hiện điều chỉnh hóa đơn thành công với số hóa đơn {0} - InvInvoiceAuthId : {1}", _thongtinhoadon, txtinv_InvoiceAuth_id.Text
                           ), newaction.Update, "UI");
                            StoredProcedure spupdate = SPs.SpInvoiceUpdateTpaymentInvInvoiceAuthId(txtinv_InvoiceAuth_id.Text,
                                globalVariables.UserName, THU_VIEN_CHUNG.GetSysDateTime(), THU_VIEN_CHUNG.GetMACAddress());
                            int t = spupdate.Execute();
                            txtinv_InvoiceAuth_id_new.Text = objKetquaHoadon.ok.inv_InvoiceAuth_id;
                            foreach (DataRow row in _dtListPayment.AsEnumerable())
                            {
                                var objhoalog = new HoadonLog();
                                objhoalog.IdThanhtoan = Utility.Int32Dbnull(row["Payment_ID"], -1);
                                sThamso = string.IsNullOrEmpty(sThamso)
                                 ? Utility.sDbnull(row["Payment_ID"], -1)
                                 : sThamso + "," + Utility.sDbnull(row["Payment_ID"], -1);
                                objhoalog.TongTien = Utility.DecimaltoDbnull(row["TONG_TIEN"], 0);
                                objhoalog.IdBenhnhan = Utility.Int32Dbnull(row["Patient_ID"], -1);
                                objhoalog.MaLuotkham = Utility.sDbnull(row["Patient_Code"], "");
                                objhoalog.MauHoadon = objKetquaHoadon.ok.mau_hd;
                                objhoalog.KiHieu = objKetquaHoadon.ok.inv_invoiceSeries;
                                objhoalog.CapphatId = -1;
                                objhoalog.MaQuyen = "";
                                objhoalog.Serie = objKetquaHoadon.ok.inv_invoiceNumber;
                                sohoadon = objKetquaHoadon.ok.inv_invoiceNumber;
                                objhoalog.MaNvien = globalVariables.UserName;
                                objhoalog.MaLdo = string.Empty;
                                objhoalog.NgayIn = THU_VIEN_CHUNG.GetSysDateTime();
                                objhoalog.IpAddress = THU_VIEN_CHUNG.GetIP4Address();
                                objhoalog.MacAddress = THU_VIEN_CHUNG.GetMACAddress();
                                objhoalog.DaGui = Utility.Byte2Bool(1);
                                objhoalog.TrangThai = 0;
                                objhoalog.QrDataCode = "";
                                objhoalog.InvInvoiceAuthId = objKetquaHoadon.ok.inv_InvoiceAuth_id;
                                objhoalog.InvInvoiceCodeId = objKetquaHoadon.ok.inv_InvoiceCode_id;
                                objhoalog.Sobaomat = objKetquaHoadon.ok.sobaomat;
                                objhoalog.Id = objKetquaHoadon.ok.inv_InvoiceAuth_id;
                                if (objHoadonLog_old != null)
                                {
                                    objhoalog.LoaiXuat = objHoadonLog_old.LoaiXuat;
                                }
                                else
                                {
                                    objhoalog.LoaiXuat = 0;
                                }
                                StoredProcedure sp =
                                    SPs.VienPhiUpdateHoaDonLog(Utility.Int32Dbnull(objhoalog.HdonLogId),
                                        Utility.Int32Dbnull(objhoalog.CapphatId),
                                        Utility.Int32Dbnull(objhoalog.IdThanhtoan),
                                        objhoalog.TongTien, Utility.Int32Dbnull(objhoalog.IdBenhnhan),
                                        objhoalog.MaLuotkham,
                                        objhoalog.MauHoadon, objhoalog.KiHieu,
                                        objhoalog.MaQuyen, objhoalog.Serie, objhoalog.MaNvien, objhoalog.MaLdo,
                                        objhoalog.NgayIn,
                                        objhoalog.IpAddress, objhoalog.MacAddress,
                                        Utility.ByteDbnull(objhoalog.TrangThai),
                                        objhoalog.DaGui, Utility.sDbnull(objhoalog.QrDataCode), 0,
                                        objhoalog.InvInvoiceAuthId, objhoalog.InvInvoiceCodeId, objhoalog.Sobaomat,
                                        objhoalog.Id, objhoalog.LoaiXuat);
                                int record = sp.Execute();
                                if (record <= 0) break;
                            }
                            // lưu thông tin bảng data_ketqua 
                            DataKetqua objdatKetqua = new DataKetqua();
                            objdatKetqua.InvInvoiceAuthId = objKetquaHoadon.ok.inv_InvoiceAuth_id;
                            objdatKetqua.IdThanhtoan = Utility.sDbnull(sThamso);
                            objdatKetqua.InvInvoiceType = objKetquaHoadon.ok.inv_invoiceType;
                            objdatKetqua.InvInvoiceCodeId = objKetquaHoadon.ok.inv_InvoiceCode_id;
                            objdatKetqua.InvInvoiceSeries = objKetquaHoadon.ok.inv_invoiceSeries;
                            objdatKetqua.InvInvoiceNumber = objKetquaHoadon.ok.inv_invoiceNumber;
                            objdatKetqua.InvInvoiceName = objKetquaHoadon.ok.inv_invoiceName;
                            objdatKetqua.InvInvoiceIssuedDate = objKetquaHoadon.ok.inv_invoiceIssuedDate;
                            objdatKetqua.InvSubmittedDate = objKetquaHoadon.ok.inv_submittedDate;
                            objdatKetqua.InvContractNumber = objKetquaHoadon.ok.inv_contractNumber;
                            objdatKetqua.InvContractDate = objKetquaHoadon.ok.inv_contractDate;
                            objdatKetqua.InvCurrencyCode = objKetquaHoadon.ok.inv_currencyCode;
                            objdatKetqua.InvExchangeRate = objKetquaHoadon.ok.inv_exchangeRate;
                            objdatKetqua.InvInvoiceNote = objKetquaHoadon.ok.inv_invoiceNote;
                            objdatKetqua.InvAdjustmentType = objKetquaHoadon.ok.inv_adjustmentType;
                            objdatKetqua.InvOriginalInvoiceId = objKetquaHoadon.ok.inv_originalInvoiceId;
                            objdatKetqua.InvAdditionalReferenceDes = objKetquaHoadon.ok.inv_additionalReferenceDes;
                            objdatKetqua.InvAdditionalReferenceDate = objKetquaHoadon.ok.inv_additionalReferenceDate;
                            objdatKetqua.InvBuyerDisplayName = objKetquaHoadon.ok.inv_buyerDisplayName;
                            objdatKetqua.MaDt = objKetquaHoadon.ok.ma_dt;
                            objdatKetqua.InvBuyerLegalName = objKetquaHoadon.ok.inv_buyerLegalName;
                            objdatKetqua.InvBuyerTaxCode = objKetquaHoadon.ok.inv_buyerTaxCode;
                            objdatKetqua.InvBuyerAddressLine = objKetquaHoadon.ok.inv_buyerAddressLine;
                            objdatKetqua.InvBuyerEmail = objKetquaHoadon.ok.inv_buyerEmail;
                            objdatKetqua.InvBuyerBankAccount = objKetquaHoadon.ok.inv_buyerBankAccount;
                            objdatKetqua.InvBuyerBankName = objKetquaHoadon.ok.inv_buyerBankName;
                            objdatKetqua.InvPaymentMethodName = objKetquaHoadon.ok.inv_paymentMethodName;
                            objdatKetqua.InvSellerBankAccount = objKetquaHoadon.ok.inv_sellerBankAccount;
                            objdatKetqua.InvSellerBankName = objKetquaHoadon.ok.inv_sellerBankName;
                            objdatKetqua.InvDiscountAmount = objKetquaHoadon.ok.inv_discountAmount;
                            objdatKetqua.TrangThai = objKetquaHoadon.ok.trang_thai;
                            objdatKetqua.UserNew = objKetquaHoadon.ok.user_new;
                            objdatKetqua.DateNew = objKetquaHoadon.ok.date_new;
                            objdatKetqua.MaDvcs = objKetquaHoadon.ok.ma_dvcs;
                            objdatKetqua.DatabaseCode = objKetquaHoadon.ok.database_code;
                            objdatKetqua.MaCt = objKetquaHoadon.ok.ma_ct;
                            objdatKetqua.SignedDate = objKetquaHoadon.ok.signedDate;
                            objdatKetqua.SubmittedDate = objKetquaHoadon.ok.submittedDate;
                            objdatKetqua.MauHd = objKetquaHoadon.ok.mau_hd;
                            objdatKetqua.SoBenhAn = objKetquaHoadon.ok.so_benh_an;
                            objdatKetqua.Sovb = objKetquaHoadon.ok.sovb;
                            objdatKetqua.Ngayvb = objKetquaHoadon.ok.ngayvb;
                            objdatKetqua.GhiChu = objKetquaHoadon.ok.ghi_chu;
                            objdatKetqua.SoHdDc = objKetquaHoadon.ok.so_hd_dc;
                            objdatKetqua.InvOriginalId = objKetquaHoadon.ok.inv_originalId;
                            objdatKetqua.Signature = objKetquaHoadon.ok.signature;
                            objdatKetqua.DieuTri = objKetquaHoadon.ok.dieu_tri;
                            objdatKetqua.Ma1 = objKetquaHoadon.ok.ma1;
                            objdatKetqua.InvItemCode = objKetquaHoadon.ok.inv_itemCode;
                            objdatKetqua.InvInvoiceName = objKetquaHoadon.ok.inv_itemName;
                            objdatKetqua.InvUnitCode = objKetquaHoadon.ok.inv_unitCode;
                            objdatKetqua.InvUnitName = objKetquaHoadon.ok.inv_unitName;
                            objdatKetqua.InvUnitPrice = objKetquaHoadon.ok.inv_unitPrice;
                            objdatKetqua.InvQuantity = objKetquaHoadon.ok.inv_quantity;
                            objdatKetqua.InvTotalAmountWithoutVat = objKetquaHoadon.ok.inv_TotalAmountWithoutVat;
                            objdatKetqua.InvVatPercentage = objKetquaHoadon.ok.inv_vatPercentage;
                            objdatKetqua.InvVatAmount = objKetquaHoadon.ok.inv_vatAmount;
                            objdatKetqua.InvTotalAmount = objKetquaHoadon.ok.inv_TotalAmount;
                            objdatKetqua.NguoiKy = objKetquaHoadon.ok.nguoi_ky;
                            objdatKetqua.Sobaomat = objKetquaHoadon.ok.sobaomat;
                            objdatKetqua.TrangThaiHd = objKetquaHoadon.ok.trang_thai_hd;
                            objdatKetqua.InChuyenDoi = objKetquaHoadon.ok.in_chuyen_doi;
                            objdatKetqua.NgayKy = objKetquaHoadon.ok.ngay_ky;
                            objdatKetqua.NguoiInCdoi = objKetquaHoadon.ok.nguoi_in_cdoi;
                            objdatKetqua.NgayInCdoi = objKetquaHoadon.ok.ngay_in_cdoi;
                            objdatKetqua.InvDeliveryOrderNumber = objKetquaHoadon.ok.inv_deliveryOrderNumber;
                            objdatKetqua.InvDeliveryOrderDate = objKetquaHoadon.ok.inv_deliveryOrderDate;
                            objdatKetqua.InvDeliveryBy = objKetquaHoadon.ok.inv_deliveryBy;
                            objdatKetqua.InvTransportationMethod = objKetquaHoadon.ok.inv_transportationMethod;
                            objdatKetqua.InvFromWarehouseName = objKetquaHoadon.ok.inv_fromWarehouseName;
                            objdatKetqua.InvToWarehouseName = objKetquaHoadon.ok.inv_toWarehouseName;
                            objdatKetqua.InvSobangke = objKetquaHoadon.ok.inv_sobangke;
                            objdatKetqua.InvNgaybangke = objKetquaHoadon.ok.inv_ngaybangke;
                            objdatKetqua.KeyApi = objKetquaHoadon.ok.key_api;
                            objdatKetqua.Id = objKetquaHoadon.ok.inv_InvoiceAuth_id;
                            objdatKetqua.IsNew = true;
                            objdatKetqua.Save();
                            Utility.ShowMsg("Lập hóa đơn điều chỉnh định danh cho hóa đơn: " + txtInvoiceNumber.Text + " với số hóa đơn mới là: " + objKetquaHoadon.ok.inv_invoiceNumber);
                        }
                        else
                        {
                            Utility.ShowMsg(result);
                        }
                       
                        }
                        else
                    {
                        Utility.ShowMsg(string.Format("Lấy hóa đơn không thành công cho người mua: {0} ",
                            txthovaten.Text.Trim()));
                    }
                    isCancel = true;
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
        private VacomInvoicesConnection _vacomInvoices = new VacomInvoicesConnection();
        private void cmdDownloadDinhDanh_Click(object sender, EventArgs e)
        {
            try
            {
                string eMessga = "";
                bool kt = new VacomInvoicesConnection().SaveFileInvoice(txtinv_InvoiceAuth_id_new.Text, true, ref eMessga);
                if (kt)
                {
                    StoredProcedure sp =
                        SPs.SpCapnhapHoadonLog(Utility.sDbnull(txtinv_InvoiceAuth_id_new.Text, ""),
                            globalVariables.gv_StaffID.ToString(), globalVariables.SysDate, 1);
                    sp.Execute();
                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }

        private void cmdExit_Click(object sender, EventArgs e)
        {
            this.Close();
            this.Dispose();
        }

        private void cmdkyhoadon_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtmaPin.Text))
                {
                    Utility.ShowMsg("Bạn cần nhập thông tin mã PIN","Cảnh báo", MessageBoxIcon.Error);
                    txtmaPin.Focus();
                    return;
                }
                if (string.IsNullOrEmpty(txtinv_InvoiceAuth_id_new.Text))
                {
                    Utility.ShowMsg("Thông tin hóa đơn chưa tồn tại", "Cảnh báo", MessageBoxIcon.Error);
                    txtmaPin.Focus(); 
                    return;
                } 
                List<data> _list = new List<data>();
                data obj = new data();
                obj.id = Utility.sDbnull(txtinv_InvoiceAuth_id_new.Text);  
                _list.Add(obj);
                SignEASSYCA objEassyca = new SignEASSYCA();
                objEassyca.pin = txtmaPin.Text;
                objEassyca.data = _list;
                  string eMessga = "";
                string sDataRequest = JsonConvert.SerializeObject(objEassyca);
                bool kt = new VacomInvoicesConnection().SignEasyca(sDataRequest, ref eMessga);
                if (kt)
                {

                }
            }
            catch (Exception ex)
            {
                Utility.ShowMsg(ex.Message);
            }
        }
    }
}
