using System;
using System.Collections.Generic;

namespace VMS.Invoice
{



    #region Model VNPT



    #endregion




    #region Model Misa

    public class MisaTraCuuHoaDon
    {
        public string userName { get; set; }
        public string maTracuu { get; set; }
    }

    public class downloadModel
    {
        public string userName { get; set; }
        public string maTracuu { get; set; }
    }

    public class DataSendInvoices
    {
        public Orginvoicedata OrgInvoiceData { get; set; }
        public bool IsSendEmail { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverEmail { get; set; }
        public string userName { get; set; }

    }
    public class MisaPhatHanhHoaDon
    {
        public List<Orginvoicedata> InvoiceData { get; set; }

        public int SignType { get; set; }
        public PublishInvoiceData PublishInvoiceData { get; set; }


    }
    public class PublishInvoiceData
    {
        public string RefID { get; set; }
        public string TransactionID { get; set; }
        public string InvSeries { get; set; }
        public string InvoiceData { get; set; }
        public bool IsInvoiceCalculatingMachine { get; set; }
        public bool IsSendEmail { get; set; }
        public string ReceiverName { get; set; }
        public string ReceiverEmail { get; set; }



    }
    public class BuyerInfor
    {
        public long Id_benhnhan { get; set; }
        public string MaLuotkham { get; set; }
        public string BuyerCode { get; set; }
        public string BuyerLegalName { get; set; }
        public string BuyerTaxCode { get; set; }
        public string BuyerAddress { get; set; }
        public string BuyerFullName { get; set; }
        public string BuyerPhoneNumber { get; set; }
        public string BuyerEmail { get; set; }
        public string BuyerBankAccount { get; set; }
        public string BuyerBankName { get; set; }
        public string BuyerIDNumber { get; set; }

        public string TenHangHoa { get; set; }
        public string Donvitinh { get; set; }
        public decimal Tongtienhang { get; set; }
        public string VAT { get; set; }
        public decimal TongtienThue { get; set; }
        public decimal ThanhtienDonhang { get; set; }

        public string MauHoadon { get; set; }
        public string Serie { get; set; }
        public string TenMauHoadon { get; set; }
    }
    public class Orginvoicedata
    {
        public string RefID { get; set; }
        public string InvSeries { get; set; }
        public string InvoiceName { get; set; }
        public string InvDate { get; set; }
        public string CurrencyCode { get; set; }
        public decimal ExchangeRate { get; set; }
        public string PaymentMethodName { get; set; }
        //Thông tin hóa đơn thay thế
        public int ReferenceType { get; set; }
        public int OrgInvoiceType { get; set; }
        public string OrgInvTemplateNo { get; set; }
        public string OrgInvSeries { get; set; }
        public string OrgInvNo { get; set; }
        public string OrgInvDate { get; set; }
        public string InvoiceNote { get; set; }
        //Các thông tin bình thường

        public string BuyerFullName { get; set; }
        public string BuyerLegalName { get; set; }
        public string BuyerTaxCode { get; set; }
        public string BuyerAddress { get; set; }
        public string BuyerEmail { get; set; }
        public string BuyerIDNumber { get; set; }
        public string ContactName { get; set; }
        public decimal TotalSaleAmountOC { get; set; }
        public decimal TotalSaleAmount { get; set; }
        public decimal TotalDiscountAmount { get; set; }
        public decimal TotalDiscountAmountOC { get; set; }
        public decimal TotalAmountWithoutVATOC { get; set; }
        public decimal TotalAmountWithoutVAT { get; set; }
        public decimal TotalVATAmountOC { get; set; }
        public decimal TotalVATAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalAmountOC { get; set; }
        public string TotalAmountInWords { get; set; }

        public List<Originalinvoicedetail> OriginalInvoiceDetail { get; set; }
        public List<Taxrateinfo> TaxRateInfo { get; set; }
        public object FeeInfo { get; set; }
        public Optionuserdefined OptionUserDefined { get; set; }
        public bool IsInvoiceCalculatingMachine { get; set; }
        public bool IsTaxReduction43 { get; set; }

        public string CustomField1 { get; set; }
    }

    public class Optionuserdefined
    {
        public string MainCurrency { get; set; }
        public string AmountDecimalDigits { get; set; }
        public string AmountOCDecimalDigits { get; set; }
        public string UnitPriceOCDecimalDigits { get; set; }
        public string UnitPriceDecimalDigits { get; set; }
        public string QuantityDecimalDigits { get; set; }
        public string CoefficientDecimalDigits { get; set; }
        public string ExchangRateDecimalDigits { get; set; }
        public string ClockDecimalDigits { get; set; }
    }

    public class Originalinvoicedetail
    {
        public int ItemType { get; set; }
        public int LineNumber { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string UnitName { get; set; }
        public decimal Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal AmountOC { get; set; }
        /// <summary>
        /// Tỷ lệ chiết khấu
        /// </summary>
        public decimal? DiscountRate { get; set; }

        /// <summary>
        /// Tiền chiết khấu
        /// </summary>
        public decimal? DiscountAmountOC { get; set; }

        /// <summary>
        /// Tiền chiết khấu quy đổi
        /// </summary>
        public decimal? DiscountAmount { get; set; }

        public decimal Amount { get; set; }
        public decimal AmountWithoutVATOC { get; set; }
        public decimal AmountWithoutVAT { get; set; }
        public string VATRateName { get; set; }
        public decimal AmountAfterTax { get; set; }
        public decimal VATAmountOC { get; set; }
        public decimal VATAmount { get; set; }
        public int SortOrder { get; set; }
    }

    public class Taxrateinfo
    {
        public string VATRateName { get; set; }
        public decimal AmountWithoutVATOC { get; set; }
        public decimal VATAmountOC { get; set; }
    }

    public class ResponseFileMinvoices
    {
        public string TransactionID { get; set; }
        public string Data { get; set; }
    }
    public class ResponseMinvoices
    {
        public string RefID { get; set; }
        public string TransactionID { get; set; }
        public string InvTemplateNo { get; set; }
        public string InvSeries { get; set; }
        public string InvNo { get; set; }
        public string InvCode { get; set; }
        public DateTime InvDate { get; set; }
        public string ErrorCode { get; set; }
        public Customdata CustomData { get; set; }
    }

    public class Customdata
    {
        public string TransactionID { get; set; }
        public string InvNo { get; set; }
    }
    public class MisaCancelModel
    {
        public string TransactionID { get; set; }
        public string InvSeries { get; set; }
        public string CancelReason { get; set; }
    }
    public class MisaCancelModel_bak
    {
        public string TransactionID { get; set; }
        public string InvNo { get; set; }
        public string RefDate { get; set; }
        public string CancelReason { get; set; }
        public string userName { get; set; }
    }



    /// <summary>
    /// Mẫu hóa đơn
    /// </summary>
    public class TemplateData
    {
        /// <summary>
        /// ID mẫu
        /// </summary>
        public string IPTemplateID { get; set; }
        /// <summary>
        /// ID công ty
        /// </summary>
        public int CompanyID { get; set; }
        /// <summary>
        /// Tên mẫu
        /// </summary>
        public string TemplateName { get; set; }
        /// <summary>
        /// Mẫu số
        /// </summary>
        public string InvTemplateNo { get; set; }
        /// <summary>
        /// Ký hiệu
        /// </summary>
        public string InvSeries { get; set; }

        /// <summary>
        /// Ký hiệu
        /// </summary>
        public string OrgInvSeries { get; set; }
        /// <summary>
        /// Loại mẫu (stimul, xslt,...)
        /// </summary>
        public int TemplateType { get; set; }
        /// <summary>
        /// Loại hóa đơn (GTGT, bán hàng, xuất kho,...)
        /// </summary>
        public int InvoiceType { get; set; }
        /// <summary>
        /// Nghiệp vụ
        /// </summary>
        public int BusinessAreas { get; set; }
        /// <summary>
        /// Thứ tự
        /// </summary>
        public int SortOrder { get; set; }
        /// <summary>
        /// Ngày ký
        /// </summary>
        public DateTime? SignedDate { get; set; }
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// Ngày sửa
        /// </summary>
        public DateTime ModifiedDate { get; set; }
        /// <summary>
        /// Người sửa
        /// </summary>
        public string ModifiedBy { get; set; }
        /// <summary>
        /// Ngừng hoạt động
        /// </summary>
        /// <returns></returns>
        public bool Inactive { get; set; }
        /// <summary>
        /// Nội dung file mẫu
        /// </summary>
        public byte[] TemplateContent { get; set; }
        /// <summary>
        /// ID mẫu mặc định
        /// </summary>
        public Guid DefaultTemplateID { get; set; }
        /// <summary>
        /// Có phải mẫu custom không
        /// </summary>
        public bool IsCustomTemplate { get; set; }
        /// <summary>
        /// kế thừa từ mẫu cũ hay không
        /// </summary>
        public bool IsInheritFromOldTemplate { get; set; }

        /// <summary>
        /// Phiên bản mẫu xslt
        /// </summary>
        public int? XsltVersion { get; set; }
        /// <summary>
        /// Đã phát hành hđ hay chưa
        /// </summary>
        public bool IsPublished { get; set; }
    }

    public class MisaResonce_HD
    {
        public bool success { get; set; }
        public object errorCode { get; set; }
        public object descriptionErrorCode { get; set; }
        public object createInvoiceResult { get; set; }
        public string publishInvoiceResult { get; set; }
    }

    public class MisaResponse
    {
        public bool Success { get; set; }
        public string ErrorCode { get; set; }
        public string DescriptionErrorCode { get; set; }
        public object Errors { get; set; }
        public object Data { get; set; }
        public string CustomData { get; set; }
    }

    public class MisaTokenModel
    {
        public string appid { get; set; }
        public string taxcode { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
    #endregion


    #region Model Hilo
    public class ThongBaoPhatHanh
    {
        public string Status { get; set; }
        public string InvSerialSuffix { get; set; }
        public string InvSerial { get; set; }
        public string InvSerialPrefix { get; set; }
        public string CurrentNo { get; set; }
        public string id { get; set; }
        public string PublishID { get; set; }
        public string Quantity { get; set; }
        public string FromNo { get; set; }
        public string ToNo { get; set; }
        public string StartDate { get; set; }
        public string RegisterID { get; set; }
        public string RegisterName { get; set; }
        public string EndDate { get; set; }
        public string ComId { get; set; }
        public string InvPattern { get; set; }
        public string InvCateName { get; set; }
        public string InvCateType { get; set; }
        public string AlertLevel { get; set; }

    }
    public class KetQuaHoaDon
    {
        public bool success { get; set; }
        public string error { get; set; }
        public string messages { get; set; }
        public List<Datahoadon> data { get; set; }
        public string lstXmlData { get; set; }
    }

    public class Datahoadon
    {
        public string pattern { get; set; }
        public string serial { get; set; }
        public string fkey { get; set; }
        public string key { get; set; }
        public string no { get; set; }
        public string detailError { get; set; }
        public string detailMessages { get; set; }
    }
    public class PdfHoaDon
    {
        public bool success { get; set; }
        public string error { get; set; }
        public string messages { get; set; }
        public string data { get; set; }
    }



    public class DataPdfhoadon
    {
        public string fKey { get; set; }
        public string pattern { get; set; }
        public string serial { get; set; }
    }

    public class InvoiceModels
    {

        public string serial { get; set; }
        public string no { get; set; }

        public string pattern { get; set; }
        public string fkey { get; set; }
        public string xmlData { get; set; }
        public string invNo { get; set; }
    }



    public class Data
    {
        public string xmlData { get; set; }
        public string pattern { get; set; }
        public string serial { get; set; }
        //public string type { get; set; }
        public bool convert { get; set; }
        public string UserCreate { get; set; }

    }

    public class Invoices
    {
        public Inv Inv { get; set; }
    }

    public class Inv
    {
        public string key { get; set; }
        public Invoice Invoice { get; set; }
    }

    public class Invoice
    {
        public string CusCode { get; set; }
        public string Buyer { get; set; }
        public string CusName { get; set; }
        public string CusEmail { get; set; }
        public string Extra01 { get; set; }
        public string CusAddress { get; set; }
        public string CusTaxCode { get; set; }
        public string CusBankNo { get; set; }
        public string CusBankName { get; set; }
        public string Note { get; set; }
        public string PaymentMethod { get; set; }
        public string ArisingDate { get; set; }
        public List<Product> Products { get; set; }

        public decimal Total { get; set; }
        public string VATRate { get; set; }
        public decimal VATAmount { get; set; }
        public decimal Amount { get; set; }
        public string AmountInWords { get; set; }
        public string Currency { get; set; }
    }

    public class Products
    {
        public List<Product> Product { get; set; }
    }

    public class Product
    {
        public string Code { get; set; }
        public string ProdName { get; set; }
        public string ProdUnit { get; set; }
        public string ProdQuantity { get; set; }
        public decimal ProdPrice { get; set; }
        public decimal Amount { get; set; }
    }

    #endregion




    #region  Model Vacom

    public class ThongTinNguoiMua
    {
        public string ngay_hoadon { get; set; }
        public string ten_nguoimua { get; set; }
        public string stk_nguoimua { get; set; }
        public string ma_donvi { get; set; }
        public string ten_donvi { get; set; }
        public string ma_sothue { get; set; }
        public string dia_chi { get; set; }
        public string email { get; set; }
        public string so_tienhoadon { get; set; }
        public string so_tienthu { get; set; }
        public string so_tienmiengiam { get; set; }
    }



    public class account
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public string MaDvcs { get; set; }
    }

    public class inchuyendoi
    {
        public string id { get; set; }
    }
    public class ketqua_Sign
    {
        List<ketqua_ketqua_Sign> data { get; set; }
    }

    public class ketqua_ketqua_Sign
    {
        public string ok { get; set; }
        public string trang_thai { get; set; }
        public string inv_invoiceNumber { get; set; }
        public string inv_InvoiceAuth_id { get; set; }
    }
    public class SignEASSYCA
    {
        public string pin { get; set; }
        public List<data> data { get; set; }
    }

    public class data
    {
        public string id { get; set; }
    }
    public class ThongTinDieuChinh
    {
        public string inv_InvoiceAuth_id { get; set; }
        public string inv_invoiceIssuedDate { get; set; }
        public string sovb { get; set; }
        public string ngayvb { get; set; }
        public string ghi_chu { get; set; }
        public string inv_buyerDisplayName { get; set; }
        public string inv_buyerAddressLine { get; set; }
        public string user_new { get; set; }
        public string date_new { get; set; }
    }
    public class ketqua_huyhoadon78
    {

        /// <summary>
        /// thành công
        /// </summary>
        public string code { get; set; }

        /// <summary>
        /// Ngày văn bản xóa bỏ phải lớn hơn ngày hóa đơn.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// inv_InvoiceAuth_id Của hóa đơn không tồn tại/ Json Input truyền lên API không đúng định dạng mẫu/ Chỉ hóa đơn đã ký mới được phép xóa bỏ.
        /// </summary>
        public string data { get; set; }
    }
    public class ketqua_huyhoadon
    {

        /// <summary>
        /// thành công
        /// </summary>
        public string ok { get; set; }

        /// <summary>
        /// Ngày văn bản xóa bỏ phải lớn hơn ngày hóa đơn.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// inv_InvoiceAuth_id Của hóa đơn không tồn tại/ Json Input truyền lên API không đúng định dạng mẫu/ Chỉ hóa đơn đã ký mới được phép xóa bỏ.
        /// </summary>
        public string error { get; set; }
    }

    public class thongtin_huyhoadon
    {
        /// <summary>
        /// Trường khóa chính của bảng đầu phiếu hóa đơn. Sau khi tạo mới thành công có trả về trường này
        /// </summary>
        public string inv_InvoiceAuth_id { get; set; }
        /// <summary>
        /// Số văn bản Xóa bỏ hóa đơn
        /// </summary>
        public string sovb { get; set; }
        /// <summary>
        /// Ngày văn bản Xóa bỏ hóa đơn Định dạng yyyy-MM-dd
        /// </summary>
        public string ngayvb { get; set; }
        /// <summary>
        /// Lý do Xóa bỏ hóa đơn
        /// </summary>
        public string ghi_chu { get; set; }

    }

    public class TocketModel
    {
        public string token { get; set; }
    }
    public class thongtin_gui_hoadon
    {
        /// <summary>
        /// Mã loại hóa đơn; WIN00187: Hóa đơn giá trị gia tăng 01GTKT; WIN00189: Hóa đơn bán hàng giá trị trực tiếp 02GTTT; 
        /// WIN00192:Hóa đơn bán hàng (07KPTQ); WIN00193: Biên lai thu tiền phí, lệ phí (01BLP); WIN00194: Phiếu xuất kho nội bộ (03XKNB)
        /// WIN00194: 
        /// </summary>
        public string windowid { get; set; }
        /// <summary>
        /// Chế độ chỉnh sửa - - 1: Tạo mới- 2: Sửa
        /// </summary>
        public int editmode { get; set; }
        /// <summary>
        /// Thông tin hóa đơn 
        /// </summary>
        public List<thongtin_hoadon> data { get; set; }
    }
    ///
    public class thongtin_hoadon
    {
        ///// <summary>
        ///// Key của Bravo truyền lên
        ///// </summary>
        //public string key_api { get; set; }
        /// <summary>
        /// Ký hiệu hóa đơn
        /// </summary>
        public string inv_invoiceSeries { get; set; }
        /// <summary>
        /// Số hóa đơn. \n Trường hợp tạo mới không cần đẩy lên API. \n Trường hợp sửa bắt buộc phải đẩy lên API

        /// </summary>
        //public string inv_invoiceNumber { get; set; }
        ///// <summary>
        ///// Ngày hóa đơn - Định dạng yyyy-MM-dd
        ///// </summary>
        public string inv_invoiceIssuedDate { get; set; }
        /// <summary>
        /// Mã ngoại tệ. - Ví dụ: VND, USD
        /// </summary>
        public string inv_currencyCode { get; set; }
        /// <summary>
        /// Tỷ giá. Nếu ngoại tệ là “VND” thì mặc định là 1
        /// </summary>
        public int inv_exchangeRate { get; set; }
        /// <summary>
        /// Tên người mua
        /// </summary>
        public string inv_buyerDisplayName { get; set; }
        /// <summary>
        /// Mã khách hàng
        /// </summary>
        public string ma_dt { get; set; }
        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string inv_buyerLegalName { get; set; }
        /// <summary>
        /// Mã số thuế bên mua
        /// </summary>
        public string inv_buyerTaxCode { get; set; }
        /// <summary>
        /// Địa chỉ người mua
        /// </summary>
        public string inv_buyerAddressLine { get; set; }
        /// <summary>
        /// Email của người mua
        /// </summary>
        public string inv_buyerEmail { get; set; }
        /// <summary>
        /// Tài khoản ngân hàng bên mua
        /// </summary>
        public string inv_buyerBankAccount { get; set; }
        /// <summary>
        /// Tên ngân hàng bên mua
        /// </summary>
        public string inv_buyerBankName { get; set; }
        /// <summary>
        /// Phương thức thanh toán - Ví dụ:
        ///- Tiền mặt
        ///- Chuyển khoản
        ///- Tiền mặt/Chuyển khoản
        /// </summary>
        public string inv_paymentMethodName { get; set; }
        /// <summary>
        /// Tài khoản ngân hàng bên bán
        /// </summary>
        public string inv_sellerBankAccount { get; set; }
        /// <summary>
        /// Tên ngân hàng bên bán
        /// </summary>
        public string Inv_sellerBankName { get; set; }
        /// <summary>
        /// Tên ngân hàng bên bán
        /// </summary>
        public string mau_hd { get; set; }
        /// <summary>
        /// Tổng tiền trước thuế của cả hóa đơn
        /// </summary>
        public string inv_TotalAmountWithoutVat { get; set; }
        /// <summary>
        /// Tổng tiền thuế của cả hóa đơn
        /// </summary>
        public string inv_vatAmount { get; set; }
        /// <summary>
        /// Tổng tiền của cả hóa đơn
        /// </summary>
        public string inv_TotalAmount { get; set; }
        /// <summary>
        /// Tiền chiết khấu cả hóa đơn
        /// </summary>
        public string inv_discountAmount { get; set; }
        /// <summary>
        /// Danh sách thông tin chi tiết hóa đơn
        /// </summary>

        public string user_new { get; set; }
        public string date_new { get; set; }
        public List<thongtin_dichvu> details { get; set; }
    }

    public class chitiet_dichvu
    {

        /// <summary>
        /// Tính chất hàng hóa.
        /// </summary>
        public int tchat { get; set; }
        /// <summary>
        /// Số thứ tự dòng của hóa đơn. 
        /// </summary>
        public string stt_rec0 { get; set; }
        /// <summary>
        /// Số thứ tự dòng của hóa đơn. 
        /// </summary>
        public decimal sothutu { get; set; }
        /// <summary>
        /// Mã hàng
        /// </summary>
        public string inv_itemCode { get; set; }
        /// <summary>
        /// Tên hàng
        /// </summary>
        public string inv_itemName { get; set; }
        /// <summary>
        /// Mã đơn vị tính
        /// </summary>
        public string inv_unitCode { get; set; }
        /// <summary>
        /// Tên đơn vị tính
        /// </summary>
        public string inv_unitName { get; set; }
        /// <summary>
        /// Đơn giá
        /// </summary>
        public decimal inv_unitPrice { get; set; }
        /// <summary>
        /// Số lượng
        /// </summary>
        public decimal inv_quantity { get; set; }
        /// <summary>
        /// Tiền trước thuế
        /// </summary>
        public decimal inv_TotalAmountWithoutVat { get; set; }
        /// <summary>
        /// Tiền thuế
        /// </summary>
        public decimal inv_vatAmount { get; set; }
        /// <summary>
        /// Tổng tiền
        /// </summary>
        public decimal inv_TotalAmount { get; set; }
        /// <summary>
        /// Hàng khuyến mãi. - True: Hàng là khuyến mãi . False: Hàng không khuyến mãi
        /// </summary>
        public bool inv_promotion { get; set; }
        /// <summary>
        /// Phần trăm chiết khấu
        /// </summary>
        public decimal inv_discountPercentage { get; set; }
        /// <summary>
        /// Tiền chiết khấu
        /// </summary>
        public decimal inv_discountAmount { get; set; }
        /// <summary>
        /// Mã thuế  - 10 : Thuế 10% - 5:  Thuế 5% - 0:  Thuế 0% - -1:  Không chịu thuế
        /// </summary>
        public string ma_thue { get; set; }
    }
    public class thongtin_dichvu
    {
        /// <summary>
        /// Id của bảng chi tiết hóa đơn
        /// </summary>
        public string tab_id { get; set; }
        /// <summary>
        /// Mảng chứa dữ liệu chi tiết hóa đơn
        /// </summary>
        public List<chitiet_dichvu> data { get; set; }
    }

    public class hoadon_dieuchinh_dinhdanh
    {
        /// <summary>
        /// Key hóa đơn bị điều chỉnh
        /// </summary>
        public string key_api_old { get; set; }
        /// <summary>
        /// Key hóa đơn điều chỉnh (hóa đơn điều chỉnh sẽ sinh ra 1 số hóa đơn mới)
        /// </summary>
        public string key_api_new { get; set; }
        /// <summary>
        /// Ngày của hóa đơn điều chỉnh - Format: yyyy-MM-dd
        /// </summary>
        public DateTime inv_invoiceIssuedDate { get; set; }
        /// <summary>
        /// Số văn bản điều chỉnh
        /// </summary>
        public string sovb { get; set; }
        /// <summary>
        /// Ngày văn bản điều chỉnh
        /// </summary>
        public DateTime ngayvb { get; set; }
        /// <summary>
        /// Ghi chú khi điều chỉnh hóa đơn
        /// </summary>
        public string ghi_chu { get; set; }
        /// <summary>
        /// Tên người hàng 
        /// </summary>
        public string inv_buyerDisplayName { get; set; }
        /// <summary>
        /// Mã đối tượng
        /// </summary>
        public string ma_dt { get; set; }
        /// <summary>
        /// Tên đơn vị mua
        /// </summary>
        public string inv_buyerLegalName { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string inv_buyerAddressLine { get; set; }
        /// <summary>
        /// Email khách hàng
        /// </summary>
        public string inv_buyerEmail { get; set; }
        /// <summary>
        /// Số tài khoản ngân hàng
        /// </summary>
        public string inv_buyerBankAccount { get; set; }
        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string inv_buyerBankName { get; set; }
    }

    public class hoadon_dieuchinh
    {
        /// <summary>
        /// Key hóa đơn bị điều chỉnh
        /// </summary>
        public string key_api_old { get; set; }
        /// <summary>
        /// Key hóa đơn điều chỉnh (hóa đơn điều chỉnh sẽ sinh ra 1 số hóa đơn mới)
        /// </summary>
        public string key_api_new { get; set; }
        /// <summary>
        /// Ngày của hóa đơn điều chỉnh - Format: yyyy-MM-dd
        /// </summary>
        public DateTime inv_invoiceIssuedDate { get; set; }
        /// <summary>
        /// Số văn bản điều chỉnh
        /// </summary>
        public string sovb { get; set; }
        /// <summary>
        /// Ngày văn bản điều chỉnh
        /// </summary>
        public DateTime ngayvb { get; set; }
        /// <summary>
        /// Ghi chú khi điều chỉnh hóa đơn
        /// </summary>
        public string ghi_chu { get; set; }
        /// <summary>
        /// dữ liệu chi tiết hóa đơn điều chỉnh
        /// </summary>
        public List<hoadon_dieuchinh_chitiet> ListhoaTangchitiets { get; set; }
    }
    /// <summary>
    /// Nếu điều chỉnh trường nào thì đẩy trường đó lên (nếu không đẩy lên hóa đơn điều chỉnh sẽ lấy  giá trị cũ của hóa đơn bị điều chỉnh)
    /// </summary>
    public class hoadon_dieuchinh_chitiet
    {
        /// <summary>
        /// Mã hàng hóa cần điều chỉnh
        /// </summary>
        public string inv_itemCode { get; set; }
        /// <summary>
        /// Số lượng
        /// </summary>
        public decimal inv_quantity { get; set; }
        /// <summary>
        /// Đơn giá
        /// </summary>
        public decimal inv_unitPrice { get; set; }
        /// <summary>
        /// Mã thuế
        /// </summary>
        public string ma_thue { get; set; }
        /// <summary>
        /// Điều chỉnh gì thì ghi vào trường này ví dụ: Số lượng, đơn giá
        /// </summary>
        public string tieu_thuc { get; set; }
    }

    public class ketqua_dieuchinh
    {
        public ok ok { get; set; }
    }
    public class ok
    {
        public string inv_InvoiceAuth_id { get; set; }
        public string inv_invoiceType { get; set; }
        public string inv_InvoiceCode_id { get; set; }
        public string inv_invoiceSeries { get; set; }
        public string inv_invoiceNumber { get; set; }
        public string inv_invoiceName { get; set; }
        public string inv_invoiceIssuedDate { get; set; }
        public string inv_submittedDate { get; set; }
        public string inv_contractNumber { get; set; }
        public string inv_contractDate { get; set; }
        public string inv_currencyCode { get; set; }
        public string inv_exchangeRate { get; set; }
        public string inv_invoiceNote { get; set; }
        public string inv_adjustmentType { get; set; }
        public string inv_originalInvoiceId { get; set; }
        public string inv_additionalReferenceDes { get; set; }
        public string inv_additionalReferenceDate { get; set; }
        public string inv_buyerDisplayName { get; set; }
        public string ma_dt { get; set; }
        public string inv_buyerLegalName { get; set; }
        public string inv_buyerTaxCode { get; set; }
        public string inv_buyerAddressLine { get; set; }
        public string inv_buyerEmail { get; set; }
        public string inv_buyerBankAccount { get; set; }
        public string inv_buyerBankName { get; set; }
        public string inv_paymentMethodName { get; set; }
        public string inv_sellerBankAccount { get; set; }
        public string inv_sellerBankName { get; set; }
        public string inv_discountAmount { get; set; }
        public string trang_thai { get; set; }
        public string user_new { get; set; }
        public string date_new { get; set; }
        public string user_edit { get; set; }
        public string date_edit { get; set; }
        public string ma_dvcs { get; set; }
        public string database_code { get; set; }
        public string ma_ct { get; set; }
        public string signedDate { get; set; }
        public string submittedDate { get; set; }
        public string mau_hd { get; set; }
        public string so_benh_an { get; set; }
        public string sovb { get; set; }
        public string ngayvb { get; set; }
        public string ghi_chu { get; set; }
        public string so_hd_dc { get; set; }
        public string inv_originalId { get; set; }
        public string signature { get; set; }
        public string dieu_tri { get; set; }
        public string ma1 { get; set; }
        public string inv_itemCode { get; set; }
        public string inv_itemName { get; set; }
        public string inv_unitCode { get; set; }
        public string inv_unitName { get; set; }
        public string inv_unitPrice { get; set; }
        public string inv_quantity { get; set; }
        public string inv_TotalAmountWithoutVat { get; set; }
        public string inv_vatPercentage { get; set; }
        public string inv_vatAmount { get; set; }
        public string inv_TotalAmount { get; set; }
        public string inv_invoiceNumber1 { get; set; }
        public string nguoi_ky { get; set; }
        public string sobaomat { get; set; }
        public string trang_thai_hd { get; set; }
        public string in_chuyen_doi { get; set; }
        public string ngay_ky { get; set; }
        public string nguoi_in_cdoi { get; set; }
        public string ngay_in_cdoi { get; set; }
        public string inv_deliveryOrderNumber { get; set; }
        public string inv_deliveryOrderDate { get; set; }
        public string inv_deliveryBy { get; set; }
        public string inv_transportationMethod { get; set; }
        public string inv_fromWarehouseName { get; set; }
        public string inv_toWarehouseName { get; set; }
        public string inv_sobangke { get; set; }
        public string inv_ngaybangke { get; set; }
        public string key_api { get; set; }
    }
    public class ketqua_hoadon
    {
        /// <summary>
        /// Mã loại hóa đơn; WIN00187: Hóa đơn giá trị gia tăng 01GTKT; WIN00189: Hóa đơn bán hàng giá trị trực tiếp 02GTTT; 
        /// WIN00192:Hóa đơn bán hàng (07KPTQ); WIN00193: Biên lai thu tiền phí, lệ phí (01BLP); WIN00194: Phiếu xuất kho nội bộ (03XKNB)
        /// WIN00194: 
        /// </summary>
        public string windowid { get; set; }
        /// <summary>
        /// kết quả 
        /// </summary>
        public string ok { get; set; }
        /// <summary>
        /// Dữ liệu kết quả trả về
        /// </summary>
        public data_ketqua data { get; set; }
    }
    public class ketqua_hoadon78
    {
        /// <summary>
        /// Mã loại hóa đơn; WIN00187: Hóa đơn giá trị gia tăng 01GTKT; WIN00189: Hóa đơn bán hàng giá trị trực tiếp 02GTTT; 
        /// WIN00192:Hóa đơn bán hàng (07KPTQ); WIN00193: Biên lai thu tiền phí, lệ phí (01BLP); WIN00194: Phiếu xuất kho nội bộ (03XKNB)
        /// WIN00194: 
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// kết quả 
        /// </summary>
        public string message { get; set; }

        /// <summary>
        /// Dữ liệu kết quả trả về
        /// </summary>
        public data_ketqua78 data { get; set; }

        public string ok { get; set; }
    }
    public class data_ketqua78
    {
        public string hoadon68_id { get; set; }
        public string cctbao_id { get; set; }
        public string hdlket_id { get; set; }
        public string tthai { get; set; }
        public string tthdon { get; set; }
        public string khieu { get; set; }
        public string shdon { get; set; }
        public string tdlap { get; set; }
        public string dvtte { get; set; }
        public string tgia { get; set; }
        public string gchu { get; set; }
        public string tnmua { get; set; }
        public string mnmua { get; set; }
        public string ten { get; set; }
        public string mst { get; set; }
        public string dchi { get; set; }
        public string email { get; set; }
        public string sdtnmua { get; set; }
        public string stknmua { get; set; }
        public string htttoan { get; set; }
        public string stknban { get; set; }
        public string sbmat { get; set; }
        public string mdvi { get; set; }
        public string nglap { get; set; }
        public string nlap { get; set; }
        public string ngsua { get; set; }
        public string nsua { get; set; }
        public string tgtcthue { get; set; }
        public string tgtthue { get; set; }
        public string ttcktmai { get; set; }
        public string tgtttbso { get; set; }
        public string tgtttbchu { get; set; }
        public string dlqrcode { get; set; }
        public string sdhang { get; set; }
        public string shdon1 { get; set; }
        public string mccqthue { get; set; }
        public string ngky { get; set; }
        public string nky { get; set; }
        public string signature { get; set; }
        public string hthdbtthe { get; set; }
        public string tdlhdbtthe { get; set; }
        public string khmshdbtthe { get; set; }
        public string khhdbtthe { get; set; }
        public string shdbtthe { get; set; }
        public string tgtphi { get; set; }
        public string tgtcthue0 { get; set; }
        public string tgtthue0 { get; set; }
        public string ttcktmai0 { get; set; }
        public string tgtttbso0 { get; set; }
        public string tgtcthue5 { get; set; }
        public string tgtthue5 { get; set; }
        public string ttcktmai5 { get; set; }
        public string tgtttbso5 { get; set; }
        public string tgtcthue10 { get; set; }
        public string tgtthue10 { get; set; }
        public string ttcktmai10 { get; set; }
        public string tgtttbso10 { get; set; }
        public string tgtcthuekct { get; set; }
        public string tgtthuekct { get; set; }
        public string ttcktmaikct { get; set; }
        public string tgtttbsokct { get; set; }
        public string tgtcthuekkk { get; set; }
        public string tgtthuekkk { get; set; }
        public string ttcktmaikkk { get; set; }
        public string tgtttbsokkk { get; set; }
        public string tgtphi0 { get; set; }
        public string tgtphi5 { get; set; }
        public string tgtphi10 { get; set; }
        public string tgtphikct { get; set; }
        public string tgtphikkk { get; set; }
        public string lhdon { get; set; }
        public string lddnbo { get; set; }
        public string tnvchuyen { get; set; }
        public string ptvchuyen { get; set; }
        public string dckhoxuat { get; set; }
        public string dckhonhap { get; set; }
        public string tennguoinhanhang { get; set; }
        public string mstnguoinhanhang { get; set; }
        public string phongban { get; set; }
        public string veviec { get; set; }
        public string sohopdong { get; set; }
        public string hdon68_id_lk { get; set; }
        public string phanHoiCQT { get; set; }
        public string ngay_duyet { get; set; }
        public string nguoi_duyet { get; set; }
        public string tgtcthuekhac { get; set; }
        public string ttcktmaikhac { get; set; }
        public string tgtthuekhac { get; set; }
        public string tgtttbsokhac { get; set; }
        public string hvtnxhang { get; set; }
        public string sovb { get; set; }
        public string ghi_chu { get; set; }
        public string ngayvb { get; set; }
        public string is_bangtonghop { get; set; }
        public string is_tthdon { get; set; }
        public string is_mau04 { get; set; }
        public string sbke { get; set; }
        public string nbke { get; set; }
        public string ma_dvcs { get; set; }
        public string nganhang_ngmua { get; set; }
        public string nganhang_ngban { get; set; }
        public string matdiep { get; set; }
        public string maloaitdiep { get; set; }
        public string matdtc { get; set; }
        public string macqt { get; set; }
        public string hdktso { get; set; }
        public string hdktngay { get; set; }
        public string ngay_hd32 { get; set; }
        public string mauso_hd32 { get; set; }
        public string kyhieu_hd32 { get; set; }
        public string so_hd32 { get; set; }
        public string loai_hd32 { get; set; }
        public string tinhchat_hd32 { get; set; }
        public string is_success { get; set; }
        public string note_error { get; set; }
        public string tgtcthue8 { get; set; }
        public string ttcktmai8 { get; set; }
        public string tgtttbso8 { get; set; }
        public string tlptdoanhthu20 { get; set; }
        public string tgtck20 { get; set; }
        public string txtck20 { get; set; }
        public string kptquan { get; set; }
        public string id { get; set; }
        public string inv_invoiceNumber { get; set; }
        public string inv_invoiceSeries { get; set; }

    }
    public class data_ketqua
    {
        public string inv_InvoiceAuth_id { get; set; }
        public string inv_invoiceType { get; set; }
        public string inv_InvoiceCode_id { get; set; }
        public string inv_invoiceSeries { get; set; }
        public string inv_invoiceNumber { get; set; }
        public string inv_invoiceName { get; set; }
        public string inv_invoiceIssuedDate { get; set; }
        public string inv_submittedDate { get; set; }
        public string inv_contractNumber { get; set; }
        public string inv_contractDate { get; set; }
        public string inv_currencyCode { get; set; }
        public string inv_exchangeRate { get; set; }
        public string inv_invoiceNote { get; set; }
        public string inv_adjustmentType { get; set; }
        public string inv_originalInvoiceId { get; set; }
        public string inv_additionalReferenceDes { get; set; }
        public string inv_additionalReferenceDate { get; set; }
        public string inv_buyerDisplayName { get; set; }
        public string ma_dt { get; set; }
        public string inv_buyerLegalName { get; set; }
        public string inv_buyerTaxCode { get; set; }
        public string inv_buyerAddressLine { get; set; }
        public string inv_buyerEmail { get; set; }
        public string inv_buyerBankAccount { get; set; }
        public string inv_buyerBankName { get; set; }
        public string inv_paymentMethodName { get; set; }
        public string inv_sellerBankAccount { get; set; }
        public string inv_sellerBankName { get; set; }
        public string inv_discountAmount { get; set; }
        public string trang_thai { get; set; }
        public string user_new { get; set; }
        public string date_new { get; set; }
        public string user_edit { get; set; }
        public string ma_dvcs { get; set; }
        public string database_code { get; set; }
        public string ma_ct { get; set; }
        public string signedDate { get; set; }
        public string submittedDate { get; set; }
        public string mau_hd { get; set; }
        public string so_benh_an { get; set; }
        public string sovb { get; set; }
        public string ngayvb { get; set; }
        public string ghi_chu { get; set; }
        public string so_hd_dc { get; set; }
        public string inv_originalId { get; set; }
        public string signature { get; set; }
        public string dieu_tri { get; set; }
        public string ma1 { get; set; }
        public string inv_itemCode { get; set; }
        public string inv_itemName { get; set; }
        public string inv_unitCode { get; set; }
        public string inv_unitName { get; set; }
        public string inv_unitPrice { get; set; }
        public string inv_quantity { get; set; }
        public string inv_TotalAmountWithoutVat { get; set; }
        public string inv_vatPercentage { get; set; }
        public string inv_vatAmount { get; set; }
        public string inv_TotalAmount { get; set; }
        public string nguoi_ky { get; set; }
        public string sobaomat { get; set; }
        public string trang_thai_hd { get; set; }
        public string in_chuyen_doi { get; set; }
        public string ngay_ky { get; set; }
        public string nguoi_in_cdoi { get; set; }
        public string ngay_in_cdoi { get; set; }
        public string inv_deliveryOrderNumber { get; set; }
        public string inv_deliveryOrderDate { get; set; }
        public string inv_deliveryBy { get; set; }
        public string inv_transportationMethod { get; set; }
        public string inv_fromWarehouseName { get; set; }
        public string inv_toWarehouseName { get; set; }
        public string inv_sobangke { get; set; }
        public string inv_ngaybangke { get; set; }
        public string key_api { get; set; }
        public string id { get; set; }

    }

    public class phathanhchitiet
    {
        public string ctthongbao_id { get; set; }
        public string dpthongbao_id { get; set; }
        public string dmmauhoadon_id { get; set; }
        public string ma_loai { get; set; }
        public string ten_loai { get; set; }
        public string mau_so { get; set; }
        public string ky_hieu { get; set; }
        public string so_luong { get; set; }
        public string tu_so { get; set; }
        public string den_so { get; set; }
        public string ngay_bd_sd { get; set; }
        public string ten_dn { get; set; }
        public string mst { get; set; }
        public string hop_dong_so { get; set; }
        public string ngay_hop_dong { get; set; }
        public string so_lien { get; set; }
        public string so_hd { get; set; }
        public string loai_hd { get; set; }
        public string loai_thanh_toan { get; set; }
        public string id { get; set; }
        public string value { get; set; }

    }

    public class danhsachphathanh
    {
        public List<phathanhchitiet> _Phathanhchitiets { get; set; }
    }
    #endregion

    public class InvoiceStatusInRefID
    {
        /// <summary>
        /// refid hóa đơn
        /// </summary>
        public string RefID { get; set; }

        /// <summary>
        /// Số hóa đơn
        ///
        /// </summary>
        public string InvNo { get; set; }

        /// <summary>
        /// Ngày hóa đơn
        ///
        /// </summary>
        public DateTime InvDate { get; set; }

        /// <summary>
        /// Ký hiệu háo đơn
        /// </summary>
        public string InvSeries { get; set; }

        /// <summary>
        /// Mẫu số HD
        /// </summary>
        public string InvTempl { get; set; }

        /// <summary>
        /// Mã tra cứu HD
        /// </summary>
        public string TransactionID { get; set; }

        /// <summary>
        /// Trạng thái phát hành, mặc định là đã phát hành
        /// </summary>
        public int PublishStatus { get; set; }

        /// <summary>
        /// Trạng thái HD 1: Gốc , 2- Xóa
        /// </summary>
        public int EInvoiceStatus { get; set; }

        /// <summary>
        /// loại HD 0 : Gốc, 1 – Thay thế , 2 – điều chỉnh
        /// </summary>
        public int ReferenceType { get; set; }

        /// <summary>
        /// Mã của cơ quan thuế cấp
        /// </summary>
        public string InvoiceCode { get; set; }

        /// <summary>
        /// Thông điệp cơ quan thuế
        /// </summary>
        public string MessageCode { get; set; }

        /// <summary>
        /// Nguồn phát hành của hóa đơn
        /// </summary>
        public string SourceType { get; set; }

        public string SendTaxStatus { get; set; }

        /// <summary>
        /// Trạng thái đã gửi email hay chưa (0: Chưa gửi; 1: Đã gửi)
        /// </summary>
        public bool IsSentEmail { get; set; }

        /// <summary>
        /// Hóa đơn bị hủy hay chưa
        /// </summary>
        public string IsDelete { get; set; }

        /// <summary>
        /// Ngày hủy
        /// </summary>
        public string DeletedDate { get; set; }

        /// <summary>
        /// Lý do hủy
        /// </summary>
        public string DeletedReason { get; set; }

        public string ReceivedStatus { get; set; }

        /// <summary>
        /// Mã tra cứu của hđ gốc (nếu hđ này là hđ điều chỉnh/thay thê)
        /// </summary>
        public string OrgTransactionID { get; set; }

        /// <summary>
        /// Trạng thái của hđ gốc
        /// </summary>
        public int OrgInvoiceStatus { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string RefInvoiceStatus { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string SummaryStatus { get; set; }
    }

    public class DataCancelMisa
    {
        public string TransactionID { get; set; }

        public string InvNo { get; set; }

        public string RefDate { get; set; }

        public string CancelReason { get; set; }
    }
}
